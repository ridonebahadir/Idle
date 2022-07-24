using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform[] target;
    //public Transform[] machine;
    public Transform startPos;
    private Animator anim;
    public int stations;
    public bool[] touch;
    public Transform bag;
    void Start()
    {
        anim = GetComponent<Animator>();
        //startPos.position = transform.position;
        //int a = Random.Range(0, machine.Length);
        //target = machine[a];
        navMeshAgent = GetComponent<NavMeshAgent>();
       
    }
    public bool go;
   
    void Update()
    {
        if (go)
        {
            navMeshAgent.SetDestination(target[stations].position);
            if (bag.transform.childCount>0)
            {
                anim.SetBool("Carry", true);
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Carry", false);
                anim.SetBool("Run", true);
            }
        }
        else
        {
            if (bag.transform.childCount>0)
            {
                anim.SetBool("Carry", true);
                anim.SetBool("Walk", false);
            }
            else
            {
                anim.SetBool("Carry", false);
                anim.SetBool("Run", false);
                anim.SetBool("Walk", false);

               
            }
        }
       
       



    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Order")
        {

            go = false;

            StartCoroutine(Station(0, 1, 3));

        }
        if (other.tag == "RawMaterial")
        {
            if (touch[0])
            {
                
                StartCoroutine(Station(1,2,3));
            }
        }
        if (other.tag == "Put")
        {
            if (touch[1])
            {
                
                StartCoroutine(Station(2, 3, 3));
            }
        }
        if (other.tag == "OutPut")
        {
            if (touch[2])
            {
               
                StartCoroutine(Station(3, 4, 3));
            }
        }
        if (other.tag == "Component")
        {
            if (touch[3])
            {
               

                StartCoroutine(Station(4,0,1));

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
}
