using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    
    public Light lightDirect;

    public Color darkColor;
    private Color whiteColor;
   
    public int repeatTime;
    public GameObject flashlight;
    public float darkIntensity;
    public Animator modelGenarator;
    private void Start()
    {
        whiteColor = RenderSettings.ambientLight;
        StartCoroutine(CloseLight());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Human")
        {
          
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
            modelGenarator.enabled = false;
            flashlight.SetActive(true);
            lightDirect.intensity = darkIntensity;
            RenderSettings.ambientLight = darkColor;
            run = false;
        }
    }
}
