using UnityEngine;

public class CheckSoundBehaviour : MonoBehaviour
{
    [HideInInspector]
    public AlertBehaviour alertBehaviour;
    [HideInInspector]
    public PatrolBehaviour patrol;
    [HideInInspector]
    public NavigationWalkBehaviour navigationWalk;

    public float range = 5f;
    Vector3 _checkPos;
    Vector3 _leavePos;
    public float waitTime=3f;
    public float checkDistance = 2.5f;
    public enum CheckState
    {
        None,
        GoingTo,
        Wait,
        Back,
    }
    public CheckState checkState { get; private set; }
    float _stateWaitTimestamp;

    private void Awake()
    {
        checkState = CheckState.None;
        alertBehaviour = GetComponent<AlertBehaviour>();
        patrol = GetComponent<PatrolBehaviour>();
        navigationWalk = GetComponent<NavigationWalkBehaviour>();
    }

    public void ReceiveSoundEvent(Vector3 pos)
    {
        if (IsInDisableReactSoundState())
            return;

        var dist = (transform.position - pos).magnitude;
        Debug.Log("ReceiveSoundEvent dist " + dist);
        if (dist > range)
            return;

        _checkPos = pos;
        _leavePos = transform.position;
        checkState = CheckState.GoingTo;
        navigationWalk.GoToDestination(_checkPos);
    }

    bool IsInDisableReactSoundState()
    {
        if (alertBehaviour.foundPlayer)
            return true;

        return false;
    }

    private void Update()
    {
        if (alertBehaviour.foundPlayer)
            return;

        if (checkState == CheckSoundBehaviour.CheckState.None)
            return;

        if (checkState == CheckSoundBehaviour.CheckState.GoingTo)
        {
            var dir = transform.position - _checkPos;
            dir.y = 0;
            var dist = dir.magnitude;
            if (dist <= checkDistance)
            {
                checkState = CheckState.Wait;
                _stateWaitTimestamp = Time.time + waitTime;
            }
            return;
        }
        if (checkState == CheckSoundBehaviour.CheckState.Wait)
        {
            if (Time.time> _stateWaitTimestamp)
            {
                checkState = CheckSoundBehaviour.CheckState.Back;
                navigationWalk.GoToDestination(_leavePos);
            }
            return;
        }
        if (checkState == CheckSoundBehaviour.CheckState.Back)
        {
            var dist = (transform.position - _leavePos).magnitude;
            if (dist < 0.5f)
            {
                checkState = CheckState.None;
                patrol.ContinuePatrol();
            }
            return;
        }
    }
}