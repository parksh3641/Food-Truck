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
public class UpgradeCandy
{
    public CandyType candyType = CandyType.Candy1;
    public int maxLevel = 0;
}

[System.Serializable]
public class UpgradeJapaneseFood
{
    public JapaneseFoodType japaneseFoodType = JapaneseFoodType.JapaneseFood1;
    public int maxLevel = 0;
}

[System.Serializable]
public class UpgradeDessert
{
    public DessertType dessertType = DessertType.Dessert1;
    public int maxLevel = 0;
}

[CreateAssetMenu(fileName = "UpgradeDataBase", menuName = "ScriptableObjects/UpgradeDataBase")]
public class UpgradeDataBase : ScriptableObject
{
    public List<UpgradeFood> upgradeFoodList = new List<UpgradeFood>();

    public List<UpgradeCandy> upgradeCandyList = new List<UpgradeCandy>();

    public List<UpgradeJapaneseFood> upgradeJapaneseFoodList = new List<UpgradeJapaneseFood>();

    public List<UpgradeDessert> upgradeDessertList = new List<UpgradeDessert>();


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

    public int GetMaxLevelFastFood(FoodType type)
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

    public UpgradeJapaneseFood GetUpgradeJapaneseFood(JapaneseFoodType type)
    {
        UpgradeJapaneseFood japaneseFood = new UpgradeJapaneseFood();

        for (int i = 0; i < upgradeJapaneseFoodList.Count; i++)
        {
            if (upgradeJapaneseFoodList[i].japaneseFoodType.Equals(type))
            {
                japaneseFood = upgradeJapaneseFoodList[i];
                break;
            }
        }

        return japaneseFood;
    }

    public UpgradeDessert GetUpgradeDessert(DessertType type)
    {
        UpgradeDessert dessert = new UpgradeDessert();

        for (int i = 0; i < upgradeDessertList.Count; i++)
        {
            if (upgradeDessertList[i].dessertType.Equals(type))
            {
                dessert = upgradeDessertList[i];
                break;
            }
        }

        return dessert;
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

    public int GetMaxLevelJapaneseFood(JapaneseFoodType type)
    {
        int number = 0;
        for (int i = 0; i < upgradeJapaneseFoodList.Count; i++)
        {
            if (upgradeJapaneseFoodList[i].japaneseFoodType.Equals(type))
            {
                number = upgradeJapaneseFoodList[i].maxLevel;
                break;
            }
        }

        return number;
    }

    public int GetMaxLevelDessert(DessertType type)
    {
        int number = 0;
        for (int i = 0; i < upgradeDessertList.Count; i++)
        {
            if (upgradeDessertList[i].dessertType.Equals(type))
            {
                number = upgradeDessertList[i].maxLevel;
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
                    need = 500 * level;
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
                price = level * level * 50;

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
                    switch (GameStateManager.instance.IslandType)
                    {
                        case IslandType.Island1:
                            success = 100 - (level * 1);
                            break;
                        case IslandType.Island2:
                            success = 96 - (level * 1);
                            break;
                        case IslandType.Island3:
                            success = 91 - (level * 1);
                            break;
                        case IslandType.Island4:
                            success = 86 - (level * 1);
                            break;
                    }
                }
                else
                {
                    success = 100;
                }
                break;
            case GameType.Rank:
                if (level > 0)
                {
                    success = 100 - (level * 0.5f);
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
