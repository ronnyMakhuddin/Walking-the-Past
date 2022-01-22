using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    protected PHASE currentPhase;
    protected int numTasks = 1;
    protected int completedTasks = 0;
    protected enum PHASE
    {
        START,
        TASK,
        END
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
        GameManager.Instance.ARCompleted();
    }

}
