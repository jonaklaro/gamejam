using System;
using System.Collections;
using UnityEngine;

public class ArealAttackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] spawningPoint;
    [SerializeField] private int index;

    [Header("Timercounts")] [SerializeField]
    private float timeTillCollider;

    [SerializeField] private float timeAfterCollider;
    [SerializeField] private float areaCoolDown;

    [SerializeField] private float range;
    private bool itsHot = false;
    [SerializeField] private ParticleSystem arealEffect;
    

    private void Start()
    {
        spawningPoint = new Transform[index];
        for (int i = 0; i < spawningPoint.Length; i++)
        {
            spawningPoint[i] = this.transform.GetChild(i);
            Debug.Log(spawningPoint);
        }
        
    }

    private void Update()
    {
        for (int i = 0; i < spawningPoint.Length; i++)
        {
            
            float distance = GetDistance(spawningPoint[i].position);
            Debug.Log(distance);

            if (distance < range)
            {
                spawningPoint[i].gameObject.SetActive(true);
                ActivateArea(spawningPoint[i].gameObject);
            }
            
        }
    }

    private void ActivateArea(GameObject spawnPoint)
    {
        SpriteRenderer sprite = spawnPoint.GetComponent<SpriteRenderer>();
        sprite.color = Color.green;
        StartCoroutine(TimeTillColliderIsON(spawnPoint));
        itsHot = true;
        //Arealeffect starten
    }

    private float GetDistance(Vector2 spawningPoint)
    {
        float distance = Vector2.Distance(spawningPoint, player.transform.position);
        return distance;
    }

    private IEnumerator TimeTillColliderIsON(GameObject area)
    {
        yield return new WaitForSeconds(timeTillCollider);
        ActivateCollider(area);
    }

    private void ActivateCollider(GameObject area)
    {
        SpriteRenderer sprite = area.GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        Collider2D areaCollider = area.gameObject.GetComponent<Collider2D>();
        areaCollider.enabled = true;
        StartCoroutine(TimeTillColliderIsOFF(area));
    }

    private IEnumerator TimeTillColliderIsOFF(GameObject area)
    {
        yield return new WaitForSecondsRealtime(timeAfterCollider);
        DeactivateCollider(area);
    }
    private void DeactivateCollider(GameObject area)
    {
        Collider2D areaCollider = area.gameObject.GetComponent<Collider2D>();
        areaCollider.enabled = false;
        SpriteRenderer sprite = area.gameObject.GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        StartCoroutine(WaitTillItsCold());
    }

    private IEnumerator WaitTillItsCold()
    {
        yield return new WaitForSeconds(areaCoolDown);
        itsHot = false;
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
    }
}
