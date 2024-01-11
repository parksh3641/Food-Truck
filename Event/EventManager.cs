using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject eventView;

    public RectTransform eventTransform;

    public GameObject welcomeEvent;
    public GameObject weekendEvent;
    public LocalizationContent weekendEventTitle;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        eventView.SetActive(false);
    }

    private void Start()
    {
        eventTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenEventView()
    {
        if (!eventView.activeInHierarchy)
        {
            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            eventView.SetActive(true);

            if(playerDataBase.WelcomeCount > 6)
            {
                welcomeEvent.SetActive(false);
            }

            weekendEvent.SetActive(true);

            if (IsWeekend())
            {
                weekendEventTitle.localizationName = "Event5Title_Now";
            }
            else
            {
                weekendEventTitle.localizationName = "Event5Title_Before";
            }

            weekendEventTitle.ReLoad();

            FirebaseAnalytics.LogEvent("OpenEvent");
        }
        else
        {
            eventView.SetActive(false);
        }
    }

    bool IsWeekend()
    {
        DayOfWeek currentDay = DateTime.Now.DayOfWeek;

        return currentDay == DayOfWeek.Saturday || currentDay == DayOfWeek.Sunday;
    }
}
