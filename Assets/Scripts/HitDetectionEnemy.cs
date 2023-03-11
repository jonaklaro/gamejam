using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionEnemy : MonoBehaviour
{
    public GameObject particlePrefab; // The particle prefab to use
    public byte enemyLife = 3;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            
            

           

            if (enemyLife > 1)
            {
                enemyLife--;
            }
            else
            {
                // Instantiate the particle prefab at the same position as the enemy
                GameObject particleObject = Instantiate(particlePrefab, transform.position, Quaternion.identity);

                // Destroy the particle object
                ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
                Destroy(particleObject, particleSystem.main.duration);

                //destroy enemy
                Destroy(gameObject);
            }
            
        }
    }
}
