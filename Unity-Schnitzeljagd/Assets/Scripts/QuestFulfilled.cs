using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestFulfilled : MonoBehaviour
{
    public static bool spireCollected = false;
    public static bool spirePlaced = false;

    public bool CheckQuest0()
    {
        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("RubbleScene")))
        {
            return true;
        }
        return false;
    }
    
    public bool CheckQuest1()
    {
        return false;
    }
    
    public bool CheckQuest2()
    {
        return false;
    }
    
    public bool CheckQuest3()
    {
        return false;
    }
    
    public bool CheckQuest4()
    {
        return spireCollected;
    }
    
    public bool CheckQuest5()
    {
        return false;
    }
    
    public bool CheckQuest6()
    {
        return false;
    }
    
    public bool CheckQuest7()
    {
        return false;
    }
    
    public bool CheckQuest8()
    {
        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("OldTownHall")))
        {
            return true;
        }
        return false;
    }
    
    public bool CheckQuest9()
    {
        return spirePlaced;
    }
    
    public bool CheckQuest10()
    {
        return false;
    }
}
