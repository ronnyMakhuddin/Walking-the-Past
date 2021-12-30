using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private GameObject item2D;
    [SerializeField] private Sprite sprite;
    private Rigidbody rb;
    private bool move = false;
    // Serialized for debugging
    [SerializeField] private bool collected = false;

    private float width;
    private float height;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        width = (float)Screen.width / 2.0f;
        height = (float) Screen.height / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("Screen pos: " + touch.position);

            Vector2 pos = touch.position;
            pos.x = (pos.x - width) / width;
            pos.y = (pos.y - height) / height;
            var position = new Vector3(-pos.x, pos.y, 0.0f);
            Debug.Log("scaled pos: " + position);
            
            RaycastHit hit;
            Physics.Raycast(position, Vector3.forward, out hit);

            if (hit.collider != null)
            {
                Debug.Log("Hit object with collider!");
                bool added = Inventory.AddItem(this);
                Debug.Log("added: " + added);
                collected = true;
                this.gameObject.SetActive(false);
                // Destroy(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("Hit nothing!");
            }
        }
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public GameObject GetItem2D()
    {
        return item2D;
    }
    
    
}
