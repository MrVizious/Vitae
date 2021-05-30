using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeBar : MonoBehaviour
{
    private Enemy enemy;
    private float maxHealth;
    private float currentPercentage;

    private void Start() {
        enemy = transform.parent.GetComponent<Enemy>();
        maxHealth = enemy.data.maxHealth;
    }

    private void Update() {
        currentPercentage = enemy.currentHealth / maxHealth;
        if (currentPercentage < 0f) currentPercentage = 0f;
        transform.localScale = new Vector3(
            currentPercentage * 0.2f,
            transform.localScale.y,
            transform.localScale.z
        );
    }

}
