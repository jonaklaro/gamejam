using System;
using UnityEngine;

public class AreaCollision : MonoBehaviour
{
    private PlayerHealthTimer playerHealthTimer;
    private float timeLost = 20f;
    private GameObject player;
    private ParticleSystem[] arealEffect;
    [SerializeField] private ParticleSystem particleOne;
    [SerializeField] private ParticleSystem particleTwo;
    [SerializeField] private ParticleSystem particlethree;
    [SerializeField] private ParticleSystem particleFour;
    [SerializeField] private ParticleSystem particleFive;
    [SerializeField] private ParticleSystem particleSix;
    [SerializeField] private ParticleSystem particleSeven;
    [SerializeField] private ParticleSystem particleEight;
    private CircleCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        player = GameObject.FindWithTag("Player");
        playerHealthTimer = player.GetComponent<PlayerHealthTimer>();
        arealEffect = new[]
        {
            particleOne, particleTwo, particlethree, particleFour, particleFive, particleSix, particleSeven,
            particleEight
        };

        if (collider.enabled == true)
        {
            for (int i = 0; i < arealEffect.Length; i++)
            {
                ParticleSystem effekt = Instantiate(arealEffect[i],transform.position, Quaternion.identity);
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
           // MakeDamage();
        }
    }

    private void MakeDamage()
    {
        Debug.Log("Ich hitte den Player");
        playerHealthTimer.TakeDamage(timeLost);
    }
}
