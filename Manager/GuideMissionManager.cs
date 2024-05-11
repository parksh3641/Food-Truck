using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideMissionManager : MonoBehaviour
{
    public GameObject guideMissonView;

    public GameObject defTickets;

    public GameObject clearObj;
    public GameObject checkMark;

    public LocalizationContent titleText;
    public Text rewardText;

    private long now = 0;
    private int need = 0;

    private bool clear = false;
    private bool firstReset = false;

    private int reward = 10;

    public GameObject buffArrow;
    public GameObject portionArrow;

    public ChangeFoodManager changeFoodManager;
    public ShopManager shopManager;
    public SkillManager skillManager;
    public TreasureManager treasureManager;
    public QuestManager questManager;
    public WelcomeManager welcomeManager;
    public AttendanceManager attendanceManager;
    public DungeonManager dungeonManager;
    public IconManager iconManager;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        guideMissonView.SetActive(false);
        clearObj.SetActive(false);
        checkMark.SetActive(false);

        rewardText.text = reward.ToString();

        buffArrow.SetActive(false);
        portionArrow.SetActive(false);
    }

    [Button]
    public void GuideReset()
    {
        playerDataBase.GuideIndex = 0;
    }

    [Button]
    public void NextGuide()
    {
        playerDataBase.GuideIndex += 1;

        Initialize();
    }

    public void Initialize()
    {
        if (GameStateManager.instance.YoutubeVideo) return;

        if (playerDataBase.GuideIndex > 27)
        {
            guideMissonView.SetActive(false);
            return;
        }

        guideMissonView.SetActive(true);

        titleText.GetText().color = Color.white;
        titleText.localizationName = "GuideMisson_" + (playerDataBase.GuideIndex + 1);

        buffArrow.SetActive(false);
        portionArrow.SetActive(false);

        switch (playerDataBase.GuideIndex)
        {
            case 0:
                if(playerDataBase.island_Total_Data.island_Max_Datas[0] != null)
                {
                    now = playerDataBase.island_Total_Data.island_Max_Datas[0].index1;
                }
                need = 1;
                break;
            case 1:
                if (playerDataBase.island_Total_Data.island_Max_Datas[0] != null)
                {
                    now = playerDataBase.island_Total_Data.island_Max_Datas[0].index2;
                }
                need = 1;
                break;
            case 2:
                now = playerDataBase.UseSauceCount;
                need = 1;

                if(now < need)
                {
                    portionArrow.SetActive(true);
                }

                break;
            case 3:
                if(!firstReset)
                {
                    firstReset = true;
                    GameStateManager.instance.GetSellGold = 0;
                }

                now = GameStateManager.instance.GetSellGold;
                need = 100000;
                break;
            case 4:
                now = playerDataBase.Character2;
                need = 1;
                break;
            case 5:
                if (playerDataBase.island_Total_Data.island_Max_Datas[0] != null)
                {
                    now = playerDataBase.island_Total_Data.island_Max_Datas[0].index3;
                }
                need = 1;
                break;
            case 6:
                now = playerDataBase.YummyTimeCount;
                need = 1;
                break;
            case 7:
                if (!firstReset)
                {
                    firstReset = true;
                    GameStateManager.instance.GetSellGold = 0;
                }

                now = GameStateManager.instance.GetSellGold;
                need = 300000;
                break;
            case 8:
                now = playerDataBase.Character3;
                need = 1;
                break;
            case 9:
                if (playerDataBase.island_Total_Data.island_Max_Datas[0] != null)
                {
                    now = playerDataBase.island_Total_Data.island_Max_Datas[0].index4;
                }
                need = 1;
                break;
            case 10:
                now = playerDataBase.GetRecipeUpgradeCount();
                need = 3;
                break;
            case 11:
                now = playerDataBase.BuffCount;
                need = 1;

                if (now < need)
                {
                    buffArrow.SetActive(true);
                }
                break;
            case 12:
                if (playerDataBase.island_Total_Data.island_Max_Datas[0] != null)
                {
                    now = playerDataBase.island_Total_Data.island_Max_Datas[0].index5;
                }
                need = 1;
                break;
            case 13:
                now = playerDataBase.QuestCount;
                need = 1;
                break;
            case 14:
                now = playerDataBase.OpenChestBox;
                need = 2;
                break;
            case 15:
                now = playerDataBase.Character4;
                need = 1;
                break;
            case 16:
                if (playerDataBase.island_Total_Data.island_Max_Datas[0] != null)
                {
                    now = playerDataBase.island_Total_Data.island_Max_Datas[0].index6;
                }
                need = 1;
                break;
            case 17:
                if (!firstReset)
                {
                    firstReset = true;
                    GameStateManager.instance.GetSellGold = 0;
                }

                now = GameStateManager.instance.GetSellGold;
                need = 500000;
                break;
            case 18:
                now = playerDataBase.Animal2;
                need = 1;
                break;
            case 19:
                now = playerDataBase.OfflineCount;
                need = 1;
                break;
            case 20:
                if (playerDataBase.island_Total_Data.island_Max_Datas[0] != null)
                {
                    now = playerDataBase.island_Total_Data.island_Max_Datas[0].index7;
                }
                need = 1;
                break;
            case 21:
                if (playerDataBase.island_Total_Data.island_Max_Datas[0] != null)
                {
                    now = playerDataBase.island_Total_Data.island_Max_Datas[0].index8;
                }
                need = 1;
                break;
            case 22:
                now = playerDataBase.TreasureCount;
                need = 10;
                break;
            case 23:
                now = playerDataBase.Dungeon1Count + playerDataBase.Dungeon2Count + playerDataBase.Dungeon3Count + playerDataBase.Dungeon4Count;
                need = 2;
                break;
            case 24:
                now = playerDataBase.GetAnimal_Total_AbilityLevel() + playerDataBase.GetTruck_Total_AbilityLevel() + playerDataBase.GetCharacter_Total_AbilityLevel()
                    + playerDataBase.GetButterfly_Total_AbilityLevel() + playerDataBase.GetTruck_Total_AbilityLevel();
                need = 1;
                break;
            case 25:
                now = playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4 + playerDataBase.RankLevel5
                    + playerDataBase.RankLevel6 + playerDataBase.RankLevel7 + playerDataBase.RankLevel8 + playerDataBase.RankLevel9 + playerDataBase.RankLevel10;
                need = 20;
                break;
            case 26:
                now = playerDataBase.Icon;
                need = 1;
                break;
            case 27:
                now = 0;
                need = 1;
                break;
        }

        titleText.plusText = "\n( " + MoneyUnitString.ToCurrencyString(now) + " / " + MoneyUnitString.ToCurrencyString(need) + " )";
        titleText.ReLoad();

        CheckMission();
    }

    public void CheckMission()
    {
        clearObj.SetActive(false);
        checkMark.SetActive(false);
        clear = false;

        if (now >= need)
        {
            CheckClear();
        }
    }

    void CheckClear()
    {
        titleText.GetText().color = Color.yellow;
        clearObj.SetActive(true);
        checkMark.SetActive(true);
        clear = true;
    }

    public void ClearButton()
    {
        if(clear)
        {
            if (!NetworkConnect.instance.CheckConnectInternet())
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
                return;
            }

            firstReset = false;

            playerDataBase.GuideIndex += 1;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("GuideIndex", playerDataBase.GuideIndex);

            if(playerDataBase.GuideIndex == 6)
            {
                if(!playerDataBase.WelcomeCheck && playerDataBase.WelcomeCount < 7)
                {
                    welcomeManager.first = true;
                    welcomeManager.OpenWelcomeView();
                }
                else if (!playerDataBase.AttendanceCheck)
                {
                    attendanceManager.first = true;
                    attendanceManager.OpenAttendanceView();
                }
            }

            PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, reward);

            SoundManager.instance.PlaySFX(GameSfxType.Success);
            NotionManager.instance.UseNotion(NotionType.SuccessReward);

            FirebaseAnalytics.LogEvent("Clear_GuideMission");

            Initialize();
        }
        else
        {
            switch(playerDataBase.GuideIndex)
            {
                case 0:
                    GameStateManager.instance.IslandType = IslandType.Island1;
                    changeFoodManager.OpenChangeFoodView();
                    break;
                case 1:
                    GameStateManager.instance.IslandType = IslandType.Island1;
                    changeFoodManager.OpenChangeFoodView();
                    break;
                case 4:
                    shopManager.OpenSpeicalShop_Guide(2);
                    break;
                case 5:
                    GameStateManager.instance.IslandType = IslandType.Island1;
                    changeFoodManager.OpenChangeFoodView();
                    break;
                case 8:
                    shopManager.OpenSpeicalShop_Guide(2);
                    break;
                case 9:
                    GameStateManager.instance.IslandType = IslandType.Island1;
                    changeFoodManager.OpenChangeFoodView();
                    break;
                case 10:
                    skillManager.OpenSkillView();
                    break;
                case 12:
                    GameStateManager.instance.IslandType = IslandType.Island1;
                    changeFoodManager.OpenChangeFoodView();
                    break;
                case 13:
                    questManager.OpenQuestView();
                    break;
                case 15:
                    shopManager.OpenSpeicalShop_Guide(2);
                    break;
                case 16:
                    GameStateManager.instance.IslandType = IslandType.Island2;
                    changeFoodManager.OpenChangeFoodView();
                    break;
                case 18:
                    shopManager.OpenSpeicalShop_Guide(0);
                    break;
                case 20:
                    GameStateManager.instance.IslandType = IslandType.Island2;
                    changeFoodManager.OpenChangeFoodView();
                    break;
                case 21:
                    GameStateManager.instance.IslandType = IslandType.Island2;
                    changeFoodManager.OpenChangeFoodView();
                    break;
                case 22:
                    treasureManager.OpenTreasureView();
                    break;
                case 23:
                    dungeonManager.OpenDungeonView();
                    break;
                case 24:
                    shopManager.OpenSpeicalShop();
                    break;
                case 25:
                    GameManager.instance.GameStart(1);
                    break;
                case 26:
                    iconManager.OpenIcon();
                    break;
                case 27:

                    break;
            }
        }
    }
}
