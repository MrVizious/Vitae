using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileScript : MonoBehaviour
{
    public ProjectileData data;

    protected Vector2 direction;


    public void DestroyProjectile() {
        Debug.Log("Destroying projectile");
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 newDirection) {
        direction = newDirection;
    }
}
