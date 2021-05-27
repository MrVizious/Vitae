using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData player;

    private Rigidbody2D rb;

    private Camera mainCam;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    private void Update() {
        UpdateLookDirection();
    }
    private void FixedUpdate() {
        Move();
    }


    public void Move() {
        if (!player.isDashing)
        {
            rb.MovePosition((Vector2)transform.position + player.movementDirection * player.movementSpeed * Time.fixedDeltaTime);
        }
    }

    public void MovementDirectionChanged(InputAction.CallbackContext context) {
        if (Time.timeScale > 0f) player.movementDirection = context.ReadValue<Vector2>();
    }

    private void UpdateLookDirection() {
        if (Time.timeScale > 0f)
        {
            Vector3 worldPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 diff = (Vector3)worldPos - transform.position;
            diff.Normalize();

            player.lookDirection = diff;
        }
    }


    public void DebugMouse(InputAction.CallbackContext context) {
        Debug.Log(context.ReadValue<Vector2>());
    }


}
