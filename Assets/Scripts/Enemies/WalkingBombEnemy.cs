using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WalkingBombEnemy : Enemy
{
    public Sprite explosionSprite;

    private void Update() {
        if (Vector2.Distance(transform.position, GetTarget().position) < data.range)
        {
            if (debug) Debug.Log("Player found nearby");
            path.maxSpeed = 0;
            Die();
        }
    }

    protected override void Die() {
        StartCoroutine(ExplodeCoroutine());
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
        Destroy(this.gameObject);
    }
}
