using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterScript : MonoBehaviour
{
    public PlayerData player;
    public GameObject projectile;

    public void Shoot(InputAction.CallbackContext context) {
        if (gameObject.activeSelf && context.started && Time.timeScale > 0f)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<ProjectileScript>().SetDirection(player.lookDirection.normalized);
        }
    }
}
