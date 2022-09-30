using UnityEngine;
using UnityEngine.AI;

/// <summary>
///  和NavMeshAgent沟通的系统，复杂使用NavMesh的ai做寻路，以及角色的走路动画
/// </summary>
public class NavigationWalkBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    Vector3 _destination;
    [HideInInspector]
    public PatrolBehaviour patrol;
    [HideInInspector]
    public LivingEntity livingEntity;

    public float tickTime = 0.4f;
    private float _nextTickTime;

    private void Awake()
    {
        patrol = GetComponent<PatrolBehaviour>();
        livingEntity = GetComponent<LivingEntity>();
        _nextTickTime = 0;
    }

    public void StopWalk()
    {
        //Debug.Log("StopWalk");
        livingEntity.animator.SetBool("crouch", false);
    }

    public void StartWalk()
    {
        //Debug.LogWarning("StartWalk");
        livingEntity.animator.SetBool("crouch", true);
    }

    private void Update()
    {
        if (Time.time>= _nextTickTime)
        {
            _nextTickTime = Time.time + tickTime;
            CheckStopNav();
        }
    }

    void CheckStopNav()
    {
        if (agent.remainingDistance < 0.05f && agent.desiredVelocity.magnitude < 0.15f)
        {
            //Debug.Log(agent.destination);
            StopWalk();
        }
    }

    public void GoToDestination(Vector3 p)
    {
        //Debug.LogWarning("GoToDestination"+ p);
        _destination = p;
        agent.SetDestination(_destination);
        StartWalk();
        _nextTickTime = Time.time + tickTime;
    }

    public void ForceStop()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        StopWalk();
    }
}
