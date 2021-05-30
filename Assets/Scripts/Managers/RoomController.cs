using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomController : MonoBehaviour
{
    //TODO: Create a GameManager with player references
    public Transform player;
    public List<GameObject> enemies;
    public UnityEvent onRoomCleared;
    public UnityEvent onReset;
    public UnityEvent onEnemiesSpawned;
    [SerializeField] private List<GameObject> spawnedEnemies;
    [SerializeField] private bool cleared = false;


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
            onEnemiesSpawned.Invoke();
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
            cleared = true;
            onRoomCleared.Invoke();
            Debug.Log("Room cleared!");
        }
    }

    public void Reset() {
        DestroySpawnedEnemies();
        if (cleared) onReset.Invoke();
    }
}
