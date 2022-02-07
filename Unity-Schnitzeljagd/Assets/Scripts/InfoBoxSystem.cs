using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBoxSystem : MonoBehaviour
{
    public GameObject infoButton;
    
    private GameObject dialogeBox;
    private DialogueSystem diaSys;

    public static int currentInfo = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogeBox = GameObject.Find("Dialogue Box");
        diaSys = dialogeBox.GetComponent<DialogueSystem>();
    }

    public static void SetInfo(int id)
    {
        currentInfo = id;
    }

    public void ActivateButton()
    {
        infoButton.SetActive(true);
    }

    public void DeactivateButton()
    {
        infoButton.SetActive(false);
    }

    public void DisplayInfo()
    {
        diaSys.StartDialogue(currentInfo);
    }
}
