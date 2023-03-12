using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public List<Friends> friends;
  public bool itemDropped = false;
  [SerializeField] Animator animator;

  public bool hasLevelEnded = false;
  SoundManager soundManager;

  //enum for the different states of the game



  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    //get the sound manager from previous scene
    soundManager = FindObjectOfType<SoundManager>();
    soundManager.SetVolume("MasterVolume", -20f);
    soundManager.SetVolume("MusicVolume", -5f);
    soundManager.SetVolume("SFXVolume", 0);
    string randomMusic = "8Bit" + Random.Range(1, 3).ToString();
    Debug.Log("Random Music: " + randomMusic);
    soundManager.PlayMusic(randomMusic);
  }

  private void Update()
  {

  }
}