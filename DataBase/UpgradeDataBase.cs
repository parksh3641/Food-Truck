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
    public FoodType foodType = FoodType.Hamburger;
    public int maxLevel = 0;
}

[System.Serializable]
public class UpgradeCandy
{
    public CandyType candyType = CandyType.Candy1;
    public int maxLevel = 0;
}

[CreateAssetMenu(fileName = "UpgradeDataBase", menuName = "ScriptableObjects/UpgradeDataBase")]
public class UpgradeDataBase : ScriptableObject
{
    public List<UpgradeFood> upgradeFoodList = new List<UpgradeFood>();

    public List<UpgradeCandy> upgradeCandyList = new List<UpgradeCandy>();

    public List<UpgradeFoodMagnification> priceList = new List<UpgradeFoodMagnification>();

    public List<UpgradeFoodNeedMagnification> needList = new List<UpgradeFoodNeedMagnification>();

    private float need, price, success = 0;

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



    public int GetNeed(int level, int value)
    {
        need = 0;

        if (level == 0)
        {
            switch (GameStateManager.instance.GameType)
            {
                case GameType.Story:
                    need = 100;
                    break;
                case GameType.Rank:
                    need = 0;
                    break;
            }

            return (int)need;
        }
        else
        {
            switch (GameStateManager.instance.GameType)
            {
                case GameType.Story:
                    need = level * value * needList[level].value;
                    break;
                case GameType.Rank:
                    need = 100 * level;
                    break;
            }

            return (int)need;
        }
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
                //if (level >= 99)
                //{
                //    price = level * level * (value * 11);
                //}
                //else if (level >= 89)
                //{
                //    price = level * level * (value * 10);
                //}
                //else if (level >= 79)
                //{
                //    price = level * level * (value * 9);
                //}
                //else if (level >= 69)
                //{
                //    price = level * level * (value * 8);
                //}
                //else if (level >= 59)
                //{
                //    price = level * level * (value * 7);
                //}
                //else if (level >= 49)
                //{
                //    price = level * level * (value * 6);
                //}
                //else if (level >= 39)
                //{
                //    price = level * level * (value * 5);
                //}
                //else if (level >= 29)
                //{
                //    price = level * level * (value * 4);
                //}
                //else if (level >= 19)
                //{
                //    price = level * level * (value * 3);
                //}
                //else if (level >= 9)
                //{
                //    price = level * level * (value * 2);
                //}
                //else
                //{
                //    price = level * level * (value * 1);
                //}

                //price *= 0.1f;

                price = 0;

                break;
        }

        return (int)price;
    }

    public float GetSuccess(int level)
    {
        success = 0;

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:
                if (level > 0)
                {
                    success = 98 - (level * 1);
                }
                else
                {
                    success = 100;
                }
                break;
            case GameType.Rank:
                if (level > 0)
                {
                    success = 100 - (level * 0.3f);
                }
                else
                {
                    success = 100;
                }
                break;
        }

        if (success < 0.1f)
        {
            success = 0.1f;
        }

        return success;
    }

}
