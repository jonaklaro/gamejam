using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] Animator animator;
    Rigidbody2D rb;
    SoundManager soundManager;
    PlayerShoot playerShoot;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerShoot = GetComponent<PlayerShoot>();

        

        //Rotation Lock
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Get the horizontal and vertical input axis
        Move();
    }

     private void Move()
    {
        // Apply rotation lock
        rb.freezeRotation = true;

        // Get input axis
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movementVector = new Vector2(horizontalInput, verticalInput).normalized;

        // Check for dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f)
        {
            // Start the dash
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        // Apply movement
        if (dashTimer > 0f)
        {
            rb.velocity = movementVector * dashSpeed;
        }
        else
        {
            rb.velocity = movementVector * moveSpeed;
        }

        // Update timers
        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;

        // Get mouse position and calculate direction vector
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Rotate player to face mouse position
        // transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Update animator parameters
        animator.SetFloat("Horizontal", movementVector.x);
        animator.SetFloat("Vertical", movementVector.y);
        animator.SetFloat("Speed", movementVector.sqrMagnitude);
        animator.SetBool("IsShooting", playerShoot.isShooting);
    }

    public void Die()
    {
        rb.velocity = Vector3.zero;
        enabled = false;

        AudioClip deathSound = soundManager.GetAudioClip("Explosion_Tiny_4");
        //get AudioSource component and play death sound
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = deathSound;
        Debug.Log("Death Sound: " + deathSound);
        audioSource.Play();
    }
}

