using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    float rot = 0;
    float current_rot = 0;
    Camera camer;
    public bool twist;
    // Start is called before the first frame update
    void Start()
    {
        camer = this.GetComponent<Camera>();
        current_rot = camer.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(twist == true)
        {
            camer.transform.Rotate(0f, 90f, 0f);
            twist = false;
        }
    }
}
