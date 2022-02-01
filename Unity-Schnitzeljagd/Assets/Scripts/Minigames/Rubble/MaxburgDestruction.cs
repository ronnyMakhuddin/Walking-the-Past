using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxburgDestruction : MonoBehaviour
{
    [Header("Maxburg Versions")]
    [SerializeField]
    GameObject maxburg_missing;
    [SerializeField]
    GameObject maxburg_remain;
    [SerializeField]
    [Header("Destruction Settings")]
    GameObject rubbleReplacement;
    [SerializeField]
    ParticleSystem smoke;    
    [SerializeField]
    ParticleSystem dust;
    [SerializeField]
    float crackedLifetime = 1f;

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
                Instantiate(maxburg_missing, Vector3.zero, Quaternion.identity);
                Instantiate(maxburg_remain, Vector3.zero, Quaternion.identity);
                StartCoroutine(DestroyBrokenMaxburg());

            }
        }
        
        /*
        if(currentStage > materials_stages.Count)
        {
            Instantiate(maxburg_missing, maxburg_whole.transform);
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

    IEnumerator DestroyBrokenMaxburg()
    {
        smoke.Play();
        yield return new WaitForSeconds(crackedLifetime);
        Destroy(maxburg_missing);
        rubbleReplacement.SetActive(true);
        dust.Play();

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
