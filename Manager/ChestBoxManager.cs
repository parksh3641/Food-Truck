using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestBoxManager : MonoBehaviour
{
    private RewardType normalRewardType = RewardType.Gold;
    private RewardType epicRewardType = RewardType.Gold;

    public GameObject ingameUI;

    public GameObject chestBoxView;

    public GameObject chestBoxIcon;

    public Image normalRewardIcon;
    public Text normalRewardText;
    public Sprite normalBackground;

    public Image epicRewardIcon;
    public Text epicRewardText;
    public Sprite epicBackground;

    private int number = 0;

    private int count = 0;
    private int goalCount = 0;
    private int random = 0;

    public QuestManager questManager;
    public GameManager gameManager;


    Sprite[] rewardArray;


    WaitForSeconds waitForSeconds = new WaitForSeconds(1);
    WaitForSeconds waitForSeconds2 = new WaitForSeconds(1.5f);

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        rewardArray = imageDataBase.GetRewardArray();
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

        random = Random.Range(0, 100);

        Debug.LogError(random);

        if (random >= 61)
        {
            normalRewardType = RewardType.Gold;
            epicRewardType = RewardType.Gold2;

            normalRewardIcon.sprite = rewardArray[(int)normalRewardType];
            normalRewardText.text = MoneyUnitString.ToCurrencyString(50000);

            epicRewardIcon.sprite = rewardArray[(int)epicRewardType];
            epicRewardText.text = MoneyUnitString.ToCurrencyString(300000);

            if (playerDataBase.Candy1MaxValue > 0)
            {
                epicRewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(1000000) + "</size>";
            }

            if (playerDataBase.JapaneseFood1MaxValue > 0)
            {
                epicRewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(3000000) + "</size>";
            }

            if (playerDataBase.Dessert1MaxValue > 0)
            {
                epicRewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(5000000) + "</size>";
            }

        }
        else if (random > 41)
        {
            normalRewardType = RewardType.Portion1;
            epicRewardType = RewardType.PortionSet;

            normalRewardIcon.sprite = rewardArray[(int)normalRewardType];
            normalRewardText.text = MoneyUnitString.ToCurrencyString(1);

            epicRewardIcon.sprite = rewardArray[(int)epicRewardType];
            epicRewardText.text = MoneyUnitString.ToCurrencyString(10);
        }
        else if (random > 21)
        {
            normalRewardType = RewardType.DefDestroyTicketPiece;
            epicRewardType = RewardType.DefDestroyTicket;

            normalRewardIcon.sprite = rewardArray[(int)normalRewardType];
            normalRewardText.text = MoneyUnitString.ToCurrencyString(1);

            epicRewardIcon.sprite = rewardArray[(int)epicRewardType];
            epicRewardText.text = MoneyUnitString.ToCurrencyString(3);
        }
        else
        {
            normalRewardType = RewardType.Crystal;
            epicRewardType = RewardType.Crystal;

            normalRewardIcon.sprite = rewardArray[(int)normalRewardType];
            normalRewardText.text = MoneyUnitString.ToCurrencyString(10);

            epicRewardIcon.sprite = rewardArray[(int)epicRewardType];
            epicRewardText.text = MoneyUnitString.ToCurrencyString(100);
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

        switch (normalRewardType)
        {
            case RewardType.Gold:
                PlayfabManager.instance.UpdateAddGold(50000);
                break;
            case RewardType.Portion1:
                PortionManager.instance.GetPortion(0, 1);
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
        GameStateManager.instance.ChestBoxCount += 1;
        Success();

        FirebaseAnalytics.LogEvent("OpenChestBox_Ad");

        switch (normalRewardType)
        {
            case RewardType.Gold:
                if (playerDataBase.Dessert1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(5000000);
                    return;
                }

                if (playerDataBase.JapaneseFood1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(3000000);
                    return;
                }

                if (playerDataBase.Candy1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(1000000);
                    return;
                }

                PlayfabManager.instance.UpdateAddGold(300000);
                break;
            case RewardType.Portion1:
                PortionManager.instance.GetRandomPortion(10);
                break;
            case RewardType.DefDestroyTicketPiece:
                PortionManager.instance.GetDefTickets(3);
                break;
            case RewardType.Crystal:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);
                break;
        }
    }

    public void ReceiveInfo()
    {
        ReceiveInfoManager.instance.OpenReceiveInfo(normalRewardType, normalBackground);
    }

    public void ReceiveInfo2()
    {
        ReceiveInfoManager.instance.OpenReceiveInfo(epicRewardType, epicBackground);
    }
}
