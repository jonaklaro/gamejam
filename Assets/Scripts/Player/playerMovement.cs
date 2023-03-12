using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] Animator animator;
    Rigidbody2D rb;
    SoundManager soundManager;
    [SerializeField] private SpriteRenderer sprite;
    
    [SerializeField] private ParticleSystem particOne;
    [SerializeField] private ParticleSystem particTwo;
    [SerializeField] private ParticleSystem particThree;
    [SerializeField] private ParticleSystem particFour;
    private ParticleSystem[] particles;
    PlayerShoot playerShoot;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 2f;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;



    private void Start()
    {
        particles = new[] { particOne, particTwo, particThree, particFour };
        soundManager = SoundManager.instance;
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

        AudioClip deathSound = soundManager.GetAudioClip("bigExplosion");
        //get AudioSource component and play death sound
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = deathSound;
        Debug.Log("Death Sound: " + deathSound);
        audioSource.Play();

        animator.SetBool("IsShooting", false);

        //particlesystem and playerobject destroy
        for (int i = 0; i < particles.Length; i++)
        {
            ParticleSystem part = Instantiate(particles[i], transform.position, Quaternion.identity);
        }

        sprite.enabled = false;
        StartCoroutine(LooseScreen());
    }

    private IEnumerator LooseScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(4);
    }
}

