using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cost : MonoBehaviour
{
    public GameManager gameManager;
    public int costValue;
    public GameObject open;
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
            open.SetActive(true);
            open.transform.DOPunchScale(new Vector3(.20f, 0.2f, .2f), 0.1f).OnComplete(()=>close.SetActive(false));
            gameManager.money -= costValue;
            gameManager.moneyText.text = gameManager.money.ToString();
           
        }
    }
}
