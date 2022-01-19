using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using Image = UnityEngine.UI.Image;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textfield;
    [SerializeField] private Image spritefield;
    [SerializeField] private Sprite spriteKarl;
    [SerializeField] private Sprite spriteOtto;
    private Dictionary<int, QuestText> texts;
    
    private int end = 20;
    private int index = 10;
    private bool dialogueRunning;
    [SerializeField] private float writingSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunning = false;
        textfield.text = String.Empty;
        texts = MenuSystem.GetTexts();
        StartDialogue(10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueRunning && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            QuestText text = texts[index];
            if (text == null)
            {
                Debug.Log("Cannot get text at index " + index);
            }

            if (!String.IsNullOrEmpty(textfield.text) && textfield.text == texts[index].text)
            {
                NextParagraph();
            }
            else
            {
                StopAllCoroutines();
                textfield.text = texts[index].text;
            }
        }
    }

    public void StartDialogue(int start, int stop)
    {
        this.gameObject.SetActive(true);
        dialogueRunning = true;
        index = start;
        end = stop;
        StartCoroutine(TypeParagraph());
    }

    IEnumerator TypeParagraph()
    {
        if (texts[index] == null)
        {
            Debug.Log("No text saved under index " + index);
            yield break;
        }
        
        string current = "";
        string text = texts[index].text;
        int character = texts[index].character;

        switch (character)
        {
            case 0: spritefield.sprite = spriteKarl; break;
            case 1: spritefield.sprite = spriteOtto; break;
            default: Debug.Log("Could not find sprite."); break;
        }
        
        for (int i = 0; i < text.Length; ++i)
        {
            current = text.Substring(0, i+1);
            textfield.text = current;
            yield return new WaitForSeconds(writingSpeed);
        }
    }

    private void NextParagraph()
    {
        if (index < end - 1)
        {
            ++index;
            textfield.text = string.Empty;
            StartCoroutine(TypeParagraph());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
}
