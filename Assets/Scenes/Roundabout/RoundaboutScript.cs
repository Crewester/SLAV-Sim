using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoundaboutScript : MonoBehaviour
{
    //get all the different walls
    public GameObject Entrance1;
    public GameObject Entrance2;
    public GameObject Entrance3;
    public GameObject Entrance4;

    public GameObject Exit1;
    public GameObject Exit2;
    public GameObject Exit3;
    public GameObject Exit4;

    public GameObject Middle1;
    public GameObject Middle2;
    public GameObject Middle3;
    public GameObject Middle4;

    public GameObject BoxEnt1;
    public GameObject BoxEnt2;
    public GameObject BoxEnt3;
    public GameObject BoxEnt4;

    public bool Box1;
    public bool Box2;
    public bool Box3;
    public bool Box4;

    public int entrancesInUse;
    // the roundabout mode (which walls are active)
    public int Mode;

    public bool GateMessages;
    // Start is called before the first frame update
    void Start()
    {
         Mode = 0;
        entrancesInUse = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkgates();
        switch (Mode)
        {
            //close all (reset)
            case 0:
                Entrance1.SetActive(true);
                Entrance2.SetActive(true);
                Entrance3.SetActive(true);
                Entrance4.SetActive(true);

                Exit1.SetActive(true);
                Exit2.SetActive(true);
                Exit3.SetActive(true);
                Exit4.SetActive(true);

                Middle1.SetActive(true);
                Middle2.SetActive(true);
                Middle3.SetActive(true);
                Middle4.SetActive(true);

                BoxEnt1.SetActive(true);
                BoxEnt2.SetActive(true);
                BoxEnt3.SetActive(true);
                BoxEnt4.SetActive(true);

                if(GateMessages == true){Debug.Log("Reset"); }
                
                break;

            //entrance 1 open
            // entrance 1 exit 2
            case 1:
                Entrance1.SetActive(false);

                Exit2.SetActive(false);

                break;
            //entrance 1 exit 3
            case 2:
                Entrance1.SetActive(false);

                Exit3.SetActive(false);

                Middle2.SetActive(false);

                break;
            //entrance 1 exit 4
            case 3:
                Entrance1.SetActive(false);

                Exit4.SetActive(false);


                Middle2.SetActive(false);
                Middle3.SetActive(false);

                break;

            //entrance 2 open
            //exit 1
            case 4:
                Entrance2.SetActive(false);

                Exit1.SetActive(false);

                Middle3.SetActive(false);
                Middle4.SetActive(false);
                break;
            //exit 3
            case 5:
                Entrance2.SetActive(false);

                Exit3.SetActive(false);

                break;
            //exit 4
            case 6:
                Entrance2.SetActive(false);

                Exit4.SetActive(false);

                Middle3.SetActive(false);

                break;

            //entrance 3 open
            //exit 1
            case 7:
                Entrance3.SetActive(false);

                Exit1.SetActive(false);

                Middle4.SetActive(false);
                break;
            //exit 2
            case 8:

                Entrance3.SetActive(false);

                Exit2.SetActive(false);


                Middle1.SetActive(false);

                Middle4.SetActive(false);
                break;
            //exit 4
            case 9:

                Entrance3.SetActive(false);

                Exit4.SetActive(false);


                break;

            //entrance 4 open
            //exit 1
            case 10:
                Entrance4.SetActive(false);

                Exit1.SetActive(false);

                break;
            //exit 2
            case 11:
                Entrance4.SetActive(false);

                Exit2.SetActive(false);

                Middle1.SetActive(false);

                break;
            //exit 3
            case 12:
                Entrance4.SetActive(false);

                Exit3.SetActive(false);

                Middle1.SetActive(false);
                Middle2.SetActive(false);
                break;
        }

        //opens a random exit based on car position
    }


    public void checkgates()
    {
        if(Box1 == true)
        {
            Box1 = false;
            BoxEnt1.SetActive(false);
            Mode = random(1);
            if (GateMessages == true) {Debug.Log("Car at Entrance 1"); }
            
            entrancesInUse++;
        }

        if (Box2 == true)
        {
            Box2 = false;
            BoxEnt2.SetActive(false);
            Mode = random(2);
            if (GateMessages == true) {Debug.Log("Car at Entrance 2"); }
            
            entrancesInUse++;
        }

        if (Box3 == true)
        {
            Box3 = false;
            BoxEnt3.SetActive(false);
            Mode = random(3);
            if (GateMessages == true) {Debug.Log("Car at Entrance 3"); }
            
            entrancesInUse++;
        }


        if (Box4 == true)
        {
            Box4 = false;
            BoxEnt4.SetActive(false);
            Mode = random(4);
            if (GateMessages == true) {Debug.Log("Car at Entrance 4"); }
            
            entrancesInUse++;
        }
        if (GateMessages == true) {Debug.Log("Entrances i use " + entrancesInUse); }
        

        if(entrancesInUse > 1 && Mode !=0)
        {
            Mode = 0;
            entrancesInUse = 0;
        }

    }
    public int random(int ent)
    {
        int number = 0;
        if (ent == 1)
        {
            //entrance 1 open
            number = Random.Range(1, 4);
        }
        if (ent == 2)
        {
            //entrance 2
             number = Random.Range(4, 7);
        }
        if (ent == 3)
        {
            //entrance 3
             number = Random.Range(7, 10);
        }
        if (ent == 4)
        {
            //entrance 4
            number = Random.Range(10, 13);
        }
        return number;
    }


}


