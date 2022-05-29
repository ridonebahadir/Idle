using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProductLine : MonoBehaviour
{
    public GameObject car;
    public Transform[] component;
  
    public int turn;
    
    bool oneTime;
    public bool run;
   
    private void Update()
    {
        
        if (component[turn].transform.childCount > 0)
        {
            if (!oneTime)
            {
                run = true;
                GoCar();
                oneTime = true;
            }
            
        }
    }
    public void GoCar()
    {
        StartCoroutine(CarMove());
    }
    IEnumerator CarMove()
    {
        if (run)
        {
            car.transform.DOLocalMove(new Vector3(component[turn].transform.position.x, car.transform.localPosition.y,car.transform.localPosition.z), 2f)
                .OnComplete(()=> { car.transform.GetChild(turn).gameObject.SetActive(true); if (turn < component.Length-1) turn++; oneTime = false; run = false;});
            yield return new WaitForSeconds(0.1f);
        }

       
    }
}
