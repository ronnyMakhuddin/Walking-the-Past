using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
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
            Destroy(gameObject);
            GameManager.Instance.ARCompleted();
        }
    }

    IEnumerator HitFeedback()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();
        foreach(Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.material.color = Color.red;
        }
        yield return new WaitForSeconds(.2f);
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.material.color = Color.white;
        }
        ps.Stop();
    }
}
