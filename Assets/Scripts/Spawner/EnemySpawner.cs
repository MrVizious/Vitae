using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> enemyPrefabs;
    public float radius;
    public int initialNumberOfEnemies;
    public float secondsBetweenSpawns;
    public GameObject target;

    private IEnumerator spawnCoroutine;
    [SerializeField] private List<GameObject> enemiesSpawned;

    private void Start() {
        enemiesSpawned = new List<GameObject>();
    }
    public void BeginSpawner() {
        DespawnEnemies();

        for (int i = 0; i < initialNumberOfEnemies; i++)
        {
            Spawn();
        }

        spawnCoroutine = SpawnCoroutine();
        StartCoroutine(spawnCoroutine);

    }

    private IEnumerator SpawnCoroutine() {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            Spawn();
        }
    }

    public void Spawn() {
        int n = Random.Range(0, enemyPrefabs.Count);

        Vector2 newPosition;
        newPosition = (Vector2)transform.position + new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));

        GameObject newGameObject = Instantiate(enemyPrefabs[n], newPosition, Quaternion.identity);
        newGameObject.GetComponent<Enemy>().Spawn(target.transform);
        enemiesSpawned.Add(newGameObject);
    }

    private void DespawnEnemies() {
        Debug.Log("Despawning enemies");
        //Debug.Log("Size: " + enemiesSpawned.Count);
        foreach (GameObject enemy in enemiesSpawned.ToArray())
        {
            Debug.Log("Despawning " + enemy);
            Destroy(enemy);
        }
        enemiesSpawned.Clear();
    }

    public void Reset() {
        DespawnEnemies();
        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }
}
