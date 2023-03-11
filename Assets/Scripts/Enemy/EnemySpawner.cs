using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemySpawnRatePerMinute;
    float enemySpawnTimer;

    List<Transform> spawnPoints = new List<Transform> ();

    bool spawnEnemy = true;

    [SerializeField] Tilemap colliderTilemap;
    List<Vector3Int> colliderTilePositions = new List<Vector3Int>();

    private void Start()
    {
        enemySpawnTimer = 1 / (float)enemySpawnRatePerMinute * 60;
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());
        spawnPoints.RemoveAt(0);


        TileBase[] tileBases = colliderTilemap.GetTilesBlock(colliderTilemap.cellBounds);
        BoundsInt cellBounds = colliderTilemap.cellBounds;
        
        int i = -1;
        for(int x = cellBounds.xMin; x < cellBounds.xMax; x++)
        {
            for(int y = cellBounds.yMin; y < cellBounds.yMax; y++)
            {
                Vector3Int gridPos = colliderTilemap.WorldToCell(new Vector3Int(x, y));
                if (colliderTilemap.GetTile(gridPos) == null) continue;

                colliderTilePositions.Add(new Vector3Int(x, y));
            }
        }

/*        foreach (Vector3Int pos in colliderTilePositions)
        {
            Debug.Log(pos);
        }*/
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
        GameObject enemyObj = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        enemyObj.GetComponent<NewEnemyMovement>().colliderTilePositions = colliderTilePositions;


        yield return new WaitForSeconds(enemySpawnTimer);
        spawnEnemy = true;

    }
}
