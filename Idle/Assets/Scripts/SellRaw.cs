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
    public GameManager gameManager;
    public BoxCollider paletCollider;
    public TextMeshPro adetText;
    public GameObject upGrade;
    IEnumerator co;

    [Header("UI")]
    public Upgrade[] upgrades;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Human")
        {
            upGrade.SetActive(true);
            upgrades[0].Control();
            upgrades[1].Control();
            run = true;
            co = SpawnRaw(); // create an IEnumerator object
            StartCoroutine(co); // start the coroutine

           
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
        {
            upGrade.SetActive(false);
            //StopCoroutine(co);
            //run = false;
        }

    }
    //private void OnMouseDown()
    //{
    //    paletCollider.enabled = false;
    //    run = true;
    //    StartCoroutine(SpawnRaw());



    //}
    float x;
    float y;
    float z;
    bool run;
    IEnumerator SpawnRaw()
    {
        yield return new WaitForSeconds(1f);
        while (run)
        {
            paletCollider.enabled = false;
            if (adet<capasity)
            {
                upgrades[0].Control();
                upgrades[1].Control();
                paletCollider.enabled = false;
                GameObject obj = Instantiate(raw, kamyon);
                obj.transform.parent = outPoint;

                gameManager.money -= value;
                gameManager.moneyText.text = gameManager.money.ToString();
                spawn.list.Add(obj);

                adet++;
                //adetText.text = outPoint.childCount.ToString();


                obj.transform.DOLocalJump(new Vector3(x, y, z), 3, 0, animTime, false).OnComplete(() => paletCollider.enabled = true).SetEase(Ease.InQuint);
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
               
                //yield return new WaitForSeconds(4f);
                paletCollider.enabled = true;
                adet = 0;
                run = false;
            }

            yield return new WaitForSeconds(0.2f);
        }


          
    }
    private void Update()
    {
       
            adetText.text = outPoint.childCount.ToString();
      
       
    }
    
}

