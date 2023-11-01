using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using SensorUnity = RosMessageTypes.Sensor.JointStateMsg;

public class Texter : MonoBehaviour
{
    public string rosTopic = "nbv/joint_states";
    private ROSConnection ROS;
    private ArticulationBody[] robotJoints;
    public GameObject robot;
    int number;
    private float[] efforts;
    TextMesh texting;
    public TextMesh warning;
    // Start is called before the first frame update
    void Start()
    {
        efforts = new float[6];
        texting = GetComponent<TextMesh>();
        robotJoints = robot.GetComponentsInChildren<ArticulationBody>();
        ROSConnection ROS = ROSConnection.GetOrCreateInstance();
        ROS.Subscribe<SensorUnity>(rosTopic, GetJointEffort);
    }

    // Update is called once per frame
    void GetJointEffort(SensorUnity message)
    {
        for (int i = 1; i < message.effort.Length; i++)
        {
            
            efforts[i-1] = (float)(message.effort[i]);
        }
    
    }

    void PrintJointEfforts()
    {
        while(true)
        {
            string temp = "";
            for (int i = 0; i < efforts.Length - 1; i++)
            {
                if(i == efforts.Length - 2)
                {
                    temp += efforts[i].ToString();
                }
                else
                {
                    temp += efforts[i].ToString() + ", ";
                }
            }
            texting.text = temp; //print string of array to unity
        }
        
    }
}
