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

        dungeonContents[0].Initialize(this, DungeonType.Dungeon1, RewardType.Gold, RewardType.AbilityPoint, RewardType.SliverBox, ItemType.DungeonKey1, 0);
        dungeonContents[1].Initialize(this, DungeonType.Dungeon2, RewardType.Exp, RewardType.AbilityPoint, RewardType.SliverBox, ItemType.DungeonKey2, 50000);
        dungeonContents[2].Initialize(this, DungeonType.Dungeon3, RewardType.Crystal, RewardType.AbilityPoint, RewardType.GoldBox, ItemType.DungeonKey3, 250000);
        dungeonContents[3].Initialize(this, DungeonType.Dungeon4, RewardType.TreasureBox, RewardType.AbilityPoint, RewardType.GoldBox, ItemType.DungeonKey4, 500000);

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

        dungeonType = type;

        dungeonInfo = dungeonDataBase.GetDungeonInfo(type);

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                playerDataBase.Dungeon1Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon1Count", playerDataBase.Dungeon1Count);
                break;
            case DungeonType.Dungeon2:
                playerDataBase.Dungeon2Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon2Count", playerDataBase.Dungeon2Count);
                break;
            case DungeonType.Dungeon3:
                playerDataBase.Dungeon3Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon3Count", playerDataBase.Dungeon3Count);
                break;
            case DungeonType.Dungeon4:
                playerDataBase.Dungeon4Count += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dungeon4Count", playerDataBase.Dungeon4Count);
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
        attackX2 += playerDataBase.Treasure3 * 0.15f;

        attackText.localizationName = "AttackPercent";
        attackText.plusText = " : " + success.ToString("N1") + "%";
        attackText.plusText += " (+" + attackPlus.ToString("N1") + "%)";

        attackDelay = 0.4f - (0.4f * (attackSpeed * 0.01f));

        attackSpeedText.localizationName = "AttackSpeedPercent";
        attackSpeedText.plusText = " : " + attackDelay.ToString("N2");
        attackSpeedText.plusText += " (-" + attackSpeed.ToString("N2") + "%)";

        attackX2Text.localizationName = "AttackX2Percent";
        attackX2Text.plusText = " : " + attackX2.ToString("N2") + "%";

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

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                titleText.plusText = " Lv." + (playerDataBase.Dungeon1Level + 1);
                break;
            case DungeonType.Dungeon2:
                titleText.plusText = " Lv." + (playerDataBase.Dungeon2Level + 1);
                break;
            case DungeonType.Dungeon3:
                titleText.plusText = " Lv." + (playerDataBase.Dungeon3Level + 1);
                break;
            case DungeonType.Dungeon4:
                titleText.plusText = " Lv." + (playerDataBase.Dungeon4Level + 1);
                break;
        }

        titleText.ReLoad();

        success = dungeonInfo.percent;

        health = dungeonInfo.health;
        saveHealth = health;

        healthText.text = health + "/" + saveHealth;
        healthFillamount.fillAmount = health * 1.0f / saveHealth * 1.0f;

        timer = dungeonInfo.timer;
        saveTimer = timer;

        clear = false;

        StartCoroutine(InGameTimerCoroution());
    }

    IEnumerator InGameTimerCoroution()
    {
        if(timer < 0)
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

        switch (dungeonType)
        {
            case DungeonType.Dungeon1:
                playerDataBase.DungeonKey1 -= 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey1", playerDataBase.DungeonKey1);
                break;
            case DungeonType.Dungeon2:
                playerDataBase.DungeonKey2 -= 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey2", playerDataBase.DungeonKey2);
                break;
            case DungeonType.Dungeon3:
                playerDataBase.DungeonKey3 -= 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey3", playerDataBase.DungeonKey3);
                break;
            case DungeonType.Dungeon4:
                playerDataBase.DungeonKey4 -= 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey4", playerDataBase.DungeonKey4);
                break;
        }

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

        //보상 창 띄우기
    }

    void GameOver()
    {
        //타임 오버! 재도전, 그만하기 창 띄우기
    }

    public void HomeButton()
    {
        if (clear) return;

        //나가시겠습니까? 창 띄우기
    }

    public void OpenHelpView() //도움말 창 띄우기
    {

    }
}
