using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SellRaw : MonoBehaviour
{
    public int value;
    public int adet;
    public GameObject raw;
    public Spawn spawn;
    public Transform outPoint;
    public Transform kamyon;
    public GameManager gameManager;
    public BoxCollider paletCollider;
   
 
    private void OnMouseDown()
    {
        paletCollider.enabled = false;
        run = true;
        StartCoroutine(SpawnRaw());
       
      
        
    }
    float x;
    float y;
    float z;
    bool run;
    IEnumerator SpawnRaw()
    {
        while (run)
        {
            if (adet<10)
            {
                GameObject obj = Instantiate(raw, kamyon);
                obj.transform.parent = outPoint;
                adet++;
                gameManager.money -= value;
                gameManager.moneyText.text = gameManager.money.ToString();
                spawn.list.Add(obj);



                obj.transform.DOLocalJump(new Vector3(x, y, z), 3, 0, 3f, false).SetEase(Ease.InQuint);
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
                yield return new WaitForSeconds(4f);
                paletCollider.enabled = true;
                adet = 0;
                run = false;
            }
           
            yield return new WaitForSeconds(0.1f);
        }


          
    }
}

