using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    bool started = false;
    bool running = false;

    static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    public bool IsStarted()
    {
        return started;
    }

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool IsRunning()
    {
        return running;
    }

    private void Start()
    {
        
    }

    void AnchorAdded(ARAnchor anchor)
    {
        started = true;
        running = true;
    }

    void AnchorRemoved(ARAnchor anchor)
    {
        running = false;
    }
}
