using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayBackSpeedController : MonoBehaviour
{
    public List<ParticleSystem> particlesInScreen;
    public Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();

        foreach (ParticleSystem item in particlesInScreen)
        {
            var j = item.main;
            slider.value = j.simulationSpeed;
        }
        
    }
    void Update()
    {
        foreach (ParticleSystem item in particlesInScreen)
        {
            var j = item.main;
            j.simulationSpeed = slider.value;
        }
    }
}
