using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public HumanMove humanMove;
    public Transform human;
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    Vector3 offset;
    public Transform[] transforms;
    bool cameraFollow = true;
    public int turn;
    Vector3 targetPos;
    private void Start()
    {
        offset =  transform.position- human.transform.position;
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
        if (turn<transforms.Length-1)
        {
            humanMove.enabled = false;
            for (int i = 0; i <= turn; i++)
            {
                transforms[turn].gameObject.SetActive(true);
            }
            cameraFollow = false;
            transform.DOMove(new Vector3(transforms[turn].position.x, transform.position.y, transforms[turn].position.z - 20), 2f).OnComplete(() => Invoke("ComeBack", 2f));
        }
       
       
       
    }
    void ComeBack()
    {
        
        transform.DOMove(targetPos, 2f).OnComplete(() => { cameraFollow = true; turn++; humanMove.enabled = true; });;
        

    }
}
