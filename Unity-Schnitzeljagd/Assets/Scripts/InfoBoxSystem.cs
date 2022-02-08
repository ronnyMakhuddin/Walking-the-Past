using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(GameManager.Instance.GetGameState());
        if(GameManager.Instance.GetGameState() == GameManager.GAMESTATE.AR)
            DeactivateButton();
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
        infoButton.SetActive(false);
        diaSys.StartDialogue(currentInfo);
        DeactivateButton();
    }
}
