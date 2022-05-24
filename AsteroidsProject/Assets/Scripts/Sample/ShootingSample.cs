using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingSample : MonoBehaviour
{
    [SerializeField] GameObject bulletSpawn , bulletTarget;
    [SerializeField] ParticleSystem anticipation;
    [SerializeField] AudioSource laserSFX;
    public float cooldown, coolTime;
    public bool isCool;
    public Slider coolBar;

    private void Awake()
    {
        coolBar.maxValue = coolTime;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isCool)
        {
            isCool = false;
            coolBar.value = 0;
            playerShoot();
            anticipation.Play();
            laserSFX.Play();
            StartCoroutine("CoolDown");
            
        }
    }

    IEnumerator CoolDown()
    {
        cooldown = 0;

        while (cooldown < coolTime)
        {
            cooldown += Time.deltaTime;
            coolBar.value = cooldown;
            if (cooldown >= coolTime)
            {
                isCool = true;
                StopCoroutine("CoolDown");
            }
            yield return null;
        }
    }

    public void playerShoot()
    {
        Vector3 playerPos = gameObject.transform.position , targetPos = bulletTarget.transform.position;

        GameObject bullet = Bulletspool.Instance.RequestBullet();
        bullet.transform.position = bulletSpawn.transform.position;
        bullet.GetComponent<BulletSample>().SetDirection(targetPos-playerPos);

    }
}
