using UnityEngine;

/// <summary>
/// 检测声音，并让角色去侦查
/// </summary>
public class CheckSoundBehaviour : MonoBehaviour
{
    /// <summary>
    /// 警戒系统，听到声音不是警戒，发现敌人才是警戒
    /// </summary>
    [HideInInspector]
    public AlertBehaviour alertBehaviour;
    /// <summary>
    /// 巡逻系统，负责巡逻路线
    /// </summary>
    [HideInInspector]
    public PatrolBehaviour patrol;
    /// <summary>
    /// 和NavMeshAgent沟通的系统，复杂使用NavMesh的ai做寻路，以及角色的走路动画
    /// </summary>
    [HideInInspector]
    public NavigationWalkBehaviour navigationWalk;

    public float range = 5f;//能听到的半径
    Vector3 _checkPos;//声音来源位置
    Vector3 _leavePos;//发现后开始去侦察时，离开的位置。保存这个位置是为了侦查结束后回到这里
    public float waitTime=3f;//在声音来源附近停留的时长
    public float checkDistance = 2.5f;//在声音来源附近停留的位置和声音来源的距离
    public enum CheckState
    {
        None,
        GoingTo,
        Wait,
        Back,
    }
    public CheckState checkState { get; private set; }//侦查的状态
    float _stateWaitTimestamp;//将会切换到下个状态的时间

    private void Awake()
    {
        checkState = CheckState.None;
        alertBehaviour = GetComponent<AlertBehaviour>();
        patrol = GetComponent<PatrolBehaviour>();
        navigationWalk = GetComponent<NavigationWalkBehaviour>();
    }

    /// <summary>
    /// 尝试接受一个可疑的声音事件
    /// </summary>
    /// <param name="pos">声音来源位置</param>
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

    /// <summary>
    /// 是否处于无法响应可疑声音的状态
    /// 目前是警戒状态优先于响应可疑声音
    /// </summary>
    /// <returns></returns>
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