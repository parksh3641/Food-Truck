using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeEventManager : MonoBehaviour
{
    public GameObject recipeEventView;

    public Text recipeEventText;

    public RectTransform recipeEventRectTransform;

    public GameObject mainAlarm;
    public GameObject alarm;

    public AttendanceContent[] attendanceContentArray;

    private int level = 50;
    private int reward = 3;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        recipeEventView.SetActive(false);

        recipeEventRectTransform.anchoredPosition = new Vector2(0, -9999);

        mainAlarm.SetActive(false);
        alarm.SetActive(false);
    }

    public void Initialize()
    {
        mainAlarm.SetActive(true);
        alarm.SetActive(true);
    }

    public void OpenRecipeEventView()
    {
        if (!recipeEventView.activeInHierarchy)
        {
            recipeEventView.SetActive(true);

            alarm.SetActive(false);
            mainAlarm.SetActive(false);

            recipeEventText.text = LocalizationManager.instance.GetString("RecipeEvent") + " : " + playerDataBase.GetRecipeUpgradeCount();

            CheckRecipeEvent();

            CheckInitialize();

            FirebaseAnalytics.LogEvent("Open_Event_Recipe");
        }
        else
        {
            recipeEventView.SetActive(false);
        }
    }

    void CheckRecipeEvent()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].InitializeRecipeEvent(i, level * (i + 1), playerDataBase.RecipeEventCount, playerDataBase.GetRecipeUpgradeCount(), this);
        }
    }

    void CheckInitialize()
    {
        for (int i = 0; i < attendanceContentArray.Length; i++)
        {
            attendanceContentArray[i].receiveContent[0].gameObject.SetActive(true);
            attendanceContentArray[i].receiveContent[0].Initialize(RewardType.SkillTicket, reward);
        }
    }

    public void ReceiveButton(Action action)
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        PortionManager.instance.GetSkillTickets(reward);

        playerDataBase.RecipeEventCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RecipeEventCount", playerDataBase.RecipeEventCount);

        action.Invoke();

        CheckRecipeEvent();

        OffAlarm();

        FirebaseAnalytics.LogEvent("Clear_Event_Recipe");

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
}
