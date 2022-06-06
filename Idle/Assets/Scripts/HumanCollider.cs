using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HumanCollider : MonoBehaviour
{
    public int capasity;
    public AllLine allLine;
    private Spawn spawn;
    public GameManager gameManager;
    public Transform bag;
    public float bagYAxis;

    [Header("RAW MATERIAL LIST")]
    public List<GameObject> bagListMetal;
    public List<GameObject> bagListPolimer;
    public List<GameObject> bagListCam ;
    public List<GameObject> bagListKablo;

    [Header("MACHÝNE OUT PUT")]
    public List<GameObject> bagListSase;
    public List<GameObject> bagListWheel;
    public List<GameObject> bagListKoltuk;
    public List<GameObject> bagListEngine;
    public List<GameObject> bagListBody;
    public List<GameObject> bagListPencere;

   
    [Header("RAW MATERIAL")]
    public GameObject metal;
    public GameObject polimer;
    public GameObject cam;
    public GameObject kablo;

    [Header("MACHÝNE OUT PUT")]
    public GameObject sase;
    public GameObject wheel;
    public GameObject koltuk;
    public GameObject engine;
    public GameObject body;
    public GameObject pencere;

    [Header("MONEY")]
    public List<GameObject> moneyList;
    public int payMoney;
    public GameObject money;
    public Transform payArea;
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag== "RawMaterial")
        {
           
                spawn = other.transform.GetComponent<Spawn>();
                run = true;
                StartCoroutine(ComeObj(true));
            
           
          
        }
       
        if (other.tag=="Put")
        {
            run = true;
            spawn = other.transform.GetComponent<Spawn>();
            if (spawn.currentList.Count>0)
            {
                StartCoroutine(GoObj(spawn.currentList, 0, false));
            }
            if (spawn.currentList2.Count > 0)
            {
                StartCoroutine(GoObj(spawn.currentList2, 1, false));
            }
           
            spawn.runMachine = true;
            StartCoroutine(spawn.OutPut(2,1));
        }
       
        if (other.tag=="OutPut")
        {
            spawn = other.transform.parent.GetChild(1).GetComponent<Spawn>();
            run = true;
            
            StartCoroutine(ComeObj(false));
        }
       
        if (other.tag=="Sell")
        {
            if (bag.childCount>0)
            {
                run = true;
                StartCoroutine(Sell(other.transform.parent.GetChild(1).transform, bagListMetal,1,true));
                StartCoroutine(Sell(other.transform.parent.GetChild(1).transform, bagListPolimer,10,true));
                StartCoroutine(Sell(other.transform.parent.GetChild(1).transform, bagListCam,1,true));
                StartCoroutine(Sell(other.transform.parent.GetChild(1).transform, bagListKablo,1,true));

                StartCoroutine(Sell(other.transform.parent.GetChild(1).transform, bagListSase,4,true));
                StartCoroutine(Sell(other.transform.parent.GetChild(1).transform, bagListWheel,5,true));
               
                
            }
           


        }
        if (other.tag=="Pay")
        {
            StartCoroutine(ComeMoney());
        }
        if (other.tag=="Component")
        {
            run = true;
            spawn = other.transform.GetComponent<Spawn>();
            StartCoroutine(GoObj(spawn.currentList, 0,true));
            
        }
        if (other.tag == "Kasa")
        {
            allLine.transform.GetChild(allLine.currentCarNumber).transform.GetComponent<ProductLine>().SellCar();

        }
        if (other.tag=="Door")
        {
            other.transform.GetChild(0).transform.DOScaleY(0,1f).SetEase(Ease.InOutExpo); ;
        }
     
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.tag == "RawMaterial")
        {
            run = false;
           

        }

        if (other.tag == "Put")
        {
            run = false;
           
        }

        if (other.tag == "OutPut")
        {
            run = false;
           

        }

        if (other.tag == "Sell")
        {


            run = false;
           


        }
        if (other.tag == "Pay")
        {
            run = false;
           

        }
        if (other.tag == "Component")
        {
            run = false;
           


        }
        if (other.tag == "Door")
        {
            other.transform.GetChild(0).transform.DOScaleY(1, 1f).SetEase(Ease.InExpo);
        }

    }
  
    public bool run;
   
    IEnumerator ComeObj(bool add)
    {
        while (run)
        {
            if (bag.childCount < capasity)
            {
                spawn.ComeObj(bag, bagYAxis, add);

            }
            else
            {
                run = false;
            }


            yield return new WaitForSeconds(0.15f);

        }
    }

    IEnumerator GoObj(List<GameObject> list,int id,bool grid)
    {
        while (run)
        {
            if (list.Count>0)
            {
                spawn.GoObj(list,id,grid);
                bagYAxis -= 0.25f;
               
            }
            else
            {
                run = false;
            }
            //else
            //{
            //    StartCoroutine(FixedHeight());
            //}

            yield return new WaitForSeconds(0.1f);

            //StartCoroutine(FixedHeight());


        }
            
        

    }

    IEnumerator FixedHeight()
    {
        for (int i = 0; i < bag.transform.childCount; i++)
        {
            bag.transform.GetChild(i).transform.DOLocalMove(new Vector3(0, i * 0.25f, 0),1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
   
    IEnumerator Sell(Transform SellArea,List<GameObject> bagList,int money,bool runSell)
    {
        int count = bagList.Count - 1;

        if (count > 0)
        {
            while (runSell)
            {

                bagList[count].transform.DOJump(SellArea.transform.position, 3, 0, 0.1f, false)
               .OnComplete(() => {
                   bagYAxis -= 0.25f;
                   bagList[count].transform.parent = SellArea;
                   bagList.RemoveAt(count);
                   count--;
                   payMoney += money;
                 
                   if (count < 0) runSell = false;

               }).SetEase(Ease.InQuint);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2f);
            StartCoroutine(PayMoney(SellArea));
        }
    }
    int x;
    int y;
    int z;
    IEnumerator PayMoney(Transform sellArea)
    {
        for (int i = 1; i < payMoney; i++)
        {
            GameObject obj = Instantiate(money, sellArea);
            obj.transform.parent = payArea;
            moneyList.Add(obj);
            obj.transform.DOLocalJump(new Vector3(x*0.5f, y*0.25f, z*0.5f), 3, 0, 0.3f, false);
            x++;
            if (x == 4)
            {
                x = 0;
                z++;
                if (z==4)
                {
                    y++;
                    z = 0;
                }
               
            }

            yield return new WaitForSeconds(0.1f);






        }
       

    }

    IEnumerator ComeMoney()
    {
        for (int i = moneyList.Count-1; i > -1; i--)
        {
            GameObject obj = moneyList[i];
            obj.transform.parent = bag;
            obj.transform.DOLocalJump(new Vector3(0,0,-0.5f), 3, 0, 0.3f, false)
                .OnComplete(()=> { 
                    obj.gameObject.SetActive(false);
                    gameManager.money++;
                    gameManager.moneyText.text = gameManager.money.ToString();
                });
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);
        x = 0;y = 0;z = 0;

    }
}
