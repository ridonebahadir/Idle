using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;
    private void Start()
    {
        moneyText.text = money.ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Sell(GameObject abj)
    {
        Debug.Log(abj.name);
    }
}
