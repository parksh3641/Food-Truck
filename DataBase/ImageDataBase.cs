using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageDataBase", menuName = "ScriptableObjects/ImageDataBase")]
public class ImageDataBase : ScriptableObject
{
    public Sprite[] foodChangeArray;
    public Sprite[] candyArray;
    public Sprite[] japaneseArray;
    public Sprite[] dessertArray;

    [Space]
    public Sprite[] foodIconArray;

    public Sprite[] itemArray;

    public Sprite[] rewardArray;

    public Sprite[] skillArray;

    public Sprite[] islandArray;

    public Sprite[] treasureArray;

    public Sprite[] rankBackgroundArray;

    public Sprite[] dungeonArray;

    public Sprite[] iconArray;

    public Sprite[] GetFoodChangeArray()
    {
        return foodChangeArray;
    }

    public Sprite[] GetCandyArray()
    {
        return candyArray;
    }

    public Sprite[] GetJapaneseFoodArray()
    {
        return japaneseArray;
    }

    public Sprite[] GetDessertArray()
    {
        return dessertArray;
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

    public Sprite[] GetIslandArray()
    {
        return islandArray;
    }

    public Sprite[] GetTreasureArray()
    {
        return treasureArray;
    }

    public Sprite[] GetRankBackgroundArray()
    {
        return rankBackgroundArray;
    }

    public Sprite[] GetDungeonArray()
    {
        return dungeonArray;
    }

    public Sprite GetIconArray(IconType type)
    {
        return iconArray[(int)type];
    }
}
