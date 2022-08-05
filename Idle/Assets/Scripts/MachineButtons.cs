using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineButtons : MonoBehaviour
{
    public GameObject bot;
    public int close;
    public GameManager gameManager;
    public int botCost;
    private SpriteRenderer sprite;
    public Material[] materials;
    public int id;
    public TextMeshPro costText;
    [Header("UpgradeBot")]
    public SpriteRenderer logoSprite;
    public Sprite botCapasityLogo;
    public int costCapasity;
    public TextMeshPro capasityText;
    private HumanCollider botHumanCollider;
    BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        botHumanCollider = bot.GetComponent<HumanCollider>();
        botHumanCollider.capasity=PlayerPrefs.GetInt("BotCapasity" + id, 5);
        close = PlayerPrefs.GetInt("Close"+id,0);
        sprite = GetComponent<SpriteRenderer>();
        if (close==1)
        {
            capasityText.gameObject.SetActive(true);
            capasityText.text = botHumanCollider.capasity.ToString();

            costCapasity = PlayerPrefs.GetInt("CostCapasity" + id, costCapasity);
            costText.text = "$ " + costCapasity.ToString();
            logoSprite.sprite = botCapasityLogo;
            bot.SetActive(true);
        }
        else
        {
            costText.text = "$ " + botCost.ToString();
        }
       
        
    }
    private void Update()
    {
        if (close==1)
        {
            if (gameManager.money >= costCapasity)
            {
                boxCollider.enabled = true;
                sprite.material = materials[0];
            }
            else
            {
                boxCollider.enabled = false;
                sprite.material = materials[1];
            }
        }
        else
        {
            if (gameManager.money >= botCost)
            {
                sprite.material = materials[0];
            }
            else
            {
                sprite.material = materials[1];
            }
        }
       
        
    }

    private void OnMouseDown()
    {
        if (close==1)
        {
            gameManager.money -= costCapasity;
            gameManager.moneyText.text = gameManager.money.ToString();
            botHumanCollider.capasity += 5;
            PlayerPrefs.SetInt("BotCapasity"+id,botHumanCollider.capasity);
            capasityText.gameObject.SetActive(true);
            capasityText.text = bot.GetComponent<HumanCollider>().capasity.ToString();
            costCapasity += 50;
            PlayerPrefs.SetInt("CostCapasity" + id, costCapasity);
            costText.text = "$ " + costCapasity.ToString();
            logoSprite.sprite = botCapasityLogo;
        }
        else
        {
            if (gameManager.money >= botCost)
            {

                gameManager.money -= botCost;
                gameManager.moneyText.text = gameManager.money.ToString();
                bot.SetActive(true);
                capasityText.gameObject.SetActive(true);
                capasityText.text = botHumanCollider.capasity.ToString();

                costCapasity = PlayerPrefs.GetInt("CostCapasity" + id, costCapasity);
                costText.text = "$ " + costCapasity.ToString();
                logoSprite.sprite = botCapasityLogo;
                close = 1;
                PlayerPrefs.SetInt("Close" + id, close);
                //transform.gameObject.SetActive(false);
            }
        }
       
       
    }
}
