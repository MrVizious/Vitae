using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public EnemyData enemy;

    private float currentHealth;

    private void Start() {
        currentHealth = enemy.maxHealth;
    }
    public void Damage(float damageAmount) {
        currentHealth -= damageAmount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
