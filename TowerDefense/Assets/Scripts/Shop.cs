
using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standartTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint laserBeamer;
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager=BuildManager.instance;
    }

    public void SelectStandartTurret()
    {
         Debug.Log("Standart Turret Selected");
         _buildManager.SelectTurretToBuild(standartTurret);
    }
    public void SelectMissileLauncher()
    {
        Debug.Log("Missle Launcher Selected");
        _buildManager.SelectTurretToBuild(missileLauncher);
    }
    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selected");
        _buildManager.SelectTurretToBuild(laserBeamer);
    }
    
}
