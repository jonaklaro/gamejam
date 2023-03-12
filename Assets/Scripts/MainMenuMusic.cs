using System;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{

    [SerializeField] private SoundManager soundManager;

    private void Start()
    {
        soundManager.SetVolume("MasterVolume", -20f);
        soundManager.PlayMusic("Lobby");
    }
}
