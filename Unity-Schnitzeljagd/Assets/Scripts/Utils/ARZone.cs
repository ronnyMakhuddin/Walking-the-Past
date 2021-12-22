using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ARZone : MonoBehaviour
{
    public string scene;
    public float secondsUntilTrigger = 10f;
    float timeSpentInZone = 0f;
    bool listening = true;
    SceneTransitionManager sceneTransitionManager;
    // Start is called before the first frame update
    void Start()
    {
        scene = Schnitzelconstants.MAXBURG_SCENE;
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
            sceneTransitionManager.GoToScene(scene, null);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
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
