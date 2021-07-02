using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public  SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    private void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        //SceneManager.LoadScene(0);
        sceneFader.FateTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FateTo(menuSceneName);
    }
}
