using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    private MenuSystem menuSystem;
    
    // Serialized for debugging
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
                Debug.Log("Touch position: " + touchPosNear);
                Vector3 worldNear = Camera.main.ScreenToWorldPoint(touchPosNear);
                Vector3 worldFar = Camera.main.ScreenToWorldPoint(touchPosFar);
                Debug.Log("To world: " + worldNear);
                RaycastHit hit;
                Physics.Raycast(worldNear, worldFar - worldNear, out hit);
                
                
                if (hit.collider != null)
                {
                    Debug.Log("Hit something!");
                    if (hit.collider.gameObject.CompareTag("Item") && !collected)
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
                        pos = touch.position;
                        pos.x = (pos.x - width) / width;
                        pos.y = (pos.y - height) / height;
                        var position = new Vector3(-pos.x, pos.y, 0.0f);
        
                        RaycastHit hit;
                        Physics.Raycast(position, Vector3.forward, out hit);
        
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
        
                                if (hit.collider != null && hit.collider.gameObject.Equals(this.gameObject))
                                {
                                    Debug.Log("Selected item");
                                    moving = true;
                                }
        
                                break;
            
                            case TouchPhase.Moved:
                                if (moving)
                                {
                                    Vector3 touchPos = new Vector3(touch.position.x, touch.position.y,
                                        Camera.main.WorldToScreenPoint(gameObject.transform.position).z);
                                    Vector3 toMove = Camera.main.ScreenToWorldPoint(touchPos);
                                    this.gameObject.transform.position = toMove;
                                }
        
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
                        
                    }
        }
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
