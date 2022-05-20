using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletspool : MonoBehaviour
{
    [SerializeField] List<GameObject> bulletList;

    private static Bulletspool instance;
    public static Bulletspool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    public GameObject RequestBullet()
    {
        foreach (GameObject item in bulletList)
        {
            if (item.activeSelf == false)
            {
                item.SetActive(true);
                return item;
            }
        }
        return null;
    }

    public void Return2Pool(GameObject item)
    {
        if (item.activeSelf == true)
        {
            item.transform.position = transform.position;
            item.SetActive(false);
        }
    }
}
