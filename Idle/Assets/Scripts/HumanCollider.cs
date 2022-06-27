using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HumanCollider : MonoBehaviour
{
    public GameObject paralar;
    public Vector3 startPos;
    public CameraFollow cameraFollow;
    public SellRaw sellRaw;
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

    [Header("MACH?NE OUT PUT")]
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

    [Header("MACH?NE OUT PUT")]
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
    private void Awake()
    {
        startPos.x = PlayerPrefs.GetFloat("HumanStartPosX",transform.position.x);
        startPos.y = PlayerPrefs.GetFloat("HumanStartPosY", transform.position.y);
        startPos.z = PlayerPrefs.GetFloat("HumanStartPosZ", transform.position.z);
    }
    private void Start()
    {
        transform.position = startPos;
    }
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
            //if (spawn.currentList2.Count > 0)
            //{
            //    StartCoroutine(GoObj(spawn.currentList2, 1, false));
            //}
           
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
                spawn = other.transform.GetComponent<Spawn>();
                if (spawn.currentList.Count > 0)
                {
                    StartCoroutine(GoObjKargo(spawn.currentList, 0, false));
                }
               


            }
           


        }
        if (other.tag=="Pay")
        {
            if (moneyList.Count>0)
            {
                StartCoroutine(ComeMoney());
            }
            
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
        if (other.tag=="BankTake")
        {
            StartCoroutine(BankTakeMoney(other.transform));
           
            
        }
     
    }
    int bagAdet;
    private void OnTriggerExit(Collider other)
    {
       
        if (other.tag == "RawMaterial")
        {
            bag.GetChild(0).transform.GetComponent<HingeJoint>().connectedBody = transform.GetChild(0).GetComponent<Rigidbody>();
            bag.GetChild(0).transform.GetComponent<Rigidbody>().isKinematic = false;


            for (int bagAdet = 1; bagAdet < bag.childCount; bagAdet++)
            {
                bag.GetChild(bagAdet).transform.GetComponent<HingeJoint>().connectedBody = bag.GetChild(bagAdet - 1).GetComponent<Rigidbody>();
                bag.GetChild(bagAdet).transform.GetComponent<Rigidbody>().isKinematic = false;
                if (bagAdet == bag.childCount-1)
                {
                    bagAdet = bag.childCount - 1;
                }
            }
            run = false;
            sellRaw = other.gameObject.transform.parent.parent.GetChild(1).GetChild(0).GetComponent<SellRaw>();
            sellRaw.Azalma();

        }

        if (other.tag == "Put")
        {
            run = false;
           
        }

        if (other.tag == "OutPut")
        {
            run = false;
            spawn.Azalma(true);
           

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
            spawn.Azalma(false);


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
                bagYAxis -= 0.40f;
               
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
    IEnumerator GoObjKargo(List<GameObject> list, int id, bool grid)
    {
        while (run)
        {
            if (list.Count > 0)
            {
               
                    spawn.GoObjKargo(list, id, grid);
                   
                
              

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
      
        Debug.Log("COunt = " + count);
        if (count > 0)
        {
            while (runSell)
            {
                bagList[count].transform.parent = SellArea;
                bagList[count].transform.DOLocalJump(Vector3.zero, 3, 0, 0.5f, false)
               .OnComplete(() => {
                  
                   
                   bagList.RemoveAt(count);

                   count--;
                   payMoney += money;
                 
                   if (count < 0) runSell = false;

               }).SetEase(Ease.InQuint);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2f);
            bagYAxis = 0f;
            //StartCoroutine(PayMoney(SellArea));
        }
    }
    int x;
    int y;
    int z;
    public IEnumerator PayMoney(int countMoney,Transform sellArea)
    {
        Debug.Log("countMoney ="+countMoney);
        for (int i = 0; i < countMoney; i++)
        {
            GameObject obj = Instantiate(money,sellArea);
            obj.transform.parent = payArea;
            moneyList.Add(obj);
            obj.transform.DOLocalJump(new Vector3(x*0.5f, y*0.25f, z*0.5f), 3, 0, 0.5f, false);
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
        float a = 1 / moneyList.Count;
        for (int i = moneyList.Count-1; i > -1; i--)
        {
            GameObject obj = moneyList[i];
            obj.transform.parent = bag.parent;
            obj.transform.DOLocalMove(Vector3.zero, 0.5f, false)
                .OnComplete(()=> { 
                    obj.gameObject.SetActive(false);
                    gameManager.money+=2;
                    gameManager.moneyText.text = gameManager.money.ToString();
                    Destroy(obj.gameObject);
                   

                });
           
            yield return new WaitForSeconds(a);
        }

        yield return new WaitForSeconds(0.1f);
        moneyList.Clear();
        x = 0;y = 0;z = 0;

    }
    IEnumerator BankTakeMoney(Transform other)
    {
        other.GetComponent<BoxCollider>().enabled = false;
        int a = other.childCount;
        int turn = 0;
        for (int i = 0; i < a; i++)
        {
            GameObject obj = other.GetChild(turn).gameObject;
            obj.transform.parent = transform;
            obj.transform.DOScale(new Vector3(0.3f,0.3f,0.3f), 0.4f).SetEase(Ease.InOutSine);
            obj.transform.DOLocalMove(Vector3.zero, 0.4f, false).OnComplete(()=>Destroy(obj.gameObject)).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(0.01f);

        }
        yield return new WaitForSeconds(0.1f);
        paralar.SetActive(false);
        gameManager.money = 20000;
        gameManager.moneyText.text = gameManager.money.ToString();
        yield return new WaitForSeconds(0.5f);
        cameraFollow.Move();
        PlayerPrefs.SetInt("Money", gameManager.money);
    }
}
