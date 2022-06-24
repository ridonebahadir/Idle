using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ProductLine : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider boxCollider;
    public int id;
    public Transform[] component;
    public Spawn[] spawn;
    public GameObject car;
    public AllLine allLine;
   
    public int turn;
    
    bool oneTime;
    public bool run;
    public Vector3 pos;
    public bool test;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        allLine = transform.parent.GetComponent<AllLine>();
        id = transform.parent.GetComponent<AllLine>().a;
        transform.parent.GetComponent<AllLine>().a++;
        pos = transform.position;
        for (int i = 0; i < component.Length; i++)
        {
            component[i] = allLine.component[i];
            //spawn[i] = allLine.spawn[i];
        }
        for (int i = 0; i < spawn.Length; i++)
        {
            spawn[i] = allLine.spawn[i];
        }

    }
    private void Update()
    {

       
            if (component[turn].childCount > 0)
            {
                
                for (int i = id; i < transform.parent.childCount-1; i++)
                {
                    transform.parent.GetChild(i + 1).GetComponent<ProductLine>().enabled = true;
                }

                if (!oneTime)
                {


                    run = true;
                    GoCar();


                    oneTime = true;

                }



            }
            else
            {
                for (int i = id; i < transform.parent.childCount - 1; i++)
                {
                    transform.parent.GetChild(i + 1).GetComponent<ProductLine>().enabled = false;
                }

            }


      




    }
    public void GoCar()
    {
       
        StartCoroutine(CarMove());
    }

    IEnumerator CarMove()
    {
        if (run)
        {
            if (turn<6)
            {
               
                transform.DOLocalMove(new Vector3(component[turn].transform.position.x, transform.localPosition.y, transform.localPosition.z), 2f)
               .OnComplete(() => {
                  
                   if (turn == 0)
                   {
                       GameObject obj = Instantiate(car, pos, Quaternion.Euler(0, -90, 0), transform.parent);
                       obj.SetActive(false);
                       obj.name = id.ToString();
                   }
                   //spawn[turn].Azalma(false);
                   transform.GetChild(turn).gameObject.SetActive(true);
                   transform.parent.GetChild(id + 1).gameObject.SetActive(true);
                   //transform.parent.GetChild(id + 1).GetComponent<ProductLine>().move = true;
                   


                   Destroy(component[turn].transform.GetChild(component[turn].childCount-1).gameObject);
                  
                   turn++;
                   oneTime = false;
                   run = false;



               });
            }
            else
            {
                transform.DOLocalMove(new Vector3(component[turn].transform.position.x, transform.localPosition.y, transform.parent.GetComponent<AllLine>().showRoomZAxis), 2f)
                    .OnComplete(()=> {

                        allLine.showRoomZAxis += 4;
                        boxCollider.enabled = true;
                        complateCar = true; });

            }

            yield return new WaitForSeconds(0.1f);
           
        }
        //yield return new WaitForSeconds(4f);
        //if (turn == 6)
        //{
        //    ParkCar();
        //}
    }
    bool complateCar;
    public void SellCar()
    {
        if (complateCar)
        {
            allLine.oneTime = false;
            allLine.go = true;
           
            transform.DOLocalMove(new Vector3(transform.parent.GetComponent<AllLine>().showRoomXAxis, transform.localPosition.y, transform.localPosition.z), 4f).OnComplete(()=>GoSellCar());
            allLine.currentCarNumber++;
            
            if (transform.parent.GetChild(id + 1).GetComponent<ProductLine>().turn==6)
            {
                transform.parent.GetChild(id + 1).transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), 4f);

            }
            allLine.showRoomXAxis += 10;
            allLine.showRoomZAxis -= 4;
        }
       



    }
    Vector3 endPos;
    void GoSellCar()
    {

        
        transform.GetChild(6).gameObject.SetActive(false);
        rb.useGravity = true;
        allLine.carAdet++;
        Invoke("Late", 1f);
       
       
        //transform.DOLocalMove(new Vector3(transform.localPosition.x-30, 0, transform.localPosition.z), 6f);
        //allLine.showRoomXAxis += 10;
    }
    void Late()
    {
        endPos = transform.position;
        allLine.GoTruck(endPos);
        transform.gameObject.SetActive(false);
    }
}
