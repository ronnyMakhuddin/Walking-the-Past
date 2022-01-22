using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MoveForARSetup : MonoBehaviour
{
    void Awake()
    {
        bool isCam = gameObject.tag.Equals("ARSessionOrigin");
        Vector3 relativePos = GameManager.Instance.arOriginRelativeToPlayer;
        Vector3 absPos = isCam ? GameManager.Instance.playerPos : GameManager.Instance.arOriginPos;
        if (isCam)
        {
            if (GameManager.Instance.useAbsolutePos)
            {
                transform.position = new Vector3(absPos.x, 1.3f, absPos.z);
            }
        }else
        {
            transform.position = !GameManager.Instance.useAbsolutePos ? new Vector3(relativePos.x, -1.3f, relativePos.z) : new Vector3(absPos.x, 0, absPos.z);
        }
    }

    private void Update()
    {
        Debug.Log(gameObject.name + ": " + transform.position);
    }
}
