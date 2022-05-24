using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowSample : MonoBehaviour
{
    public ParticleSystem blowVFX;
    public AudioSource blowSFX;
    public bool isCool;
    public float shityCooldown;
    public GameObject mainObj;

    private void Awake()
    {
        var index = blowVFX;
        shityCooldown = index.main.duration + 1;
    }

    private void Update()
    {
        if (!isCool)
        {
            shityCooldown -= Time.deltaTime;
            if (shityCooldown <= 0)
            {
                isCool = true;
            }
        }
        else
        {
            var index = blowVFX;
            shityCooldown = index.main.duration + 1;
            mainObj.SetActive(true);
        }

    }
    public void Blow()
    {
        if (!blowVFX.isPlaying)
        {
            mainObj.SetActive(false);
            blowSFX.Play();
            blowVFX.Play();
            isCool = false;
        }
    }
}
