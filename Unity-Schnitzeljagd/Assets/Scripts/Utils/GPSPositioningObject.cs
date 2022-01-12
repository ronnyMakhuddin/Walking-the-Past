/*
 * Inspired by:
 * Mário Vairinhos 2020 
 * DECA LAR - LOCATIVE AUGMENTED REALITY
*/

using UnityEngine;
using UnityEngine.UI;

public class GPSPositioningObject : MonoBehaviour
{
    [Header("Object Data")]
    public double altitude = 0;
    //TODO set private later and send through gamemanager
    public double latitude;
    public double longitude;

    //Debugging
    public Text debug;

    // Update is called once per frame
    void Update()
    {

        transform.position = GPSARCoord.CalculateRelativePosFromCoord(latitude, longitude, altitude);
        debug.text = "" + (transform.position - GPSPositioningCam.Instance.gameObject.transform.position);

    }


    public void SetARObjectGPS(Vector2 coords)
    {
        latitude = coords[0];
        longitude = coords[1];
    }
}
