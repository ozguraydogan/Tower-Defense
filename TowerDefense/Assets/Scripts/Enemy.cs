using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed = 10f;
    public float starthealth = 100;
    private float health;
    
    public int worth = 50;
    public GameObject deathEffect;
    [Header("Unity Stuff")] 
    public Image healthBar;


    private void Start()
    {
        speed = startSpeed;
        health = starthealth;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health/starthealth;
        if (health <= 0)
        {
            Die();
        }
    }


    public void Slow(float pct)
    {
        speed = startSpeed *(1f - pct);
    }
    void Die()
    {
        
       GameObject effect= Instantiate(deathEffect, transform.position, Quaternion.identity);
       Destroy(effect,5f);
       WaveSpawner.EnemiesAlive--;
       PlayerStats.Money += worth;
        Destroy(gameObject);
    }

    
}
