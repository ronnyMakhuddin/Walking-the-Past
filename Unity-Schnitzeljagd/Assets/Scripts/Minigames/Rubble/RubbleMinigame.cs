using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleMinigame : Minigame
{
    public Transform sceneOrigin;
    List<RubbleHealth> rubblePiles;
    MaxburgDestruction destruction;
    public int rubbleMaxHealth = 3;
    int phase = 1;
    float initialNumPiles = 0;

    void Awake()
    {
        destruction = FindObjectOfType<MaxburgDestruction>();
        GetAllRubbleInScene();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (phase == 1)
        {
            if (rubblePiles.Count == 0)
            {
                //All rubble destroyed, trigger second phase
                phase = 2;
            }
            if (rubblePiles.Count == Mathf.RoundToInt(initialNumPiles / 2f))
            {
                destruction.ProgressState();
                VibrationTypes.OnMaxburgCracksVibrate();
            }
        }
        else
        {
            //Final State, swaps building models + triggers particle systems
            destruction.ProgressState();
            //Start pole placement task

        }
    }

    void WrapupSwipePiles()
    {

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
        initialNumPiles = rubblePiles.Count;
    }


    public void RemoveRubble(RubbleHealth toRemove)
    {
        if (rubblePiles.Contains(toRemove))
        {
            rubblePiles.Remove(toRemove);
        }
    }
}
