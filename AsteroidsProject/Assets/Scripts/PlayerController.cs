using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] ShootingSystem shootingSystem;

    #region Movement Variables
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] CharacterController controller;
    [SerializeField] Vector2 positionChange;
    Vector3 movementVector;
    #endregion

    #region Borders Variables
    [Header("CheckBorders")]
    [SerializeField] float borderX;
    [SerializeField] float borderY;
    [SerializeField] bool canMoveH, canMoveY;
    #endregion

    #region ReSpawn Variables
    [Header("ReSpawn")]
    [SerializeField] GameObject spawnPointObject;
    [SerializeField] Vector3 spawnPoint;
    #endregion

    #region VFX
    [Header("Vfx")]
    [SerializeField] ParticleSystem blowVFX;
    [SerializeField] AudioSource blowSFX;
    [SerializeField] GameObject playerObj;
    #endregion

    private void Start()
    {
        spawnPoint = spawnPointObject.transform.position;
        controller.enabled = true;
    }
    void Update()
    {
        CheckBorders();
        Movement();
    }

    private void Movement()
    {
        if (canMoveH) positionChange.x = Input.GetAxis("Horizontal");
        if (canMoveY) positionChange.y = Input.GetAxis("Vertical");

        movementVector = new Vector3(positionChange.x, positionChange.y, 0f);

        controller.Move(movementVector * speed * Time.deltaTime);
    }

    public void ResetPlayer()
    {
        blowVFX.Clear();
        blowSFX.Stop();

        transform.position = spawnPoint;

        print("ResetPlayer");

        playerObj.SetActive(true);
        controller.enabled = true;
        shootingSystem.enabled = true;
    }

    private void Blow()
    {
        shootingSystem.enabled = false;
        playerObj.SetActive(false);

        blowSFX.Play();
        blowVFX.Play();

        controller.enabled = false;

        gameController.GameOver();
    }

    private void CheckBorders()
    {
        if (transform.position.x >= borderX)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                canMoveH = false;
                positionChange.x = 0;
            }
            else if (Input.GetAxis("Horizontal") < 0) canMoveH = true;
        }
        if (transform.position.x <= -borderX)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                canMoveH = false;
                positionChange.x = 0;
            }
            else if (Input.GetAxis("Horizontal") > 0) canMoveH = true;
        }

        if (transform.position.y >= borderY)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                canMoveY = false;
                positionChange.y = 0;
            }
            else if (Input.GetAxis("Vertical") < 0) canMoveY = true;
        }
        if (transform.position.y <= -borderY)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                canMoveY = false;
                positionChange.y = 0;
            }
            else if (Input.GetAxis("Vertical") > 0) canMoveY = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Blow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Blow();
            gameController.GameOver();
            print("le pegue al asteroide");
        }
    }
}
