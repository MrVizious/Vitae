using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerEnemy : Enemy
{
    private float secondsSinceLastDamage = 0f;
    private IEnumerator damagingCoroutine = null;

    private void OnCollisionEnter2D(Collision2D other) {
        if (debug) Debug.Log("Collision entered!");
        if (other.gameObject.tag.Equals("Player"))
        {
            if (debug) Debug.Log("Collided with Player!");
            if (damagingCoroutine == null)
            {
                if (debug) Debug.Log("Trying to damage player!");
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    secondsSinceLastDamage = data.delay + 1;
                    damagingCoroutine = DamagingCoroutine(playerHealth);
                    StartCoroutine(damagingCoroutine);
                }
                else
                {
                    Debug.LogError("Can't find PlayerHealth");
                }
            }
        }
    }

    protected void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (damagingCoroutine != null)
            {
                StopCoroutine(damagingCoroutine);
                damagingCoroutine = null;
            }
        }
    }

    protected IEnumerator DamagingCoroutine(PlayerHealth playerHealth) {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            secondsSinceLastDamage += Time.deltaTime;
            if (secondsSinceLastDamage >= data.delay)
            {
                if (debug) Debug.Log("Damage!");
                playerHealth.Damage(data.damage);
                secondsSinceLastDamage = 0;
            }
        }
    }

}