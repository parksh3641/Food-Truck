using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFoodContent : MonoBehaviour
{
    public float size = 0f;
    public float saveSize = 0f;


    private void OnEnable()
    {
        if (saveSize == 0)
        {
            saveSize = transform.localScale.x;

            transform.localScale = new Vector3(saveSize, saveSize, saveSize);
        }
    }

    public void Initialize()
    {
        if (saveSize == 0)
        {
            saveSize = transform.localScale.x;

            transform.localScale = new Vector3(saveSize, saveSize, saveSize);
        }
        else
        {
            transform.localScale = new Vector3(saveSize, saveSize, saveSize);
        }
    }

    public void SetSize(float number)
    {
        size = saveSize * number;

        if(size <= saveSize * 0.1f)
        {
            size = saveSize * 0.1f;
        }

        transform.localScale = new Vector3(size, size, size);
    }
}
