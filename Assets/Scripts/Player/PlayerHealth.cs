using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public bool debug = false;
    public PlayerData player;
    public UnityEvent onPlayerDie;
    public float currentHealth;

    private void Start() {
        Reset();
    }

    public void Damage(float damageAmount) {
        if (player.isDashing) return;
        if (debug) Debug.Log("Getting " + damageAmount + " points of damage");
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die() {
        onPlayerDie.Invoke();
    }

    public void Reset() {
        currentHealth = player.maxHealth;
    }
}