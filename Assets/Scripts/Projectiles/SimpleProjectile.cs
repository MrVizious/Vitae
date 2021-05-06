using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : ProjectileScript
{
    private float secondsAlive;
    private void Start() {
        secondsAlive = 0f;
    }

    private void Update() {
        secondsAlive += Time.deltaTime;
        if (secondsAlive > data.maxSecondsAlive)
        {
            DestroyProjectile();
        }
        transform.position += (Vector3)direction * data.speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if ((data.targetsLayerMask | 1 << other.gameObject.layer) == data.targetsLayerMask)
        {
            other.gameObject.GetComponent<Enemy>()?.Damage(data.damage);
            DestroyProjectile();
        }
    }
}
