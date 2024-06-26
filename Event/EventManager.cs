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
    public GameObject reviewEvent;

    public GameObject gifticonEvent;
    public GameObject gifticonEventLocked;

    public LocalizationContent weekendEventTitle;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        eventView.SetActive(false);

        eventTransform.anchoredPosition = new Vector2(0, -999);
    }

    public void OpenEventView()
    {
        if (!eventView.activeInHierarchy)
        {
            eventView.SetActive(true);

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            welcomeEvent.SetActive(false);
            if (playerDataBase.WelcomeCount < 7)
            {
                welcomeEvent.SetActive(true);
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


            reviewEvent.SetActive(true);
            if (playerDataBase.ReviewNumber == 2 || GameStateManager.instance.StoreType == StoreType.OneStore)
            {
                reviewEvent.SetActive(false);
            }

            gifticonEventLocked.SetActive(false);

#if UNITY_EDITOR || UNITY_EDITOR_OSX
            CheckGifticon(true);
#else
            PlayfabManager.instance.GetTitleInternalData("Gifticon", CheckGifticon);
#endif

            FirebaseAnalytics.LogEvent("Open_Event");
        }
        else
        {
            eventView.SetActive(false);
        }
    }

    void CheckGifticon(bool check)
    {
        if (check && GameStateManager.instance.Region == "ko" && GameStateManager.instance.StoreType != StoreType.OneStore)
        {
            gifticonEvent.SetActive(true);
            eventTransform.anchoredPosition = new Vector2(0, -499);
        }
    }

    bool IsWeekend()
    {
        DayOfWeek currentDay = DateTime.Now.DayOfWeek;

        return currentDay == DayOfWeek.Saturday || currentDay == DayOfWeek.Sunday;
    }
}
