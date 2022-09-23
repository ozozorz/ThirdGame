using UnityEngine;
using System.Collections.Generic;

public class EnemySystem : MonoBehaviour
{
    public static EnemySystem instance;

    public List<EnemyBehaviour> enemies;

    private void Awake()
    {
        instance = this;
    }

}
