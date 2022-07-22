using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManageProduction : MonoBehaviour
{
    private Animator roboticAnim;
    public Transform[] breakPoints;
    int turn;
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
                roboticAnim.SetInteger("Turn", animTurn);
                if (animTurn<3)
                {
                    animTurn++;
                    turn++;
                }
              
                oneTime = true;
            }
        }
    }
}
