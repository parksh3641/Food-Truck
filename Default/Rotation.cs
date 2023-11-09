using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public bool rotateY = false;

    public float rotationSpeed = 10f;

    private void Update()
    {
        if(rotateY)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0f);
        }
        else
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
