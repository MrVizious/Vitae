using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    public float rotationSpeed = 20f;

    void Update() {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}


