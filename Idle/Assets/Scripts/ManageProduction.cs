using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManageProduction : MonoBehaviour
{
    private Animator roboticAnim;
    public Transform[] breakPoints;
    public Transform hazne;
    public int turn;
    int animTurn;
    bool oneTime;
    void Start()
    {
        roboticAnim = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (breakPoints[turn].childCount>0)
        {
           
            if (!oneTime)
            {

                StartCoroutine(Run());
                


                oneTime = true;
            }
        }
        
    }
  
    IEnumerator Run()
    {
        
        roboticAnim.SetInteger("Turn", animTurn);
        GameObject obj = breakPoints[turn].GetChild(0).gameObject;
       
        yield return new WaitForSeconds(0.45f);
        obj.transform.parent = hazne;
        obj.transform.DOLocalMove(Vector3.zero,0.2f);
        for (int i = 0; i < breakPoints[turn].childCount; i++)
        {
            breakPoints[turn].GetChild(i).DOLocalMove(new Vector3(0,i+1,0),0.5f);
        }
        yield return new WaitForSeconds(1f);
        Destroy(obj);
        yield return new WaitForSeconds(1f);
        if (turn < 2)
        {
            animTurn++;
            turn++;
        }
        else
        {
            animTurn = 0;
            turn = 0;
        }
        oneTime = false;


    }
}
