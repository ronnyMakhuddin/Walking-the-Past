using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{
    private float writing_speed = 50;
    public void StartText(string inputText, Text target)
    {
        StartCoroutine(Typing(inputText, target));
    }

    private IEnumerator Typing(string inputText, Text target)
    {
        yield return new WaitForSeconds(1.5f);

        float t_passed = 0;
        int num_chars_typed = 0;

        while(num_chars_typed < inputText.Length)
        {
            t_passed += writing_speed * Time.deltaTime;
            num_chars_typed = Mathf.Clamp(Mathf.FloorToInt(t_passed), 0, inputText.Length);

            target.text = inputText.Substring(0, num_chars_typed);

            yield return null;
        }

        target.text = inputText;
    }

    public void SetWritingSpeed(int speed)
    {
        writing_speed = speed;
    }
}
