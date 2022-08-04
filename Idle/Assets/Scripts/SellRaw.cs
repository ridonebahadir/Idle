using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SellRaw : MonoBehaviour
{
    private BoxCollider boxCollider;
    public TextMeshPro textOrder;
    public int capasity = 5;
    public float animTime = 3;
    public int id;
    private int value;
    public int adet;
    public GameObject raw;
    public Spawn spawn;
    public Transform outPoint;
    public Transform kamyon;
    public Transform kamyonObj;
    public Animator kamyonAnim;
    public Transform[] backdoor;
    public GameManager gameManager;
  
    public TextMeshPro adetText;
    public GameObject upGrade;
    IEnumerator co;
    public Image waitImage;
 
    [Header("UI")]
    public Upgrade[] upgrades;
    
    bool isTimer;
    public GameObject warning;
    
    private void Awake()
    {
        capasity = PlayerPrefs.GetInt("Capasity"+id,5);
        animTime = PlayerPrefs.GetFloat("AnimTime"+id, 3);
    }
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        switch (id)
        {
            case 1:
                value=ValueObj.metal;
                break;
            case 2:
                value = ValueObj.polimer;
                break;
            case 3:
                value = ValueObj.kablo;
                break;
            case 4:
                value = ValueObj.cam;
                break;
           
        }
    }
    private void Update()
    {
       
        adetText.text = outPoint.childCount.ToString();


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Human")
        {
            if (other.GetComponent<HumanCollider>().human)
            {
                upGrade.SetActive(true);
                upgrades[0].Control();
                upgrades[1].Control();
            }
          
           
            isTimer = true;
            co = Timer();
            // start the coroutine
            StartCoroutine(co);
           
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
        {
            upGrade.SetActive(false);
           
            warning.SetActive(false);
            
          
            Azalma();
            
            isTimer = false;
           
            StopCoroutine(co);

            waitImage.DOFillAmount(0, 1);
            //run = false;
        }

    }
  
    float x;
    float y;
    float z;
    bool run;
    IEnumerator SpawnRaw()
    {
        if (outPoint.childCount<capasity)
        {
           
            float a = animTime;

            waitImage.DOFillAmount(0, animTime);
            for (int i = 0; i <= a; i++)
            {
               
               
                textOrder.text = animTime.ToString();
                animTime--;
                yield return new WaitForSeconds(1);
               
               
               
            }
            
            textOrder.text = "Order";
            animTime = PlayerPrefs.GetFloat("AnimTime", 3);
            kamyonObj.gameObject.SetActive(true);
            kamyonObj.transform.DOLocalMove(Vector3.zero, 1f).OnComplete(()=> {
                kamyonAnim.enabled = false;
                backdoor[0].transform.DORotate(new Vector3(-90,0,130),0.2f);
                backdoor[1].transform.DORotate(new Vector3(-90,0,-130),0.2f);
            }).SetEase(Ease.OutExpo);


        }
        else
        {
            warning.SetActive(true);
           
            waitImage.fillAmount = 1;
            waitImage.color = Color.red;
           
        }
        yield return new WaitForSeconds(1f);
        while (run)
        {
            if ((adet < capasity) && (outPoint.childCount < capasity))
            {
                upgrades[0].Control();
                upgrades[1].Control();

                GameObject obj = Instantiate(raw, kamyon);
                obj.transform.parent = outPoint;

                gameManager.money -= value;
                gameManager.moneyText.text = gameManager.money.ToString();


                adet++;
                //adetText.text = outPoint.childCount.ToString();


                obj.transform.DOLocalJump(new Vector3(x, y, z), 8, 0, 1, false).OnComplete(() => { spawn.list.Add(obj); }).SetEase(Ease.InQuint);
                x += 1.4f;
                if (x >= 2.8f)
                {
                    x = 0;
                    z += 1f;
                    if (z >= 3f)
                    {
                        y += 1f;
                        z = 0;
                    }

                }
            }

            else
            {
                waitImage.color = Color.red;
                yield return new WaitForSeconds(0.5f);

               
                kamyonAnim.enabled = true;
                backdoor[0].transform.DORotate(new Vector3(-90, 0, 0), 0.2f);
                backdoor[1].transform.DORotate(new Vector3(-90, 0, 0), 0.2f).OnComplete(()=>
                {

                    kamyonObj.transform.DOLocalMove(new Vector3(20, 0, 0), 1f).OnComplete(() => { kamyonObj.gameObject.SetActive(false); }).SetEase(Ease.InExpo);

                });
                adet = 0;
                run = false;

            }


            yield return new WaitForSeconds(0.2f);
            }
           
        


          
    }
   
    IEnumerator Timer()
    {
        while (isTimer) 
        {
            waitImage.color = Color.green;
            waitImage.DOFillAmount(1, 1);
            yield return new WaitForSeconds(1f);
            run = true;
           
            StartCoroutine(SpawnRaw());
            isTimer = false;
        }
        
    }

    public void Azalma()
    {
        x = 0;
        y = 0;
        z = 0;
        for (int i = 0; i < outPoint.childCount; i++)
        {

            x += 1.4f;
            if (x >= 2.8f)
            {
                x = 0;
                z += 1f;
                if (z >= 3f)
                {
                    y += 1f;
                    z = 0;
                }

            }
        }

    }
}

