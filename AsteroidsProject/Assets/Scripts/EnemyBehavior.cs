using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] GameController gameManager;

    [Header("STATS")]
    [SerializeField] float health;
    [SerializeField] float respawnTime;

    [Header("LOOK AT PLAYER")]
    [SerializeField] GameObject player;

    #region MOVEMENT VARIABLES
    [Header("MOVEMENT")]
    [SerializeField] float speed;
    [SerializeField] CharacterController characterController;
    [SerializeField] bool canMove;
    #endregion

    #region VFX & SFX
    [Header("VFX")]
    [SerializeField] ParticleSystem receiveDamage;
    [SerializeField] AudioSource receiveDamageSFX;
    [SerializeField] ParticleSystem explotion;
    [SerializeField] AudioSource explotionSFX;
    [SerializeField] Animator anima;
    #endregion

    #region CHECKBORDERS
    [Header("CheckBorders")]
    [SerializeField] Vector3 initialPosition;
    [SerializeField] float borderY, deActivateFurtive, activateFurtive;
    #endregion

    private void Start()
    {
        initialPosition = transform.position;
    }
    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = true;
        canMove = true;
    }

    void Update()
    {
        CheckBorders();

        transform.LookAt(player.transform, Vector3.forward * -1);
        Move();

        if (health <= 0)
        {
            Blow();
            respawnTime -= 1 * Time.deltaTime;
            if (respawnTime <= 0)
            {
                ResetEnemy();
                respawnTime = 5;
            }
        }
    }

    public void Move()
    {
        characterController.Move(new Vector3(0, -1, 0) * speed * Time.deltaTime);
    }

    private void Blow()
    {
        characterController.enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
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

    private void CheckBorders()
    {
        if (transform.position.y <= deActivateFurtive && transform.position.y > activateFurtive)
        {
            anima.SetBool("ActivateFutive", false);
        }
        else if (transform.position.y <= activateFurtive)
        {
            anima.SetBool("ActivateFutive", true);
        }

        if (transform.position.y <= -borderY)
        {
            gameObject.SetActive(false);
            ResetEnemy();
        }
    }

    public void ResetEnemy()
    {
        print("ResetEnemy");
        health = 50;
        transform.position = initialPosition;
        gameObject.SetActive(true);
        GetComponent<MeshRenderer>().enabled = true;
        characterController.enabled = true;
    }
}
