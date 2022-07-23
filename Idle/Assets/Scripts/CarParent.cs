using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParent : MonoBehaviour
{
    public float zAxis = -17;
    public float xAxis = -90;
    public int a;
    public int gidenAraba;
    public float StartzAxis;
    public float StartxAxis;
    private void Start()
    {
        StartzAxis = zAxis;
        StartxAxis = xAxis;
    }
}
