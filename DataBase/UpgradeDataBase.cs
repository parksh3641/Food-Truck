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
    public int maxLevel = 20;

    public List<UpgradeInfo> priceList = new List<UpgradeInfo>();

    public int GetNeed(int level)
    {
        //return priceList[level].need;
        return (level + 1) * (level + 1) * 100;
    }

    public int GetPrice(int level)
    {
        int price = 0;

        if (level >= 99)
        {
            price = level * level * 80000;
        }
        else if (level >= 89)
        {
            price = level * level * 70000;
        }
        else if (level >= 79)
        {
            price = level * level * 60000;
        }
        else if (level >= 69)
        {
            price = level * level * 51200;
        }
        else if (level >= 59)
        {
            price = level * level * 25600;
        }
        else if (level >= 49)
        {
            price = level * level * 19200;
        }
        else if (level >= 39)
        {
            price = level * level * 12800;
        }
        else if (level >= 29)
        {
            price = level * level * 6400;
        }
        else if (level >= 19)
        {
            price = level * level * 3200;
        }
        else if(level >= 9)
        {
            price = level * level * 1600;
        }
        else
        {
            price = level * level * 800;
        }

        return price;
    }

    public float GetSuccess(int level)
    {
        //return priceList[level].success;

        float percent = 0;

        if(level >= 90)
        {
            percent = 40 - ((level - 90) * 0.1f);
        }
        else if (level >= 30)
        {
            percent = 70 - ((level - 30) - 0.5f);
        }
        else
        {
            percent = 100 - (level * 1);
        }

        if(percent <= 0.1f)
        {
            percent = 0.1f;
        }

        return percent;
    }

}

[CreateAssetMenu(fileName = "UpgradeDataBase", menuName = "ScriptableObjects/UpgradeDataBase")]
public class UpgradeDataBase : ScriptableObject
{
    public List<UpgradeFood> upgradeFoodList = new List<UpgradeFood>();

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
}
