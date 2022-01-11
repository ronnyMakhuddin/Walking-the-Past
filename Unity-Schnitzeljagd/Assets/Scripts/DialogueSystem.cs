using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textfield;
    private Dictionary<int, QuestText> texts = new Dictionary<int, QuestText>();
    private int end = 20;
    private int index = 10;
    [SerializeField] private float writingSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        textfield.text = String.Empty;
        texts = MenuSystem.GetTexts();
        StartDialogue(10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
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

    void StartDialogue(int start, int stop)
    {
        index = start;
        end = stop;
        StartCoroutine(TypeParagraph());
    }

    IEnumerator TypeParagraph()
    {
        string current = "";
        string text = texts[index].text;
        for (int i = 0; i < text.Length; ++i)
        {
            current = text.Substring(0, i+1);
            textfield.text = current;
            yield return new WaitForSeconds(writingSpeed);
        }
    }

    void NextParagraph()
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
