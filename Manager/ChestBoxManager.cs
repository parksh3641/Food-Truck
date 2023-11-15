using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestBoxManager : MonoBehaviour
{
    private RewardType rewardType = RewardType.Gold;

    public GameObject ingameUI;

    public GameObject chestBoxView;

    public GameObject chestBoxIcon;

    public GameObject[] chestBoxArray;

    private int count = 0;
    private int goalCount = 0;
    private int random = 0;

    public QuestManager questManager;
    public GameManager gameManager;


    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }


    public void Initialize()
    {
        chestBoxIcon.SetActive(false);
        chestBoxView.SetActive(false);

        if (GameStateManager.instance.ChestBoxCount >= 20) return;

        goalCount = GameStateManager.instance.ChestBoxCoolTime;

        count = 0;
        StartCoroutine(TimerCoroution());
    }

    IEnumerator TimerCoroution()
    {
        if(count >= goalCount)
        {
            SetChestBox();
            yield break;
        }
        else
        {
            if(ingameUI.activeInHierarchy)
            {
                count += 1;
            }
        }

        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
    }

    void SetChestBox()
    {
        if(playerDataBase.LockTutorial >= 2)
        {
            chestBoxIcon.SetActive(true);

            SoundManager.instance.PlaySFX(GameSfxType.ChestBox);
        }
        else
        {
            count = 0;
            StartCoroutine(TimerCoroution());
        }
    }

    public void OpenChestBox()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        chestBoxView.SetActive(true);

        chestBoxArray[0].SetActive(false);
        chestBoxArray[1].SetActive(false);
        chestBoxArray[2].SetActive(false);

        random = Random.Range(0, 10);

        if (random >= 8)
        {
            rewardType = RewardType.Gold;

            chestBoxArray[0].SetActive(true);
        }
        else if (random >= 3)
        {
            rewardType = RewardType.PortionSet;

            chestBoxArray[1].SetActive(true);
        }
        else
        {
            rewardType = RewardType.Crystal;

            chestBoxArray[2].SetActive(true);
        }
    }

    public void GetFreeReward()
    {
        switch (rewardType)
        {
            case RewardType.Gold:
                PlayfabManager.instance.UpdateAddGold(200000);

                break;
            case RewardType.PortionSet:
                switch (Random.Range(0, 5))
                {
                    case 0:
                        playerDataBase.Portion1 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
                        break;
                    case 1:
                        playerDataBase.Portion2 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
                        break;
                    case 2:
                        playerDataBase.Portion3 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
                        break;
                    case 3:
                        playerDataBase.Portion4 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
                        break;
                    case 4:
                        playerDataBase.Portion5 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
                        break;
                }

                break;
            case RewardType.Crystal:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1);
                break;
        }

        playerDataBase.OpenChestBox += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OpenChestBox", playerDataBase.OpenChestBox);

        gameManager.CheckPortion();

        questManager.CheckGoal();

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        GameStateManager.instance.ChestBoxCount += 1;

        Initialize();
    }

    public void GetAdsReward()
    {
        GoogleAdsManager.instance.admobReward_ChestBoxGold.ShowAd(2);
    }

    public void SuccessWatchAd()
    {
        switch (rewardType)
        {
            case RewardType.Gold:
                PlayfabManager.instance.UpdateAddGold(2000000);

                break;
            case RewardType.PortionSet:
                switch (Random.Range(0, 5))
                {
                    case 0:
                        playerDataBase.Portion1 += 10;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
                        break;
                    case 1:
                        playerDataBase.Portion2 += 10;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
                        break;
                    case 2:
                        playerDataBase.Portion3 += 10;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
                        break;
                    case 3:
                        playerDataBase.Portion4 += 10;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
                        break;
                    case 4:
                        playerDataBase.Portion5 += 10;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
                        break;
                }
                break;
            case RewardType.Crystal:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 10);
                break;
        }

        playerDataBase.OpenChestBox += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OpenChestBox", playerDataBase.OpenChestBox);

        gameManager.CheckPortion();

        questManager.CheckGoal();

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        GameStateManager.instance.ChestBoxCount += 1;

        FirebaseAnalytics.LogEvent("ChestBox");

        Initialize();
    }
}
