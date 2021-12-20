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
        if (distance > 0.00000001f)
        {
            transform.Translate(Vector3.forward * walkingSpeed);
            CharacterAnimator.SetBool("IsWalking", true);
        }
        else
        {
            CharacterAnimator.SetBool("IsWalking", false);
        }

        prevPosition = currPosition;
    }
}
