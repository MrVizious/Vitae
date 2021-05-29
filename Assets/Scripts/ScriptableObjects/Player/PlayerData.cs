using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{

    [Header("Health Data")]
    public float maxHealth;


    [Space(10)]


    [Header("Movement Variables")]
    public float movementSpeed = 5f;
    [HideInInspector] public Vector2 movementDirection;
    [HideInInspector] public Vector2 lookDirection;


    [Space(10)]


    [Header("Dash Variables")]
    public float dashDuration = 0.3f;
    public float dashSpeed = 80;

    [HideInInspector] public bool isDashing = false;

    private void Awake() {
        Reset();
    }

    public void Reset() {
        movementDirection = Vector2.zero;
        lookDirection = Vector2.right;
        isDashing = false;
    }
}
