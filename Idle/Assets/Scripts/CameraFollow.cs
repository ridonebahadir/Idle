using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public GameManager gameManager;
    public Transform human;
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    Vector3 offset;
    Vector3 offsetUp;
    public Transform[] transforms;
    bool cameraFollow = true;
    public int turn;
    Vector3 targetPos;
    Vector3 backPos;
    public Transform UpPoint;
    public HumanCollider humanCollider;
    public Transform[] changeRotate;
    public Transform cameraRotateParent;
   
    private void Awake()
    {
        if (!gameManager.locked)
        {
            turn = 12;
        }
        else
        {
            transforms[0].gameObject.SetActive(false);
            turn = PlayerPrefs.GetInt("Turn", 0);
        }
       
    }
    private void Start()
    {
        offset =  transform.position- human.transform.position;
        offsetUp =  UpPoint.position- human.transform.position;
        
            if (turn > 0)
            {
                for (int i = 1; i <= turn; i++)
                {

                    transforms[i].gameObject.SetActive(true);
                }
            }
            else
            {
                transforms[turn].gameObject.SetActive(true);
            }
        
      
      
    }
    
    void Update()
    {
        cameraRotateParent.transform.position = human.transform.position;
        if (cameraFollow)
        {
            targetPos = human.position+new Vector3(0,humanCollider.bag.childCount/2,0) + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
       
      
    }
    public void Move()
    {
        turn++;
        PlayerPrefs.SetInt("Turn", turn);
        if (turn<transforms.Length)
        {
            
           
            transforms[turn].gameObject.SetActive(true);
            
            cameraFollow = false;
            backPos = transform.position;
            transform.DOMove(new Vector3(transforms[turn].position.x, transform.position.y, transforms[turn].position.z - 20), 1.5f).OnComplete(() => Invoke("ComeBack", 1f)).SetEase(Ease.InOutSine);
        }
       
       
       
    }
    void ComeBack()
    {
        cameraFollow = true;
       
       
        //transform.DOMove(backPos, 2f).OnComplete(() => { cameraFollow = true; turn++; });;


    }
    bool go = true;
    Vector3 currentPoint;
    public void CameraGoUp()
    {
        if (go)
        {
            cameraFollow = false;
            currentPoint = transform.position;
            transform.DOMove(UpPoint.position, 1.5f).SetEase(Ease.InOutSine);
            transform.DORotate(new Vector3(90, -90, 0), 1.5f).SetEase(Ease.InOutSine);
            go = false;
            return;
        }
        else
        {
            cameraFollow = true;
            transform.DORotate(new Vector3(45, 0, 0), 1.5f).SetEase(Ease.InOutSine);
            go = true;
        }
    }
    public int value;
    public HumanMove humanMove;
    public Button cameraRotateButton;
    public DynamicJoystick dynamicJoystick;
    public Transform[] rotateObj;
    public Transform[] OrderObj;
    public Transform[] upGraade;
    public Transform unlockMachineUpgrade;
    public void CameraRotate()
    {
        humanMove.run = false;
        dynamicJoystick.enabled = false;
        cameraRotateButton.interactable = false;
        cameraFollow = false;
       
        transform.DOMove(changeRotate[value].position, 1.2f).OnComplete(() =>
        {
            offset = transform.position - human.transform.position;
            cameraRotateButton.interactable = true;
            humanMove.run = true;
            dynamicJoystick.enabled = true;
            cameraFollow = true;

        }).SetEase(Ease.InOutSine);
        transform.DORotate(changeRotate[value].rotation.eulerAngles,1.2f).SetEase(Ease.InOutSine);
        unlockMachineUpgrade.transform.DOLocalRotate(new Vector3(unlockMachineUpgrade.localRotation.eulerAngles.x, changeRotate[value].rotation.eulerAngles.y, 0), 1.2f);

        if (value<3) value++;
        else value = 0;
        for (int i = 0; i < rotateObj.Length; i++)
        {
            rotateObj[i].transform.DOLocalRotate(new Vector3(rotateObj[i].transform.eulerAngles.x, changeRotate[value].rotation.eulerAngles.y, 0), 1.2f);
        }
        for (int i = 0; i < upGraade.Length; i++)
        {
            upGraade[i].transform.DOLocalRotate(new Vector3(125, changeRotate[value].rotation.eulerAngles.y-90, 0), 1.2f);
        }
        if (value==2||value==0)
        {
            for (int i = 0; i < OrderObj.Length; i++)
            {
                OrderObj[i].transform.DOLocalRotate(new Vector3(OrderObj[i].transform.rotation.eulerAngles.x, changeRotate[value].rotation.eulerAngles.y - 90, 0), 1.2f);
            }
           


        }
       
    }
}
