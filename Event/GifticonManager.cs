using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifticonManager : MonoBehaviour
{
    public GameObject gifticonView;

    public RectTransform gifticonGrid;

    public GifticonContent[] gifticonContents;

    public Text timerText;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    DateTime f, g;
    TimeSpan h;

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        gifticonView.SetActive(false);

        gifticonGrid.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenGifticonView()
    {
        if (!gifticonView.activeInHierarchy)
        {
            gifticonView.SetActive(true);

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            timerText.text = "";
            f = DateTime.Now;
            g = new DateTime(2024, 3, 31);
            StartCoroutine(TimerCoroution());
        }
        else
        {
            StopAllCoroutines();

            gifticonView.SetActive(false);
        }
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
}
