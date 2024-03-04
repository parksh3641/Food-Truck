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

    DateTime currentDate = DateTime.Now;
    public DateTime targetDate;

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

            gifticonEvent.SetActive(false);

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            welcomeEvent.SetActive(true);
            if (playerDataBase.WelcomeCount > 6)
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


            reviewEvent.SetActive(true);
            if (playerDataBase.ReviewNumber == 2)
            {
                reviewEvent.SetActive(false);
            }


            gifticonEventLocked.SetActive(true);
            if (playerDataBase.Level > 9)
            {
                gifticonEventLocked.SetActive(false);
            }

            PlayfabManager.instance.GetTitleInternalData("Gifticon", CheckGifticon);

            FirebaseAnalytics.LogEvent("OpenEvent");
        }
        else
        {
            eventView.SetActive(false);
        }
    }

    void CheckGifticon(bool check)
    {
        if (check)
        {
            PlayfabManager.instance.GetTitleInternalData("GifticonDate", CheckGifticonDate);
        }
    }

    void CheckGifticonDate(string date)
    {
        gifticonEvent.SetActive(true);

        targetDate = DateTime.ParseExact(date, "yyyyMMdd", null);

        if (currentDate > targetDate || GameStateManager.instance.Language != LanguageType.Korean)
        {
            gifticonEvent.SetActive(false);
        }

        eventTransform.anchoredPosition = new Vector2(0, -499);
    }

    bool IsWeekend()
    {
        DayOfWeek currentDay = DateTime.Now.DayOfWeek;

        return currentDay == DayOfWeek.Saturday || currentDay == DayOfWeek.Sunday;
    }
}
