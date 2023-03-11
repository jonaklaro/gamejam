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

    private void Awake()
    {
        StartCoroutine(WaitUntilYouCanStart());
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
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 playerDirection = playerTransform.position - transform.position;
            playerDirection = playerDirection.normalized;
            StartCoroutine(TimeBetweenShoots(playerDirection));
        }
        
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

    private IEnumerator WaitUntilYouCanStart()
    {
        yield return new WaitForSeconds(5);
        youMayStart = true;
    }
    private void StartShooting(Vector2 playerDir)
    {
        Vector3 bulletPos = transform.position + new Vector3(playerDir.x, playerDir.y, 0);
        Instantiate(bossStats.bullet, shootingPos.position, Quaternion.identity);

    }

    private void GetDamage(int damage)
    {
        bossStats.health -= damage;

        if (bossStats.health < 1)
        {
            JustDie();
        }
    }

    private void JustDie()
    {
        //abspielen von Animation
        //Destroy(this.gameObject);
        //Load new scene etc.
    }
}
