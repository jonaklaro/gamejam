using System;
using System.Collections;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Stats")] [SerializeField]
    private float range;

    [SerializeField] private float standingRange;
    [SerializeField] private float stopFollowing;
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private int shootTime;
    [SerializeField] private int waitTime;

    private int[] behaviour;
    private int index;

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
        behaviour = new[] { 0, 1, 2, 3 };
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, playerPos.transform.position);
        Vector2 direction = playerPos.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        if (distance < range)
        {
            index = behaviour[0];
        }
        
        if (distance > range)
        {
            index = behaviour[1];
        }
        if (distance < standingRange)
        {
            index = behaviour[2];
        }
        

        
        switch (behaviour[index])
        {
            case 0:
                GetAgressive(movement);
                range = stopFollowing;
                break;
            case 1:
                range = 5;
                break;
            case 2:
                JustShootGodDammit();
                break;
           }
    }

    private void GetAgressive(Vector2 direction)
    {
         /*transform.position =
                Vector2.MoveTowards(this.transform.position, playerPos.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);*/
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));
        if(isShooting) return;
        StartCoroutine(TimeBetweenShoots());
    }

    private void JustShootGodDammit()
    {
        StartCoroutine(TimeBetweenShoots());
    }

    /*private IEnumerator WalkBackToPlace(float angle)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = Vector2.MoveTowards(this.transform.position, guardingPos.transform.position,
            speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        
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
