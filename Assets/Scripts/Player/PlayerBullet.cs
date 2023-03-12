using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public ParticleSystem particleHit; // The particle prefab to use

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        ParticleSystem particleObject = Instantiate(particleHit, transform.position, Quaternion.identity);


        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
        Destroy(particleObject, particleSystem.main.duration);

        Destroy(particleObject.gameObject, particleObject.main.duration);

        Destroy(gameObject);
    }

    
}
