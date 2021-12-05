using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform story_anchor;
    public Text story_text;
    private string test_text;

    // Start is called before the first frame update
    void Start()
    {
        test_text = "blablablablablablabla \nblablablablablablabla \nblablablablablablabla \nblablablablablablablallllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllll\nblablablablablablabla\nblablablablablablabla\nblablablabla...";
        story_text.gameObject.GetComponent<TypingText>().StartText(test_text, story_text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
