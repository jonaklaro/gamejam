using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Stats")] [SerializeField]
    private float range;
    [SerializeField] private float stopFollowing;

    [SerializeField] private int shootTime;
    [SerializeField] private int waitTime;

    [SerializeField] private bool isShooting;
    private float distance;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPos;

    [SerializeField] private float speed;
    private Transform guardingPos;
    [SerializeField] private GameObject playerPos;


    private void Start()
    {
        guardingPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, playerPos.transform.position);
        Vector2 direction = playerPos.transform.position - transform.position;
        
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < range)
        {
            GetAgressive(angle);
            range = stopFollowing;
        }

        if (distance > range)
        {
            range = 5;
            //StartCoroutine(WalkBackToPlace());
        }


    }

    private void GetAgressive(float angle)
    {
         transform.position =
                Vector2.MoveTowards(this.transform.position, playerPos.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        if(isShooting) return;
        StartCoroutine(TimeBetweenShoots());
    }

    /*private IEnumerator WalkBackToPlace()
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = Vector2.MoveTowards(this.transform.position, guardingPos.transform.position,
            speed * Time.deltaTime);
        
    }*/
    private IEnumerator TimeBetweenShoots()
    {
        // Just in case avoid concurrent routines
        if(isShooting) yield break;
        isShooting = true;
        StartShooting();
        yield return new WaitForSeconds(shootTime);
        isShooting = false;

    }
    
    private void StartShooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPos.transform.position, Quaternion.identity);
        
    }

}
