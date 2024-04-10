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

    public AttendanceContent[] attendanceContentArray;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    DateTime f, g;
    TimeSpan h;

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


    [Button]
    void OpenTreasureBox()
    {
        TreasureManager.instance.OpenTreasure(1);
    }

    public void Initialize()
    {
        if(!playerDataBase.AttendanceCheck)
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

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            timerText.text = "";
            f = DateTime.Now;
            g = DateTime.Today.AddDays(1);
            StartCoroutine(TimerCoroution());

            CheckAttendance();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("Open_Event_Attendance");
        }
        else
        {
            StopAllCoroutines();

            attendanceView.SetActive(false);
        }
    }

    public void CheckAttendance()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].InitializeAttendance(i, playerDataBase.AttendanceCount, playerDataBase.AttendanceCheck, this);
        }
    }

    public void CheckInitialize()
    {
        attendanceContentArray[0].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[0].receiveContent[0].Initialize(RewardType.Crystal, 50);

        attendanceContentArray[1].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[1].receiveContent[0].Initialize(RewardType.Crystal, 100);

        attendanceContentArray[2].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[2].receiveContent[0].Initialize(RewardType.Crystal, 150);

        attendanceContentArray[3].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[3].receiveContent[0].Initialize(RewardType.Crystal, 200);

        attendanceContentArray[4].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[4].receiveContent[0].Initialize(RewardType.Crystal, 250);

        attendanceContentArray[5].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[5].receiveContent[0].Initialize(RewardType.Crystal, 300);

        attendanceContentArray[6].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[6].receiveContent[0].Initialize(RewardType.Crystal, 350);
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
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 50);

                break;
            case 1:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);

                break;
            case 2:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 150);

                break;
            case 3:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 200);

                break;
            case 4:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 250);

                break;
            case 5:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);

                break;
            case 6:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 350);

                break;
        }

        playerDataBase.AttendanceCount += 1;
        playerDataBase.AttendanceCheck = true;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCount", playerDataBase.AttendanceCount);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCheck", 1);

        FirebaseAnalytics.LogEvent("Clear_Event_Attendance : " + playerDataBase.AttendanceCount);

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
