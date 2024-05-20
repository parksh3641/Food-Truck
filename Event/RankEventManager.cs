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

    private int totalLevel = 0;
    private int level = 50;
    private int reward = 10000;

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
                case 11:
                    totalLevel = playerDataBase.TotalLevel_11;
                    break;
                case 12:
                    totalLevel = playerDataBase.TotalLevel_12;
                    break;
                case 13:
                    totalLevel = playerDataBase.TotalLevel_13;
                    break;
                case 14:
                    totalLevel = playerDataBase.TotalLevel_14;
                    break;
                case 15:
                    totalLevel = playerDataBase.TotalLevel_15;
                    break;
                case 16:
                    totalLevel = playerDataBase.TotalLevel_16;
                    break;
                case 17:
                    totalLevel = playerDataBase.TotalLevel_17;
                    break;
                case 18:
                    totalLevel = playerDataBase.TotalLevel_18;
                    break;
                case 19:
                    totalLevel = playerDataBase.TotalLevel_19;
                    break;
                case 20:
                    totalLevel = playerDataBase.TotalLevel_20;
                    break;
            }

            rankEventText.text = LocalizationManager.instance.GetString("Ranking2") + " : " + totalLevel;

            CheckRankEvent();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("Open+Event_Rank");
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
            attendanceContentArray[i].receiveContent[0].Initialize(RewardType.Exp, reward);
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

        PortionManager.instance.GetExp(reward);

        playerDataBase.RankEventCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankEventCount", playerDataBase.RankEventCount);

        action.Invoke();

        CheckRankEvent();

        OffAlarm();

        FirebaseAnalytics.LogEvent("Clear_Event_Rank");

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
