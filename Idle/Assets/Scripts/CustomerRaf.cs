using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerRaf : MonoBehaviour
{
    public int wheelSell;
    private Spawn spawn;
    private Transform target1;
    bool oneTime = false;
    public Transform kucak;
    public GameObject customerRafPrefab;
    private Vector3 startPos;
    private Transform parent;
    private CustomerRaf customerRaf;
    public GameObject money;
    public Transform moneyParent;
    private Animator anim;
    public Transform particle;
    private void Start()
    {
        particle.GetChild(0).gameObject.SetActive(true);
        particle.GetChild(1).gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        for (int i = 0; i < 10; i++)
        {
           GameObject obj = Instantiate(money, transform.position, Quaternion.identity, moneyParent);
            obj.SetActive(false);
        }
        spawn = transform.parent.parent.GetChild(0).GetComponent<Spawn>();
        parent = transform.parent;
       
      
        startPos = transform.position;
        target1 = transform.parent.parent.GetChild(2).transform;
    }
    public void Update()
    {
        if (target1.childCount>0)
        {
            if (!oneTime)
            {
                Move();
                anim.SetBool("Walk", true);
                oneTime = true;
            }
        }
       
    }
    private void Move()
    {
       
        transform.DOMove(target1.position-new Vector3(0,0,2), 3f).OnComplete(() => {
            anim.SetBool("Walk", false);
            transform.parent = null;
            Instantiate(customerRafPrefab, startPos, Quaternion.identity, parent);
            parent.GetChild(0).GetComponent<CustomerRaf>().enabled=false;

            target1.GetChild(target1.childCount - 1).gameObject.transform.parent = kucak;
            kucak.GetChild(0).gameObject.transform.DOJump(kucak.position, 3, 0, 1f, false).OnComplete(()=>Pay()).SetEase(Ease.InQuint);
        });
    }
    void Pay()
    {
       
        StartCoroutine(PayGo(true));
        
    }
   
    IEnumerator PayGo(bool run)
    {
        while (run)
        {
            if (moneyParent.transform.childCount>0)
            {
                GameObject obj = moneyParent.GetChild(0).transform.gameObject;
                obj.transform.parent = spawn.humanCollider.bag;
                obj.SetActive(true);
                obj.transform.DOLocalJump(new Vector3(0, 0, 0), 3, 0, 1.5f, false).OnComplete(() => Destroy(obj)).SetEase(Ease.OutQuint);
                spawn.humanCollider.gameManager.money += wheelSell;
                spawn.humanCollider.gameManager.moneyText.text = spawn.humanCollider.gameManager.money.ToString();
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                
                Move2();
                run = false;
            }

        }
        
    }
    void Move2()
    {
        particle.GetChild(0).gameObject.SetActive(false);
        particle.GetChild(1).gameObject.SetActive(true);
        anim.SetBool("Carry", true);
        transform.DOLocalRotate(new Vector3(0, 180, 0), 0.2f);
        customerRaf = parent.GetChild(0).GetComponent<CustomerRaf>();
        customerRaf.enabled = true;
        transform.DOMove( (transform.position+new Vector3(3,0,-10)), 5f);
        
    }
}
