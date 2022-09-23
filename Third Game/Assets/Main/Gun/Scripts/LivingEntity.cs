using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable {
    public float startingHealth;
    protected float health;
    protected bool dead;
    protected Animator animator;
    protected CharacterController cc;

    public virtual void Start() {
        health = startingHealth;
        animator = GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();
    }

    public void TakeHit(float damage, RaycastHit hit) {
        health -= damage;
        if (health <= 0 && !dead) {
            Die();
        }
    }

    public void Die() {
        dead = true;
        // Destroy(gameObject);
        cc.enabled = false;
        animator.SetBool("死亡", true);
    }
}
