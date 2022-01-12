using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleMinigame : MonoBehaviour
{
    public Transform sceneOrigin;
    List<RubbleHealth> rubblePiles;

    void Awake()
    {
        GetAllRubbleInScene();

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

    void GetAllRubbleInScene()
    {
        rubblePiles = new List<RubbleHealth>();
        foreach (Transform child in sceneOrigin.GetComponentsInChildren<Transform>())
        {
            if (child == null) continue;
            RubbleHealth rh = child.GetComponent<RubbleHealth>();
            if (rh != null) rubblePiles.Add(rh);
        }
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
