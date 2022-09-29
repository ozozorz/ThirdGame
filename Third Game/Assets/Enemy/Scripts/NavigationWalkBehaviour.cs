using UnityEngine;
using UnityEngine.AI;

public class NavigationWalkBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    Vector3 _destination;
    [HideInInspector]
    public PatrolBehaviour patrol;
    [HideInInspector]
    public LivingEntity livingEntity;

    private void Awake()
    {
        patrol = GetComponent<PatrolBehaviour>();
        livingEntity = GetComponent<LivingEntity>();
    }

    public void StopWalk()
    {
        livingEntity.animator.SetBool("crouch", false);
    }

    public void StartWalk()
    {
        livingEntity.animator.SetBool("crouch", true);
    }

    private void Update()
    {
        CheckStopNav();
    }

    void CheckStopNav()
    {
        if (agent.remainingDistance < 0.05f && agent.desiredVelocity.magnitude < 0.15f)
        {
            StopWalk();
        }
    }

    public void GoToDestination(Vector3 p)
    {
        _destination = p;
        agent.SetDestination(_destination);
        StartWalk();
    }

    public void ForceStop()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        StopWalk();
    }
}
