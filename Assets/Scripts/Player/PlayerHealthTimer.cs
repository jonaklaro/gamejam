using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthTimer : MonoBehaviour
{
    [SerializeField] float healthTimer = 30f;
    [SerializeField] float healthTimerMax = 60f;

    [SerializeField] playerMovement playerMovement;

    [SerializeField] Image healthBarSpriteLeft = null;
    [SerializeField] Image healthBarSpriteRight = null;
    

    int highscore = 0;

    [SerializeField] TextMeshProUGUI highscoreText;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Resource"))
        {
            Destroy(other.gameObject);
            GatherRessource(15f);
        } 
        else if (other.gameObject.CompareTag("ProjectileEnemy"))
        {
            TakeDamage(5);
        }
    }

    private void Start()
    {
        healthTimer = Mathf.Min(healthTimer, healthTimerMax);
    }

    void Update()
    {
        highscore = (int)(Time.time * 10);
        highscoreText.text = highscore.ToString();

        healthTimer -= Time.deltaTime;
        healthBarSpriteLeft.fillAmount = healthTimer / healthTimerMax;
        healthBarSpriteRight.fillAmount = healthBarSpriteLeft.fillAmount;

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

    public void KilledEnemy()
    {
        highscore += 50;
    }
}
