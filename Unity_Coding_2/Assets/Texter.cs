using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texter : MonoBehaviour
{
    int number;
    TextMesh texting;
    // Start is called before the first frame update
    void Start()
    {
        texting = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        number = number + 1;
        texting.text = number.ToString();
    }
}
