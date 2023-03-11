using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public ParticleSystem particleHit; // The particle prefab to use
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Reduce enemy health
            EnemyHealthCheck enemyHealth = collision.gameObject.GetComponent<EnemyHealthCheck>();
            if (enemyHealth != null)
            {
                enemyHealth.ReduceEnemyLife();
            }
        }



        if (!(collision.gameObject.CompareTag("Player")))
        {
            ParticleSystem particleObject = Instantiate(particleHit, transform.position, Quaternion.identity);


            ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
            Destroy(particleObject, particleSystem.main.duration);

            Destroy(particleObject.gameObject, particleObject.main.duration);


            Destroy(gameObject);
        }
    }
}
