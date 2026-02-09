using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  [SerializeField] float sceneLoadDelay =2f;
  ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void PlayGame()
     {
        scoreKeeper.ResetScore();
        SceneManager.LoadSceneAsync("Endless Mode");
     }

     public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

   public void GameOver()
   {
    StartCoroutine(LoadDelay("Game Over",sceneLoadDelay));
   }

    public void QuitGame()
    {
      Application.Quit();
    }

    IEnumerator LoadDelay(string sceneName, float delay)
    {
      yield return new WaitForSeconds(delay);
      SceneManager.LoadScene(sceneName);
    }
}
