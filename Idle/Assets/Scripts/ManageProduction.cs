using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManageProduction : MonoBehaviour
{
    private Animator roboticAnim;
    public Transform[] breakPoints;
    public Transform hazne;
    public int turn;
   
    public int setactiveTurn;
    public CarGo carGo;
    int startSetactive;
    int animTurn;
    bool oneTime;
    public bool firsRobotic;
    public ManageProduction manageProduction;
    private GameObject car;
    public GameObject carPrefab;
    public CarParent carParent;
    void Start()
    {
        startSetactive = setactiveTurn;
        roboticAnim = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (breakPoints[turn].childCount>0)
        {
            
            if (!oneTime)
            {
                if (firsRobotic && turn == 0)
                {
                    car = Instantiate(carPrefab, carParent.transform.position,Quaternion.Euler(0,-90,0),carParent.transform);
                }
                StartCoroutine(Run());
                


                oneTime = true;
            }
        }
        
    }
   
    IEnumerator Run()
    {
        if (!firsRobotic&&turn==0)
        {
            car = manageProduction.car;
        }
      
        roboticAnim.SetInteger("Turn", animTurn);
        GameObject obj = breakPoints[turn].GetChild(0).gameObject;
       
        yield return new WaitForSeconds(0.45f);
        obj.transform.parent = hazne;
        obj.transform.DOLocalMove(Vector3.zero,0.2f);
        for (int i = 0; i < breakPoints[turn].childCount; i++)
        {
            breakPoints[turn].GetChild(i).DOLocalMove(new Vector3(0,i+1,0),0.5f);
        }
        yield return new WaitForSeconds(1f);
        Destroy(obj);
        car.transform.GetChild(setactiveTurn).gameObject.SetActive(true);
       
        if (turn==2)
        {
            if (!firsRobotic)
            {
                carGo = carParent.transform.GetChild(carParent.a).GetComponent<CarGo>();
                carGo.Go();
                carParent.a++;


            }
            yield return new WaitForSeconds(2f);//arac olusturuldu
        }
        yield return new WaitForSeconds(0.5f);
        if (turn < 2)
        {
            animTurn++;
            turn++;
            setactiveTurn++;
        }
        else
        {
            setactiveTurn = startSetactive;
            animTurn = 0;
            turn = 0;
        }
        oneTime = false;


    }
}
