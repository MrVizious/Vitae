using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomController : MonoBehaviour
{
    //TODO: Create a GameManager with player references
    public Transform player;
    public List<Door> entryDoors;
    public List<Door> exitDoors;
    public UnityEvent onRoomCleared;
    public UnityEvent onReset;
    [SerializeField] private bool cleared = false;
    private List<GameObject> enemies;
    [SerializeField] private List<GameObject> spawnedEnemies;


    void Start() {
        if (enemies == null || enemies.Count == 0)
        {
            enemies = new List<GameObject>();
            Enemy[] foundEnemies = GetComponentsInChildren<Enemy>();
            foreach (Enemy enemy in foundEnemies)
            {
                enemies.Add(enemy.gameObject);
            }
        }
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    public void SpawnEnemies() {
        if (!cleared && spawnedEnemies.Count == 0)
        {
            // Close Doors
            foreach (Door door in entryDoors)
            {
                door.Close();
            }
            foreach (Door door in exitDoors)
            {
                door.Close();
            }

            spawnedEnemies = new List<GameObject>();
            foreach (GameObject enemy in enemies)
            {
                Debug.Log("Spawning enemy!");
                GameObject newEnemy = Instantiate(enemy, gameObject.transform);
                newEnemy.SetActive(true);
                newEnemy.GetComponent<Enemy>().Spawn(player);
                newEnemy.GetComponent<Enemy>().OnDie.AddListener(
                    delegate
                    {
                        OnEnemyDie(newEnemy);
                    }
                );
                spawnedEnemies.Add(newEnemy);
            }
        }
    }

    public void DestroySpawnedEnemies() {
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();
    }

    private void OnEnemyDie(GameObject enemyKilled) {
        spawnedEnemies.Remove(enemyKilled);
        Destroy(enemyKilled);
        if (spawnedEnemies.Count <= 0)
        {
            ClearRoom();
        }
    }

    public void Reset() {
        DestroySpawnedEnemies();
        if (cleared)
        {
            foreach (Door door in entryDoors)
            {
                door.Open();
            }
            foreach (Door door in exitDoors)
            {
                door.Open();
            }
            onReset.Invoke();
        }
    }

    public void ClearRoom() {
        cleared = true;
        onRoomCleared.Invoke();

        DestroySpawnedEnemies();

        //Open doors
        foreach (Door door in entryDoors)
        {
            door.Open();
        }
        foreach (Door door in exitDoors)
        {
            door.Open();
        }

        Debug.Log("Room cleared!");
    }
}
