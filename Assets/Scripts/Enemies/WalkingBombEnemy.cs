using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WalkingBombEnemy : Enemy
{
    public Sprite explosionSprite;
    public float explosionAnimationTime = 0.2f;
    private SpriteRenderer spriteRenderer;
    private IEnumerator explodeCoroutine = null;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        if (Vector2.Distance(transform.position, GetTarget().position) < data.range)
        {
            if (debug) Debug.Log("Player found nearby");
            Die();
        }
    }

    protected override void Die() {
        if (explodeCoroutine == null)
        {
            path.maxSpeed = 0;
            explodeCoroutine = ExplodeCoroutine();
            StartCoroutine(explodeCoroutine);
        }
    }

    private void Explode() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,
        data.range);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag.Equals("Player"))
            {
                collider.gameObject.GetComponent<PlayerHealth>().Damage(data.damage);
            }
            else if (collider.tag.Equals("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().Damage(data.damage);
            }
        }
    }

    private IEnumerator ExplodeCoroutine() {
        yield return new WaitForSeconds(data.delay);
        Explode();
        StartCoroutine(ExplosionAnimationCoroutine());
    }

    private IEnumerator ExplosionAnimationCoroutine() {
        GetComponent<Collider2D>().enabled = false;
        spriteRenderer.sprite = explosionSprite;
        spriteRenderer.color = Color.red;
        float timeExploded = 0;
        while (timeExploded < explosionAnimationTime)
        {
            yield return null;
            timeExploded += Time.deltaTime;
            float size = FloatExtensionMethods.Map(timeExploded, 0, explosionAnimationTime, 0, data.range);
            transform.localScale = new Vector2(size, size);
        }
        base.Die();
    }

}
