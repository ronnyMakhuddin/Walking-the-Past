using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Debugging to display the cam position and pile position on the screen.
/// </summary>
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
