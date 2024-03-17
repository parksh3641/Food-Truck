using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        changeIslandContentList[0].SetLevel(playerDataBase.NextFoodNumber / 6);

        if (playerDataBase.IslandNumber >= 1)
        {
            changeIslandContentList[1].UnLock();
            changeIslandContentList[1].SetLevel(playerDataBase.NextFoodNumber2 / 8);
        }

        if (playerDataBase.IslandNumber >= 2)
        {
            changeIslandContentList[2].UnLock();
            changeIslandContentList[2].SetLevel(playerDataBase.NextFoodNumber3 / 6);
        }

        if (playerDataBase.IslandNumber >= 3)
        {
            changeIslandContentList[3].UnLock();
            changeIslandContentList[3].SetLevel(playerDataBase.NextFoodNumber4 / 8);
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
                if(playerDataBase.Island1Level + 1 > 29)
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
                if (playerDataBase.Island2Level + 1 > 29)
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
                if (playerDataBase.Island3Level + 1 > 29)
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
                if (playerDataBase.Island4Level + 1 > 29)
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
