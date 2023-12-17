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

    private int level = 20;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        rankEventView.SetActive(false);
        alarm.SetActive(false);

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

            rankEventText.text = LocalizationManager.instance.GetString("Ranking2_Info") + " : " + playerDataBase.TotalLevel;

            CheckRankEvent();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("RankEvent");
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
            attendanceContentArray[i].InitializeRankEvent(i, level * (i + 1), playerDataBase.RankEventCount, playerDataBase.TotalLevel, this);
        }
    }

    void CheckInitialize()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].receiveContent[0].gameObject.SetActive(true);
            attendanceContentArray[i].receiveContent[0].Initialize(RewardType.TreasureBox, 10);
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

        treasureManager.OpenTreasure(10);

        playerDataBase.RankEventCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankEventCount", playerDataBase.RankEventCount);

        action.Invoke();

        CheckRankEvent();

        OffAlarm();

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
