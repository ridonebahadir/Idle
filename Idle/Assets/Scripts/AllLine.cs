using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AllLine : MonoBehaviour
{
    public GameManager gameManager;
    public Transform customers;
    public Transform[] component;
    public Spawn[] spawn;
    public int a = 0;
    public int currentCarNumber;
    public float showRoomZAxis = -43;
    public bool go;
    public Transform SellCustomer;
    private Customers customer;
    public bool oneTime = true;
    public int carValue;
    private int oneMoneyValue;
    private void Start()
    {
        oneMoneyValue = carValue / 10;
    }
    void Update()
    {
        if (go)
        {
            if (!oneTime)
            {

                Go();
                oneTime = false;
            }

        }
    }

    private void Go()
    {
        customer = customers.transform.GetChild(0).GetComponent<Customers>();
        StartCoroutine(Sell(SellCustomer,true));
        customers.transform.GetChild(0).DOLocalMove(new Vector3(0, 0, 6), 4f).OnComplete(() => Destroy(customers.transform.GetChild(0).gameObject));
        customers.transform.GetChild(0).GetComponent<Animator>().SetBool("Walk", true);
        go = false;
    }


    IEnumerator Sell(Transform SellArea, bool runSell)
    {

        int count = customer.listMoney.Count - 1;
        int a = 9;
        while (runSell)
            {
           
            customer.listMoney[count].gameObject.SetActive(true);
            customer.listMoney[count].gameObject.transform.parent = SellArea;
            count--;
            if (count <= 0)
            {
                runSell = false;
            }
            customer.listMoney[count + 1].gameObject.transform.DOLocalJump(Vector3.zero, 3, 0, 1f, false)

               .OnComplete(() =>
               {
                   Debug.Log("Count" + a);
                   Destroy(customer.listMoney[a].gameObject);
                   gameManager.money += oneMoneyValue;
                   gameManager.moneyText.text = gameManager.money.ToString();
                   a--;


               });
                yield return new WaitForSeconds(0.1f);
            }
           
          
        
    }

}
