using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MoveForARSetup : MonoBehaviour
{
    private Vector3 scale_real_to_map = new Vector3(-2f, 1f, -2f);
    void Awake()
    {
        bool isCam = gameObject.tag.Equals("ARSessionOrigin");
        Vector3 relativePos = GameManager.Instance.arOriginRelativeToPlayer;
        Vector3 absPos = isCam ? GameManager.Instance.playerPos : GameManager.Instance.arOriginPos;
        if (isCam)
        {
            if (GameManager.Instance.useAbsolutePos)
            {
                transform.position = Vector3.Scale(new Vector3(absPos.x, 1.3f, absPos.z), scale_real_to_map);

                Vector3 playerRot = GameManager.Instance.playerOrientation.eulerAngles;
                transform.Rotate(new Vector3(0f, playerRot.y + 180f, 0f)); // player's y direction is inverted on mapbox!
            }
        }else
        {
            //Debug.Log(gameObject.name);
            if (GameManager.Instance.useAbsolutePos)
            {
                transform.position = Vector3.Scale(new Vector3(absPos.x, 0, absPos.z), scale_real_to_map);
                transform.rotation = Quaternion.Inverse(GameManager.Instance.arAnchorOrientation);
            }
            else
            {
                transform.position = Vector3.Scale(new Vector3(relativePos.x, -1.3f, relativePos.z), scale_real_to_map);
                //transform.rotation = GameManager.Instance.arAnchorOrientation;
            }
        }
    }

    private void Update()
    {
        //Debug.Log(gameObject.name + ": " + transform.position);
    }
    
}
