using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int inventorySize = 8;
    public static QuestItem[] items = new QuestItem[inventorySize];
    public static bool[] filled = new bool[inventorySize];
    private static int itemCount = 0;

    public static bool AddItem(QuestItem item)
    {
        Debug.Log("Adding Item: " + item);
        for (int i = 0; i < items.Length; ++i)
        {
            if (!filled[i])
            {
                items[i] = item;
                itemCount += 1;
                filled[i] = true;
                return true;
            }
        }
        return false;
    }
    
    public bool RemoveItem(QuestItem item)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] == item)
            {
                items[i] = null;
                itemCount -= 1;
                filled[i] = false;
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
        filled[i] = false;
        return true;
    }

    public static int getCount()
    {
        return itemCount;
    }
}
