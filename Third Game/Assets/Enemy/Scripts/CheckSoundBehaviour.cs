using UnityEngine;
using UnityEngine.AI;

public class CheckSoundBehaviour : MonoBehaviour
{
    public AlertBehaviour alertBehaviour;

    public float range = 5f;
    public NavMeshAgent mna;
    Vector3 _checkPos;

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
        if (IsInDisableReactSoundState())
        {
            return;
        }

        _checkPos = pos;
        checkState = CheckState.GoingTo;
        mna.SetDestination(_checkPos);
    }

    bool IsInDisableReactSoundState()
    {
        if (alertBehaviour.foundPlayer)
            return true;

        return false;
    }

}
