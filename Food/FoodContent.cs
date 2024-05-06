using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContent : MonoBehaviour
{
    public float size = 0f;
    public float saveSize = 0f;

    private float posX = 0;
    private float posY = 0;
    private float posZ = 0;

    private float sizeUp = 1.5f;

    public bool speicalFood = false;

    public MeshRenderer meshRenderer;

    Color speicalColor = new Color(1, 50f / 255f, 1);

    public bool isColor = false;
    public Color color;


    Rotation rotation;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();

        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        speicalFood = false;

        if(isColor)
        {
            meshRenderer.material.color = color;
        }
    }

    private void OnEnable()
    {
        if(saveSize == 0)
        {
            saveSize = transform.localScale.x;
            size = saveSize * 0.1f;

            posX = saveSize;
            posY = saveSize;
            posZ = saveSize;

            transform.localScale = new Vector3(saveSize, saveSize, saveSize);
        }
    }

    private void OnDisable()
    {
        meshRenderer.material.color = Color.white;

        speicalFood = false;

        if (saveSize == 0)
        {
            saveSize = transform.localScale.x;
            size = saveSize * 0.1f;

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
        if (speicalFood)
        {
            transform.localScale = new Vector3(posX + (size * (number + 1)) * sizeUp, posY + (size * (number + 1)) * sizeUp, posZ + (size * (number + 1)) * sizeUp);
        }
        else
        {
            transform.localScale = new Vector3(posX + (size * (number + 1)), posY + (size * (number + 1)), posZ + (size * (number + 1)));
        }
    }

    public void RankInitialize(int number)
    {
        transform.localScale = new Vector3((posX + (size * (number + 1)) * 0.1f), (posY + (size * (number + 1)) * 0.1f), (posZ + (size * (number + 1)) * 0.1f));
    }


    public void LevelReset()
    {
        transform.localScale = new Vector3(saveSize, saveSize, saveSize);
    }

    public void FeverOn()
    {
        rotation.rotationSpeed = 120;
    }

    public void FeverOff()
    {
        rotation.rotationSpeed = 30;
    }

    public void SetSpeicalFood(bool check)
    {
        if(check)
        {
            if(!speicalFood)
            {
                speicalFood = true;
                meshRenderer.materials[0].color = speicalColor;
            }
        }
        else
        {
            meshRenderer.materials[0].color = Color.white;

            speicalFood = false;
        }
    }
}
