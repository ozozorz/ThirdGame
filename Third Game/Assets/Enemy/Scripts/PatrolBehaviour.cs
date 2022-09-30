using UnityEngine;

/// <summary>
/// 巡逻系统，负责巡逻路线
/// </summary>
public class PatrolBehaviour : MonoBehaviour
{
    [HideInInspector]
    public AlertBehaviour alertBehaviour;
    [HideInInspector]
    public CheckSoundBehaviour checkSound;

    public Animator animator;

    public Vector3 _startPos;

    private void Awake()
    {
        alertBehaviour = GetComponent<AlertBehaviour>();
        checkSound = GetComponent<CheckSoundBehaviour>();
    }

    /// <summary>
    /// 巡逻点的定义
    /// </summary>
    [System.Serializable]
    public class PatrolPoint
    {
        /// <summary>
        /// 巡逻点的transform
        /// </summary>
        public Transform pos;
        /// <summary>
        /// 在这个点停留的时间
        /// </summary>
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
        
        // zk
        // animator.SetBool("向前走", true);
    }

    public void OnPatrolArrived()
    {
        _waitTimer = patrolPoints[_patrolIndex].waitTime;
        if (_waitTimer <= 0)
        {
            //这里强制至少留了一定的停留时间，这样看起来比较自然
            _waitTimer = 0.5f;
        }

        _patrolIndex++;
        if (_patrolIndex >= patrolPoints.Length)
            _patrolIndex = 0;
        
        // zk
        // animator.SetBool("向前走", false);
    }
}