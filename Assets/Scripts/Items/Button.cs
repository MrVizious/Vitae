using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Button : MonoBehaviour
{
    public UnityEvent OnActivated;
    [Range(0, 1)]
    public float colorDimmingFactor = 0.6f;

    private SpriteRenderer spriteRenderer;
    private Color initialColor;

    private void Start() {
        GetComponent<Collider2D>().isTrigger = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player"))
        {
            OnActivated.Invoke();
            DimSprite();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag.Equals("Player"))
        {
            DimSprite();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Equals("Player"))
        {
            UndimSprite();
        }
    }

    private void DimSprite() {

        spriteRenderer.color = new Color(
            initialColor.r * colorDimmingFactor,
            initialColor.g * colorDimmingFactor,
            initialColor.b * colorDimmingFactor,
            initialColor.a
        );
    }

    private void UndimSprite() {
        spriteRenderer.color = initialColor;
    }
}
