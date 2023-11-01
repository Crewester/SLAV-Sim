using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundaboutController : MonoBehaviour
{
    // this script will control the roundabout functions, turning off gates and picking random gates to open

    private int Random;

    public GameObject EntGate1;
    public GameObject EntGate2;
    public GameObject EntGate3;
    public GameObject EntGate4;

    public GameObject ExitGate1;
    public GameObject ExitGate2;
    public GameObject ExitGate3;
    public GameObject ExitGate4;

    public GameObject Entranceway1;
    public GameObject Entranceway2;
    public GameObject Entranceway3;
    public GameObject Entranceway4;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // triggers when the car enters the roundabout
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            // pick a random number and open that gate    
            Debug.Log("Car Here");
        }
    }

    // triggers when the car leaves the roundabout
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {

        }
    }

}
