using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    public LocativeGPS gps;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>(); 

    }

    // Update is called once per frame
    void Update()
    {
        text.text = gps.latitude + "\n" + gps.longitude;
    }
}