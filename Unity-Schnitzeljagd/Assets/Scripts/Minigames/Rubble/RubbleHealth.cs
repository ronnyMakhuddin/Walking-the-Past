using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RubbleHealth : MonoBehaviour
{
    private int hp = 3;
    public bool special = false;
    public GameObject toSpawn;
    private Color feedbackCol = Color.grey;
    RubbleMinigame minigame;
    // Start is called before the first frame update
    void Awake()
    {
        minigame = FindObjectOfType<RubbleMinigame>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging cheatcode
       if (Input.GetKeyDown(KeyCode.P))
        {
            DecreaseHP();
            DecreaseHP();
            DecreaseHP();
        }
    }

    public void DecreaseHP()
    {
        hp--;
        StartCoroutine(HitFeedback());

        if(hp <= 0)
        {
            //Maybe a nice animation?
            HandleDestruction();
        }
    }

    IEnumerator HitFeedback()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();
        foreach(Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.material.color = feedbackCol;
        }
        yield return new WaitForSeconds(.2f);
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.material.color = Color.white;
        }
        ps.Stop();
    }

    void HandleDestruction()
    {
        minigame.RemoveRubble(this);
        Instantiate(toSpawn, gameObject.transform.parent);
        Destroy(gameObject);
    }
}
