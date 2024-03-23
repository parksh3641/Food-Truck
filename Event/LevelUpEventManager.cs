using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpEventManager : MonoBehaviour
{
    public GameObject levelUpEventView;

    public Text levelUpEventText;

    public RectTransform levelUpEventRectTransform;

    public GameObject mainAlarm;
    public GameObject alarm;

    public AttendanceContent[] attendanceContentArray;

    private int level = 5;
    private int reward = 100;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        levelUpEventView.SetActive(false);

        levelUpEventRectTransform.anchoredPosition = new Vector2(0, -9999);

        mainAlarm.SetActive(false);
        alarm.SetActive(false);
    }

    public void Initialize()
    {
        mainAlarm.SetActive(true);
        alarm.SetActive(true);
    }

    public void OpenLevelUpView()
    {
        if (!levelUpEventView.activeInHierarchy)
        {
            levelUpEventView.SetActive(true);

            alarm.SetActive(false);
            mainAlarm.SetActive(false);

            levelUpEventText.text = LocalizationManager.instance.GetString("LevelUpEvent") + " : " + playerDataBase.Level;

            CheckLevelUpEvent();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("Open_Event_LevelUp");
        }
        else
        {
            levelUpEventView.SetActive(false);
        }
    }

    void CheckLevelUpEvent()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].InitializeLevelUpEvent(i, level * (i + 1), playerDataBase.LevelUpEventCount, playerDataBase.Level, this);
        }
    }

    void CheckInitialize()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].receiveContent[0].gameObject.SetActive(true);
            attendanceContentArray[i].receiveContent[0].Initialize(RewardType.Crystal, reward);
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

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, reward);

        playerDataBase.LevelUpEventCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("LevelUpEventCount", playerDataBase.LevelUpEventCount);

        action.Invoke();

        CheckLevelUpEvent();

        OffAlarm();

        FirebaseAnalytics.LogEvent("Clear_Event_LevelUp");

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
