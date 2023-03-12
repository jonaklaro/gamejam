using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyMovement : MonoBehaviour
{
    PlayerShoot playerShoot;
    [SerializeField] Animator animator;
    public float stopFollowRadius = 15f; // The radius in which the enemy will stop following the player
    public float moveSpeed = 3f; // The speed at which the enemy moves
    public float decelerationFactor = 0.5f; // The factor at which the enemy decelerates when the player is out of range
    public List<Vector3Int> colliderTilePositions;
    [SerializeField] Object bullet;
    [SerializeField] float shootTime = 1;

    private Transform playerTransform; // The transform of the player
    private Vector3Int TileToGetTo;
    private Rigidbody2D rb; // The rigidbody of the enemy
    private Vector2 movementDirection; // The direction in which the enemy is moving
    private bool isShooting;

    private bool playerInRange = false; // Whether the player is currently in range

    private Vector2 startingPosition; // The starting position of the enemy

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player is in range");
            playerInRange = true;
            
        }
        else if (other.gameObject.CompareTag("Projectile") && GetDistance(other.gameObject) < 1.2f)
        {
            Die();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) playerInRange = false;
    }

    private void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();
        startingPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        

        FindNewTile();

        rb.freezeRotation = true;
    }

    void Update()
    {
        movementDirection = TileToGetTo - transform.position;
        movementDirection.Normalize();

        if(GetDistance(TileToGetTo) < .1f)
        {
            FindNewTile();
        }

        // Move the enemy
        rb.velocity = movementDirection * moveSpeed;

        if (playerInRange)
        {
            StartCoroutine(TimeBetweenShoots());
        }

        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
        animator.SetBool("IsShooting", playerShoot.isShooting);
    }

    void FindNewTile()
    {
        Vector3 vecToPlayer = playerTransform.position - transform.position;
        vecToPlayer = vecToPlayer.normalized;

        Vector3 wantedNextPosition = transform.position + vecToPlayer;
        List<TileDistancePair> tilesSurroundingEnemy = new List<TileDistancePair>();
        Vector3 enemyPos = transform.position;

        for(int x = -1; x < 2; x++)
        {
            for(int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0) continue;

                Vector3Int tilePosInt = new Vector3Int(Mathf.RoundToInt(enemyPos.x + x), Mathf.RoundToInt(enemyPos.y + y),0);
                TileDistancePair tdPair = new TileDistancePair(tilePosInt, GetDistance((Vector3)tilePosInt, wantedNextPosition));
                tilesSurroundingEnemy.Add(tdPair);
            }
        }

        TileToGetTo = FindClosestTile(tilesSurroundingEnemy);
    }

    private IEnumerator TimeBetweenShoots()
    {
        // Just in case avoid concurrent routines
        if (isShooting) yield break;
        isShooting = true;

        Vector2 playerDir = playerTransform.position - transform.position;
        playerDir.Normalize();

        StartShooting(playerDir);
        yield return new WaitForSeconds(shootTime);
        isShooting = false;

    }

    private void StartShooting(Vector2 playerDir)
    {
        Vector3 bulletPos = transform.position + new Vector3(playerDir.x, playerDir.y, 0);
        Instantiate(bullet, bulletPos, Quaternion.identity);

    }

    void Die()
    {
        Destroy(gameObject);
    }

    Vector3Int FindClosestTile(List<TileDistancePair> surroundingTiles)
    {
        int tileIndex= 0;
        for(int i = 1;i< surroundingTiles.Count; i++)
        {
            if(surroundingTiles[i].distance < surroundingTiles[tileIndex].distance)
            {
                tileIndex = i;
            }
        }

        if (colliderTilePositions.Contains(surroundingTiles[tileIndex].pos))
        {
            surroundingTiles.RemoveAt(tileIndex);
            return FindClosestTile(surroundingTiles);
        }

        return surroundingTiles[tileIndex].pos;
    }

    float GetDistance(GameObject other)
    {
        Vector3 vecBetweenObjects = transform.position - other.transform.position;
        return vecBetweenObjects.magnitude;
    }

    float GetDistance(Vector3 other)
    {
        Vector3 vecBetweenObjects = transform.position - other;
        return vecBetweenObjects.magnitude;
    }

    float GetDistance(Vector2 v1 ,Vector2 v2)
    {
        Vector3 vecBetweenObjects = v1 - v2;
        return vecBetweenObjects.magnitude;
    }
}

class TileDistancePair
{
    public Vector3Int pos;
    public float distance;

    public TileDistancePair(Vector3Int pos, float distance)
    {
        this.pos = pos;
        this.distance = distance;
    }
}
