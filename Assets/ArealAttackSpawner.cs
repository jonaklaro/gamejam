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
    /*private ParticleSystem[] arealEffect;
    [SerializeField] private ParticleSystem particleOne;
    [SerializeField] private ParticleSystem particleTwo;
    [SerializeField] private ParticleSystem particlethree;
    [SerializeField] private ParticleSystem particleFour;
    [SerializeField] private ParticleSystem particleFive;
    [SerializeField] private ParticleSystem particleSix;
    [SerializeField] private ParticleSystem particleSeven;
    [SerializeField] private ParticleSystem particleEight;*/
    
    private bool isHot;


    private void Awake()
    {
        /*arealEffect = new[]
        {
            particleOne, particleTwo, particlethree, particleFour, particleFive, particleSix, particleSeven,
            particleEight
        };*/
        spawningPoint = new Transform[index];
        for (int i = 0; i < spawningPoint.Length; i++)
        {
            spawningPoint[i] = this.transform.GetChild(i);

        }

    }

    private void Update()
    {
        for (int i = 0; i < spawningPoint.Length; i++)
        {

            float distance = GetDistance(spawningPoint[i].position);

            if (distance < range)
            {
                ActivateArea(spawningPoint[i].gameObject);
             
            }

        }
    }

    private void ActivateArea(GameObject spawnPoint)
    {

        AreaCollision area = spawnPoint.GetComponent<AreaCollision>();
        area.gameObject.SetActive(true);
        Debug.Log("Ich  starte");
        StartCoroutine(TimeTillColliderIsOFF(spawnPoint));
    }

    private float GetDistance(Vector2 spawningPoint)
    {
        float distance = Vector2.Distance(spawningPoint, player.transform.position);
        return distance;
    }

    /*private IEnumerator TimeTillColliderIsON(GameObject area)
    {
        Debug.Log("bin in der nächsten Methode");
        yield return new WaitForSeconds(timeTillCollider);
        ActivateCollider(area);
    }*/

    /*private void ActivateCollider(GameObject area)
    {
        Collider2D areaCollider = area.gameObject.GetComponent<Collider2D>();
        areaCollider.enabled = true;
        StartCoroutine(TimeTillColliderIsOFF(area));
    }*/

    private IEnumerator TimeTillColliderIsOFF(GameObject area)
    {
        yield return new WaitForSecondsRealtime(timeAfterCollider);
        DeactivateCollider(area);
    }

    private void DeactivateCollider(GameObject area)
    {
        AreaCollision areaCollision = area.GetComponent<AreaCollision>();
        areaCollision.gameObject.SetActive(false);
        StartCoroutine(WaitTillItsCold());
    }

    private IEnumerator WaitTillItsCold()
    {
        yield return new WaitForSeconds(areaCoolDown);
        isHot = false;
        Debug.Log(isHot);
    }
    

}
