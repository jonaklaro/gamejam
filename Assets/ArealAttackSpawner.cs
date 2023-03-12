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
    [SerializeField] private ParticleSystem arealEffect;
    private bool isHot;


    private void Start()
    {
        spawningPoint = new Transform[index];
        for (int i = 0; i < spawningPoint.Length; i++)
        {
            spawningPoint[i] = this.transform.GetChild(i);
            Collider2D areaCollider = spawningPoint[i].gameObject.GetComponent<Collider2D>();
            areaCollider.enabled = false;
            
            Debug.Log(spawningPoint);
        }

    }

    private void Update()
    {
        for (int i = 0; i < spawningPoint.Length; i++)
        {

            float distance = GetDistance(spawningPoint[i].position);

            if (distance < range)
            {
                if (!isHot)
                {
                    isHot = true;
                    ActivateArea(spawningPoint[i].gameObject);
                }
                
            }

        }
    }

    private void ActivateArea(GameObject spawnPoint)
    {
        SpriteRenderer sprite = spawnPoint.GetComponent<SpriteRenderer>();
        sprite.color = Color.green;
        StartCoroutine(TimeTillColliderIsON(spawnPoint));
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
        isHot = false;
    }

}
