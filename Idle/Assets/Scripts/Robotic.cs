using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Robotic : MonoBehaviour
{
    public GameManager gameManager;
    public Animator[] roboticAnim;
    public Transform[] breakPoints;
    public Transform[] hazne;
    public int turn;

    public int setactiveTurn;
    public CarGo carGo;
    int startSetactive;
    public int animTurn;
    public int animTurn1;
    bool oneTime;


    private GameObject car;
    public GameObject[] carPrefab;
    public CarParent carParent;

    void Start()
    {
        oneTime = false;
        startSetactive = setactiveTurn;

    }


    void Update()
    {
        if (breakPoints[turn].childCount > 0)
        {

            if (!oneTime)
            {
                if (turn == 0)
                {
                    car = Instantiate(carPrefab[gameManager.upgradeCount], carParent.transform.position, Quaternion.Euler(0, -90, 0), carParent.transform);
                }

                StartCoroutine(Run());





                oneTime = true;
            }
        }

    }

    IEnumerator Run()
    {
        GameObject obj = breakPoints[turn].GetChild(0).gameObject;
        int a = turn % 2;
        roboticAnim[turn].SetTrigger("Run");
        //if (a==0)
        //{
        //    roboticAnim[0].SetInteger("Turn", animTurn);
        //}
        //else
        //{
        //    roboticAnim[1].SetInteger("Turn", animTurn1);
        //}
        yield return new WaitForSeconds(0.5f);
        //roboticAnim.SetInteger("Turn", animTurn);
        obj.transform.parent = hazne[turn];
        //if (a == 0)
        //{
        //    obj.transform.parent = hazne[0];
        //}
        //else
        //{
        //    obj.transform.parent = hazne[1];
        //}
        //obj.transform.parent = hazne;
        obj.transform.DOLocalMove(Vector3.zero, 0.2f);
        breakPoints[turn].transform.parent.GetChild(0).GetComponent<Spawn>().y -= 1;
        for (int i = 0; i < breakPoints[turn].childCount; i++)
        {
            breakPoints[turn].GetChild(i).DOLocalMove(new Vector3(0, i + 1, 0), 0.5f);

        }
        yield return new WaitForSeconds(1f);
        Destroy(obj);

        car.transform.GetChild(setactiveTurn).gameObject.SetActive(true);
       
        if (turn==1)
        {
            car.transform.DOLocalMoveX(car.transform.localPosition.x-15, 1f);
        }
        if (turn==3)
        {
            car.transform.DOLocalMoveX(car.transform.localPosition.x - 15, 1f);
        }
        if (turn == 5)
        {
            car.transform.GetChild(setactiveTurn + 1).gameObject.SetActive(true);
            carGo = carParent.transform.GetChild(0).GetComponent<CarGo>();
            yield return new WaitForSeconds(0.25f);
            //willGoCar.Add(car);

            carGo.Go();


            //carParent.a++;

            //yield return new WaitForSeconds(0.5f);//arac olusturuldu
        }
        yield return new WaitForSeconds(0.25f);
        if (turn < 5)
        {
            if (a == 0)
            {
                animTurn++;
            }
            else
            {
                animTurn1++;
            }

            turn++;
            setactiveTurn++;
        }
        else
        {
            setactiveTurn = startSetactive;
            animTurn = 0;
            animTurn1 = 0;
            turn = 0;
        }
        yield return new WaitForSeconds(0.31f);
        oneTime = false;


    }
}
