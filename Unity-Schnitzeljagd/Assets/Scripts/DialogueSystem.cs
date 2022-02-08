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

    [SerializeField] private List<GameObject> pictures;
    
    [SerializeField] private GameObject pictureField;

    [SerializeField] private GameObject spriteField;

    private Dictionary<int, QuestText> texts;
    
    private int index = 10;
    private bool dialogueRunning;
    private bool pictureShown = false;
    private GameObject currentPic;
    [SerializeField] private float writingSpeed = 0.05f;

    private void OnEnable()
    {
        texts = MenuSystem.GetTexts();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunning = false;
        textfield.text = String.Empty;
        StartDialogue(QuestSystem.GetMain().getDialogueStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueRunning && ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began ) || Input.GetMouseButtonDown(0)))
        {
            if (pictureShown)
            {
                pictureShown = false;
                pictureField.SetActive(false);
                currentPic.SetActive(false);
                NextParagraph();
                return;
            }
            
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

    public void StartDialogue(int start)
    {
        gameObject.SetActive(true);
        dialogueRunning = true;
        index = start;
        if (texts[index].character < 0)
        {
            spriteField.SetActive(false);
        }
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
        int picture = texts[index].character;
        if (picture <= 0)
        {
            for (int i = 0; i < text.Length; ++i)
            {
                current = text.Substring(0, i+1);
                textfield.text = current;
                yield return new WaitForSeconds(writingSpeed);
            }
        }
        else if (picture <= pictures.Count)
        {
            ShowPicture(picture);
        }
    }

    private void NextParagraph()
    {
        texts.TryGetValue(++index, out var test);
        if (test != null)
        {
            textfield.text = string.Empty;
            StartCoroutine(TypeParagraph());
        }
        else
        {
            spriteField.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void ShowPicture(int index)
    {
        pictureShown = true;
        pictureField.SetActive(true);
        currentPic = pictures[index - 1];
        currentPic.SetActive(true);
    }
    
}
