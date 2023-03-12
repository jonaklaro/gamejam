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


    private void Awake()
    {
        spawningPoint = new Transform[index];
        for (int i = 0; i < spawningPoint.Length; i++)
        {
            spawningPoint[i] = this.transform.GetChild(i);
            Collider2D areaCollider = spawningPoint[i].gameObject.GetComponent<Collider2D>();
            areaCollider.enabled = false;
           
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
        GameObject effect = spawnPoint.transform.GetChild(0).gameObject;
        effect.SetActive(true);
        StartCoroutine(TimeTillColliderIsON(spawnPoint, effect));
    }

    private float GetDistance(Vector2 spawningPoint)
    {
        float distance = Vector2.Distance(spawningPoint, player.transform.position);
        return distance;
    }

    private IEnumerator TimeTillColliderIsON(GameObject area, GameObject effekt)
    {
        yield return new WaitForSeconds(timeTillCollider);
        ActivateCollider(area, effekt);
    }

    private void ActivateCollider(GameObject area, GameObject effect)
    {
        Collider2D areaCollider = area.gameObject.GetComponent<Collider2D>();
        areaCollider.enabled = true;
        effect.SetActive(false);
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
        Debug.Log(isHot);
    }
    

}
