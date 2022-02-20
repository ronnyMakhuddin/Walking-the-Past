using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RubbleHealth : MonoBehaviour
{
    private int hp = 0;
    public bool special = false;
    public List<GameObject> toSpawn;
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
            HandleDestruction();
        }
    }


    IEnumerator HitFeedback()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        //vibration
        VibrationTypes.OnSwipeVibrate(true);
        //smoke
        ps.Play();
        //color flash
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
        //spawn a random pipe object
        int id = Random.Range(0, toSpawn.Count);
        Quaternion rot = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90);
        Instantiate(toSpawn[id], transform.position, rot);
        Destroy(gameObject);
    }
}
