using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;
    public Wave[] waves;

    public Transform enemyPrefab;
    public Transform spawnPoint;
    
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveIndex = 0;

    public GameManager gameManager;

    public Text waveCountdownText;
    public int counter=0;
    
    private void Start()
    {
        EnemiesAlive = 0;
        Debug.Log("Start");
    }

    private void Update()
    {
        if (EnemiesAlive>0)
        {
            return; 
        }
        
        if (countdown <= 0f)
        {
            Debug.Log("deneme");
             StartCoroutine(SpawnWave());
             countdown = timeBetweenWaves;
             return;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}",countdown);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        //EnemiesAlive = wave.count;  //yeniden spawn olmasını engelliyor
        PlayerStats.Rounds++;
        counter = wave.count;
      
            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }
            
            waveIndex++;
            
            if(waveIndex==waves.Length &&counter<=0)
            {
                Debug.Log("Level Won");
                gameManager.WinLevel();
         
                //SceneManager.LoadScene(SceneManager.GetActsiveScene().buildIndex);

                this.enabled = false;
            }
        
   
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position,spawnPoint.rotation);
        EnemiesAlive++;
        Debug.Log(EnemiesAlive.ToString());
    }
}
