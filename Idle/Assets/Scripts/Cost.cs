using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Cost : MonoBehaviour
{
    public GameManager gameManager;
    public int costValue;
    public GameObject open;
    public Transform[] transforms;
    public GameObject close;
    public GameObject money;
    public TextMeshPro costText;
    IEnumerator co;
    bool run;
    private void Start()
    {
        costText.text = costValue.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Human")
        {
            co = Timer(other.transform);
            run = true;
            if (gameManager.money>costValue)
            {
               
                isTimer = true;
                
               
                StartCoroutine(co);
                

            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
        {
            isTimer = false;
            StopCoroutine(co);
            run = false;
        }
    }

    IEnumerator OpenMachine()
    {
        
        yield return new WaitForSeconds(2f);
        if (run)
        {
            //open.SetActive(true);
            StartCoroutine(OpenTurn());
           
            gameManager.money -= costValue;
            gameManager.moneyText.text = gameManager.money.ToString();
           
        }
    }

    IEnumerator OpenTurn()
    {
        //yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < transforms.Length; i++)
        {
            if (i == transforms.Length - 1)
            {
                close.SetActive(false);
            }
           
            transforms[i].gameObject.SetActive(true);
            transforms[i].transform.DOPunchScale(new Vector3(.2f, 0.2f, .2f), 0.1f);   //KUFU_ANIM

            yield return new WaitForSeconds(0.3f);
        }
    }
    bool isTimer;
    IEnumerator Timer(Transform human)
    {
        while (isTimer)
        {
            yield return new WaitForSeconds(2f);
            run = true;

           
            StartCoroutine(MoneyGo(human));
        }

    }
    IEnumerator MoneyGo(Transform human)
    {
        for (int i = 0; i < costValue; i++)
        {
            GameObject obj= Instantiate(money,human);
            obj.transform.parent = transform;
            obj.transform.DOLocalJump(new Vector3(0, 0, 0), 3, 0, 1.5f, false).OnComplete(() => Destroy(obj)).SetEase(Ease.OutQuint);
            yield return new WaitForSeconds(0.1f);
            if (i== costValue-1)
            {
                isTimer = false;
                StopCoroutine(co);
                StartCoroutine(OpenMachine());
            }

        }
    }
}
