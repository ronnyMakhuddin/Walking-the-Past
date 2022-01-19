using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private Quest startQuest;
    [SerializeField] private DialogueSystem dialogueSystem;
    private Quest mainQuest;
    private Quest sideQuest;
    
    private void Start()
    {
        mainQuest = startQuest;
        Debug.Log("Quest System starting");
    }

    public void SetMain(Quest main)
    {
        mainQuest = main;
    }

    public void SetSide(Quest side)
    {
        sideQuest = side;
    }

    public Quest GetMain()
    {
        return mainQuest;
    }

    public Quest GetSide()
    {
        return sideQuest;
    }
    
    public void NextMain()
    {
        Quest next = mainQuest.GetNextQuest();
        if (next != null)
        {
            mainQuest = next;
            dialogueSystem.StartDialogue(mainQuest.getDialogueStart(), mainQuest.getDialogueStop());
            return;
        }
        mainQuest = null;
    }
    
    public void NextSide()
    {
        Quest next = sideQuest.GetNextQuest();
        if (next != null)
        {
            sideQuest = next;
            dialogueSystem.StartDialogue(sideQuest.getDialogueStart(), sideQuest.getDialogueStop());
            return;
        }
        sideQuest = null;
    }
    
}
