using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using SensorUnity = RosMessageTypes.Sensor.JointStateMsg;


public class Transfer : MonoBehaviour
{
    // Start is called before the first frame update
    public string rosTopic = "nbv/joint_states";
    private ROSConnection ROS;
    private ArticulationBody[] robotJoints;
    public GameObject robot;

    void Start()
    {
        robotJoints = robot.GetComponentsInChildren<ArticulationBody>();
        ROSConnection ROS = ROSConnection.GetOrCreateInstance();
        ROS.Subscribe<SensorUnity>(rosTopic, GetJointPositions);
    }


    private void GetJointPositions(SensorUnity message)
    {
        for (int i = 2; i < message.name.Length + 1; i++)
        {
            var joint1XDrive = robotJoints[i].xDrive;
            joint1XDrive.target = (float)(message.position[i-1]) * Mathf.Rad2Deg;
            robotJoints[i].xDrive = joint1XDrive;
        }
    }

 
    public void UnSub()
    {
        ROS.Unsubscribe(rosTopic);
    }
}
