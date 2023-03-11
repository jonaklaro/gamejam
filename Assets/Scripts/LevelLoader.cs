using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
  public Animator transition;
  public float transitionTime = 2f;

  private GameManager gameManager;

  void Start()
  {
    gameManager = GameManager.Instance;
  }

  // Update is called once per frame
  void Update()
  {
    if (gameManager.hasLevelEnded == true)
    {
      LoadNextLevel();
    }

  }

  public void LoadNextLevel()
  {
    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
  }

  IEnumerator LoadLevel(int levelIndex)
  {
    transition.SetTrigger("levelEnd");
    yield return new WaitForSeconds(transitionTime);
    SceneManager.LoadScene(levelIndex);
  }
}
