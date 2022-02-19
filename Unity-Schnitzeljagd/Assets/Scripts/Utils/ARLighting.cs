using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARLighting : MonoBehaviour
{
    ARCameraManager aRCameraManager;
    Light arLight;

    private void Awake()
    {
        aRCameraManager = GameObject.FindObjectOfType<ARCameraManager>();
        arLight = GetComponent<Light>();
    }

    private void OnEnable()
    {
        aRCameraManager.frameReceived += FrameUpdated;
    }

    private void OnDisable()
    {
        aRCameraManager.frameReceived -= FrameUpdated;

    }

    void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            arLight.intensity = args.lightEstimation.averageBrightness.Value;
        }
        if (args.lightEstimation.mainLightColor.HasValue)
        {
            arLight.color = args.lightEstimation.mainLightColor.Value;
        }
        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            arLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }
        //TODO Get correct direction (nice to have)
        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            //arLight.transform.Rotate(args.lightEstimation.mainLightDirection.Value); //this causes light to spin in circle...
        }
    }
}
