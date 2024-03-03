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

    public GameObject dungeonClearView;
    public ReceiveContent[] clearReceiveContents;

    public RectTransform rectTransform;

    public GameObject alarm;
    public GameObject ingameAlarm;

    public DungeonContent[] dungeonContents;

    public ReceiveContent[] receiveContents;

    public BossFoodContent[] bossFoodContents;

    public FadeInOut fadeInOut;

    public ChangeFoodManager changeFoodManager;

    [Space]
    [Title("Text")]
    public Text gourmetPointText;
    public Text timerText;

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

    DungeonInfo dungeonInfo = new DungeonInfo();


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

            alarm.SetActive(false);
            ingameAlarm.SetActive(false);

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

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
        Debug.LogError(type + " 입장");

        SoundManager.instance.PlayBoss();

        dungeonType = type;

        dungeonInfo = dungeonDataBase.GetDungeonInfo(type);

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                playerDataBase.Dungeon1Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon1Count", playerDataBase.Dungeon1Count);

                plusNumber1 = dungeonInfo.rewardInfos[0].addNumber * playerDataBase.Dungeon1Level;
                plusNumber2 = dungeonInfo.rewardInfos[1].addNumber * playerDataBase.Dungeon1Level;
                break;
            case DungeonType.Dungeon2:
                playerDataBase.Dungeon2Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon2Count", playerDataBase.Dungeon2Count);

                plusNumber1 = dungeonInfo.rewardInfos[0].addNumber * playerDataBase.Dungeon2Level;
                plusNumber2 = dungeonInfo.rewardInfos[1].addNumber * playerDataBase.Dungeon2Level;
                break;
            case DungeonType.Dungeon3:
                playerDataBase.Dungeon3Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon3Count", playerDataBase.Dungeon3Count);

                plusNumber1 = dungeonInfo.rewardInfos[0].addNumber * playerDataBase.Dungeon3Level;
                plusNumber2 = dungeonInfo.rewardInfos[1].addNumber * playerDataBase.Dungeon3Level;
                break;
            case DungeonType.Dungeon4:
                playerDataBase.Dungeon4Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon4Count", playerDataBase.Dungeon4Count);

                plusNumber1 = dungeonInfo.rewardInfos[0].addNumber * playerDataBase.Dungeon4Level;
                plusNumber2 = dungeonInfo.rewardInfos[1].addNumber * playerDataBase.Dungeon4Level;
                break;
        }

        FirebaseAnalytics.LogEvent(dungeonType.ToString());

        StartCoroutine(EnterDungeonCoroution());
    }

    IEnumerator EnterDungeonCoroution()
    {
        fadeInOut.canvasGroup.gameObject.SetActive(true);
        fadeInOut.FadeOut();

        yield return waitForSeconds;

        OpenDungeonView();
        changeFoodManager.CloseChangeFoodView();

        GameManager.instance.GameStart_Dungeon();
        Initialize();
        CheckPercent();
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

        attackSpeed += butterflyDataBase.GetButterflyEffect(playerDataBase.GetButterflyHighNumber());
        attackSpeed += playerDataBase.Skill9 * 0.05f;
        attackSpeed += playerDataBase.Treasure2 * 0.1f;
        attackSpeed += playerDataBase.Advancement * 0.05f;
        attackSpeed *= 10;

        attackX2 += totemsDataBase.GetTotemsEffect(playerDataBase.GetTotemsHighNumber());
        attackX2 += playerDataBase.Treasure3 * 0.2f;

        attackText.localizationName = "AttackPercent";
        attackText.plusText = " : " + success.ToString("N1") + "%";
        attackText.plusText += " (+" + attackPlus.ToString("N1") + "%)";

        attackDelay = 0.4f - (0.4f * (attackSpeed * 0.01f));

        attackSpeedText.localizationName = "AttackSpeedPercent";
        attackSpeedText.plusText = " : " + attackDelay.ToString("N2");
        attackSpeedText.plusText += " (-" + attackSpeed.ToString("N2") + "%)";

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
        titleText.ReLoad();

        success = dungeonInfo.percent;
        health = dungeonInfo.health;

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                titleText.plusText = "  Lv." + (playerDataBase.Dungeon1Level + 1);

                health += (playerDataBase.Dungeon1Level * (health / 10));

                break;
            case DungeonType.Dungeon2:
                titleText.plusText = "  Lv." + (playerDataBase.Dungeon2Level + 1);
                break;
            case DungeonType.Dungeon3:
                titleText.plusText = "  Lv." + (playerDataBase.Dungeon3Level + 1);
                break;
            case DungeonType.Dungeon4:
                titleText.plusText = "  Lv." + (playerDataBase.Dungeon4Level + 1);
                break;
        }

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
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (clear) return;

        if (delay) return;


        if (Random.Range(0, 100f) >= 100 - success)
        {
            if(Random.Range(0, 100f) < attackX2)
            {
                health -= 2;

                SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                NotionManager.instance.UseNotion(NotionType.SuccessAttackX2);
            }
            else
            {
                health -= 1;

                SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                NotionManager.instance.UseNotion(NotionType.SuccessAttack);
            }

            Damage();
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.UpgradeFail);
            NotionManager.instance.UseNotion(NotionType.FailAttack);
        }
    }

    void Damage()
    {
        healthText.text = health + "/" + saveHealth;

        healthPercent = health * 1.0f / saveHealth * 1.0f;

        healthFillamount.fillAmount = healthPercent;

        bossFoodContents[(int)dungeonType].SetSize(healthPercent);

        if(health == 0)
        {
            Debug.Log("던전 성공!");

            StartCoroutine(SuccessCoroution());
        }

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

        FirebaseAnalytics.LogEvent(dungeonType.ToString() + "_Clear");

        bossFoodContents[(int)dungeonType].gameObject.SetActive(false);

        if (GameStateManager.instance.Effect)
        {
            successParticle.gameObject.SetActive(false);
            successParticle.gameObject.SetActive(true);
            successParticle.Play();
        }

        yield return waitForSeconds;

        SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);
        NotionManager.instance.UseNotion(NotionType.SuccessSleepFood); //폭주 음식을 잠재우는데 성공했습니다!

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

            switch (dungeonType)
            {
                case DungeonType.Dungeon1:
                    clearReceiveContents[i].Initialize(dungeonInfo.rewardInfos[i].rewardType, dungeonInfo.rewardInfos[i].number +
                        (dungeonInfo.rewardInfos[i].addNumber * playerDataBase.Dungeon1Level));
                    break;
                case DungeonType.Dungeon2:
                    clearReceiveContents[i].Initialize(dungeonInfo.rewardInfos[i].rewardType, dungeonInfo.rewardInfos[i].number +
                        (dungeonInfo.rewardInfos[i].addNumber * playerDataBase.Dungeon2Level));
                    break;
                case DungeonType.Dungeon3:
                    clearReceiveContents[i].Initialize(dungeonInfo.rewardInfos[i].rewardType, dungeonInfo.rewardInfos[i].number +
                        (dungeonInfo.rewardInfos[i].addNumber * playerDataBase.Dungeon3Level));
                    break;
                case DungeonType.Dungeon4:
                    clearReceiveContents[i].Initialize(dungeonInfo.rewardInfos[i].rewardType, dungeonInfo.rewardInfos[i].number +
                        (dungeonInfo.rewardInfos[i].addNumber * playerDataBase.Dungeon4Level));
                    break;
            }
        }

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                playerDataBase.DungeonKey1 -= 1;
                playerDataBase.Dungeon1Level += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey1", playerDataBase.DungeonKey1);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon1Level", playerDataBase.Dungeon1Level);
                break;
            case DungeonType.Dungeon2:
                playerDataBase.DungeonKey2 -= 1;
                playerDataBase.Dungeon2Level += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey2", playerDataBase.DungeonKey2);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon2Level", playerDataBase.Dungeon2Level);
                break;
            case DungeonType.Dungeon3:
                playerDataBase.DungeonKey3 -= 1;
                playerDataBase.Dungeon3Level += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey3", playerDataBase.DungeonKey3);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon3Level", playerDataBase.Dungeon3Level);
                break;
            case DungeonType.Dungeon4:
                playerDataBase.DungeonKey4 -= 1;
                playerDataBase.Dungeon4Level += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey4", playerDataBase.DungeonKey4);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon4Level", playerDataBase.Dungeon4Level);
                break;
        }
    }

    public void ReceiveButton()
    {
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
                    PortionManager.instance.GetAbilityPoint(dungeonInfo.rewardInfos[i].number + plusNumber);
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
}
