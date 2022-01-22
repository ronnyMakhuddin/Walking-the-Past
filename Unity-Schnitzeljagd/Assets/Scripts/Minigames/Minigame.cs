using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Minigame : MonoBehaviour
{
    public Button buttonAR; 
    protected PHASE currentPhase;
    protected int numTasks = 1;
    protected int completedTasks = 0;
    protected enum PHASE
    {
        START,
        TASK,
        END
    }

    private void Start()
    {
        buttonAR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void AdvancePhase()
    {
        switch (currentPhase)
        {
            case PHASE.START: currentPhase = PHASE.TASK; break;
            case PHASE.TASK:
                completedTasks++;
                if (completedTasks == numTasks)
                {
                    currentPhase = PHASE.END; break;
                }
                else
                {
                    //next task?
                }
                break;
        }
    }

    private void StartTaskSetup()
    {

    }

    protected void OnMinigameFinished()
    {
        buttonAR.enabled = true;
        //GameManager.Instance.ARCompleted();
    }

}
