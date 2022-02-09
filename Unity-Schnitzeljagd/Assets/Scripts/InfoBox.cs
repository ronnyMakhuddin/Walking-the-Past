using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBox : MonoBehaviour
{
    public int id;

    private GameObject infoBox;
    private InfoBoxSystem infoSys;

    // Start is called before the first frame update
    void Start()
    {
        infoBox = GameObject.Find("Quest System");
        infoSys = infoBox.GetComponent<InfoBoxSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && GameManager.Instance.GetGameState() == GameManager.GAMESTATE.WORLD)
        {
            InfoBoxSystem.SetInfo(id);
            infoSys.ActivateButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        infoSys.DeactivateButton();
    }
}
