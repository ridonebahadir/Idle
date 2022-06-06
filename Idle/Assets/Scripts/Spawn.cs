using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Spawn : MonoBehaviour
{
    
    public GameObject prefab;
    public float gridX = 0.3f;
    public float gridY = 0.3f;
    public float spacingx = 0.1f;
    public float spacingy = 0.1f;
    public List<GameObject> list = new List<GameObject>();
    public HumanCollider humanCollider;
    public bool start;
    //public GameObject warning;
    [Header("SHAKE")]
    public Transform machine;
    private float shakePower = 0.8f;
    private float shakeTimer = 1f;
   
    

    [Header("OUTPUT")]
    public Transform outPoint;
    public Transform breakPoint;
    public int[] productCount;
    public int outPutCount;
    public bool runMachine;
    public TextMeshPro[] textMesh;

    [Header("COMPONENT ARALIK")]
    public int xComponent;
    public int zComponent;
    public int yComponent;
    public int enComponent;
    public int boyComponent;

    [Header("OUTPUT ARALIK")]
    public int xOutPut;
    public int zOutPut;
    public int yOutPut;
    public int enOutPut;
    public int boyOutPut;
    //[Header("PRODUCT L?NE")]
    //public bool runLine;


    void Start()
    {
        WhichList();
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
     int y;
     int z;
    bool oneTime = true;
    public bool productLine;
   
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
            humanCollider.bagYAxis += 0.25f;
            //Azalma(true);
            Vibration.Vibrate(10);
        }
        else
        {
            x = 0; y = 0; z = 0;
            humanCollider.run = false;
        }
      

    }

    public void GoObj(List<GameObject> bagList, int id, bool grid)
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
        Vibration.Vibrate(40);
       
    }
    
    public IEnumerator OutPut(int a,int b)
    {
        yield return new WaitForSeconds(3f);
       
        while (runMachine)
        {

            if (((productCount[0] -= a) >= 0) && ((productCount[1] -= b) >= 0))
            {
                machine.DOShakeRotation(shakeTimer, shakePower, fadeOut: true);
                textMesh[0].text = productCount[0].ToString();
                textMesh[1].text = productCount[1].ToString();
                //for (int i = 0; i < productCount.Length; i++)
                //{
                //    textMesh[i].text = productCount[i].ToString();
                //}

                outPutCount++;
            }
           
            if (productCount[0] <= 0)
            {
               
                runMachine = false;
                productCount[0] = 0;
            }
            if (productCount[1] <= 0)
            {
                
                runMachine = false;
                productCount[1] = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < outPutCount; i++)
        {
            GameObject obj = Instantiate(prefab,breakPoint);

            obj.transform.parent = outPoint;

            obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
            obj.transform.DOLocalJump(new Vector3(x*0.5f, y*0.5f, z*0.5f), 3, 3, 0.3f, false).SetEase(Ease.InQuint);

            x+=xOutPut;
                if (x == xOutPut*enOutPut)
                {
                    x = 0;
                    z+=zOutPut;
                    if (z == zOutPut*boyOutPut)
                    {
                        y+=yOutPut;
                        z = 0;
                    }

                }
            
            
            list.Add(obj);
            textMesh[0].text = productCount[0].ToString();
            textMesh[1].text = productCount[1].ToString();  
            yield return new WaitForSeconds(0.1f);

        }
        outPutCount = 0;
    }
   
    public void Azalma(bool isOut)
    {
        if (isOut)
        {
            x -= xOutPut;
            if (x == 0)
            {
                x = xOutPut * enOutPut;
                z -= zOutPut;
                if (z ==0)
                {
                    y -= yOutPut;
                    z = zOutPut * boyOutPut;
                }

            }
        }
        else
        {
            x -= xComponent;
            if (x == xComponent * enComponent)
            {
                x = xComponent;
                z -= zComponent;
                if (z == zComponent * boyComponent)
                {
                    y -= yComponent;
                    z = zComponent;
                }

            }
        }
    }
    public enum ListSelect {
        metal,polimer,cam,kablo,
        sase,wheel,koltuk,engine, body, pencere,
        saseOut, wheelOut, koltukOut, engineOut, bodyOut, pencereOut,
        saseComponent, wheelComponenet,koltukComponent, engineComponent, bodyComponent, pencereComponent,
        raf};
    public ListSelect whichList;
    public List<GameObject> currentList;
    public List<GameObject> currentList2;
   
    
  

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
                currentList2 = humanCollider.bagListPolimer;
                break;
            case ListSelect.wheel:
                currentList = humanCollider.bagListMetal;
                currentList2 = humanCollider.bagListPolimer;
                break;
            case ListSelect.koltuk:
                currentList = humanCollider.bagListPolimer;
                currentList2 = humanCollider.bagListKablo;
                break;
            case ListSelect.engine:
                currentList = humanCollider.bagListMetal;
                currentList2 = humanCollider.bagListKablo;
                break;
            case ListSelect.body:
                currentList = humanCollider.bagListMetal;
                currentList2 = humanCollider.bagListKablo;
                break;
            case ListSelect.pencere:
                currentList2 = humanCollider.bagListCam;
                currentList = humanCollider.bagListPolimer;
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
        }

    }
}
