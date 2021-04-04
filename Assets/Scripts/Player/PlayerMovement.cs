using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float movementSpeed = 2f;

    [Space(10)]

    [Header("Dash Variables")]
    public float dashDuration = 0.3f;
    public float dashSpeed = 25f;
    public ParticleSystem dashParticles;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private Vector2 lookDirection;

    private IEnumerator dashCoroutine;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        dashCoroutine = null;
        lookDirection = transform.up;
    }

    private void FixedUpdate() {
        Move();
    }


    public void Move() {
        if (dashCoroutine == null)
        {
            rb.MovePosition((Vector2)transform.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
        }
    }

    public void MovementDirectionChanged(InputAction.CallbackContext context) {
        Debug.Log("Input: " + context.ReadValue<Vector2>());
        movementDirection = context.ReadValue<Vector2>();
    }

    public void Dash(InputAction.CallbackContext context) {
        Debug.Log("Dash started!");
        if (context.started && dashCoroutine == null)
        {
            Vector2 dashDirection = movementDirection;
            if (dashDirection.magnitude < 0.1f) dashDirection = lookDirection;
            dashCoroutine = DashCoroutine(dashDirection, dashDuration, dashSpeed);
            StartCoroutine(dashCoroutine);
        }
        else
        {
            Debug.Log("Can't dash yet!");
        }
    }

    private IEnumerator DashCoroutine(Vector2 direction, float duration, float speed) {
        Debug.Log("Dash initiated!");
        dashParticles.Play();
        float timeSinceDashStarted = 0f;
        while (timeSinceDashStarted < duration)
        {
            rb.MovePosition((Vector2)transform.position + direction.normalized * speed * Time.deltaTime);
            yield return null;
            timeSinceDashStarted += Time.deltaTime;
        }
        EndDash();
    }

    public void EndDash() {
        StopCoroutine(dashCoroutine);
        dashCoroutine = null;
        dashParticles.Stop();
    }

    public void DebugMouse(InputAction.CallbackContext context) {
        Debug.Log(context.ReadValue<Vector2>());
    }

}
