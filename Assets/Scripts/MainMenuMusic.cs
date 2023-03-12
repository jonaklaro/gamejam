using System;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{

  [SerializeField] private SoundManager soundManager;

  private void Start()
  {
    //if sound manager not null
    if (soundManager == null)
    {
      //get the sound manager from previous scene
      soundManager = FindObjectOfType<SoundManager>();
      //play the music
      soundManager.PlayMusic("Lobby");
    }
  }
}
