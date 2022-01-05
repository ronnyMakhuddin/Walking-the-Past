using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Object = UnityEngine.Object;

[Serializable]
public struct QuestText
{
    public int id;
    public string text;
}

public class MenuSystem : MonoBehaviour
{
    public static bool ItemMenuOpen = false;
    public GameObject itemMenuUI;

    [SerializeField] private GameObject[] itemSlots;
    private bool[] filled;
    
    private Dictionary<int, QuestText> texts;
    // serialized for debug
    [SerializeField] private List<QuestText> quests;
    [SerializeField] private string filename = "questText.json";
    

    private void Start()
    {
        filled = new bool[itemSlots.Length];
        texts = new Dictionary<int, QuestText>();
        quests = new List<QuestText>();
        
        // only works if json file is already there
        texts = FileManager.LoadQuests(filename);
        
        // List for debugging
        List<int> keys = new List<int>(texts.Keys);
        foreach (var key in keys)
        {
            quests.Add(texts[key]);
        }
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
        questImage.sprite = currentQuest.GetCharacter();
        StartCoroutine(DisplayQuest());
        currentText = "";
    }

    IEnumerator DisplayQuest()
    {
        string text = texts[currentQuest.GetID()].text;
        for (int i = 0; i < text.Length; ++i)
        {
            currentText = text.Substring(0, i);
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
        Debug.Log(Inventory.items[i]);
        Debug.Log(Inventory.items[i].gameObject);
        Inventory.items[i].gameObject.transform.position = Vector3.forward * 5;
        Inventory.items[i].transform.position = Vector3.zero + Inventory.items[i].gameObject.transform.position;
        Inventory.items[i].gameObject.SetActive(true);
        itemSlots[i].GetComponent<Image>().sprite = null;
        Inventory.items[i].Select(true);
    }
}
