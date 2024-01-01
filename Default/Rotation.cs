using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public bool rotateY = false;
    public bool rotateZ = false;

    public float rotationSpeed = 30f;

    private void Update()
    {
        if(rotateY)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0f);
        }
        else if(rotateZ)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
