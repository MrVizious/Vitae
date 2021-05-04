using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public bool debug = false;
    public EnemyData data;

    [SerializeField] protected float currentHealth;

    protected virtual void Start() {
        currentHealth = data.maxHealth;
    }
    public virtual void Damage(float damageAmount) {
        currentHealth -= damageAmount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

}
