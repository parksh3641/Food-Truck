using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageDataBase", menuName = "ScriptableObjects/ImageDataBase")]
public class ImageDataBase : ScriptableObject
{
    public Sprite[] foodChangeArray;

    public Sprite[] foodIconArray;

    public Sprite[] itemArray;

    public Sprite[] rewardArray;

    public Sprite[] skillArray;

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

    public Sprite[] GetRewardArray()
    {
        return rewardArray;
    }

    public Sprite[] GetSkillArray()
    {
        return skillArray;
    }
}
