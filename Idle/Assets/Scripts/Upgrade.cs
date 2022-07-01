using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public SellRaw sellRaw;
    public GameManager gameManager;
    public int cost;
    public bool isCapacity;
    public TextMeshPro costText;
    public TextMeshPro textUpgrade;
    public SpriteRenderer colorChangeObj;
    public Color ok;
    public Color not;
    private void Start()
    {
        if (isCapacity)
        {
            textUpgrade.text = sellRaw.capasity.ToString();
            
        }
        else
        {
            textUpgrade.text = sellRaw.animTime.ToString();
        }
        costText.text = cost.ToString();
        
    }
    private void OnMouseDown()
    {
        Control();
        if (gameManager.money>cost)
        {
            
            cost += 5;
            costText.text = cost.ToString();    
            gameManager.money -= cost;
            gameManager.moneyText.text = gameManager.money.ToString();
            if (isCapacity)
            {
                sellRaw.capasity += 5;
                PlayerPrefs.SetInt("Capasity", sellRaw.capasity);
               
                textUpgrade.text = sellRaw.capasity.ToString();
            }
            else
            {
                if (sellRaw.animTime>0.1f)
                {
                    sellRaw.animTime -= 1f;
                     PlayerPrefs.SetFloat("AnimTime", sellRaw.animTime);
                    textUpgrade.text = sellRaw.animTime.ToString();
                }
                
            }
           
        }
       
       
    }
    public void Control()
    {
        if (gameManager.money>cost)
        {
            colorChangeObj.color = ok;
        }
        else
        {
            colorChangeObj.color = not;
        }
    }
}
