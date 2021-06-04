using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlinkingEnemy : SeekerEnemy
{
    public float blinkingSpeed;
    public SpriteRenderer healthBar, healthBackground;
    private float offset;
    private SpriteRenderer spriteRenderer;
    private float timeSinceBeginning = 0f;

    protected override void Start() {
        base.Start();
        timeSinceBeginning = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        offset = Random.Range(0f, 2f);
        StartCoroutine(BlinkingCoroutine());
    }

    private IEnumerator BlinkingCoroutine() {
        while (true)
        {
            float alphaValue = (Mathf.Cos((timeSinceBeginning + offset) * blinkingSpeed) + 1) / 2;
            SetAlpha(spriteRenderer, alphaValue);
            SetAlpha(healthBar, alphaValue);
            SetAlpha(healthBackground, alphaValue);

            timeSinceBeginning += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetAlpha(SpriteRenderer sprite, float alphaValue) {
        sprite.color = new Color(
            sprite.color.r,
            sprite.color.g,
            sprite.color.b,
            alphaValue
        );
    }
}
