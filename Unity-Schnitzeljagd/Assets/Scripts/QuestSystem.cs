using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private Quest startQuest;
    [SerializeField] private DialogueSystem dialogueSystem;
    private static Quest mainQuest;
    private static Quest sideQuest;
    public static bool mainSet = false;

    private QuestFulfilled check;
    
    private void Start()
    {

        mainQuest = startQuest;
        mainSet = true;
        check = gameObject.GetComponent<QuestFulfilled>();
        if (check == null)
        {
            check = gameObject.AddComponent<QuestFulfilled>();
        }
    }

    private void Update()
    {
        if (mainSet && Fulfilled(mainQuest.GetID()))
        {
            NextMain();
        }
    }

    public void SetMain(Quest main)
    {
        mainQuest = main;
    }

    public void SetSide(Quest side)
    {
        sideQuest = side;
    }

    public static Quest GetMain()
    {
        return mainQuest;
    }

    public static Quest GetSide()
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

        mainSet = false;
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

    public void ResetQuests()
    {
        mainQuest = startQuest;
        dialogueSystem.StartDialogue(mainQuest.getDialogueStart(), mainQuest.getDialogueStop());
    }

    private bool Fulfilled(int id)
    {
        switch (id)
        {
            case 0:
                return check.CheckQuest0();
            case 1:
                return check.CheckQuest1();
            case 2:
                return check.CheckQuest2();
            case 3:
                return check.CheckQuest3();
            case 4:
                return check.CheckQuest4();
            case 5:
                return check.CheckQuest5();
            case 6:
                return check.CheckQuest6();
            case 7:
                return check.CheckQuest7();
            case 8:
                return check.CheckQuest8();
            case 9:
                return check.CheckQuest9();
            case 10:
                return check.CheckQuest10();
        }

        return false;
    }

    
}
