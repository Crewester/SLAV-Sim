using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    // all positions of raycast origins and angles
    //Front
    [SerializeField] Transform FrontCenter;
    [SerializeField] Transform Frontmiddle;
    [SerializeField] Transform Front_Left;
    [SerializeField] Transform Front_Right;

    //Right
    [SerializeField] Transform Right_Center;
    [SerializeField] Transform Right_Front;
    [SerializeField] Transform Right_Middle;
    [SerializeField] Transform Right_Rear;

    //Left
    [SerializeField] Transform Left_Center;
    [SerializeField] Transform Left_Front;
    [SerializeField] Transform Left_Middle;
    [SerializeField] Transform Left_Rear;
    LayerMask LayerMask = 3;

    // Rear
    [SerializeField] Transform Rear_Center;
    [SerializeField] Transform Rear_Middle;

    // front raycasts
    RaycastHit Front_Middle_Hit;
    RaycastHit Front_Left_Hit;
    RaycastHit Front_Right_Hit;

    //Right
    RaycastHit Right_Front_Hit;
    RaycastHit Right_Middle_Hit;
    RaycastHit Right_Rear_Hit;

    //Left
    RaycastHit Left_Front_Hit;
    RaycastHit Left_Middle_Hit;
    RaycastHit Left_Rear_Hit;

    //Rear
    RaycastHit Rear_Hit;

    public GameObject CarControllerGameobject;

    //front
    public float Front_Middle_Distance;
    public float Front_Left_Distance;
    public float Front_Right_Distance;
    //Right
    public float Right_Front_Distance;
    public float Right_Middle_Distance;
    public float Right_Rear_Distance;
    //Left
    public float Left_Front_Distance;
    public float Left_Middle_Distance;
    public float Left_Rear_Distance;
    //Rear
    public float Rear_Middle_Distance;

    // debug tools
    public bool DrawRays;
    public bool DebugMessages = false;

    public bool ShowDistance;

    // Update is called once per frame
    void Update()
    {
        DrawFront();

        DrawLeft();

        DrawRight();

        DrawRear();

        GetDistances();

        //MiddleOfRoadPunishment();
    }

    private void DrawFront() // draws all front rays
    {
        //front middle
        if(Physics.Raycast(FrontCenter.transform.position, ((FrontCenter.transform.position - Frontmiddle.transform.position) * -1), out Front_Middle_Hit))
        {

        }
        // Front Left
        if (Physics.Raycast(FrontCenter.transform.position, ((FrontCenter.transform.position - Front_Left.transform.position) * -1), out Front_Left_Hit))
        {

        }

        // Front Right
        if (Physics.Raycast(FrontCenter.transform.position, ((FrontCenter.transform.position - Front_Right.transform.position) * -1), out Front_Right_Hit))
        {

        }
        // if draw rays true (Draws rays)
        if (DrawRays == true)
        {
            Debug.DrawRay(FrontCenter.transform.position, ((FrontCenter.transform.position - Frontmiddle.transform.position) * -1) * Front_Middle_Distance, Color.red);
            Debug.DrawRay(FrontCenter.transform.position, ((FrontCenter.transform.position - Front_Left.transform.position) * -1)* Front_Left_Distance, Color.red);
            Debug.DrawRay(FrontCenter.transform.position, ((FrontCenter.transform.position - Front_Right.transform.position) * -1)*Front_Right_Distance, Color.red);
        }

    }
    private void DrawRight()
    {
        // Right Middle
        if (Physics.Raycast(Right_Center.transform.position, ((Right_Center.transform.position - Right_Middle.transform.position) * -1), out Right_Middle_Hit))
        {
            if(Right_Middle_Hit.transform.gameObject.layer == 6)
            {
                CarControllerGameobject.GetComponent<MLController>().CorrectSideOfRoad();
                if (DebugMessages == true) { Debug.Log("Correct side of road"); }
            }
        }

        // Right Front
        if (Physics.Raycast(Right_Center.transform.position, ((Right_Center.transform.position - Right_Front.transform.position) * -1), out Right_Front_Hit))
        {

        }
        // Right Rear
        if (Physics.Raycast(Right_Center.transform.position, ((Right_Center.transform.position - Right_Rear.transform.position) * -1), out Right_Rear_Hit))
        {

        }

        if(DrawRays == true)
        {
            Debug.DrawRay(Right_Center.transform.position, ((Right_Center.transform.position - Right_Middle.transform.position) * -1) * Right_Middle_Distance, Color.green);
            Debug.DrawRay(Right_Center.transform.position, ((Right_Center.transform.position - Right_Front.transform.position) * -1) * Right_Front_Distance, Color.green);
            Debug.DrawRay(Right_Center.transform.position, ((Right_Center.transform.position - Right_Rear.transform.position) * -1) * Right_Rear_Distance, Color.green);
            float Front_Middle_Distance = Front_Middle_Hit.distance;
        }
    }

    private void DrawLeft()
    {
        // Left Middle
        if (Physics.Raycast(Left_Center.transform.position, ((Left_Center.transform.position - Left_Middle.transform.position) * -1), out Left_Middle_Hit))
        {

        }

        // Left Front
        if (Physics.Raycast(Left_Center.transform.position, ((Left_Center.transform.position - Left_Front.transform.position) * -1), out Left_Front_Hit))
        {

        }
        // Left Rear
        if (Physics.Raycast(Left_Center.transform.position, ((Left_Center.transform.position - Left_Rear.transform.position) * -1), out Left_Rear_Hit))
        {

        }
        if (DrawRays == true)
        {
            Debug.DrawRay(Left_Center.transform.position, ((Left_Center.transform.position - Left_Middle.transform.position) * -1) * Left_Middle_Distance, Color.blue);
            Debug.DrawRay(Left_Center.transform.position, ((Left_Center.transform.position - Left_Front.transform.position) * -1) * Left_Front_Distance, Color.blue);
            Debug.DrawRay(Left_Center.transform.position, ((Left_Center.transform.position - Left_Rear.transform.position) * -1) * Left_Rear_Distance, Color.blue);
        }

    }

    private void DrawRear()
    {
        // Rear Middle
        if (Physics.Raycast(Rear_Center.transform.position, ((Rear_Center.transform.position - Rear_Middle.transform.position) * -1), out Rear_Hit))
        {

        }
        if (DrawRays == true)
        {
            Debug.DrawRay(Rear_Center.transform.position, ((Rear_Center.transform.position - Rear_Middle.transform.position) * -1) * Rear_Middle_Distance, Color.white);
        }
    }
    public void GetDistances()
    {
        //front
         Front_Middle_Distance = Front_Middle_Hit.distance;
         Front_Left_Distance = Front_Left_Hit.distance;
         Front_Right_Distance = Front_Right_Hit.distance;
        //Right
         Right_Front_Distance = Right_Front_Hit.distance;
         Right_Middle_Distance = Right_Middle_Hit.distance;
         Right_Rear_Distance = Right_Rear_Hit.distance;
        //Left
         Left_Front_Distance = Left_Front_Hit.distance;
         Left_Middle_Distance = Left_Middle_Hit.distance;
         Left_Rear_Distance = Left_Rear_Hit.distance;

        //Rear
         Rear_Middle_Distance = Rear_Hit.distance;

        if(ShowDistance == true)
        {
            //Front
            Debug.Log("Front Middle = " + Front_Middle_Hit.distance);
            Debug.Log("Front Left = " + Front_Left_Hit.distance);
            Debug.Log("Front Right = " + Front_Right_Hit.distance);
            //Right
            Debug.Log("Right Front = " + Right_Front_Hit.distance);
            Debug.Log("Right Middle = " + Right_Middle_Hit.distance);
            Debug.Log("Right Rear = " + Right_Rear_Hit.distance);
            //Left
            Debug.Log("Left Front = " + Left_Front_Hit.distance);
            Debug.Log("Left Middle = " + Left_Middle_Hit.distance);
            Debug.Log("Left Rear = " + Left_Rear_Hit.distance);
            //Back
            Debug.Log("Rear Rear = " + Rear_Hit.distance);

        }

    }
}
