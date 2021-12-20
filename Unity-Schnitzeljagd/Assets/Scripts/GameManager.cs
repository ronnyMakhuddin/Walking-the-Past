using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;

public class GameManager : MonoBehaviour
{
    public enum GAMESTATE
    {
        STARTED,
        WORLD,
        PAUSED,
        BACKGROUND,
        AR
    }

    public enum AR_SITE
    {
        OLD_TOWNHALL,
        MAXBURG
    }

    GAMESTATE state;


    public Location playerMapboxLocation;
    public AbstractMap map;

    bool started = false;
    bool running = false;
    bool arPossible = false;
    
    

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

    public GAMESTATE GetGameState()
    {
        return state;
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

    private void Update()
    {
        if(state == GAMESTATE.WORLD)
        {
            if (arPossible)
            {
                //show AR enable button
            }
        }

    }

    public void EnterAR(AR_SITE site)
    {

    }

    private bool checkARLocation(AR_SITE site)
    {
        return false;
    }


}
