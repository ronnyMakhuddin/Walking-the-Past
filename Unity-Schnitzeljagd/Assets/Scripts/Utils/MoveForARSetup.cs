using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;


public class MoveForARSetup : MonoBehaviour
{
    private bool isTownhall = false; 
    private float hardCodeDist = 10f; 
    private Vector3 scale_real_to_map = new Vector3(-2f, 1f, -2f);
    void Awake()
    {
        bool trialMode = false;

        isTownhall = SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName(GameConstants.OLD_TOWNHALL_SCENE));
        Debug.Log(isTownhall);

        if (isTownhall) scale_real_to_map = Vector3.Scale(scale_real_to_map, new Vector3(-1f, 1f, -1f));

        bool isCam = gameObject.tag.Equals("ARSessionOrigin");
        Vector3 relativePos = GameManager.Instance.arOriginRelativeToPlayer;
        Vector3 absPos = isCam ? GameManager.Instance.playerPos : GameManager.Instance.arOriginPos;
        if (isCam)
        {
            if (GameManager.Instance.useAbsolutePos)
            {
                    transform.position = trialMode ? new Vector3(0f, 1.6f, 0f) : Vector3.Scale(new Vector3(absPos.x, 1.6f, absPos.z), scale_real_to_map);
                    if (isTownhall)
                    {
                    if (!trialMode)
                    {
                        transform.rotation = GameManager.Instance.playerOrientation;// Quaternion.Inverse(GameManager.Instance.playerOrientation);
                    }
                    }
                    else
                    {
                        Vector3 playerRot = GameManager.Instance.playerOrientation.eulerAngles;
                        transform.Rotate(new Vector3(0f, playerRot.y + 180f, 0f)); // player's y direction is inverted on mapbox for this specific starting point!
                    }
                
            }
        }else
        {
            //Debug.Log(gameObject.name);
            if (GameManager.Instance.useAbsolutePos)
            {
                transform.position = Vector3.Scale(new Vector3(absPos.x, 0, absPos.z), scale_real_to_map);
                Vector3 playerRot = GameManager.Instance.playerOrientation.eulerAngles;
                Quaternion playerRotationAR = GameManager.Instance.playerOrientation * Quaternion.Euler(0, 180f, 0);

                if (isTownhall)
                {
                    if (!trialMode)
                    {
                        transform.rotation = (GameManager.Instance.arAnchorOrientation);//Quaternion.Inverse(playerRotationAR) * GameManager.Instance.arAnchorOrientation;
                        transform.position = Vector3.Scale(new Vector3(absPos.x, 0f, absPos.z), scale_real_to_map);
                    }
                    else
                    {
                        transform.position = new Vector3(0f, 0f, hardCodeDist);
                    }
                }
                else
                {
                    transform.rotation = Quaternion.Inverse(GameManager.Instance.arAnchorOrientation);
                    transform.position = Vector3.Scale(new Vector3(absPos.x, 0f, absPos.z), scale_real_to_map);

                }


            }
            else
            {
                transform.position = Vector3.Scale(new Vector3(relativePos.x, -1.6f, relativePos.z), scale_real_to_map);
                //transform.rotation = GameManager.Instance.arAnchorOrientation;
            }
        }
    }

    private void Update()
    {
        //Debug.Log(gameObject.name + ": " + transform.position);
    }
    
}
