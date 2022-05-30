using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProductLine : MonoBehaviour
{
   
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
            transform.GetChild(turn).gameObject.SetActive(true);
            transform.DOLocalMove(new Vector3(component[turn].transform.position.x, transform.localPosition.y,transform.localPosition.z), 2f)
                .OnComplete(()=> {  if (turn < component.Length-1) turn++; oneTime = false; run = false;});
            yield return new WaitForSeconds(0.1f);
        }

       
    }
}
