using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    
    public Transform human;
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    Vector3 offset;
    public Transform[] transforms;
    bool cameraFollow = true;
    public int turn;
    Vector3 targetPos;
    Vector3 backPos;
    private void Awake()
    {
        turn = PlayerPrefs.GetInt("Turn", 0);
    }
    private void Start()
    {
        offset =  transform.position- human.transform.position;

        if (turn>0)
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
        if (turn<transforms.Length-1)
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
}
