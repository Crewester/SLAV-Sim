using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Transactions;

public class TestScript : MonoBehaviour
{
    List<string> CrashList = new List<string>();
    List<string> CrashListSepearated = new List<string>();
    public bool test;
    public float testLength;
    public string Savelocation;
    public float timescale;
    private bool notrun;

    public GameObject car;

    // Start is called before the first frame update
    void Start()
    {
        notrun = false;
        Savelocation = Savelocation + ".csv";
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > testLength & test)
        {
            if(notrun == false)
            {
                DisplayTest();
            }
        }
    }
    
    //called from car controller script
    public void Test(Vector3 crashpoint)
    {
        // is called when the car crashes only when test mode is active
        
        if (test & Savelocation !=null)
        {
            // get crash location and write to file
            try
            {
                using StreamWriter file = new StreamWriter(@Savelocation,true);
                file.WriteLine(crashpoint + ",");
            }
            catch
            {
                throw new ApplicationException("There was an error writing to the file");
            }
        }
    }

    public void DisplayTest() 
    {
        //this will read the file and place objects where the cordinates are
        try
        {
            using (StreamReader sr = new StreamReader(Savelocation))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Replace("(", String.Empty);
                    line = line.Replace(")", String.Empty);

                    CrashList.Add(line);
                }
            }
        }

        //make file into list
        catch (Exception ex)
        {
            throw new ApplicationException("there was a error when reading the file", ex);
        }
        for (int x = 0; x < CrashList.Count; x++)
        {
            //split the data 

            string[] cleanlist = CrashList[x].Split(',');

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cube.transform.localScale = new Vector3(5, 5, 5);
            cube.transform.position = new Vector3(float.Parse(cleanlist[0]), float.Parse(cleanlist[1]), float.Parse(cleanlist[2]));

            cube.GetComponent<Renderer>().material.color = new Color(255, 0, 0);

        }
        notrun = true;
        //pause and deactivate car
        
        //find car

        // deactivate
        car.SetActive(false);
        //pause
        Time.timeScale = 0;
    }
}
