using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // This is the prefab for the projectile we will shoot
    public float shootInterval = 2f; // The interval at which to shoot projectiles
    public float projectileLifetime = 5f; // The amount of time before the projectile disappears
    private float lastShootTime = 0f; // The time at which we last shot a projectile

    void Update()
    {
        // Check if enough time has passed since the last shot
        if (Time.time - lastShootTime >= shootInterval)
        {
            ShootProjectile();
            lastShootTime = Time.time;
        }
    }

    void ShootProjectile()
    {
        // Create a new projectile based on the prefab
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Add a force to the projectile to make it move in the direction of the mouse
        Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();
        Vector2 shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        projectileRb.AddForce(shootDirection.normalized * 500f);

        // Rotate the projectile to face the direction of the mouse
        Vector3 direction = shootDirection.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Destroy the projectile after its lifetime has expired
        Destroy(newProjectile, projectileLifetime);
    }
}
