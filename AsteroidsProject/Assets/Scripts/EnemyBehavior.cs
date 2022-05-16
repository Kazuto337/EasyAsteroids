using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] float health;

    [Header("LOOK AT PLAYER")]
    [SerializeField] GameObject player;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] CharacterController characterController;

    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(player.transform, Vector3.forward * -1);
        Move();

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Move()
    {
        characterController.Move(new Vector3(0, -1, 0) * speed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            health--;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            print("Nave Enemiga al Player");
            other.gameObject.GetComponent<PlayerController>().Blow();
            health = 0;
        }
    }
}
