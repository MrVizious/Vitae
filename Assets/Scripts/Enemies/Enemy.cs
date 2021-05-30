using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public abstract class Enemy : MonoBehaviour
{
    public bool debug = false;
    public EnemyData data;
    public UnityEvent OnDie;

    public float currentHealth;

    protected AIPath path;
    protected Seeker seeker;
    protected AIDestinationSetter destinationSetter;

    protected virtual void Start() {

    }

    public virtual void Spawn(Transform newTarget) {

        // Get component references
        path = GetComponent<AIPath>();
        seeker = GetComponent<Seeker>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        OnDie = new UnityEvent();
        // Attribute setting
        currentHealth = data.maxHealth;
        if (path == null) { Debug.Log("Oh, crap, no path", this); }
        else path.maxSpeed = data.speed;

        SetTarget(newTarget);

    }

    public virtual void Damage(float damageAmount) {
        currentHealth -= damageAmount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die() {
        OnDie.Invoke();
        gameObject.SetActive(false);
    }

    public void SetTarget(Transform newTarget) {
        destinationSetter.target = newTarget;
    }

    public Transform GetTarget() {
        return destinationSetter.target;
    }

}
