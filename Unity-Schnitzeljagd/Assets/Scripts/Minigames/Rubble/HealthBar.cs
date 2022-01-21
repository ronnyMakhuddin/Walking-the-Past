using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Transform arCam;
    public Slider slider;
    public TextMeshProUGUI hpText;

    // Start is called before the first frame update
    void Awake()
    {
        arCam = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(arCam);
    }

    public void InitSlider(int maxHP)
    {
        slider.maxValue = maxHP;
        slider.value = maxHP;
        hpText.text = (int)slider.value + "/" + slider.maxValue;
    }

    public void UpdateSlider(int hpToSet)
    {
        slider.value = hpToSet;
        hpText.text = (int)slider.value + "/" + slider.maxValue;
    }

}
