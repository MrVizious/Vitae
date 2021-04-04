using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldScript : MonoBehaviour
{
    public Sprite rightSprite, upSprite, leftSprite, downSprite;
    public float distanceFromPlayer = 1f;
    private float angleWithPlayer;

    private SpriteRenderer spriteRenderer;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private float AngleWithPlayer() {

        Vector2 delta = (Vector2)transform.position - (Vector2)transform.parent.transform.position;
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        return angle;
    }

    // TODO: Make shield appear underneath player when pointing up
    private void UpdateSpriteAccordingToAngle(float angle) {

        //Right
        if ((angle >= 0 && angle <= 45) || (angle <= 0 && angle > -45))
        {
            spriteRenderer.sprite = rightSprite;
        }

        //Up
        else if (angle > 45 && angle <= 135)
        {
            spriteRenderer.sprite = upSprite;
        }

        //Left
        if ((angle > 135 && angle <= 180) || (angle >= -180 && angle <= -135))
        {
            spriteRenderer.sprite = leftSprite;
        }

        //Down
        else if (angle > -135 && angle <= -45)
        {
            spriteRenderer.sprite = downSprite;
        }

    }

    public void SetPosition(InputAction.CallbackContext context) {
        Vector2 offset = context.ReadValue<Vector2>();
        if (offset.magnitude > Mathf.Epsilon)
        {
            transform.position = (Vector2)transform.parent.position + offset.normalized * distanceFromPlayer;

            angleWithPlayer = AngleWithPlayer();
            UpdateSpriteAccordingToAngle(angleWithPlayer);
            UpdateRotation(angleWithPlayer);
        }
    }

    private void UpdateRotation(float angle) {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle - 90f);
    }
}
