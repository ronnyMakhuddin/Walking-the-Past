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

    bool GPS;

    public static GPSPositioningCam Instance { set; get; }
    private GameObject cameraContainer;

    Gyroscope gyroscope;
    Quaternion rotation;

    bool isCam = false;
    bool isEditor = true;
    private void Awake()
    {
        isEditor = Application.isEditor;
        if (!isEditor)
        {
            Instance = this;
            altitude = bodyHeight;

            transform.position = new Vector3(0, bodyHeight, 0);

            StartCoroutine(StartLocationService());

        }
        else
        {
            Instance = this;
            latitude_cam = 48.1834f;
            longitude_cam = 11.4923f;
            altitude = bodyHeight;

            transform.localPosition = new Vector3(0, bodyHeight, 0);


        }
    }

    private void Update()
    {
        if (!Application.isEditor)
        {
            if (GPS)
            {
                latitude_cam = Input.location.lastData.latitude;
                longitude_cam = Input.location.lastData.longitude;

                transform.position = GPSARCoord.CalculateRelativePosFromCoord(latitude_cam, longitude_cam, altitude, true);
                //transform.rotation = gyroscope.attitude * rotation;
            }
        }
        else
        {
            latitude_cam = 48.1834f;
            longitude_cam = 11.4923f;
            transform.position = GPSARCoord.CalculateRelativePosFromCoord(latitude_cam, longitude_cam, altitude, true);
        }
    }

    public double GetCamLat()
    {
        return Application.isEditor ? 48.1834f : latitude_cam;
    }
    public double GetCamLon()
    {
        return Application.isEditor ? 11.4923f : longitude_cam;
    }

    public double GetAltitude()
    {
        return altitude;
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }
        Input.location.Start(0.1f, 0.1f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait <= 0)
        {
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            yield break;
        }
        latitude_cam = (double)Input.location.lastData.latitude;
        longitude_cam = (double)Input.location.lastData.longitude;
        GPS = true;
        yield break;
    }
}
