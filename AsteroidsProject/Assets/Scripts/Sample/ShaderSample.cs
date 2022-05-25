using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderSample : MonoBehaviour
{
    public GameObject mainObj;
    public Slider slider;
    [TextArea] public string floatReference;
    public bool isBackground;
    // Start is called before the first frame update
    void Start()
    {
        if (isBackground)
        {
            mainObj.GetComponent<Renderer>().material.SetVector("Speed_", Vector4.zero);
        }        
        slider = GetComponent<Slider>();        
    }

    // Update is called once per frame
    void Update()
    {        
        if (isBackground)
        {
            mainObj.GetComponent<Renderer>().material.SetVector(floatReference, new Vector4(0, slider.value, 0, 0));
        }
        else
        {
            mainObj.GetComponent<Renderer>().material.SetFloat(floatReference, slider.value);
        }
    }
}
