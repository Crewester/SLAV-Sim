using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleOfRoadChecker : MonoBehaviour
{
    public GameObject MLObject;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer==6)
        {
            MLObject.GetComponent<MLController>().middleofroadPunishment();
        }
    }
}
