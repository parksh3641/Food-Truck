using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimeManager : MonoBehaviour
{
    public GameObject playTimeView;

    public Text playTimeText;

    public RectTransform playTimeRectTransform;

    public GameObject mainAlarm;
    public GameObject alarm;

    public AttendanceContent[] attendanceContentArray;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        playTimeView.SetActive(false);
        alarm.SetActive(false);

        playTimeRectTransform.anchoredPosition = new Vector2(0, -9999);

        mainAlarm.SetActive(false);
        alarm.SetActive(false);
    }

    public void Initialize()
    {
        if(playerDataBase.PlayTimeCount < 6)
        {
            mainAlarm.SetActive(true);
            alarm.SetActive(true);
        }
    }

    public void OpenPlayTimeView()
    {
        if (!playTimeView.activeInHierarchy)
        {
            playTimeView.SetActive(true);

            playTimeText.text = LocalizationManager.instance.GetString("PlayTime") + " : " + GameStateManager.instance.PlayTime;

            CheckPlayTime();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("Open_Event_PlayTime");
        }
        else
        {
            playTimeView.SetActive(false);
        }
    }

    void CheckPlayTime()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].InitializePlayTime(GameStateManager.instance.PlayTime, playerDataBase.PlayTimeCount, this);
        }
    }

    void CheckInitialize()
    {
        attendanceContentArray[0].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[0].receiveContent[0].Initialize(RewardType.RepairTicket, 2);

        attendanceContentArray[1].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[1].receiveContent[0].Initialize(RewardType.Crystal, 10);

        attendanceContentArray[2].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[2].receiveContent[0].Initialize(RewardType.Exp, 10000);

        attendanceContentArray[3].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[3].receiveContent[0].Initialize(RewardType.Crystal, 20);

        attendanceContentArray[4].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[4].receiveContent[0].Initialize(RewardType.EventTicket, 5);

        attendanceContentArray[5].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[5].receiveContent[0].Initialize(RewardType.TreasureBox, 1);
    }

    public void ReceiveButton(int index, Action action)
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        switch (index)
        {
            case 0:
                PortionManager.instance.GetRepairTickets(2);
                break;
            case 1:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 10);
                break;
            case 2:
                PortionManager.instance.GetExp(10000);
                break;
            case 3:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 20);
                break;
            case 4:
                PortionManager.instance.GetEventTicket(10);
                break;
            case 5:
                TreasureManager.instance.OpenTreasure(1);
                break;
        }

        playerDataBase.PlayTimeCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("PlayTimeCount", playerDataBase.PlayTimeCount);

        action.Invoke();

        CheckPlayTime();

        FirebaseAnalytics.LogEvent("Clear_Event_PlayTime : " + index);

        if (playerDataBase.PlayTimeCount > 5)
        {
            OffAlarm();

            FirebaseAnalytics.LogEvent("AllClear_Event_PlayTime");
        }

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
