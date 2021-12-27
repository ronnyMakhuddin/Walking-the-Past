using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleHealth : MonoBehaviour
{
    private int hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHP()
    {
        hp--;
        StartCoroutine(HitFeedback());

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator HitFeedback()
    {
        foreach(Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.material.color = Color.red;
        }
        yield return new WaitForSeconds(.2f);
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.material.color = Color.white;
        }
    }
}
