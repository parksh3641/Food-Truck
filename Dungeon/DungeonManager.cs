using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    public GameObject dungeonView;
    public GameObject dungeonInfoView;

    public Text timerText;

    public RectTransform rectTransform;

    public GameObject alarm;
    public GameObject ingameAlarm;

    public DungeonContent[] dungeonContents;

    bool isTimer = false;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);


    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        dungeonView.SetActive(false);
        dungeonInfoView.SetActive(false);

        alarm.SetActive(true);
        ingameAlarm.SetActive(true);

        rectTransform.anchoredPosition = new Vector2(0, -9999);
    }

    private void Start()
    {
        isTimer = true;
        timerText.text = "";
        StartCoroutine(TimerCoroution());
    }

    public void OpenDungeonView()
    {
        if(!dungeonView.activeInHierarchy)
        {
            dungeonView.SetActive(true);

            alarm.SetActive(false);
            ingameAlarm.SetActive(false);

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            if (!isTimer)
            {
                isTimer = true;
                StartCoroutine(TimerCoroution());
            }

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            Initialize();
        }
        else
        {
            dungeonView.SetActive(false);

        }
    }

    void Initialize()
    {
        dungeonContents[0].Initialize(DungeonType.Dungeon1, RewardType.Gold, RewardType.EquipExp, ItemType.DungeonKey1);
        dungeonContents[1].Initialize(DungeonType.Dungeon2, RewardType.Crystal, RewardType.EquipExp, ItemType.DungeonKey2);
        dungeonContents[2].Initialize(DungeonType.Dungeon3, RewardType.BuffTicket, RewardType.EquipExp, ItemType.DungeonKey3);
        dungeonContents[3].Initialize(DungeonType.Dungeon4, RewardType.SkillTicket, RewardType.EquipExp, ItemType.DungeonKey4);
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
