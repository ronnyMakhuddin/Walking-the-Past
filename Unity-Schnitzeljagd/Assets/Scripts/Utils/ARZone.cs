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
    SceneTransitionManager sceneTransitionManager;
    // Start is called before the first frame update
    void Start()
    {
        scene = GameManager.AR_SITE.MAXBURG;
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>().GetComponent<SceneTransitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(listening && timeSpentInZone >= secondsUntilTrigger)
        {
            // for now instant switch
            listening = false;
            Debug.Log("Success");
            // sceneTransitionManager.GoToScene(scene, null);
            GameManager.Instance.EnterAR(transform.InverseTransformPoint(playerPos), scene);
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
            timeSpentInZone = 0f;
            listening = true;
        }
    }
}
