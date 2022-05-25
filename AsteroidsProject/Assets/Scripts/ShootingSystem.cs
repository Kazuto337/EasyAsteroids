using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] GameObject bulletSpawn;
    [SerializeField] ParticleSystem anticipation;
    [SerializeField] AudioSource laserSFX;
    private float cooldown;
    [SerializeField] float coolTime;
    public bool isCool;

    void Update()
    {
        Vector3 mousePos = GetWorldMousePos();
        Vector3 playerPos = gameObject.transform.position;

        mousePos.z = playerPos.z;

        Debug.DrawRay(playerPos, mousePos - playerPos, Color.red);
        if (Input.GetButtonDown("Fire1") && isCool)
        {
            isCool = false;
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
        Vector3 mousePos = GetWorldMousePos();
        Vector3 playerPos = gameObject.transform.position;

        mousePos.z = playerPos.z;

        GameObject bullet = Bulletspool.Instance.RequestBullet();
        bullet.transform.position = bulletSpawn.transform.position;
        bullet.GetComponent<Bullet>().SetDirection(mousePos - playerPos);
        

        #region RaycastFeature
        /*RaycastHit hit;
        Debug.DrawRay(playerPos, mousePos - playerPos, Color.magenta);

        if (Physics.Raycast(playerPos, mousePos - playerPos, out hit))
        {
            print("raycast");

            print(hit.transform.name);
        }*/
        #endregion
    }

    public Vector4 GetWorldMousePos()
    {
        Camera camera = Camera.main;
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0;

        return worldPos;
    }

}
