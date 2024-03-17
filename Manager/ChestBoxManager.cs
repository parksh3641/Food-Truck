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

    private int portion = 0;

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

        goalCount = 120;
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

        FirebaseAnalytics.LogEvent("Open_ChestBox");
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
            if(ingameUI.activeInHierarchy  && !GameStateManager.instance.YoutubeVideo)
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

        //Debug.LogError(random);

        if (random >= 71)
        {
            normalRewardType = RewardType.Gold;
            epicRewardType = RewardType.Gold2;

            normalRewardIcon.sprite = rewardArray[(int)normalRewardType];
            normalRewardText.text = MoneyUnitString.ToCurrencyString(50000);

            epicRewardIcon.sprite = rewardArray[(int)epicRewardType];
            epicRewardText.text = MoneyUnitString.ToCurrencyString(1000000);

            if (playerDataBase.Candy1MaxValue > 0)
            {
                normalRewardText.text = MoneyUnitString.ToCurrencyString(300000);
                epicRewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(3000000) + "</size>";
            }

            if (playerDataBase.JapaneseFood1MaxValue > 0)
            {
                normalRewardText.text = MoneyUnitString.ToCurrencyString(500000);
                epicRewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(5000000) + "</size>";
            }

            if (playerDataBase.Dessert1MaxValue > 0)
            {
                normalRewardText.text = MoneyUnitString.ToCurrencyString(1000000);
                epicRewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(10000000) + "</size>";
            }

        }
        else if (random > 31)
        {
            portion = Random.Range(0, 5);

            switch(portion)
            {
                case 0:
                    normalRewardType = RewardType.Portion1;
                    epicRewardType = RewardType.Portion1;
                    break;
                case 1:
                    normalRewardType = RewardType.Portion2;
                    epicRewardType = RewardType.Portion2;
                    break;
                case 2:
                    normalRewardType = RewardType.Portion3;
                    epicRewardType = RewardType.Portion3;
                    break;
                case 3:
                    normalRewardType = RewardType.Portion4;
                    epicRewardType = RewardType.Portion4;
                    break;
                case 4:
                    normalRewardType = RewardType.Portion5;
                    epicRewardType = RewardType.Portion5;
                    break;
            }

            normalRewardIcon.sprite = rewardArray[(int)normalRewardType];
            normalRewardText.text = MoneyUnitString.ToCurrencyString(1);

            epicRewardIcon.sprite = rewardArray[(int)epicRewardType];
            epicRewardText.text = "5 ~ 10";
        }
        else if (random > 11)
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
                if (playerDataBase.Dessert1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(1000000);
                }
                else if (playerDataBase.JapaneseFood1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(500000);
                }
                else if (playerDataBase.Candy1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(300000);
                }
                else
                {
                    PlayfabManager.instance.UpdateAddGold(50000);
                }
                break;
            case RewardType.Portion1:
                PortionManager.instance.GetPortion(0, 1);
                break;
            case RewardType.Portion2:
                PortionManager.instance.GetPortion(1, 1);
                break;
            case RewardType.Portion3:
                PortionManager.instance.GetPortion(2, 1);
                break;
            case RewardType.Portion4:
                PortionManager.instance.GetPortion(3, 1);
                break;
            case RewardType.Portion5:
                PortionManager.instance.GetPortion(4, 1);
                break;
            case RewardType.DefDestroyTicketPiece:
                PortionManager.instance.GetDefTicketPiece(1);
                break;
            case RewardType.Crystal:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 10);
                break;
        }

        Success();

        FirebaseAnalytics.LogEvent("Open_ChestBox_Free");
    }

    public void GetAdsReward()
    {
        GoogleAdsManager.instance.admobReward_ChestBoxGold.ShowAd(2);
    }

    public void SuccessWatchAd()
    {
        GameStateManager.instance.ChestBoxCount += 1;

        FirebaseAnalytics.LogEvent("Open_ChestBox_Ad");

        switch (epicRewardType)
        {
            case RewardType.Gold2:
                if (playerDataBase.Dessert1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(10000000);
                }
                else if (playerDataBase.JapaneseFood1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(5000000);
                }
                else if (playerDataBase.Candy1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(3000000);
                }
                else
                {
                    PlayfabManager.instance.UpdateAddGold(1000000);
                }
                break;
            case RewardType.Portion1:
                PortionManager.instance.GetPortion(0, Random.Range(5, 11));
                break;
            case RewardType.Portion2:
                PortionManager.instance.GetPortion(1, Random.Range(5, 11));
                break;
            case RewardType.Portion3:
                PortionManager.instance.GetPortion(2, Random.Range(5, 11));
                break;
            case RewardType.Portion4:
                PortionManager.instance.GetPortion(3, Random.Range(5, 11));
                break;
            case RewardType.Portion5:
                PortionManager.instance.GetPortion(4, Random.Range(5, 11));
                break;
            case RewardType.DefDestroyTicketPiece:
                PortionManager.instance.GetDefTickets(3);
                break;
            case RewardType.Crystal:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);
                break;
        }

        Success();
    }

    void Success()
    {
        playerDataBase.OpenChestBox += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OpenChestBox", playerDataBase.OpenChestBox);

        GameStateManager.instance.OpenChestBox += 1;

        GameManager.instance.CheckPortion();

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        FirebaseAnalytics.LogEvent("Open_ChestBox");

        Initialize();
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
