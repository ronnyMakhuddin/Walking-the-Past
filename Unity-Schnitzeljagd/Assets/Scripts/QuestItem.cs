using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    [SerializeField] private GameObject item3D;
    [SerializeField] private Sprite sprite;
    private MenuSystem menuSystem;
    
    // Serialized for debugging
    [SerializeField] bool collected = false;
    [SerializeField] bool selected = false;
    [SerializeField] bool moving = false;

    private float width;
    private float height;
    private Vector2 pos = Vector2.zero;
    private Vector2 oldPos;
    [SerializeField] private float factor = 5f;
    
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
                var position = new Vector3(-pos.x, pos.y, 0.0f);

                RaycastHit hit;
                Physics.Raycast(position, Vector3.forward, out hit);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("Item") && !collected)
                    {
                         bool added = Inventory.AddItem(this);
                         collected = true;
                         item3D.gameObject.SetActive(false);
                    }
                }
            }
        } else if (selected) {
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        oldPos = pos;
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
                                    item3D.SetActive(false);
                                    menuSystem.DisplayItems();
                                       
                                }
                                break;
                        }
                        
                    }
        }
    }

    public GameObject GetItem3D()
    {
        return item3D;
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
