using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GPSARCoord
{
    /// <summary>
    /// Converts a GPS position to to a world coordinate
    /// </summary>
    private static double earthRadiusMunichMeters = 6366844;
    public static Vector3 CalculateRelativePosFromCoord(double latitude, double longitude, double altitude, bool isAnchor = false)
    {
        double dlat = latitude - GPSPositioningCam.Instance.GetCamLat();
        dlat = GetAxisCoordInMetersFromRadius(earthRadiusMunichMeters, dlat);

        double dlon = longitude - GPSPositioningCam.Instance.GetCamLon();

        double radiusLat = Mathf.Cos((float)latitude) * earthRadiusMunichMeters;
        dlon = GetAxisCoordInMetersFromRadius(radiusLat, dlon);


        double dalt = altitude - GPSPositioningCam.Instance.GetAltitude();
        return new Vector3((float)dlat, (float)dalt, (float)dlon);
    }

    private static double GetAxisCoordInMetersFromRadius(double radius, double angle)
    {
        double meters = (radius / 180) * Mathf.PI;// #meters that equal 1 degree at this radius
        meters *= angle;
        return meters;
    }
}

