using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTimer : MonoBehaviour
{
    [SerializeField] float healthTimer = 30f;
    [SerializeField] float healthTimerMax = 60f;

    [SerializeField] Color color1;
    [SerializeField] Color color2;

    [SerializeField] playerMovement playerMovement;

    [SerializeField] Image healthBarSpriteLeft = null;
    [SerializeField] Image healthBarSpriteRight = null;

    int highscore = 0;
    [SerializeField] LevelManager levelManager;

    private void Start() {
      healthTimer = Mathf.Min(healthTimer, healthTimerMax);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Resource"))
        {
            Destroy(other.gameObject);
            GatherRessource(15f);
            Debug.Log("Resource collected");
            levelManager.CollectItem();
        } 
        else if (other.gameObject.CompareTag("ProjectileEnemy") /*&& GetDistance(other.gameObject) < .2f*/)
        {
            TakeDamage(5);
            other.gameObject.GetComponent<EnemyBullet>().DestroyBullet();
        }
    }


    void Update()
    {
        healthTimer -= Time.deltaTime;
        float fillAmount = healthTimer / healthTimerMax;
        healthBarSpriteLeft.fillAmount = fillAmount;
        healthBarSpriteLeft.color = LerpColor(color2, color1, fillAmount);

        healthBarSpriteRight.fillAmount = fillAmount;
        healthBarSpriteRight.color = LerpColor(color2, color1, fillAmount);

        if (healthTimer < 0)
            Die();
    }

    public void TakeDamage(float timeLost)
    {
        healthTimer -= timeLost;
        healthBarSpriteLeft.fillAmount = healthTimer / healthTimerMax;
        healthBarSpriteRight.fillAmount = healthBarSpriteLeft.fillAmount;
        if (healthTimer < 0)
            Die();
    }

    void GatherRessource(float timeGained)
    {
        healthTimer += timeGained;
        healthTimer = Mathf.Min(healthTimer, healthTimerMax);
        healthBarSpriteLeft.fillAmount = healthTimer / healthTimerMax;
        healthBarSpriteRight.fillAmount = healthBarSpriteLeft.fillAmount;
    }

    void Die()
    {
        //Debug.Log("you ded");
        playerMovement.Die();
        GetComponent<PlayerShoot>().enabled = false;
        enabled = false;
    }

    Color LerpColor(Color  c1, Color c2, float t)
    {
        return new Color(
            Mathf.Lerp(c1.r, c2.r, t),
            Mathf.Lerp(c1.g, c2.g, t),
            Mathf.Lerp(c1.b, c2.b, t));
    }

}

