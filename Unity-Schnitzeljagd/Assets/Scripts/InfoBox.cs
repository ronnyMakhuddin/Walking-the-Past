using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBox : MonoBehaviour
{
    public int id;

    private GameObject dialogeBox;
    private DialogueSystem diaSys;

    // Start is called before the first frame update
    void Start()
    {
        dialogeBox = GameObject.Find("Dialogue Box");
        diaSys = dialogeBox.GetComponent<DialogueSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        diaSys.StartDialogue(id);
    }
}
