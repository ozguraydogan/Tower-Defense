
using System;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    private Transform target;
    private Enemy targetEnemy;
    [Header("General")]
    public float range = 15f;
    
    [Header("Use Bullet (default)")]
    public GameObject bulletPrefab;
    public float fireRate=1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")] 
    public float slowAmount = .5f;
    public bool useLaser = false;
    public ParticleSystem impactEffect;
    public LineRenderer lineRenderer;
    public Light impactLight;
    
    public int damageOverTime=30;
    
    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed=10f;

    public Transform firePoint;
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                nearestEnemy = enemy; 
            }
        }

        if (nearestEnemy != null && shortesDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            
            return;
        }

        LockOnTarget();
        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
   

      
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position +dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
       
    }

    void LockOnTarget()
    {
    
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime*turnSpeed).eulerAngles;
        partToRotate.rotation= Quaternion.Euler(0f,rotation.y,0f);
    }

    void Shoot()
    {
        GameObject bulletGO= (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
