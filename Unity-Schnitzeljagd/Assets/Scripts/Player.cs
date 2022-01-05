using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator CharacterAnimator;
    public float walkingSpeed = 1;
    Vector3 prevPosition = Vector3.zero;
    Vector3 currPosition = Vector3.zero;
    // Start is called before the first frame update
    private void Awake()
    {
        prevPosition = Vector3.zero;
        currPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        currPosition = transform.position;
        float distance = Mathf.Abs(Vector3.Distance(currPosition, prevPosition));
        CharacterAnimator.SetBool("IsWalking", true);

        if (distance != 0f)
        {
            //transform.Translate(Vector3.forward * walkingSpeed);
            CharacterAnimator.SetBool("IsWalking", true);
        }
        else
        {
            StartCoroutine(StopWalking());
        }

        prevPosition = currPosition;
    }

    IEnumerator StopWalking()
    {
        yield return new WaitForSeconds(2f);
        CharacterAnimator.SetBool("IsWalking", false);
    }
}
