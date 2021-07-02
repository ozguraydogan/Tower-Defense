
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    
    public Color hoverColor;
    public Vector3 positionOffset;
    public Color notEnoughMoneyColor;
    
    [HideInInspector]
    public GameObject turret;

    [HideInInspector] 
    public TurretBluePrint turretBluePrint;

    [HideInInspector] 
    public bool isUpgraded = false;
    
    private Renderer rend;
    private Color startColor;

    private BuildManager _buildManager;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        _buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        
        if (turret != null)
        {
            _buildManager.SelectNode(this);
            Debug.Log("Can't build there! - TODO: Display on screen.");
            return;
        }
        if (!_buildManager.CanBuild)
            return;
        
        BuildTurret(_buildManager.GetTurretToBuild());

        //_buildManager.BuildTurrentOn(this);
    }

    void BuildTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.Money < bluePrint.cost)
        {
            Debug.Log("Not enough money to build that !");
            return;
        }

        PlayerStats.Money -= bluePrint.cost;
        GameObject _turret= (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(),Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;

        GameObject effect=(GameObject)Instantiate(_buildManager.buildEfect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        Debug.Log("Turret build!");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that !");
            return;
        }

        PlayerStats.Money -= turretBluePrint.upgradeCost;
        
        // Get rid of the old turret
        Destroy(turret);
        
        // Build a new one
        GameObject _turret= (GameObject)Instantiate(turretBluePrint.upgradedPrefab , GetBuildPosition(),Quaternion.identity);
        turret = _turret;
        GameObject effect=(GameObject)Instantiate(_buildManager.buildEfect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        isUpgraded = true;
        
        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();
        //Spawn a cool effect
        GameObject effect=(GameObject)Instantiate(_buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        
        Destroy(turret);
        turretBluePrint = null;
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() == null)
            return;
        
        if (!_buildManager.CanBuild)
            return;

        if (_buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        
 
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
