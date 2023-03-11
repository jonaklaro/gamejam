using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTimer : MonoBehaviour
{
    [SerializeField] float healthTimer = 30f;
    [SerializeField] float healthTimerMax = 60f;

    [SerializeField] playerMovement playerMovement;

    [SerializeField] Image healthBarSpriteLeft = null;
    [SerializeField] Image healthBarSpriteRight = null;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Resource"))
        {
            Debug.Log("Resource collected");
            Destroy(other.gameObject);
            GatherRessource(10f);
        }
    }

    private void Start()
    {
        healthTimer = Mathf.Min(healthTimer, healthTimerMax);
    }

    void Update()
    {
        healthTimer -= Time.deltaTime;
        healthBarSpriteLeft.fillAmount = healthTimer / healthTimerMax;
        healthBarSpriteRight.fillAmount = healthBarSpriteLeft.fillAmount;

        if (healthTimer < 0)
            Die();
    }

    public void TakeDamage(float timeLost)
    {
        healthTimer -= timeLost;
        if (healthTimer < 0)
            Die();
    }

    void GatherRessource(float timeGained)
    {
        healthTimer += timeGained;
        healthTimer = Mathf.Min(healthTimer, healthTimerMax);
    }

    void Die()
    {
        //Debug.Log("you ded");
        playerMovement.Die();
        GetComponent<PlayerShoot>().enabled = false;
        enabled = false;
    }
}
