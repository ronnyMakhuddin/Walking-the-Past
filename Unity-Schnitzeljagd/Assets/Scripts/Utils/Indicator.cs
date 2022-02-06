using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField]
    float speed = 20f;

    

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * speed, 0f));
    }
}
