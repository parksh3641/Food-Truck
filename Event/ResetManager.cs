﻿using PlayFab;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResetManager : MonoBehaviour
{
    public static ResetManager instance;

    DateTime serverTime;
    DateTime nextMondey;

    public ShopManager shopManager;

    public AttendanceManager attendanceManager;
    //public EventManager eventManager;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        instance = this;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void Initialize()
    {
        //if (!playerDataBase.AttendanceCheck)
        //{
        //    attendanceManager.OnSetAlarm();
        //}

        OnCheckAttendanceDay();
    }

    public void OnCheckAttendanceDay()
    {
        if (PlayfabManager.instance.isActive) PlayfabManager.instance.GetServerTime(SetModeContent);
    }

    private void SetModeContent(DateTime time)
    {
        if (playerDataBase.AttendanceDay.Length < 2)
        {
            Debug.Log("데일리 미션 맨 처음 초기화");

            playerDataBase.AttendanceDay = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
            playerDataBase.AccessDate += 1;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("AccessDate", playerDataBase.AccessDate);
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceDay", int.Parse(playerDataBase.AttendanceDay));

            playerDataBase.DailyReward = 0;
            playerDataBase.DailyReward_Portion = 0;
            playerDataBase.DailyReward_DefTicket = 0;
            playerDataBase.DailyAdsReward = 0;
            playerDataBase.DailyAdsReward2 = 0;
            playerDataBase.DailyCastleReward = 0;
            playerDataBase.DailyQuestReward = 0;
            playerDataBase.DailyTreasureReward = 0;

            GameStateManager.instance.UpgradeCount = 0;
            GameStateManager.instance.SellCount = 0;
            GameStateManager.instance.UseSauce = 0;
            GameStateManager.instance.OpenChestBox = 0;
            GameStateManager.instance.YummyTimeCount = 0;

            GameStateManager.instance.ChestBoxCount = 0;

            if (playerDataBase.AttendanceCheck)
            {
                playerDataBase.AttendanceCheck = false;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCheck", 0);

                if (playerDataBase.AttendanceCount >= 7)
                {
                    playerDataBase.AttendanceCount = 0;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCount", 0);

                    Debug.Log("출석 체크 보상 리셋");
                }
                else
                {
                    Debug.Log("다음 출석 체크 보상 오픈");
                }

                shopManager.SetAlarm();

                attendanceManager.OnSetAlarm();
            }

            //if(playerDataBase.WelcomeCheck)
            //{
            //    playerDataBase.WelcomeCheck = false;
            //    PlayfabManager.instance.UpdatePlayerStatisticsInsert("WelcomeCheck", 0);

            //    eventManager.OnSetWelcomeAlarm();
            //}
        }
        else
        {
            if (ComparisonDate(playerDataBase.AttendanceDay, time))
            {
                Debug.Log("하루가 지났습니다");

                playerDataBase.AttendanceDay = System.DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                playerDataBase.AccessDate += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("AccessDate", playerDataBase.AccessDate);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceDay", int.Parse(playerDataBase.AttendanceDay));

                StartCoroutine(ResetCoroution());

                GameStateManager.instance.UpgradeCount = 0;
                GameStateManager.instance.SellCount = 0;
                GameStateManager.instance.UseSauce = 0;
                GameStateManager.instance.OpenChestBox = 0;
                GameStateManager.instance.YummyTimeCount = 0;

                GameStateManager.instance.ChestBoxCount = 0;

                if (playerDataBase.AttendanceCheck)
                {
                    playerDataBase.AttendanceCheck = false;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCheck", 0);

                    if(playerDataBase.AttendanceCount >= 7)
                    {
                        playerDataBase.AttendanceCount = 0;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AttendanceCount", 0);

                        Debug.Log("출석 체크 보상 리셋");
                    }
                    else
                    {
                        Debug.Log("다음 출석 체크 보상 오픈");
                    }

                    shopManager.SetAlarm();

                    attendanceManager.OnSetAlarm();
                }

                //if (playerDataBase.WelcomeCheck)
                //{
                //    playerDataBase.WelcomeCheck = false;
                //    PlayfabManager.instance.UpdatePlayerStatisticsInsert("WelcomeCheck", 0);

                //    eventManager.OnSetWelcomeAlarm();
                //}
            }
            else
            {
                Debug.Log("아직 하루가 안 지났습니다.");
            }
        }

        if (playerDataBase.NextMonday.Length < 2)
        {
            Debug.Log("위클리 미션 초기화");

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

    IEnumerator ResetCoroution()
    {
        playerDataBase.DailyReward = 0;
        playerDataBase.DailyReward_Portion = 0;
        playerDataBase.DailyReward_DefTicket = 0;
        playerDataBase.DailyAdsReward = 0;
        playerDataBase.DailyAdsReward2 = 0;
        playerDataBase.DailyCastleReward = 0;
        playerDataBase.DailyQuestReward = 0;
        playerDataBase.DailyTreasureReward = 0;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyReward", playerDataBase.DailyReward);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyReward_Portion", playerDataBase.DailyReward_Portion);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyReward_DefTicket", playerDataBase.DailyReward_DefTicket);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyAdsReward", playerDataBase.DailyAdsReward);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyAdsReward2", playerDataBase.DailyAdsReward2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyCastleReward", playerDataBase.DailyCastleReward);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyQuestReward", playerDataBase.DailyQuestReward);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyTreasureReward", playerDataBase.DailyTreasureReward);
    }
}
