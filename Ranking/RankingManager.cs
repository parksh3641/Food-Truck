using Firebase.Analytics;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public static RankingManager instance;

    public GameObject rankingView;

    public GameObject nextSeason;

    public GameObject alarm;

    public LocalizationContent infoText;

    [Space]
    [Title("Ranking Reward")]
    public GameObject rankingRewardView;
    public RectTransform rankingRewardTransform;
    public ReceiveContent[] receiveContents;
    public GameObject[] rankingRewardCheck;
    public Text rankingRewardDateText;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;

    public RankContent rankContentPrefab;
    public RankContent myRankContent;
    public RectTransform rankContentParent;

    public bool isDelay = false;
    public bool isDelay2 = false;

    private int record = 0;
    private int topNumber = 0;

    private string recordStr = "";
    public string country = "";

    private int listNumber = 0;
    private int listMaxNumber = 200;

    [Space]
    List<RankContent> rankContentList = new List<RankContent>();
    public List<string> rankList = new List<string>();
    public List<int> iconList = new List<int>();

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        instance = this;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        for (int i = 0; i < 100; i ++)
        {
            RankContent monster = Instantiate(rankContentPrefab) as RankContent;
            monster.name = "RankContent_" + i;
            monster.transform.position = Vector3.zero;
            monster.transform.localScale = Vector3.one;
            monster.transform.SetParent(rankContentParent);
            monster.gameObject.SetActive(false);

            rankContentList.Add(monster);
        }

        rankingView.SetActive(false);
        rankingRewardView.SetActive(false);

        rankContentParent.anchoredPosition = new Vector2(0, -9999);
        rankingRewardTransform.anchoredPosition = new Vector2(0, -9999);

        topNumber = -1;

        alarm.SetActive(true);
    }

    public void OpenRanking()
    {
        if(!rankingView.activeSelf)
        {
            rankingView.SetActive(true);

            alarm.SetActive(false);

            if (topNumber == -1)
            {
                ChangeTopToggle(0);
            }

            FirebaseAnalytics.LogEvent("OpenRanking");
        }
        else
        {
            if(!isDelay)
            {
                rankingView.SetActive(false);
            }
        }
    }

    public void OpenRankingReward()
    {
        if (!rankingRewardView.activeSelf)
        {
            rankingRewardView.SetActive(true);

            rankingRewardDateText.text = LocalizationManager.instance.GetString("Date") + " : ";

            switch (SeasonManager.instance.CheckSeason_Ranking())
            {
                case 0:
                    rankingRewardDateText.text += "2024.01.01 ~ 2024.01.31";
                    break;
                case 1:
                    rankingRewardDateText.text += "2024.02.02 ~ 2024.02.14";
                    break;
                case 2:
                    rankingRewardDateText.text += "2024.02.16 ~ 2024.02.29";
                    break;
                case 3:
                    rankingRewardDateText.text += "2024.03.02 ~ 2024.03.16";
                    break;
                case 4:
                    rankingRewardDateText.text += "2024.03.18 ~ 2024.03.31";
                    break;
                case 5:
                    rankingRewardDateText.text += "2024.04.02 ~ 2024.04.15";
                    break;
                case 6:
                    rankingRewardDateText.text += "2024.04.17 ~ 2024.04.30";
                    break;
                case 7:
                    rankingRewardDateText.text += "2024.05.02 ~ 2024.05.16";
                    break;
                case 8:
                    rankingRewardDateText.text += "2024.05.18 ~ 2024.05.31";
                    break;
                case 9:
                    rankingRewardDateText.text += "2024.06.02 ~ 2024.06.15";
                    break;
                case 10:
                    rankingRewardDateText.text += "2024.06.17 ~ 2024.06.30";
                    break;
                default:
                    rankingRewardDateText.text = LocalizationManager.instance.GetString("SeasonWait");
                    break;
            }

            for(int i = 0; i < rankingRewardCheck.Length; i ++)
            {
                rankingRewardCheck[i].SetActive(false);
            }

            receiveContents[0].Initialize(RewardType.Crystal, 10000);
            receiveContents[1].Initialize(RewardType.RankPoint, 5000);
            receiveContents[2].Initialize(RewardType.Icon_Ranking1, 1);

            receiveContents[3].Initialize(RewardType.Crystal, 8000);
            receiveContents[4].Initialize(RewardType.RankPoint, 4000);
            receiveContents[5].Initialize(RewardType.Icon_Ranking2, 1);

            receiveContents[6].Initialize(RewardType.Crystal, 6000);
            receiveContents[7].Initialize(RewardType.RankPoint, 3000);
            receiveContents[8].Initialize(RewardType.Icon_Ranking3, 1);

            receiveContents[9].Initialize(RewardType.Crystal, 4000);
            receiveContents[10].Initialize(RewardType.RankPoint, 2000);
            receiveContents[11].Initialize(RewardType.Icon_Ranking4, 1);

            receiveContents[12].Initialize(RewardType.Crystal, 2000);
            receiveContents[13].Initialize(RewardType.RankPoint, 1000);

            receiveContents[14].Initialize(RewardType.Crystal, 1000);
            receiveContents[15].Initialize(RewardType.RankPoint, 500);

            receiveContents[16].Initialize(RewardType.Crystal, 300);
            receiveContents[17].Initialize(RewardType.RankPoint, 100);

            isDelay = true;

            if (SeasonManager.instance.CheckSeason_Ranking() == 0)
            {
                PlayfabManager.instance.GetLeaderboarder(RankingType.TotalLevel.ToString(), 0, SetRankingReward);
            }
            else if (SeasonManager.instance.CheckSeason_Ranking() > 0)
            {
                PlayfabManager.instance.GetLeaderboarder("TotalLevel_" + SeasonManager.instance.CheckSeason().ToString(), 0, SetRankingReward);
            }
            else
            {
                isDelay = false;
            }

            FirebaseAnalytics.LogEvent("OpenRankingReward");
        }
        else
        {
            if (!isDelay && !isDelay2)
            {
                rankingRewardView.SetActive(false);
            }
        }
    }

    public void ChangeTopToggle(int number)
    {
        if (isDelay || isDelay2) return;

        if (topNumber == number) return;

        topNumber = number;

        for (int i = 0; i < topMenuImgArray.Length; i++)
        {
            topMenuImgArray[i].sprite = topMenuSpriteArray[0];
        }

        topMenuImgArray[number].sprite = topMenuSpriteArray[1];

        nextSeason.SetActive(false);

        for (int i = 0; i < rankContentList.Count; i++)
        {
            rankContentList[i].transform.localScale = Vector3.one;
            rankContentList[i].IconState(IconType.Icon_1);
            rankContentList[i].gameObject.SetActive(false);
        }

        switch (number)
        {
            case 0:
                ChangeRankingView(RankingType.UpgradeCount.ToString());

                infoText.localizationName = "Ranking1_Info";

                FirebaseAnalytics.LogEvent("OpenRanking1");
                break;
            case 1:
                if(SeasonManager.instance.CheckSeason_Ranking() == 0)
                {
                    ChangeRankingView("TotalLevel");
                }
                else if(SeasonManager.instance.CheckSeason_Ranking() > 0)
                {
                    ChangeRankingView("TotalLevel_" + SeasonManager.instance.CheckSeason().ToString());
                }
                else
                {
                    CheckCountry(country);

                    nextSeason.SetActive(true);
                }

                infoText.localizationName = "Ranking2_Info";

                FirebaseAnalytics.LogEvent("OpenRanking2");
                break;
            case 2:
                ChangeRankingView(RankingType.GourmetLevel.ToString());

                infoText.localizationName = "Ranking3_Info";

                FirebaseAnalytics.LogEvent("OpenRanking3");
                break;
            case 3:
                ChangeRankingView(RankingType.Level.ToString());

                infoText.localizationName = "Ranking4_Info";

                FirebaseAnalytics.LogEvent("OpenRanking4");
                break;
        }

        infoText.ReLoad();

    }

    public void ChangeRankingView(string type)
    {
        rankingView.SetActive(true);

        isDelay = true;
        PlayfabManager.instance.GetLeaderboarder(type, 0, SetRanking);

        if (type.Equals("UpgradeCount"))
        {
            isDelay2 = true;
            rankList.Clear();
            listNumber = 0;

            PlayfabManager.instance.GetLeaderboarder(type, listNumber, SetRankingList);
        }
    }

    public void SetRankingList(GetLeaderboardResult result)
    {
        var curBoard = result.Leaderboard;

        foreach (PlayerLeaderboardEntry player in curBoard)
        {
            if (player.DisplayName == null)
            {
                rankList.Add(player.PlayFabId);
            }
            else
            {
                rankList.Add(player.DisplayName);
            }
        }

        if(listNumber < listMaxNumber)
        {
            listNumber += 100;

            PlayfabManager.instance.GetLeaderboarder("UpgradeCount", listNumber, SetRankingList);
        }
        else
        {
            int index = 0;

            for(int i = 0; i < rankList.Count; i ++)
            {
                if(rankList[i].Equals(GameStateManager.instance.NickName))
                {
                    index = i + 1;
                    break;
                }
            }

            if(index != 0)
            {
                myRankContent.SetIndex(index);
            }
            else
            {
                myRankContent.SetIndex(listMaxNumber);
            }

            isDelay2 = false;

            for (int i = 0; i < rankContentList.Count; i++)
            {
                rankContentList[i].gameObject.SetActive(true);
            }

            rankContentParent.anchoredPosition = new Vector2(0, -9999);
        }
    }


    public void SetRanking(GetLeaderboardResult result)
    {
        int index = 1;
        bool isMine = false;
        bool isCheck = false;
        string nickName = "";

        var curBoard = result.Leaderboard;
        int num = 0;

        for(int i = 0; i < rankContentList.Count; i ++)
        {
            rankContentList[i].transform.localScale = Vector3.one;
            rankContentList[i].gameObject.SetActive(false);
        }

        foreach(PlayerLeaderboardEntry player in curBoard)
        {
            var location = curBoard[num].Profile.Locations[0].CountryCode.Value.ToString().ToLower();

            isMine = false;

            if (player.StatValue != 0)
            {
                recordStr = player.StatValue.ToString();
            }
            else
            {
                recordStr = "0";
            }

            if (player.DisplayName == null)
            {
                nickName = player.PlayFabId;
            }
            else
            {
                nickName = player.DisplayName;
            }

            if (player.PlayFabId.Equals(GameStateManager.instance.PlayfabId))
            {
                isMine = true;
                isCheck = true;

                country = location;

                myRankContent.InitState(index, location, nickName, recordStr, true);
            }
            else if(player.DisplayName != null)
            {
                if (player.DisplayName.Equals(GameStateManager.instance.NickName))
                {
                    isMine = true;
                    isCheck = true;

                    country = location;

                    myRankContent.InitState(index, location, nickName, recordStr, true);
                }
            }

            rankContentList[num].InitState(index, location, nickName, recordStr, isMine);

            if (!isDelay2)
            {
                rankContentList[num].gameObject.SetActive(true);
            }

            index++;
            num++;
        }

        if(!isCheck)
        {
            PlayfabManager.instance.GetPlayerProfile(GameStateManager.instance.PlayfabId, CheckCountry);
        }

        isDelay = false;

        PlayfabManager.instance.GetLeaderboarder("Advancement", 0, SetTitle);

        rankContentParent.anchoredPosition = new Vector2(0, -9999);
    }

    void SetTitle(GetLeaderboardResult result)
    {
        var curBoard = result.Leaderboard;

        for (int i = 0; i < rankContentList.Count; i++)
        {
            rankContentList[i].TitleState(0);
        }

        foreach (PlayerLeaderboardEntry player in curBoard)
        {
            for (int i = 0; i < rankContentList.Count; i++)
            {
                //rankContentList[i].TitleState(0);

                if (rankContentList[i].nickNameText.text.Equals(player.DisplayName) ||
                    rankContentList[i].nickNameText.text.Equals(player.PlayFabId))
                {
                    if (player.StatValue > 0)
                    {
                        rankContentList[i].TitleState(player.StatValue);
                    }

                    continue;
                }
            }
        }

        myRankContent.TitleState(playerDataBase.Advancement);

        PlayfabManager.instance.GetLeaderboarder("Icon", 0, SetIcon);
    }

    void SetIcon(GetLeaderboardResult result)
    {
        var curBoard = result.Leaderboard;

        foreach (PlayerLeaderboardEntry player in curBoard)
        {
            for (int i = 0; i < rankContentList.Count; i++)
            {
                if (rankContentList[i].nickNameText.text.Equals(player.DisplayName) ||
                    rankContentList[i].nickNameText.text.Equals(player.PlayFabId))
                {
                    if (player.StatValue > 0)
                    {
                        rankContentList[i].IconState((IconType)player.StatValue);
                    }

                    continue;
                }
            }

            myRankContent.IconState((IconType)playerDataBase.Icon);
        }
    }

    void CheckCountry(string code)
    {
        int number = 0;

        switch (topNumber)
        {
            case 0:
                number = playerDataBase.UpgradeCount;
                break;
            case 1:
                switch(SeasonManager.instance.CheckSeason_Ranking())
                {
                    case -1:
                        number = 0;
                        break;
                    case 0:
                        number = playerDataBase.TotalLevel;
                        break;
                    case 1:
                        number = playerDataBase.TotalLevel_1;
                        break;
                    case 2:
                        number = playerDataBase.TotalLevel_2;
                        break;
                    case 3:
                        number = playerDataBase.TotalLevel_3;
                        break;
                    case 4:
                        number = playerDataBase.TotalLevel_4;
                        break;
                    case 5:
                        number = playerDataBase.TotalLevel_5;
                        break;
                    case 6:
                        number = playerDataBase.TotalLevel_6;
                        break;
                    case 7:
                        number = playerDataBase.TotalLevel_7;
                        break;
                    case 8:
                        number = playerDataBase.TotalLevel_8;
                        break;
                    case 9:
                        number = playerDataBase.TotalLevel_9;
                        break;
                    case 10:
                        number = playerDataBase.TotalLevel_10;
                        break;
                }
                break;
            case 2:
                number = playerDataBase.ChallengePoint;
                break;
            case 3:
                number = playerDataBase.Level;
                break;
        }

        if (number != 0)
        {
            recordStr = number.ToString();
        }
        else
        {
            recordStr = "0";
        }

        myRankContent.InitState(999, code, GameStateManager.instance.NickName, recordStr, true);
    }

    public void SetRankingReward(GetLeaderboardResult result)
    {
        var curBoard = result.Leaderboard;

        int index = 1;

        foreach (PlayerLeaderboardEntry player in curBoard)
        {
            if (player.PlayFabId.Equals(GameStateManager.instance.PlayfabId))
            {
                if(index == 1)
                {
                    rankingRewardCheck[0].SetActive(true);
                }
                else if (index == 2)
                {
                    rankingRewardCheck[1].SetActive(true);
                }
                else if (index == 3)
                {
                    rankingRewardCheck[2].SetActive(true);
                }
                else if (index > 3 && index < 11)
                {
                    rankingRewardCheck[3].SetActive(true);
                }
                else if (index > 10 && index < 21)
                {
                    rankingRewardCheck[4].SetActive(true);
                }
                else if (index > 20 && index < 101)
                {
                    rankingRewardCheck[5].SetActive(true);
                }
                else
                {
                    if(playerDataBase.TotalLevel > 0)
                    {
                        rankingRewardCheck[6].SetActive(true);
                    }
                }
                break;
            }

            index++;
        }

        isDelay = false;
    }
}
