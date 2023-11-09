using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Robotics.ROSTCPConnector;
using SensorUnity = RosMessageTypes.Sensor.JointStateMsg;

public class jeffrey : MonoBehaviour
{
    TMP_Text Text;
    public string rosTopic = "nbv/joint_states";
    private ROSConnection ROS;
    private ArticulationBody[] robotJoints;
    public GameObject robot;
    int number;
    private float[] positions;
    // Start is called before the first frame update
    void Start()
    {
       Text = this.GetComponent<TMP_Text>();
       Text.text = "Position (x,y,z)";
       positions = new float[6];
       robotJoints = robot.GetComponentsInChildren<ArticulationBody>();
       ROSConnection ROS = ROSConnection.GetOrCreateInstance();
       ROS.Subscribe<SensorUnity>(rosTopic, GetJointPosition);
    }

    void GetJointPosition(SensorUnity message)
    {
        for (int i = 0; i < message.position.Length; i++)
        {
            positions[i] = (float)(message.position[i]);
        }
    
    }

    void PrintJointPositions()
    {
        
            string temp = "";
            float num;
            for (int i = 0; i < positions.Length; i++)
            {
                if(i == positions.Length - 1)
                {
                    if (positions[i] == 0)
                    {
                        temp += "0.00";
                    }
                    else{
                        num = positions[i];
                        num = Mathf.Round(num * 100.0f) * 0.01f;
                        temp += num.ToString();
                    }
                    
                }
                else
                {
                    if (positions[i] == 0)
                    {
                        temp += "0.00, ";
                    }
                    else{
                        num = positions[i];
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
        PrintJointPositions();
    }
}
