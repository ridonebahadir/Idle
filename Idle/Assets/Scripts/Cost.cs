using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Cost : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public GameManager gameManager;
    public float costValue;
   
    public Transform[] transforms;
    public GameObject close;
    public GameObject money;
    public TextMeshPro costText;
    public Image image;
    private float imageAmount;
    public Transform mainCamera;
    private BoxCollider boxCollider;
    IEnumerator co;
    [Header("For BANT")]
    public bool isBant;
    public GameObject productLine;
    bool run;
    private void Start()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i].localPosition = new Vector3(transforms[i].localPosition.x, transforms[i].localPosition.y+20, transforms[i].localPosition.z);
        }
        imageAmount =1 / costValue;
        costText.text = costValue.ToString();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Human")
        {
            co = Timer(other.transform);
            run = true;
           

                isTimer = true;
            pay = true;

                StartCoroutine(co);


            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
        {
            isTimer = false;
            StopCoroutine(co);
            run = false;
            pay = false;
        }
    }

    void OpenMachine()
    {
        Debug.Log("sfsgdsgdf");
        //yield return new WaitForSeconds(2f);
       
            //open.SetActive(true);
            StartCoroutine(OpenTurn());
           
           
           
        
    }

    IEnumerator OpenTurn()
    {
        close.SetActive(false);
        boxCollider.enabled = false;
        //yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < transforms.Length; i++)
        {
            

           

            transforms[i].gameObject.SetActive(true);
            transforms[i].transform.DOLocalMoveY(0, 0.5f).OnComplete(()=> {
                mainCamera.DOShakeRotation(0.1f,0.3f);
                //if (i == transforms.Length - 1)
                //{
                //    transform.gameObject.SetActive(false);
                //}


            }).SetEase(Ease.InExpo);   //KUFU_ANIM
            yield return new WaitForSeconds(0.1f);
           
        }
        if (isBant)
        {
            productLine.SetActive(true);
        }
        yield return new WaitForSeconds(2f);
        cameraFollow.Move();
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
    bool pay = true;
    float katsayi;
   
    IEnumerator MoneyGo(Transform human)
    {
       
        katsayi = costValue / 30;
        
        while (pay)
        {
           
            GameObject obj = Instantiate(money, human);
            obj.transform.parent = transform;
            obj.transform.DOLocalJump(new Vector3(0, 0, 0), 3, 0, 1.5f, false).OnComplete(() => Destroy(obj)).SetEase(Ease.OutQuint);
            Vibration.Vibrate(40);
            image.fillAmount += imageAmount;
            costValue -= katsayi;
            gameManager.money-=(int)katsayi;
            
            costText.text = costValue.ToString();
            gameManager.moneyText.text = gameManager.money.ToString();
            if (gameManager.money <= 0)
            {
                costValue -= gameManager.money;
                costText.text = costValue.ToString();

               
                gameManager.moneyText.text = gameManager.money.ToString();
                
                isTimer = false;
                StopCoroutine(co);
                run = false;
                pay = false;
            }
            if (costValue <= katsayi)
            {
                isTimer = false;
                StopCoroutine(co);
                Debug.Log("PAY");

                    isTimer = true;

                 OpenMachine();
                    //StartCoroutine(OpenMachine());
                pay = false;



            }
            yield return new WaitForSeconds(0.1f);
        }
           

        
    }
}
