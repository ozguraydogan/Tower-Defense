using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameOverUI;
    
    public string nextLevel = "Level02";
    public int levelToUnluck = 2;
    public SceneFader sceneFader;
    
    private void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if(GameIsOver)
            return;
      
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
            GetComponent<WaveSpawner>().enabled = false;
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        PlayerPrefs.SetInt(nextLevel,levelToUnluck );
        sceneFader.FateTo(nextLevel);
    }
}
