using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] Animator animator;
    Rigidbody2D rb;
    SoundManager soundManager;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        soundManager = SoundManager.instance;
        soundManager.SetVolume("MasterVolume", -40f);
        soundManager.PlayMusic("8Bit2");

        //Rotation Lock
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Get the horizontal and vertical input axis
        Move();
    }

    void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Create a movement vector from the input axis
        Vector2 movementVector = new Vector2(horizontalInput, verticalInput);

        // Normalize the movement vector to prevent faster diagonal movement
        movementVector = movementVector.normalized;

        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the direction vector from the player to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Rotate the player to face the mouse position
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Move the player in the movement direction using Rigidbody2D velocity
        rb.velocity = movementVector * moveSpeed;

        

        animator.SetFloat("Horizontal", movementVector.x);
        animator.SetFloat("Vertical", movementVector.y);
        animator.SetFloat("Speed", movementVector.sqrMagnitude);

    }

    public void Die()
    {
        rb.velocity = Vector3.zero;
        enabled = false;
    }
}

