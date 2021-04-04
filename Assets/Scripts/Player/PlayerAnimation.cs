using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Camera mainCam;
    private void Start() {
        mainCam = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void Update() {
        SetOrientationMouse();
    }

    public void SetOrientationController(InputAction.CallbackContext context) {
        //Debug.Log("Orientation changed");
        Vector2 lookOrientation = context.ReadValue<Vector2>();

        animator.SetFloat("Look_X", lookOrientation.x);
        animator.SetFloat("Look_Y", lookOrientation.y);
    }

    public void SetOrientationMouse() {
        Vector3 worldPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 diff = (Vector3)worldPos - transform.position;
        diff.Normalize();
        //Debug.Log("New orientation: " + diff);

        animator.SetFloat("Look_X", diff.x);
        animator.SetFloat("Look_Y", diff.y);

    }

    public void SetMovementSpeed(InputAction.CallbackContext context) {
        float speed = context.ReadValue<Vector2>().magnitude;
        animator.SetFloat("Speed", speed);
        Debug.Log(animator.GetFloat("Speed"));
    }
}