using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    [Header("ROTATOR")]
    [SerializeField] Rigidbody rgbd;
    [SerializeField] Vector3 angularVelocity;
    [SerializeField] float rotationSpeed;

    [Header("MOVEMENT")]
    [SerializeField] GameObject target;
    Vector3 velocity, acceleration;
    [SerializeField] float speedFactor;


    void Start()
    {
        angularVelocity = new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1)).normalized;
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
        AsteroidRotator();
        transform.position += velocity * speedFactor * Time.deltaTime; //Movement
    }

    public void AsteroidRotator()
    {
        rgbd.angularVelocity = angularVelocity * rotationSpeed;
    }
}
