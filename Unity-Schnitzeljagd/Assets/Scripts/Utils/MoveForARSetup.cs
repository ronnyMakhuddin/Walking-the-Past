using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;


public class MoveForARSetup : MonoBehaviour
{
    private bool isTownhall = false; 
    private Vector3 scale_real_to_map = new Vector3(-1/0.3270666f, 1f, -1/0.3270666f);
    void Awake()
    {
        isTownhall = SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName(Schnitzelconstants.OLD_TOWNHALL_SCENE));
        Debug.Log(isTownhall);

        if (isTownhall) scale_real_to_map = Vector3.Scale(scale_real_to_map, new Vector3(-1f, 1f, -1f));

        bool isCam = gameObject.tag.Equals("ARSessionOrigin");

        Vector3 relativePos = GameManager.Instance.arOriginRelativeToPlayer;
        Vector3 absPos = isCam ? GameManager.Instance.playerPos : GameManager.Instance.arOriginPos;
        if (isCam)
        {
            if (GameManager.Instance.useAbsolutePos)
            {
                transform.position = Vector3.Scale(new Vector3(absPos.x, 1.3f, absPos.z), scale_real_to_map);
                if (isTownhall)
                {
                    transform.rotation = (GameManager.Instance.playerOrientation);
                }
                else
                {
                    Vector3 playerRot = GameManager.Instance.playerOrientation.eulerAngles;
                    transform.Rotate(new Vector3(0f, playerRot.y + 180f, 0f)); // player's y direction is inverted on mapbox!
                }
            }
            else
            {
                transform.rotation = (GameManager.Instance.playerOrientation);
            }
        }
        else
        {
            Vector3 playerRot = GameManager.Instance.playerOrientation.eulerAngles;
            Quaternion playerRotationAR = GameManager.Instance.playerOrientation * Quaternion.Euler(0, 180f, 0);
            //Debug.Log(gameObject.name);
            if (GameManager.Instance.useAbsolutePos)
            {
                transform.position = Vector3.Scale(new Vector3(absPos.x, 0, absPos.z), scale_real_to_map);

                if (isTownhall)
                {
                    transform.rotation = (GameManager.Instance.arAnchorOrientation);//Quaternion.Inverse(playerRotationAR) * GameManager.Instance.arAnchorOrientation;
                }
                else
                {
                    transform.rotation = Quaternion.Inverse(GameManager.Instance.arAnchorOrientation);
                }

                transform.position = Vector3.Scale(new Vector3(absPos.x, 1.3f, absPos.z), scale_real_to_map);
            }
            else
            {
                transform.position = new Vector3(relativePos.x, -1.3f, relativePos.z);
                transform.rotation = Quaternion.Inverse(playerRotationAR) * GameManager.Instance.arAnchorOrientation;
            }
        }
    }

    private void Update()
    {
        //Debug.Log(gameObject.name + ": " + transform.position);
    }
    
}
