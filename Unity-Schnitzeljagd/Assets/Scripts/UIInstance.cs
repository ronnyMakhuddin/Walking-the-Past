using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInstance : MonoBehaviour
{
    private static UIInstance myUI;

    private void Awake()
    {
        if (myUI != null && myUI != this)
        {
            Destroy(gameObject);
        }
        else
        {
            myUI = this;
        }
    }
}
