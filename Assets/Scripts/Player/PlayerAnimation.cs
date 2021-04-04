using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerData player;
    private Animator animator;
    private Camera mainCam;
    private void Start() {
        mainCam = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void Update() {
        UpdateLookAnimation();
    }

    public void UpdateLookAnimation() {

        animator.SetFloat("Look_X", player.lookDirection.x);
        animator.SetFloat("Look_Y", player.lookDirection.y);

    }

    public void SetMovementSpeed(InputAction.CallbackContext context) {
        float speed = context.ReadValue<Vector2>().magnitude;
        animator.SetFloat("Speed", speed);
        Debug.Log(animator.GetFloat("Speed"));
    }
}