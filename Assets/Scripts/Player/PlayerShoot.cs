using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
  public GameObject projectilePrefab; // This is the prefab for the projectile we will shoot
  public float shootInterval = 5f; // The interval at which to shoot projectiles
  public float projectileLifetime = 5f; // The amount of time before the projectile disappears
  private float lastShootTime = 0f; // The time at which we last shot a projectile

  private bool firstShotFired = false;
  private bool secondShotFired = false;
  SoundManager soundManager;
  AudioSource audioSource;

  AudioClip clip;

  private void Start()
  {
    soundManager = SoundManager.instance;
    audioSource = GetComponent<AudioSource>();
  }


  void Update()
  {

    //first click is 1/3 of the shoot interval
    float firstClick = shootInterval / 3;
    //second click is 2/3 of the shoot interval
    float secondClick = shootInterval * 2 / 3;

    if (Time.time - lastShootTime >= firstClick && !firstShotFired)
    {
      audioSource.time = 0;
      clip = soundManager.GetAudioClip("no_bullet");

      audioSource.clip = clip;
      audioSource.Play();
      firstShotFired = true;
    }
    if (Time.time - lastShootTime >= secondClick && !secondShotFired)
    {
      audioSource.time = 0;
      clip = soundManager.GetAudioClip("no_bullet");

      audioSource.clip = clip;
      audioSource.Play();
      secondShotFired = true;
    }
    if (Time.time - lastShootTime >= shootInterval)
    {
      clip = soundManager.GetAudioClip("laser_gun");
      audioSource.clip = clip;
      audioSource.time = 0.15f;
      audioSource.Play();

      ShootProjectile();
      lastShootTime = Time.time;
      firstShotFired = false;
      secondShotFired = false;
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
