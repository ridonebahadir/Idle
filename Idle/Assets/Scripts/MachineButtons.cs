using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineButtons : MonoBehaviour
{
    public GameObject bot;
    public static bool close;
    public GameManager gameManager;
    public int botCost;
    public SpriteRenderer sprite;
    public Material[] materials;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (close)
        {
            transform.gameObject.SetActive(false);
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
        if (gameManager.money>botCost)
        {
            
            gameManager.money -= botCost;
            gameManager.moneyText.text = gameManager.money.ToString();
            bot.SetActive(true);
            close = true;
            transform.gameObject.SetActive(false);
        }
       
    }
}
