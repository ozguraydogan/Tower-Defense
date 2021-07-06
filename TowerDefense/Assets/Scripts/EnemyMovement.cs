using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private int wavepointIndex = 0;
    private Transform target;

    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        
        target = Waypoints.points[0];
    }
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized*_enemy.speed*Time.deltaTime,Space.World );
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        _enemy.speed = _enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex>=Waypoints.points.Length-1)
        {
            EndPath();
            return;
        }
        
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        WaveSpawner.EnemiesAlive--;
        PlayerStats.Lives--;
        
        Destroy(gameObject);
    }
}
