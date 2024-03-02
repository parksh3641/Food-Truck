using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankEventManager : MonoBehaviour
{
    public GameObject rankEventView;

    public Text rankEventText;

    public RectTransform rankEventRectTransform;

    public GameObject mainAlarm;
    public GameObject alarm;

    public AttendanceContent[] attendanceContentArray;

    public TreasureManager treasureManager;

    private int totalLevel = 0;
    private int level = 20;
    private int reward = 3;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        rankEventView.SetActive(false);

        rankEventRectTransform.anchoredPosition = new Vector2(0, -9999);

        mainAlarm.SetActive(false);
        alarm.SetActive(false);
    }

    public void Initialize()
    {
        mainAlarm.SetActive(true);
        alarm.SetActive(true);
    }

    public void OpenRankEventView()
    {
        if (!rankEventView.activeInHierarchy)
        {
            rankEventView.SetActive(true);

            alarm.SetActive(false);
            mainAlarm.SetActive(false);

            switch(SeasonManager.instance.CheckSeason_Ranking())
            {
                case -1:
                    rankEventView.SetActive(false);

                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.SeasonWaitNotion);
                    break;
                case 0:
                    totalLevel = playerDataBase.TotalLevel;
                    break;
                case 1:
                    totalLevel = playerDataBase.TotalLevel_1;
                    break;
                case 2:
                    totalLevel = playerDataBase.TotalLevel_2;
                    break;
                case 3:
                    totalLevel = playerDataBase.TotalLevel_3;
                    break;
                case 4:
                    totalLevel = playerDataBase.TotalLevel_4;
                    break;
                case 5:
                    totalLevel = playerDataBase.TotalLevel_5;
                    break;
                case 6:
                    totalLevel = playerDataBase.TotalLevel_6;
                    break;
                case 7:
                    totalLevel = playerDataBase.TotalLevel_7;
                    break;
                case 8:
                    totalLevel = playerDataBase.TotalLevel_8;
                    break;
                case 9:
                    totalLevel = playerDataBase.TotalLevel_9;
                    break;
                case 10:
                    totalLevel = playerDataBase.TotalLevel_10;
                    break;
            }

            rankEventText.text = LocalizationManager.instance.GetString("Ranking2_Info") + " : " + totalLevel;

            CheckRankEvent();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("OpenRankEvent");
        }
        else
        {
            rankEventView.SetActive(false);
        }
    }

    void CheckRankEvent()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].InitializeRankEvent(i, level * (i + 1), playerDataBase.RankEventCount, totalLevel, this);
        }
    }

    void CheckInitialize()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].receiveContent[0].gameObject.SetActive(true);
            attendanceContentArray[i].receiveContent[0].Initialize(RewardType.TreasureBox, reward);
        }
    }

    public void ReceiveButton(Action action)
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        treasureManager.OpenTreasure(reward);

        playerDataBase.RankEventCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankEventCount", playerDataBase.RankEventCount);

        action.Invoke();

        CheckRankEvent();

        OffAlarm();

        FirebaseAnalytics.LogEvent("RankEventClear");

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }

    public void SetAlarm()
    {
        mainAlarm.SetActive(true);
        alarm.SetActive(true);
    }

    public void OffAlarm()
    {
        mainAlarm.SetActive(false);
        alarm.SetActive(false);
    }
}
