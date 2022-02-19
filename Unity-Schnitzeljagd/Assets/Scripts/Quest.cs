using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Quest", fileName = "New Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private int questID = 1;

    [SerializeField] private Sprite character;
    [SerializeField] private int dialogueStart = 1;

    public GameManager.AR_SITE ar_site;

    public Sprite GetCharacter()
    {
        return character;
    }
    
    public int GetID()
    {
        return questID;
    }

    public int getDialogueStart()
    {
        return dialogueStart;
    }

    public void SetSprite(Sprite sprite)
    {
        character = sprite;
    }
    
    public void SetID(int id)
    {
        questID = id;
    }

    [SerializeField] private Quest nextQuest;
    public Quest GetNextQuest()
    {
        return nextQuest;
    }

}
