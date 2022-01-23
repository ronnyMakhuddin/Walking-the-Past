using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Wayfinding : MonoBehaviour
{
    LineRenderer lineRenderer;
    List<Transform> activeCheckpoints;
    Transform player;
    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ConfigureLines();
    }

    // Update is called once per frame
    void Update()
    {
        ConfigureLines();
    }

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


}