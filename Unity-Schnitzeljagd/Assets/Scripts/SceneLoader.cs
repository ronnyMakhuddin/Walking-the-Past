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
        SceneManager.LoadScene("Mapbox");
        Debug.Log("Loading Mapbox Scene...");
    }
}
