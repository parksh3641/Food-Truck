using Firebase.Analytics;
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
    public Text questClearValueText;

    public GameObject lockedObj;
    public GameObject lockedAdObj;
    public GameObject clearObj;
    public GameObject clearAdObj;

    private int value = 0;
    private int reward = 0;

    private bool isDelay = false;

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

            Initialize();

            FirebaseAnalytics.LogEvent("OpenQuest");
        }
        else
        {
            questView.SetActive(false);
        }
    }

    void Initialize()
    {
        questTitleText.text = LocalizationManager.instance.GetString("Quest");
        questTitleText.text += " " + (playerDataBase.QuestCount + 1).ToString();

        reward = questDataBase.reward;

        questClearValueText.text = MoneyUnitString.ToCurrencyString(reward);

        if (playerDataBase.DailyQuestReward == 0)
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
    }

    public void CheckingAlarm()
    {
        if (playerDataBase.DailyQuestReward == 1) return;

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
    + "\n(" + MoneyUnitString.ToCurrencyString(value) + "/" + MoneyUnitString.ToCurrencyString(questInfo.need * ((playerDataBase.QuestCount / 5) + 1)) + ")";

        if (value >= questInfo.need * ((playerDataBase.QuestCount / 5) + 1))
        {
            lockedObj.SetActive(false);
            lockedAdObj.SetActive(false);
        }
    }

    public void ClearButton()
    {
        if (playerDataBase.DailyQuestReward == 1) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        playerDataBase.DailyQuestReward = 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyQuestReward", playerDataBase.DailyQuestReward);

        playerDataBase.QuestCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("QuestCount", playerDataBase.QuestCount);

        PlayfabManager.instance.UpdateAddGold(reward);

        NotionManager.instance.UseNotion(NotionType.QuestNotion);
        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);

        Initialize();

        alarm.SetActive(false);

        FirebaseAnalytics.LogEvent("QuestClear");
    }

    public void ClearAdButton()
    {
        if (playerDataBase.DailyQuestReward == 1) return;

        GoogleAdsManager.instance.admobReward_Quest.ShowAd(6);
    }

    public void SuccessWatchAd()
    {
        playerDataBase.DailyQuestReward = 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyQuestReward", playerDataBase.DailyQuestReward);

        playerDataBase.QuestCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("QuestCount", playerDataBase.QuestCount);

        float random = Random.Range(0, 100f);

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

        PlayfabManager.instance.UpdateAddGold(reward * number);

        NotionManager.instance.UseNotion(NotionType.QuestNotion);
        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);

        Initialize();

        alarm.SetActive(false);

        FirebaseAnalytics.LogEvent("QuestClear");
    }
}
