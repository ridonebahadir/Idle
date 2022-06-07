using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform human;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

   
    void Update()
    {
        navMeshAgent.SetDestination(human.position);
    }
}
