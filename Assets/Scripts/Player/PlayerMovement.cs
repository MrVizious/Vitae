using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData player;
    public ParticleSystem dashParticles;

    private Rigidbody2D rb;

    private IEnumerator dashCoroutine;
    private Camera mainCam;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        dashCoroutine = null;
    }

    private void Update() {
        UpdateLookDirection();
    }
    private void FixedUpdate() {
        Move();
    }


    public void Move() {
        if (dashCoroutine == null)
        {
            rb.MovePosition((Vector2)transform.position + player.movementDirection * player.movementSpeed * Time.fixedDeltaTime);
        }
    }

    public void MovementDirectionChanged(InputAction.CallbackContext context) {
        player.movementDirection = context.ReadValue<Vector2>();
    }

    private void UpdateLookDirection() {
        Vector3 worldPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 diff = (Vector3)worldPos - transform.position;
        diff.Normalize();

        player.lookDirection = diff;
    }

    public void Dash(InputAction.CallbackContext context) {
        Debug.Log("Dash started!");
        if (context.started && dashCoroutine == null)
        {
            Vector2 dashDirection = player.movementDirection;
            if (dashDirection.magnitude < 0.1f) dashDirection = player.lookDirection;
            dashCoroutine = DashCoroutine(dashDirection, player.dashDuration, player.dashSpeed);
            StartCoroutine(dashCoroutine);
        }
        else
        {
            Debug.Log("Can't dash yet!");
        }
    }

    private IEnumerator DashCoroutine(Vector2 direction, float duration, float speed) {
        Debug.Log("Dash initiated!");

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyProjectile"), true);
        Debug.Log("Collisions DISABLED between layers " + LayerMask.NameToLayer("Player") + " and " + LayerMask.NameToLayer("Enemy"));

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
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
            dashCoroutine = null;

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyProjectile"), false);
            Debug.Log("Collisions ENABLED between layers " + LayerMask.NameToLayer("Player") + " and " + LayerMask.NameToLayer("Enemy"));

            //TODO: Check if user is on pit when ending (https://docs.unity3d.com/ScriptReference/Physics2D.OverlapPoint.html)
            Debug.Log("Collisions ENABLED between layers " + LayerMask.NameToLayer("Player") + " and " + LayerMask.NameToLayer("Enemy"));
            dashParticles.Stop();
        }
    }

    public void DebugMouse(InputAction.CallbackContext context) {
        Debug.Log(context.ReadValue<Vector2>());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Obstacle"))
        {
            EndDash();
        }
    }

}
