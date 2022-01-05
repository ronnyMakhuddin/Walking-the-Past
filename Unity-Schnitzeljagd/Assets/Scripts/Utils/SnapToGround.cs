using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SnapToGround : MonoBehaviour
{
    void Awake()
    {
        Vector3 relativePos = GameManager.Instance.arOriginRelativeToPlayer;
        transform.position = new Vector3(relativePos.x, relativePos.y - 1.3f, relativePos.z);
    }
}
