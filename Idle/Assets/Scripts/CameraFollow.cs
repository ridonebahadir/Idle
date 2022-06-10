using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform human;
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    Vector3 offset;

    private void Start()
    {
        offset =  transform.position- human.transform.position;
    }
    void Update()
    {
        Vector3 targetPos = human.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
