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

    //make enemy flash red when hit
    StartCoroutine(FlashRed());

    if (enemyLife > 1)
    {
      AudioSource audioSource = GetComponent<AudioSource>();
      //get own audio source and play sound from sound manager
      audioSource.clip = SoundManager.instance.GetAudioClip("EnemyHit");
      audioSource.time = 0.7f;
      audioSource.Play();

    }

    enemyLife--; // Reduce enemy life by 1
    if (enemyLife <= 0)
    {
      //create new audio source and play sound from sound manager
      AudioSource audioSource = gameObject.AddComponent<AudioSource>();
      audioSource.clip = SoundManager.instance.GetAudioClip("Explosion_Tiny_4");
      audioSource.time = 0.0f;
      audioSource.Play();

      //delete audio source after sound is played
      // Destroy(audioSource, audioSource.clip.length);
      Destroy(gameObject); // Destroy the enemy if its life reaches 0
    }
  }

  private System.Collections.IEnumerator FlashRed()
  {
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.color = Color.red;
    yield return new WaitForSeconds(0.1f);
    spriteRenderer.color = Color.white;
  }
  
  float GetDistance(GameObject other)
  {
    Vector3 vecBetweenObjects = transform.position - other.transform.position;
    return vecBetweenObjects.magnitude;
  }
}
