using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform human;
    public CameraFollow cameraFollow;
    public int money;
    public TextMeshProUGUI moneyText;
    [Header("Lock")]
   
    public Transform bant;
    public Transform productLine;
    public bool locked = true;
    public Transform[] tasarimPos;
    public Transform[] makineHammadde;
    private void Awake()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        if (locked)
        {
            for (int i = 0; i < tasarimPos.Length; i++)
            {
                tasarimPos[i].transform.localPosition = new Vector3(tasarimPos[i].transform.localPosition.x,20, tasarimPos[i].transform.localPosition.z);
            }
            for (int i = 0; i < makineHammadde.Length; i++)
            {
                makineHammadde[i].gameObject.SetActive(false);
            }

        }
    }
    private void Start()
    {
       
        moneyText.text = money.ToString();
        //if (!locked)
        //{
        //    foreach (var item in cameraFollow.transforms)
        //    {
        //        item.gameObject.SetActive(true);
        //    }
        //    foreach (Transform item in bant)
        //    {
        //        item.gameObject.SetActive(true);
        //        item.transform.localPosition = new Vector3(item.transform.localPosition.x, 0, item.transform.localPosition.z);
                
        //    }
        //    productLine.gameObject.SetActive(true);
        //    foreach (var item in close)
        //    {
        //        item.SetActive(false);
        //    }
        //    for (int i = 0; i < open.Length; i++)
        //    {
        //        for (int x = 0; x < 3; x++)
        //        {
        //            open[i].transform.GetChild(x).gameObject.SetActive(true);
        //            open[i].transform.GetChild(x).transform.localPosition=new Vector3(open[i].transform.GetChild(x).transform.localPosition.x, 0, open[i].transform.GetChild(x).transform.localPosition.z);

        //        }
        //    }
        //}
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("Money", money);
        OnApplicationQuit();
    }
    public void Sell(GameObject abj)
    {
        Debug.Log(abj.name);
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        MachineButtons.close = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        PlayerPrefs.SetInt("Money",money);
     
    }
   
   
    public void GoCameraUp()
    {
        cameraFollow.CameraGoUp();
    }
    
}
