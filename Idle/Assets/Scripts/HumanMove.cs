using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMove : MonoBehaviour
{
    public Transform dragMove;
    public DynamicJoystick dynamicJoystick;
    public float speed;
    public Animator anim;
    public Transform humanBody;
    public Transform bag;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = dynamicJoystick.Horizontal;
        float verticalInput = dynamicJoystick.Vertical;

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);


       

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            humanBody.rotation = Quaternion.RotateTowards(humanBody.rotation, toRotation, 720 * Time.deltaTime);
        }
        if (bag.transform.childCount>0)
        {
            
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("Carry", true);
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Carry", true);
                anim.SetBool("Walk", false);
            }
        }
        else
        {
           
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("Carry", false);
                anim.SetBool("Run", true);
              
            }
            else
            {
                anim.SetBool("Carry", false);
                anim.SetBool("Run", false);
                anim.SetBool("Walk", false);
               
            }
        }
       
    }
}
