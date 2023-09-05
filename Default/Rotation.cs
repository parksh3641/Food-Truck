using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Adjust this value to control the rotation speed

    private void Update()
    {
        // Rotate the object continuously on the y-axis
        transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
    }
}
