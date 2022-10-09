using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public Behaviour[] dieDisables;
    public GameObject[] dieDisactives;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Die()
    {
        base.Die();

        foreach (var d in dieDisables)
            d.enabled = false;
        foreach (var d in dieDisactives)
            d.SetActive(false);
    }
}
