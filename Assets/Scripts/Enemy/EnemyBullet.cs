using System;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float timeLost;
    [SerializeField] private float force;
    [SerializeField] private float existingTime = 5f;
    [SerializeField] private ParticleSystem particleSystem;
    private PlayerHealthTimer playerHealthTimer;

    private float timer;

    private void Start()
    {
        playerHealthTimer = player.GetComponent<PlayerHealthTimer>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        rb.gravityScale = 0;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = quaternion.Euler(0,0, rot);
        
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > existingTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ParticleSystem particObject = Instantiate(particleSystem, transform.position, Quaternion.identity);
        ParticleSystem particle = particObject.GetComponent<ParticleSystem>();
        Destroy(particObject, particle.main.duration);
        Destroy(gameObject);

        if (col.gameObject.tag.Equals("Player"))
        {
            playerHealthTimer.TakeDamage(timeLost);
        }
    }
}
