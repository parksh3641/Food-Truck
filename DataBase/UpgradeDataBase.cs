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

    public int GetNeed(int level)
    {
        return (level + 1) * (level + 1) * 50;
    }

    public int GetPrice(int level)
    {
        int price = 0;

        if (level >= 99)
        {
            price = level * level * 12800;
        }
        else if (level >= 89)
        {
            price = level * level * 9600;
        }
        else if (level >= 79)
        {
            price = level * level * 8400;
        }
        else if (level >= 69)
        {
            price = level * level * 7200;
        }
        else if (level >= 59)
        {
            price = level * level * 6000;
        }
        else if (level >= 49)
        {
            price = level * level * 4800;
        }
        else if (level >= 39)
        {
            price = level * level * 3200;
        }
        else if (level >= 29)
        {
            price = level * level * 2400;
        }
        else if (level >= 19)
        {
            price = level * level * 1800;
        }
        else if(level >= 9)
        {
            price = level * level * 1200;
        }
        else
        {
            price = level * level * 600;
        }

        return price;
    }

    public float GetSuccess(int level)
    {
        return 100 - (level * 1);
    }

}

[System.Serializable]
public class UpgradeCandy
{
    public CandyType candyType = CandyType.Candy1;
    public int maxLevel = 0;

    //public List<UpgradeInfo> priceList = new List<UpgradeInfo>();

    public int GetNeed(int level)
    {
        return (level + 1) * (level + 1) * 50;
    }

    public int GetPrice(int level)
    {
        int price = 0;

        if (level >= 99)
        {
            price = level * level * 12800;
        }
        else if (level >= 89)
        {
            price = level * level * 9600;
        }
        else if (level >= 79)
        {
            price = level * level * 8400;
        }
        else if (level >= 69)
        {
            price = level * level * 7200;
        }
        else if (level >= 59)
        {
            price = level * level * 6000;
        }
        else if (level >= 49)
        {
            price = level * level * 4800;
        }
        else if (level >= 39)
        {
            price = level * level * 3200;
        }
        else if (level >= 29)
        {
            price = level * level * 2400;
        }
        else if (level >= 19)
        {
            price = level * level * 1800;
        }
        else if (level >= 9)
        {
            price = level * level * 1200;
        }
        else
        {
            price = level * level * 600;
        }

        return price;
    }

    public float GetSuccess(int level)
    {
        int price = 0;

        if(level > 0)
        {
            price = 96 - (level * 1);
        }
        else
        {
            price = 100;
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
