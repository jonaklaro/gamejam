using System;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{

  [SerializeField] private SoundManager soundManager;

  private void Start()
  {
    //get the sound manager, if it's not already set
    if (soundManager == null)
    {
      soundManager = FindObjectOfType<SoundManager>();
    }
    soundManager.SetVolume("MasterVolume", -20f);
    soundManager.SetVolume("MusicVolume", -5f);
    soundManager.SetVolume("SFXVolume", 0);
    soundManager.PlayMusic("Lobby");
  }
}
