using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Recorder;
using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;

public class CarController : MonoBehaviour
{
    // Inputs
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    float horizontalInput;
    float verticalInput;

    //Breaking and Motor control
    public float BreakForce;
    private bool isBreaking;

    public float motorforce;
    [SerializeField] private WheelCollider FL;
    [SerializeField] private WheelCollider FR;
    [SerializeField] private WheelCollider RL;
    [SerializeField] private WheelCollider RR;

    [SerializeField] private Transform FLTransform;
    [SerializeField] private Transform FRTransform;
    [SerializeField] private Transform RLTransform;
    [SerializeField] private Transform RRTransform;

    // steering 
    [SerializeField] private float maxSteeringAngle;
    private float currentsteeringangle;
    // Breaklights
    public GameObject BreakLeft;
    public GameObject BreakRight;

    public float Speed;

    public float speedLimit;

    public bool SelfDrive;

    private Transform RespawnPoint; 

    public string TrackName = "BasicLoop";

    public List<Transform> children;

    public GameObject RaycastGameObject;

    public float StopbreakingDistance;
    GameObject Tracknametemp;

    // enable debugMessages
    public bool DebugMessages = false;

    void Start()
    {
        //Ignore collisions in the middle lane
        Physics.IgnoreLayerCollision(6, 6);

        string ObjectName = transform.name;
        string Objectnumberstring = Regex.Replace(ObjectName, "[^.0-9]", String.Empty);

        int ObjectNumber = int.Parse(Objectnumberstring);

        Tracknametemp = GameObject.Find(TrackName + " " + '(' + ObjectNumber + ')');
        
        if (String.IsNullOrEmpty(Objectnumberstring))
        {
            Tracknametemp = GameObject.Find(TrackName);
        }
        else
        {
            Transform currenttrack = Tracknametemp.transform;
            RespawnPoint = currenttrack.Find("RespawnPoints");
        }

        foreach (Transform child in RespawnPoint.transform)
        {
            children.Add(child.transform);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInputUser();
        Motor();
        Steering();
        UpdateWheels();
        GetSpeed();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == 3) || (collision.gameObject.layer == 7))
        {
            Respawn();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Traffic Light"))
        {
            other.gameObject.SetActive(false);
            GetComponent<MLController>().trafficLightReward();
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 2 && Speed <= 0.1)
        {
            GetComponent<MLController>().StopZone();
            //Debug.Log("In stop Zone");
        }
    }

    private void GetInputUser()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void Motor()
    {
        // self drive off
        if(SelfDrive == false)
        {
            FL.motorTorque = verticalInput * motorforce;
            FR.motorTorque = verticalInput * motorforce;
            ApplyBreaking();
        }
        // self drive on
        else
        {
            FL.motorTorque = GetComponent<MLController>().movement * motorforce;
            FR.motorTorque = GetComponent<MLController>().movement * motorforce;


            if(Speed > 0 && Speed < speedLimit)
            {
                //how close the speed is to the speed limit
                float DistanceFromSpeedLimit= Mathf.Abs(speedLimit - Speed);

                //sends the distance from speed limit value to the MLController
                    GetComponent<MLController>().forwardReward(DistanceFromSpeedLimit, speedLimit);
                
            }
            ApplyBreaking();
        }
    }

    private void ApplyBreaking()
    {
        //self drive off
        if(SelfDrive == false)
        {
            if (isBreaking == true)
            {
                FL.brakeTorque = BreakForce;
                FR.brakeTorque = BreakForce;
                RL.brakeTorque = BreakForce;
                RR.brakeTorque = BreakForce;
                BreakLeft.SetActive(true);
                BreakRight.SetActive(true);
            }

            else
            {
                FL.brakeTorque = 0;
                FR.brakeTorque = 0;
                RL.brakeTorque = 0;
                RR.brakeTorque = 0;

                // sets breaklights to inactive
                BreakLeft.SetActive(false);
                BreakRight.SetActive(false);
            }
        }
        //self drive on
        else
        {
            if (GetComponent<MLController>().breaking == true && Speed > 0.2)
            {
                FL.brakeTorque = BreakForce;
                FR.brakeTorque = BreakForce;
                RL.brakeTorque = BreakForce;
                RR.brakeTorque = BreakForce;
                BreakLeft.SetActive(true);
                BreakRight.SetActive(true);
                
                if (RaycastGameObject.GetComponent<RaycastController>().Front_Middle_Distance < 4.5 && RaycastGameObject.GetComponent<RaycastController>().Front_Middle_Distance > 1 &&  Speed > 1)
                {
                    GetComponent<MLController>().BreakingCorrectly();
                }
                else if(RaycastGameObject.GetComponent<RaycastController>().Front_Middle_Distance > StopbreakingDistance && Speed < 5)
                {
                    GetComponent<MLController>().UnnecessaryBreaking();
                }
            }
            else if (Speed < 0.2 && GetComponent<MLController>().breaking == true)
            {
                FL.brakeTorque = 0;
                FR.brakeTorque = 0;
                RL.brakeTorque = 0;
                RR.brakeTorque = 0;

                // sets breaklights to inactive
                BreakLeft.SetActive(false);
                BreakRight.SetActive(false);

                FL.motorTorque = GetComponent<MLController>().movement * motorforce;
                FR.motorTorque = GetComponent<MLController>().movement * motorforce;
                if (DebugMessages == true) { Debug.Log("Reversing"); }
            }

            else
            {
                FL.brakeTorque = 0;
                FR.brakeTorque = 0;
                RL.brakeTorque = 0;
                RR.brakeTorque = 0;

                // sets breaklights to inactive
                BreakLeft.SetActive(false);
                BreakRight.SetActive(false);
            }
        }
    }

    private void Steering()
    {
        // self drive off
        if (SelfDrive == false)
        {
            currentsteeringangle = maxSteeringAngle * horizontalInput;
            FL.steerAngle = currentsteeringangle;
            FR.steerAngle = currentsteeringangle;
        }
        //self drive on
        else
        {
            currentsteeringangle = maxSteeringAngle * GetComponent<MLController>().turning;
            FL.steerAngle = currentsteeringangle;
            FR.steerAngle = currentsteeringangle;
        }
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(FL, FLTransform);
        UpdateSingleWheel(FR, FRTransform);
        UpdateSingleWheel(RL, RLTransform);
        UpdateSingleWheel(RR, RRTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos ;
        wheelTransform.rotation = rot;
    }

    public void GetSpeed()
    {
        Speed = GetComponent<Rigidbody>().velocity.magnitude;
    }

    public void Respawn()
    {
        // activates the TestScript if test is active
        GameObject.Find("Terrain").GetComponent<TestScript>().Test(transform.position);

        // respawn logic
        int CurrentRespwanPoint = UnityEngine.Random.Range(0, children.Count);

        Transform temp = children[CurrentRespwanPoint];

        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

        transform.position =  temp.transform.position;
        transform.rotation = temp.transform.rotation;

        GetComponent<MLController>().WallHit();
        GetComponent<MLController>().endep();
    }



}