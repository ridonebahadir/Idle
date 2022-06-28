using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    Transform door;
    private void Start()
    {
        door = transform.GetChild(0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Kamyon")
        {
            door.transform.DOScaleY(0, 0.5f).SetEase(Ease.InOutExpo);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Kamyon")
        {
            door.transform.DOScaleY(1, 0.5f).SetEase(Ease.InExpo);
        }
    }
}
