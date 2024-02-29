using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    public GameObject dungeonView;
    public GameObject dungeonInfoView;

    public RectTransform rectTransform;

    public GameObject alarm;
    public GameObject ingameAlarm;

    public DungeonContent[] dungeonContents;

    public ReceiveContent[] receiveContents;

    [Space]
    [Title("Text")]
    public Text gourmetPointText;
    public Text timerText;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    DateTime f, g;
    TimeSpan h;

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        dungeonView.SetActive(false);
        dungeonInfoView.SetActive(false);

        alarm.SetActive(true);
        ingameAlarm.SetActive(true);

        rectTransform.anchoredPosition = new Vector2(0, -9999);
    }


    public void OpenDungeonView()
    {
        if(!dungeonView.activeInHierarchy)
        {
            dungeonView.SetActive(true);

            alarm.SetActive(false);
            ingameAlarm.SetActive(false);

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            timerText.text = "";
            f = DateTime.Now;
            g = DateTime.Today.AddDays(1);
            StartCoroutine(TimerCoroution());

            Initialize();
        }
        else
        {
            StopAllCoroutines();

            dungeonView.SetActive(false);
        }
    }

    void Initialize()
    {
        gourmetPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.GourmetLevel);

        dungeonContents[0].Initialize(this, DungeonType.Dungeon1, RewardType.Crystal, RewardType.AbilityPoint, ItemType.DungeonKey1, 0);
        dungeonContents[1].Initialize(this, DungeonType.Dungeon2, RewardType.TreasureBox, RewardType.AbilityPoint, ItemType.DungeonKey2, 50000);
        dungeonContents[2].Initialize(this, DungeonType.Dungeon3, RewardType.SliverBox, RewardType.AbilityPoint, ItemType.DungeonKey3, 250000);
        dungeonContents[3].Initialize(this, DungeonType.Dungeon4, RewardType.GoldBox, RewardType.AbilityPoint, ItemType.DungeonKey4, 500000);

        receiveContents[0].Initialize(RewardType.AbilityPoint, 0);
        receiveContents[1].Initialize(RewardType.SliverBox, 0);
        receiveContents[2].Initialize(RewardType.GoldBox, 0);
    }
    IEnumerator TimerCoroution()
    {
        if (timerText.gameObject.activeInHierarchy)
        {
            h = g - f;

            timerText.text = localization_Reset + " : " + h.Hours.ToString("D2") + localization_Hours + " " + h.Minutes.ToString("D2") + localization_Minutes;

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
                yield break;
            }
        }

        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
    }

    public void EnterDungeon(DungeonType type)
    {
        Debug.LogError(type + " ¿‘¿Â");

        switch (type)
        {
            case DungeonType.Dungeon1:
                playerDataBase.DungeonKey1 -= 1;
                playerDataBase.Dungeon1Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey1", playerDataBase.DungeonKey1);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon1Count", playerDataBase.Dungeon1Count);
                break;
            case DungeonType.Dungeon2:
                playerDataBase.DungeonKey2 -= 1;
                playerDataBase.Dungeon2Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey2", playerDataBase.DungeonKey2);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon2Count", playerDataBase.Dungeon2Count);
                break;
            case DungeonType.Dungeon3:
                playerDataBase.DungeonKey3 -= 1;
                playerDataBase.Dungeon3Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey3", playerDataBase.DungeonKey3);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon3Count", playerDataBase.Dungeon3Count);
                break;
            case DungeonType.Dungeon4:
                playerDataBase.DungeonKey4 -= 1;
                playerDataBase.Dungeon4Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey4", playerDataBase.DungeonKey4);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon4Count", playerDataBase.Dungeon4Count);
                break;
        }
    }
}
