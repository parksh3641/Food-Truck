using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeManager : MonoBehaviour
{
    public GameObject welcomeView;

    public RectTransform welcomeTransform;

    public GameObject closeButton;

    public Text timerText;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    public bool first = false;

    public GameObject mainAlarm;
    public GameObject alarm;

    public AttendanceContent[] attendanceContentArray;

    public AttendanceManager attendanceManager;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    DateTime f, g;
    TimeSpan h;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        welcomeView.SetActive(false);
        alarm.SetActive(false);

        welcomeTransform.anchoredPosition = new Vector2(0, -9999);

        timerText.text = "";

        mainAlarm.SetActive(false);
        alarm.SetActive(false);

        first = false;
    }

    public void Initialize()
    {
        if (!playerDataBase.WelcomeCheck)
        {
            if (playerDataBase.WelcomeCount < 7)
            {
                SetAlarm();
            }
        }
    }

    public void OpenWelcomeView()
    {
        if (!welcomeView.activeInHierarchy)
        {
            welcomeView.SetActive(true);

            closeButton.SetActive(true);
            if (first)
            {
                closeButton.SetActive(false);
            }

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

            CheckWelcome();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("Open_Event_Welcome");
        }
        else
        {
            StopAllCoroutines();

            welcomeView.SetActive(false);

            if(first)
            {
                first = false;

                if (!playerDataBase.AttendanceCheck)
                {
                    attendanceManager.first = true;
                    attendanceManager.OpenAttendanceView();
                }
            }
        }
    }

    void CheckWelcome()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].InitializeWelcome(i, playerDataBase.WelcomeCount, playerDataBase.WelcomeCheck, this);
        }
    }

    void CheckInitialize()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].receiveContent[0].gameObject.SetActive(true);
            attendanceContentArray[i].receiveContent[1].gameObject.SetActive(true);
            attendanceContentArray[i].receiveContent[2].gameObject.SetActive(true);

            switch(i)
            {
                case 0:
                    attendanceContentArray[i].receiveContent[0].Initialize(RewardType.Crystal, 300);
                    attendanceContentArray[i].receiveContent[1].Initialize(RewardType.EventTicket, 100);
                    attendanceContentArray[i].receiveContent[2].gameObject.SetActive(false);
                    break;
                case 1:
                    attendanceContentArray[i].receiveContent[0].Initialize(RewardType.Crystal, 300);
                    attendanceContentArray[i].receiveContent[1].Initialize(RewardType.EventTicket, 100);
                    attendanceContentArray[i].receiveContent[2].gameObject.SetActive(false);
                    break;
                case 2:
                    attendanceContentArray[i].receiveContent[0].Initialize(RewardType.TreasureBox, 5);
                    attendanceContentArray[i].receiveContent[1].Initialize(RewardType.EventTicket, 100);
                    attendanceContentArray[i].receiveContent[2].gameObject.SetActive(false);
                    break;
                case 3:
                    attendanceContentArray[i].receiveContent[0].Initialize(RewardType.Crystal, 500);
                    attendanceContentArray[i].receiveContent[1].Initialize(RewardType.EventTicket, 100);
                    attendanceContentArray[i].receiveContent[2].gameObject.SetActive(false);
                    break;
                case 4:
                    attendanceContentArray[i].receiveContent[0].Initialize(RewardType.Crystal, 500);
                    attendanceContentArray[i].receiveContent[1].Initialize(RewardType.EventTicket, 100);
                    attendanceContentArray[i].receiveContent[2].gameObject.SetActive(false);
                    break;
                case 5:
                    attendanceContentArray[i].receiveContent[0].Initialize(RewardType.TreasureBox, 10);
                    attendanceContentArray[i].receiveContent[1].Initialize(RewardType.EventTicket, 100);
                    attendanceContentArray[i].receiveContent[2].gameObject.SetActive(false);
                    break;
                case 6:
                    attendanceContentArray[i].receiveContent[0].Initialize(RewardType.Crystal, 1000);
                    attendanceContentArray[i].receiveContent[1].Initialize(RewardType.EventTicket, 100);
                    attendanceContentArray[i].receiveContent[2].Initialize(RewardType.Icon_Attendance, 1);
                    break;
            }
        }
    }

    public void ReceiveButton(int index, Action action)
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        switch(index)
        {
            case 0:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);

                break;
            case 1:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);

                break;
            case 2:
                TreasureManager.instance.OpenTreasure(5);

                break;
            case 3:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 500);

                break;
            case 4:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 500);

                break;
            case 5:
                TreasureManager.instance.OpenTreasure(10);

                break;
            case 6:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1000);

                if (!playerDataBase.CheckIcon(IconType.Icon_16))
                {
                    playerDataBase.SetIcon(IconType.Icon_16, 1);
                    PlayfabManager.instance.GrantItemsToUser((IconType.Icon_16).ToString(), "Icon");
                }
                break;
        }

        PortionManager.instance.GetEventTicket(100);

        playerDataBase.WelcomeCount += 1;
        playerDataBase.WelcomeCheck = true;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("WelcomeCount", playerDataBase.WelcomeCount);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("WelcomeCheck", 1);

        FirebaseAnalytics.LogEvent("Clear_Event_Welcome : " + playerDataBase.WelcomeCount);

        action.Invoke();

        CheckWelcome();

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
}
