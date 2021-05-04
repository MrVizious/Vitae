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

    private float secondsSinceLastSpawn = 0f;

    private void Start() {
        for (int i = 0; i < initialNumberOfEnemies; i++)
        {
            Spawn();
        }
    }

    private void Update() {
        secondsSinceLastSpawn += Time.deltaTime;
        if (secondsSinceLastSpawn >= secondsBetweenSpawns)
        {
            secondsSinceLastSpawn = 0f;
            Spawn();
        }
    }

    public void Spawn() {
        int n = Random.Range(0, enemyPrefabs.Count);
        Collider2D anyCollision;
        Vector2 newPosition;
        do
        {
            newPosition = (Vector2)transform.position + new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
            anyCollision = Physics2D.OverlapCircle(newPosition, 5f);
        }
        while (anyCollision != null);
        //Debug.Log("Spawning");
        GameObject newGameObject = Instantiate(enemyPrefabs[n], newPosition, Quaternion.identity);
        newGameObject.GetComponent<Enemy>().SetTarget(target.transform);
    }
}
