using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public Transform spawnbalas;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            Pool.Instance.Get().transform.position = spawnbalas.position;

        }

    }


}
