using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AllLine : MonoBehaviour
{
    public Transform customers;
    public Transform[] component;
    public int a = 0;
    public int currentCarNumber;
    public float showRoomZAxis = -43;
    public bool go;
    public Transform SellCustomer;

    public bool oneTime = true;
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
        StartCoroutine(Sell(SellCustomer, customers.transform.GetChild(0).GetChild(2).transform,true));
        customers.transform.GetChild(0).DOLocalMove(new Vector3(0, 0, 6), 2f).OnComplete(() => Destroy(customers.transform.GetChild(0).gameObject));
        customers.transform.GetChild(0).GetComponent<Animator>().SetBool("Walk",true);
        go = false;
    }


    IEnumerator Sell(Transform SellArea, Transform customer, bool runSell)
    {

        int count = customer.childCount - 1;

        while (runSell)
            {
            customer.GetChild(count).gameObject.SetActive(true);
            
            customer.GetChild(count).transform.DOJump(SellArea.transform.position, 3, 0, 0.1f, false)
               .OnComplete(() => {
                   customer.GetChild(count).gameObject.SetActive(false);
                   count--;
                   if (count<=0)
                   {
                       runSell = false;
                   }
               }).SetEase(Ease.InQuint);
                yield return new WaitForSeconds(0.1f);
            }
           
          
        
    }

}
