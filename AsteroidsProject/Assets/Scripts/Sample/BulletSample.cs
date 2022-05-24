using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSample : MonoBehaviour
{
    [SerializeField] [Range(0.01f, 0.5f)] float speed;
    public float damage;
    Vector3 acceleration, velocity;
    public Slider speedSlider;

    [Header("CHECKBORDERS")]
    [SerializeField] float borderX;
    [SerializeField] float borderY;

    void Awake()
    {
        speedSlider.maxValue = 0.5f;
        speedSlider.minValue = 0.01f;
    }
    public void SetDirection(Vector3 target)
    {
        velocity = target * Time.deltaTime;
        velocity.z = 0;
    }
    private void Update()
    {
        speed = speedSlider.value;

        transform.position += velocity.normalized * speed;
        CheckBorders();
    }
    private void CheckBorders()
    {
        if (transform.position.x >= borderX || transform.position.x <= -borderX || transform.position.y >= borderY || transform.position.y <= -borderY)
        {
            Bulletspool.Instance.Return2Pool(gameObject);
        }
    }
}
