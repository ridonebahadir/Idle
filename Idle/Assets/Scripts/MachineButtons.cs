using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineButtons : MonoBehaviour
{
    public GameObject bot;
    public int close;
    public GameManager gameManager;
    public int botCost;
    public SpriteRenderer sprite;
    public Material[] materials;
    public int id;
    
    private void Start()
    {
        close = PlayerPrefs.GetInt("Close"+id,0);
        sprite = GetComponent<SpriteRenderer>();
        if (close==1)
        {
            transform.gameObject.SetActive(false);
            bot.SetActive(true);
        }
       
        
    }
    private void Update()
    {
        if (gameManager.money>botCost)
        {
            sprite.material = materials[0];
        }
        else
        {
            sprite.material = materials[1];
        }
        
    }

    private void OnMouseDown()
    {
        if (gameManager.money>=botCost)
        {
            
            gameManager.money -= botCost;
            gameManager.moneyText.text = gameManager.money.ToString();
            bot.SetActive(true);
            close = 1;
            PlayerPrefs.SetInt("Close"+ id, close);
            transform.gameObject.SetActive(false);
        }
       
    }
}
