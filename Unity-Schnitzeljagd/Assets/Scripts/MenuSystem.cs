using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MenuSystem : MonoBehaviour
{
    public static bool ItemMenuOpen = false;
    public GameObject itemMenuUI;

    [SerializeField] private GameObject[] sprites;

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

    void DisplayItems()
    {
        for (int i = 0; i < sprites.Length && i < Inventory.getCount(); ++i)
        {
            Collectible item = Inventory.items[i];
            if (item != null)
            {
                sprites[i].GetComponent<Image>().sprite = item.GetItem2D().GetComponent<SpriteRenderer>().sprite;
                Debug.Log("Setting sprite nr. " + i);
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
    
    void DisplayQuestz()
    {
        questText.text = currentQuest.getTask();
    }
 
}
