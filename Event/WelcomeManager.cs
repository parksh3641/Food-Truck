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

    public Text timerText;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    bool isTimer = false;

    public GameObject mainAlarm;
    public GameObject alarm;

    public AttendanceContent[] attendanceContentArray;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

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
    }

    private void Start()
    {
        isTimer = true;
        timerText.text = "";
        StartCoroutine(TimerCoroution());
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

            if (!isTimer)
            {
                isTimer = true;
                StartCoroutine(TimerCoroution());
            }

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            CheckWelcome();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("OpenWelcome");
        }
        else
        {
            welcomeView.SetActive(false);
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
            attendanceContentArray[i].receiveContent[0].Initialize(RewardType.Crystal, 200 + (100 * (i + 1)));
            attendanceContentArray[i].receiveContent[1].Initialize(RewardType.RepairTicket, 5);
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

        PortionManager.instance.GetRecoverTickets(5);

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 200 + (100 * (index + 1)));

        playerDataBase.WelcomeCount += 1;
        playerDataBase.WelcomeCheck = true;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("WelcomeCount", playerDataBase.WelcomeCount);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("WelcomeCheck", 1);

        action.Invoke();

        CheckWelcome();

        OffAlarm();

        FirebaseAnalytics.LogEvent("WelcomeClear");

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
}
