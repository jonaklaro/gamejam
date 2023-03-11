using UnityEngine;


[CreateAssetMenu(fileName = "BossContainer", menuName = "ScriptableObjects/Boss")]
public class Bossstats : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float timeDamage;
    [SerializeField] private float timer;
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem particleEffect;
    [SerializeField] private ParticleSystem arealEffect;
}
