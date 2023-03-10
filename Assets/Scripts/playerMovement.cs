using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [SerializeField] Animator animator;
    

    private void Update()
    {
        // Get the horizontal and vertical input axis
        Move();
    }

    void Move() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Create a movement vector from the input axis
        Vector2 movementVector = new Vector2(horizontalInput, verticalInput);

        // Normalize the movement vector to prevent faster diagonal movement
        movementVector = movementVector.normalized;

        // Move the player in the movement direction
        transform.position += new Vector3(movementVector.x, movementVector.y, 0f) * moveSpeed * Time.deltaTime;

        animator.SetFloat("Horizontal", movementVector.x);
        animator.SetFloat("Vertical", movementVector.y);
        animator.SetFloat("Speed", movementVector.sqrMagnitude);

        
    }


}


