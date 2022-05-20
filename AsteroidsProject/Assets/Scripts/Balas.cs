using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{

    public float speed = 20f;
    public float ttl = 3f;
    

    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(ReturnToPoolCoroutine());
    }

    IEnumerator ReturnToPoolCoroutine()
    {
        yield return new WaitForSeconds(ttl);
        Pool.Instance.Return(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
    }
}
