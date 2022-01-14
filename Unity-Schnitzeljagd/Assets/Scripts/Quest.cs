using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest", fileName = "New Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private string task = "This is the task.";
    [SerializeField] private int questID = 1;
    
    /*public string GetTask()
    {
        return task;
    }*/

    [SerializeField] private Sprite character;

    public Sprite GetCharacter()
    {
        return character;
    }
    
    public int GetID()
    {
        return questID;
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
