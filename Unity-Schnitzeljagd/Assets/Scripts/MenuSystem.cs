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
        //filled = new bool[itemSlots.Length];
        texts = new Dictionary<int, QuestText>();

        // only works if json file is already there
        texts = FileManager.LoadQuests(filename);
        Debug.Log("Number texts loaded: " + texts.Keys.Count);
    }
 
    public static Dictionary<int, QuestText> GetTexts()
    {
        return texts;
    }
    
    
    [Header("Item Menu")]
    [SerializeField] private GameObject itemMenuUI;
    [SerializeField] private GameObject[] itemSlots;
    //private bool[] filled;
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
            if (Inventory.filled[i])
            {
                itemSlots[i].GetComponent<Image>().sprite = item.GetSprite();
                Inventory.filled[i] = true;
            }
            else
            {
                Inventory.filled[i] = false;
            }
        }
    }
    
    [Header("Quest Menu")]
    [SerializeField] private QuestSystem questSys;
    [SerializeField] private GameObject questMenuUI;
    [SerializeField] private Text questText;
    [SerializeField] private UnityEngine.UI.Image questImage;
    [SerializeField] private float writingSpeed = 0.5f;

    private Quest currentQuest;
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
        if (QuestSystem.GetMain() != null)
        {
            currentQuest = QuestSystem.GetMain();
        }
        if (QuestSystem.GetSide() != null)
        {
            currentQuest = QuestSystem.GetSide();
        }
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
        if (!Inventory.filled[i])
        {
            Debug.Log("Empty!");
            return;
        }
        Debug.Log("Spawning nr. " + i);
        Debug.Log(Inventory.items[i]);
        Debug.Log(Inventory.items[i].gameObject);
        //Inventory.items[i].gameObject.transform.position = Camera.main.transform.forward * 1f;
        Inventory.items[i].gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.3f
                                                                       ;// 
        Inventory.items[i].transform.localScale = Vector3.one * Inventory.items[i].scaleFactor;
        Inventory.items[i].transform.rotation.Set(0, 0, 0, 0);
        Inventory.items[i].transform.Rotate(Vector3.right, Camera.main.transform.rotation.eulerAngles.y);
        Inventory.items[i].gameObject.SetActive(true);
        itemSlots[i].GetComponent<Image>().sprite = null;
        Inventory.items[i].Select(true);
    }
}
