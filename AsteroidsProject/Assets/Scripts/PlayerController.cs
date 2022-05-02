using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] CharacterController controller;
    Vector2 positionChange;
    void Update()
    {
        positionChange.x = Input.GetAxis("Horizontal");
        positionChange.y = Input.GetAxis("Vertical");

        Vector3 movemntVector = new Vector3(positionChange.x * Time.deltaTime, positionChange.y * Time.deltaTime , 0f);

        controller.Move(movemntVector * speed);
    }
}
