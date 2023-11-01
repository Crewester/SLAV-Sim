using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MLController : Agent
{
    public float movement;
    public float turning;
    public bool breaking;
    public GameObject RayScript;

    float FrontRayLeft;
    float FrontRayMiddle;
    float FrontRayRight;

    float RightRayForward;
    float RightRayMiddle;
    float RightRayBack;
    float LeftRayFront;
    float LeftRayMiddle;
    float LeftRayBack;
    float BackRay;

    float Speed;

    public bool motorway = false;

    public bool cameraInput;
    // Debug tools
    public bool DebugMessages = false;

    public bool Discrete;

    public override void OnActionReceived(float[] actions)
    {
        // discrete actions
        if (Discrete)
        {
            if (movement == 0)
            {
                movement = -1;
                breaking = true;
            }

            if (movement == 1)
            {
                movement = 0;
                breaking = false;
            }

            if (movement == 2)
            {
                movement = 1;
                breaking = false;
            }
            movement = actions[0];


            if (turning == 0)
            {
                turning = -1;
            }

            if (turning == 1)
            {
                turning = 0;
            }

            if (turning == 2)
            {
                turning = 1;
            }
            turning = actions[1];   
        }

        // continuous actions
        else
        {
            movement = actions[0];
            turning = actions[1];
        }
        base.OnActionReceived(actions);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (cameraInput== false)
        {
            // converting raycast values to observations

            //front
            FrontRayLeft = RayScript.GetComponent<RaycastController>().Front_Left_Distance;
            sensor.AddObservation(FrontRayLeft);

            FrontRayMiddle = RayScript.GetComponent<RaycastController>().Front_Middle_Distance;
            sensor.AddObservation(FrontRayMiddle);

            FrontRayRight = RayScript.GetComponent<RaycastController>().Front_Right_Distance;
            sensor.AddObservation(FrontRayRight);

            //Right
            RightRayForward = RayScript.GetComponent<RaycastController>().Right_Front_Distance;
            sensor.AddObservation(RightRayForward);

            RightRayMiddle = RayScript.GetComponent<RaycastController>().Right_Middle_Distance;
            sensor.AddObservation(RightRayMiddle);

            RightRayBack = RayScript.GetComponent<RaycastController>().Right_Rear_Distance;
            sensor.AddObservation(RightRayBack);

            //Left
            LeftRayFront = RayScript.GetComponent<RaycastController>().Left_Front_Distance;
            sensor.AddObservation(LeftRayFront);

            LeftRayMiddle = RayScript.GetComponent<RaycastController>().Left_Middle_Distance;
            sensor.AddObservation(LeftRayMiddle);

            LeftRayBack = RayScript.GetComponent<RaycastController>().Left_Rear_Distance;
            sensor.AddObservation(LeftRayBack);

            //back
            BackRay = RayScript.GetComponent<RaycastController>().Rear_Middle_Distance;
            sensor.AddObservation(BackRay);
        
            //Speed
            Speed = GetComponent<CarController>().Speed;
            sensor.AddObservation(Speed);
        }

        // always collects rotation
        sensor.AddObservation(transform.rotation);

        base.CollectObservations(sensor);
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }
    private void FixedUpdate()
    {
        // constant punishment
        AddReward(-0.1f);
        WallDistance();
    }

    // wrong side of road pinushment
    public void CorrectSideOfRoad()
    {
        if (Speed > 1 && motorway == false)
        {
            AddReward(0.1f);
            if (DebugMessages == true) { Debug.Log("Correct of road"); }
        }
    }

    // punishes agent for being in middle of road
    public void middleofroadPunishment()
    {
        AddReward(-0.5f);
        if (DebugMessages == true) { Debug.Log("Middle of road"); }
    }

    //speed limit check     
    public void Speeding()
    {
        AddReward(-0.5f);
        if (DebugMessages == true) { Debug.Log("Speeding"); }
    }
    
    //reward for breaking if something is too close in front of the vehivle (check ray distance) and remove forward reward
    public void BreakingCorrectly()
    {
        AddReward(0.5f);
        if (DebugMessages == true) { Debug.Log("Breaking Correctly"); }
    }
    public void UnnecessaryBreaking()
    {
        AddReward(-0.5f);
        if (DebugMessages == true) { Debug.Log("Unnecassary Breaking"); }
    }

    //forward reward
    public void forwardReward(float DistanceFromSpeedLimit, float speedlimit)
    {

     float rewardValue = (speedlimit - DistanceFromSpeedLimit)/10;
     AddReward(rewardValue);

     if (DebugMessages == true) { Debug.Log("Forward Reward = " + rewardValue); }
    }

    // distance from sides punishment
    public void WallDistance()
    {
        float[] wallDistance = { RightRayForward, RightRayMiddle, RightRayBack, LeftRayFront, LeftRayMiddle, LeftRayBack};

        for (int x = 0; x < 5; x++)
        {
            if (wallDistance[x] < 2 && wallDistance[x] >1)
            {            
                AddReward(-0.02f);

                if (DebugMessages == true)
                { 
                    Debug.Log("Close to wall"); 
                }
            }
            else if(wallDistance[x] < 1)
            {
                AddReward(-0.05f);
                if (DebugMessages == true) 
                {
                    Debug.Log("Very Close to wall"); 
                }
            }
        }
    }
    // gives a reward when the passes through a reward gate 
    public void trafficLightReward()
    {
        AddReward(10f);
    }

    //activates if car in stop zone and stopped
    public void StopZone()
    {
        AddReward(0.15f);
    }

    public void WallHit() 
    {
        AddReward(-15f); 
        if (DebugMessages == true) { Debug.Log("Hit Wall"); }
    }

    //ends the episode when vehicle is respawned
    public void endep()
    {
        EndEpisode();
    }

}
