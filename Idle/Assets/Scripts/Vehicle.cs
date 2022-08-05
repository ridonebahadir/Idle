using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public HumanMove humanMove;
    public HumanCollider humanCollider;
    public int a = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Human")
        {
            if (humanCollider.human)
            {
                if (a < humanMove.runClip.Length - 1)
                {
                    a++;
                    humanMove.VehicleChange(a);

                }
                else
                {
                    a = 0;
                    humanMove.VehicleChange(a);
                }
            }
            
        }
    }
}
