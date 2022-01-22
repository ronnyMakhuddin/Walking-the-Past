using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator CharacterAnimator;
    public float walkingSpeed = 1;
    bool walking = false;
    Vector3 prevPosition = Vector3.zero;
    Vector3 currPosition = Vector3.zero;
    // Start is called before the first frame update
    private void Awake()
    {
        prevPosition = Vector3.zero;
        currPosition = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currPosition = transform.position;
        float distance = Mathf.Abs(Vector3.Distance(currPosition, prevPosition));

        if (!walking)
        {
            if (distance > 0.01f)
            {
                StartCoroutine(StartWalking());
            }
            else
            {
                StartCoroutine(StopWalking());
            }
        }
        prevPosition = currPosition;

    }

    IEnumerator StopWalking()
    {
        CharacterAnimator.SetBool("IsWalking", false);
        walking = false;
        yield return null;
    }

    IEnumerator StartWalking()
    {
        CharacterAnimator.SetBool("IsWalking", true);
        walking = true;
        yield return new WaitForSeconds(2f);
        walking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Path")
        {
            Debug.Log("Path found");
            other.gameObject.SetActive(false);
        }
    }
}
