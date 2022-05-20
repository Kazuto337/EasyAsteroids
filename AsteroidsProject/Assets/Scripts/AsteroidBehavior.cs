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


    void Start()
    {
        angularVelocity = new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1)).normalized;
        initialPosition = transform.position;

        print(initialPosition.ToString());
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
            transform.position += velocity * speedFactor * Time.deltaTime; //Movement
        }
        CheckBorders();
    }

    public void AsteroidRotator()
    {
        rgbd.angularVelocity = angularVelocity * rotationSpeed;
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        acceleration = target.transform.position - transform.position;
        velocity = acceleration * Time.deltaTime;
    }

    public void CheckBorders()
    {
        if (transform.position.x >= borderX || transform.position.x <= -borderX || transform.position.y >= borderY || transform.position.y <= -borderY)
        {
            ResetPosition();
        }
    }
}
