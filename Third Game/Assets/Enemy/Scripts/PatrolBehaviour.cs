using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    [HideInInspector]
    public AlertBehaviour alertBehaviour;
    [HideInInspector]
    public CheckSoundBehaviour checkSound;

    public Vector3 _startPos;

    private void Awake()
    {
        alertBehaviour = GetComponent<AlertBehaviour>();
        checkSound = GetComponent<CheckSoundBehaviour>();
    }

    [System.Serializable]
    public class PatrolPoint
    {
        public Transform pos;
        public float waitTime = 1f;
    }

    public PatrolPoint[] patrolPoints;
    public NavigationWalkBehaviour navigationWalk;

    int _patrolIndex;
    bool _hasPatrolPoints;
    float _waitTimer;

    public void Start()
    {
        _startPos = transform.position;
        _hasPatrolPoints = false;
        _waitTimer = 0;
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            _hasPatrolPoints = true;
            _patrolIndex = 0;
        }

        if (_hasPatrolPoints)
        {
            navigationWalk.GoToDestination(patrolPoints[0].pos.position);
        }
    }

    private void Update()
    {
        if (alertBehaviour.foundPlayer)
            return;
        if (checkSound.checkState != CheckSoundBehaviour.CheckState.None)
            return;
        if (!_hasPatrolPoints)
            return;

        if (_waitTimer > 0)
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0)
            {
                navigationWalk.GoToDestination(patrolPoints[_patrolIndex].pos.position);
            }
            return;
        }

        var dir = transform.position - patrolPoints[_patrolIndex].pos.position;
        dir.y = 0;
        var dist = dir.magnitude;
        if (dist < 0.75f)
        {
            OnPatrolArrived();
        }
    }

    public void ContinuePatrol()
    {
        if (!_hasPatrolPoints)
        {
            navigationWalk.GoToDestination(_startPos);
            return;
        }

        navigationWalk.GoToDestination(patrolPoints[_patrolIndex].pos.position);
    }

    public void OnPatrolArrived()
    {
        _waitTimer = patrolPoints[_patrolIndex].waitTime;
        if (_waitTimer <= 0)
        {
            _waitTimer = 0.5f;
        }

        _patrolIndex++;
        if (_patrolIndex >= patrolPoints.Length)
            _patrolIndex = 0;
    }
}