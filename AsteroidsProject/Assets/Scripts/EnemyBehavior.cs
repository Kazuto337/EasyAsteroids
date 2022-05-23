using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] GameController gameManager;

    [Header("STATS")]
    [SerializeField] float health;

    [Header("LOOK AT PLAYER")]
    [SerializeField] GameObject player;

    [Header("MOVEMENT")]
    [SerializeField] float speed;
    [SerializeField] CharacterController characterController;
    [SerializeField] bool canMove;

    [Header("VFX")]
    [SerializeField] ParticleSystem receiveDamage;
    [SerializeField] AudioSource receiveDamageSFX;
    [SerializeField] ParticleSystem explotion;
    [SerializeField] AudioSource explotionSFX;

    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = true;
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            transform.LookAt(player.transform, Vector3.forward * -1);
            Move();
        }

        if (health <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            characterController.enabled = false;
            canMove = false;
        }
    }

    public void Move()
    {
        characterController.Move(new Vector3(0, -1, 0) * speed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health = 0;
            gameManager.GameOver();

            if (health <= 0)
            {
                explotionSFX.Play();
                explotion.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            health -= other.GetComponent<Bullet>().damage;

            receiveDamageSFX.Play();
            receiveDamage.Play();

            Bulletspool.Instance.Return2Pool(other.gameObject);

            if (health <= 0)
            {
                explotionSFX.Play();
                explotion.Play();
            }
        }
    }
}
