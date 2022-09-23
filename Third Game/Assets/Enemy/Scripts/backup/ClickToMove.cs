using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ClickToMove : MonoBehaviour
{
    public AlertBehaviour alertBehaviour;

    public Transform[] patrolPoints;

    public NavMeshAgent meshAgent;
    public Transform destination;

    public static ClickToMove instance;

    private bool _forceStopFlag;

    public CharacterAnimationBehaviour characterAnimation;

    public Transform startDest;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        if (startDest != null)
        {
            destination.transform.position = startDest.position;
            GoToDestination();
        }
    }

    private void Update()
    {
        CheckStopNav();
    }

    void CheckStopNav()
    {
        if (meshAgent.remainingDistance < 0.05f && meshAgent.desiredVelocity.magnitude < 0.15f)
        {
            characterAnimation.StopWalk();
        }
    }

    public void GoToDestination()
    {
        //ClearLastPath();
        meshAgent.SetDestination(destination.position);
        characterAnimation.StartWalk();
        //DrawPath();
    }

    public void ForceStop(bool b)
    {
        _forceStopFlag = b;
        meshAgent.isStopped = b;

        if (_forceStopFlag)
        {
            characterAnimation.StopWalk();
        }
    }

    public void Stop()
    {
        meshAgent.SetDestination(transform.position);
    }

    public void CheckClick()
    {
        if (_forceStopFlag)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                destination.transform.position = hit.point;
                GoToDestination();
            }
        }
    }
}