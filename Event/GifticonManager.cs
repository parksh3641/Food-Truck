using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifticonManager : MonoBehaviour
{
    public GameObject gifticonView;

    public GameObject gifticonInfoView;

    public GameObject mainAlarm;
    public GameObject alarm;

    public RectTransform gifticonGrid;

    public GifticonContent[] gifticonContents;

    public Text timerText;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    private bool first = false;

    public Text ticketText;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    DateTime f, g;
    TimeSpan h;

    DateTime currentDate = DateTime.Now;
    public DateTime targetDate;

    public EventManager eventManager;

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        gifticonView.SetActive(false);
        gifticonInfoView.SetActive(false);

        gifticonGrid.anchoredPosition = new Vector2(0, -9999);

        mainAlarm.SetActive(true);
        alarm.SetActive(true);
    }

    public void OpenGifticonView()
    {
        if (!gifticonView.activeInHierarchy)
        {
            gifticonView.SetActive(true);

            Initialize();

            if (!first)
            {
                first = true;

                localization_Reset = LocalizationManager.instance.GetString("Reset");
                localization_Days = LocalizationManager.instance.GetString("Days");
                localization_Hours = LocalizationManager.instance.GetString("Hours");
                localization_Minutes = LocalizationManager.instance.GetString("Minutes");

                timerText.text = "";
                PlayfabManager.instance.GetTitleInternalData("GifticonDate", CheckGifticonDate);
            }

            mainAlarm.SetActive(false);
            alarm.SetActive(false);
        }
        else
        {
            StopAllCoroutines();

            gifticonView.SetActive(false);
        }
    }

    void CheckGifticonDate(string date)
    {
        targetDate = DateTime.ParseExact(date, "yyyyMMdd", null);

        if (currentDate > targetDate)
        {
            timerText.text = LocalizationManager.instance.GetString("EndEvent");

            for(int i = 0; i < gifticonContents.Length; i ++)
            {
                gifticonContents[i].CheckingEvent();
            }

            //이벤트 종료
        }
        else
        {
            f = DateTime.Now;
            g = targetDate;
            StartCoroutine(TimerCoroution());
        }
    }

    public bool CheckDate()
    {
        bool check = true;

        if (currentDate > targetDate)
        {
            check = false;
        }

        return check;
    }

    IEnumerator TimerCoroution()
    {
        if (timerText.gameObject.activeInHierarchy)
        {
            h = g - f;

            timerText.text = localization_Reset + " : " + h.Days.ToString("D2") + localization_Days + " " + h.Hours.ToString("D2") + localization_Hours
            + " " + h.Minutes.ToString("D2") + localization_Minutes;
        }
        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
    }

    public void Initialize()
    {
        ticketText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket);

        for (int i = 0; i < gifticonContents.Length; i ++)
        {
            gifticonContents[i].Initialize(GifticonType.Gifticon_1 + i, this);
        }
    }

    public void OpenGifticonInfoView()
    {
        Application.OpenURL("https://sites.google.com/view/whilili-gifticon");

        //if (!gifticonInfoView.activeInHierarchy)
        //{
        //    gifticonInfoView.SetActive(true);
        //}
        //else
        //{
        //    gifticonInfoView.SetActive(false);
        //}
    }

    public void OpenEventTicketInfo()
    {
        ReceiveInfoManager.instance.OpenReceiveInfo(RewardType.EventTicket);
    }
}
