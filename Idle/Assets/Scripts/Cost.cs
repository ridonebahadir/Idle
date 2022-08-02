using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Cost : MonoBehaviour
{
    public GameObject moneyBar;
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
    public GameObject[] BotOpen;
    bool run;
    int closeOrOpen;
    public int id;
    float imageValue;
    [Header("For UPGRADE CAR")]
    public bool isUpgrade;
    public int[] costCarUpgrade;
    public Transform[] icons;
    private void Awake()
    {
       
        if (!gameManager.locked)
        {
            closeOrOpen = 1;
        }
        else
        {
            closeOrOpen = PlayerPrefs.GetInt("CloseOrOpen" + id);
            costValue = PlayerPrefs.GetFloat("CostValue" + id, costValue);
            imageValue = PlayerPrefs.GetFloat("ImageValue" + id);
        }
        if (isUpgrade)
        {
            //costValue = costCarUpgrade[gameManager.upgradeCount];
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i].gameObject.SetActive(false);
            }
            icons[gameManager.upgradeCount].gameObject.SetActive(true);
        }
       
    
    }
    private void Start()
    {
        
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i].localPosition = new Vector3(transforms[i].localPosition.x, transforms[i].localPosition.y + 20, transforms[i].localPosition.z);
        }
        imageAmount = 1 / costValue;
        image.fillAmount = imageValue;




        costText.text = costValue.ToString();
        boxCollider = GetComponent<BoxCollider>();
        if (closeOrOpen == 1)
        {
            StartCoroutine(OpenTurn(false));
        }
        if (isUpgrade)
        {
            close.SetActive(true);  
            boxCollider.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.money>0)
        {
            if (other.tag == "Human")
            {
                co = Timer(other.transform);
                run = true;


                isTimer = true;
                pay = true;

                StartCoroutine(co);




            }
        }
        else
        {
            moneyBar.transform.DOShakeRotation(3, 5f);

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
        {
            PlayerPrefs.SetFloat("CostValue" + id, costValue);
          
            PlayerPrefs.SetFloat("ImageValue" + id, image.fillAmount);
           
            isTimer = false;
            StopCoroutine(co);
            run = false;
            pay = false;
        }
    }

    void OpenMachine()
    {
        PlayerPrefs.SetInt("CloseOrOpen" + id,1);
        StartCoroutine(OpenTurn(true));
           
           
           
        
    }

    IEnumerator OpenTurn(bool levelup)
    {
        close.SetActive(false);
        boxCollider.enabled = false;
        //yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < transforms.Length; i++)
        {
            

           

            transforms[i].gameObject.SetActive(true);
            transforms[i].transform.DOLocalMoveY(0, 1f).OnComplete(()=> {
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
            foreach (GameObject item in BotOpen)
            {
                item.SetActive(true);
            }
        }
        yield return new WaitForSeconds(2f);
        if (levelup)
        {
            if (cameraFollow.turn<cameraFollow.transforms.Length-1)
            {
                cameraFollow.Move();
            }
           
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
            isTimer = false;
        }

    }
    bool pay = true;
    //public float katsayi;
    
   
    IEnumerator MoneyGo(Transform human)
    {

       
        
        while (pay)
        {
           
            GameObject obj = Instantiate(money, human);
            obj.transform.parent = transform;
            obj.transform.DOLocalJump(new Vector3(0, -3, 0), 5, 0, 1.5f, false).OnComplete(() => Destroy(obj)).SetEase(Ease.OutQuint);
            Vibration.Vibrate(40);
            image.fillAmount += imageAmount;
            costValue--;             
            costText.text = costValue.ToString("f0");

            gameManager.money--; ;
            gameManager.moneyText.text = gameManager.money.ToString();
            //if (gameManager.money <= 0)
            //{
            //    costValue -= gameManager.money;
            //    costText.text = costValue.ToString();


            //    gameManager.moneyText.text = gameManager.money.ToString();

            //    isTimer = false;
            //    StopCoroutine(co);
            //    run = false;
            //    pay = false;
            //}

            if (costValue <= 0)
            {
                isTimer = false;
                StopCoroutine(co);
               

                isTimer = true;
                if (isUpgrade)
                {
                    gameManager.Upgrade();
                    costValue = costCarUpgrade[gameManager.upgradeCount];
                    costText.text = costCarUpgrade[gameManager.upgradeCount].ToString();
                    image.fillAmount = 0;
                    for (int i = 0; i < icons.Length; i++)
                    {
                        icons[i].gameObject.SetActive(false);
                    }
                    icons[gameManager.upgradeCount].gameObject.SetActive(true);
                }
                else
                {
                    OpenMachine();

                }
                //StartCoroutine(OpenMachine());
                pay = false;
                StartCoroutine(SoundManagerSfx.Play("Unlocked", 0.5f));


            }
            if (gameManager.money <= 0)
            {
                moneyBar.transform.DOShakeRotation(3, 5f);

                PlayerPrefs.SetFloat("CostValue" + id, costValue);

                PlayerPrefs.SetFloat("ImageValue" + id, image.fillAmount);

                isTimer = false;
                StopCoroutine(co);
                run = false;
                pay = false;
            }
            yield return new WaitForSeconds(0.05f);
        }
           

        
    }
}
