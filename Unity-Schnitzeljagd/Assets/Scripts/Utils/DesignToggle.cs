using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject volume;
    bool isRetro = true;

    public void DesignChange()
    {
        // by disabling the volume object, the postprocessing is deactivated as well
        volume.SetActive(!isRetro);
        isRetro = !isRetro;
    }
}
