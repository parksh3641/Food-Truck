using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContent : MonoBehaviour
{
    public float size = 0f;
    public float saveSize = 0f;

    public float posX = 0;
    public float posY = 0;
    public float posZ = 0;

    public Rotation rotation;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();
    }

    private void OnEnable()
    {
        if(saveSize == 0)
        {
            saveSize = transform.localScale.x;
            size = saveSize * 0.15f;

            posX = saveSize;
            posY = saveSize;
            posZ = saveSize;
        }
    }

    private void OnDisable()
    {
        if (saveSize == 0)
        {
            saveSize = transform.localScale.x;
            size = saveSize * 0.05f;

            posX = saveSize;
            posY = saveSize;
            posZ = saveSize;
        }
        else
        {
            posX = saveSize;
            posY = saveSize;
            posZ = saveSize;

            transform.localScale = new Vector3(saveSize, saveSize, saveSize);
        }
    }

    public void Initialize(int number)
    {
        if (number >= 5)
        {
            number = 5;
        }

        transform.localScale = new Vector3(posX += (size * number), posY += (size * number), posZ += (size * number));
    }


    public void LevelUp()
    {
        transform.localScale = new Vector3(posX += size, posY += size, posZ += size);
    }

    public void LevelReset()
    {
        transform.localScale = new Vector3(saveSize, saveSize, saveSize);
    }

    public void FeverOn()
    {
        rotation.rotationSpeed = 80;
    }

    public void FeverOff()
    {
        rotation.rotationSpeed = 30;
    }
}
