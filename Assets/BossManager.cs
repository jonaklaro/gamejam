using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    
    [SerializeField] private float timeUntilNextPhase;

    [SerializeField] private FirstBoss bossShoot;
    [SerializeField] private GameObject spawner;

    private float timer;

    private void Awake()
    {
        StartCoroutine(BossFightStart());
        spawner.SetActive(false);
    }

    private IEnumerator StartArealPhase()
    {
        spawner.SetActive(true);
        bossShoot.SetBool(false);
        yield return new WaitForSeconds(timeUntilNextPhase);
        StartCoroutine(BossFightStart());
    }
    
    private IEnumerator StartShootPhase()
    {
        bossShoot.SetBool(true);
        spawner.SetActive(false);
        yield return new WaitForSeconds(timeUntilNextPhase);
        StartCoroutine(StartArealPhase());
    }

    private IEnumerator BossFightStart()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(StartShootPhase());
    }
}
