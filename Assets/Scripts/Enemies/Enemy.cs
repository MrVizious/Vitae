using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
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
