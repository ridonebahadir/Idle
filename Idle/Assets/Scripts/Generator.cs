using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Generator : MonoBehaviour
{
    
    public Light lightDirect;

    public Color darkColor;
    private Color whiteColor;
   
    public int repeatTime;
    public GameObject flashlight;
    public float darkIntensity;
    public Animator modelGenarator;
    [Header("BOT")]
    public Bot[] bots;
    public NavMeshAgent[] navMeshAgents;
    public Animator[] animators;
    public BoxCollider[] colliders;
    [Header("ROBOT")]
    //public ManageProduction manageProduction;
    public Robotic robotic;
    public Animator[] animatorsRobot;
    [Header("CAR PARENT")]
    public Transform carparent;
    private void Start()
    {
        whiteColor = RenderSettings.ambientLight;
        StartCoroutine(CloseLight());
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Human")
        {
           
            StartCoroutine(SoundManagerSfx.Play("GeneratorOpen", 0));
            for (int i = 0; i < bots.Length; i++)
            {
               
                bots[i].enabled = true;
                navMeshAgents[i].enabled = true;
                animators[i].enabled = true;
                colliders[i].enabled = true;

            }
            foreach (Transform item in carparent)
            {
                item.GetComponent<CarGo>().enabled = true;
            }
            //manageProduction.enabled = true;
            robotic.enabled = true;
            for (int i = 0; i < animatorsRobot.Length; i++)
            {
                animatorsRobot[i].enabled = true;
            }
            
            lightDirect.intensity = 1;
            flashlight.SetActive(false);
            RenderSettings.ambientLight = whiteColor;
            modelGenarator.enabled = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
        {
            
                run = true;
                StartCoroutine(CloseLight());
            
        }
            
    }
    bool run = true;
    IEnumerator CloseLight()
    {
        while (run)
        {
            yield return new WaitForSeconds(repeatTime);
            StartCoroutine(SoundManagerSfx.Play("GeneratorClose", 0));
            for (int i = 0; i < bots.Length; i++)
            {
                bots[i].enabled = false;
                navMeshAgents[i].enabled = false;
                animators[i].enabled = false;
                colliders[i].enabled = false;
                
            }
            foreach (Transform item in carparent)
            {
                item.GetComponent<CarGo>().enabled = false;
            }
            //manageProduction.enabled = false;
            robotic.enabled = true;
            animatorsRobot[0].enabled = false;
            animatorsRobot[1].enabled = false;
            modelGenarator.enabled = false;
            flashlight.SetActive(true);
            lightDirect.intensity = darkIntensity;
            RenderSettings.ambientLight = darkColor;
            
            run = false;
        }
    }
}
