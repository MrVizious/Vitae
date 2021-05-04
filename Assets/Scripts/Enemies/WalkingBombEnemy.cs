using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBombEnemy : Enemy
{

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
