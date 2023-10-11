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
    private ArticulationBody[] robotJoints = new ArticulationBody[6];
    public GameObject robot;

    void Start()
    {
        robotJoints = robot.GetComponentsInChildren<ArticulationBody>();git
        ROSConnection ROS = ROSConnection.GetOrCreateInstance();
        ROS.Subscribe<SensorUnity>(rosTopic, GetJointPositions);
    }


    private void GetJointPositions(SensorUnity message)
    {
        for (int i = 0; i < message.name.Length; i++)
        {
            var joint1XDrive = robotJoints[i].xDrive;
            joint1XDrive.target = (float)(message.position[i]) * Mathf.Rad2Deg;
            robotJoints[i].xDrive = joint1XDrive;
        }
    }

 
    public void UnSub()
    {
        ROS.Unsubscribe(rosTopic);
    }
}
