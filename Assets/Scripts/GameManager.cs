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
  public enum GameState
  {
    Menu,
    Playing,
    GameOver
  }


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
    soundManager = SoundManager.instance;
    soundManager.SetVolume("MasterVolume", -20f);
    //make a list with "8Bit1" and "8Bit2" and then randomly pick one of them
    string randomMusic = "8Bit" + Random.Range(1, 3).ToString();
    Debug.Log("Random Music: " + randomMusic);
    soundManager.PlayMusic(randomMusic);
  }

  private void Update()
  {

  }
}