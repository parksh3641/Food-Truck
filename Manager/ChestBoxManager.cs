using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestBoxManager : MonoBehaviour
{
    private RewardType rewardType = RewardType.Gold;

    public GameObject ingameUI;

    public Text[] portionPlusTextArray;

    public GameObject chestBoxView;

    public GameObject chestBoxIcon;

    public GameObject[] chestBoxArray;

    private int number = 0;

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

        for(int i = 0; i < portionPlusTextArray.Length; i ++)
        {
            portionPlusTextArray[i].gameObject.SetActive(false);
        }
    }


    public void Initialize()
    {
        chestBoxIcon.SetActive(false);
        chestBoxView.SetActive(false);

        GameStateManager.instance.Pause = false;

        if (GameStateManager.instance.ChestBoxCount >= 20) return;

        goalCount = Random.Range(60, 120);

#if UNITY_EDITOR
        goalCount = 1;
#endif

        count = 0;
        StartCoroutine(TimerCoroution());

        FirebaseAnalytics.LogEvent("ChestBox");
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

        GameStateManager.instance.Pause = true;

        chestBoxView.SetActive(true);

        chestBoxArray[0].SetActive(false);
        chestBoxArray[1].SetActive(false);
        chestBoxArray[2].SetActive(false);

        random = Random.Range(0, 100);

        if (random >= 61)
        {
            rewardType = RewardType.Gold;

            chestBoxArray[0].SetActive(true);
        }
        else if (random > 5)
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

    void Success()
    {
        playerDataBase.OpenChestBox += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OpenChestBox", playerDataBase.OpenChestBox);

        GameStateManager.instance.OpenChestBox += 1;

        gameManager.CheckPortion();

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        Initialize();
    }

    public void GetFreeReward()
    {
        if(Random.Range(0, 10) > 6)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.FailGetItem);

            Initialize();
            return;
        }

        switch (rewardType)
        {
            case RewardType.Gold:
                PlayfabManager.instance.UpdateAddGold(100000);

                break;
            case RewardType.PortionSet:

                number = Random.Range(0, 4);

                switch (number)
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
                    //case 4:
                    //    playerDataBase.Portion5 += 1;
                    //    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
                    //    break;
                }

                portionPlusTextArray[number].gameObject.SetActive(false);
                portionPlusTextArray[number].gameObject.SetActive(true);
                portionPlusTextArray[number].text = "+1";

                break;
            case RewardType.Crystal:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1);
                break;
        }

        Success();
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
                PlayfabManager.instance.UpdateAddGold(300000);

                break;
            case RewardType.PortionSet:

                number = Random.Range(0, 4);

                switch (number)
                {
                    case 0:
                        playerDataBase.Portion1 += 3;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
                        break;
                    case 1:
                        playerDataBase.Portion2 += 3;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
                        break;
                    case 2:
                        playerDataBase.Portion3 += 3;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
                        break;
                    case 3:
                        playerDataBase.Portion4 += 3;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
                        break;
                    //case 4:
                    //    playerDataBase.Portion5 += 3;
                    //    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
                    //    break;
                }

                portionPlusTextArray[number].gameObject.SetActive(false);
                portionPlusTextArray[number].gameObject.SetActive(true);
                portionPlusTextArray[number].text = "+3";

                break;
            case RewardType.Crystal:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 3);
                break;
        }

        GameStateManager.instance.ChestBoxCount += 1;
        Success();
    }
}
