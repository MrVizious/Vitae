using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public ProjectileData projectile;

    public Vector2 direction;

    private float secondsAlive;
    private void Start() {
        secondsAlive = 0f;
    }

    private void FixedUpdate() {
        secondsAlive += Time.fixedDeltaTime;
        if (secondsAlive > projectile.maxSecondsAlive)
        {
            DestroyProjectile();
        }
        transform.position += (Vector3)direction * projectile.speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if ((projectile.targetsLayerMask | 1 << other.gameObject.layer) == projectile.targetsLayerMask)
        {
            other.gameObject.GetComponent<EnemyScript>()?.Damage(projectile.damage);
            DestroyProjectile();
        }
    }

    public void DestroyProjectile() {
        Debug.Log("Destroying projectile");
        Destroy(gameObject);
    }
}
