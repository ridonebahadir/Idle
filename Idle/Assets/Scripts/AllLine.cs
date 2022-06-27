using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AllLine : MonoBehaviour
{
    public HumanCollider humanCollider;
    public Transform payParent;
    public GameManager gameManager;
    //public Transform customers;
    public Transform[] component;
    public Spawn[] spawn;
    public int a = 0;
    public int currentCarNumber;
    public float showRoomZAxis = -43;
    public float showRoomXAxis = -55;
    public bool go;
    //public Transform SellCustomer;
    //private Customers customer;
    public bool oneTime = true;
    public int carValue;
    private int oneMoneyValue;
    public Transform Truck;
    public GameObject TruckObj;
    public Animator[] wheelAnim;
    public int carAdet;
    public GameObject newCar;
    private float axisXStart;
    private Vector3 truckStartPos;
    public BoxCollider kasaCollider;
    private void Start()
    {
        oneMoneyValue = carValue / 10;
        axisXStart = showRoomXAxis;
        truckStartPos = Truck.transform.position;
    }
    public void GoTruck(Vector3 pos)
    {
        GameObject obj = Instantiate(newCar, pos , Quaternion.Euler(0, -90, 0), Truck);
        //obj.GetComponent<ProductLine>().enabled = false;
        //car.GetComponent<ProductLine>().enabled = false;
       
        //car.transform.parent = Truck;
        if (carAdet==3)
        {
            kasaCollider.enabled = false;
            showRoomXAxis = axisXStart;
            for (int i = 0; i < wheelAnim.Length; i++)
            {
                wheelAnim[i].enabled = true;
            }
            Truck.transform.DOMove(new Vector3(-40, 0, 0) + Truck.transform.position, 3f).OnComplete(()=>NextTruck());
            StartCoroutine(humanCollider.PayMoney(30, payParent, humanCollider.moneyListTir,humanCollider.payAreaTir));
        }
       
    }
    void NextTruck()
    {
        Truck.gameObject.SetActive(false);
        GameObject obj = Instantiate(TruckObj, Truck.transform.position, Quaternion.identity);
        Truck = obj.transform;
        for (int i = 0; i < wheelAnim.Length; i++)
        {
            wheelAnim[i] = Truck.GetComponent<TirAnim>().anim[i];
        }
        obj.transform.DOMove(truckStartPos,3f).OnComplete(() => { 
            kasaCollider.enabled = true;
            wheelAnim[0].enabled = false;
            wheelAnim[1].enabled = false;
            wheelAnim[2].enabled = false;
            wheelAnim[3].enabled = false;
           
        });
        carAdet = 0; carAdet = 0;
    }
    //void Update()
    //{
    //    if (go)
    //    {
    //        if (!oneTime)
    //        {

    //            Go();
    //            oneTime = false;
    //        }

    //    }
    //}

    //private void Go()
    //{
    //    //customer = customers.transform.GetChild(0).GetComponent<Customers>();
    //    ////StartCoroutine(Sell(SellCustomer,true));
    //    //customers.transform.GetChild(0).DOLocalMove(new Vector3(0, 0, 6), 4f).OnComplete(() => Destroy(customers.transform.GetChild(0).gameObject));
    //    //customers.transform.GetChild(0).GetComponent<Animator>().SetBool("Walk", true);
    //    go = false;
    //}


    //IEnumerator Sell(Transform SellArea, bool runSell)
    //{

    //    int count = customer.listMoney.Count - 1;
    //    int a = 9;
    //    while (runSell)
    //        {

    //        customer.listMoney[count].gameObject.SetActive(true);
    //        customer.listMoney[count].gameObject.transform.parent = SellArea;
    //        count--;
    //        if (count <= 0)
    //        {
    //            runSell = false;
    //        }
    //        customer.listMoney[count + 1].gameObject.transform.DOLocalJump(Vector3.zero, 3, 0, 1f, false)

    //           .OnComplete(() =>
    //           {
    //               Debug.Log("Count" + a);
    //               Destroy(customer.listMoney[a].gameObject);
    //               gameManager.money += oneMoneyValue;
    //               gameManager.moneyText.text = gameManager.money.ToString();
    //               a--;


    //           });
    //            yield return new WaitForSeconds(0.1f);
    //        }



    //}

}
