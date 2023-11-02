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
        for (int i = 0; i < message.effort.Length; i++)
        {
            efforts[i] = (float)(message.effort[i]);
        }
    
    }

    void PrintJointEfforts()
    {
        
            string temp = "";
            for (int i = 0; i < efforts.Length; i++)
            {
                if(i == efforts.Length - 1)
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

    void OnGUI()
    {
        for (int i = 0; i < efforts.Length; i++)
        {
            if (efforts[i] >= 0.03 || efforts[i] <= -0.03)
            {
                GUI.color = Color.red;
                //GUI.Box(new Rect(Screen.width/2 - 600, Screen.height/2 - 400, 2400, 800), "WARNING! HIGH EFFORT!");
                GUI.Box(new Rect(Screen.width/2, Screen.height/2,1000,1000), "WARNING! HIGH EFFORT!");

            }
        }
    }
        
    void Update()
    {
        PrintJointEfforts();
    }
}
