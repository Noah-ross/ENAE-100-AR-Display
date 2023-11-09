using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Robotics.ROSTCPConnector;
using SensorUnity = RosMessageTypes.Sensor.JointStateMsg;

public class jeffrey2 : MonoBehaviour
{
    TMP_Text Text;
    public string rosTopic = "nbv/joint_states";
    private ROSConnection ROS;
    private ArticulationBody[] robotJoints;
    public GameObject robot;
    int number;
    private float[] velocities;
    // Start is called before the first frame update
    void Start()
    {
        Text = this.GetComponent<TMP_Text>(); 
        Text.text = "Velocity";
        velocities = new float[6];
        robotJoints = robot.GetComponentsInChildren<ArticulationBody>();
        ROSConnection ROS = ROSConnection.GetOrCreateInstance();
        ROS.Subscribe<SensorUnity>(rosTopic, GetJointVelocity);
      
    }

    void GetJointVelocity(SensorUnity message)
    {
        for (int i = 0; i < message.velocity.Length; i++)
        {
            velocities[i] = (float)(message.velocity[i]);
        }
    }
    

    void PrintJointVelocities()
    {
        
            string temp = "";
            float num;
            for (int i = 0; i < velocities.Length; i++)
            {
                if(i == velocities.Length - 1)
                {
                    if (velocities[i] == 0)
                    {
                        temp += "0.00";
                    }
                    else{
                        num = velocities[i];
                        num = Mathf.Round(num * 100.0f) * 0.01f;
                        temp += num.ToString();
                    }
                    
                }
                else
                {
                    if (velocities[i] == 0)
                    {
                        temp += "0.00, ";
                    }
                    else{
                        num = velocities[i];
                        num = Mathf.Round(num * 100.0f) * 0.01f;
                        temp += num.ToString()+ ", ";
                    }
                }
            }
            Text.text = temp; //print string of array to unity
    }
    // Update is called once per frame
    void Update()
    {
        PrintJointVelocities();
    }
}
