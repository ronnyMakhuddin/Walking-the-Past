/*
 * Inspired by:
 * Mário Vairinhos 2020 
 * DECA LAR - LOCATIVE AUGMENTED REALITY
*/

using UnityEngine;

public class GPSPositioningObject : MonoBehaviour
{
    [Header("Object Data")]
    public double altitude = 0;
    //TODO set private later and send through gamemanager
    public double latitude;
    public double longitude;
    private double earthRadiusMunichMeters = 6366844; //6372797.560856f

    // Update is called once per frame
    void Update()
    {
        if (!Application.isEditor)
        {
            calculateRelativePosFromCoord();
        }
    }

    private void calculateRelativePosFromCoord()
    { 
        double dlat = latitude - GPSPositioningCam.Instance.GetCamLat();
        dlat = getAxisCoordInMetersFromRadius(earthRadiusMunichMeters, dlat);

        double dlon = longitude - GPSPositioningCam.Instance.GetCamLon();

        double radiusLat = Mathf.Cos((float)latitude) * earthRadiusMunichMeters;
        dlon = getAxisCoordInMetersFromRadius(radiusLat, dlon);


        double dalt = altitude - LocativeGPS.Instance.altitude; 
        transform.position = new Vector3((float)dlat, (float)dalt, (float)dlon);
    }

    private double getAxisCoordInMetersFromRadius(double radius, double angle)
    {
        double meters = (radius / 180) * Mathf.PI;// #meters that equal 1 degree at this radius
        meters *= angle;
        return meters;
    }

    public void SetARObjectGPS(Vector2 coords)
    {
        latitude = coords[0];
        longitude = coords[1];
    }
}
