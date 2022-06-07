using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cost : MonoBehaviour
{
    public GameManager gameManager;
    public int costValue;
    public GameObject open;
    public Transform[] transforms;
    public GameObject close;
    bool run;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Human")
        {
            run = true;
            if (gameManager.money>costValue)
            {
                StartCoroutine(OpenMachine());
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
        {
            run = false;
        }
    }

    IEnumerator OpenMachine()
    {
        
        yield return new WaitForSeconds(2f);
        if (run)
        {
            //open.SetActive(true);
            StartCoroutine(OpenTurn());
           
            gameManager.money -= costValue;
            gameManager.moneyText.text = gameManager.money.ToString();
           
        }
    }

    IEnumerator OpenTurn()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i].gameObject.SetActive(true);
            transforms[i].transform.DOPunchScale(new Vector3(.2f, 0.2f, .2f), 0.1f);   //KUFU_ANIM
            yield return new WaitForSeconds(0.3f);
            if (i==transforms.Length-1)
            {
                close.SetActive(false);
            }
        }
    }  
}
