using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public Text upgradeCost;
    private Node target;
    public Button upgradeButton;
    public Text sellAmount;

    public void SetTarget(Node _target)
    {
    target = _target;
    
    transform.position = target.GetBuildPosition();

    if (!target.isUpgraded)
    {
        upgradeCost.text = "$"+target.turretBluePrint.upgradeCost;
        upgradeButton.interactable = true;
    }
    else
    {
        upgradeCost.text = "Done";
        upgradeButton.interactable = false;
    }

    sellAmount.text = "$" + target.turretBluePrint.GetSellAmount();
    
    
    ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
