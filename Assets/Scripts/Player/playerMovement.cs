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
        soundManager = SoundManager.instance;
        rb = GetComponent<Rigidbody2D>();
        soundManager.SetVolume("MasterVolume", -20f);
        //make a list with "8Bit1" and "8Bit2" and then randomly pick one of them
        string randomMusic = "8Bit" + Random.Range(1, 3).ToString();
        Debug.Log("Random Music: " + randomMusic);
        soundManager.PlayMusic(randomMusic);

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

        AudioClip deathSound = soundManager.GetAudioClip("Explosion_Tiny_4");
        //get AudioSource component and play death sound
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = deathSound;
        Debug.Log("Death Sound: " + deathSound);
        audioSource.Play();
    }
}

