using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ARZone : MonoBehaviour
{
    public GameManager.AR_SITE scene;
    public float secondsUntilTrigger = 10f;
    float timeSpentInZone = 0f;
    bool listening = true;
    private Vector3 playerPos;

    private void Awake()
    {
        if (GameManager.Instance.GetCompletedCheckpoints() != null)
        {
            if (GameManager.Instance.GetCompletedCheckpoints().Contains(scene))
            {
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Zone Pos: " + transform.position);

        if (listening && timeSpentInZone >= secondsUntilTrigger)
        {

            Vector3 relativeToPlayer = transform.position - playerPos;

            //Debug.Log(relativeToPlayer);
            GameManager.Instance.ConfigureAR(scene, relativeToPlayer);
            if (GameManager.Instance.useAbsolutePos)
            {
                GameManager.Instance.playerPos = playerPos;
                GameManager.Instance.arOriginPos = transform.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerPos = other.transform.position;
            timeSpentInZone += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.arPossible = false;
            timeSpentInZone = 0f;
            listening = true;
        }

    }
}
