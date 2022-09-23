using UnityEngine;
using UnityEngine.AI;

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

    public NavMeshAgent meshAgent;
    public Transform destination;

    public CharacterAnimationBehaviour characterAnimation;

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
            GoToDestination(patrolPoints[0].pos.position);
        }
    }

    void OnPatrolArrived()
    {
        _waitTimer = patrolPoints[_patrolIndex].waitTime;
        _patrolIndex++;
        if (_patrolIndex >= patrolPoints.Length)
            _patrolIndex = 0;
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
                GoToDestination(patrolPoints[_patrolIndex].pos.position);
            }
            return;
        }

        CheckStopNav();
    }

    void CheckStopNav()
    {
        if (meshAgent.remainingDistance < 0.05f && meshAgent.desiredVelocity.magnitude < 0.15f)
        {
            characterAnimation.StopWalk();
            OnPatrolArrived();
        }
    }

    public void GoToDestination(Vector3 p)
    {
        destination.transform.position =p;
        meshAgent.SetDestination(destination.position);
        characterAnimation.StartWalk();
    }

    public void ForceStop()
    {
        meshAgent.SetDestination(transform.position);
        meshAgent.isStopped = true;
        characterAnimation.StopWalk();
    }
}