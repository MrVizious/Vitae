using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowAnimation : MonoBehaviour
{
    private Camera mainCam;
    private void Start() {
        mainCam = Camera.main;
    }
    private void Update() {
        SetOrientationMouse();
    }
    public void SetOrientationMouse() {
        Vector3 worldPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 diff = (Vector3)worldPos - transform.position;

        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


    }
}
