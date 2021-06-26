
using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Build manager in scene");
            return;
        }
        instance = this;
    }

    public GameObject standartTurretPrefab;
    private void Start()
    {
        turretToBuild = standartTurretPrefab;
    }

    private GameObject turretToBuild;
    
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
