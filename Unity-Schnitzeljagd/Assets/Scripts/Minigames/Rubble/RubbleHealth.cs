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
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private float colorBlinkDuration = .2f;
    [SerializeField]
    private Material destructionMat;
    // Start is called before the first frame update
    void Awake()
    {
        minigame = FindObjectOfType<RubbleMinigame>();
        hp = minigame.rubbleMaxHealth;
        healthBar.InitSlider(hp);
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging cheatcode
       if (Input.GetKeyDown(KeyCode.P))
        {
            DecreaseHP();

        }
    }

    public void DecreaseHP()
    {
        hp--;
        StartCoroutine(HitFeedback());
        healthBar.UpdateSlider(hp);

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
        yield return new WaitForSeconds(colorBlinkDuration);
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
