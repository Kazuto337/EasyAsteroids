using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    [Header("GAME START")]
    public bool isMoving;
    [SerializeField] Vector3 initialPosition;

    [Header("ROTATOR")]
    [SerializeField] Rigidbody rgbd;
    [SerializeField] Vector3 angularVelocity;
    [SerializeField] float rotationSpeed;

    [Header("MOVEMENT")]
    [SerializeField] GameObject target;
    Vector3 velocity, acceleration;
    [SerializeField] float speedFactor;

    [Header("CheckBorders")]
    [SerializeField] float borderX;
    [SerializeField] float borderY;

    [Header("VFX")]
    [SerializeField] float health = 3;
    [SerializeField] ParticleSystem explotion;
    [SerializeField] AudioSource explotionSFX;
    [SerializeField] Vector3 playerDistance;
    [SerializeField] float intensity;


    void Start()
    {
        angularVelocity = new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1)).normalized;
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        #region setting target
        acceleration = target.transform.position - transform.position;
        velocity = acceleration * Time.deltaTime;
        #endregion
    }

    private void Update()
    {
        if (isMoving)
        {
            AsteroidRotator();
            transform.position += velocity.normalized * speedFactor * Time.deltaTime; //Movement
        }
        else
        {
            health -= 1 * Time.deltaTime;
            if (health <= 0)
            {
                ResetPosition();
                health = 3;
            }
        }
        CheckBorders();
        playerDistance = target.transform.position - gameObject.transform.position;
        Debug.DrawRay(gameObject.transform.position, playerDistance, Color.white);
        print(playerDistance.magnitude);
        GetComponent<Renderer>().material.SetFloat("intensity_", intensity);
        if (playerDistance.magnitude <= 3)
        {
            if (intensity <= 8)
            {
                intensity ++ ;
            }
        }
        else if (playerDistance.magnitude > 3)
        {
            if (intensity >= 8)
            {
                intensity --;
            }
        }
    }

    public void AsteroidRotator()
    {
        rgbd.angularVelocity = angularVelocity * rotationSpeed;
    }

    public void ResetPosition()
    {
        isMoving = true;
        GetComponent<MeshCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;

        transform.position = initialPosition;
        acceleration = target.transform.position - transform.position;
        velocity = acceleration * Time.deltaTime;

        intensity = 2;
    }

    public void CheckBorders()
    {
        if (transform.position.x >= borderX || transform.position.x <= -borderX || transform.position.y >= borderY || transform.position.y <= -borderY)
        {
            ResetPosition();
        }
    }

    private void Blow()
    {
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        explotionSFX.Play();
        explotion.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            Bulletspool.Instance.Return2Pool(other.gameObject);
            isMoving = false;
            Blow();
        }
    }
}
