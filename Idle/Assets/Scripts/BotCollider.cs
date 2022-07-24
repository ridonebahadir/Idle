using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollider : MonoBehaviour
{
    private Spawn spawn;
    private SellRaw sellRaw;
    bool run;
    public Transform bag;
    public float bagYAxis;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RawMaterial")
        {

            spawn = other.transform.GetComponent<Spawn>();
            run = true;
            StartCoroutine(ComeObj(true));



        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RawMaterial")
        {
            //StartCoroutine(FixedHeight());
            
            run = false;
            sellRaw = other.gameObject.transform.parent.parent.GetChild(1).GetChild(0).GetComponent<SellRaw>();
            sellRaw.Azalma();

        }
    }

    IEnumerator ComeObj(bool add)
    {
        while (run)
        {
           
                spawn.ComeObj(bag, bagYAxis, add);

            
         

            yield return new WaitForSeconds(0.15f);

        }
    }
}
