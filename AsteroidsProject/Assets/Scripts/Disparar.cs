using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public Transform spawnbalas;
    public bool sedispara;

    private void Start()
    {
        sedispara = true;
    }

    
    void FixedUpdate()
    {
        disparar();

    }

    public void disparar()
    {
        if (sedispara== true &&Input.GetKey(KeyCode.Space) )
        {
            StartCoroutine(cadencia());
            
                
            Pool.Instance.Get().transform.position = spawnbalas.position;
                


        }
        
    }

    IEnumerator cadencia()
    {
        sedispara = false;        
        yield return new WaitForSeconds(0.5f);
        sedispara = true;


    }

}
