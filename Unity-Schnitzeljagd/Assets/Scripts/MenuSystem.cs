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

// class to help save dicionary to json
[Serializable]
public class QuestText
{
    public int id;
    public int character;
    public string text;
}

public class MenuSystem : MonoBehaviour
{   
    [Header("Dialogue System")]
    [SerializeField] private GameObject dialogueSys;
    private static Dictionary<int, QuestText> texts;
    [SerializeField] private string filename = "questText.json";
    // for debug
    // [SerializeField] private List<QuestText> quests;
    
    private void Awake()
    {
        filled = new bool[itemSlots.Length];
        texts = new Dictionary<int, QuestText>();

        // only works if json file is already there
        texts = FileManager.LoadQuests(filename);
        Debug.Log("Number texts loaded: " + texts.Keys.Count);
    }
    
    // create list of json texts for debug
    /*
    private void Start()
    {

        // List for debugging
        List<int> keys = new List<int>(texts.Keys);
        foreach (var key in keys)
        {
            quests.Add(texts[key]);
        }
    }*/

    public static Dictionary<int, QuestText> GetTexts()
    {
        return texts;
    }
    
    
    [Header("Item Menu")]
    [SerializeField] private GameObject itemMenuUI;
    [SerializeField] private GameObject[] itemSlots;
    private bool[] filled;
    public static bool ItemMenuOpen = false;
    
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
    
    [Header("Quest Menu")]
    [SerializeField] private Quest currentQuest;
    [SerializeField] private GameObject questMenuUI;
    [SerializeField] private Text questText;
    [SerializeField] private UnityEngine.UI.Image questImage;
    [SerializeField] private float writingSpeed = 0.5f;
    
    private bool questMenuOpen = false;
    private String currentText = "";

    public void QuestMenu()
    {
        if (questMenuOpen)
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
        questMenuOpen = false;
    }

    void ShowQuest()
    {
        questMenuUI.SetActive(true);
        questMenuOpen = true;
        questImage.sprite = currentQuest.GetCharacter();
        StartCoroutine(DisplayQuest());
        currentText = "";
    }

    IEnumerator DisplayQuest()
    {
        string text = texts[currentQuest.GetID()].text;
        for (int i = 0; i < text.Length; ++i)
        {
            currentText = text.Substring(0, i+1);
            questText.text = currentText;
            yield return new WaitForSeconds(writingSpeed);
        }
    }

    // respawning item into the scene
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
