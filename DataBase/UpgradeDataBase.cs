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

        if (level >= 30)
        {
            price = level * level * 38400;
        }
        else if (level >= 25)
        {
            price = level * level * 19200;
        }
        else if (level >= 20)
        {
            price = level * level * 9600;
        }
        else if (level >= 15)
        {
            price = level * level * 4800;
        }
        else if (level >= 10)
        {
            price = level * level * 2400;
        }
        else if(level >= 5)
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
