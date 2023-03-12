using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class FirstBoss : MonoBehaviour
{
    [SerializeField] private Bossstats bossStats;
    [FormerlySerializedAs("targetTransform")] 
    [SerializeField] Transform playerTransform;

    private bool youMayStart = false;
    private bool isShooting;
    [SerializeField] private Transform shootingPos;
    private BossHealth bossHealth;


    private void Awake()
    {
        bossHealth = GetComponent<BossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (youMayStart)
        {
            Vector2 direction = playerTransform.position - transform.position;

            // Calculate the angle to rotate the enemy
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply the rotation to the enemy's transform
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Vector2 playerDirection = playerTransform.position - transform.position;
            playerDirection = playerDirection.normalized;
            StartCoroutine(TimeBetweenShoots(playerDirection));
        }
        
    }

    public void SetBool(bool active)
    {
        youMayStart = active;
    }
    
    private IEnumerator TimeBetweenShoots(Vector2 playerDir)
    {
        // Just in case avoid concurrent routines
        if (isShooting) yield break;
        isShooting = true;
        StartShooting(playerDir);
        yield return new WaitForSeconds(bossStats.shootTime);
        isShooting = false;

    }
    
    private void StartShooting(Vector2 playerDir)
    {
        Vector3 bulletPos = transform.position + new Vector3(playerDir.x, playerDir.y, 0);
        Instantiate(bossStats.bullet, shootingPos.position, Quaternion.identity);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Projectile"))
        {
            bossHealth.TakeDamage(10);
        }
    }
    
    public void JustDie()
    {
        Debug.Log("Boss isdefeated");
        //abspielen von Animation
        //Destroy(this.gameObject);
        //Load new scene etc.
    }
}
