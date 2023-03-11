using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionEnemy : MonoBehaviour
{
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.CompareTag("Projectile"))
        {
            
            // Display debug message
            Debug.Log("Bullet hit an enemy!");

            
            Destroy(gameObject);
        }
    }



}
