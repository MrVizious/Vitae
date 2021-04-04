using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public EnemyData enemy;

    [SerializeField] private float currentHealth;

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

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>()?.Damage(enemy.contactDamage);
        }
    }
}
