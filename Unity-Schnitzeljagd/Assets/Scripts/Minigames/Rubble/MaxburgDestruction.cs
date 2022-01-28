using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxburgDestruction : MonoBehaviour
{
    [Header("Maxburg Versions")]
    public GameObject maxburg_whole;
    public GameObject maxburg_shattered;

    [Header("Cracked versions")]
    public List<Material> materials;

    int currentMatId = 0;

    public void SetStage(int stage)
    {
        if(stage > materials.Count)
        {
            Instantiate(maxburg_shattered, maxburg_whole.transform);
            Destroy(maxburg_whole);
        }
        else
        {
            maxburg_whole.GetComponent<Renderer>().material = materials[stage];
        }
    }
}
