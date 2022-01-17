using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GAMESTATE
    {
        WORLD,
        AR
    }

    List<AR_SITE> completedCheckpoints;
    public AR_SITE currCheckpoint;
    Button EnterARButton;
    string arSceneName = "";

    public List<AR_SITE> GetCompletedCheckpoints()
    {
        return completedCheckpoints;
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
    public bool arPossible = false;

    public Vector3 arOriginRelativeToPlayer;
    SceneTransitionManager sceneTransitionManager;


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
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>().GetComponent<SceneTransitionManager>();

        if (state == GAMESTATE.WORLD)
        {
            EnterARButton = GameObject.FindGameObjectWithTag("ARLoadButton").GetComponent<Button>();
            ToggleEnterARButton(false);
            EnterARButton.onClick.AddListener(EnterAR);
        }

        if (instance == null)
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
        completedCheckpoints = new List<AR_SITE>();
    }



    public GAMESTATE GetGameState()
    {
        return state;
    }


    private void Update()
    {
        if (state == GAMESTATE.WORLD)
        {

            //show enter AR button
            ToggleEnterARButton(arPossible);
        }
    }

    public void ARCompleted()
    {
        completedCheckpoints.Add(currCheckpoint);
        EnterMapbox();
    }

    public void ConfigureAR(AR_SITE site, Vector3 POI2PlayerPos)
    {

        currCheckpoint = site;
        arOriginRelativeToPlayer = POI2PlayerPos;
        arSceneName = "";
        switch (site)
        {
            case AR_SITE.MAXBURG: arSceneName = Schnitzelconstants.MAXBURG_SCENE; break;
            case AR_SITE.OLD_TOWNHALL: arSceneName = Schnitzelconstants.OLD_TOWNHALL_SCENE; break;
            default: break;
        }
        arPossible = true;
    }

    public void EnterMapbox()
    {
        arPossible = false;
        state = GAMESTATE.WORLD;
        sceneTransitionManager.GoToScene(Schnitzelconstants.WORLD_SCENE, null);
    }

    public void EnterAR()
    {
        state = GAMESTATE.AR;
        sceneTransitionManager.GoToScene(arSceneName, null);
    }

    void ToggleEnterARButton(bool active)
    {
        EnterARButton.enabled = active;
        EnterARButton.image.enabled = active;
        EnterARButton.transform.GetChild(0).gameObject.SetActive(active);
    }
}
