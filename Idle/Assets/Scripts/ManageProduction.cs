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
    bool oneTÝme;
    void Start()
    {
        roboticAnim = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (breakPoints[turn].childCount>0)
        {
            if (!oneTÝme)
            {
                roboticAnim.SetInteger("Turn", animTurn);
                if (animTurn<3)
                {
                    animTurn++;
                    turn++;
                }
              
                oneTÝme = true;
            }
        }
    }
}
