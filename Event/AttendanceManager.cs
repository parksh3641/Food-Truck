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

            FirebaseAnalytics.LogEvent("OpenAttendance");
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
            attendanceContentArray[i].InitializeAttendance(i, playerDataBase.AttendanceCount, playerDataBase.AttendanceCheck, this);
        }
    }

    public void CheckInitialize()
    {
        attendanceContentArray[0].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[0].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[0].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[0].receiveContent[1].Initialize(RewardType.Portion1, 3);

        attendanceContentArray[1].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[1].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[1].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[1].receiveContent[1].Initialize(RewardType.Portion2, 3);

        attendanceContentArray[2].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[2].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[2].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[2].receiveContent[1].Initialize(RewardType.Portion3, 3);

        attendanceContentArray[3].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[3].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[3].receiveContent[0].Initialize(RewardType.Gold, 250000);
        attendanceContentArray[3].receiveContent[1].Initialize(RewardType.Crystal, 100);

        attendanceContentArray[4].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[4].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[4].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[4].receiveContent[1].Initialize(RewardType.Portion4, 3);

        attendanceContentArray[5].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[5].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[5].receiveContent[0].Initialize(RewardType.Gold, 100000);
        attendanceContentArray[5].receiveContent[1].Initialize(RewardType.Portion5, 3);

        attendanceContentArray[6].receiveContent[0].gameObject.SetActive(true);
        attendanceContentArray[6].receiveContent[1].gameObject.SetActive(true);
        attendanceContentArray[6].receiveContent[2].gameObject.SetActive(true);
        attendanceContentArray[6].receiveContent[0].Initialize(RewardType.Gold, 500000);
        attendanceContentArray[6].receiveContent[1].Initialize(RewardType.Crystal, 300);
        attendanceContentArray[6].receiveContent[2].Initialize(RewardType.DefDestroyTicket, 3);
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
                PortionManager.instance.GetPortion(0, 3);

                break;
            case 1:
                PlayfabManager.instance.UpdateAddGold(100000);
                PortionManager.instance.GetPortion(1, 3);

                break;
            case 2:
                PlayfabManager.instance.UpdateAddGold(100000);
                PortionManager.instance.GetPortion(2, 3);

                break;
            case 3:
                PlayfabManager.instance.UpdateAddGold(250000);
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);

                break;
            case 4:
                PlayfabManager.instance.UpdateAddGold(100000);
                PortionManager.instance.GetPortion(3, 3);

                break;
            case 5:
                PlayfabManager.instance.UpdateAddGold(100000);
                PortionManager.instance.GetPortion(4, 3);

                break;
            case 6:
                PlayfabManager.instance.UpdateAddGold(500000);
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);
                PortionManager.instance.GetDefTickets(3);

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
}
