using Firebase.Analytics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questView;

    public GameObject alarm;

    public Text questTitleText;
    public Text questInfoText;
    public LocalizationContent questClearTitleText;
    public ReceiveContent[] receiveContents;

    public Text questClearCountText;

    public GameObject lockedObj;
    public GameObject lockedAdObj;
    public GameObject clearObj;
    public GameObject clearAdObj;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;
    public GameObject[] contentArray;

    private int value = 0;
    private int reward = 0;
    private int reward2 = 0;

    private int plus = 0;

    private int index = -1;

    QuestType questType = QuestType.UpgradeCount;
    QuestInfo questInfo = new QuestInfo();

    PlayerDataBase playerDataBase;
    QuestDataBase questDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (questDataBase == null) questDataBase = Resources.Load("QuestDataBase") as QuestDataBase;

        questView.SetActive(false);
        alarm.SetActive(false);
        clearObj.SetActive(false);
        clearAdObj.SetActive(false);
    }

    public void OpenQuestView()
    {
        if(!questView.activeInHierarchy)
        {
            if (playerDataBase.AttendanceDay == System.DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            questView.SetActive(true);

            if (index == -1)
            {
                ChangeTopToggle(0);
            }

            FirebaseAnalytics.LogEvent("Open_Quest");
        }
        else
        {
            questView.SetActive(false);
        }
    }

    public void ChangeTopToggle(int number)
    {
        if (number == 1 || number == 2)
        {
            NotionManager.instance.UseNotion(NotionType.ComingSoon);
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            return;
        }

        if (index == number) return;

        index = number;

        for (int i = 0; i < topMenuImgArray.Length; i++)
        {
            topMenuImgArray[i].sprite = topMenuSpriteArray[0];
            contentArray[i].gameObject.SetActive(false);
        }

        topMenuImgArray[number].sprite = topMenuSpriteArray[1];
        contentArray[number].gameObject.SetActive(true);

        switch (number)
        {
            case 0:
                Initialize();
                break;
            case 1:

                break;
            case 2:

                break;
        }
    }

    void Initialize()
    {
        questTitleText.text = LocalizationManager.instance.GetString("Quest");
        questTitleText.text += " " + (playerDataBase.QuestCount + 1).ToString();

        plus = 0;

        if (IsWeekend())
        {
            reward = questDataBase.reward * 2;
            reward2 = questDataBase.reward2 * 2;

            plus += 100;
        }
        else
        {
            reward = questDataBase.reward;
            reward2 = questDataBase.reward2;
        }

        if((playerDataBase.QuestCount / 5) > 10)
        {
            reward = reward * 2;
        }
        else
        {
            reward += (int)(reward * (1f * (playerDataBase.QuestCount / 5)));
        }

        reward = Mathf.RoundToInt(reward + (reward * (0.01f * ((playerDataBase.Treasure11 * 0.5f) + playerDataBase.GetEquipValue(EquipType.Equip_Index_9)))));
        reward2 = Mathf.RoundToInt(reward2 + (reward2 * (0.01f * ((playerDataBase.Treasure11 * 0.5f) + playerDataBase.GetEquipValue(EquipType.Equip_Index_9)))));

        plus += playerDataBase.Treasure11;

        questClearTitleText.localizationName = "ClearReward";
        if (plus > 0)
        {
            questClearTitleText.plusText = "  (+" + plus.ToString() + "%)";
        }

        receiveContents[0].Initialize(RewardType.Gold, reward);
        receiveContents[1].Initialize(RewardType.Crystal, reward2);

        questClearTitleText.ReLoad();

        questClearCountText.text = LocalizationManager.instance.GetString("ClearCount") + " : " + playerDataBase.QuestCount;

        if (playerDataBase.resetInfo.dailyQuestReward == 0)
        {
            questType = QuestType.UpgradeCount + (playerDataBase.QuestCount % 5);
            questInfo = questDataBase.GetQuestInfo(questType);

            CheckGoal();
        }
        else
        {
            questInfoText.text = LocalizationManager.instance.GetString("QuestInfo");

            lockedObj.SetActive(true);
            lockedAdObj.SetActive(true);

            clearObj.SetActive(true);
            clearAdObj.SetActive(true);
        }

#if UNITY_EDITOR
        lockedObj.SetActive(false);
        lockedAdObj.SetActive(false);
#endif
    }

    public void CheckingAlarm()
    {
        if (playerDataBase.resetInfo.dailyQuestReward == 1) return;

        switch (questType)
        {
            case QuestType.UpgradeCount:
                value = GameStateManager.instance.UpgradeCount;
                break;
            case QuestType.SellCount:
                value = GameStateManager.instance.SellCount;
                break;
            case QuestType.UseSources:
                value = GameStateManager.instance.UseSauce;
                break;
            case QuestType.OpenChestBox:
                value = GameStateManager.instance.OpenChestBox;
                break;
            case QuestType.YummyTime:
                value = GameStateManager.instance.YummyTimeCount;
                break;
        }

        if (value >= questInfo.need * ((playerDataBase.QuestCount / 5) + 1))
        {
            alarm.SetActive(true);
        }
    }

    public void CheckGoal()
    {
        lockedObj.SetActive(true);
        lockedAdObj.SetActive(true);

        switch (questType)
        {
            case QuestType.UpgradeCount:
                value = GameStateManager.instance.UpgradeCount;
                break;
            case QuestType.SellCount:
                value = GameStateManager.instance.SellCount;
                break;
            case QuestType.UseSources:
                value = GameStateManager.instance.UseSauce;
                break;
            case QuestType.OpenChestBox:
                value = GameStateManager.instance.OpenChestBox;
                break;
            case QuestType.YummyTime:
                value = GameStateManager.instance.YummyTimeCount;
                break;
        }

        questInfoText.text = LocalizationManager.instance.GetString("Quest" + ((playerDataBase.QuestCount % 5) + 1).ToString())
    + "\n( " + MoneyUnitString.ToCurrencyString(value) + " / " + MoneyUnitString.ToCurrencyString(questInfo.need * ((playerDataBase.QuestCount / 5) + 1)) + " )";

        if (value >= questInfo.need * ((playerDataBase.QuestCount / 5) + 1))
        {
            lockedObj.SetActive(false);
            lockedAdObj.SetActive(false);
        }
    }

    public void ClearButton()
    {
        if (playerDataBase.resetInfo.dailyQuestReward == 1) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        ResetManager.instance.SetResetInfo(ResetType.DailyQuestReward);

        playerDataBase.QuestCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("QuestCount", playerDataBase.QuestCount);

        questClearCountText.text = LocalizationManager.instance.GetString("ClearCount") + " : " + playerDataBase.QuestCount;

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, reward2);
        PlayfabManager.instance.UpdateSellPriceGold(reward);
        PlayfabManager.instance.moneyAnimation.PlusMoney(reward);

        QuestClear();
    }

    public void ClearAdButton()
    {
        if (playerDataBase.resetInfo.dailyQuestReward == 1) return;

        GoogleAdsManager.instance.admobReward_Quest.ShowAd(6);
    }

    public void SuccessWatchAd()
    {
        ResetManager.instance.SetResetInfo(ResetType.DailyQuestReward);

        playerDataBase.QuestCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("QuestCount", playerDataBase.QuestCount);

        questClearCountText.text = LocalizationManager.instance.GetString("ClearCount") + " : " + playerDataBase.QuestCount;

        float random = UnityEngine.Random.Range(0, 100f);

        int number = 0;

        if(random >= 60f)
        {
            number = 2;
        }
        else if (random >= 40f)
        {
            number = 3;
        }
        else if (random >= 25f)
        {
            number = 4;
        }
        else if (random >= 15f)
        {
            number = 5;
        }
        else if (random >= 10f)
        {
            number = 6;
        }
        else if (random >= 6f)
        {
            number = 7;
        }
        else if (random >= 3f)
        {
            number = 8;
        }
        else if (random >= 1f)
        {
            number = 9;
        }
        else
        {
            number = 10;
        }

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, reward2 * number);

        PlayfabManager.instance.UpdateSellPriceGold(reward * number);
        PlayfabManager.instance.moneyAnimation.PlusMoney(reward * number);

        QuestClear();
    }

    void QuestClear()
    {
        //NotionManager.instance.UseNotion(NotionType.QuestNotion);
        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);

        Initialize();

        alarm.SetActive(false);

        FirebaseAnalytics.LogEvent("Clear_Quest");

        GourmetManager.instance.Initialize();
    }

    bool IsWeekend()
    {
        DayOfWeek currentDay = DateTime.Now.DayOfWeek;

        return currentDay == DayOfWeek.Saturday || currentDay == DayOfWeek.Sunday;
    }
}
