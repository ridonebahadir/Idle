using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform human;
    Vector3 offset;

    private void Start()
    {
        offset =  transform.position- human.transform.position;
    }
    void Update()
    {
        transform.position = human.transform.position+ offset;
    }
}
