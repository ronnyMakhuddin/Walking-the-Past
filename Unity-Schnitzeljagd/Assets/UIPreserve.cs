using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPreserve : MonoBehaviour
{
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
}
