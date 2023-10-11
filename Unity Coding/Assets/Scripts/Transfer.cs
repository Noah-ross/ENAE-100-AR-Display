using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using SensorUnity = RosMessageTypes.Sensor.JointStateMsg;
using testMessage = RosMessageTypes.Std.StringMsg;


public class Transfer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string rosTopic = "/nbv/joint_states";
    [SerializeField] private string newTopic = "balls";
    [SerializeField] private ROSConnection ROS;
    [SerializeField] private ArticulationBody[] robotJoints = new ArticulationBody[9];
    void Start()
    {
        ROSConnection ROS = ROSConnection.GetOrCreateInstance();
        ROS.Subscribe<testMessage>(newTopic, test);
    }

    private void test(testMessage message)
    {
        print(message.data);
    }

    private void GetJointPositions(SensorUnity sensorMsg)
    {
        StartCoroutine(SetJointValues(sensorMsg));
    }
    IEnumerator SetJointValues(SensorUnity message)
    {
        print(message.position[0]);
 
        yield return new WaitForSeconds(0.5f);
    }
 
    public void UnSub()
    {
        ROS.Unsubscribe(rosTopic);
    }
}
