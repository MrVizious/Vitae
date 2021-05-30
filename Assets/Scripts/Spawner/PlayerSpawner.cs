using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerHealth player;
    public Transform spawnPoint;

    private static PlayerSpawner instance;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static PlayerSpawner getInstance() {
        return instance;
    }
    public void Respawn() {
        player.Reset();
        player.transform.position = spawnPoint.position;
    }

    public void setSpawnPoint(Transform newSpawnPoint) {
        spawnPoint = newSpawnPoint;
    }
}
