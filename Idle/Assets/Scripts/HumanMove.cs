using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HumanMove : MonoBehaviour
{
    public Transform[] palet;
    public Transform dragMove;
    public DynamicJoystick dynamicJoystick;
   
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
    [Header("MOVEMENT")]
    public float speed;
    public float maxSpeed;
    public float duration;
    public Ease newEase;
    bool acceleration;
    float startSpeed;
    private Sequence sequence;
    private Guid uid;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        startSpeed = speed;
        weaponIndex = 0;
        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;

    }
    public CameraFollow cameraFollow;
    public bool run;
    void Update()
    {

        float horizontalInput = dynamicJoystick.Horizontal;
        float verticalInput = dynamicJoystick.Vertical;
        switch (cameraFollow.value)
        {
            case 0:
                Movement(horizontalInput, verticalInput);
                break;
            case 1:
                Movement(verticalInput, -horizontalInput);
                break;
            case 2:
                Movement(-horizontalInput, -verticalInput);
                break;
            case 3:
                Movement(-verticalInput, horizontalInput);
                break;
           
        }
        if (Input.GetMouseButtonDown(0))
        {
            acceleration = false;
            
        }
        if (bag.transform.childCount>0)
        {
            
            if (Input.GetMouseButton(0)&&run)
            {
                
                if (!acceleration)
                {
                    Acceleration(newEase, duration);
                    acceleration = true;
                }
               
                anim.SetBool("Carry", true);
                anim.SetBool("Walk", true);
                
            }
            else
            {
                StartCoroutine(SoundManagerSfx.Play("SkateStop", 0));
                speed = startSpeed;
                DOTween.Kill(uid);
                sequence = null;
                anim.SetBool("Carry", true);
                anim.SetBool("Walk", false);
            }
        }
        else
        {
           
            if (Input.GetMouseButton(0)&&run)
            {
                if (!acceleration)
                {
                    Acceleration(newEase, duration);
                    acceleration = true;
                }
                anim.SetBool("Carry", false);
                anim.SetBool("Run", true);
               
            }
            else
            {
                //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                StartCoroutine(SoundManagerSfx.Play("SkateStop", 0));
                DOTween.Kill(uid);
                sequence = null;
                speed = startSpeed;
                anim.SetBool("Carry", false);
                anim.SetBool("Run", false);
                anim.SetBool("Walk", false);
               
            }
        }
       
    }
    void Movement(float a,float b)
    {
       
        Vector3 movementDirection;
        movementDirection = new Vector3(a, 0, b);
        movementDirection.Normalize();
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);


        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            humanBody.rotation = Quaternion.RotateTowards(humanBody.rotation, toRotation, 720 * Time.deltaTime);
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

                duration = 2f;
                speed = 10;
                maxSpeed = 15;
                newEase = Ease.InSine;

                break;
            case 1:
                palet[1].gameObject.SetActive(false);
                palet[2].gameObject.SetActive(true);
                

                duration = 4f;
                speed = 13;
                maxSpeed = 20;
                newEase = Ease.OutSine;
                break;
            case 2:
                palet[1].gameObject.SetActive(true);
                palet[2].gameObject.SetActive(false);

                duration = 4f;
                speed = 15;
                maxSpeed = 23;
                newEase = Ease.OutSine;
                break;
            default:
                break;
        }

        vehicle[vecihleCount].gameObject.SetActive(true);
        //speed = (vecihleCount + 5) * 2;
       
        whichVehicle[vecihleCount] = true;
        animatorOverrideController["Running(1)"] = runClip[vecihleCount];
        animatorOverrideController["Running"] = runMoveClip[vecihleCount];


    }
   
   public void Acceleration(Ease ease,float time)
    {
        
        StartCoroutine(SoundManagerSfx.Play("Skate", 0));
        sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => speed, x => speed = x, maxSpeed, time)
              .OnUpdate(() => {

              }).SetEase(ease));

        uid = System.Guid.NewGuid();
        sequence.id = uid;
    }
}
