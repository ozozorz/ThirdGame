using UnityEngine;

public class CheckSoundBehaviour : MonoBehaviour
{
    public AlertBehaviour alertBehaviour;

    public float range = 5f;
    Vector3 _checkPos;

    public NavigationWalkBehaviour navigationWalk;

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
    }

    public void ReceiveSoundEvent(Vector3 pos)
    {
        Debug.Log("ReceiveSoundEvent");
        if (IsInDisableReactSoundState())
            return;

        var dist = (transform.position - pos).magnitude;
        Debug.Log("dist " + dist);
        if (dist > range)
            return;

        _checkPos = pos;
        checkState = CheckState.GoingTo;
        navigationWalk.GoToDestination(_checkPos);
    }

    bool IsInDisableReactSoundState()
    {
        if (alertBehaviour.foundPlayer)
            return true;

        return false;
    }
}