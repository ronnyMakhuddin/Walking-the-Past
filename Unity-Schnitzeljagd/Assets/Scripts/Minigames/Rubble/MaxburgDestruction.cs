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
    GameObject smoke;    
    [SerializeField]
    ParticleSystem dust;
    [SerializeField]
    float crackedLifetime = 1.5f;

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
        if (!allStagesDone && currentStage < maxburg_visual_states.Count)
        {
            Debug.Log("Set State");
            GameObject toSpawn = maxburg_visual_states[currentStage];

            GameObject spawned = Instantiate(toSpawn, Vector3.zero, Quaternion.Euler(-90f, 0f, 0f), transform.parent);
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
                maxburg_missing = Instantiate(maxburg_missing, Vector3.zero, Quaternion.Euler(-90f, 0f, 0f), transform.parent);
                Instantiate(maxburg_remain, Vector3.zero, Quaternion.Euler(-90f, 0f, 0f), transform.parent);
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
        ParticleSystem[] smoke_effects = smoke.GetComponentsInChildren<ParticleSystem>();
        List<float> lifetime = new List<float>();

        foreach (ParticleSystem effect in smoke_effects) {
            effect.Play();
            lifetime.Add(effect.main.duration);
        }

        float smoke_duration = Mathf.Max(lifetime.ToArray());
        //Make rubble appear
        rubbleReplacement.SetActive(true);
        foreach (Collider c in rubbleReplacement.GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }


        //Hide maxburg
        StartCoroutine(Fade(smoke_duration, true));
        //Has rendering order issues
        //StartCoroutine(Fade(smoke_duration, false));

        yield return new WaitForSeconds(smoke_duration);

        Destroy(maxburg_missing);
        dust.gameObject.SetActive(true);

    }

    IEnumerator Fade(float duration, bool fadeOut)
    {
        float time = 0f;
        GameObject obj = fadeOut ? maxburg_missing : rubbleReplacement;
        while(time < duration)
        {
            float interpolation = Mathf.InverseLerp(0f, duration, time);
            float alpha = fadeOut ? 1 - interpolation : interpolation;
            foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>())
            {
                Color col = renderer.material.color;
                col.a = alpha;
                renderer.material.color = col;
                //Debug.Log(renderer.material.color);
            }
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
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
