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

    private LevelType scoreLevel = LevelType.Insane;

    public GameObject rankingView;

    public Text titleText;

    public RankContent rankContentPrefab;
    public RankContent myRankContent;
    public RectTransform rankContentParent;

    private int openNumber = 0;
    private bool isDelay = false;

    private int timerMinutes = 0;
    private int timerSeconds = 0;
    private float timermilliseconds = 0;

    private int record = 0;

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
            monster.transform.parent = rankContentParent;
            monster.gameObject.SetActive(false);

            rankContentList.Add(monster);
        }

        rankingView.SetActive(false);

        rankContentParent.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenRanking()
    {
        if(!rankingView.activeSelf)
        {
            if (NetworkConnect.instance.CheckConnectInternet())
            {
                ChangeRankingView(GameStateManager.instance.LevelType);
            }
            else
            {
                NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            }
        }
        else
        {
            if(!isDelay)
            {
                rankingView.SetActive(false);
            }
        }
    }

    public void ChangeRankingView(LevelType type)
    {
        rankingView.SetActive(true);

        isDelay = true;

        //switch (type)
        //{
        //    case LevelType.Easy:
        //        titleText.text = LocalizationManager.instance.GetString("Game1");
        //        break;
        //    case LevelType.Normal:
        //        titleText.text = LocalizationManager.instance.GetString("Game2");
        //        break;
        //    case LevelType.Hard:
        //        titleText.text = LocalizationManager.instance.GetString("Game3");
        //        break;
        //    case LevelType.Crazy:
        //        titleText.text = LocalizationManager.instance.GetString("Game4");
        //        break;
        //    case LevelType.Insane:
        //        titleText.text = LocalizationManager.instance.GetString("Game5");
        //        break;
        //    default:
        //        titleText.text = LocalizationManager.instance.GetString("Classic");
        //        break;
        //}

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
                if(GameStateManager.instance.LevelType != scoreLevel)
                {
                    record = 3600000 - player.StatValue;

                    timerMinutes = (record / 60000);
                    timerSeconds = (record % 60000) / 1000;
                    timermilliseconds = int.Parse(record.ToString().Substring(Mathf.Max(record.ToString().Length - 3, 0)));

                    if (timerMinutes >= 1)
                    {
                        recordStr = timerMinutes.ToString("D2") + ":" + timerSeconds.ToString("D2") + ".<size=11>" + timermilliseconds.ToString("000") + "</size>";
                    }
                    else
                    {
                        recordStr = timerSeconds.ToString("D2") + ".<size=11>" + timermilliseconds.ToString("000") + "</size>";
                    }
                }
                else
                {
                    recordStr = player.StatValue.ToString();
                }
            }
            else
            {
                if (GameStateManager.instance.LevelType != scoreLevel)
                {
                    recordStr = "00:00.<size=11>000</size>";
                }
                else
                {
                    recordStr = "0";
                }
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

        switch (openNumber)
        {
            case 0:
                number = playerDataBase.Easy;
                break;
            case 1:
                number = playerDataBase.Normal;
                break;
            case 2:
                number = playerDataBase.Hard;
                break;
            case 3:
                number = playerDataBase.Crazy;
                break;
            case 4:
                number = playerDataBase.Insane;
                break;
        }

        if (number != 0)
        {
            if (GameStateManager.instance.LevelType != scoreLevel)
            {
                record = 3600000 - number;

                timerMinutes = (record / 36000);
                timerSeconds = (record % 36000) / 1000;
                timermilliseconds = int.Parse(record.ToString().Substring(Mathf.Max(record.ToString().Length - 3, 0)));

                if (timerMinutes >= 1)
                {
                    recordStr = timerMinutes.ToString("D2") + ":" + timerSeconds.ToString("D2") + ".<size=11>" + timermilliseconds.ToString("000") + "</size>";
                }
                else
                {
                    recordStr = timerSeconds.ToString("D2") + ".<size=11>" + timermilliseconds.ToString("000") + "</size>";
                }
            }
            else
            {
                recordStr = number.ToString();
            }
        }
        else
        {
            if (GameStateManager.instance.LevelType != scoreLevel)
            {
                recordStr = "00:00.<size=11>000</size>";
            }
            else
            {
                recordStr = "0";
            }
        }

        myRankContent.InitState(999, code, GameStateManager.instance.NickName, recordStr, true);
    }
}
