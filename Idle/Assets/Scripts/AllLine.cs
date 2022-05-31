using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AllLine : MonoBehaviour
{
    public Transform customers;
    public Transform[] component;
    public int a = 0;
    public int currentCarNumber;
    public float showRoomZAxis = -43;
    public bool go;


    public bool oneTime = true;
    void Update()
    {
        if (go)
        {
            if (!oneTime)
            {

                Go();
                oneTime = false;
            }

        }
    }

    private void Go()
    {
        customers.transform.GetChild(0).DOLocalMove(new Vector3(0, 0, 6), 2f).OnComplete(()=>Destroy(customers.transform.GetChild(0).gameObject));
        customers.transform.GetChild(0).GetComponent<Animator>().SetBool("Walk",true);
        go = false;
    }
}
