using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable {
    public float startingHealth;
    protected float health;
    protected bool dead;
    protected Animator animator;

    public virtual void Start() {
        health = startingHealth;
        animator = GetComponent<Animator>();
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
        animator.SetBool("死亡", true);
    }
}
