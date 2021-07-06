using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
  public GameObject ui;

    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    public void Retry()
    {
        sceneFader.FateTo(SceneManager.GetActiveScene().name);
     
    }

    public void Menu()
    {
        sceneFader.FateTo(menuSceneName);
    }
}
