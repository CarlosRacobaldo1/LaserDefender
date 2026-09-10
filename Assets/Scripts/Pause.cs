using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

   public void PauseGame()
   {
     pauseMenu.SetActive(true);
     Time.timeScale = 0;
     Debug.Log("Game Paused");
   }

   public void ResumeGame()
   {
     pauseMenu.SetActive(false);
     Time.timeScale = 1;
     Debug.Log("Game Resumed");
   }
}
