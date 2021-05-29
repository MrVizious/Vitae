using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlinkingEnemy : SeekerEnemy
{
    public float blinkingSpeed;
    private SpriteRenderer spriteRenderer;
    private float timeSinceBeginning = 0f;

    protected override void Start() {
        base.Start();
        timeSinceBeginning = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkingCoroutine());
    }

    private IEnumerator BlinkingCoroutine() {
        while (true)
        {
            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                (Mathf.Cos(timeSinceBeginning * blinkingSpeed) + 1) / 2
            );
            timeSinceBeginning += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
