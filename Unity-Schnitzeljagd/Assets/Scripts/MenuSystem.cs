using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MenuSystem : MonoBehaviour
{
    public static bool ItemMenuOpen = false;
    public GameObject itemMenuUI;

    [SerializeField] private GameObject[] itemSlots;
    private bool[] filled;
    
    private float width;
    private float height;

    private void Start()
    {
        filled = new bool[itemSlots.Length];
        width = (float)Screen.width / 2.0f;
        height = (float) Screen.height / 2.0f;
    }
    

    public void ItemMenu()
    {
        if (ItemMenuOpen)
        {
            CloseItemMenu();
        }
        else
        {
            ShowItems();
        }
    }
    
    void CloseItemMenu()
    {
        itemMenuUI.SetActive(false);
        ItemMenuOpen = false;
    }
    
    void ShowItems()
    {
        itemMenuUI.SetActive(true);
        ItemMenuOpen = true;
        DisplayItems();
    }

    public void DisplayItems()
    {
        for (int i = 0; i < itemSlots.Length && i < Inventory.getCount(); ++i)
        {
            QuestItem item = Inventory.items[i];
            if (item != null)
            {
                Debug.Log("Item in menu! Nr. " + i);
                itemSlots[i].GetComponent<Image>().sprite = item.GetSprite();
                Debug.Log("Setting sprite nr. " + i);
                filled[i] = true;
            }
            else
            {
                filled[i] = false;
            }
        }
    }
    
    
    public static bool QuestMenuOpen = false;
    [SerializeField] private Quest currentQuest;
    public GameObject questMenuUI;
    public Text questText;
    public UnityEngine.UI.Image questImage;
    [SerializeField] private float writingSpeed = 0.5f;
    private String currentText = "";

    public void QuestMenu()
    {
        if (QuestMenuOpen)
        {
            CloseQuestMenu();
        }
        else
        {
            ShowQuest();
        }
    }
    
    void CloseQuestMenu()
    {
        questMenuUI.SetActive(false);
        QuestMenuOpen = false;
    }

    void ShowQuest()
    {
        questMenuUI.SetActive(true);
        // pause the game
        QuestMenuOpen = true;
        questImage.sprite = currentQuest.getCharacter();
        StartCoroutine(DisplayQuest());
        currentText = "";
    }

    IEnumerator DisplayQuest()
    {
        for (int i = 0; i < currentQuest.getTask().Length; ++i)
        {
            currentText = currentQuest.getTask().Substring(0, i);
            questText.text = currentText;
            yield return new WaitForSeconds(writingSpeed);
        }
    }

    public void Respawn(int i)
    {
        if (!filled[i])
        {
            Debug.Log("Empty!");
            return;
        }
        Debug.Log("Spawning nr. " + i);
        Debug.Log(Inventory.items[i].GetItem3D());
        Debug.Log(Inventory.items[i].gameObject);
        Inventory.items[i].gameObject.transform.position = Vector3.forward * 5;
        Inventory.items[i].GetItem3D().transform.position = Vector3.zero + Inventory.items[i].gameObject.transform.position;
        Inventory.items[i].GetItem3D().SetActive(true);
        itemSlots[i].GetComponent<Image>().sprite = null;
        Inventory.items[i].Select(true);
    }
}
