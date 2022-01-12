using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.XR.ARFoundation;

public class GPSPositioningCam : MonoBehaviour
{
    [Header("Player height")]
    public float bodyHeight = 1.7f;
    double latitude_cam = 0;
    double longitude_cam = 0;
    double altitude = 0;

    public static GPSPositioningCam Instance { set; get; }

    Gyroscope gyroscope;
    Quaternion rotation;

    bool isCam = false;
    bool isEditor = true;
    private void Awake()
    {
        isEditor = Application.isEditor;
        if (!isEditor)
        {
            isCam = GetComponent<ARSessionOrigin>() != null ? true : false;

            if (isCam)
            {
                latitude_cam = Input.location.lastData.latitude;
                longitude_cam = Input.location.lastData.longitude;
                altitude = bodyHeight;

                gyroscope = Input.gyro;
                gyroscope.enabled = true;
                rotation = new Quaternion(0, 0, 1, 0);

                transform.position = new Vector3(0, bodyHeight, 0);

                Instance = this;
            }
        }
    }

    private void Update()
    {
        if (isCam)
        {
            latitude_cam = Input.location.lastData.latitude;
            longitude_cam = Input.location.lastData.longitude;
            //transform.rotation = gyroscope.attitude * rotation;
        }
    }

    public double GetCamLat()
    {
        return latitude_cam;
    }
    public double GetCamLon()
    {
        return longitude_cam;
    }
}
