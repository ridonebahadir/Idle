using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarGo : MonoBehaviour
{
    public Vector3[] stations;
    
    public float[] time;
    private CarParent carParent;
    public float zAxisYan = 5;
   
    private void Start()
    {
       
        carParent = transform.parent.GetComponent<CarParent>();
    }
    public void Go()
    {
       
        StartCoroutine(GoCar());
        
    }
   
    IEnumerator GoCar()
    {
        

        for (int i = 0; i < stations.Length; i++)
        {
            transform.DOLocalMove(stations[i], time[i]);
           
            yield return new WaitForSeconds(time[i]);
            
            
            
        }
        transform.DOLocalMoveZ(carParent.zAxis, 2f).OnComplete(()=> 
        { 
            transform.parent = null;
            carParent.willGoCar.Add(gameObject);
        });

        carParent.zAxis += zAxisYan;




    }
}
