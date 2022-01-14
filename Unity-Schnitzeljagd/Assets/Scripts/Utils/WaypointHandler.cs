using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Mapbox.Unity.MeshGeneration.Factories;

[RequireComponent(typeof(DirectionsFactory))]
public class WaypointHandler : MonoBehaviour
{
    /*
    [SerializeField]
    AbstractMap map;

    public Location coordinates;

    public void MoveWaypointToGeoLocation(Transform waypoint)
    {
        waypoint.MoveToGeocoordinate(coordinates.LatitudeLongitude, map.CenterMercator, map.WorldRelativeScale);
    }
    */
    DirectionsFactory directionsFactory;
    Transform currentObjective;

    private void Awake()
    {
        directionsFactory = GetComponent<DirectionsFactory>();
    }

}
