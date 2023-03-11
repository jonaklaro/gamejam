using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int enemySpawnRatePerMinute;
    float enemySpawnTimer;

    List<Transform> spawnPoints = new List<Transform> ();

    bool spawnEnemy = true;

    private void Start()
    {
        enemySpawnTimer = 1 / (float)enemySpawnRatePerMinute * 60;
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());
        spawnPoints.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnemy)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        spawnEnemy = false;
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        Instantiate(enemy, spawnPoints[spawnIndex].position, Quaternion.identity);
        yield return new WaitForSeconds(enemySpawnTimer);
        spawnEnemy = true;

    }
}
