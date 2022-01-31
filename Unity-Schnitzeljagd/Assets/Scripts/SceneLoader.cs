using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject myUI;

    private void Awake()
    {
        if (myUI == null)
        {
            myUI = GameObject.Find("UI").gameObject;
        }
        DontDestroyOnLoad(this);
    }

    public void LoadAR()
    {
        DontDestroyOnLoad(myUI);
        SceneManager.LoadScene("AR");
    }

    public void LoadMapbox()
    {
        //DontDestroyOnLoad(myUI);
        GameManager.Instance.ARCompleted();//EnterMapbox();
        //SceneManager.LoadScene(Schnitzelconstants.WORLD_SCENE);
        Debug.Log("Loading Mapbox Scene...");
    }

    public void LoadOldTownHall()
    {
        DontDestroyOnLoad(myUI);
        SceneManager.LoadScene("OldTownHall");
    }
}
