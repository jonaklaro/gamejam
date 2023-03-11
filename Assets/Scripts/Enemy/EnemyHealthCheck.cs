using UnityEngine;

public class EnemyHealthCheck : MonoBehaviour
{
    public byte enemyLife = 3; // Starting enemy life

    public void ReduceEnemyLife()
    {
        enemyLife--; // Reduce enemy life by 1
        if (enemyLife <= 0)
        {
            Destroy(gameObject); // Destroy the enemy if its life reaches 0
        }
    }
}
