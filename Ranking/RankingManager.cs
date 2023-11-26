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

    public GameObject alarm;

    private LevelType scoreLevel = LevelType.Insane;

    public GameObject rankingView;

    public LocalizationContent infoText;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;

    public RankContent rankContentPrefab;
    public RankContent myRankContent;
    public RectTransform rankContentParent;

    private bool isDelay = false;

    private int timerMinutes = 0;
    private int timerSeconds = 0;
    private float timermilliseconds = 0;

    private int record = 0;

    private int topNumber = 0;

    private string recordStr = "";

    [Space]
    List<RankContent> rankContentList = new List<RankContent>();

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

        rankContentParent.anchoredPosition = new Vector2(0, -9999);

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

    public void ChangeTopToggle(int number)
    {
        if (topNumber == number) return;

        if (isDelay) return;

        topNumber = number;

        for (int i = 0; i < topMenuImgArray.Length; i++)
        {
            topMenuImgArray[i].sprite = topMenuSpriteArray[0];
        }

        topMenuImgArray[number].sprite = topMenuSpriteArray[1];

        switch(number)
        {
            case 0:
                ChangeRankingView(RankingType.UpgradeCount);

                infoText.localizationName = "Ranking1_Info";
                break;
            case 1:
                ChangeRankingView(RankingType.TotalLevel);

                infoText.localizationName = "Ranking2_Info";
                break;
            case 2:
                ChangeRankingView(RankingType.Michelin);

                infoText.localizationName = "Ranking3_Info";
                break;
        }

        infoText.ReLoad();

    }

    public void ChangeRankingView(RankingType type)
    {
        rankingView.SetActive(true);

        isDelay = true;

        PlayfabManager.instance.GetLeaderboarder(type.ToString(), SetRanking);
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

                myRankContent.InitState(index, location, nickName, recordStr, true);
            }
            else if(player.DisplayName != null)
            {
                if (player.DisplayName.Equals(GameStateManager.instance.NickName))
                {
                    isMine = true;
                    isCheck = true;

                    myRankContent.InitState(index, location, nickName, recordStr, true);
                }
            }

            rankContentList[num].InitState(index, location, nickName, recordStr, isMine);
            rankContentList[num].gameObject.SetActive(true);

            index++;
            num++;
        }

        if(!isCheck)
        {
            PlayfabManager.instance.GetPlayerProfile(GameStateManager.instance.PlayfabId, CheckCountry);
        }

        isDelay = false;

        rankContentParent.anchoredPosition = new Vector2(0, -9999);
    }

    void CheckCountry(string code)
    {
        int number = 0;

        switch (topNumber)
        {
            case 0:
                number = playerDataBase.GourmetLevel;
                break;
            case 1:
                number = playerDataBase.QuestCount;
                break;
            case 2:
                number = playerDataBase.Michelin;
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
}
