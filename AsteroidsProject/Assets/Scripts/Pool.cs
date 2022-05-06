using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance { get; private set; }
    public GameObject prefab;
    public int initialamount = 50;
   

    List<GameObject> pool = new List<GameObject>();

    public void Awake()
    {
        Instance = this;
        FillPool();
    }


    void FillPool()
    {
        for (int t = 0; t < initialamount; t++) 
        {
            GameObject go = Instantiate(prefab);
            go.SetActive(false);

            pool.Add(go);

        }

    }


    public GameObject Get()
    {
        GameObject ret;
        if (pool.Count > 0)
        {
            ret = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        }
        else
        {
            ret = Instantiate(prefab);
        }
        ret.SetActive(true);
        return ret;
    }

    public void Return(GameObject go)
    {
        go.SetActive(false);
        pool.Add(go);
    }


}
