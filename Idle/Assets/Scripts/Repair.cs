using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Repair : MonoBehaviour
{
    public BoxCollider[] spawnsCollider;
    public Transform[] machines;
    public Transform[] repairButton;
    public int repairTime;
    private NavMeshAgent navMeshAgent;
   
    float destination;
    Animator anim;
    Vector3 target;
    Vector3 startPos;

    public int randomMachine;
    private void Start()
    {
        foreach (Transform item in repairButton)
        {
            item.gameObject.SetActive(false);
        }
        startPos = transform.position;
        anim =transform.GetChild(0).GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        StartCoroutine(BrokenMachine());
    }
    IEnumerator BrokenMachine()
    {
        yield return new WaitForSeconds(repairTime);

        randomMachine = Random.Range(0, spawnsCollider.Length);
        repairButton[randomMachine].gameObject.SetActive(true);
        spawnsCollider[randomMachine].enabled = false;
        target = machines[randomMachine].position;
       
    }



    
    public IEnumerator Go()
    {
        bool run = true;
        while (run)
        {
            anim.SetBool("Walk", true);

            navMeshAgent.SetDestination(target);
            yield return new WaitForSeconds(1);
            destination = navMeshAgent.remainingDistance;
            
            if (destination < 1)
            {
                anim.SetBool("Repair", true);
                anim.SetBool("Walk", false);
                Invoke("GoBack", 12);
            }
            else
            {
                anim.SetBool("Repair", false);
                anim.SetBool("Walk", true);
            }
            
        }
        
    }

    void GoBack()
    {
        //StartCoroutine(Go());
        
        target = startPos;
        spawnsCollider[randomMachine].enabled = true;
        repairButton[randomMachine].gameObject.SetActive(false);
        //repairTime = 10;
        //StartCoroutine(BrokenMachine());

    }
}
