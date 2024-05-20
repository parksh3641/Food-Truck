using Firebase.Analytics;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SeasonRewardInfo
{
    public List<int> seasonRewardList = new List<int>();

    public void Initialize()
    {
        if(seasonRewardList.Count <= 0)
        {
            for (int i = 0; i < 999; i++)
            {
                seasonRewardList.Add(0);
            }
        }

        for (int i = 0; i < seasonRewardList.Count; i ++)
        {
            seasonRewardList[i] = 0;
        }
    }

    public void SaveServerData(SeasonRewardInfo info)
    {
        for(int i = 0; i < info.seasonRewardList.Count; i ++)
        {
            seasonRewardList[i] = info.seasonRewardList[i];
        }
    }
}

public class SeasonManager : MonoBehaviour
{
    public static SeasonManager instance;

    public GameObject seasonRewardView;

    public LocalizationContent seasonText;
    public Text seasonRewardText;

    public ReceiveContent[] receiveContents;

    private int number = 0;
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

    DateTime season12Date_Start = new DateTime(2024, 7, 2);
    DateTime season12Date_End = new DateTime(2024, 7, 16, 23, 59, 59);

    DateTime season13Date_Start = new DateTime(2024, 7, 18);
    DateTime season13Date_End = new DateTime(2024, 7, 31, 23, 59, 59);

    DateTime season14Date_Start = new DateTime(2024, 8, 2);
    DateTime season14Date_End = new DateTime(2024, 8, 16, 23, 59, 59);

    DateTime season15Date_Start = new DateTime(2024, 8, 18);
    DateTime season15Date_End = new DateTime(2024, 8, 31, 23, 59, 59);

    DateTime season16Date_Start = new DateTime(2024, 9, 2);
    DateTime season16Date_End = new DateTime(2024, 9, 15, 23, 59, 59);

    DateTime season17Date_Start = new DateTime(2024, 9, 16);
    DateTime season17Date_End = new DateTime(2024, 9, 30, 23, 59, 59);

    DateTime season18Date_Start = new DateTime(2024, 10, 2);
    DateTime season18Date_End = new DateTime(2024, 10, 16, 23, 59, 59);

    DateTime season19Date_Start = new DateTime(2024, 10, 18);
    DateTime season19Date_End = new DateTime(2024, 10, 31, 23, 59, 59);

    DateTime season20Date_Start = new DateTime(2024, 11, 2);
    DateTime season20Date_End = new DateTime(2024, 11, 15, 23, 59, 59);


    WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    PlayerDataBase playerDataBase;

    private Dictionary<string, string> playerData = new Dictionary<string, string>();


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

                receiveContents[0].Initialize(RewardType.Crystal, 5000);
                receiveContents[1].Initialize(RewardType.RankPoint, 10000);
                receiveContents[2].Initialize(RewardType.Icon_Ranking1, 1);

                break;
            case 1:
                receiveContents[2].gameObject.SetActive(true);

                receiveContents[0].Initialize(RewardType.Crystal, 2500);
                receiveContents[1].Initialize(RewardType.RankPoint, 5000);
                receiveContents[2].Initialize(RewardType.Icon_Ranking2, 1);

                break;
            case 2:
                receiveContents[2].gameObject.SetActive(true);

                receiveContents[0].Initialize(RewardType.Crystal, 1500);
                receiveContents[1].Initialize(RewardType.RankPoint, 2500);
                receiveContents[2].Initialize(RewardType.Icon_Ranking3, 1);

                break;
            case 3:
                receiveContents[2].gameObject.SetActive(true);

                receiveContents[0].Initialize(RewardType.Crystal, 1000);
                receiveContents[1].Initialize(RewardType.RankPoint, 1500);
                receiveContents[2].Initialize(RewardType.Icon_Ranking4, 1);

                break;
            case 4:
                receiveContents[0].Initialize(RewardType.Crystal, 750);
                receiveContents[1].Initialize(RewardType.RankPoint, 1000);

                break;
            case 5:
                receiveContents[0].Initialize(RewardType.Crystal, 500);
                receiveContents[1].Initialize(RewardType.RankPoint, 750);

                break;
            case 6:
                receiveContents[0].Initialize(RewardType.Crystal, 250);
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
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 5000);
                PortionManager.instance.GetRankPoint(10000);

                if (!playerDataBase.CheckIcon(IconType.Icon_39))
                {
                    playerDataBase.SetIcon(IconType.Icon_39, 1);
                    PlayfabManager.instance.GrantItemsToUser((IconType.Icon_39).ToString(), "Icon");
                }
                break;
            case 1:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 2500);
                PortionManager.instance.GetRankPoint(5000);

                if (!playerDataBase.CheckIcon(IconType.Icon_40))
                {
                    playerDataBase.SetIcon(IconType.Icon_40, 1);
                    PlayfabManager.instance.GrantItemsToUser((IconType.Icon_40).ToString(), "Icon");
                }
                break;
            case 2:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1500);
                PortionManager.instance.GetRankPoint(2500);

                if (!playerDataBase.CheckIcon(IconType.Icon_41))
                {
                    playerDataBase.SetIcon(IconType.Icon_41, 1);
                    PlayfabManager.instance.GrantItemsToUser((IconType.Icon_41).ToString(), "Icon");
                }
                break;
            case 3:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1000);
                PortionManager.instance.GetRankPoint(1500);

                if (!playerDataBase.CheckIcon(IconType.Icon_42))
                {
                    playerDataBase.SetIcon(IconType.Icon_42, 1);
                    PlayfabManager.instance.GrantItemsToUser((IconType.Icon_42).ToString(), "Icon");
                }
                break;
            case 4:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 750);
                PortionManager.instance.GetRankPoint(1000);
                break;
            case 5:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 500);
                PortionManager.instance.GetRankPoint(750);
                break;
            case 6:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 250);
                PortionManager.instance.GetRankPoint(500);
                break;
        }

        playerDataBase.seasonRewardInfo.seasonRewardList[season] = 1;

        playerData.Clear();
        playerData.Add("SeasonRewardInfo", JsonUtility.ToJson(playerDataBase.seasonRewardInfo));
        PlayfabManager.instance.SetPlayerData(playerData);


        yield return waitForSeconds;

        for (int i = 0; i < GameStateManager.instance.RankFoodLevel.Length; i++)
        {
            GameStateManager.instance.RankFoodLevel[i] = 0;
        }

        playerDataBase.RankLevel1 = 0;
        playerDataBase.RankLevel2 = 0;
        playerDataBase.RankLevel3 = 0;
        playerDataBase.RankLevel4 = 0;
        playerDataBase.RankLevel5 = 0;
        playerDataBase.RankLevel6 = 0;
        playerDataBase.RankLevel7 = 0;
        playerDataBase.RankLevel8 = 0;
        playerDataBase.RankLevel9 = 0;
        playerDataBase.RankLevel10 = 0;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel1", playerDataBase.RankLevel1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel2", playerDataBase.RankLevel2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel3", playerDataBase.RankLevel3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel4", playerDataBase.RankLevel4);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel5", playerDataBase.RankLevel5);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel6", playerDataBase.RankLevel6);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel7", playerDataBase.RankLevel7);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel8", playerDataBase.RankLevel8);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel9", playerDataBase.RankLevel9);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel10", playerDataBase.RankLevel10);

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
                        case 12:
                            if (playerDataBase.TotalLevel_11 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 13:
                            if (playerDataBase.TotalLevel_12 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 14:
                            if (playerDataBase.TotalLevel_13 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 15:
                            if (playerDataBase.TotalLevel_14 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 16:
                            if (playerDataBase.TotalLevel_15 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 17:
                            if (playerDataBase.TotalLevel_16 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 18:
                            if (playerDataBase.TotalLevel_17 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 19:
                            if (playerDataBase.TotalLevel_18 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 20:
                            if (playerDataBase.TotalLevel_19 > 4)
                            {
                                SetReward(6);
                            }
                            break;
                        case 21:
                            if (playerDataBase.TotalLevel_20 > 4)
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

        number = 0;

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
        else if (currentDate > season12Date_Start && currentDate < season12Date_End)
        {
            number = 11;
        }
        else if (currentDate > season13Date_Start && currentDate < season13Date_End)
        {
            number = 12;
        }
        else if (currentDate > season14Date_Start && currentDate < season14Date_End)
        {
            number = 13;
        }
        else if (currentDate > season15Date_Start && currentDate < season15Date_End)
        {
            number = 14;
        }
        else if (currentDate > season16Date_Start && currentDate < season16Date_End)
        {
            number = 15;
        }
        else if (currentDate > season17Date_Start && currentDate < season17Date_End)
        {
            number = 16;
        }
        else if (currentDate > season18Date_Start && currentDate < season18Date_End)
        {
            number = 17;
        }
        else if (currentDate > season19Date_Start && currentDate < season19Date_End)
        {
            number = 18;
        }
        else if (currentDate > season20Date_Start && currentDate < season20Date_End)
        {
            number = 19;
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

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season3Date_End)
        {
            season = 2;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_1 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season4Date_End)
        {
            season = 3;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_2 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season5Date_End)
        {
            season = 4;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_3 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season6Date_End)
        {
            season = 5;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_4 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season7Date_End)
        {
            season = 6;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_5 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season8Date_End)
        {
            season = 7;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_6 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season9Date_End)
        {
            season = 8;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_7 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season10Date_End)
        {
            season = 9;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_8 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season11Date_End)
        {
            season = 10;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_9 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season12Date_End)
        {
            season = 11;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_10 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season13Date_End)
        {
            season = 12;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_11 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season14Date_End)
        {
            season = 13;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_12 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season15Date_End)
        {
            season = 14;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_13 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season16Date_End)
        {
            season = 15;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_14 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season17Date_End)
        {
            season = 16;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_15 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season18Date_End)
        {
            season = 17;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_16 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season19Date_End)
        {
            season = 18;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_17 > 0)
            {
                Initialize();
            }
        }
        else if (currentDate < season20Date_End)
        {
            season = 19;

            if (playerDataBase.seasonRewardInfo.seasonRewardList[season] == 0 && playerDataBase.TotalLevel_18 > 0)
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
