using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragRotate : MonoBehaviour
{
    private float speed = 6f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
            //transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);
        }
    }
}







