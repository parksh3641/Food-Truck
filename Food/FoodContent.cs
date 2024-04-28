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
    Rotation rotation;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();

        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        speicalFood = false;
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
        meshRenderer.material.color = new Color(1, 1, 1);

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
                meshRenderer.materials[0].color = new Color(Random.Range(0, 200f) / 255f, Random.Range(0, 200f) / 255f, Random.Range(0, 200f) / 255f);
            }
        }
        else
        {
            meshRenderer.materials[0].color = new Color(1, 1, 1);

            speicalFood = false;
        }
    }
}
