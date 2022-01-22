using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class Tap2PlaceObject : MonoBehaviour
{
    public GameObject toBeSpawned;
    private GameObject spawnedObj;
    private ARRaycastManager aRRaycastManager;
    private Vector2 tappedPos;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool GetTouchPos(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }
        touchPos = default;
        return false;
    }

    private void Update()
    {
        if(!GetTouchPos(out Vector2 touchPos))
        {
            return;
        }

        if(aRRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                VibrationTypes.OnTapVibrate(true);
            }
            Pose pose = hits[0].pose;

            if(spawnedObj == null)
            {
                spawnedObj = Instantiate(toBeSpawned, pose.position, pose.rotation);
            }
            else
            {
                spawnedObj.transform.position = pose.position;
            }

            //VibrationTypes.OnTapVibrate(true);

        }
    }
}
