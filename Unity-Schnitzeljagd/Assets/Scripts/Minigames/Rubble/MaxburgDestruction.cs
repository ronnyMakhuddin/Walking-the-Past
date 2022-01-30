using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxburgDestruction : MonoBehaviour
{
    [Header("Maxburg Versions")]
    public GameObject maxburg_shattered;
    public GameObject maxburg_remain;

    [Header("Cracked Versions")]
    public List<GameObject> maxburg_visual_states;
    //public MaterialStages materials_stages;

    int currentStage = -1;
    GameObject currentVersion;

    bool allStagesDone;

    public void Awake()
    {
        ProgressState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ProgressState();
        }
    }

    public void ProgressState()
    {
        currentStage++;
        Debug.Log(currentStage);
        if (!allStagesDone && currentStage < maxburg_visual_states.Count)
        {
            Debug.Log("Set State");
            GameObject toSpawn = maxburg_visual_states[currentStage];

            GameObject spawned = Instantiate(toSpawn, Vector3.zero, Quaternion.identity);
            if (currentStage != 0)
            {
                Destroy(currentVersion);
            }
            currentVersion = spawned;
        }
        else
        {
            if (!allStagesDone)
            {
                allStagesDone = true;
                Destroy(currentVersion);
                Instantiate(maxburg_shattered, Vector3.zero, Quaternion.identity);
                Instantiate(maxburg_remain, Vector3.zero, Quaternion.identity);
            }
        }
        
        /*
        if(currentStage > materials_stages.Count)
        {
            Instantiate(maxburg_shattered, maxburg_whole.transform);
            Instantiate(maxburg_remain, maxburg_whole.transform);
            Destroy(maxburg_whole);
        }
        else
        {
            // List<Material> updatedMats = materials_stages[currentStage - 1];
            //maxburg_whole.GetComponent<Renderer>().material = materials[stage];
        }
        */
    }
}


/*
[System.Serializable]
public class MaterialList
{
    public List<Material> list_of_materials;
}

[System.Serializable]
public class MaterialStages
{
    public List<MaterialList> material_stages;

}
*/
