using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashScript : MonoBehaviour
{
    public PlayerData player;
    public ParticleSystem dashParticles;
    private Rigidbody2D rb;
    private IEnumerator dashCoroutine = null;
    private bool walled = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        dashCoroutine = null;
    }

    public void Dash(InputAction.CallbackContext context) {
        if (Time.timeScale > 0f && this.enabled)
        {

            if (context.started && dashCoroutine == null && !player.isDashing)
            {
                Vector2 dashDirection = player.movementDirection;
                if (dashDirection.magnitude < 0.1f) dashDirection = player.lookDirection;
                if (!IsWallInfront(dashDirection))
                {
                    dashCoroutine = DashCoroutine(dashDirection, player.dashDuration, player.dashSpeed);
                    StartCoroutine(dashCoroutine);
                }
                else Debug.Log("Can't dash against the wall");
            }
            else
            {
                Debug.Log("Can't dash yet!");
            }
        }
    }

    private IEnumerator DashCoroutine(Vector2 direction, float duration, float speed) {
        Debug.Log("Dash initiated!");

        player.isDashing = true;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyProjectile"), true);
        Debug.Log("Collisions DISABLED between layers " + LayerMask.NameToLayer("Player") + " and " + LayerMask.NameToLayer("Enemy"));

        dashParticles.Play();
        float timeSinceDashStarted = 0f;
        while (timeSinceDashStarted < duration)
        {
            rb.MovePosition((Vector2)transform.position + direction.normalized * speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
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
            player.isDashing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Obstacle"))
        {
            EndDash();
            walled = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Obstacle"))
        {
            walled = false;
        }
    }

    private bool IsWallInfront(Vector2 direction) {
        if (walled)
        {
            Debug.DrawLine(transform.position, (Vector2)transform.position + direction * 1f, Color.white, 0.1f);
            int layerToHit = (1 << LayerMask.NameToLayer("Obstacle"));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, layerToHit);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name, hit.collider.gameObject);
                return true;
            }
        }
        return false;
    }

}
