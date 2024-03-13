using Firebase.Analytics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DungeonManager : MonoBehaviour
{
    DungeonType dungeonType = DungeonType.Dungeon1;

    public GameObject dungeonView;
    public GameObject dungeonInfoView;
    public GameObject dungeonInfo2View;

    public GameObject dungeonLevelView;

    public GameObject dungeonClearView;
    public ReceiveContent[] clearReceiveContents;

    public RectTransform rectTransform;

    public GameObject alarm;
    public GameObject ingameAlarm;

    public DungeonContent[] dungeonContents;
    public ReceiveContent[] receiveContents;
    public BossFoodContent[] bossFoodContents;

    public FadeInOut fadeInOut;

    [Space]
    [Title("Text")]
    public Text gourmetPointText;
    public Text timerText;

    [Space]
    [Title("Dungeon Level Select")]
    public Text targetText;
    public Text levelText;

    public ReceiveContent[] levelReceiveContents;

    public GameObject leftButton;
    public GameObject rightButton;

    [Space]
    [Title("Dungeon UI")]
    public LocalizationContent titleText;
    public LocalizationContent attackText;
    public LocalizationContent attackSpeedText;
    public LocalizationContent attackX2Text;
    public LocalizationContent inGameTimerText;

    public Image timerFillamount;
    public Image healthFillamount;

    public Text healthText;

    private int health = 0;
    private int saveHealth = 0;
    private float healthPercent = 0;

    private int timer = 0;
    private int saveTimer = 0;

    private float success = 0;
    private float attackPlus = 0;
    private float attackSpeed = 0;
    private float attackX2 = 0;

    private float attackDelay = 0.4f;

    private bool delay = false;
    private bool clear = false;

    private int plusNumber = 0;
    private int plusNumber1 = 0;
    private int plusNumber2 = 0;

    private int nowLevel = 0;
    private int highLevel = 0;

    DungeonInfo dungeonInfo = new DungeonInfo();

    public ChangeFoodManager changeFoodManager;

    public ParticleSystem successParticle;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    DateTime f, g;
    TimeSpan h;

    PlayerDataBase playerDataBase;
    DungeonDataBase dungeonDataBase;
    CharacterDataBase characterDataBase;
    LevelDataBase levelDataBase;
    ButterflyDataBase butterflyDataBase;
    TotemsDataBase totemsDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (dungeonDataBase == null) dungeonDataBase = Resources.Load("DungeonDataBase") as DungeonDataBase;
        if (characterDataBase == null) characterDataBase = Resources.Load("CharacterDataBase") as CharacterDataBase;
        if (butterflyDataBase == null) butterflyDataBase = Resources.Load("ButterflyDataBase") as ButterflyDataBase;
        if (levelDataBase == null) levelDataBase = Resources.Load("LevelDataBase") as LevelDataBase;
        if (totemsDataBase == null) totemsDataBase = Resources.Load("TotemsDataBase") as TotemsDataBase;

        dungeonView.SetActive(false);
        dungeonInfoView.SetActive(false);
        dungeonInfo2View.SetActive(false);
        dungeonLevelView.SetActive(false);
        dungeonClearView.SetActive(false);

        alarm.SetActive(true);
        ingameAlarm.SetActive(true);

        for (int i = 0; i < bossFoodContents.Length; i++)
        {
            bossFoodContents[i].gameObject.SetActive(false);
        }

        successParticle.gameObject.SetActive(false);

        rectTransform.anchoredPosition = new Vector2(0, -9999);
    }


    public void OpenDungeonView()
    {
        if (!dungeonView.activeInHierarchy)
        {
            dungeonView.SetActive(true);

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            alarm.SetActive(false);
            ingameAlarm.SetActive(false);

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            timerText.text = "";
            f = DateTime.Now;
            g = DateTime.Today.AddDays(1);
            StartCoroutine(TimerCoroution());

            ContentInitialize();
        }
        else
        {
            StopAllCoroutines();

            dungeonView.SetActive(false);
        }
    }

    void ContentInitialize()
    {
        gourmetPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.GourmetLevel);

        dungeonContents[0].Initialize(this, DungeonType.Dungeon1, dungeonDataBase.dungeonInfos[0], ItemType.DungeonKey1, 0);
        dungeonContents[1].Initialize(this, DungeonType.Dungeon2, dungeonDataBase.dungeonInfos[1], ItemType.DungeonKey2, 30000);
        dungeonContents[2].Initialize(this, DungeonType.Dungeon3, dungeonDataBase.dungeonInfos[2], ItemType.DungeonKey3, 100000);
        dungeonContents[3].Initialize(this, DungeonType.Dungeon4, dungeonDataBase.dungeonInfos[3], ItemType.DungeonKey4, 250000);

        //receiveContents[0].Initialize(RewardType.SliverBox, 0);
        //receiveContents[1].Initialize(RewardType.GoldBox, 0);
        //receiveContents[2].gameObject.SetActive(false);
    }

    IEnumerator TimerCoroution()
    {
        if (timerText.gameObject.activeInHierarchy)
        {
            h = g - f;

            timerText.text = localization_Reset + " : " + h.Hours.ToString("D2") + localization_Hours + " " + h.Minutes.ToString("D2") + localization_Minutes;

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
                yield break;
            }
        }

        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
    }

    public void EnterDungeon(DungeonType type)
    {
        Debug.LogError(type + " 입장 전");

        dungeonType = type;
        dungeonInfo = dungeonDataBase.GetDungeonInfo(type);

        dungeonLevelView.SetActive(true);

        targetText.text = LocalizationManager.instance.GetString(type.ToString() + "Title");

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                nowLevel = playerDataBase.Dungeon1Level;
                highLevel = playerDataBase.Dungeon1Level;

                break;
            case DungeonType.Dungeon2:
                nowLevel = playerDataBase.Dungeon2Level;
                highLevel = playerDataBase.Dungeon2Level;

                break;
            case DungeonType.Dungeon3:
                nowLevel = playerDataBase.Dungeon3Level;
                highLevel = playerDataBase.Dungeon3Level;

                break;
            case DungeonType.Dungeon4:
                nowLevel = playerDataBase.Dungeon4Level;
                highLevel = playerDataBase.Dungeon4Level;

                break;
        }

        LevelInitialize(nowLevel);
    }

    void LevelInitialize(int level)
    {
        nowLevel = level;

        leftButton.SetActive(true);
        rightButton.SetActive(true);

        if (level == 0)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(false);

            if(level < highLevel)
            {
                rightButton.SetActive(true);
            }
        }

        if(nowLevel == highLevel)
        {
            rightButton.SetActive(false);
        }

        levelText.text = "Lv. " + (level + 1).ToString();

        for (int i = 0; i < levelReceiveContents.Length; i++)
        {
            levelReceiveContents[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < dungeonInfo.rewardInfos.Count; i++)
        {
            levelReceiveContents[i].gameObject.SetActive(true);

            levelReceiveContents[i].Initialize(dungeonInfo.rewardInfos[i].rewardType, dungeonInfo.rewardInfos[i].number +
                (dungeonInfo.rewardInfos[i].addNumber * nowLevel));
        }
    }

    public void LeftButton()
    {
        if (nowLevel == 0) return;

        if(nowLevel > 0)
        {
            nowLevel -= 1;
        }

        LevelInitialize(nowLevel);
    }

    public void RightButton()
    {
        if (nowLevel == highLevel) return;

        if(nowLevel < highLevel)
        {
            nowLevel += 1;
        }

        LevelInitialize(nowLevel);
    }

    public void GameStart()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                if (playerDataBase.DungeonKey1 <= 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                    return;
                }
                break;
            case DungeonType.Dungeon2:
                if (playerDataBase.DungeonKey2 <= 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                    return;
                }
                break;
            case DungeonType.Dungeon3:
                if (playerDataBase.DungeonKey3 <= 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                    return;
                }
                break;
            case DungeonType.Dungeon4:
                if (playerDataBase.DungeonKey4 <= 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                    return;
                }
                break;
        }

        Debug.LogError(dungeonType + " 시작");

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                playerDataBase.Dungeon1Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon1Count", playerDataBase.Dungeon1Count);

                plusNumber1 = dungeonInfo.rewardInfos[0].addNumber * nowLevel;
                plusNumber2 = dungeonInfo.rewardInfos[1].addNumber * nowLevel;
                break;
            case DungeonType.Dungeon2:
                playerDataBase.Dungeon2Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon2Count", playerDataBase.Dungeon2Count);

                plusNumber1 = dungeonInfo.rewardInfos[0].addNumber * nowLevel;
                plusNumber2 = dungeonInfo.rewardInfos[1].addNumber * nowLevel;
                break;
            case DungeonType.Dungeon3:
                playerDataBase.Dungeon3Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon3Count", playerDataBase.Dungeon3Count);

                plusNumber1 = dungeonInfo.rewardInfos[0].addNumber * nowLevel;
                plusNumber2 = dungeonInfo.rewardInfos[1].addNumber * nowLevel;
                break;
            case DungeonType.Dungeon4:
                playerDataBase.Dungeon4Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon4Count", playerDataBase.Dungeon4Count);

                plusNumber1 = dungeonInfo.rewardInfos[0].addNumber * nowLevel;
                plusNumber2 = dungeonInfo.rewardInfos[1].addNumber * nowLevel;
                break;
        }

        FirebaseAnalytics.LogEvent("Count_Dungeon : " + dungeonType.ToString());

        StartCoroutine(EnterDungeonCoroution());
    }

    IEnumerator EnterDungeonCoroution()
    {
        fadeInOut.canvasGroup.gameObject.SetActive(true);
        fadeInOut.FadeOut();

        yield return waitForSeconds;

        SoundManager.instance.PlayBoss();

        dungeonLevelView.SetActive(false);

        OpenDungeonView();
        changeFoodManager.CloseChangeFoodView();

        GameManager.instance.GameStart_Dungeon();
        Initialize();
        CheckPercent();

        NotionManager.instance.UseNotion2(NotionType.StartDungeon);
        SoundManager.instance.PlaySFX(GameSfxType.CleanKitchenStart);
    }

    public void CheckPercent()
    {
        attackPlus = 0;
        attackSpeed = 0;
        attackX2 = 0;

        attackPlus += characterDataBase.GetCharacterEffect(playerDataBase.GetCharacterHighNumber());
        attackPlus += playerDataBase.Skill7 * 0.1f;
        attackPlus += levelDataBase.GetLevel(playerDataBase.Exp) * 0.05f;
        attackPlus += playerDataBase.Treasure1 * 0.2f;
        attackPlus += playerDataBase.Advancement * 0.1f;

        success += attackPlus;

        if(success > 100)
        {
            success = 100;
        }

        attackSpeed += butterflyDataBase.GetButterflyEffect(playerDataBase.GetButterflyHighNumber());
        attackSpeed += playerDataBase.Skill9 * 0.05f;
        attackSpeed += playerDataBase.Skill15 * 0.5f;
        attackSpeed += playerDataBase.Treasure2 * 0.1f;
        attackSpeed += playerDataBase.Advancement * 0.05f;

        attackX2 += totemsDataBase.GetTotemsEffect(playerDataBase.GetTotemsHighNumber());
        attackX2 += playerDataBase.Treasure3 * 0.2f;
        attackX2 += playerDataBase.Skill16 * 0.3f;

        attackText.localizationName = "AttackPercent";
        attackText.plusText = " : " + success.ToString("N1") + "%";
        attackText.plusText += " (+" + attackPlus.ToString("N1") + "%)";

        attackDelay = 0.4f - (0.4f * (attackSpeed * 0.01f));

        if(attackDelay < 0.1f)
        {
            attackDelay = 0.1f;
        }

        attackSpeedText.localizationName = "AttackSpeedPercent";
        attackSpeedText.plusText = " : " + attackDelay.ToString("N2");

        if (attackSpeed > 0)
        {
            attackSpeedText.plusText += " (-" + attackSpeed.ToString("N2") + "%)";
        }

        attackX2Text.localizationName = "AttackX2Percent";
        attackX2Text.plusText = " : " + attackX2.ToString("N1") + "%";

        attackText.ReLoad();
        attackSpeedText.ReLoad();
        attackX2Text.ReLoad();
    }

    void Initialize()
    {
        for(int i = 0; i < bossFoodContents.Length; i ++)
        {
            bossFoodContents[i].gameObject.SetActive(false);
        }

        bossFoodContents[(int)dungeonType].gameObject.SetActive(true);
        bossFoodContents[(int)dungeonType].Initialize();

        titleText.localizationName = dungeonInfo.dungeonType.ToString() + "Title";
        titleText.plusText = "  Lv." + (nowLevel + 1);
        titleText.ReLoad();

        success = dungeonInfo.percent;
        health = dungeonInfo.health;
        health += nowLevel * (health / 10);

        saveHealth = health;

        healthText.text = health + "/" + saveHealth;
        healthFillamount.fillAmount = health * 1.0f / saveHealth * 1.0f;

        timer = dungeonInfo.timer + 1;
        saveTimer = timer;

        clear = false;

        StartCoroutine(InGameTimerCoroution());
    }

    IEnumerator InGameTimerCoroution()
    {
        if(clear)
        {
            yield break;
        }

        if(timer < 1)
        {
            Debug.Log("던전 타임 오버");

            GameOver();
            yield break;
        }
        else
        {
            timer -= 1;
        }

        inGameTimerText.localizationName = "TimeLeft";
        inGameTimerText.plusText = " : " + timer.ToString();
        inGameTimerText.ReLoad();

        timerFillamount.fillAmount = timer * 1.0f / saveTimer * 1.0f;

        yield return waitForSeconds;

        StartCoroutine(InGameTimerCoroution());
    }

    public void AttackButton()
    {
        if (clear) return;

        if (delay) return;

        if (Random.Range(0, 100f) >= 100 - success)
        {
            if(Random.Range(0, 100f) < attackX2)
            {
                health -= 2;

                SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                NotionManager.instance.UseNotion2(NotionType.SuccessAttackX2);
            }
            else
            {
                health -= 1;

                SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                NotionManager.instance.UseNotion2(NotionType.SuccessAttack);
            }

            Damage();
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.UpgradeFail);
            NotionManager.instance.UseNotion2(NotionType.FailAttack);
        }
    }

    void Damage()
    {
        if (health <= 0)
        {
            health = 0;

            Debug.Log("던전 성공!");
            StartCoroutine(SuccessCoroution());
        }

        healthText.text = health + "/" + saveHealth;
        healthPercent = health * 1.0f / saveHealth * 1.0f;
        healthFillamount.fillAmount = healthPercent;

        bossFoodContents[(int)dungeonType].SetSize(healthPercent);

        delay = true;
        Invoke("Delay", attackDelay);
    }

    void Delay()
    {
        delay = false;
    }

    IEnumerator SuccessCoroution()
    {
        clear = true;

        FirebaseAnalytics.LogEvent("Clear_Dungeon : " + dungeonType.ToString());

        bossFoodContents[(int)dungeonType].gameObject.SetActive(false);

        if (GameStateManager.instance.Effect)
        {
            successParticle.gameObject.SetActive(false);
            successParticle.gameObject.SetActive(true);
            successParticle.Play();
        }

        SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);

        yield return waitForSeconds;

        SoundManager.instance.PlaySFX(GameSfxType.CleanKitchenEnd);
        NotionManager.instance.UseNotion2(NotionType.SuccessSleepFood);

        yield return waitForSeconds;

        GameClear();
    }

    void GameClear()
    {
        dungeonClearView.SetActive(true);

        for (int i = 0; i < clearReceiveContents.Length; i++)
        {
            clearReceiveContents[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < dungeonInfo.rewardInfos.Count; i++)
        {
            clearReceiveContents[i].gameObject.SetActive(true);

            clearReceiveContents[i].Initialize(dungeonInfo.rewardInfos[i].rewardType, dungeonInfo.rewardInfos[i].number +
                (dungeonInfo.rewardInfos[i].addNumber * nowLevel));
        }

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                playerDataBase.DungeonKey1 -= 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey1", playerDataBase.DungeonKey1);

                if(playerDataBase.Dungeon1Level < nowLevel + 1)
                {
                    playerDataBase.Dungeon1Level += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon1Level", playerDataBase.Dungeon1Level);
                }
                break;
            case DungeonType.Dungeon2:
                playerDataBase.DungeonKey2 -= 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey2", playerDataBase.DungeonKey2);

                if (playerDataBase.Dungeon2Level < nowLevel + 1)
                {
                    playerDataBase.Dungeon2Level += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon2Level", playerDataBase.Dungeon2Level);
                }
                break;
            case DungeonType.Dungeon3:
                playerDataBase.DungeonKey3 -= 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey3", playerDataBase.DungeonKey3);

                if (playerDataBase.Dungeon3Level < nowLevel + 1)
                {
                    playerDataBase.Dungeon3Level += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon3Level", playerDataBase.Dungeon3Level);
                }
                break;
            case DungeonType.Dungeon4:
                playerDataBase.DungeonKey4 -= 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey4", playerDataBase.DungeonKey4);

                if (playerDataBase.Dungeon4Level < nowLevel + 1)
                {
                    playerDataBase.Dungeon4Level += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon4Level", playerDataBase.Dungeon4Level);
                }
                break;
        }
    }

    public void ReceiveButton()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        dungeonClearView.SetActive(false);

        for (int i = 0; i < dungeonInfo.rewardInfos.Count; i++)
        {
            switch(i)
            {
                case 0:
                    plusNumber = plusNumber1;
                    break;
                case 1:
                    plusNumber = plusNumber2;
                    break;
            }

            switch (dungeonInfo.rewardInfos[i].rewardType)
            {
                case RewardType.Gold:
                    PlayfabManager.instance.UpdateAddGold(dungeonInfo.rewardInfos[i].number + plusNumber);
                    break;
                case RewardType.DefDestroyTicket:
                    PortionManager.instance.GetDefTickets(dungeonInfo.rewardInfos[i].number + plusNumber);
                    break;
                case RewardType.Portion1:
                    break;
                case RewardType.Portion2:
                    break;
                case RewardType.Portion3:
                    break;
                case RewardType.Portion4:
                    break;
                case RewardType.PortionSet:
                    break;
                case RewardType.Crystal:
                    PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal,dungeonInfo.rewardInfos[i].number + plusNumber);
                    break;
                case RewardType.Exp:
                    PortionManager.instance.GetExp(dungeonInfo.rewardInfos[i].number + plusNumber);
                    break;
                case RewardType.Treasure1:
                    break;
                case RewardType.Treasure2:
                    break;
                case RewardType.Treasure3:
                    break;
                case RewardType.Treasure4:
                    break;
                case RewardType.Treasure5:
                    break;
                case RewardType.Treasure6:
                    break;
                case RewardType.Portion5:
                    break;
                case RewardType.Treasure7:
                    break;
                case RewardType.Treasure8:
                    break;
                case RewardType.Treasure9:
                    break;
                case RewardType.TreasureBox:
                    break;
                case RewardType.DefDestroyTicketPiece:
                    break;
                case RewardType.BuffTicket:
                    break;
                case RewardType.Portion6:
                    break;
                case RewardType.SkillTicket:
                    break;
                case RewardType.Treasure10:
                    break;
                case RewardType.Treasure11:
                    break;
                case RewardType.Treasure12:
                    break;
                case RewardType.Gold2:
                    break;
                case RewardType.Gold3:
                    break;
                case RewardType.RankPoint:
                    break;
                case RewardType.RepairTicket:
                    break;
                case RewardType.RemoveAds:
                    break;
                case RewardType.GoldX2:
                    break;
                case RewardType.AutoUpgrade:
                    break;
                case RewardType.AutoPresent:
                    break;
                case RewardType.Island1_Heart:
                    break;
                case RewardType.Island2_Heart:
                    break;
                case RewardType.Island3_Heart:
                    break;
                case RewardType.Island4_Heart:
                    break;
                case RewardType.SpeicalCharacter:
                    break;
                case RewardType.AbilityPoint:
                    PortionManager.instance.GetAbilityPoint(dungeonInfo.rewardInfos[i].number + plusNumber);
                    break;
                case RewardType.DungeonKey1:
                    break;
                case RewardType.DungeonKey2:
                    break;
                case RewardType.DungeonKey3:
                    break;
                case RewardType.DungeonKey4:
                    break;
                case RewardType.Icon_Ranking1:
                    break;
                case RewardType.Icon_Ranking2:
                    break;
                case RewardType.Icon_Ranking3:
                    break;
                case RewardType.Icon_Ranking4:
                    break;
                case RewardType.SliverBox:
                    break;
                case RewardType.GoldBox:
                    break;
                case RewardType.EventTicket:
                    break;
            }
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        GameOver();
    }

    void GameOver()
    {
        SoundManager.instance.StopBoss();
        GameManager.instance.GameStop_Dungeon();

        OpenDungeonView();
    }

    public void HomeButton()
    {
        if (clear) return;

        SoundManager.instance.StopBoss();
        GameManager.instance.GameStop_Dungeon();

        OpenDungeonView();
    }

    public void OpenHelpView()
    {
        if(!dungeonInfoView.activeInHierarchy)
        {
            dungeonInfoView.SetActive(true);
        }
        else
        {
            dungeonInfoView.SetActive(false);
        }
    }

    public void OpenHelp2View()
    {
        if (!dungeonInfo2View.activeInHierarchy)
        {
            dungeonInfo2View.SetActive(true);
        }
        else
        {
            dungeonInfo2View.SetActive(false);
        }
    }

    public void CloseDungeonLevelView()
    {
        dungeonLevelView.SetActive(false);
    }

    public void SuccessWatchAd(int number)
    {
        switch(number)
        {
            case 0:
                playerDataBase.DailyDungeonKey1 += 1;
                playerDataBase.DungeonKey1 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyDungeonKey1", playerDataBase.DailyDungeonKey1);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey1", playerDataBase.DungeonKey1);
                break;
            case 1:
                playerDataBase.DailyDungeonKey2 += 1;
                playerDataBase.DungeonKey2 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyDungeonKey2", playerDataBase.DailyDungeonKey2);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey2", playerDataBase.DungeonKey2);
                break;
            case 2:
                playerDataBase.DailyDungeonKey3 += 1;
                playerDataBase.DungeonKey3 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyDungeonKey3", playerDataBase.DailyDungeonKey3);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey3", playerDataBase.DungeonKey3);
                break;
            case 3:
                playerDataBase.DailyDungeonKey4 += 1;
                playerDataBase.DungeonKey4 += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyDungeonKey4", playerDataBase.DailyDungeonKey4);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey4", playerDataBase.DungeonKey4);
                break;
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessWatchAd);

        ContentInitialize();
    }
}
