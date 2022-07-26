using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairButton : MonoBehaviour
{
    public Repair repair;
    public GameManager gameManager;
    public int repairCost;
    private void OnMouseDown()
    {
        if (gameManager.money> repairCost)
        {
            gameManager.money -= repairCost;
            gameManager.moneyText.text = gameManager.money.ToString();
            StartCoroutine(repair.Go());
        }
        
    }
}
