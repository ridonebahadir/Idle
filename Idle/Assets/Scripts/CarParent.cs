using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarParent : MonoBehaviour
{
    public int[] costMoneyCar;
    public float zAxis = -15;
    public float xAxis = -75;
    public GameManager gameManager;
   
   
    public List<GameObject> willGoCar;
    public HumanCollider humanCollider;
    public Transform sellArea;
   
   
    int moveZCarTime = 1;


   
    public void SellCar(Collider other)
    {
         
       int  a = willGoCar.Count;
        if (a > 0)
        {
            other.enabled = false;
             GameObject obj = willGoCar[0];
            obj.transform.DOMoveX(xAxis, 2f).OnComplete(()=>PayMoney());
           
            willGoCar.RemoveAt(0);
            a = willGoCar.Count;
            Debug.Log("a = " + a);
            if (a > 0)
            {
                for (int i = 0; i < a; i++)
                {
                    willGoCar[i].transform.DOMoveZ(willGoCar[i].transform.position.z-5, moveZCarTime);
                    zAxis -= 5;
                }
            }
            else
            {
                zAxis -= 5;
            }
            StartCoroutine(Late(other));

        }
    }
    IEnumerator Late(Collider other)
    {
        yield return new WaitForSeconds(moveZCarTime+0.5f);
        other.enabled = true;
    }
    void PayMoney()
    {
        StartCoroutine(humanCollider.PayMoney(costMoneyCar[gameManager.upgradeCount], sellArea, humanCollider.moneyListTir, humanCollider.payAreaTir));
    }
}
