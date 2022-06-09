using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public GameObject money;
    public Transform moneyParent;
    public List<GameObject> listMoney;
   
    int count = 10;
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
           GameObject obj= Instantiate(money, moneyParent);
            listMoney.Add(obj);
            obj.SetActive(false);
        }
       
    }

   
}
