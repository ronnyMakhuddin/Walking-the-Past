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

    public GameObject[] targetPositions;
    public GameObject spire;
    private int maxPoles = 4; 

    void Awake()
    {
        numTasks = 2;
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
                // rubble quest complete
                QuestFulfilled.rubbleGone = true;
                //EnableTargetPositions();
            }
            //proceed destruction
            if (destruction.GetStage() == 0 && rubblePiles.Count == Mathf.RoundToInt(initialNumPiles / 2f))
            {
                destruction.ProgressState();
                VibrationTypes.OnMaxburgCracksVibrate();
            }
        }
        if (phase == 2)
        {
            //Final State, swaps building models + triggers particle systems
            if(destruction.GetStage() == 1) destruction.ProgressState();
            //Start pole placement task
            if (QuestFulfilled.polesPlaced >= maxPoles)
            {
                EndMinigame();
            }
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
        initialNumPiles = rubblePiles.Count;
    }


    public void RemoveRubble(RubbleHealth toRemove)
    {
        if (rubblePiles.Contains(toRemove))
        {
            rubblePiles.Remove(toRemove);
        }
    }

    void EnableTargetPositions()
    {
        foreach (var pos in targetPositions)
        {
            pos.SetActive(true);
        }
    }

    void EndMinigame()
    {
        spire.SetActive(true);
        //OnMinigameFinished();
        phase = 3;
    }
    
}
