using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeOnObject : MonoBehaviour
{

    Vector2 swipeStart = Vector2.zero;
    Vector2 swipeEnd = Vector2.zero;

    [SerializeField]
    float swipeDistance = 200f;
    [SerializeField]
    float depthReach = 3f;

    GameObject objectBeneathSwipe = null;

    bool selectObject = false;
    bool swipeSuccess = false;
    
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
                        if (!swipeSuccess)
                        {
                            swipeEnd = touch.position;
                            RaycastHit hit;
                            Ray ray = Camera.main.ScreenPointToRay(swipeEnd);
                            Debug.Log(selectObject);
                            int mask = LayerMask.GetMask("Swipeable");
                            if (selectObject && Physics.Raycast(ray, out hit, depthReach, mask))
                            {
                                objectBeneathSwipe = hit.transform.gameObject;
                                selectObject = false;
                            }

                            if (Mathf.Abs(swipeStart.x - swipeEnd.x) >= swipeDistance || Mathf.Abs(swipeStart.y - swipeEnd.y) >= swipeDistance)
                            {
                                //Debug.Log(Mathf.Abs(swipeStart.x - swipeEnd.x));
                                if (objectBeneathSwipe != null)
                                {
                                    RubbleHealth rh = objectBeneathSwipe.GetComponent<RubbleHealth>();
                                    if (rh != null)
                                    {
                                        rh.DecreaseHP();
                                        swipeSuccess = true;
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        objectBeneathSwipe = null;
                        swipeSuccess = false;
                        break;
                }
            }
        }
    }
}
