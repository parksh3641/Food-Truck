using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleContent : MonoBehaviour
{
    public float size = 0f;
    public float saveSize = 0f;

    private float posX = 0;
    private float posY = 0;
    private float posZ = 0;

    public void Initialize(int number)
    {
        if (saveSize == 0)
        {
            saveSize = transform.localScale.x;
            size = saveSize * 0.05f;

            posX = saveSize;
            posY = saveSize;
            posZ = saveSize;

            transform.localScale = new Vector3(saveSize, saveSize, saveSize);
        }

        transform.localScale = new Vector3(posX + (size * (number + 1)), posY + (size * (number + 1)), posZ + (size * (number + 1)));
    }
}
