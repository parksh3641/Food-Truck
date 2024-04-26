using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageDataBase", menuName = "ScriptableObjects/ImageDataBase")]
public class ImageDataBase : ScriptableObject
{
    public Sprite[] foodIconArray;

    public Sprite[] rankFoodIconArray;

    public Sprite[] itemArray;

    public Sprite[] rewardArray;

    public Sprite[] skillArray;

    public Sprite[] islandArray;

    public Sprite[] treasureArray;

    public Sprite[] rankBackgroundArray;

    public Sprite[] dungeonArray;

    public Sprite[] iconArray;

    public Sprite[] gifticonArray;

    public Sprite[] advancementArray;

    public Sprite[] equipArray;

    public Sprite GetFoodIconArray(FoodType type)
    {
        return foodIconArray[(int)type];
    }

    public Sprite[] GetFoodIconArray()
    {
        return foodIconArray;
    }

    public Sprite GetRankFoodIconArray(RankFoodType type)
    {
        return rankFoodIconArray[(int)type];
    }

    public Sprite[] GetRankFoodIconArray()
    {
        return rankFoodIconArray;
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

    public Sprite GetRankBackgroundArray(int number)
    {
        return rankBackgroundArray[number];
    }

    public Sprite[] GetDungeonArray()
    {
        return dungeonArray;
    }

    public Sprite GetIconArray(IconType type)
    {
        return iconArray[(int)type];
    }

    public Sprite GetGifticonArray(GifticonType type)
    {
        return gifticonArray[(int)type];
    }

    public Sprite GetFoodIconType(FoodType type)
    {
        return foodIconArray[(int)type];
    }

    public Sprite GetAdvancementArray(int number)
    {
        return advancementArray[number];
    }

    public Sprite GetEquipArray(int number)
    {
        return equipArray[number];
    }
}
