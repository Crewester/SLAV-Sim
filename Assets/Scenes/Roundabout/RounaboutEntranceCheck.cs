using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RounaboutEntranceCheck : MonoBehaviour
{
    public GameObject roundabout;
    public bool active;
    string name;
    // this script exists to monitor if the car passes triggers on the entrance for each of the different roundabout entrances 

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && gameObject.name == "BoxEnt1")
        {
            roundabout.GetComponent<RoundaboutScript>().Box1 = true;
        }
        if (other.gameObject.layer == 7 && gameObject.name == "BoxEnt2")
        {
            roundabout.GetComponent<RoundaboutScript>().Box2 = true;
        }
        if (other.gameObject.layer == 7 && gameObject.name == "BoxEnt3")
        {
            roundabout.GetComponent<RoundaboutScript>().Box3 = true;
        }
        if (other.gameObject.layer == 7 && gameObject.name == "BoxEnt4")
        {
            roundabout.GetComponent<RoundaboutScript>().Box4 = true;
        }
        active = false;
    }
}
