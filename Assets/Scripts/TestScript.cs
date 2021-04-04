using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float playbackSpeed = 1f, look_X = 0f, look_Y = 0f;
    public Animator animator;
    public Text textUI;
    public Transform a, b;

    private void Update() {
        AnimationSpeedTest();
    }

    private void AngleTest() {
        Vector2 delta = (Vector2)b.transform.position - (Vector2)a.transform.position;
        float angle = Mathf.Atan2(delta.y, delta.x);
        textUI.text = (angle * Mathf.Rad2Deg + 180f).ToString();
    }

    private void AnimationSpeedTest() {
        animator.speed = playbackSpeed;
        animator.SetFloat("Look_X", look_X);
        animator.SetFloat("Look_Y", look_Y);
    }

}
