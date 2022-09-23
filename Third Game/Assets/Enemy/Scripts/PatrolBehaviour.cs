using UnityEngine;


public class PatrolBehaviour : MonoBehaviour
{
    public AlertBehaviour alertBehaviour;
    public CheckSoundBehaviour checkSound;

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

        var dist = (transform.position - patrolPoints[_patrolIndex].pos.position).magnitude;
        if (dist < 0.5f)
        {
            OnPatrolArrived();
        }


    }

    public void OnPatrolArrived()
    {
        _waitTimer = patrolPoints[_patrolIndex].waitTime;
        _patrolIndex++;
        if (_patrolIndex >= patrolPoints.Length)
            _patrolIndex = 0;
    }
}