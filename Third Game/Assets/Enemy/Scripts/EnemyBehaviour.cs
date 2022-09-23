using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    protected Animator animator;
    protected CharacterController cc;

    void Start()
    {
        EnemySystem.instance.enemies.Add(this);
    }

}
