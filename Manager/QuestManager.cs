using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questView;

    public Text questTitleText;
    public Text questInfoText;

    public GameObject clearObj;

    private int value = 0;

    QuestType questType = QuestType.HamburgerMaxValue;
    QuestInfo questInfo = new QuestInfo();

    PlayerDataBase playerDataBase;
    QuestDataBase questDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (questDataBase == null) questDataBase = Resources.Load("QuestDataBase") as QuestDataBase;

        questView.SetActive(false);
        clearObj.SetActive(false);
    }

    public void Initialize()
    {
        questView.SetActive(true);

        questTitleText.text = LocalizationManager.instance.GetString("Quest");
        questTitleText.text += " " + (playerDataBase.QuestCount + 1).ToString();

        questType = QuestType.HamburgerMaxValue + (playerDataBase.QuestCount % 10);

        questInfo = questDataBase.GetQuestInfo(questType);

        CheckGoal();
    }

    public void CheckGoal()
    {
        clearObj.SetActive(false);

        switch (questType)
        {
            case QuestType.HamburgerMaxValue:
                value = playerDataBase.HamburgerMaxValue;
                break;
            case QuestType.SandwichMaxValue:
                value = playerDataBase.SandwichMaxValue;
                break;
            case QuestType.SnackLabMaxValue:
                value = playerDataBase.SnackLabMaxValue;
                break;
            case QuestType.DrinkMaxValue:
                value = playerDataBase.DrinkMaxValue;
                break;
            case QuestType.PizzaMaxValue:
                value = playerDataBase.PizzaMaxValue;
                break;
            case QuestType.UpgradeCount:
                value = playerDataBase.UpgradeCount;
                break;
            case QuestType.SellCount:
                value = playerDataBase.SellCount;
                break;
            case QuestType.UseSources:
                value = playerDataBase.UseSources;
                break;
            case QuestType.OpenChestBox:
                value = playerDataBase.OpenChestBox;
                break;
            case QuestType.FeverModeCount:
                value = playerDataBase.FeverModeCount;
                break;
        }

        questInfoText.text = LocalizationManager.instance.GetString("Quest" + ((playerDataBase.QuestCount % 10) + 1).ToString())
    + " (" + value + "/" + questInfo.need * ((playerDataBase.QuestCount / 10) + 1) + ")";

        if (value >= questInfo.need * ((playerDataBase.QuestCount / 10) + 1))
        {
            clearObj.SetActive(true);
        }
    }

    public void ClearButton()
    {
        playerDataBase.QuestCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("QuestCount", playerDataBase.QuestCount);

        int gold = questDataBase.reward * ((playerDataBase.QuestCount / 10) + 1);

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, gold);

        NotionManager.instance.UseNotion(NotionType.QuestNotion);
        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);

        Initialize();

        FirebaseAnalytics.LogEvent("ClearQuest");
    }
}
