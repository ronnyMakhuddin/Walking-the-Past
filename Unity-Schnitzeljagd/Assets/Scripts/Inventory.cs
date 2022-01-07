using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static QuestItem[] items = new QuestItem[3];
    private static int itemCount = 0;

    public static bool AddItem(QuestItem item)
    {
        Debug.Log("Adding Item: " + item);
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
    
    public bool RemoveItem(QuestItem item)
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
