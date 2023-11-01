using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingScript : MonoBehaviour
{
    public GameObject Walls;

    public GameObject red;
    public GameObject amber;
    public GameObject green;

    public GameObject red1;
    public GameObject amber1;
    public GameObject green1;

    public GameObject RewardGate;

    public GameObject StopZone;

    public bool debug;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LightTime()
    {
        //on time

        Red();
        float temp1 = Random.Range(25, 50);
        yield return new WaitForSeconds(temp1);

        ChangingGreen();
        yield return new WaitForSeconds(2);

        //off time
        Green();
        float temp2 = Random.Range(45, 70);
        yield return new WaitForSeconds(temp2);

        ChangingRed();
        yield return new WaitForSeconds(5);

        StartCoroutine(LightTime());
    }

    public void Red()
    {
        // add a check if the car is in the traffic light then do not change to red
        if (debug) Debug.Log("Red");
        Walls.SetActive(true);


        green.SetActive(false);
        amber.SetActive(false);

        red.SetActive(true);

        green1.SetActive(false);
        amber1.SetActive(false);

        red1.SetActive(true);

        RewardGate.SetActive(false);

        StopZone.SetActive(true);
    }

    public void ChangingGreen()
    {
        red.SetActive(true);
        amber.SetActive(true);

        red1.SetActive(true);
        amber1.SetActive(true);

        StopZone.SetActive(false);

    }

    public void ChangingRed()
    {
        green.SetActive(false);
        amber.SetActive(true);

        green1.SetActive(false);
        amber1.SetActive(true);
    }

    public void Green()
    {

        if (debug) Debug.Log("Green");
        Walls.SetActive(false);

        red.SetActive(false);
        amber.SetActive(false);

        green.SetActive(true);

        red1.SetActive(false);
        amber1.SetActive(false);

        green1.SetActive(true);

        RewardGate.SetActive(true);

        StopZone.SetActive(false);
    }
}
