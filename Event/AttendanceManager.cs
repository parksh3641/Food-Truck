using Firebase.Analytics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AttendanceManager : MonoBehaviour
{
    public GameObject attendanceView;

    public RectTransform attendanceRectTransform;

    public GameObject mainAlarm;
    public GameObject alarm;

    public Text timerText;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    bool isTimer = false;

    public AttendanceContent[] attendanceContentArray;

    public TreasureManager treasureManager;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        attendanceView.SetActive(false);
        alarm.SetActive(false);

        attendanceRectTransform.anchoredPosition = new Vector2(0, -9999);

        timerText.text = "";

        mainAlarm.SetActive(false);
        alarm.SetActive(false);
    }

    private void Start()
    {
        isTimer = true;
        timerText.text = "";
        StartCoroutine(TimerCoroution());
    }

    [Button]
    void OpenTreasureBox()
    {
        treasureManager.OpenTreasure(1);
    }

    public void Initialize()
    {
        if(!playerDataBase.attendanceCheck)
        {
            SetAlarm();
        }
    }

    [Button]
    public void NextDay()
    {
        playerDataBase.AttendanceCheck = false;

        if (playerDataBase.AttendanceCount >= 7)
        {
            playerDataBase.AttendanceCount = 0;
        }
        
        CheckAttendance();
    }

    public void OpenAttendanceView()
    {
        if(!attendanceView.activeInHierarchy)
        {
            attendanceView.SetActive(true);

            if (!isTimer)
            {
                isTimer = true;
                StartCoroutine(TimerCoroution());
            }

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            CheckAttendance();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("Attendance");
        }
        else
        {
            attendanceView.SetActive(false);
        }
    }

    public void CheckAttendance()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].InitializeAttendance(playerDataBase.AttendanceCount, playerDataBase.AttendanceCheck, this);
        }
    }

    public void CheckInitialize()
    {
        attendanceContentArray[0].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[0].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[0].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[0].receiveContent[1].Initialize(RewardType.Portion1, 2);

        attendanceContentArray[1].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[1].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[1].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[1].receiveContent[1].Initialize(RewardType.Portion2, 2);

        attendanceContentArray[2].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[2].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[2].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[2].receiveContent[1].Initialize(RewardType.Portion3, 2);

        attendanceContentArray[3].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[3].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[3].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[3].receiveContent[1].Initialize(RewardType.TreasureBox, 1);

        attendanceContentArray[4].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[4].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[4].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[4].receiveContent[1].Initialize(RewardType.Portion4, 2);

        attendanceContentArray[5].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[5].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[5].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[5].receiveContent[1].Initialize(RewardType.Portion5, 2);

        attendanceContentArray[6].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[6].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[6].receiveContent[2].gameObject.SetActive(true);
        attendanceContentArray[6].receiveContent[0].Initialize(RewardType.Gold, 500000);
        attendanceContentArray[6].receiveContent[1].Initialize(RewardType.TreasureBox, 3);
        attendanceContentArray[6].receiveContent[2].Initialize(RewardType.DefDestroyTicket, 1);
    }

    public void ReceiveButton(int index, Action action)
    {
        if (playerDataBase.AttendanceCheck) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        switch (index)
        {
            case 0:
                PlayfabManager.instance.UpdateAddGold(100000);
                GetRandomPortion(0, 2);

                break;
            case 1:
                PlayfabManager.instance.UpdateAddGold(100000);
                GetRandomPortion(1, 2);

                break;
            case 2:
                PlayfabManager.instance.UpdateAddGold(100000);
                GetRandomPortion(2, 2);

                break;
            case 3:
                PlayfabManager.instance.UpdateAddGold(100000);
                treasureManager.OpenTreasure(1);

                break;
            case 4:
                PlayfabManager.instance.UpdateAddGold(100000);
                GetRandomPortion(3, 2);

                break;
            case 5:
                GetRandomPortion(4, 2);

                break;
            case 6:
                PlayfabManager.instance.UpdateAddGold(300000);
                treasureManager.OpenTreasure(3);

                playerDataBase.DefDestroyTicket += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicket", playerDataBase.DefDestroyTicket);
                break;
        }

        playerDataBase.AttendanceCount += 1;
        playerDataBase.AttendanceCheck = true;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCount", playerDataBase.AttendanceCount);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCheck", 1);

        action.Invoke();

        CheckAttendance();

        OffAlarm();

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }

    IEnumerator TimerCoroution()
    {
        if (timerText.gameObject.activeInHierarchy)
        {
            System.DateTime f = System.DateTime.Now;
            System.DateTime g = System.DateTime.Today.AddDays(1);
            System.TimeSpan h = g - f;

            timerText.text = localization_Reset + " : " + h.Hours.ToString("D2") + localization_Hours + " " + h.Minutes.ToString("D2") + localization_Minutes;

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                isTimer = false;
                ResetManager.instance.Initialize();
                yield break;
            }

        }
        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
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

    void GetRandomPortion(int index, int number)
    {
        switch (index)
        {
            case 0:
                playerDataBase.Portion1 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
                break;
            case 1:
                playerDataBase.Portion2 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
                break;
            case 2:
                playerDataBase.Portion3 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
                break;
            case 3:
                playerDataBase.Portion4 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
                break;
            case 4:
                playerDataBase.Portion5 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
                break;
        }
    }

    void GetAllPortion(int number)
    {
        playerDataBase.Portion1 += number;
        playerDataBase.Portion2 += number;
        playerDataBase.Portion3 += number;
        playerDataBase.Portion4 += number;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
    }
}
