/*
 * Inspired by:
 * Mario Vairinhos 2020 
 * DECA LAR - LOCATIVE AUGMENTED REALITY
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.XR.ARFoundation;
using Mapbox.Unity.Location;

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

    ILocationProvider _defaultLocationProvider;
    DeviceLocationProviderAndroidNative nativeLocationProvider;

    Gyroscope gyroscope;
    Quaternion rotation;

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

            DefaultLocationProvider = nativeLocationProvider; 


            // enable gyro
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
            rotation = new Quaternion(0, 0, 1, 0);

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

    private void FixedUpdate()
    {
        if (!Application.isEditor)
        {
            if (GPS)
            {
                //Location currentLocation = DefaultLocationProvider.CurrentLocation;
                latitude_cam = Input.location.lastData.latitude;
                longitude_cam = Input.location.lastData.longitude;

                transform.position = GPSARCoord.CalculateRelativePosFromCoord(latitude_cam, longitude_cam, altitude, true);
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

    public ILocationProvider DefaultLocationProvider
    {
        get
        {
            return _defaultLocationProvider;
        }
        set
        {
            _defaultLocationProvider = value;
        }
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
