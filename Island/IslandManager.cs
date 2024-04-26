using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Island_Total_Data
{
    [Header("¸¸·¾ È½¼ö")]
    public Island_Max_Data[] island_Max_Datas;

    [Header("·¹¾î È½¼ö")]
    public Island_Rare_Data[] island_Rare_Datas;

    public void Initialize()
    {
        island_Max_Datas = new Island_Max_Data[System.Enum.GetValues(typeof(IslandType)).Length];
        island_Rare_Datas = new Island_Rare_Data[System.Enum.GetValues(typeof(IslandType)).Length];
    }

    public int GetMaxValue(IslandType islandType, FoodType foodType)
    {
        return island_Max_Datas[(int)islandType].GetValue(foodType);
    }

    public int GetRareValue(IslandType islandType, FoodType foodType)
    {
        return island_Rare_Datas[(int)islandType].GetValue(foodType);
    }

    public int GetMaxTotalValue()
    {
        int number = 0;

        for (int i = 0; i < island_Max_Datas.Length; i++)
        {
            number += island_Max_Datas[i].GetMaxValue();
        }

        return number;
    }

    public int GetRareTotalValue()
    {
        int number = 0;

        for(int i = 0; i < island_Rare_Datas.Length; i ++)
        {
            number += island_Rare_Datas[i].GetMaxValue();
        }

        return number;
    }
}


[System.Serializable]
public class Island_Max_Data
{
    public int index1 = 0;
    public int index2 = 0;
    public int index3 = 0;
    public int index4 = 0;
    public int index5 = 0;
    public int index6 = 0;
    public int index7 = 0;
    public int index8 = 0;
    public int index9 = 0;

    public void SetValue(FoodType type, int number)
    {
        switch ((int)type)
        {
            case 0:
                index1 += number;
                break;
            case 1:
                index2 += number;
                break;
            case 2:
                index3 += number;
                break;
            case 3:
                index4 += number;
                break;
            case 4:
                index5 += number;
                break;
            case 5:
                index6 += number;
                break;
            case 6:
                index7 += number;
                break;
            case 7:
                index8 += number;
                break;
            case 8:
                index9 += number;
                break;
        }
    }

    public int GetValue(FoodType type)
    {
        int number = 0;

        switch((int)type % 9)
        {
            case 0:
                number = index1;
                break;
            case 1:
                number = index2;
                break;
            case 2:
                number = index3;
                break;
            case 3:
                number = index4;
                break;
            case 4:
                number = index5;
                break;
            case 5:
                number = index6;
                break;
            case 6:
                number = index7;
                break;
            case 7:
                number = index8;
                break;
            case 8:
                number = index9;
                break;
        }

        return number;
    }

    public int GetMaxValue()
    {
        return index1 + index2 + index3 + index4 + index5 + index6 + index7 + index8 + index9;
    }

    public int GetBookValue()
    {
        int number = 0;

        if (index1 > 0)
        {
            number++;
        }

        if (index2 > 0)
        {
            number++;
        }

        if (index3 > 0)
        {
            number++;
        }

        if (index4 > 0)
        {
            number++;
        }

        if (index5 > 0)
        {
            number++;
        }

        if (index6 > 0)
        {
            number++;
        }

        if (index7 > 0)
        {
            number++;
        }

        if (index8 > 0)
        {
            number++;
        }

        if (index9 > 0)
        {
            number++;
        }

        return number;
    }
}

[System.Serializable]
public class Island_Rare_Data
{
    public int index1 = 0;
    public int index2 = 0;
    public int index3 = 0;
    public int index4 = 0;
    public int index5 = 0;
    public int index6 = 0;
    public int index7 = 0;
    public int index8 = 0;
    public int index9 = 0;

    public void SetValue(FoodType type, int number)
    {
        switch ((int)type)
        {
            case 0:
                index1 += number;
                break;
            case 1:
                index2 += number;
                break;
            case 2:
                index3 += number;
                break;
            case 3:
                index4 += number;
                break;
            case 4:
                index5 += number;
                break;
            case 5:
                index6 += number;
                break;
            case 6:
                index7 += number;
                break;
            case 7:
                index8 += number;
                break;
            case 8:
                index9 += number;
                break;
        }
    }

    public int GetValue(FoodType type)
    {
        int number = 0;

        switch ((int)type % 9)
        {
            case 0:
                number = index1;
                break;
            case 1:
                number = index2;
                break;
            case 2:
                number = index3;
                break;
            case 3:
                number = index4;
                break;
            case 4:
                number = index5;
                break;
            case 5:
                number = index6;
                break;
            case 6:
                number = index7;
                break;
            case 7:
                number = index8;
                break;
            case 8:
                number = index9;
                break;

        }

        return number;
    }

    public int GetMaxValue()
    {
        return index1 + index2 + index3 + index4 + index5 + index6 + index7 + index8 + index9;
    }

    public int GetBookValue()
    {
        int number = 0;

        if(index1 > 0)
        {
            number++;
        }

        if (index2 > 0)
        {
            number++;
        }

        if (index3 > 0)
        {
            number++;
        }

        if (index4 > 0)
        {
            number++;
        }

        if (index5 > 0)
        {
            number++;
        }

        if (index6 > 0)
        {
            number++;
        }

        if (index7 > 0)
        {
            number++;
        }

        if (index8 > 0)
        {
            number++;
        }

        if (index9 > 0)
        {
            number++;
        }

        return number;
    }
}

public class IslandManager : MonoBehaviour
{
    public GameObject changeIslandView;

    public GameObject alarmObj;

    public ChangeIslandContent changeIslandContent;

    public RectTransform changeIslandContentTransform;

    public List<ChangeIslandContent> changeIslandContentList = new List<ChangeIslandContent>();

    Sprite[] islandArray;

    public GameManager gameManager;
    public ChangeFoodManager changeFoodManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;
    UpgradeDataBase upgradeDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (upgradeDataBase == null) upgradeDataBase = Resources.Load("UpgradeDataBase") as UpgradeDataBase;

        islandArray = imageDataBase.GetIslandArray();

        changeIslandView.SetActive(false);

        alarmObj.SetActive(true);
    }

    private void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(IslandType)).Length; i++)
        {
            ChangeIslandContent monster = Instantiate(changeIslandContent);
            monster.transform.SetParent(changeIslandContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.Initialize(IslandType.Island1 + i, islandArray[i], this);
            monster.gameObject.SetActive(true);

            changeIslandContentList.Add(monster);
        }

        changeIslandContentTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenChangeIslandView()
    {
        if (!changeIslandView.activeInHierarchy)
        {
            changeIslandView.SetActive(true);

            alarmObj.SetActive(false);

            if ((int)GameStateManager.instance.IslandType > playerDataBase.IslandNumber)
            {
                playerDataBase.IslandNumber = (int)GameStateManager.instance.IslandType;
            }

            Initialize();

            FirebaseAnalytics.LogEvent("Open_Island");
        }
        else
        {
            changeIslandView.SetActive(false);
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < changeIslandContentList.Count; i++)
        {
            changeIslandContentList[i].Locked();
            changeIslandContentList[i].UnSelected();
            changeIslandContentList[i].LevelInitialize();
        }

        changeIslandContentList[(int)GameStateManager.instance.IslandType].Selected();

        changeIslandContentList[0].UnLock();
        changeIslandContentList[0].SetLevel(playerDataBase.NextFoodNumber / 9);

        for(int i = 1; i < changeIslandContentList.Count; i ++)
        {
            if(playerDataBase.IslandNumber >= i)
            {
                changeIslandContentList[i].UnLock();
                changeIslandContentList[i].SetLevel((playerDataBase.NextFoodNumber + 9) / (9 * (i + 1)));
            }
        }
    }

    public void ChangeIsland(IslandType type)
    {
        if (GameStateManager.instance.IslandType == type) return;

        FirebaseAnalytics.LogEvent("Change_Island_" + type.ToString());

        changeIslandView.SetActive(false);

        gameManager.ChangeIsland(type);

        if(changeFoodManager.changeFoodView.activeInHierarchy)
        {
            changeFoodManager.CheckFood();
        }
    }

    public void LevelUp(IslandType type)
    {
        switch (type)
        {
            case IslandType.Island1:
                if(playerDataBase.Island1Level + 1 > 9)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                else
                {
                    if(playerDataBase.Island1Count < (playerDataBase.Island1Level + 1) * 100)
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LimitIslandHeart);
                        return;
                    }
                }

                playerDataBase.Island1Count -= (playerDataBase.Island1Level + 1) * 100;
                playerDataBase.Island1Level += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island1Count", playerDataBase.Island1Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island1Level", playerDataBase.Island1Level);

                changeIslandContentList[0].LevelInitialize();
                break;
            case IslandType.Island2:
                if (playerDataBase.Island2Level + 1 > 9)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                else
                {
                    if (playerDataBase.Island2Count < (playerDataBase.Island2Level + 1) * 100)
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LimitIslandHeart);
                        return;
                    }
                }

                playerDataBase.Island2Count -= (playerDataBase.Island2Level + 1) * 100;
                playerDataBase.Island2Level += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island2Count", playerDataBase.Island2Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island2Level", playerDataBase.Island2Level);

                changeIslandContentList[1].LevelInitialize();
                break;
            case IslandType.Island3:
                if (playerDataBase.Island3Level + 1 > 9)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                else
                {
                    if (playerDataBase.Island3Count < (playerDataBase.Island3Level + 1) * 100)
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LimitIslandHeart);
                        return;
                    }
                }

                playerDataBase.Island3Count -= (playerDataBase.Island3Level + 1) * 100;
                playerDataBase.Island3Level += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island3Count", playerDataBase.Island3Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island3Level", playerDataBase.Island3Level);

                changeIslandContentList[2].LevelInitialize();
                break;
            case IslandType.Island4:
                if (playerDataBase.Island4Level + 1 > 9)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                    return;
                }
                else
                {
                    if (playerDataBase.Island4Count < (playerDataBase.Island4Level + 1) * 100)
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                        NotionManager.instance.UseNotion(NotionType.LimitIslandHeart);
                        return;
                    }
                }

                playerDataBase.Island4Count -= (playerDataBase.Island4Level + 1) * 100;
                playerDataBase.Island4Level += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island4Count", playerDataBase.Island4Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island4Level", playerDataBase.Island4Level);

                changeIslandContentList[3].LevelInitialize();
                break;
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

        FirebaseAnalytics.LogEvent("LevelUp_" + type.ToString());

        GourmetManager.instance.Initialize();
    }
}
