using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamPos : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform sceneOrigin;

    // Update is called once per frame
    void Update()
    {
        text.text = "origin pos: " + transform.parent.position + "\nPile at: " + sceneOrigin.transform.position;
    }
}
