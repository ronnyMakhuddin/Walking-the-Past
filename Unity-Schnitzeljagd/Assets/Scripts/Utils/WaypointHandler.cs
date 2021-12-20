using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;

public class WaypointHandler : MonoBehaviour
{
    [SerializeField]
    AbstractMap map;

    public Location coordinates;

    public void MoveWaypointToGeoLocation(Transform waypoint)
    {
        waypoint.MoveToGeocoordinate(coordinates.LatitudeLongitude, map.CenterMercator, map.WorldRelativeScale);
    }
}
