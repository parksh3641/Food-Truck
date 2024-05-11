using Firebase.Analytics;
using PlayFab;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResetInfo
{
    public int dailyReward = 0;
    public int dailyReward_Portion = 0;
    public int dailyReward_DefTicket = 0;
    public int dailyAdsReward = 0;
    public int dailyAdsReward2 = 0;
    public int dailyCastleReward = 0;
    public int dailyQuestReward = 0;
    public int dailyTreasureReward = 0;
    public int dailyReward_Crystal = 0;
    public int dailyDungeonKey1 = 0;
    public int dailyDungeonKey2 = 0;
    public int dailyDungeonKey3 = 0;
    public int dailyDungeonKey4 = 0;
    public int dailyReset1 = 0;
    public int dailyReset2 = 0;
    public int dailyReset3 = 0;
    public int dailyReset4 = 0;
    public int dailyReset5 = 0;
    public int dailyReset6 = 0;
    public int dailyReset7 = 0;
    public int dailyReset8 = 0;
    public int dailyReset9 = 0;
    public int dailyReset10 = 0;
}


public class ResetManager : MonoBehaviour
{
    public static ResetManager instance;

    private ResetInfo resetInfo = new ResetInfo();

    DateTime nextMondey;

    public ShopManager shopManager;
    public AttendanceManager attendanceManager;
    public WelcomeManager welcomeManager;

    private Dictionary<string, string> playerData = new Dictionary<string, string>();

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.6f);

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        instance = this;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void Initialize()
    {
        OnCheckAttendanceDay();
    }

    public void OnCheckAttendanceDay()
    {
        PlayfabManager.instance.GetServerTime(SetModeContent);
    }

    private void SetModeContent(DateTime time)
    {
        if (playerDataBase.AttendanceDay.Length < 2)
        {
            Debug.Log("일일 미션 처음 초기화");

            ResetValue();

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey1", playerDataBase.DungeonKey1);
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey2", playerDataBase.DungeonKey2);
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey3", playerDataBase.DungeonKey3);
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey4", playerDataBase.DungeonKey4);

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("AccessDate", playerDataBase.AccessDate);
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceDay", int.Parse(playerDataBase.AttendanceDay));
        }
        else
        {
            if (ComparisonDate(playerDataBase.AttendanceDay, time))
            {
                Debug.Log("하루가 지났습니다");

                ResetValue();
                StartCoroutine(ResetCoroution());
            }
            else
            {
                Debug.Log("아직 하루가 안 지났습니다.");
            }
        }

        if (playerDataBase.NextMonday.Length < 2)
        {
            Debug.Log("주간 미션 처음 초기화");

            nextMondey = DateTime.Today.AddDays(((int)DayOfWeek.Monday - (int)DateTime.Today.DayOfWeek + 7) % 7);

            if (nextMondey == DateTime.Today)
            {
                nextMondey = nextMondey.AddDays(7);
            }

            playerDataBase.NextMonday = nextMondey.ToString("yyyyMMdd");
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextMonday", int.Parse(playerDataBase.NextMonday));
        }
        else
        {
            if (ComparisonDate(playerDataBase.NextMonday, time))
            {
                Debug.Log("월요일이 되었습니다");

                nextMondey = DateTime.Today.AddDays(((int)DayOfWeek.Monday - (int)DateTime.Today.DayOfWeek + 7) % 7);

                if (nextMondey == DateTime.Today)
                {
                    nextMondey = nextMondey.AddDays(7);
                }

                playerDataBase.NextMonday = nextMondey.ToString("yyyyMMdd");
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextMonday", int.Parse(playerDataBase.NextMonday));
            }
            else
            {
                Debug.Log("아직 다음주 월요일이 아닙니다");
            }
        }

        StateManager.instance.SuccessReset();
    }

    public bool ComparisonDate(string target, System.DateTime time)
    {
        System.DateTime server = time;
        System.DateTime system = System.DateTime.ParseExact(target, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

        bool c = false;

        if (server.Year > system.Year)
        {
            c = true;
        }
        else
        {
            if (server.Year == system.Year)
            {
                if (server.Month > system.Month)
                {
                    c = true;
                }
                else
                {
                    if (server.Month == system.Month)
                    {
                        if (server.Day >= system.Day)
                        {
                            c = true;
                        }
                        else
                        {
                            c = false;
                        }
                    }
                    else
                    {
                        c = false;
                    }
                }
            }
            else
            {
                c = false;
            }
        }

        return c;
    }

    void ResetValue()
    {
        playerDataBase.AttendanceDay = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
        playerDataBase.AccessDate += 1;

        FirebaseAnalytics.LogEvent("AccessDate : " + playerDataBase.AccessDate);

        if (playerDataBase.AttendanceCheck)
        {
            playerDataBase.AttendanceCheck = false;

            if (playerDataBase.AttendanceCount >= 7)
            {
                playerDataBase.AttendanceCount = 0;

                Debug.Log("출석 체크 보상 리셋");
            }
            else
            {
                Debug.Log("다음 출석 체크 보상 오픈");
            }

            shopManager.SetAlarm();
            attendanceManager.SetAlarm();
        }

        if(playerDataBase.WelcomeCheck)
        {
            playerDataBase.WelcomeCheck = false;

            if (playerDataBase.WelcomeCount < 7)
            {
                welcomeManager.SetAlarm();
            }
        }

        playerDataBase.resetInfo = new ResetInfo();

        playerDataBase.PlayTimeCount = 0;
        playerDataBase.DungeonKey1 = 2;
        playerDataBase.DungeonKey2 = 2;
        playerDataBase.DungeonKey3 = 2;
        playerDataBase.DungeonKey4 = 2;

        GameStateManager.instance.UpgradeCount = 0;
        GameStateManager.instance.SellCount = 0;
        GameStateManager.instance.UseSauce = 0;
        GameStateManager.instance.OpenChestBox = 0;
        GameStateManager.instance.YummyTimeCount = 0;
        GameStateManager.instance.ChestBoxCount = 0;
        GameStateManager.instance.PlayTime = 0;
        GameStateManager.instance.HideNotice = false;
        GameStateManager.instance.YesterdayGold = GameStateManager.instance.TodayGold;
        GameStateManager.instance.TodayGold = 0;
        GameStateManager.instance.TodayQuiz = false;

        GameStateManager.instance.Portion1Ad = false;
        GameStateManager.instance.Portion2Ad = false;
        GameStateManager.instance.Portion3Ad = false;
        GameStateManager.instance.Portion4Ad = false;
        GameStateManager.instance.Portion5Ad = false;
    }

    IEnumerator ResetCoroution()
    {
        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AccessDate", playerDataBase.AccessDate);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceDay", int.Parse(playerDataBase.AttendanceDay));
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCheck", 0);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCount", 0);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey1", playerDataBase.DungeonKey1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey2", playerDataBase.DungeonKey2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey3", playerDataBase.DungeonKey3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey4", playerDataBase.DungeonKey4);

        yield return waitForSeconds;

        playerData.Clear();
        playerData.Add("ResetInfo", JsonUtility.ToJson(resetInfo));
        PlayfabManager.instance.SetPlayerData(playerData);
    }

    public void SetResetInfo(ResetType type)
    {
        switch (type)
        {
            case ResetType.DailyReward:
                playerDataBase.resetInfo.dailyReward = 1;
                break;
            case ResetType.DailyReward_Portion:
                playerDataBase.resetInfo.dailyReward_Portion = 1;
                break;
            case ResetType.DailyReward_DefTicket:
                playerDataBase.resetInfo.dailyReward_DefTicket = 1;
                break;
            case ResetType.DailyAdsReward:
                playerDataBase.resetInfo.dailyAdsReward = 1;
                break;
            case ResetType.DailyAdsReward2:
                playerDataBase.resetInfo.dailyAdsReward2 = 1;
                break;
            case ResetType.DailyCastleReward:
                playerDataBase.resetInfo.dailyCastleReward = 1;
                break;
            case ResetType.DailyQuestReward:
                playerDataBase.resetInfo.dailyQuestReward = 1;
                break;
            case ResetType.DailyTreasureReward:
                playerDataBase.resetInfo.dailyTreasureReward = 1;
                break;
            case ResetType.DailyReward_Crystal:
                playerDataBase.resetInfo.dailyReward_Crystal = 1;
                break;
            case ResetType.DailyDungeonKey1:
                playerDataBase.resetInfo.dailyDungeonKey1 = 1;
                break;
            case ResetType.DailyDungeonKey2:
                playerDataBase.resetInfo.dailyDungeonKey2 = 1;
                break;
            case ResetType.DailyDungeonKey3:
                playerDataBase.resetInfo.dailyDungeonKey3 = 1;
                break;
            case ResetType.DailyDungeonKey4:
                playerDataBase.resetInfo.dailyDungeonKey4 = 1;
                break;
            case ResetType.DailyReset1:
                playerDataBase.resetInfo.dailyReset1 = 1;
                break;
            case ResetType.DailyReset2:
                playerDataBase.resetInfo.dailyReset2 = 1;
                break;
            case ResetType.DailyReset3:
                playerDataBase.resetInfo.dailyReset3 = 1;
                break;
            case ResetType.DailyReset4:
                playerDataBase.resetInfo.dailyReset4 = 1;
                break;
            case ResetType.DailyReset5:
                playerDataBase.resetInfo.dailyReset5 = 1;
                break;
            case ResetType.DailyReset6:
                playerDataBase.resetInfo.dailyReset6 = 1;
                break;
            case ResetType.DailyReset7:
                playerDataBase.resetInfo.dailyReset7 = 1;
                break;
            case ResetType.DailyReset8:
                playerDataBase.resetInfo.dailyReset8 = 1;
                break;
            case ResetType.DailyReset9:
                playerDataBase.resetInfo.dailyReset9 = 1;
                break;
            case ResetType.DailyReset10:
                playerDataBase.resetInfo.dailyReset10 = 1;
                break;
        }

        playerData.Clear();
        playerData.Add("ResetInfo", JsonUtility.ToJson(playerDataBase.resetInfo));
        PlayfabManager.instance.SetPlayerData(playerData);
    }
}
