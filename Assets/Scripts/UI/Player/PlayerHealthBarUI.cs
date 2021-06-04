using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    public PlayerHealth health;
    private Image healthBar;

    private void Start() {
        healthBar = GetComponent<Image>();
    }
    void Update() {
        healthBar.fillAmount = health.currentHealth / health.player.maxHealth;
    }
}
