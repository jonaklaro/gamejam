using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    
    [SerializeField] private float timeUntilNextPhase;

    [SerializeField] private FirstBoss bossShoot;
    [SerializeField] private GameObject spawner;
    [SerializeField] Animator animator;

    private float timer;

    private void Awake()
    {
        StartCoroutine(BossFightStart());
        spawner.SetActive(false);

        //get sound manager
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        soundManager.PlayMusic("BossMusic");

    }

    private IEnumerator StartArealPhase()
    {
        animator.SetBool("IsShooting", false);
        animator.SetBool("CoolDown", true);
        spawner.SetActive(true);
        bossShoot.SetBool(false);
        yield return new WaitForSeconds(timeUntilNextPhase);
        StartCoroutine(BossFightStart());
    }
    
    private IEnumerator StartShootPhase()
    {
        animator.SetBool("IsShooting", true);
        bossShoot.SetBool(true);
        spawner.SetActive(false);
        yield return new WaitForSeconds(timeUntilNextPhase);
        StartCoroutine(StartArealPhase());
    }

    private IEnumerator BossFightStart()
    {
        animator.SetBool("CoolDown", false);
        yield return new WaitForSeconds(5);
        StartCoroutine(StartShootPhase());
    }
    
}
