using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderSample : MonoBehaviour
{
    public GameObject mainObj;
    public Slider slider;
    [TextArea] public string floatReference;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();        
    }

    // Update is called once per frame
    void Update()
    {
        mainObj.GetComponent<Renderer>().material.SetFloat(floatReference, slider.value);
    }
}
