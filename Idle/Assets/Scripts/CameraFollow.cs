using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    private void Awake()
    {
        if (!gameManager.locked)
        {
            turn = 12;
        }
        else
        {
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
        if (cameraFollow)
        {
            targetPos = human.position + offset;
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
            transform.DOMove(UpPoint.position, 2f);
             transform.DORotate(new Vector3(90, 0, 0), 2f);
            go = false;
            return;
        }
        else
        {
            cameraFollow = true;
            transform.DORotate(new Vector3(45, 0, 0), 2f);
            go = true;
        }
       
    }
}
