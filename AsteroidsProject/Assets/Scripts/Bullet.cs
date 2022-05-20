using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField][Range(0.01f , 0.5f )] float speed;
    Vector3 acceleration, velocity;

    [Header("CHECKBORDERS")]
    [SerializeField] float borderX;
    [SerializeField] float borderY;

    public void SetDirection(Vector3 target)
    {
        velocity = target * Time.deltaTime;

        velocity.z = 0;

        float rads = Mathf.Atan2(target.y, target.x);
        float deg = (Mathf.Rad2Deg * rads) - 90;
        print(deg);
        transform.eulerAngles = new Vector3(deg, -90, 0);
    }
    private void Update()
    {
        transform.position += velocity.normalized * speed ;
        CheckBorders();
    }
    public void CheckBorders()
    {
        if (transform.position.x >= borderX || transform.position.x <= -borderX || transform.position.y >= borderY || transform.position.y <= -borderY)
        {
            Bulletspool.Instance.Return2Pool(gameObject);
        }
    }
}
