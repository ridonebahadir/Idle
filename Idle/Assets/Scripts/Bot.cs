using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class Bot : MonoBehaviour
{
    
    private NavMeshAgent navMeshAgent;
   
    //public Transform[] machine;
    public Transform pamarlik; 
    private Animator anim;
    public int stations;
    public bool[] touch;
    public Transform bag;
    public Transform[] target;
    public WaitArea waitArea;
    void Start()
    {
        touch[0] = true;
        anim = GetComponent<Animator>();
        //startPos.position = transform.position;
        //int a = Random.Range(0, machine.Length);
        //target = machine[a];
        navMeshAgent = GetComponent<NavMeshAgent>();
        des = navMeshAgent.remainingDistance;
        anim.SetBool("Run", true);
    }
    public bool go;
    float des;
    bool wait;
    Spawn spawn;
    BoxCollider boxCollider;
    void Update()
    {
        boxCollider = GetComponent<BoxCollider>();
        des = navMeshAgent.remainingDistance;

        if (go)
        {
            navMeshAgent.SetDestination(target[stations].position);

            //if (bag.transform.childCount>0)
            //{
            //    anim.SetBool("Carry", true);
            //    anim.SetBool("Walk", true);
            //}
            //else
            //{
            //    anim.SetBool("Carry", false);
            //    anim.SetBool("Run", true);
            //}
            if (!wait)
            {
                if (stations == 0)
                {
                    if (des < 0.5f && !waitArea.isEmpty)
                    {
                        anim.SetBool("Run", false);
                        anim.SetBool("Wait", true);

                    }
                }
                else
                {
                    //anim.SetBool("Run", true);
                    anim.SetBool("Wait", false);
                    wait = true;
                }
            }
                
            
           
            
          
        }

       
        //else
        //{
        //    if (bag.transform.childCount>0)
        //    {
        //        anim.SetBool("Carry", true);
        //        anim.SetBool("Walk", false);
        //    }
        //    else
        //    {
        //        anim.SetBool("Carry", false);
        //        anim.SetBool("Run", false);
        //        anim.SetBool("Walk", false);


        //    }
        //}





    }
   
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "WaitPoint")
        {

            if (waitArea.isEmpty&&touch[0])
            {
               
                StartCoroutine(Station(1, 1, 0));
               
            }
           


        }
        if (other.tag== "Order")
        {
            if (touch[1])
            {
              
                StartCoroutine(Station(2, 2, 8));
                anim.SetBool("Run", false);
                anim.SetBool("Carry", false);
            }
            

        }
        if (other.tag == "RawMaterial")
        {
            if (touch[1])
            {
                
                anim.SetBool("Run", true);
                
            }
            if (touch[2])
            {

                
                StartCoroutine(Station(3,3,5));
                anim.SetBool("Run", false);
                anim.SetBool("Wait", false);
            }
            
        }
        if (other.tag == "Put")
        {
            if (touch[3])
            {

                spawn = other.GetComponent<Spawn>();
                anim.SetBool("Carry", false);
                anim.SetBool("CarryRun", false);
                anim.SetBool("Run", false);
                if (spawn.outPoint.childCount<spawn.capasity)
                {
                    StartCoroutine(Station(0, 0, 3));
                    
                }
                else
                {
                    boxCollider.enabled = false;
                }
               
                
            }
        }
        
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag== "WaitPoint")
        {
            anim.SetBool("Run", true);
        }
        if (other.tag== "Order")
        {

            anim.SetBool("Carry", true);
            
        }
        if (other.tag== "RawMaterial")
        {
            if (touch[3])
            {
                anim.SetBool("CarryRun",true);
            }
       
        }
        if (other.tag=="Put")
        {
            Invoke("Late",1f);
            if (touch[0])
            {
               
                anim.SetBool("Run", true);
                anim.SetBool("Wait", false);
            }
        }
    }
    IEnumerator Station(int touchValue,int stationsVAlue,float waitValue)
    {
        go = false;
       
        yield return new WaitForSeconds(waitValue);
        
            for (int i = 0; i < touch.Length; i++)
            {
                touch[i] = false;
            }
            touch[touchValue] = true;
            stations = stationsVAlue;
       
       go = true;
       


    }
    void Late()
    {
        wait = false;
    }
}
