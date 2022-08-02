using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HumanMove : MonoBehaviour
{
    public Transform[] palet;
    public Transform dragMove;
    public DynamicJoystick dynamicJoystick;
    public float speed;
    public Animator anim;
    public Transform humanBody;
    public Transform bag;
    [Header("VEHICLE")]
    public bool[] whichVehicle;
    public Transform[] vehicle;
   
    
    protected AnimatorOverrideController animatorOverrideController;
    protected int weaponIndex;
    public AnimationClip[] runClip;
    public AnimationClip[] runMoveClip;
    Rigidbody rb;
    // Update is called once per frame



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        weaponIndex = 0;
        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;

    }
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
                //if ((!whichVehicle[1])||(!whichVehicle[2]))
                //{
                //    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                //}
                //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                //if (whichVehicle[1])
                //{
                   
                    
                //}
              
                //if (whichVehicle[2])
                //{
                //    transform.position = new Vector3(transform.position.x,  0.3f, transform.position.z);

                    
                //}
               
            }
            else
            {
                //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
               
                anim.SetBool("Carry", false);
                anim.SetBool("Run", false);
                anim.SetBool("Walk", false);
               
            }
        }
       
    }
    public void VehicleChange(int vecihleCount)
    {
        //transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
        for (int i = 0; i < whichVehicle.Length; i++)
        {
            vehicle[i].gameObject.SetActive(false);
            whichVehicle[i] = false;
            palet[i].gameObject.SetActive(false);
        }
        switch (vecihleCount)
        {
            case 0:
                palet[1].gameObject.SetActive(true);
                palet[2].gameObject.SetActive(true);
                break;
            case 1:
                palet[1].gameObject.SetActive(false);
                palet[2].gameObject.SetActive(true);
                break;
            case 2:
                palet[1].gameObject.SetActive(true);
                palet[2].gameObject.SetActive(false);
                break;
            default:
                break;
        }

        vehicle[vecihleCount].gameObject.SetActive(true);
        speed = (vecihleCount + 5) * 2;
       
        whichVehicle[vecihleCount] = true;
        animatorOverrideController["Running(1)"] = runClip[vecihleCount];
        animatorOverrideController["Running"] = runMoveClip[vecihleCount];


    }
   
}
