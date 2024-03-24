using Firebase.Analytics;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour
{
    public static SeasonManager instance;

    public GameObject seasonRewardView;

    public LocalizationContent seasonText;
    public Text seasonRewardText;

    public ReceiveContent[] receiveContents;

    private int season = 0;
    private int myRank = 0;
    private int reward = 0;

    private bool isDelay = false;

    DateTime currentDate;
    DateTime serverDate;
    DateTime season1Date = new DateTime(2024, 1, 31, 23, 59, 59);

    DateTime season2Date_Start = new DateTime(2024, 2, 2);
    DateTime season2Date_End = new DateTime(2024, 2, 14, 23, 59, 59);


    DateTime season3Date_Start = new DateTime(2024, 2, 16);
    DateTime season3Date_End = new DateTime(2024, 2, 29, 23, 59, 59);


    DateTime season4Date_Start = new DateTime(2024, 3, 2);
    DateTime season4Date_End = new DateTime(2024, 3, 16, 23, 59, 59);


    DateTime season5Date_Start = new DateTime(2024, 3, 18);
    DateTime season5Date_End = new DateTime(2024, 3, 31, 23, 59, 59);


    DateTime season6Date_Start = new DateTime(2024, 4, 2);
    DateTime season6Date_End = new DateTime(2024, 4, 15, 23, 59, 59);


    DateTime season7Date_Start = new DateTime(2024, 4, 17);
    DateTime season7Date_End = new DateTime(2024, 4, 30, 23, 59, 59);


    DateTime season8Date_Start = new DateTime(2024, 5, 2);
    DateTime season8Date_End = new DateTime(2024, 5, 16, 23, 59, 59);


    DateTime season9Date_Start = new DateTime(2024, 5, 18);
    DateTime season9Date_End = new DateTime(2024, 5, 31, 23, 59, 59);


    DateTime season10Date_Start = new DateTime(2024, 6, 2);
    DateTime season10Date_End = new DateTime(2024, 6, 15, 23, 59, 59);


    DateTime season11Date_Start = new DateTime(2024, 6, 17);
    DateTime season11Date_End = new DateTime(2024, 6, 30, 23, 59, 59);


    WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        instance = this;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        seasonRewardView.SetActive(false);
    }

    public void Initialize()
    {
        PlayfabManager.instance.GetServerTime(CheckSeason);
    }

    private void CheckSeason(DateTime time)
    {
        currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        serverDate = new DateTime(time.Year, time.Month, time.Day);

        if (DateTime.Compare(currentDate, serverDate) == 0 || DateTime.Compare(currentDate.AddDays(1), serverDate) == 0 
            || DateTime.Compare(currentDate.AddDays(-1), serverDate) == 0)
        {
            Debug.Log("서버와 날짜가 같습니다.");

            isDelay = true;

            if (season == 1)
            {
                PlayfabManager.instance.GetLeaderboarder(RankingType.TotalLevel.ToString(), 0, SetRankingReward);
            }
            else
            {
                PlayfabManager.instance.GetLeaderboarder(RankingType.TotalLevel.ToString() + "_" + (season - 1), 0, SetRankingReward);
            }

        }
        else
        {
            Debug.Log("서버와 날짜가 다릅니다.");

#if UNITY_EDITOR
            isDelay = true;

            if (season == 1)
            {
                PlayfabManager.instance.GetLeaderboarder(RankingType.TotalLevel.ToString(), 0, SetRankingReward);
            }
            else
            {
                PlayfabManager.instance.GetLeaderboarder(RankingType.TotalLevel.ToString() + "_" + (season - 1), 0, SetRankingReward);
            }
#endif
        }
    }

    public void SetReward(int number)
    {
        seasonRewardView.SetActive(true);

        SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);

        reward = number;

        receiveContents[0].gameObject.SetActive(true);
        receiveContents[1].gameObject.SetActive(true);
        receiveContents[2].gameObject.SetActive(false);

        switch (number)
        {
            case 0:
                receiveContents[2].gameObject.SetActive(true);

                receiveContents[0].Initialize(RewardType.Crystal, 10000);
                receiveContents[1].Initialize(RewardType.RankPoint, 5000);
                receiveContents[2].Initialize(RewardType.Icon_Ranking1, 1);

                break;
            case 1:
                receiveContents[2].gameObject.SetActive(true);

                receiveContents[0].Initialize(RewardType.Crystal, 8000);
                receiveContents[1].Initialize(RewardType.RankPoint, 4000);
                receiveContents[2].Initialize(RewardType.Icon_Ranking2, 1);

                break;
            case 2:
                receiveContents[2].gameObject.SetActive(true);

                receiveContents[0].Initialize(RewardType.Crystal, 6000);
                receiveContents[1].Initialize(RewardType.RankPoint, 3000);
                receiveContents[2].Initialize(RewardType.Icon_Ranking3, 1);

                break;
            case 3:
                receiveContents[2].gameObject.SetActive(true);

                receiveContents[0].Initialize(RewardType.Crystal, 4000);
                receiveContents[1].Initialize(RewardType.RankPoint, 2000);
                receiveContents[2].Initialize(RewardType.Icon_Ranking4, 1);

                break;
            case 4:
                receiveContents[0].Initialize(RewardType.Crystal, 2000);
                receiveContents[1].Initialize(RewardType.RankPoint, 1500);

                break;
            case 5:
                receiveContents[0].Initialize(RewardType.Crystal, 1000);
                receiveContents[1].Initialize(RewardType.RankPoint, 1000);

                break;
            case 6:
                receiveContents[0].Initialize(RewardType.Crystal, 300);
                receiveContents[1].Initialize(RewardType.RankPoint, 500);

                break;
            case 7:
                receiveContents[0].Initialize(RewardType.Crystal, 0);
                receiveContents[1].Initialize(RewardType.RankPoint, 0);
                break;
        }

        if(number != 7)
        {
            seasonRewardText.text = LocalizationManager.instance.GetString("SeasonRewardInfo") + "\n" + LocalizationManager.instance.GetString("BestRank")
+ " : " + myRank.ToString();
        }
        else
        {
            seasonRewardText.text = LocalizationManager.instance.GetString("SeasonRewardInfo");
        }
    }

    public void ReceiveButton()
    {
        if (isDelay) return;

        seasonRewardView.SetActive(false);

        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        StartCoroutine(ReceiveCoroution());
    }

    IEnumerator ReceiveCoroution()
    {
        switch (reward)
        {
            case 0:
                PortionManager.instance.GetRankPoint(5000);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 10000);

                if (!playerDataBase.CheckIcon(IconType.Icon_39))
                {
                    playerDataBase.SetIcon(IconType.Icon_39, 1);
                    PlayfabManager.instance.GrantItemsToUser((IconType.Icon_39).ToString(), "Icon");
                }
                break;
            case 1:
                PortionManager.instance.GetRankPoint(4000);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 8000);

                if (!playerDataBase.CheckIcon(IconType.Icon_40))
                {
                    playerDataBase.SetIcon(IconType.Icon_40, 1);
                    PlayfabManager.instance.GrantItemsToUser((IconType.Icon_40).ToString(), "Icon");
                }
                break;
            case 2:
                PortionManager.instance.GetRankPoint(3000);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 6000);

                if (!playerDataBase.CheckIcon(IconType.Icon_41))
                {
                    playerDataBase.SetIcon(IconType.Icon_41, 1);
                    PlayfabManager.instance.GrantItemsToUser((IconType.Icon_41).ToString(), "Icon");
                }
                break;
            case 3:
                PortionManager.instance.GetRankPoint(2000);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 4000);

                if (!playerDataBase.CheckIcon(IconType.Icon_42))
                {
                    playerDataBase.SetIcon(IconType.Icon_42, 1);
                    PlayfabManager.instance.GrantItemsToUser((IconType.Icon_42).ToString(), "Icon");
                }
                break;
            case 4:
                PortionManager.instance.GetRankPoint(1500);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 2000);
                break;
            case 5:
                PortionManager.instance.GetRankPoint(1000);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1000);
                break;
            case 6:
                PortionManager.instance.GetRankPoint(500);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);
                break;
        }

        switch (season)
        {
            case 1:
                playerDataBase.Season1Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season1Reward", playerDataBase.Season1Reward);
                break;
            case 2:
                playerDataBase.Season2Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season2Reward", playerDataBase.Season2Reward);
                break;
            case 3:
                playerDataBase.Season3Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season3Reward", playerDataBase.Season3Reward);
                break;
            case 4:
                playerDataBase.Season4Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season4Reward", playerDataBase.Season4Reward);
                break;
            case 5:
                playerDataBase.Season5Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season5Reward", playerDataBase.Season5Reward);
                break;
            case 6:
                playerDataBase.Season6Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season6Reward", playerDataBase.Season6Reward);
                break;
            case 7:
                playerDataBase.Season7Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season7Reward", playerDataBase.Season7Reward);
                break;
            case 8:
                playerDataBase.Season8Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season8Reward", playerDataBase.Season8Reward);
                break;
            case 9:
                playerDataBase.Season9Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season9Reward", playerDataBase.Season9Reward);
                break;
            case 10:
                playerDataBase.Season10Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season10Reward", playerDataBase.Season10Reward);
                break;
            case 11:
                playerDataBase.Season11Reward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Season11Reward", playerDataBase.Season11Reward);
                break;
        }

        yield return waitForSeconds;

        GameStateManager.instance.Food8Level = 0;
        GameStateManager.instance.Candy10Level = 0;
        GameStateManager.instance.JapaneseFood8Level = 0;
        GameStateManager.instance.Dessert10Level = 0;

        playerDataBase.RankLevel1 = 0;
        playerDataBase.RankLevel2 = 0;
        playerDataBase.RankLevel3 = 0;
        playerDataBase.RankLevel4 = 0;
        playerDataBase.TotalLevel = 0;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel1", playerDataBase.RankLevel1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel2", playerDataBase.RankLevel2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel3", playerDataBase.RankLevel3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel4", playerDataBase.RankLevel4);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel", playerDataBase.TotalLevel);

        playerDataBase.RankEventCount = 0;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankEventCount", playerDataBase.RankEventCount);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        FirebaseAnalytics.LogEvent("Clear_SeasonReward");
    }

    public void SetRankingReward(GetLeaderboardResult result)
    {
        var curBoard = result.Leaderboard;

        int index = 1;
        bool myCheck = false;

        foreach (PlayerLeaderboardEntry player in curBoard)
        {
            if (player.PlayFabId.Equals(GameStateManager.instance.PlayfabId))
            {
                myRank = index;

                myCheck = true;

                if (index == 1)
                {
                    SetReward(0);
                }
                else if (index == 2)
                {
                    SetReward(1);
                }
                else if (index == 3)
                {
                    SetReward(2);
                }
                else if (index > 3 && index < 11)
                {
                    SetReward(3);
                }
                else if (index > 10 && index < 21)
                {
                    SetReward(4);
                }
                else if (index > 20 && index < 101)
                {
                    SetReward(5);
                }
                else
                {
                    switch(season)
                    {
                        case 1:
                            if (playerDataBase.TotalLevel > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 2:
                            if (playerDataBase.TotalLevel_1 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 3:
                            if (playerDataBase.TotalLevel_2 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 4:
                            if (playerDataBase.TotalLevel_3 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 5:
                            if (playerDataBase.TotalLevel_4 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 6:
                            if (playerDataBase.TotalLevel_5 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 7:
                            if (playerDataBase.TotalLevel_6 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 8:
                            if (playerDataBase.TotalLevel_7 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 9:
                            if (playerDataBase.TotalLevel_8 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 10:
                            if (playerDataBase.TotalLevel_9 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 11:
                            if (playerDataBase.TotalLevel_10 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                    }
                }

                break;
            }

            index++;
        }

        if(!myCheck)
        {
            SetReward(7);
        }

        isDelay = false;
    }


    public int CheckSeason_Ranking()
    {
        currentDate = DateTime.Now;

        int number = 0;

        if (currentDate < season1Date)
        {
            number = 0;
        }
        else if (currentDate > season2Date_Start && currentDate < season2Date_End)
        {
            number = 1;
        }
        else if (currentDate > season3Date_Start && currentDate < season3Date_End)
        {
            number = 2;
        }
        else if (currentDate > season4Date_Start && currentDate < season4Date_End)
        {
            number = 3;
        }
        else if (currentDate > season5Date_Start && currentDate < season5Date_End)
        {
            number = 4;
        }
        else if (currentDate > season6Date_Start && currentDate < season6Date_End)
        {
            number = 5;
        }
        else if (currentDate > season7Date_Start && currentDate < season7Date_End)
        {
            number = 6;
        }
        else if (currentDate > season8Date_Start && currentDate < season8Date_End)
        {
            number = 7;
        }
        else if (currentDate > season9Date_Start && currentDate < season9Date_End)
        {
            number = 8;
        }
        else if (currentDate > season10Date_Start && currentDate < season10Date_End)
        {
            number = 9;
        }
        else if (currentDate > season11Date_Start && currentDate < season11Date_End)
        {
            number = 10;
        }
        else
        {
            number = -1;
        }

        return number;

    }

    public int CheckSeason()
    {
        currentDate = DateTime.Now;

        if (currentDate < season1Date)
        {
            season = 0;
        }
        else if (currentDate < season2Date_End)
        {
            season = 1;

            if (playerDataBase.Season1Reward == 0 && playerDataBase.TotalLevel > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season3Date_End)
        {
            season = 2;

            if (playerDataBase.Season2Reward == 0 && playerDataBase.TotalLevel_1 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season4Date_End)
        {
            season = 3;

            if (playerDataBase.Season3Reward == 0 && playerDataBase.TotalLevel_2 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season5Date_End)
        {
            season = 4;

            if (playerDataBase.Season4Reward == 0 && playerDataBase.TotalLevel_3 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season6Date_End)
        {
            season = 5;

            if (playerDataBase.Season5Reward == 0 && playerDataBase.TotalLevel_4 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season7Date_End)
        {
            season = 6;

            if (playerDataBase.Season6Reward == 0 && playerDataBase.TotalLevel_5 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season8Date_End)
        {
            season = 7;

            if (playerDataBase.Season7Reward == 0 && playerDataBase.TotalLevel_6 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season9Date_End)
        {
            season = 8;

            if (playerDataBase.Season8Reward == 0 && playerDataBase.TotalLevel_7 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season10Date_End)
        {
            season = 9;

            if (playerDataBase.Season9Reward == 0 && playerDataBase.TotalLevel_8 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season11Date_End)
        {
            season = 10;

            if (playerDataBase.Season10Reward == 0 && playerDataBase.TotalLevel_9 > 0)
            {
                Initialize();
            }
        }

        seasonText.localizationName = "Season";
        seasonText.plusText = (season + 1).ToString();
        seasonText.ReLoad();

        Debug.Log("현재 시즌 : " + season);

        return season;
    }
}
