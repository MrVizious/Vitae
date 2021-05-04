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

    protected AIPath path;
    protected Seeker seeker;
    protected AIDestinationSetter destinationSetter;

    protected virtual void Start() {

        // Get component references
        path = GetComponent<AIPath>();
        seeker = GetComponent<Seeker>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        // Attribute setting
        currentHealth = data.maxHealth;
        path.maxSpeed = data.speed;

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

    public void SetTarget(Transform newTarget) {
        destinationSetter.target = newTarget;
    }

    public Transform GetTarget() {
        return destinationSetter.target;
    }

}
