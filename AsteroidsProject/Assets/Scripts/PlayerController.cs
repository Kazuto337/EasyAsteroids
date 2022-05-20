using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] CharacterController controller;
    [SerializeField] Vector2 positionChange;
    Vector3 movementVector;

    [Header("CheckBorders")]
    [SerializeField] float borderX;
    [SerializeField] float borderY;
    [SerializeField] bool canMoveH, canMoveY;

    [Header("ReSpawn")]
    [SerializeField] GameController gameController;
    [SerializeField] GameObject spawnPointObject;
    [SerializeField] Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = spawnPointObject.transform.position;
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

    public void ResetTransform()
    {
        print("ResetPlayer");
        transform.position = spawnPoint;
    }

    public void Blow()
    {
        ResetTransform();
        gameController.GameOver();
    }

    public void CheckBorders()
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
            print("le pegue al enemy");
            Blow();
        }
    }
}
