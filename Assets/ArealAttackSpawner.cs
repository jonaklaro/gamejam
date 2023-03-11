using System;
using System.Collections;
using UnityEngine;

public class ArealAttackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawningPoint;

    [Header("Timercounts")] [SerializeField]
    private float timeTillCollider;

    [SerializeField] private float range;
    private GameObject player;

    [SerializeField] private ParticleSystem arealEffect;
    

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawningPoint = new GameObject[13];
        for (int i = 0; i < spawningPoint.Length; i++)
        {
            spawningPoint[i].SetActive(false);
        }
    }

    private void Update()
    {
        for (int i = 0; i < spawningPoint.Length; i++)
        {
            Vector2 spawnPos = new Vector2(spawningPoint[i].transform.position.x, spawningPoint[i].transform.position.y);
            float distance = GetDistance(spawnPos);

            if (distance < range)
            {
                spawningPoint[i].SetActive(true);
                ActivateArea(spawningPoint[i]);
            }
            
        }
    }

    private void ActivateArea(GameObject spawnPoint)
    {
        
       //Arealeffect starten
    }

    private float GetDistance(Vector2 spawningPoint)
    {
        float distance = Vector2.Distance(spawningPoint, player.transform.position);
        return distance;
    }

    private IEnumerator TurnColliderON()
    {
        yield return new WaitForSeconds(timeTillCollider);
        TurnColliderON();
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
        
    }
}
