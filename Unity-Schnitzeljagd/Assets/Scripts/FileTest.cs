using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileTest : MonoBehaviour
{

    private Dictionary<int, Task> test;
    private Dictionary<int, Task> read;

    // Start is called before the first frame update
    void Start()
    {
        test = new Dictionary<int, Task>();
        string filename = "testDictionary.json";
        Task quest1 = new Task(1, "This is your first task.");
        Task quest2 = new Task(2, "bla blq bla");
        
        test.Add(quest1.taskID, quest1);
        test.Add(quest2.taskID, quest2);

        int count = 1;
        Task temp;
        if (test.TryGetValue(count, out temp))
        {
            Debug.Log("Found Entry!");
            Debug.Log(temp);
        }
        else
        {
            Debug.Log("Failure!");
        }
        SaveTasks(test, filename);
        LoadTasks(filename);
        Debug.Log("read dictionary");
    }
    
    public void LoadTasks(string filename)
    {
        this.read = new Dictionary<int, Task>();
        List<Task> temp = FileManager.ReadFromJSON<Task>(filename);
        foreach (var task in temp)
        {
            this.read.Add(task.taskID, task);
        }
    }

    public void SaveTasks(Dictionary<int, Task> tasks, string filename)
    {
        List<int> keys = new List<int>(tasks.Keys);
        List<Task> toSave = new List<Task>();
        foreach (var key in keys)
        {
            toSave.Add(test[key]);
        }
        FileManager.SaveToJSON(toSave, filename);
    }
}

[Serializable]
public struct Task
{
    public int taskID;
    public string tasktext;
    public Task(int taskID, string tasktext)
    {
        this.taskID = taskID;
        this.tasktext = tasktext;
    }
}



