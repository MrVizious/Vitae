using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerSpawnPoint : MonoBehaviour
{
    private void Start() {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player"))
            PlayerSpawner.getInstance().setSpawnPoint(transform);
    }
}
