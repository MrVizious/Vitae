using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : ProjectileScript
{
    private float secondsAlive;
    private void Start() {
        secondsAlive = 0f;
    }

    private void FixedUpdate() {
        secondsAlive += Time.fixedDeltaTime;
        if (secondsAlive > data.maxSecondsAlive)
        {
            DestroyProjectile();
        }
        transform.position += (Vector3)direction * data.speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if ((data.targetsLayerMask | 1 << other.gameObject.layer) == data.targetsLayerMask)
        {
            other.gameObject.GetComponent<Enemy>()?.Damage(data.damage);
            DestroyProjectile();
        }
    }
}
