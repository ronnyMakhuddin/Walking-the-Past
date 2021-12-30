using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // serialized for debugging
    [SerializeField] public static Collectible[] items = new Collectible[3];
    private static int itemCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool AddItem(Collectible item)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] == null)
            {
                items[i] = item;
                itemCount += 1;
                return true;
            }
        }
        return false;
    }
    
    public bool RemoveItem(GameObject item)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] == item)
            {
                items[i] = null;
                itemCount -= 1;
                return true;
            }
        }
        return false;
    }

    public static bool RemoveItem(int i)
    {
        if (items[i] == null)
        {
            return false;
        }
        items[i] = null;
        return true;
    }

    public static int getCount()
    {
        return itemCount;
    }
}
