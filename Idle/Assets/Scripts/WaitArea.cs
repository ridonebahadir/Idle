using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitArea : MonoBehaviour
{
    public bool isEmpty = true;
    BoxCollider boxColliderWaitPoint;
    private void Start()
    {
        isEmpty = true;
        boxColliderWaitPoint =transform.GetChild(0).GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Human")
        {
            isEmpty = false;
            boxColliderWaitPoint.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Human")
        {
            isEmpty = true;
            boxColliderWaitPoint.enabled = true;
        }
    }
}
