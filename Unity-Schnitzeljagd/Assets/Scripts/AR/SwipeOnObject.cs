using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeOnObject : MonoBehaviour
{

    Vector2 swipeStart = Vector2.zero;
    Vector2 swipeEnd = Vector2.zero;

    [SerializeField]
    float swipeDistance = 50f;
    [SerializeField]
    float depthReach = 3f;

    GameObject objectBeneathSwipe = null;
    bool selectObject = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        selectObject = true;
                        swipeStart = swipeEnd = touch.position; 
                        break;
                    case TouchPhase.Moved:
                        swipeEnd = touch.position;
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(swipeEnd);

                        if (selectObject && Physics.Raycast(ray, out hit, depthReach, 6))
                        {
                            objectBeneathSwipe = hit.transform.gameObject;
                            selectObject = false;
                        }

                        if(Mathf.Abs(swipeStart.x - swipeEnd.x) >= swipeDistance)
                        {
                            if(objectBeneathSwipe != null)
                            {
                                objectBeneathSwipe.Destroy();
                            }
                        }
                        break;
                    default:
                        objectBeneathSwipe = null;
                        break;
                }
            }
        }
    }
}
