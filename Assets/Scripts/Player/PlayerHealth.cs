using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool debug = false;
    public PlayerData player;
    [SerializeField] private float currentHealth;

    private void Start() {
        currentHealth = player.maxHealth;
    }

    public void Damage(float damageAmount) {
        if (debug) Debug.Log("Getting " + damageAmount + " points of damage");
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
