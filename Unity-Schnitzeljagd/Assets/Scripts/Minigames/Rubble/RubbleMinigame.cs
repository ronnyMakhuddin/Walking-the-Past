using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleMinigame : MonoBehaviour
{
    private Transform sceneOrigin;
    List<RubbleHealth> rubblePiles;
    // Start is called before the first frame update
    void Awake()
    {
        sceneOrigin = GameObject.Find("SceneOrigin").transform;
        rubblePiles = GetAllRubbleInScene();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rubblePiles.Count == 0)
        {
            //All rubble destroyed, trigger ending
            OnMinigameCompleted();
        }
    }

    List<RubbleHealth> GetAllRubbleInScene()
    {
        List<RubbleHealth> rubble = new List<RubbleHealth>();
        foreach (Transform child in sceneOrigin)
        {
            if (child == null) continue;
            RubbleHealth rh = child.GetComponent<RubbleHealth>();
            if (rh != null) rubble.Add(rh);
        }
        return rubble;
    }

    void OnMinigameCompleted()
    {
        //Maybe have a dialogue over the found item here?
        //...

        //After resolved load mapbox
        //GameManager.Instance.ARCompleted();
        Debug.Log("Rubble Completed");
    }

    public void RemoveRubble(RubbleHealth toRemove)
    {
        if (rubblePiles.Contains(toRemove))
        {
            rubblePiles.Remove(toRemove);
        }
    }
}
