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
        animator = GetComponentInChildren<Animator>();
        EnemySystem.instance.enemies.Add(this);
    }

    public void Stand()
    {
        animator.SetBool("向前走", false);
        animator.SetBool("向后退", false);
        animator.SetBool("向右走", false);
        animator.SetBool("向左走", false);
        Debug.Log("Stand");
        animator.SetTrigger("stand");
    }
}