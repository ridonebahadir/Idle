using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ProductLine : MonoBehaviour
{
    public int id;
    public Transform[] component;
    public GameObject car;
  
    public int turn;
    
    bool oneTime;
    public bool run;
    public Vector3 pos;
    public bool test;
   
    private void Start()
    {
        id = transform.parent.GetComponent<AllLine>().a;
        transform.parent.GetComponent<AllLine>().a++;
        pos = transform.position;
        for (int i = 0; i < component.Length; i++)
        {
            component[i] = transform.parent.GetComponent<AllLine>().component[i];
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
                   transform.GetChild(turn).gameObject.SetActive(true);
                   transform.parent.GetChild(id + 1).gameObject.SetActive(true);
                    //transform.parent.GetChild(id + 1).GetComponent<ProductLine>().move = true;
                    Destroy(component[turn].transform.GetChild(0).gameObject);
                   if (turn < component.Length) turn++;
                   oneTime = false;
                   run = false;



               });
            }
            else
            {
                transform.DOLocalMove(new Vector3(component[turn].transform.position.x, transform.localPosition.y, component[turn].transform.GetChild(id).localPosition.z), 2f);

            }

            yield return new WaitForSeconds(0.1f);
           
        }
        //yield return new WaitForSeconds(4f);
        //if (turn == 6)
        //{
        //    ParkCar();
        //}
    }

    private void ParkCar()
    {
        transform.DOLocalMove(new Vector3(component[turn].transform.position.x, transform.localPosition.y, component[turn].transform.GetChild(id).localPosition.z), 2f);

    }
}
