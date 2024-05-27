using Firebase.Analytics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Island_Total_Data
{
    [Header("¸¸·¾ È½¼ö")]
    public Island_Max_Data[] island_Max_Datas;

    [Header("·¹¾î È½¼ö")]
    public Island_Rare_Data[] island_Rare_Datas;

    [Header("Âø¿ëµÈ Ä¡Àå")]
    public Island_Equip_Data[] island_Equip_Datas;

    public void SaveServerData(Island_Total_Data total)
    {
        if(island_Max_Datas.Length >= total.island_Max_Datas.Length)
        {
            for (int i = 0; i < total.island_Max_Datas.Length; i++)
            {
                island_Max_Datas[i] = total.island_Max_Datas[i];
            }

            for (int i = 0; i < total.island_Rare_Datas.Length; i++)
            {
                island_Rare_Datas[i] = total.island_Rare_Datas[i];
            }

            for (int i = 0; i < total.island_Equip_Datas.Length; i++)
            {
                island_Equip_Datas[i] = total.island_Equip_Datas[i];
            }
        }
        else
        {
            Island_Max_Data[] island_Max_DatasArray = new Island_Max_Data[Enum.GetValues(typeof(FoodType)).Length];
            Island_Rare_Data[] island_Rare_DatasArray = new Island_Rare_Data[Enum.GetValues(typeof(FoodType)).Length];
            Island_Equip_Data[] island_Equip_DatasArray = new Island_Equip_Data[Enum.GetValues(typeof(FoodType)).Length];

            for (int i = 0; i < total.island_Max_Datas.Length; i++)
            {
                total.island_Max_Datas[i] = island_Max_DatasArray[i];
                total.island_Rare_Datas[i] = island_Rare_DatasArray[i];
                total.island_Equip_Datas[i] = island_Equip_DatasArray[i];
            }

            island_Max_Datas = new Island_Max_Data[Enum.GetValues(typeof(FoodType)).Length];
            island_Rare_Datas = new Island_Rare_Data[Enum.GetValues(typeof(FoodType)).Length];
            island_Equip_Datas = new Island_Equip_Data[Enum.GetValues(typeof(FoodType)).Length];

            for (int i = 0; i < island_Max_DatasArray.Length; i++)
            {
                island_Max_DatasArray[i] = total.island_Max_Datas[i];
                island_Rare_DatasArray[i] = total.island_Rare_Datas[i];
                island_Equip_DatasArray[i] = total.island_Equip_Datas[i];
            }
        }

        island_Max_Datas[0].index1 = 1;
    }

    public void Initialize()
    {
        for(int i = 0; i < island_Max_Datas.Length; i ++)
        {
            island_Max_Datas[i].Initialize();
            island_Rare_Datas[i].Initialize();
            island_Equip_Datas[i].Initialize();
        }
    }

    public int GetMaxValue(IslandType islandType, FoodType foodType)
    {
        return island_Max_Datas[(int)islandType].GetValue((int)foodType % GameStateManager.instance.Island);
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
            if (island_Max_Datas[i] != null)
            {
                number += island_Max_Datas[i].GetMaxValue();
            }
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

    public void Initialize()
    {
        index1 = 0;
        index2 = 0;
        index3 = 0;
        index4 = 0;
        index5 = 0;
    }

    public void SetValue(int index, int number)
    {
        switch (index)
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
        }
    }

    //public int GetValue(int index)
    //{
    //    int number = 0;

    //    switch(index)
    //    {
    //        case 0:
    //            number = index1;
    //            break;
    //        case 1:
    //            number = index2;
    //            break;
    //        case 2:
    //            number = index3;
    //            break;
    //        case 3:
    //            number = index4;
    //            break;
    //        case 4:
    //            number = index5;
    //            break;
    //    }

    //    return number;
    //}

    public int GetValue(int index)
    {
        int number = 0;

        switch (index)
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
        }

        return number;
    }

    public int GetMaxValue()
    {
        return index1 + index2 + index3 + index4 + index5;
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

    public void Initialize()
    {
        index1 = 0;
        index2 = 0;
        index3 = 0;
        index4 = 0;
        index5 = 0;
    }

    public void SetValue(int index, int number)
    {
        switch (index)
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
        }
    }

    public int GetValue(FoodType type)
    {
        int number = 0;

        switch ((int)type)
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

        }

        return number;
    }

    public int GetValue(int index)
    {
        int number = 0;

        switch (index)
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
        }

        return number;
    }

    public int GetMaxValue()
    {
        return index1 + index2 + index3 + index4 + index5;
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

        return number;
    }
}

[System.Serializable]
public class Island_Equip_Data
{
    public int index1 = 0;
    public int index2 = 0;
    public int index3 = 0;
    public int index4 = 0;
    public int index5 = 0;

    public void Initialize()
    {
        index1 = 0;
        index2 = 0;
        index3 = 0;
        index4 = 0;
        index5 = 0;
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

    [Title("Reward")]
    public GameObject clearView;

    public Image clearIcon;

    public Text clearText;

    public ReceiveContent receiveContent;

    private int index = 0;

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

        clearView.SetActive(false);
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
        changeIslandContentList[0].SetLevel(((playerDataBase.NextFoodNumber + 1) * 1.0f) / (GameStateManager.instance.Island * 1.0f));

        for(int i = 1; i < changeIslandContentList.Count; i ++)
        {
            if(playerDataBase.IslandNumber >= i)
            {
                changeIslandContentList[i].UnLock();

                if (playerDataBase.NextFoodNumber + 1 >= GameStateManager.instance.Island * i)
                {
                    changeIslandContentList[i].SetLevel(((((playerDataBase.NextFoodNumber + 1) - GameStateManager.instance.Island * i) * 1.0f) / ((GameStateManager.instance.Island * 1.0f))));
                }
                else
                {
                    changeIslandContentList[i].SetLevel(0);
                }
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

    public void NewIsland(int number)
    {
        if (number > Enum.GetValues(typeof(IslandType)).Length - 1) return;

        if (playerDataBase.IslandReward >= number) return;

        index = number;

        clearView.SetActive(true);

        clearIcon.sprite = imageDataBase.GetIslandArray(IslandType.Island1 + number);

        clearText.text = LocalizationManager.instance.GetString((IslandType.Island1 + number).ToString());

        receiveContent.Initialize(RewardType.Crystal, 300);
    }

    public void ClearButton()
    {
        clearView.SetActive(false);

        playerDataBase.IslandReward = index;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandReward", playerDataBase.IslandReward);

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);

        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }
}
