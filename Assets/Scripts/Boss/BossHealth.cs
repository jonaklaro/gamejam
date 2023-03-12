using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float healthTimer = 30f;
    [SerializeField] float healthTimerMax = 60f;

    [SerializeField] private FirstBoss firstBoss;

    [SerializeField] private Image healthBarSpriteLeft = null;
    [SerializeField] private Image healthBarSpriteRight = null;

    

    private void Start()
    {
        healthTimer = Mathf.Min(healthTimer, healthTimerMax);
    }

    void Update()
    {
        //healthTimer -= Time.deltaTime;
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
    
    void Die()
    {
        //Debug.Log("you ded");
        firstBoss.JustDie();
    }
}
