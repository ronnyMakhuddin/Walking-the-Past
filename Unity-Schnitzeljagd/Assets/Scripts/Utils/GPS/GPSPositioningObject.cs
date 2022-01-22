/*
 * Inspired by:
 * Mario Vairinhos 2020 
 * DECA LAR - LOCATIVE AUGMENTED REALITY
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GPSPositioningObject : MonoBehaviour
{
    [Header("Object Data")]
    public double altitude = 0;
    //TODO set private later and send through gamemanager
    public double latitude = 0f;
    public double longitude = 0f;

    float timeFromSpawn = 0;
    float adjustTime = 10;

    //Debugging
    public Text debug;

    bool positionUpdates = true;
    private List<Vector3> positions;

    private void Awake()
    {
        timeFromSpawn = 0;
        transform.position = AdjustPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (positionUpdates)
        {
            if (timeFromSpawn < adjustTime)
            {
                positions.Add(AdjustPosition());
                debug.text = "" + (transform.position - GPSPositioningCam.Instance.gameObject.transform.position);
                timeFromSpawn += Time.deltaTime;
            }
            else
            {
                Vector3 endPos = Vector3.zero;
                foreach(Vector3 pos in positions)
                {
                    endPos += pos;
                }

                transform.position = endPos / positions.Count;
                debug.text = "" + (transform.position - GPSPositioningCam.Instance.gameObject.transform.position);

            }
        }

    }

    public Vector3 AdjustPosition()
    {
        return GPSARCoord.CalculateRelativePosFromCoord(latitude, longitude, altitude);
    }

    public void SetARObjectGPS(Vector2 coords)
    {
        latitude = coords[0];
        longitude = coords[1];
    }
}
