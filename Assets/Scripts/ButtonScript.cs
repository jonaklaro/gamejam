using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject alphonso;
    [SerializeField] private SoundManager soundManager;

    public void StartButton()
    {
        soundManager.SetVolume("MasterVolume", -20f);
        soundManager.PlayMusic("8Bit1");
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
