using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageDataBase", menuName = "ScriptableObjects/ImageDataBase")]
public class ImageDataBase : ScriptableObject
{
    public Sprite[] foodChangeArray;

    public Sprite[] foodIconArray;

    public Sprite[] itemArray;

    public Sprite[] GetFoodChangeArray()
    {
        return foodChangeArray;
    }

    public Sprite[] GetFoodIconArray()
    {
        return foodIconArray;
    }

    public Sprite[] GetItemArray()
    {
        return itemArray;
    }
}
