using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public GameObject money;
    int count = 10;
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(money, transform);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
