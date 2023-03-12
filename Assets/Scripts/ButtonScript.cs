using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject alphonso;

    public void StartButton()
    {
        SceneManager.LoadScene(3);
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ControlButton()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ClickAlphonso()
    {
        alphonso.SetActive(true);
    }
}
