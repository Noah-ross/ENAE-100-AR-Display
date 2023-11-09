using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Robotics.ROSTCPConnector;
using SensorUnity = RosMessageTypes.Sensor.JointStateMsg;

public class jeffrey3 : MonoBehaviour
{
    TMP_Text Text;
    public string rosTopic = "nbv/joint_states";
    private ROSConnection ROS;
    private ArticulationBody[] robotJoints;
    public GameObject robot;
    int number;
    private float[] efforts;
    TextMesh texting;
    public GameObject warning;
    TextMesh warningText;
    // Start is called before the first frame update
    void Start()
    {
        Text = this.GetComponent<TMP_Text>(); 
        Text.text = "Effort";
        efforts = new float[6];
        robotJoints = robot.GetComponentsInChildren<ArticulationBody>();
        ROSConnection ROS = ROSConnection.GetOrCreateInstance();
        ROS.Subscribe<SensorUnity>(rosTopic, GetJointEffort);
        warningText = warning.GetComponent<TextMesh>();
        warningText.text = "WARNING! HIGH EFFORT!";
        warning.SetActive(false);
      
    }

    void GetJointEffort(SensorUnity message)
    {
        for (int i = 0; i < message.effort.Length; i++)
        {
            efforts[i] = (float)(message.effort[i]);
            //warning message
            if (efforts[i] >= 0.03 || efforts[i] <= -0.03)
            {
                warning.SetActive(true);
            }
            else
            {
                warning.SetActive(false);
            }
        }
    
    }

    void PrintJointEfforts()
    {
        
            string temp = "";
            float num;
            for (int i = 0; i < efforts.Length; i++)
            {
                if(i == efforts.Length - 1)
                {
                    if (efforts[i] == 0)
                    {
                        temp += "0.00";
                    }
                    else{
                        num = efforts[i];
                        num = Mathf.Round(num * 100.0f) * 0.01f;
                        temp += num.ToString();
                    }
                    
                }
                else
                {
                    if (efforts[i] == 0)
                    {
                        temp += "0.00, ";
                    }
                    else{
                        num = efforts[i];
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
        PrintJointEfforts();
    }
}
