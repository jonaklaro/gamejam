using UnityEngine;


[CreateAssetMenu(fileName = "BossContainer", menuName = "ScriptableObjects/Boss")]
public class Bossstats : ScriptableObject
{
    [SerializeField] public float health;
    [SerializeField] public GameObject bullet;
    [SerializeField] public float timeDamage;
    [SerializeField] public float timer;
    [SerializeField] public GameObject player;
    [SerializeField] public ParticleSystem particleEffect;
    [SerializeField] public ParticleSystem arealEffect;
    [SerializeField] public float shootTime;
}
