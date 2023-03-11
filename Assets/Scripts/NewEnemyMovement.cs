using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyMovement : MonoBehaviour
{
  public float stopFollowRadius = 15f; // The radius in which the enemy will stop following the player
  public float moveSpeed = 3f; // The speed at which the enemy moves
  public float obstacleAvoidanceDistance = 1f; // The distance at which the enemy will try to avoid obstacles
  public float decelerationFactor = 0.5f; // The factor at which the enemy decelerates when the player is out of range

  private Transform playerTransform; // The transform of the player
  private Rigidbody2D rb; // The rigidbody of the enemy
  private Vector2 movementDirection; // The direction in which the enemy is moving
  private LayerMask obstacleLayer; // The layer mask for obstacles
  private float distanceToPlayer; // The distance between the enemy and the player

  private bool playerInRange = false; // Whether the player is currently in range

  private Vector2 startingPosition; // The starting position of the enemy

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      Debug.Log("Player is in range");
      playerInRange = true;
    }
  }

  private void Start()
  {
    startingPosition = transform.position;
    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    rb = GetComponent<Rigidbody2D>();
    obstacleLayer = LayerMask.GetMask("Wall");
  }

  void Update()
  {
    // Calculate the distance to the player
    distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

    // Check if the player is within the follow radius
    if (distanceToPlayer <= stopFollowRadius && playerInRange)
    {
      // Calculate the direction to the player
      Vector2 playerDirection = playerTransform.position - transform.position;

      // Check if the player is within the detection radius
      if (playerInRange)
      {
        // Move towards the player
        movementDirection = playerDirection.normalized;
      }
      else
      {
        // Move towards the last known position of the player
        movementDirection = rb.velocity.normalized;
      }
    }
    else
    {
      // Stop moving and move back to starting position
      movementDirection = Vector2.zero;
      playerInRange = false;
      transform.position = Vector2.MoveTowards(transform.position, startingPosition, moveSpeed * Time.deltaTime);
    }

    // Check for obstacles in the way
    RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, obstacleAvoidanceDistance, obstacleLayer);
    if (hit.collider != null)
    {
      // Avoid the obstacle
      Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
      movementDirection = avoidanceDirection;
    }

    // Move the enemy
    rb.velocity = movementDirection * moveSpeed;
  }
}
