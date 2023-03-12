using System;
using UnityEngine;

public class AreaCollision : MonoBehaviour
{
    private PlayerHealthTimer playerHealthTimer;
    private float timeLost = 20f;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerHealthTimer = player.GetComponent<PlayerHealthTimer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
            MakeDamage();
        }
    }

    private void MakeDamage()
    {
        Debug.Log("Ich hitte den Player");
        playerHealthTimer.TakeDamage(timeLost);
    }
}
