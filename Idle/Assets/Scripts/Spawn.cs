using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Spawn : MonoBehaviour
{

    public GameManager gameManager;
    public bool productLine;
    public GameObject[] prefab;
    public float gridX = 0.3f;
    public float gridY = 0.3f;
    public float spacingx = 0.1f;
    public float spacingy = 0.1f;
    public List<GameObject> list = new List<GameObject>();
    public HumanCollider humanCollider;
    public bool start;
    public bool isMachine;
    //public GameObject warning;

    [Space(50)]
    [Header("SHAKE")]
    public Transform machine;
    private float shakePower = 0.8f;
    private float shakeTimer = 1f;


    [Space(50)]
    [Header("OUTPUT")]
    public bool isOutPut;
    public Transform outPoint;
    public Transform breakPoint;
    public int[] productCount;
    public int outPutCount;
    public bool runMachine;
    public TextMeshPro[] textMesh;
    public int capasity;
    public BoxCollider botCollider;
    public GameObject warningOutPut;
    public GameObject capasityMachine;



    [Space(50)]
    [Header("COMPONENT ARALIK")]
    public int xComponent;
    public int zComponent;
    public int yComponent;
    public int enComponent;
    public int boyComponent;


    [Space(50)]
    [Header("OUTPUT ARALIK")]
    public int xOutPut;
    public int zOutPut;
    public int yOutPut;
    public int enOutPut;
    public int boyOutPut;

    [Space(50)]
    [Header("KARGO ARAC")]
    public Transform kargoarac;
    public Animator wheel1;
    public Animator wheel2;
    //public int borderCount;
    //int borderStartCount;
    Vector3 startPosKargoarac;
    Vector3 startRotateBackDoor;
    Vector3 startRotateBackDoor2;
    BoxCollider boxColliderSell;
    public Transform backDoor;
    public TextMeshPro borderCountText;
    public Transform logo;
    int price;
    public CameraFollow cameraFollow;
    //[Header("PRODUCT L?NE")]
    //public bool runLine;

    void Start()
    {
        if (isMachine)
        {
            warningOutPut.gameObject.SetActive(false);
            capasityMachine.gameObject.SetActive(false);
        }
        if (isMachine)
        {
            popUp.gameObject.SetActive(false);
        }
        if (kargoarac!=null)
        {
            //borderStartCount = borderCount;
            //borderCountText.text = borderCount.ToString();
            boxColliderSell = GetComponent<BoxCollider>();
            startPosKargoarac = kargoarac.transform.localPosition;
            startRotateBackDoor = backDoor.GetChild(0).transform.localRotation.eulerAngles; 
            startRotateBackDoor2 = backDoor.GetChild(1).transform.localRotation.eulerAngles; 
        }
        //WhichList();
        //if (start)
        //{
        //    for (int y = 0; y < gridY; y++)
        //    {
        //        for (int x = 0; x < gridX; x++)
        //        {
        //            GameObject obj = Instantiate(prefab);
        //            obj.transform.parent = outPoint;
        //            Vector3 pos = new Vector3(x * spacingx, 0, y * spacingy);
        //            obj.transform.localPosition = pos;
        //            list.Add(obj);

        //        }
        //    }
        //}
        
    }
     int x;
     public int y;
     int z;
   
   
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag=="Human")
    //    {
    //        humanCollider = other.GetComponent<HumanCollider>();
    //    }
    //}
    private void Update()
    {
        if (productLine)
        {
            if (breakPoint.childCount<=0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
           productCount[0] = breakPoint.childCount;
            textMesh[0].text = productCount[0].ToString();
        }
       
    }


    public void ComeObj(Transform bag,float bagYaxis,bool add)
    {

        if (list.Count>0)
        {
            int a = list.Count - 1;
            if (add)
            {
                currentList.Add(list[a]);
                
            }
            else
            {
               
               transform.parent.GetChild(2).GetComponent<Spawn>().currentList.Add(list[a]);
               
            }
           
            list[a].transform.parent = bag;

            list[a].transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
            list[a].transform.DOLocalJump(new Vector3(0, bagYaxis, 0), 3, 0, 0.4f, false).SetEase(Ease.OutQuint);
            list.RemoveAt(a);
            humanCollider.bagYAxis += 0.40f;
            //Azalma(true);
            if (humanCollider.human)
            {
                StartCoroutine(SoundManagerSfx.Play("Alma", 0));
                Vibration.Vibrate(10);

            }
           
        }
        else
        {
            x = 0; y = 0; z = 0;
            humanCollider.run = false;
        }
      

    }
   
    public void GoObj(List<GameObject> bagList, int id, bool grid,HumanCollider humanCollider)
    {
        
        int a = bagList.Count - 1;
        GameObject obj = bagList[a];
        obj.transform.parent = breakPoint;
        obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
       
        
       
        if (grid)
        {
            obj.transform.DOLocalJump(new Vector3(x, y, z), 3, 0, 0.3f, false).SetEase(Ease.InQuint);
            x+=xComponent;
            if (x == xComponent*enComponent)
            {
                x = 0;
                z+=zComponent;
                if (z == zComponent*boyComponent)
                {
                    y+=yComponent;
                    z = 0;
                }
                

            }
        }
        else
        {
           
            obj.transform.DOLocalJump(new Vector3(0, 0, 0), 3, 1, 0.3f, false)
           .OnComplete(() => { if (!grid) obj.transform.gameObject.SetActive(false); }).SetEase(Ease.InQuint);
        }

        bagList.RemoveAt(a);
        
        productCount[id]++;
        textMesh[id].text = productCount[id].ToString();
        if (humanCollider.human)
        {
            StartCoroutine(SoundManagerSfx.Play("Verme", 0));
            Vibration.Vibrate(40);
        }
       
        
       
    }
    public void GoObjKargo(List<GameObject> bagList, int id, bool grid)
    {
        //if (borderCount>0)
        //{
            int a = bagList.Count - 1;
            GameObject obj = bagList[a];
            obj.transform.parent = breakPoint;
            obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
            




            obj.transform.DOLocalJump(new Vector3(0, 0, 0), 3, 1, 0.3f, false)
           .OnComplete(() => { if (!grid) obj.transform.gameObject.SetActive(false); /*currentList.Add(obj);*/ }).SetEase(Ease.InQuint);

           
            bagList.RemoveAt(a);
            //borderCount--;
            //borderCountText.text = borderCount.ToString();
            humanCollider.bagYAxis -= 0.40f;

            Vibration.Vibrate(40);
        if (a==0)
        {
            StartCoroutine(KargoAracMove(currentList));
        }
        //}
        //else
        //{
        //    humanCollider.run = false;
        //    StartCoroutine(KargoAracMove());
        //}

    }
    public int first;
    public IEnumerator KargoAracMove(List <GameObject> list)
    {
        first = PlayerPrefs.GetInt("First");

        StartCoroutine(humanCollider.PayMoney(list.Capacity * price, transform,humanCollider.moneyListKargo,humanCollider.payArea));
        if (first==0)
        {
            cameraFollow.Move();
            PlayerPrefs.SetInt("First", 1);
        }
        logo.transform.parent.gameObject.SetActive(false);
        boxColliderSell.enabled = false;
        yield return new WaitForSeconds(1f);
        foreach (Transform item in backDoor)
        {
            item.DOLocalRotate(Vector3.zero, 0.5f);
        }
        list.Clear();
        
        yield return new WaitForSeconds(1f);
        wheel1.enabled = true;
        wheel2.enabled = true;
        kargoarac.DOLocalMoveZ(startPosKargoarac.z + 20, 1f).OnComplete(()=> 
        {
           
            kargoarac.DOLocalMoveZ(startPosKargoarac.z, 5f).OnComplete(()=> 
            {
                wheel1.enabled = false;
                wheel2.enabled = false;
                backDoor.GetChild(0).DOLocalRotate(startRotateBackDoor, 0.5f);
                backDoor.GetChild(1).DOLocalRotate(startRotateBackDoor2, 0.5f);
                
                int a = System.Enum.GetValues(typeof(ListSelect)).Length;
                whichList = (ListSelect)Random.Range(a-2, a);
                WhichList();
                boxColliderSell.enabled = true;
                //borderCount = Random.Range(1, 5);
                //borderStartCount = borderCount;
                //borderCountText.text = borderCount.ToString();
                logo.transform.parent.gameObject.SetActive(true);
               
;            }).SetEase(Ease.OutExpo);
        }).SetEase(Ease.InExpo);
        
       
    }
    
    public IEnumerator OutPut(int a,int b)
    {
        
        warningOutPut.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
       
        while (runMachine)
        {
          
            if (productCount[0]<=1)
            {
                botCollider.enabled = true;
            }
           

            if (((productCount[0] -= a) >= 0))
            {
                machine.DOShakeRotation(shakeTimer, shakePower, fadeOut: true);
                textMesh[0].text = productCount[0].ToString();
                //textMesh[1].text = productCount[1].ToString();


                outPutCount++;
            }

            if (productCount[0] < a)
            {

                runMachine = false;
                //productCount[0] = 0;
            }

            //if (productCount[1] <= 0)
            //{

            //    runMachine = false;
            //    productCount[1] = 0;
            //}
            yield return new WaitForSeconds(0.1f);
        }


        yield return new WaitForSeconds(1f);
        for (int i = 0; i < outPutCount; i++)
        {
            if (outPoint.childCount<capasity)
            {
                //warningOutPut.gameObject.SetActive(false);
                GameObject obj = Instantiate(prefab[gameManager.upgradeCount], breakPoint);

                obj.transform.parent = outPoint;

                obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
                obj.transform.DOLocalJump(new Vector3(x * 0.5f, y * 0.5f, z * 0.5f), 3, 3, 0.3f, false).SetEase(Ease.InQuint);

                x += xOutPut;
                if (x == xOutPut * enOutPut)
                {
                    x = 0;
                    z += zOutPut;
                    if (z == zOutPut * boyOutPut)
                    {
                        y += yOutPut;
                        z = 0;
                    }

                }


                list.Add(obj);
                textMesh[0].text = productCount[0].ToString();
                textMesh[1].text = productCount[1].ToString();
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                warningOutPut.gameObject.SetActive(true);
                productCount[0]+=a;
                textMesh[0].text = productCount[0].ToString();
            }
           

        }
        outPutCount = 0;



    }

  
    public void Azalma(bool isOut)
    {
        x = 0;
        y = 0;
        z = 0;
        if (isOut)
        {
            for (int i = 0; i < outPoint.childCount; i++)
            {
                x += xOutPut;
                if (x == xOutPut * enOutPut)
                {
                    x = 0;
                    z += zOutPut;
                    if (z == zOutPut * boyOutPut)
                    {
                        y += yOutPut;
                        z = 0;
                    }

                }
            }
        }
        else
        {
            for (int i = 0; i < outPoint.childCount; i++)
            {
                x += xComponent;
                if (x == xComponent * enComponent)
                {
                    x = 0;
                    z += zComponent;
                    if (z == zComponent * boyComponent)
                    {
                        y += yComponent;
                        z = 0;
                    }


                }
            }
        }
       

    }
    public enum ListSelect {
        metal,polimer,cam,kablo,
        sase,wheel,koltuk,engine, body, pencere,
        saseOut, wheelOut, koltukOut, engineOut, bodyOut, pencereOut,
        saseComponent, wheelComponenet,koltukComponent, engineComponent, bodyComponent, pencereComponent,
        raf,
        Recycle,
        kargoWheel,kargoKoltuk //YENI LISTE EKLERKEN SONA EKLEME
        };

    [Header("SELECT")]
    public ListSelect whichList;
    public List<GameObject> currentList;
    public List<GameObject> currentList2;

    public Transform popUp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Human")
        {
            if (!isOutPut)
            {
                humanCollider = other.GetComponent<HumanCollider>();
            }
            else
            {
                capasityMachine.gameObject.SetActive(true);
            }
           
            if (isMachine&& humanCollider.human)
            {
               
                popUp.gameObject.SetActive(true);
            }
            
            
            WhichList();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isMachine)
        {
            if (other.tag == "Human")
            {
                popUp.gameObject.SetActive(false);
            }
        }
        if (isOutPut)
        {
            if (other.tag == "Human")
            {
                capasityMachine.gameObject.SetActive(false);
              
            }
        }
        
    }
    public void WhichList()
    {
        switch (whichList)
        {
            //HAMMADDE
            case ListSelect.metal:
                currentList = humanCollider.bagListMetal;
                break;
            case ListSelect.polimer:
                currentList = humanCollider.bagListPolimer;
                break;
            case ListSelect.cam:
                currentList = humanCollider.bagListCam;
                break;
            case ListSelect.kablo:
                currentList = humanCollider.bagListKablo;
                break;


            //MAKINA
            case ListSelect.sase:
                currentList = humanCollider.bagListMetal;
                //currentList2 = humanCollider.bagListKablo;
                break;
            case ListSelect.wheel:
                currentList = humanCollider.bagListKablo;
                //currentList2 = humanCollider.bagListPolimer;
                break;
            case ListSelect.koltuk:
                currentList = humanCollider.bagListPolimer;
                //currentList2 = humanCollider.bagListMetal;
                break;
            case ListSelect.engine:
                currentList = humanCollider.bagListMetal;
                //currentList2 = humanCollider.bagListKablo;
                break;
            case ListSelect.body:
                currentList = humanCollider.bagListKablo;
                //currentList2 = humanCollider.bagListKablo;
                break;
            case ListSelect.pencere:
                currentList = humanCollider.bagListCam;
                //currentList = humanCollider.bagListPolimer;
                break;

            //OUT
            case ListSelect.saseOut:
                currentList = humanCollider.bagListSase;
                break;
            case ListSelect.wheelOut:
                currentList = humanCollider.bagListWheel;
                break;
            case ListSelect.koltukOut:
                currentList = humanCollider.bagListKoltuk;
                break;
            case ListSelect.engineOut:
                currentList = humanCollider.bagListEngine;
                break;
            case ListSelect.bodyOut:
                currentList = humanCollider.bagListBody;
                break;
            case ListSelect.pencereOut:
                currentList = humanCollider.bagListPencere;
                break;


            //COMPONENT
            case ListSelect.saseComponent:
                currentList = humanCollider.bagListSase;
                break;
            case ListSelect.wheelComponenet:
                currentList = humanCollider.bagListWheel;
                break;
            case ListSelect.koltukComponent:
                currentList = humanCollider.bagListKoltuk;
                break;
            case ListSelect.engineComponent:
                currentList = humanCollider.bagListEngine;
                break;
            case ListSelect.bodyComponent:
                currentList = humanCollider.bagListBody;
                break;
            case ListSelect.pencereComponent:
                currentList = humanCollider.bagListPencere;
                break;
            default:
                break;

            //RAF
            case ListSelect.raf:
                currentList = humanCollider.bagListWheel;
                break;

            //KARGO
            case ListSelect.kargoWheel:
                currentList = humanCollider.bagListWheel;
                logo.GetChild(0).gameObject.SetActive(true);
                logo.GetChild(1).gameObject.SetActive(false);
                price = ValueObj.metal*4;
                break;
            case ListSelect.kargoKoltuk:
                currentList = humanCollider.bagListKoltuk;
                logo.GetChild(0).gameObject.SetActive(false);
                logo.GetChild(1).gameObject.SetActive(true);
                price = ValueObj.polimer*5;
                break;

            //Recycle
            case ListSelect.Recycle:
                currentList = humanCollider.bagListMetal;
               
                price = 3;
                break;
        }

    }
}
