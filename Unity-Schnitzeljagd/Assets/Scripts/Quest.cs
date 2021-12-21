using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest", fileName = "New Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private string task = "This is the task.";

    public string getTask()
    {
        return task;
    }

    [SerializeField] private Sprite character;

    public Sprite getCharacter()
    {
        return character;
    }

    [SerializeField] private Quest nextQuest;

    public Quest getNextQuest()
    {
        return nextQuest;
    }
}
