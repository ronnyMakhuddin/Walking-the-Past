using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject volume;
    bool isRetro = false;

    public void DesignChange()
    {
        volume.SetActive(!isRetro);
        isRetro = !isRetro;
    }
}
