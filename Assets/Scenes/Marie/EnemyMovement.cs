using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Stats")] [SerializeField]
    private float range;

    private float distance;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPos;

    [SerializeField] private float speed;

    [SerializeField] private GameObject playerPos;

    
  
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
        }
    }

    private void GetAgressive(float angle)
    {
        transform.position =
            Vector2.MoveTowards(this.transform.position, playerPos.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        StartCoroutine(TimeBetweenShoots());
    }
    private IEnumerator TimeBetweenShoots()
    {
        yield return new WaitForSeconds(3);
        StartShooting();
        
    }
    
    private void StartShooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPos.transform.position, Quaternion.identity);
        
    }

}
