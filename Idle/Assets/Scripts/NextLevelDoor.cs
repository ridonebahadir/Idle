using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextLevelDoor : MonoBehaviour
{
    public Transform door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Human")
        {
            door.transform.DOScaleY(0.8f, 0.5f).SetEase(Ease.InOutExpo).OnComplete(()=> {
                door.transform.DOScaleY(1, 0.5f).SetEase(Ease.InExpo);
            });
        }
    }
   
}
