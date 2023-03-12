using UnityEngine;

public class EnemyHealthCheck : MonoBehaviour
{
    public byte enemyLife = 1; // Starting enemy life

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile") && GetDistance(other.gameObject) < 1.2f)
        {
            ReduceEnemyLife();
            PlayerBullet pBullet = other.gameObject.GetComponent<PlayerBullet>();
            pBullet.DestroyBullet();
        }
    }

    public void ReduceEnemyLife()
    {
        enemyLife--; // Reduce enemy life by 1
        if (enemyLife <= 0)
        {
            Destroy(gameObject); // Destroy the enemy if its life reaches 0
        }
    }

    float GetDistance(GameObject other)
    {
        Vector3 vecBetweenObjects = transform.position - other.transform.position;
        return vecBetweenObjects.magnitude;
    }
}
