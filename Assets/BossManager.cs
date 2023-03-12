using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool shootPhase;
    [SerializeField] private bool arealPhase;
    [SerializeField] private float timeUntilNextPhase;

    [SerializeField] private FirstBoss bossShoot;
    [SerializeField] private GameObject spawner;

    private float timer;

    private void Awake()
    {
        StartCoroutine(BossFightStart());
        spawner.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        
        if (shootPhase)
        {
            bossShoot.SetBool(true);
            spawner.SetActive(false);
            StartCoroutine(StartArealPhase());
        }

        if (arealPhase)
        {
            Debug.Log("Neuephase");
            spawner.SetActive(true);
            bossShoot.SetBool(false);
            StartCoroutine(StartShootPhase());
        }

    }

    private IEnumerator StartArealPhase()
    {
        yield return new WaitForSeconds(timeUntilNextPhase);
        shootPhase = false;
        arealPhase = true;
    }
    
    private IEnumerator StartShootPhase()
    {
        yield return new WaitForSeconds(timeUntilNextPhase);
        shootPhase = true;
        arealPhase = false;
    }

    private IEnumerator BossFightStart()
    {
        yield return new WaitForSeconds(5);
        shootPhase = true;
    }
}
