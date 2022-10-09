using UnityEngine;

/// <summary>
/// 管理敌人的入口类，方便关卡管理的类了解敌人的整体情况（数量，死活）
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
    protected Animator animator;
    protected CharacterController cc;

    void Start()
    {
        EnemySystem.instance.enemies.Add(this);
    }
}