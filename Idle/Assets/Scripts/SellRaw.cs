using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SellRaw : MonoBehaviour
{
    public int capasity = 5;
    public float animTime = 3;
    public int value;
    public int adet;
    public GameObject raw;
    public Spawn spawn;
    public Transform outPoint;
    public Transform kamyon;
    public Transform kamyonObj;
    
    public GameManager gameManager;
  
    public TextMeshPro adetText;
    public GameObject upGrade;
    IEnumerator co;

    [Header("UI")]
    public Upgrade[] upgrades;
    
    bool isTimer;
    private void Update()
    {
       
        adetText.text = outPoint.childCount.ToString();


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Human")
        {
            upGrade.SetActive(true);
            upgrades[0].Control();
            upgrades[1].Control();
           
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
            Azalma();
            upGrade.SetActive(false);
            isTimer = false;
           
            StopCoroutine(co);
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
            kamyonObj.gameObject.SetActive(true);
            kamyonObj.transform.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.OutQuint);
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


                obj.transform.DOLocalJump(new Vector3(x, y, z), 3, 0, animTime, false).OnComplete(() => { spawn.list.Add(obj); }).SetEase(Ease.InQuint);
                x += 0.7f;
                if (x == 2.8f)
                {
                    x = 0;
                    z += 0.5f;
                    if (z == 2)
                    {
                        y += 0.5f;
                        z = 0;
                    }

                }
            }

            else
            {
               
                yield return new WaitForSeconds(0.5f);
              
              
                kamyonObj.transform.DOLocalMove(new Vector3(20,0,0), 1f).OnComplete(()=>kamyonObj.gameObject.SetActive(false)).SetEase(Ease.InQuint);
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
           
            x += 0.7f;
            if (x == 2.8f)
            {
                x = 0;
                z += 0.5f;
                if (z == 2)
                {
                    y += 0.5f;
                    z = 0;
                }

            }
        }

    }
}

