using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity {
    public FieldOfView fov;
    public CheckSoundBehaviour csb;
    public NavigationWalkBehaviour nwb;
    public NavMeshAgent nma;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        fov = GetComponent<FieldOfView>();
        csb = GetComponent<CheckSoundBehaviour>();
        nwb = GetComponent<NavigationWalkBehaviour>();
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Die() {
        base.Die();
        fov.enabled = false;
        csb.enabled = false;
        nwb.enabled = false;
        nma.enabled = false;
    }
    
}
