using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Wayfinding : MonoBehaviour
{
    LineRenderer lineRenderer;
    List<Transform> activeCheckpoints;
    Transform player;

    public Wayfinding.ROUTES ROUTE;

    public float yOffset = 2f;
    void Awake()
    {
        Wayfinding.ROUTES currRoute = GameManager.Instance.GetCurrentRoute();
        Debug.Log(currRoute);
        if (currRoute != ROUTE || currRoute == ROUTES.NONE)
        {
            //Debug.Log("Disable: " + gameObject.name);
            gameObject.SetActive(false);
        }
        else
        {
            lineRenderer = GetComponent<LineRenderer>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            ConfigureLines();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //mapbox automatically snaps y position to zero -> we need to enforce it by code. This avoids rendering issues ("y-fighting"?)
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        ConfigureLines();
    }
    /// <summary>
    /// Configures LineRenderer positions array
    /// </summary>
    void ConfigureLines()
    {
        activeCheckpoints = new List<Transform>();
        activeCheckpoints.Add(player);
        activeCheckpoints.AddRange(GetComponentsInChildren<Transform>(false));
        activeCheckpoints.Remove(activeCheckpoints[1]); //get rid of own transform
        lineRenderer.positionCount = activeCheckpoints.Count;
        SetPoints();
    }

    void SetPoints()
    {
        Vector3 playerPos = new Vector3(player.position.x, 1, player.position.z);
        lineRenderer.SetPosition(0, playerPos);

        for(int c=0; c<activeCheckpoints.Count; c++)
        {
            lineRenderer.SetPosition(c, activeCheckpoints[c].position);
        }
    }
    /// <summary>
    /// A container for all possible routes.
    /// </summary>
    public enum ROUTES
    {
        TOWNHALL_MAXBURG,
        MAXBURG_MUSEUM,
        MUSEUM_TOWNHALL,
        NONE
    }


}
