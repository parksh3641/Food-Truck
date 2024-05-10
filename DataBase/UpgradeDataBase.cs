using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeFoodNeedMagnification
{
    public float value = 0;
}

[System.Serializable]
public class UpgradeFoodMagnification
{
    public float value = 0;
}

[System.Serializable]
public class UpgradeFood
{
    public FoodType foodType = FoodType.Food1;
    public int maxLevel = 0;
}

[System.Serializable]
public class UpgradeRankFood
{
    public RankFoodType rankFoodType = RankFoodType.RankFood1;
    public int maxLevel = 0;
}

[CreateAssetMenu(fileName = "UpgradeDataBase", menuName = "ScriptableObjects/UpgradeDataBase")]
public class UpgradeDataBase : ScriptableObject
{
    public List<UpgradeFood> upgradeFoodList = new List<UpgradeFood>();

    public List<UpgradeRankFood> upgradeRankFoodList = new List<UpgradeRankFood>();

    public List<UpgradeFoodMagnification> priceList = new List<UpgradeFoodMagnification>();

    public List<UpgradeFoodNeedMagnification> needList = new List<UpgradeFoodNeedMagnification>();

    private float need, price, success = 0;

    UpgradeFood food;

    public void Initialize()
    {
        upgradeFoodList.Clear();

        for(int i = 0; i < System.Enum.GetValues(typeof(FoodType)).Length; i ++)
        {
            UpgradeFood food = new UpgradeFood();
            food.foodType = FoodType.Food1 + i;

            food.maxLevel = 10 + ((i % GameStateManager.instance.Island) * 5) + (i / GameStateManager.instance.Island);

            upgradeFoodList.Add(food);
        }
    }

    public UpgradeFood GetUpgradeFood(FoodType type)
    {
        food = new UpgradeFood();

        for (int i = 0; i < upgradeFoodList.Count; i ++)
        {
            if(upgradeFoodList[i].foodType.Equals(type))
            {
                food = upgradeFoodList[i];
                break;
            }
        }

        return food;
    }

    public UpgradeFood GetUpgradeRankFood(RankFoodType type)
    {
        food = new UpgradeFood();

        for (int i = 0; i < upgradeFoodList.Count; i++)
        {
            if (upgradeFoodList[i].foodType.Equals(type))
            {
                food = upgradeFoodList[i];
                break;
            }
        }

        return food;
    }

    public int GetFoodMaxLevel(FoodType type)
    {
        int number = 0;
        for(int i = 0; i < upgradeFoodList.Count; i ++)
        {
            if(upgradeFoodList[i].foodType.Equals(type))
            {
                number = upgradeFoodList[i].maxLevel;
                break;
            }
        }

        return number;
    }

    public int GetRankFoodMaxLevel(RankFoodType type)
    {
        int number = 0;
        for (int i = 0; i < upgradeRankFoodList.Count; i++)
        {
            if (upgradeRankFoodList[i].rankFoodType.Equals(type))
            {
                number = upgradeRankFoodList[i].maxLevel;
                break;
            }
        }

        return number;
    }

    public int GetNeed(int level, int value)
    {
        need = 0;

        if (level == 0)
        {
            need = 100;
        }
        else
        {
            need = level * value;
        }

        return (int)need;
    }

    public int GetPrice(int level, int value)
    {
        price = 0;

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:
                price = level * (value * priceList[level].value);
                break;
            case GameType.Rank:
                if(level > priceList.Count - 1)
                {
                    price = level * (value * (10 + (level - (priceList.Count - 1)) / 20));
                }
                else
                {
                    price = level * (value * priceList[level].value);
                }

                price *= 0.1f;

                break;
        }

        return (int)price;
    }

}
