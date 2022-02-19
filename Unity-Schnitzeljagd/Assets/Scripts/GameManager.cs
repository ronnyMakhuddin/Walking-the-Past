using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GAMESTATE
    {
        AR,
        WORLD,
        STORY
    }

    List<AR_SITE> completedCheckpoints;
    
    public AR_SITE currCheckpoint;

    //TODO set this flag by Quests!
    private Wayfinding.ROUTES currRoute = Wayfinding.ROUTES.NONE;

    Button EnterARButton;
    string arSceneName = "";
    public bool useAbsolutePos = true;

    public List<AR_SITE> GetCompletedCheckpoints()
    {
        return completedCheckpoints;
    }
    public enum AR_SITE
    {
        OLD_TOWNHALL,
        MAXBURG,
        MUSEUM
    }

    GAMESTATE state;


    public Location playerMapboxLocation;
    public AbstractMap map;

    bool started = false;
    bool running = false;
    public bool arPossible = false;

    public Vector3 arOriginRelativeToPlayer;
    public Quaternion playerOrientation;
    public Quaternion arAnchorOrientation;
    public Vector3 playerPos = Vector3.zero;
    public Vector3 arOriginPos = Vector3.zero;
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

        if(SceneManager.GetActiveScene().name.Equals(Schnitzelconstants.WORLD_SCENE)) {
            state = GAMESTATE.WORLD;
        }
        else
        {
            state = GAMESTATE.AR;
        }

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
        
        DontDestroyOnLoad(GameObject.Find("UI").gameObject);
    }

    public bool IsRunning()
    {
        return running;
    }

    private void Start()
    {
        completedCheckpoints = new List<AR_SITE>();
    }

    public Wayfinding.ROUTES GetCurrentRoute()
    {
        return currRoute;
    }



    public GAMESTATE GetGameState()
    {
        return state;
    }

    public void SetGameState(GAMESTATE state)
    {
        this.state = state;
    }


    private void LateUpdate()
    {
        //Debug.Log("enabled: " + EnterARButton.enabled + " state: " + state + " arpossible: " + arPossible);
        if (!EnterARButton.enabled && state == GAMESTATE.WORLD && arPossible)
        {

            //show enter AR button
            ToggleEnterARButton(true);
        }

        if (EnterARButton.enabled && (!arPossible || state == GAMESTATE.STORY))
        {
            ToggleEnterARButton(false);
        }
        
    }

    public void ARCompleted()
    {
        switch (currCheckpoint)
        {
            case AR_SITE.OLD_TOWNHALL: currRoute = Wayfinding.ROUTES.TOWNHALL_MAXBURG; break;
            case AR_SITE.MAXBURG: currRoute = Wayfinding.ROUTES.MAXBURG_MUSEUM; break;
            case AR_SITE.MUSEUM: currRoute = Wayfinding.ROUTES.MUSEUM_TOWNHALL; break;
            default: currRoute = Wayfinding.ROUTES.NONE; break; 
        }
        ToggleEnterARButton(false);
        EnterMapbox();
    }

    public void ConfigureAR(AR_SITE site, Vector3 POI2PlayerPos)
    {

        currCheckpoint = site;
        arOriginRelativeToPlayer = POI2PlayerPos;
        arSceneName = "";
        ARSiteToScene(site);

        arPossible = true;
    }

    private void ARSiteToScene(AR_SITE site)
    {
        switch (site)
        {
            case AR_SITE.MAXBURG: arSceneName = Schnitzelconstants.MAXBURG_SCENE; break;
            case AR_SITE.OLD_TOWNHALL: arSceneName = Schnitzelconstants.OLD_TOWNHALL_SCENE; break;
            case AR_SITE.MUSEUM: arSceneName = Schnitzelconstants.MUSEUM; break;
            default: break;
        }
    }

    private string ARSiteToTitle(AR_SITE site)
    {
        switch (site)
        {
            case AR_SITE.MAXBURG: return "Maxburg";
            case AR_SITE.OLD_TOWNHALL: return "Old Town Hall";
            case AR_SITE.MUSEUM: return "Stadtmuseum";
            default: return "$undefined$";
        }
    }

    public void EnterMapbox()
    {
        if (currCheckpoint != AR_SITE.OLD_TOWNHALL)
        {
            completedCheckpoints.Add(currCheckpoint); //we need to return to the townhall!
            arPossible = false;
        }

        state = GAMESTATE.WORLD;
        ToggleEnterARButton(arPossible);
        sceneTransitionManager.GoToScene(Schnitzelconstants.WORLD_SCENE, null);
    }

    public void EnterAR()
    {
        sceneTransitionManager.GoToScene(arSceneName, null);
        ToggleEnterARButton(false);
        state = GAMESTATE.AR;
    }

    void ToggleEnterARButton(bool active)
    {
        EnterARButton.enabled = active;
        EnterARButton.image.enabled = active;
        EnterARButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ARSiteToTitle(currCheckpoint);
        EnterARButton.transform.GetChild(0).gameObject.SetActive(active);
        if (active) state = GAMESTATE.WORLD;
    }
}
