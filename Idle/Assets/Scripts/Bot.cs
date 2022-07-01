using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform target;
    //public Transform[] machine;
    public Transform startPos;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        startPos.position = transform.position;
        //int a = Random.Range(0, machine.Length);
        //target = machine[a];
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

   
    void Update()
    {
        navMeshAgent.SetDestination(target.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Machine")
        {
            anim.SetBool("Carry", true);
            Debug.Log("asdsad");
            other.transform.parent = transform;
            target = startPos;
        }
    }
}
