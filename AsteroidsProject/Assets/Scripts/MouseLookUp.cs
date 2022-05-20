using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookUp : MonoBehaviour
{
    [SerializeField] Vector3 mousePos;
    Vector3 diff;
    Transform indexTransform;

    private void Start()
    {
        print(transform.localRotation.eulerAngles);
        indexTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        mousePos = GetWorldMousePos();
        diff = mousePos - transform.position;

        float rads = Mathf.Atan2(diff.y, diff.x);
        float deg = -1*(rads * Mathf.Rad2Deg);
        transform.eulerAngles = new Vector3(deg, 90, -90);
    }

    public Vector4 GetWorldMousePos()
    {
        Camera camera = Camera.main;
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0;

        return worldPos;
    }
}
