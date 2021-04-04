using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerData player;
    [SerializeField] private float currentHealth;

    private void Start() {
        currentHealth = player.maxHealth;
    }

    public void Damage(float damageAmount) {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die() {
        //TODO: Implement event via ScriptableObject
        Destroy(gameObject);
        SceneController.QuitGame();
    }

}
