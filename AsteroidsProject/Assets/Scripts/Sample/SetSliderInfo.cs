using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderInfo : MonoBehaviour
{
    public Slider slider;
    public Text sliderValue;
    void Start()
    {
        sliderValue = GetComponent<Text>();
        sliderValue.text = slider.value.ToString();
    }
    void Update()
    {
        sliderValue.text = slider.value.ToString();
    }
}
