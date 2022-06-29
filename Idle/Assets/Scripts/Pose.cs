using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    Animator anim;
    public int howAnim;
    public KeyCode key;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
  
    private void Update()
    {
        Move(key);
       
           
       
    }

    int a = 1;
     void Move(KeyCode key)
    {
        

        if (Input.GetKeyUp(key))
        {
            if (a == howAnim)
            {
                a = 0;
            }

            anim.SetInteger("Turn", a);
            a++;
        }
       
    }
    void Before()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            a--;
                if (a<0)
            {
                a = 0;
            }
        }
    }
    void After()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            a++;
            if (a < 0)
            {
                a = 0;
            }
        }
    }
}
