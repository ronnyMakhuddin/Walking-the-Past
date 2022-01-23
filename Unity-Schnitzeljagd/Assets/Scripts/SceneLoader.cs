using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
     
    public void LoadAR()
    {
        SceneManager.LoadScene("AR");
    }

    public void LoadMapbox()
    {
        GameManager.Instance.ARCompleted();//EnterMapbox();
        //SceneManager.LoadScene(Schnitzelconstants.WORLD_SCENE);
        Debug.Log("Loading Mapbox Scene...");
    }

    public void LoadOldTownHall()
    {
        SceneManager.LoadScene("OldTownHall");
    }
}
