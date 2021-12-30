using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    [SerializeField] private GameObject item3D;
    [SerializeField] private GameObject item2D;

    public GameObject GetItem3D()
    {
        return item3D;
    }
    
    public GameObject GetItem2D()
    {
        return item2D;
    }

    public void switchTo3D()
    {
        item2D.SetActive(false);
        item3D.SetActive(true);
    }
    
    public void switchTo2D()
    {
        item3D.SetActive(false);
        item2D.SetActive(true);
    }
}
