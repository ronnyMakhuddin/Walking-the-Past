using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestItem : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject trigger;
    private MenuSystem menuSystem;
    private float maxDistanceOnSelection = 100f;
    private Vector2 touchposition;
    
    // Serialized for debugging
    [Header ("Serialized for Debug only")]
    [SerializeField] bool collected = false;
    [SerializeField] bool selected = false;
    [SerializeField] bool moving = false;

    private float width;
    private float height;
    private Vector2 pos = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width / 2.0f;
        height = (float) Screen.height / 2.0f;
        menuSystem = FindObjectOfType<MenuSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!collected) {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                pos = touch.position;
                pos.x = (pos.x - width) / width;
                pos.y = (pos.y - height) / height;
                // var position = new Vector3(-pos.x, pos.y, 0.0f);
                Vector3 touchPosNear = new Vector3(touch.position.x, touch.position.y,
                    Camera.main.nearClipPlane);
                Vector3 touchPosFar = new Vector3(touch.position.x, touch.position.y,
                    Camera.main.farClipPlane);
                Vector3 worldNear = Camera.main.ScreenToWorldPoint(touchPosNear);
                Vector3 worldFar = Camera.main.ScreenToWorldPoint(touchPosFar);
                RaycastHit hit;
                Physics.Raycast(worldNear, worldFar - worldNear, out hit);
                
                
                if (hit.collider != null)
                {
                    Debug.Log("Hit something!");
                    //hit.collider.gameObject.CompareTag("Item")
                    if (hit.collider.gameObject == this.gameObject && !collected)
                    {
                        Debug.Log("Hit uncollected item!");
                         bool added = Inventory.AddItem(this);
                         collected = true;
                         this.gameObject.SetActive(false);
                    }
                }
            }
        } else if (selected) {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:

                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit hitObject;

                        if (Physics.Raycast(ray, out hitObject, maxDistanceOnSelection))
                        {
                            if (hitObject.collider.gameObject == this.gameObject)
                            {
                                Debug.Log("Object detected!");
                                moving = true;
                            }
                        }

                        break;
            
                    case TouchPhase.Moved:
                        touchposition = touch.position;
                        break;
                            
                    case TouchPhase.Ended:
                        if (moving)
                        {
                            Debug.Log("Item back into inventory");
                            moving = false;
                            // back into inventory
                            selected = false;
                            this.gameObject.SetActive(false);
                            menuSystem.DisplayItems();
                                       
                        }
                        break;
                }
                
                if (moving)
                {
                    Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, Camera.main.WorldToScreenPoint(gameObject.transform.position).z
                        );//
                    Vector3 toMove = Camera.main.ScreenToWorldPoint(touchPos);
                    this.gameObject.transform.position = toMove;
                    
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;

                    LayerMask mask = LayerMask.GetMask("TriggerZone");
                    if (Physics.Raycast(ray, out hitObject, maxDistanceOnSelection, mask)) // 
                    {
                        if (hitObject.collider.isTrigger && hitObject.Equals(trigger))
                        {
                            Debug.Log("trigger");
                            TriggerZoneEntered();
                        }
                    }
                }
            }
        }
    }

    public void TriggerZoneEntered()
    {
        selected = false;
        moving = false;
        //Handheld.Vibrate();
        VibrationTypes.OnTapVibrate(true);
        StartCoroutine(Freeze());
        Inventory.RemoveItem(this);
        this.gameObject.SetActive(false);
    }

    public IEnumerator Freeze()
    {
        yield return new WaitForSeconds(1);
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public bool Collect()
    {
        if (!collected)
        {
            collected = true;
            return true;
        }
        return false;
    }
    
    public void Select(bool select)
    {
        selected = select;
    }

}
