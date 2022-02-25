using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestFulfilled : MonoBehaviour
{
    public static bool rubbleGone = false;
    public static bool spireCollected = false;
    public static bool spirePlaced = false;
    public static bool walkedSynagoge = true;

    public int maxPoles = 4;
    public static int polesCollected = 0;
    public static int polesPlaced = 0;

    public int maxDancers = 6;
    public static int dancersCollected = 0;

    private void Start()
    {
        rubbleGone = false;
        spireCollected = false;
        spirePlaced = false;
        walkedSynagoge = true;
        polesCollected = 0;
        polesPlaced = 0;
        dancersCollected = 0;
    }

    public bool CheckQuest0()
    {
        return SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName(GameConstants.MAXBURG_SCENE));
    }
    
    public bool CheckQuest1()
    {
        return rubbleGone;
    }
    
    public bool CheckQuest2()
    {
        return polesCollected >= maxPoles;
    }
    
    public bool CheckQuest3()
    {
        return polesPlaced >= maxPoles;
    }
    
    public bool CheckQuest4()
    {
        return spireCollected;
    }
    
    public bool CheckQuest5()
    {
        return SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName(GameConstants.MUSEUM));
    }
    
    public bool CheckQuest6()
    {
        return dancersCollected >= maxDancers;
    }
    
    public bool CheckQuest7()
    {
        return walkedSynagoge;
    }
    
    public bool CheckQuest8()
    {
        return SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName(GameConstants.OLD_TOWNHALL_SCENE));
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
