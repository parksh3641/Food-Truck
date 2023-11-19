using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeInfo
{
    public int need = 0;
    public int price = 0;
    public int success = 0;
}

[System.Serializable]
public class UpgradeFood
{
    public FoodType foodType = FoodType.Hamburger;
    public int maxLevel = 0;

    //public List<UpgradeInfo> priceList = new List<UpgradeInfo>();

    public int GetNeed(int level, int value)
    {
        switch (GameStateManager.instance.GameType)
        {
            case GameType.Rank:
                value /= 2;
                break;
        }

        return (level + 1) * (level + 1) * value;
    }

    public int GetPrice(int level, int value)
    {
        float price = 0;

        if (level >= 99)
        {
            price = level * level * (value * 11);
        }
        else if (level >= 89)
        {
            price = level * level * (value * 10);
        }
        else if (level >= 79)
        {
            price = level * level * (value * 9);
        }
        else if (level >= 69)
        {
            price = level * level * (value * 8);
        }
        else if (level >= 59)
        {
            price = level * level * (value * 7);
        }
        else if (level >= 49)
        {
            price = level * level * (value * 6);
        }
        else if (level >= 39)
        {
            price = level * level * (value * 5);
        }
        else if (level >= 29)
        {
            price = level * level * (value * 4);
        }
        else if (level >= 19)
        {
            price = level * level * (value * 3);
        }
        else if (level >= 9)
        {
            price = level * level * (value * 2);
        }
        else
        {
            price = level * level * (value * 1);
        }

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:

                break;
            case GameType.Rank:
                price *= 0.1f;
                break;
        }

        return (int)price;
    }

    public float GetSuccess(int level)
    {
        float price = 0;

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:
                if (level > 0)
                {
                    price = 100 - (level * 1);
                }
                else
                {
                    price = 100;
                }
                break;
            case GameType.Rank:
                if (level > 0)
                {
                    price = 100 - (level * 0.1f);
                }
                else
                {
                    price = 100;
                }
                break;
        }

        if(price < 0.1f)
        {
            price = 0.1f;
        }

        return price;
    }

}

[System.Serializable]
public class UpgradeCandy
{
    public CandyType candyType = CandyType.Candy1;
    public int maxLevel = 0;

    //public List<UpgradeInfo> priceList = new List<UpgradeInfo>();

    public int GetNeed(int level, int value)
    {
        switch (GameStateManager.instance.GameType)
        {
            case GameType.Rank:
                value /= 2;
                break;
        }

        return (level + 1) * (level + 1) * value;
    }

    public int GetPrice(int level, int value)
    {
        float price = 0;

        if (level >= 99)
        {
            price = level * level * (value * 11);
        }
        else if (level >= 89)
        {
            price = level * level * (value * 10);
        }
        else if (level >= 79)
        {
            price = level * level * (value * 9);
        }
        else if (level >= 69)
        {
            price = level * level * (value * 8);
        }
        else if (level >= 59)
        {
            price = level * level * (value * 7);
        }
        else if (level >= 49)
        {
            price = level * level * (value * 6);
        }
        else if (level >= 39)
        {
            price = level * level * (value * 5);
        }
        else if (level >= 29)
        {
            price = level * level * (value * 4);
        }
        else if (level >= 19)
        {
            price = level * level * (value * 3);
        }
        else if (level >= 9)
        {
            price = level * level * (value * 2);
        }
        else
        {
            price = level * level * (value * 1);
        }

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:

                break;
            case GameType.Rank:
                price *= 0.1f;
                break;
        }

        return (int)price;
    }

    public float GetSuccess(int level)
    {
        float price = 0;

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:
                if (level > 0)
                {
                    price = 96 - (level * 1);
                }
                else
                {
                    price = 100;
                }
                break;
            case GameType.Rank:
                if (level > 0)
                {
                    price = 96 - (level * 0.1f);
                }
                else
                {
                    price = 100;
                }
                break;
        }

        if (price < 0.1f)
        {
            price = 0.1f;
        }

        return price;
    }
}

[CreateAssetMenu(fileName = "UpgradeDataBase", menuName = "ScriptableObjects/UpgradeDataBase")]
public class UpgradeDataBase : ScriptableObject
{
    public List<UpgradeFood> upgradeFoodList = new List<UpgradeFood>();

    public List<UpgradeCandy> upgradeCandyList = new List<UpgradeCandy>();

    public UpgradeFood GetUpgradeFood(FoodType type)
    {
        UpgradeFood food = new UpgradeFood();

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

    public int GetMaxLevel(FoodType type)
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


    public UpgradeCandy GetUpgradeCandy(CandyType type)
    {
        UpgradeCandy candy = new UpgradeCandy();

        for (int i = 0; i < upgradeCandyList.Count; i++)
        {
            if (upgradeCandyList[i].candyType.Equals(type))
            {
                candy = upgradeCandyList[i];
                break;
            }
        }

        return candy;
    }

    public int GetMaxLevelCandy(CandyType type)
    {
        int number = 0;
        for (int i = 0; i < upgradeCandyList.Count; i++)
        {
            if (upgradeCandyList[i].candyType.Equals(type))
            {
                number = upgradeCandyList[i].maxLevel;
                break;
            }
        }

        return number;
    }
}
