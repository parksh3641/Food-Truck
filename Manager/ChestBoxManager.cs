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

    private int number = 0;

    private int count = 0;
    private int goalCount = 0;
    private int random = 0;

    public QuestManager questManager;
    public GameManager gameManager;


    WaitForSeconds waitForSeconds = new WaitForSeconds(1);
    WaitForSeconds waitForSeconds2 = new WaitForSeconds(1.5f);

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

        goalCount = Random.Range(60, 120);
        goalCount = (int)(goalCount - (goalCount * (0.003f * playerDataBase.Treasure12)));

        if(playerDataBase.AutoPresent)
        {
            goalCount = (int)(goalCount - (goalCount * 0.1f));
        }

#if UNITY_EDITOR
        goalCount = 2;
#endif
        count = 0;
        StartCoroutine(TimerCoroution());

        FirebaseAnalytics.LogEvent("OpenChestBox");
    }

    public void CheckAuto()
    {
        if (!playerDataBase.AutoPresent) return;

        if (playerDataBase.LockTutorial >= 2)
        {
            if (GameStateManager.instance.AutoPresent)
            {
                if (chestBoxIcon.activeInHierarchy)
                {
                    GetFreeReward();
                }
            }
        }
    }

    IEnumerator TimerCoroution()
    {
        if(count >= goalCount)
        {
            StartCoroutine(SetChestBox());
            yield break;
        }
        else
        {
            if(ingameUI.activeInHierarchy && !GameStateManager.instance.Pause)
            {
                count += 1;
            }
        }

        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
    }

    IEnumerator SetChestBox()
    {
        if(playerDataBase.LockTutorial >= 2)
        {
            chestBoxIcon.SetActive(true);

            SoundManager.instance.PlaySFX(GameSfxType.ChestBox);
            NotionManager.instance.UseNotion2(NotionType.OpenChestBoxNotion);

            yield return waitForSeconds2;

            if (GameStateManager.instance.AutoPresent)
            {
                GetFreeReward();
            }
        }
        else
        {
            count = 0;
            StartCoroutine(TimerCoroution());
        }
    }

    public void OpenChestBox()
    {
        if (GameStateManager.instance.AutoPresent) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        chestBoxView.SetActive(true);

        for(int i = 0; i < chestBoxArray.Length; i ++)
        {
            chestBoxArray[i].SetActive(false);
        }

        random = Random.Range(0, 100);

        Debug.LogError(random);

        if (random >= 61)
        {
            rewardType = RewardType.Gold;

            chestBoxArray[0].SetActive(true);
        }
        else if (random > 26)
        {
            rewardType = RewardType.PortionSet;

            chestBoxArray[1].SetActive(true);
        }
        else if (random > 16)
        {
            rewardType = RewardType.DefDestroyTicketPiece;

            chestBoxArray[2].SetActive(true);
        }
        else
        {
            rewardType = RewardType.Crystal;

            chestBoxArray[3].SetActive(true);
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
        //if(Random.Range(0, 10) > 7)
        //{
        //    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        //    NotionManager.instance.UseNotion(NotionType.FailGetItem);

        //    Initialize();
        //    return;
        //}

        switch (rewardType)
        {
            case RewardType.Gold:
                PlayfabManager.instance.UpdateAddGold(50000);
                break;
            case RewardType.PortionSet:
                PortionManager.instance.GetRandomPortion(1);
                break;
            case RewardType.DefDestroyTicketPiece:
                PortionManager.instance.GetDefTicketPiece(1);
                break;
            case RewardType.Crystal:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 10);
                break;
        }

        Success();

        FirebaseAnalytics.LogEvent("OpenChestBox_Free");
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
                PlayfabManager.instance.UpdateAddGold(500000);
                break;
            case RewardType.PortionSet:
                PortionManager.instance.GetRandomPortion(5);
                break;
            case RewardType.DefDestroyTicketPiece:
                PortionManager.instance.GetDefTickets(1);
                break;
            case RewardType.Crystal:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);
                break;
        }

        GameStateManager.instance.ChestBoxCount += 1;
        Success();

        FirebaseAnalytics.LogEvent("OpenChestBox_Ad");
    }
}
