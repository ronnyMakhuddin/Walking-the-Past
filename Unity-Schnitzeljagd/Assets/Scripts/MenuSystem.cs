using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuSystem : MonoBehaviour
{
    public static bool ItemMenuOpen = false;
    public GameObject itemMenuUI;

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
        
    }
    
    
    public static bool QuestMenuOpen = false;
    [SerializeField] private Quest currentQuest;
    public GameObject questMenuUI;
    public Text questText;
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
