using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CapasityMachine : MonoBehaviour
{
    public Spawn spawn;
    public GameManager gameManager;
    public int cost;
    public TextMeshPro costValueText;
    public TextMeshPro mevcutValueText;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    public int id;
    private void Start()
    {
        spawn.capasity= PlayerPrefs.GetInt("Spawncapasity"+id,5);
        cost = PlayerPrefs.GetInt("CapasityMachineCost"+id, 10);
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        mevcutValueText.text = spawn.capasity.ToString();
        costValueText.text = "$ "+ cost.ToString();
    }
    private void Update()
    {
        if (gameManager.money>=cost)
        {
            spriteRenderer.color = Color.green;
            boxCollider2D.enabled = true;
        }
        else
        {
            spriteRenderer.color = Color.red;
            boxCollider2D.enabled = false;
        }
    }
    private void OnMouseDown()
    {
        if (gameManager.money>=cost)
        {
            gameManager.money -= cost;
            gameManager.moneyText.text = gameManager.money.ToString();
            spawn.capasity += 5;
            PlayerPrefs.SetInt("Spawncapasity"+id,spawn.capasity);
            mevcutValueText.text = spawn.capasity.ToString();
            cost += 50;
            PlayerPrefs.SetInt("CapasityMachineCost"+id,cost);
            costValueText.text = "$ " + cost.ToString();

        }
    }
}
