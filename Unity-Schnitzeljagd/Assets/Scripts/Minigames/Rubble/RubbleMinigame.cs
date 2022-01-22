using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleMinigame : Minigame
{
    public Transform sceneOrigin;
    List<RubbleHealth> rubblePiles;
    public int rubbleMaxHealth = 3;
    int phase = 1;

    void Awake()
    {
        GetAllRubbleInScene();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rubblePiles.Count == 0)
        {
            //All rubble destroyed, trigger second phase
            base.OnMinigameFinished();
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


    public void RemoveRubble(RubbleHealth toRemove)
    {
        if (rubblePiles.Contains(toRemove))
        {
            rubblePiles.Remove(toRemove);
        }
    }
}
