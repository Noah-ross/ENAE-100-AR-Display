using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class jeff : MonoBehaviour
{
    TMP_Text Text;
    // Start is called before the first frame update
    void Start()
    {
       Text = this.GetComponent<TMP_Text>();
        Text.text = "Hello World";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
