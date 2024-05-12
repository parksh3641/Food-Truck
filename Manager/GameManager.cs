using Firebase.Analytics;
#if UNITY_ANDROID
using Google.Play.AppUpdate;
using Google.Play.Common;
#endif
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject checkUpdate;
    public GameObject rankingNoticeView;
    public GameObject rankingNoticeButton;

    public CameraController cameraController;

    public Text goldText;
    public Text crystalText;
    public Text signText;

    public GameObject goldx2;
    public GameObject removeAds;
    public GameObject superOffline;
    public GameObject autoUpgrade;
    public GameObject autoPresent;
    public GameObject superExp;
    public GameObject superKitchen;

    public GameObject privacypolicyView;
    public GameObject genderView;

    public Sprite[] buttonImg;
    public Image[] genderButton;

    public GameObject coupon;
    public GameObject deleteAccount;
    public GameObject notice;

    public GameObject changeFoodAlarmObj;

    public Image changeFoodImg;
    public Image islandImg;

    public GameObject portion6Obj;

    public LocalizationContent bestRankLevelText;

    public GameObject rankLocked;
    public GameObject inGameRankLocked;
    public GameObject treasureLocked;

    public LocalizationContent changeModeText;

    public ButtonClickAnimation upgradeButtonAnim;
    public ButtonClickAnimation sellButtonAnim;
    public GameObject notUpgrade;
    public GameObject notSell;

    public GameObject testModeButton;
    public Text testModeText;
    public GameObject testMode;

    public GameObject homeButton;

    public GameObject moveArrow1;
    public GameObject moveArrow2;
    public GameObject moveArrow3;

    public GameObject rareFood;
    public GameObject gifticonEvent;

    private bool gifticon = false;

    [Space]
    [Title("Buff")]
    public Text buff1Text;
    public Text buff2Text;
    public Text buff3Text;

    public Image[] buffImage;
    public ButtonScaleAnimation[] buffScaleAnim;

    Color buffGrayColor = new Color(200 / 255f, 200 / 255f, 200 / 255f);
    Color buff1Color = new Color(255 / 255f, 211 / 255f, 6 / 255f);
    Color buff2Color = new Color(28 / 255f, 197 / 255f, 250 / 255f);
    Color buff3Color = new Color(255 / 255f, 50 / 255f, 255 / 255f);
    Color buff4Color = new Color(0 / 255f, 230 / 255f, 0 / 255f);

    private int buff1Value = 100;
    private int buff2Value = 20;
    private int buff3Value = 40;

    private bool buff1 = false;
    private bool buff2 = false;
    private bool buff3 = false;
    private bool buff4 = false;

    [Space]
    [Title("Sauce_Ad")]
    public GameObject portionAdView;
    public ReceiveContent portionReceiveContent;

    private int portionAd = 0;

    [Space]
    [Title("Background Particle")]
    public GameObject christmasSnow;
    public GameObject[] island_Particle;

    [Space]
    [Title("Truck")]
    public Animator[] mainTruckArray;

    [Space]
    [Title("Food")]
    public FoodContent[] foodArray;
    public FoodContent[] rankFoodArray;

    [Space]
    private List<FoodContent> foodArrayList = new List<FoodContent>();

    [Space]
    [Title("Animal")]
    public Animator[] truckAnimator;
    public Animator[] characterAnimator;
    public Animator[] animalAnimator;
    public Animator[] butterflyAnimator;

    [Space]
    [Title("UI")]
    public GameObject island;
    public GameObject dungeon;

    public GameObject mainUI;
    public GameObject inGameUI;
    public GameObject dungeonUI;

    public LocalizationContent todayGoldText;
    public LocalizationContent yesterdayGoldText;

    public Text myMoneyPlusText;

    public Text portionText1, portionText2, portionText3, portionText4, portionText5, portionText6;
    public Image portionFillamount1, portionFillamount2, portionFillamount3, portionFillamount4, portionFillamount5, portionFillamount6;
    public GameObject portionAd1, portionAd2, portionAd3, portionAd4, portionAd5, portionAd6;
    private bool portion1, portion2, portion3, portion4, portion5, portion6;

    public LocalizationContent titleText;
    public LocalizationContent highLevelText;
    public LocalizationContent needText;
    public LocalizationContent priceText;
    public LocalizationContent successText;
    public LocalizationContent defDestroyText;
    public LocalizationContent successX2Text;
    public LocalizationContent sellPriceTipText;

    public GameObject defTicketObj;
    public Text defTicketText;
    public GameObject checkMark;

    [Space]
    [Title("Fever")]
    public Image feverFillamount;

    public GameObject feverEffect;
    public GameObject backButton;

    public Text feverText;

    private float feverCount = 0;
    private float feverMaxCount = 30000;
    private float feverCountPlus = 100;

    private float feverTime = 0;
    private float feverPlus = 15;

    private float destoryPercent = 0; //파괴될 확률
    private float defDestroy = 0;
    private float defDestroyPlus = 100;

    private int needPrice = 0;
    private float needPricePlus = 0;

    private int sellPrice = 0;
    private float sellPricePlus = 0;

    private float sellPriceTip = 0;

    private int expUp = 0;
    private float expUpPlus = 0;

    private float success = 0;
    private float successPlus = 0;

    private float successX2 = 0;
    private float successX3 = 0;

    private int goldPerSecond = 0;

    private float speicalFoodCount = 0;
    private float speicalFoodNeedCount = 3;

    private float portion1TimePlus = 0;
    private float portion2TimePlus = 0;
    private float portion3TimePlus = 0;
    private float portion5TimePlus = 0;
    private float portion6TimePlus = 0;

    private float portion1Time = 0;
    private float portion2Time = 0;
    private float portion3Time = 0;
    private float portion4Plus = 0;
    private float portion5Time = 0;
    private float portion6Time = 0;

    private int portion1Value = 40;
    private int portion2Value = 30;
    private int portion3Value = 10;
    private int portion4Value = 50;
    private int portion5Value = 15;
    private int portion6Value = 0;

    float currentTime, currentTime1, currentTime2, currentTime3, currentTime4, currentTime5, currentTime6;
    float fillAmount, fillAmount1, fillAmount2, fillAmount3, fillAmount4, fillAmount5, fillAmount6;
    public ButtonScaleAnimation[] portionScaleAnim;

    private int level = 0;
    private int maxLevel = 0;
    private int recoverLevel = 0;
    private int playTime = 0;

    private int supportCount = 0;
    private int supportMaxCount = 499;

    public bool isDelay_Camera = false;
    private bool isUpgradeDelay = false;
    //public bool isSellDelay = false;
    private bool isDef = false;
    private bool isExp = false;
    private bool feverMode = false;

    private int serverCount = 0;
    private int nowExp = 0;
    private int nowUpgradeCount = 0;
    private int nowSellCount = 0;
    private int defaultNeed = 150;
    private int defaultSellPrice = 2000;
    private int season = 0;
    private int gender = 0;

    private int rankTotalLevel = 0;

    private float rareFoodPercent = 10.0f;
    private float recoverTicketPercent = 10.0f;
    private float eventTicketPercent = 5.0f;
    private float itemDropPercent = 0f;

    private bool clickDelay = false;
    private bool isReady = false;
    private bool auto = false;
    private bool buffAutoUpgrade = false;
    private bool isRareFood = false;
    private bool isWeekend = false;
    private bool isGoldPerSecond = false;

    public GameObject buff4Obj;

    [Space]
    [Title("Bankruptcy")]
    public GameObject bankruptcyView;
    public ReceiveContent bankReceiveContent;

    [Space]
    [Title("Login")]
    public GameObject loginView;
    public GameObject[] loginButtonArray;

    [Space]
    [Title("DeleteAccount")]
    public GameObject deleteAccountView;

    [Space]
    [Title("AppReview")]
    public GameObject appReview;

    public GameObject checkInternet;

    Sprite[] islandArray;

    [Space]
    [Title("Particle")]
    public ParticleSystem[] level1UpParticle;
    public ParticleSystem[] level5UpParticle;
    public ParticleSystem[] levelMaxParticle;
    public ParticleSystem bombAllParticle;
    public ParticleSystem[] bombParticle;
    public ParticleSystem yummyTimeParticle;
    public ParticleSystem[] yummyTime2Particle;
    public ParticleSystem speicalFoodParticle;
    public ParticleSystem[] sellParticle;

    private List<ParticleContent> particleSystemList = new List<ParticleContent>();

    public ShopManager shopManager;
    public TutorialManager tutorialManager;
    public LockManager lockManager;
    public QuestManager questManager;
    public LevelManager levelManager;
    public ChangeFoodManager changeFoodManager;
    public GourmetManager gourmetManager;
    public ChestBoxManager chestBoxManager;
    public RecoverManager recoverManager;
    public GuideMissionManager guideMissionManager;
    public NoticeManager noticeManager;
    public BuffManager buffManager;
    public IslandManager islandManager;

    UpgradeDataBase upgradeDataBase;
    IslandDataBase islandDataBase;

    PlayerDataBase playerDataBase;
    CharacterDataBase characterDataBase;
    TruckDataBase truckDataBase;
    AnimalDataBase animalDataBase;
    ButterflyDataBase butterflyDataBase;
    TotemsDataBase totemsDataBase;
    EtcDataBase etcDataBase;

    ImageDataBase imageDataBase;
    LevelDataBase levelDataBase;

    WaitForSeconds serverSeconds = new WaitForSeconds(2.0f);
    WaitForSeconds timerSeconds = new WaitForSeconds(1.0f);
    WaitForSeconds firstSeconds = new WaitForSeconds(0.5f);
    WaitForSeconds autoUpgradeSecond = new WaitForSeconds(0.4f);
    WaitForSeconds buffUpgradeSecond = new WaitForSeconds(0.5f);
    WaitForSeconds buffUpgradeSecond_Fever = new WaitForSeconds(0.25f);
    WaitForSeconds maxLevelSecond = new WaitForSeconds(0.6f);
    WaitForSeconds maxLevelSecond_Fever = new WaitForSeconds(0.4f);

    private float delay = 0.2f;

    DateTime currentDate = DateTime.Now;
    DateTime decemberStart;
    DateTime decemberEnd;

    private Dictionary<string, string> playerData = new Dictionary<string, string>();


    private void Awake()
    {
        instance = this;

        QualitySettings.SetQualityLevel(3);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (upgradeDataBase == null) upgradeDataBase = Resources.Load("UpgradeDataBase") as UpgradeDataBase;
        if (characterDataBase == null) characterDataBase = Resources.Load("CharacterDataBase") as CharacterDataBase;
        if (truckDataBase == null) truckDataBase = Resources.Load("TruckDataBase") as TruckDataBase;
        if (animalDataBase == null) animalDataBase = Resources.Load("AnimalDataBase") as AnimalDataBase;
        if (butterflyDataBase == null) butterflyDataBase = Resources.Load("ButterflyDataBase") as ButterflyDataBase;
        if (islandDataBase == null) islandDataBase = Resources.Load("IslandDataBase") as IslandDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (levelDataBase == null) levelDataBase = Resources.Load("LevelDataBase") as LevelDataBase;
        if (totemsDataBase == null) totemsDataBase = Resources.Load("TotemsDataBase") as TotemsDataBase;
        if (etcDataBase == null) etcDataBase = Resources.Load("EtcDataBase") as EtcDataBase;

        upgradeDataBase.Initialize();

        islandArray = imageDataBase.GetIslandArray();

        goldText.text = "0";
        crystalText.text = "0";
        signText.text = "0";

        loginView.SetActive(false);
        deleteAccountView.SetActive(false);
        appReview.SetActive(false);
        checkInternet.SetActive(false);
        rankingNoticeView.SetActive(false);
        portionAdView.SetActive(false);

        loginButtonArray[0].SetActive(false);
        loginButtonArray[1].SetActive(false);
        loginButtonArray[2].SetActive(false);

        island.SetActive(true);
        dungeon.SetActive(false);

        mainUI.SetActive(true);
        inGameUI.SetActive(false);
        dungeonUI.SetActive(false);

        isUpgradeDelay = false;
        isDef = false;

        checkMark.SetActive(false);

        feverMode = false;
        feverCount = 0;
        feverFillamount.fillAmount = 0;

        feverEffect.SetActive(false);

        for(int i = 0; i < level1UpParticle.Length; i ++)
        {
            level1UpParticle[i].gameObject.SetActive(false);
            level5UpParticle[i].gameObject.SetActive(false);
            levelMaxParticle[i].gameObject.SetActive(false);
            bombParticle[i].gameObject.SetActive(false);
            yummyTime2Particle[i].gameObject.SetActive(false);
            sellParticle[i].gameObject.SetActive(false);

            particleSystemList.Add(level1UpParticle[i].GetComponent<ParticleContent>());
            particleSystemList.Add(level5UpParticle[i].GetComponent<ParticleContent>());
            particleSystemList.Add(levelMaxParticle[i].GetComponent<ParticleContent>());
            particleSystemList.Add(bombParticle[i].GetComponent<ParticleContent>());
            particleSystemList.Add(yummyTime2Particle[i].GetComponent<ParticleContent>());
            particleSystemList.Add(sellParticle[i].GetComponent<ParticleContent>());
        }

        bombAllParticle.gameObject.SetActive(false);
        yummyTimeParticle.gameObject.SetActive(false);
        speicalFoodParticle.gameObject.SetActive(false);

        particleSystemList.Add(bombAllParticle.GetComponent<ParticleContent>());
        particleSystemList.Add(yummyTimeParticle.GetComponent<ParticleContent>());
        particleSystemList.Add(speicalFoodParticle.GetComponent<ParticleContent>());

        portion1 = false;
        portion2 = false;
        portion3 = false;
        portion4 = false;
        portion5 = false;
        portion6 = false;

        portionText1.text = "0";
        portionText2.text = "0";
        portionText3.text = "0";
        portionText4.text = "0";
        portionText5.text = "0";
        portionText6.text = "0";

        portionFillamount1.fillAmount = 0;
        portionFillamount2.fillAmount = 0;
        portionFillamount3.fillAmount = 0;
        portionFillamount4.fillAmount = 0;
        portionFillamount5.fillAmount = 0;
        portionFillamount6.fillAmount = 0;

        for(int i = 0; i < buffImage.Length; i ++)
        {
            buffImage[i].color = buffGrayColor;
        }

        defTicketObj.SetActive(false);
        bankruptcyView.SetActive(false);
        privacypolicyView.SetActive(false);
        genderView.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(false);
        testMode.SetActive(false);

        checkUpdate.SetActive(false);

        christmasSnow.SetActive(false);

        for(int i = 0; i < island_Particle.Length; i ++)
        {
            island_Particle[i].SetActive(false);
        }

        homeButton.SetActive(true);

        moveArrow1.SetActive(false);
        moveArrow2.SetActive(false);
        moveArrow3.SetActive(false);

        rareFood.SetActive(false);

        decemberStart = new DateTime(currentDate.Year, 12, 1);
        decemberEnd = new DateTime(currentDate.Year, 1, 31);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            GameStateManager.instance.FeverCount = feverCount;
            GameStateManager.instance.SupportCount = supportCount;
        }
        else
        {

        }
    }

    bool IsWeekend()
    {
        DayOfWeek currentDay = DateTime.Now.DayOfWeek;

        return currentDay == DayOfWeek.Saturday || currentDay == DayOfWeek.Sunday;
    }


    private void Start()
    {
        GameStateManager.instance.Pause = false;
        GameStateManager.instance.DestroyCount = 0;

        if(GameStateManager.instance.Region.Length == 0)
        {
            if (Application.systemLanguage == SystemLanguage.Korean)
            {
                GameStateManager.instance.Region = "ko";
            }
            else
            {
                GameStateManager.instance.Region = "en";
            }
        }

        if (GameStateManager.instance.AutoLogin)
        {
            if (!NetworkConnect.instance.CheckConnectInternet())
            {
                checkInternet.SetActive(true);
                return;
            }

            if (!PlayfabManager.instance.isActive)
            {
                PlayfabManager.instance.Login();
            }
        }
        else
        {
            OpenLoginView();
        }

#if !UNITY_EDITOR
            GameStateManager.instance.Developer = false;
            GameStateManager.instance.YoutubeVideo = false;
#endif

        if (!GameStateManager.instance.Privacypolicy)
        {
            privacypolicyView.SetActive(true);
        }

        BackgroundEffect();
    }

    void BackgroundEffect()
    {
        if (GameStateManager.instance.BackgroundEffect)
        {
            if (currentDate >= decemberStart || currentDate <= decemberEnd)
            {
                christmasSnow.SetActive(true);

                Debug.LogError("Snow Particle Play");
            }
            else
            {
                for (int i = 0; i < island_Particle.Length; i++)
                {
                    island_Particle[i].SetActive(false);
                }

                if (GameStateManager.instance.YoutubeVideo) return;

                island_Particle[(int)GameStateManager.instance.IslandType].SetActive(true);
            }
        }
    }

    public void BackgroundEffect(bool check)
    {
        if (GameStateManager.instance.YoutubeVideo) return;

        if (check)
        {
            if (currentDate >= decemberStart || currentDate <= decemberEnd)
            {
                christmasSnow.SetActive(true);
            }
            else
            {
                for (int i = 0; i < island_Particle.Length; i++)
                {
                    island_Particle[i].SetActive(false);
                }

                island_Particle[0].SetActive(true);
            }
        }
        else
        {
            christmasSnow.SetActive(false);

            for (int i = 0; i < island_Particle.Length; i++)
            {
                island_Particle[i].SetActive(false);
            }
        }
    }

    public void SuccessLogin()
    {
        if (!GameStateManager.instance.Gender && playerDataBase.Gender == 0)
        {
            genderView.SetActive(true);
        }

        GameStateManager.instance.ParticleType = ParticleType.Effect1 + Random.Range(0, 4);

        isReady = false;
        isDelay_Camera = true;

        checkInternet.SetActive(false);
        loginView.SetActive(false);

        CheckPurchase();

        foodArrayList.Clear();

        for (int i = 0; i < foodArray.Length; i++)
        {
            foodArrayList.Add(foodArray[i]);
        }

        for (int i = 0; i < rankFoodArray.Length; i++)
        {
            foodArrayList.Add(rankFoodArray[i]);
        }

        buff1Text.text = "+" + buff1Value.ToString() + "%";
        buff2Text.text = "+" + buff2Value.ToString() + "%";
        buff3Text.text = "+" + buff3Value.ToString() + "%";

        islandImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];

        feverText.text = LocalizationManager.instance.GetString("FeverGauge") + "  0%";

        feverCount = GameStateManager.instance.FeverCount;

        isExp = levelManager.CheckMaxLevel();

        nowExp = playerDataBase.Exp;
        nowUpgradeCount = playerDataBase.UpgradeCount;
        nowSellCount = playerDataBase.SellCount;

        CheckFoodLevel();
        CheckFoodState();

        if (playerDataBase.InGameTutorial == 0)
        {
            moveArrow1.SetActive(true);

            tutorialManager.TutorialStart();
        }
        else
        {
            if(!GameStateManager.instance.TodayQuiz)
            {
                GameStateManager.instance.TodayQuiz = true;

                tutorialManager.TodayQuizStart();
            }
        }

        levelManager.Initialize();

        bestRankLevelText.localizationName = "Best";
        bestRankLevelText.plusText = "";

        switch (SeasonManager.instance.CheckSeason_Ranking())
        {
            case -1:
                bestRankLevelText.localizationName = "SeasonWait";
                break;
            case 0:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel;
                break;
            case 1:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_1;
                break;
            case 2:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_2;
                break;
            case 3:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_3;
                break;
            case 4:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_4;
                break;
            case 5:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_5;
                break;
            case 6:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_6;
                break;
            case 7:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_7;
                break;
            case 8:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_8;
                break;
            case 9:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_9;
                break;
            case 10:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_10;
                break;
        }

        bestRankLevelText.ReLoad();

        CheckLocked();

        testModeButton.SetActive(false);

        if (playerDataBase.TestAccount > 0)
        {
            testModeButton.SetActive(true);
            testModeText.text = "Cheat Mode ON";
        }

#if UNITY_EDITOR
        testModeButton.SetActive(true);
#endif

        //changeFoodManager.CheckProficiency();
        questManager.CheckingAlarm();

        playTime = 0;
        StartCoroutine(PlayTimeCoroution());

        StartCoroutine(ServerDelayCoroution());

        if(playerDataBase.Update == 1)
        {
            playerDataBase.Update = 0;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("Update", playerDataBase.Update);

            OnNeedUpdate();
        }

#if UNITY_EDITOR || UNITY_EDITOR_OSX
        CheckGifticon(true);
#else
        PlayfabManager.instance.GetTitleInternalData("Gifticon", CheckGifticon);
#endif

        supportCount = GameStateManager.instance.SupportCount;

        if(GameStateManager.instance.SaveGold > 100)
        {
            PlayfabManager.instance.UpdateAddGold((int)GameStateManager.instance.SaveGold);
        }
        else if(GameStateManager.instance.SaveGold < 0)
        {
            PlayfabManager.instance.UpdateSubtractGold((int)GameStateManager.instance.SaveGold);
        }

        GameStateManager.instance.SaveGold = 0;

        todayGoldText.localizationName = "TodayGold";
        todayGoldText.plusText = " : <color=#FFFF00>" + MoneyUnitString.ToCurrencyString(GameStateManager.instance.TodayGold) + "</color>";
        todayGoldText.ReLoad();

        yesterdayGoldText.localizationName = "YesterdayGold";
        yesterdayGoldText.plusText = " : <color=#FFFF00>" + MoneyUnitString.ToCurrencyString(GameStateManager.instance.YesterdayGold) + "</color>";
        yesterdayGoldText.ReLoad();

        Invoke("ServerDelay", 2.0f);
    }

    void CheckGifticon(bool check)
    {
        gifticon = check;

        gifticonEvent.SetActive(check);

        if(GameStateManager.instance.StoreType == StoreType.OneStore)
        {
            gifticonEvent.SetActive(false);
        }
    }

    void ServerDelay()
    {
        isReady = true;

        PlayfabManager.instance.GetTitleInternalData("Coupon", CheckCoupon);

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Version", int.Parse(Application.version.Replace(".", "")));
    }

    public void CheckPurchase()
    {
        removeAds.SetActive(false);
        goldx2.SetActive(false);
        superOffline.SetActive(false);
        autoUpgrade.SetActive(false);
        autoPresent.SetActive(false);
        superExp.SetActive(false);
        superKitchen.SetActive(false);

        if (playerDataBase.RemoveAds)
        {
            removeAds.SetActive(true);
        }

        if (playerDataBase.GoldX2)
        {
            goldx2.SetActive(true);
        }

        if (playerDataBase.SuperOffline)
        {
            superOffline.SetActive(true);
        }

        if (playerDataBase.AutoUpgrade)
        {
            autoUpgrade.SetActive(true);
        }

        if (playerDataBase.AutoPresent)
        {
            autoPresent.SetActive(true);
        }

        if (playerDataBase.SuperExp)
        {
            superExp.SetActive(true);
        }

        if (playerDataBase.SuperKitchen)
        {
            superKitchen.SetActive(true);
        }
    }

    void CheckFoodLevel()
    {
        if(GameStateManager.instance.GameType == GameType.Story)
        {
            level = GameStateManager.instance.FoodLevel[(int)GameStateManager.instance.FoodType];
        }
        else
        {
            level = GameStateManager.instance.RankFoodLevel[(int)GameStateManager.instance.RankFoodType];
        }
    }

    IEnumerator FirstDelay()
    {
#if UNITY_ANDROID
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OS", 0);
        if (GameStateManager.instance.StoreType == StoreType.OneStore)
        {
            FirebaseAnalytics.LogEvent("OneStore");
        }
        else
        {
            FirebaseAnalytics.LogEvent("Google");
        }
#elif UNITY_IOS
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OS", 1);
        FirebaseAnalytics.LogEvent("Apple");
#else
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OS", 2);
        FirebaseAnalytics.LogEvent("Web");
#endif

        yield return firstSeconds;

        PlayfabManager.instance.GrantItemsToUser("Character1", "Character");
        PlayfabManager.instance.GrantItemsToUser("Truck1", "Truck");
        PlayfabManager.instance.GrantItemsToUser("Animal1", "Animal");

        yield return firstSeconds;

        PlayfabManager.instance.GrantItemsToUser("Butterfly1", "Butterfly");
        PlayfabManager.instance.GrantItemsToUser("Totems1", "Totems");
        PlayfabManager.instance.GrantItemsToUser("Flower1", "Flower");

        yield return firstSeconds;

        PlayfabManager.instance.GrantItemsToUser("Bucket1", "Bucket");
        PlayfabManager.instance.GrantItemsToUser("Chair1", "Chair");
        PlayfabManager.instance.GrantItemsToUser("Tube1", "Tube");

        yield return firstSeconds;

        PlayfabManager.instance.GrantItemsToUser("Surfboard1", "Surfboard");
        PlayfabManager.instance.GrantItemsToUser("Umbrella1", "Umbrella");

        if (Application.systemLanguage == SystemLanguage.Korean)
        {
            PlayfabManager.instance.SetProfileLanguage("ko");

            GameStateManager.instance.Region = "ko";
        }
        else
        {
            PlayfabManager.instance.SetProfileLanguage("en");

            GameStateManager.instance.Region = "en";
        }

        yield return firstSeconds;

        PortionManager.instance.GetAllPortion(3);

        yield return firstSeconds;

        PortionManager.instance.GetPortion(4,3);

        CheckPortion();

        yield return firstSeconds;

        PortionManager.instance.GetRepairTickets(10);

        yield return firstSeconds;

        PortionManager.instance.GetBuffTickets(1);

        yield return firstSeconds;

        playerDataBase.FirstDate = "1" + DateTime.Now.ToString("MMddHHmm");
        playerDataBase.FirstServerDate = "1" + DateTime.Now.AddDays(3).ToString("MMddHHmm");
        playerDataBase.CastleDate = "1" + DateTime.Now.ToString("MMddHHmm");
        playerDataBase.CastleServerDate = "1" + DateTime.Now.AddDays(1).ToString("MMddHHmm");

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("FirstDate", int.Parse("1" + playerDataBase.FirstDate));
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("FirstServerDate", int.Parse("1" + playerDataBase.FirstServerDate));
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("CastleDate", int.Parse("1" + playerDataBase.CastleDate));
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("CastleServerDate", int.Parse("1" + playerDataBase.CastleServerDate));
    }

    IEnumerator PlayTimeCoroution()
    {
        if (playTime >= 60)
        {
            playTime = 0;
            playerDataBase.PlayTime += 1;
            GameStateManager.instance.PlayTime += 1;

            if(serverCount == 0)
            {
                serverCount += 1;
            }
            else
            {
                serverCount = 0;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("PlayTime", playerDataBase.PlayTime);
            }

            if(isWeekend)
            {
                if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
                {
                    isWeekend = false;
                    CheckPercent();
                }
            }

            Debug.LogError("1 Min Passed");
        }
        else
        {
            playTime += 1;
        }

        yield return timerSeconds;
        StartCoroutine(PlayTimeCoroution());
    }

    void CheckCoupon(bool check)
    {
#if UNITY_EDITOR
        coupon.SetActive(true);
        deleteAccount.SetActive(true);
#elif UNITY_ANDROID
        coupon.SetActive(true);
        deleteAccount.SetActive(false);
#else
        if(check)
        {
            coupon.SetActive(true);
            deleteAccount.SetActive(false);
        }
        else
        {
            coupon.SetActive(false);
            deleteAccount.SetActive(true);
            notice.SetActive(false);
            noticeManager.CloseNoticeView();
        }
#endif
    }

    public void CheckInternet()
    {
        if (NetworkConnect.instance.CheckConnectInternet())
        {
            PlayfabManager.instance.Login();
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
        }
    }

    public void RenewalVC()
    {
        playerDataBase.Coin = playerDataBase.CoinA + (playerDataBase.CoinB * 100000000) + playerDataBase.SaveCoin;

        goldText.text = MoneyUnitString.ToCurrencyString(playerDataBase.Coin);
        crystalText.text = MoneyUnitString.ToCurrencyString(playerDataBase.Crystal);
    }

    public void GameStartDelay()
    {
        clickDelay = true;
    }

    public void GameStart(int number)
    {
        if (!isReady) return;
        if (!isDelay_Camera) return;

        GameStateManager.instance.GameType = GameType.Story + number;

        rankingNoticeButton.SetActive(false);

        if (GameStateManager.instance.GameType == GameType.Story)
        {
            mainUI.SetActive(false);
            inGameUI.SetActive(true);

            changeModeText.localizationName = "ChangeRanking";

            ChangeFood(GameStateManager.instance.FoodType);

            FirebaseAnalytics.LogEvent("NormalMode");
        }
        else
        {
            if(SeasonManager.instance.CheckSeason_Ranking() == -1)
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.SeasonWaitNotion);
                return;
            }

            //SoundManager.instance.PlayBoss();

            mainUI.SetActive(false);
            inGameUI.SetActive(true);

            changeModeText.localizationName = "ChangeNormal";

            rankingNoticeButton.SetActive(true);

            ChangeRankFood(GameStateManager.instance.RankFoodType);

            if (!GameStateManager.instance.RankingNotice)
            {
                OpenRankingNoticeView();
                GameStateManager.instance.RankingNotice = true;
            }

            FirebaseAnalytics.LogEvent("RankingMode");
        }

        changeModeText.ReLoad();

        if (playerDataBase.LockTutorial == 1)
        {
            moveArrow2.SetActive(true);
        }

        isDelay_Camera = false;
        cameraController.GoToB();

        season = SeasonManager.instance.CheckSeason();

        guideMissionManager.Initialize();

        lockManager.Localization();

        CheckPercent();

        isDef = false;
        checkMark.SetActive(false);

        CheckFever();
        //CheckDefTicket();
        CheckPortion();
        UpgradeInitialize();

        portion6Obj.SetActive(false);
        //if (playerDataBase.Portion6 > 0 && playerDataBase.LockTutorial > 1)
        //{
        //    portion6Obj.SetActive(true);
        //}

        if(playerDataBase.RemoveAds)
        {
            buffManager.RemoveAdsBuff();

            buffImage[0].color = buff1Color;
            buffImage[1].color = buff2Color;
            buffImage[2].color = buff3Color;

            if (buff4Obj.gameObject.activeInHierarchy)
            {
                OffBuff(3);
                buff4Obj.SetActive(false);
            }
        }

        if(!playerDataBase.AutoUpgrade)
        {
            GameStateManager.instance.AutoUpgrade = false;
        }
        else
        {
            if(buff4Obj.gameObject.activeInHierarchy)
            {
                OffBuff(3);
                buff4Obj.SetActive(false);
            }
        }

        if(!playerDataBase.AutoPresent)
        {
            GameStateManager.instance.AutoPresent = false;
        }

        CheckAuto();

        clickDelay = false;
        Invoke("GameStartDelay", 1.0f);
    }

    public void GameStop()
    {
        if (!isDelay_Camera) return;

        isDelay_Camera = false;
        cameraController.GoToA();

        todayGoldText.localizationName = "TodayGold";
        todayGoldText.plusText = " : <color=#FFFF00>" + MoneyUnitString.ToCurrencyString(GameStateManager.instance.TodayGold) +"</color>";
        todayGoldText.ReLoad();

        yesterdayGoldText.localizationName = "YesterdayGold";
        yesterdayGoldText.plusText = " : <color=#FFFF00>" + MoneyUnitString.ToCurrencyString(GameStateManager.instance.YesterdayGold) + "</color>";
        yesterdayGoldText.ReLoad();

        mainUI.SetActive(true);
        inGameUI.SetActive(false);

        GameStateManager.instance.FeverCount = feverCount;

        bestRankLevelText.localizationName = "Best";
        bestRankLevelText.plusText = "";

        switch (SeasonManager.instance.CheckSeason_Ranking())
        {
            case -1:
                bestRankLevelText.localizationName = "SeasonWait";
                break;
            case 0:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel;
                break;
            case 1:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_1;
                break;
            case 2:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_2;
                break;
            case 3:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_3;
                break;
            case 4:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_4;
                break;
            case 5:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_5;
                break;
            case 6:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_6;
                break;
            case 7:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_7;
                break;
            case 8:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_8;
                break;
            case 9:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_9;
                break;
            case 10:
                bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel_10;
                break;
        }

        bestRankLevelText.ReLoad();

        CheckLocked();

        questManager.CheckingAlarm();
        GourmetManager.instance.Initialize();

#if UNITY_EDITOR || UNITY_EDITOR_OSX
        CheckGifticon(true);
#else
        PlayfabManager.instance.GetTitleInternalData("Gifticon", CheckGifticon);
#endif
    }

    public void GameStart_Dungeon()
    {
        if (!isDelay_Camera) return;

        island.SetActive(false);
        dungeon.SetActive(true);

        mainUI.SetActive(false);
        inGameUI.SetActive(false);
        dungeonUI.SetActive(true);
    }

    public void GameStop_Dungeon()
    {
        if (!isDelay_Camera) return;

        mainUI.SetActive(false);
        inGameUI.SetActive(true);
        dungeonUI.SetActive(false);

        island.SetActive(true);
        dungeon.SetActive(false);
    }

    public void CheckPercent()
    {
        if (!inGameUI.activeInHierarchy) return;

        successPlus = 0;
        successX2 = 0;
        successX3 = 0;
        needPricePlus = 0;
        sellPricePlus = 0;
        sellPriceTip = 0;
        defDestroy = 0;
        expUp = 0;
        expUpPlus = 0;
        destoryPercent = 0;
        itemDropPercent = 0;
        expUp = 0;
        expUpPlus = 0;

        rareFoodPercent = 10.0f;
        recoverTicketPercent = 5.0f;
        eventTicketPercent = 2.0f;

        expUp = 20;
        expUp += (int)animalDataBase.GetAnimalEffect(playerDataBase.GetAnimalHighNumber());
        expUpPlus += playerDataBase.GetAnimal_Total_AbilityLevel() * animalDataBase.retentionValue;
        expUpPlus += playerDataBase.GetEquipValue(EquipType.Equip_Index_14);
        expUp = (int)(expUp + (expUp * (expUpPlus * 0.01f)));

        if (playerDataBase.SuperExp)
        {
            expUp *= 2;
        }

        itemDropPercent += (playerDataBase.Treasure15 * 1f);
        itemDropPercent += etcDataBase.GetBucketEffect(playerDataBase.GetBucketHighNumber());
        itemDropPercent += playerDataBase.GetBucket_Total_AbilityLevel() * etcDataBase.bucketInfoList[0].retentionValue;
        itemDropPercent += playerDataBase.GetEquipValue(EquipType.Equip_Index_7);

        recoverTicketPercent += recoverTicketPercent * (itemDropPercent * 0.01f);
        eventTicketPercent += eventTicketPercent * (itemDropPercent * 0.01f);

        //changeFoodImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];

        successPlus += playerDataBase.Skill7 * 0.5f;
        successPlus += playerDataBase.Skill17 * 0.5f;
        if (playerDataBase.Level > 99)
        {
            successPlus += 50;
        }
        else
        {
            successPlus += playerDataBase.Level * 0.5f;
        }
        successPlus += playerDataBase.Treasure1 * 1f;
        successPlus += playerDataBase.Advancement * 0.5f;
        successPlus += playerDataBase.GetNormalBookNumber() * 0.3f;
        successPlus += playerDataBase.GetEquipValue(EquipType.Equip_Index_1);
        successPlus += characterDataBase.GetCharacterEffect(playerDataBase.GetCharacterHighNumber());
        successPlus += playerDataBase.GetCharacter_Total_AbilityLevel() * characterDataBase.retentionValue;

        if (isRareFood)
        {
            successPlus -= 10;
        }

        if(GameStateManager.instance.GameType == GameType.Rank)
        {
            successPlus += etcDataBase.GetTubeEffect(playerDataBase.GetTubeHighNumber());
            successPlus += playerDataBase.GetTube_Total_AbilityLevel() * etcDataBase.tubeInfoList[0].retentionValue;
        }

        successX2 += playerDataBase.Treasure3 * 0.5f;
        successX2 += playerDataBase.GetEquipValue(EquipType.Equip_Index_3);
        successX2 += playerDataBase.GetTotems_Total_AbilityLevel() * totemsDataBase.retentionValue;
        successX2 += butterflyDataBase.GetButterflyEffect(playerDataBase.GetButterflyHighNumber());
        successX2 += playerDataBase.GetButterfly_Total_AbilityLevel() * butterflyDataBase.retentionValue;

        successX3 += etcDataBase.GetSurfboardEffect(playerDataBase.GetSurfboardHighNumber());
        successX3 += playerDataBase.GetSurfboard_Total_AbilityLevel() * etcDataBase.surfboardInfoList[0].retentionValue;

        sellPricePlus += playerDataBase.Skill8 * 1f;
        sellPricePlus += playerDataBase.Skill18 * 1f;
        sellPricePlus += playerDataBase.Proficiency * 2;
        sellPricePlus += playerDataBase.Treasure7 * 2f;
        sellPricePlus += playerDataBase.Advancement * 1f;
        sellPricePlus += playerDataBase.GetIconHoldNumber() * 0.5f;
        sellPricePlus += truckDataBase.GetTruckEffect(playerDataBase.GetTruckHighNumber());
        sellPricePlus += playerDataBase.GetTruck_Total_AbilityLevel() * truckDataBase.retentionValue;
        sellPricePlus += playerDataBase.GetEquipValue(EquipType.Equip_Index_2);

        if (IsWeekend())
        {
            sellPricePlus += 30;

            isWeekend = true;
        }

        sellPriceTip += 0;
        sellPriceTip += playerDataBase.Skill14 * 0.3f;
        sellPriceTip += playerDataBase.Treasure8 * 0.3f;

        if(sellPriceTip >= 100)
        {
            sellPriceTip = 100;
        }

        destoryPercent = islandDataBase.GetDestroy(GameStateManager.instance.IslandType);

        defDestroy += playerDataBase.Skill9 * 0.25f;
        defDestroy += playerDataBase.Skill19 * 0.25f;
        defDestroy += playerDataBase.Treasure2 * 0.5f;
        defDestroy += playerDataBase.Advancement * 0.25f;
        defDestroy += playerDataBase.GetEpicBookNumber() * 0.5f;
        defDestroy += playerDataBase.GetEquipValue(EquipType.Equip_Index_4);

        needPricePlus += playerDataBase.Skill10 * 0.3f;

        goldPerSecond = (int)totemsDataBase.GetTotemsEffect(playerDataBase.GetTotemsHighNumber());

        if (goldPerSecond > 0)
        {
            if(!isGoldPerSecond)
            {
                isGoldPerSecond = true;
                StartCoroutine(GoldPerSecondCoroution());
            }
        }

        feverTime = 20;
        feverTime += 20 * (0.002f * playerDataBase.Skill1);
        feverTime += 20 * (0.005f * playerDataBase.Treasure9);

        feverCountPlus += feverCountPlus * ((playerDataBase.Skill2 + playerDataBase.GetEquipValue(EquipType.Equip_Index_9)) * 0.01f);
        feverPlus = 15 + (15 * (0.002f * playerDataBase.Skill3));

        portion1TimePlus = playerDataBase.Skill4 + playerDataBase.Treasure6 + playerDataBase.GetEquipValue(EquipType.Equip_Index_8);
        portion2TimePlus = playerDataBase.Skill5 + playerDataBase.Treasure6 + playerDataBase.GetEquipValue(EquipType.Equip_Index_8);
        portion3TimePlus = playerDataBase.Skill6 + playerDataBase.Treasure6 + playerDataBase.GetEquipValue(EquipType.Equip_Index_8);
        portion5TimePlus = playerDataBase.Skill13 + playerDataBase.Treasure6 + playerDataBase.GetEquipValue(EquipType.Equip_Index_8);

        portion1Time = 20 + (20 * portion1TimePlus * 0.01f);
        portion2Time = 20 + (20 * portion2TimePlus * 0.01f);
        portion3Time = 20 + (20 * portion3TimePlus * 0.01f);
        portion4Plus = 0.002f * playerDataBase.Skill12;
        portion5Time = 20 + (20 * portion5TimePlus * 0.01f);

        if (playerDataBase.GoldX2)
        {
            sellPricePlus += 300;
        }

        if (feverMode)
        {
            successPlus += feverPlus;
        }

        if (portion1)
        {
            needPricePlus += portion1Value;
        }

        if (portion2)
        {
            sellPricePlus += portion2Value;
        }

        if (portion3)
        {
            successPlus += portion3Value;
        }

        if (portion5)
        {
            defDestroy += portion5Value;
        }

        if(playerDataBase.RemoveAds)
        {
            sellPricePlus += buff1Value;
            defDestroy += buff2Value;
            successX2 += buff3Value;
        }
        else
        {
            if (buff1)
            {
                sellPricePlus += buff1Value;
            }

            if (buff2)
            {
                defDestroy += buff2Value;
            }

            if (buff3)
            {
                successX2 += buff3Value;
            }
        }

        if(defDestroy >= 100)
        {
            defDestroy = 100;
        }

        if(successX2 >= 100)
        {
            successX2 = 100;
        }

        if(needPricePlus >= 100)
        {
            needPricePlus = 100;
        }

        if(GameStateManager.instance.YoutubeVideo)
        {
            defDestroy = 0;
            successX2 = 0;
        }

        UpgradeInitialize();
    }

    public void CheckAuto()
    {
        if (GameStateManager.instance.AutoUpgrade)
        {
            if (!auto)
            {
                auto = true;
                StartCoroutine(AutoUpgradeCoroution());
            }
        }
        else
        {
            if(buff4 && !buffAutoUpgrade)
            {
                buffAutoUpgrade = true;
                StartCoroutine(BuffAutoUpgradeCoroution());
            }
        }

        if(GameStateManager.instance.AutoPresent)
        {
            chestBoxManager.CheckAuto();
        }
    }

    IEnumerator GoldPerSecondCoroution()
    {
        if (!inGameUI.activeInHierarchy)
        {
            isGoldPerSecond = false;
            yield break;
        }

        PlayfabManager.instance.UpdateSellPriceGold(goldPerSecond);

        yield return timerSeconds;

        StartCoroutine(GoldPerSecondCoroution());
    }

    IEnumerator AutoUpgradeCoroution()
    {
        if(!inGameUI.activeInHierarchy || !GameStateManager.instance.AutoUpgrade || GameStateManager.instance.GameType == GameType.Rank)
        {
            auto = false;
        }

        if(!auto)
        {
            yield break;
        }

        if (level >= GameStateManager.instance.AutoUpgradeLevel - 1 || level + 1 > maxLevel - 1)
        {
            sellButtonAnim.AutoClick();
            SellButton(2);
        }
        else
        {
            upgradeButtonAnim.AutoClick();
            UpgradeButton(2);
        }

        yield return autoUpgradeSecond;

        StartCoroutine(AutoUpgradeCoroution());
    }

    IEnumerator BuffAutoUpgradeCoroution()
    {
        if (!inGameUI.activeInHierarchy || GameStateManager.instance.GameType == GameType.Rank)
        {
            buffAutoUpgrade = false;
        }

        if (!buffAutoUpgrade)
        {
            yield break;
        }

        if (level >= 9)
        {
            sellButtonAnim.AutoClick();
            SellButton(2);
        }
        else
        {
            upgradeButtonAnim.AutoClick();
            UpgradeButton(2);
        }

        yield return buffUpgradeSecond;

        StartCoroutine(BuffAutoUpgradeCoroution());
    }

    IEnumerator ServerDelayCoroution()
    {
        yield return serverSeconds;

        if (!isExp)
        {
            if (playerDataBase.Exp > nowExp)
            {
                nowExp = playerDataBase.Exp;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);
                levelManager.Initialize();

                isExp = levelManager.CheckMaxLevel();
            }
        }

        if (playerDataBase.UpgradeCount > nowUpgradeCount)
        {
            nowUpgradeCount = playerDataBase.UpgradeCount;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("UpgradeCount", playerDataBase.UpgradeCount);
        }

        if (playerDataBase.SellCount > nowSellCount)
        {
            nowSellCount = playerDataBase.SellCount;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("SellCount", playerDataBase.SellCount);
        }

        yield return serverSeconds;

#if !UNITY_EDITOR

        if (GameStateManager.instance.GameType == GameType.Rank)
        {
            if (GameStateManager.instance.RankFoodLevel[0] + 1 > playerDataBase.RankLevel1)
            {
                playerDataBase.RankLevel1 = GameStateManager.instance.RankFoodLevel[0] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel1", playerDataBase.RankLevel1);
            }

            if (GameStateManager.instance.RankFoodLevel[1] + 1 > playerDataBase.RankLevel2)
            {
                playerDataBase.RankLevel2 = GameStateManager.instance.RankFoodLevel[1] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel2", playerDataBase.RankLevel2);
            }

            if (GameStateManager.instance.RankFoodLevel[2] + 1 > playerDataBase.RankLevel3)
            {
                playerDataBase.RankLevel3 = GameStateManager.instance.RankFoodLevel[2] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel3", playerDataBase.RankLevel3);
            }

            if (GameStateManager.instance.RankFoodLevel[3] + 1 > playerDataBase.RankLevel4)
            {
                playerDataBase.RankLevel4 = GameStateManager.instance.RankFoodLevel[3] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel4", playerDataBase.RankLevel4);
            }

            yield return serverSeconds;

            if (GameStateManager.instance.RankFoodLevel[4] + 1 > playerDataBase.RankLevel5)
            {
                playerDataBase.RankLevel5 = GameStateManager.instance.RankFoodLevel[4] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel5", playerDataBase.RankLevel5);
            }

            if (GameStateManager.instance.RankFoodLevel[5] + 1 > playerDataBase.RankLevel6)
            {
                playerDataBase.RankLevel6 = GameStateManager.instance.RankFoodLevel[5] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel6", playerDataBase.RankLevel6);
            }

            if (GameStateManager.instance.RankFoodLevel[6] + 1 > playerDataBase.RankLevel7)
            {
                playerDataBase.RankLevel7 = GameStateManager.instance.RankFoodLevel[6] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel7", playerDataBase.RankLevel7);
            }

            if (GameStateManager.instance.RankFoodLevel[7] + 1 > playerDataBase.RankLevel8)
            {
                playerDataBase.RankLevel8 = GameStateManager.instance.RankFoodLevel[7] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel8", playerDataBase.RankLevel8);
            }

            yield return serverSeconds;

            if (GameStateManager.instance.RankFoodLevel[8] + 1 > playerDataBase.RankLevel9)
            {
                playerDataBase.RankLevel9 = GameStateManager.instance.RankFoodLevel[8] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel9", playerDataBase.RankLevel9);
            }

            if (GameStateManager.instance.RankFoodLevel[9] + 1 > playerDataBase.RankLevel10)
            {
                playerDataBase.RankLevel10 = GameStateManager.instance.RankFoodLevel[9] + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel10", playerDataBase.RankLevel10);
            }

            yield return serverSeconds;

            rankTotalLevel = playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4 + playerDataBase.RankLevel5
                + playerDataBase.RankLevel6 + playerDataBase.RankLevel7 + playerDataBase.RankLevel8 + playerDataBase.RankLevel9 + playerDataBase.RankLevel10;

            switch (season)
            {
                case 0:
                    if (rankTotalLevel > playerDataBase.TotalLevel)
                    {
                        playerDataBase.TotalLevel = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel", playerDataBase.TotalLevel);
                    }
                    break;
                case 1:
                    if (rankTotalLevel > playerDataBase.TotalLevel_1)
                    {
                        playerDataBase.TotalLevel_1 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_1", playerDataBase.TotalLevel_1);
                    }
                    break;
                case 2:
                    if (rankTotalLevel > playerDataBase.TotalLevel_2)
                    {
                        playerDataBase.TotalLevel_2 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_2", playerDataBase.TotalLevel_2);
                    }
                    break;
                case 3:
                    if (rankTotalLevel > playerDataBase.TotalLevel_3)
                    {
                        playerDataBase.TotalLevel_3 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_3", playerDataBase.TotalLevel_3);
                    }
                    break;
                case 4:
                    if (rankTotalLevel > playerDataBase.TotalLevel_4)
                    {
                        playerDataBase.TotalLevel_4 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_4", playerDataBase.TotalLevel_4);
                    }
                    break;
                case 5:
                    if (rankTotalLevel > playerDataBase.TotalLevel_5)
                    {
                        playerDataBase.TotalLevel_5 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_5", playerDataBase.TotalLevel_5);
                    }
                    break;
                case 6:
                    if (rankTotalLevel > playerDataBase.TotalLevel_6)
                    {
                        playerDataBase.TotalLevel_6 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_6", playerDataBase.TotalLevel_6);
                    }
                    break;
                case 7:
                    if (rankTotalLevel > playerDataBase.TotalLevel_7)
                    {
                        playerDataBase.TotalLevel_7 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_7", playerDataBase.TotalLevel_7);
                    }
                    break;
                case 8:
                    if (rankTotalLevel > playerDataBase.TotalLevel_1)
                    {
                        playerDataBase.TotalLevel_8 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_8", playerDataBase.TotalLevel_8);
                    }
                    break;
                case 9:
                    if (rankTotalLevel > playerDataBase.TotalLevel_9)
                    {
                        playerDataBase.TotalLevel_9 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_9", playerDataBase.TotalLevel_9);
                    }
                    break;
                case 10:
                    if (rankTotalLevel > playerDataBase.TotalLevel_10)
                    {
                        playerDataBase.TotalLevel_10 = rankTotalLevel;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_10", playerDataBase.TotalLevel_10);
                    }
                    break;
            }
        }

#endif

        StartCoroutine(ServerDelayCoroution());
    }

    public void Initialize()
    {
        signText.text = GameStateManager.instance.NickName;

        RenewalVC();

        SuccessLogin();
    }

    public void FirstReward()
    {
        if (playerDataBase.FirstReward == 0)
        {
            moveArrow1.SetActive(true);

            FirebaseAnalytics.LogEvent("New_" + DateTime.Now.ToString("yyyyMMdd"));

            playerDataBase.FirstReward = 1;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("FirstReward", 1);

            GameStateManager.instance.FeverCount = feverMaxCount * 0.5f;

            PlayfabManager.instance.UpdateAddGold(100000);

            StartCoroutine(FirstDelay());
        }
    }

    void OFFSpeicalFood()
    {
        isRareFood = false;
        speicalFoodParticle.gameObject.SetActive(false);
        rareFood.SetActive(false);
    }

    public void ChangeIsland(IslandType type)
    {
        GameStateManager.instance.IslandType = type;

        islandImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];
        //changeFoodImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];

        OFFSpeicalFood();

        ChangeFood(FoodType.Food1 + ((int)type * GameStateManager.instance.Island));

        if (GameStateManager.instance.Effect)
        {
            if (feverMode)
            {
                for (int i = 0; i < level1UpParticle.Length; i++)
                {
                    yummyTime2Particle[i].gameObject.SetActive(false);
                }

                yummyTime2Particle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
            }
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeIslandNotion);

        CheckFoodState();
        if(inGameUI.activeInHierarchy)
        {
            UpgradeInitialize();
        }
        BackgroundEffect();
    }

    public void ChangeFood_Book(FoodType type, int index)
    {
        for (int i = 0; i < foodArrayList.Count; i++)
        {
            foodArrayList[i].gameObject.SetActive(false);
        }

        foodArray[(int)type].gameObject.SetActive(true);
        foodArray[(int)type].Initialize(-1);

        if(index == 0)
        {
            foodArray[(int)type].SetSpeicalFood(false);
        }
        else
        {
            foodArray[(int)type].SetSpeicalFood(true);
        }
    }

    public void ChangeFood(FoodType type)
    {
        GameStateManager.instance.FoodType = type;

        FirebaseAnalytics.LogEvent("ChangeFood_" + type);

        OFFSpeicalFood();

        if (type == FoodType.Food4 && GameStateManager.instance.StoreType != StoreType.OneStore)
        {
            if (!playerDataBase.AppReview)
            {
                OpenAppReview();

                playerDataBase.AppReview = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("AppReview", 1);
            }
        }

        CheckFoodLevel();
        CheckFoodState();
        UpgradeInitialize();
    }

    public void ChangeRankFood(RankFoodType type)
    {
        GameStateManager.instance.RankFoodType = type;

        FirebaseAnalytics.LogEvent("ChangeRankFood_" + type);

        OFFSpeicalFood();

        CheckFoodLevel();
        CheckFoodState();
        UpgradeInitialize();
    }


    public void UpgradeInitialize()
    {
        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:
                maxLevel = upgradeDataBase.GetFoodMaxLevel(GameStateManager.instance.FoodType);
                titleText.localizationName = GameStateManager.instance.FoodType.ToString();

                for (int i = 0; i < particleSystemList.Count; i++)
                {
                    particleSystemList[i].Initialize(level);
                }
                break;
            case GameType.Rank:
                maxLevel = upgradeDataBase.GetRankFoodMaxLevel(GameStateManager.instance.RankFoodType);
                titleText.localizationName = GameStateManager.instance.RankFoodType.ToString();

                for (int i = 0; i < particleSystemList.Count; i++)
                {
                    particleSystemList[i].Initialize(level / 20);
                }
                break;
        }

        if (level > maxLevel)
        {
            level = maxLevel;
        }

        sellPrice = upgradeDataBase.GetPrice(level, defaultSellPrice);
        sellPrice += (int)(sellPrice * (islandDataBase.GetSellPrice(GameStateManager.instance.IslandType) * 0.01f));

        if (isRareFood)
        {
            sellPrice += (int)(sellPrice * 1.5f);
        }

        needPrice = upgradeDataBase.GetNeed(level, defaultNeed);
        needPrice += (int)(needPrice * (islandDataBase.GetSellPrice(GameStateManager.instance.IslandType) * 0.01f));

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Rank:
                needPrice *= 10;
                break;
        }

        if (needPricePlus > 0)
        {
            needPrice -= Mathf.CeilToInt((needPrice * (0.01f * needPricePlus)));

            if (needPricePlus >= 100)
            {
                needPrice = 0;
            }
        }


        if (sellPricePlus > 0)
        {
            sellPrice += Mathf.CeilToInt((sellPrice * (0.01f * sellPricePlus)));
        }

        if (sellPrice >= 500000000)
        {
            sellPrice = 500000000;
        }

        titleText.plusText = " ( " + (level + 1) + " / " + maxLevel + " )";

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:
                highLevelText.localizationName = " ";
                highLevelText.plusText = "";

                success = 100 - (islandDataBase.GetSuccess(GameStateManager.instance.IslandType)) - (level * 1);

                break;
            case GameType.Rank:
                highLevelText.localizationName = "Best";
                highLevelText.plusText = "";

                success = 100 - (islandDataBase.GetSuccess_Rank(GameStateManager.instance.RankFoodType)) - (level * 1);

                switch (GameStateManager.instance.RankFoodType)
                {
                    case RankFoodType.RankFood1:
                        if (GameStateManager.instance.RankFoodLevel[0] > playerDataBase.RankLevel1)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[0] + 1);
                        }
                        else
                        {
                            highLevelText.plusText = " : " + playerDataBase.RankLevel1;
                        }

                        break;
                    case RankFoodType.RankFood2:
                        if (GameStateManager.instance.RankFoodLevel[1] > playerDataBase.RankLevel2)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[1] + 1);
                        }
                        else
                        {

                            highLevelText.plusText = " : " + playerDataBase.RankLevel2;
                        }
                        break;
                    case RankFoodType.RankFood3:
                        if (GameStateManager.instance.RankFoodLevel[2] > playerDataBase.RankLevel3)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[2] + 1);
                        }
                        else
                        {
                            highLevelText.plusText = " : " + playerDataBase.RankLevel3;
                        }
                        break;
                    case RankFoodType.RankFood4:
                        if (GameStateManager.instance.RankFoodLevel[3] > playerDataBase.RankLevel4)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[3] + 1);
                        }
                        else
                        {
                            highLevelText.plusText = " : " + playerDataBase.RankLevel4;
                        }
                        break;
                    case RankFoodType.RankFood5:
                        if (GameStateManager.instance.RankFoodLevel[4] > playerDataBase.RankLevel5)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[4] + 1);
                        }
                        else
                        {
                            highLevelText.plusText = " : " + playerDataBase.RankLevel5;
                        }
                        break;
                    case RankFoodType.RankFood6:
                        if (GameStateManager.instance.RankFoodLevel[5] > playerDataBase.RankLevel6)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[5] + 1);
                        }
                        else
                        {
                            highLevelText.plusText = " : " + playerDataBase.RankLevel6;
                        }
                        break;
                    case RankFoodType.RankFood7:
                        if (GameStateManager.instance.RankFoodLevel[6] > playerDataBase.RankLevel7)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[6] + 1);
                        }
                        else
                        {
                            highLevelText.plusText = " : " + playerDataBase.RankLevel7;
                        }
                        break;
                    case RankFoodType.RankFood8:
                        if (GameStateManager.instance.RankFoodLevel[7] > playerDataBase.RankLevel8)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[7] + 1);
                        }
                        else
                        {
                            highLevelText.plusText = " : " + playerDataBase.RankLevel8;
                        }
                        break;
                    case RankFoodType.RankFood9:
                        if (GameStateManager.instance.RankFoodLevel[8] > playerDataBase.RankLevel9)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[8] + 1);
                        }
                        else
                        {
                            highLevelText.plusText = " : " + playerDataBase.RankLevel9;
                        }
                        break;
                    case RankFoodType.RankFood10:
                        if (GameStateManager.instance.RankFoodLevel[9] > playerDataBase.RankLevel10)
                        {
                            highLevelText.plusText = " : " + (GameStateManager.instance.RankFoodLevel[9] + 1);
                        }
                        else
                        {
                            highLevelText.plusText = " : " + playerDataBase.RankLevel10;
                        }
                        break;
                    default:
                        break;
                }
                break;
        }

        if (level >= 34)
        {
            titleText.GetText().color = Color.white;
        }
        else if (level >= 29)
        {
            titleText.GetText().color = Color.red;
        }
        else if (level >= 24)
        {
            titleText.GetText().color = Color.green;
        }
        else if (level >= 19)
        {
            titleText.GetText().color = Color.yellow;
        }
        else if (level >= 14)
        {
            titleText.GetText().color = new Color(1, 0, 1);
        }
        else if (level >= 9)
        {
            titleText.GetText().color = new Color(0, 200 / 255f, 1);
        }
        else if (level >= 4)
        {
            titleText.GetText().color = new Color(1, 200 / 255f, 0);
        }
        else
        {
            titleText.GetText().color = Color.white;
        }

        notSell.SetActive(false);

        if (level == 0)
        {
            notSell.SetActive(true);
        }

        success += successPlus;

        if (success >= 100)
        {
            success = 100;
        }

        //if (success < 1f)
        //{
        //    success = 1f;
        //}

        if (GameStateManager.instance.Developer) success = 100;

        successText.localizationName = "SuccessPercent";
        successText.plusText = " : " + success.ToString("N1") + "%";

        if (successPlus > 0)
        {
            successText.plusText += " (+" + (successPlus).ToString("N1") + "%)";
        }

        needText.localizationName = "NeedPrice";
        needText.plusText = "";

        if (needPricePlus > 0)
        {
            needText.plusText += " (-" + (needPricePlus.ToString("N1")) + "%)\n" + MoneyUnitString.ToCurrencyString(needPrice);
        }
        else
        {
            needText.plusText += "\n" + MoneyUnitString.ToCurrencyString(needPrice);
        }

        priceText.localizationName = "NowPrice";
        priceText.plusText = "";

        if (sellPricePlus > 0)
        {
            priceText.plusText += " (+" + sellPricePlus.ToString("N1") + "%)\n" + MoneyUnitString.ToCurrencyString(sellPrice);
        }
        else
        {
            priceText.plusText += "\n" + MoneyUnitString.ToCurrencyString(sellPrice);
        }

        if (defDestroy >= 100)
        {
            defDestroy = 100;
        }

        defDestroyText.localizationName = "DefDestroyPercent";
        defDestroyText.plusText = " : " + defDestroy.ToString("N2") + "%";

        if (successX2 >= 100)
        {
            successX2 = 100;
        }

        successX2Text.localizationName = "SuccessX2Percent";
        successX2Text.plusText = " : " + successX2.ToString("N1") + "%";

        if (successX3 > 0)
        {
            successX2Text.localizationName = "SuccessX3Percent";
            successX2Text.plusText = " : " + successX3.ToString("N1") + "%";
        }

        sellPriceTipText.localizationName = "SellPriceX2Up";
        sellPriceTipText.plusText += "\n" + sellPriceTip.ToString("N1") + "%";

        CheckDefTicket();
        CheckBankruptcy();

        notUpgrade.SetActive(false);

        if (level + 1 >= maxLevel)
        {
            notUpgrade.SetActive(true);
            defTicketObj.SetActive(false);
            successText.localizationName = "MaxLevel";
            successText.plusText = "";
            needText.localizationName = "NeedPrice";
            needText.plusText = "\n-";
        }

        titleText.ReLoad();
        successText.ReLoad();
        needText.ReLoad();
        priceText.ReLoad();
        defDestroyText.ReLoad();
        successX2Text.ReLoad();
        highLevelText.ReLoad();
        //sellPriceTipText.ReLoad();

        if (guideMissionManager.guideMissonView.gameObject.activeInHierarchy)
        {
            guideMissionManager.Initialize();
        }
    }

    void CheckFoodState()
    {
        for(int i = 0; i < foodArrayList.Count; i ++)
        {
            foodArrayList[i].gameObject.SetActive(false);
        }

        if (GameStateManager.instance.GameType == GameType.Story)
        {
            foodArray[(int)GameStateManager.instance.FoodType].gameObject.SetActive(true);
            foodArray[(int)GameStateManager.instance.FoodType].Initialize(level);
            foodArray[(int)GameStateManager.instance.FoodType].SetSpeicalFood(isRareFood);
        }
        else
        {
            rankFoodArray[(int)GameStateManager.instance.RankFoodType].gameObject.SetActive(true);
            rankFoodArray[(int)GameStateManager.instance.RankFoodType].RankInitialize(level);
        }

        if (feverMode)
        {
            for (int i = 0; i < foodArrayList.Count; i++)
            {
                if (foodArrayList[i].gameObject.activeInHierarchy)
                {
                    foodArrayList[i].FeverOn();
                }
            }
        }
        else
        {
            for (int i = 0; i < foodArrayList.Count; i++)
            {
                if (foodArrayList[i].gameObject.activeInHierarchy)
                {
                    foodArrayList[i].FeverOff();
                }
            }
        }
    }

    void CheckFoodState_MaxLevel()
    {
        for (int i = 0; i < foodArrayList.Count; i++)
        {
            foodArrayList[i].gameObject.SetActive(false);
        }

        if (GameStateManager.instance.GameType == GameType.Story)
        {
            foodArray[(int)GameStateManager.instance.FoodType].gameObject.SetActive(true);
            foodArray[(int)GameStateManager.instance.FoodType].Initialize(level + 10);
            foodArray[(int)GameStateManager.instance.FoodType].SetSpeicalFood(isRareFood);
        }
        else
        {
            rankFoodArray[(int)GameStateManager.instance.RankFoodType].gameObject.SetActive(true);
            rankFoodArray[(int)GameStateManager.instance.RankFoodType].RankInitialize(level + 20);
        }
    }

    public void UpgradeButton(int number)
    {
        if(number < 2)
        {
            if (GameStateManager.instance.GameType == GameType.Story)
            {
                if (GameStateManager.instance.AutoUpgrade && number == 0)
                {
                    return;
                }

                if (buff4)
                {
                    return;
                }
            }

            if (isUpgradeDelay) return;
        }

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);

            auto = false;
            buffAutoUpgrade = false;
            return;
        }

        if (level + 1 >= maxLevel)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.MaxLevel);
            return;
        }

        RenewalVC();

        if (playerDataBase.Coin < needPrice)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowCoin);
            return;
        }

        if (GameStateManager.instance.StoreType != StoreType.OneStore)
        {
            if (supportCount >= supportMaxCount)
            {
                supportCount = 0;

                OpenSupportPackage();
            }
            else
            {
                supportCount += 1;
            }
        }

        PlayfabManager.instance.UpdateSellPriceGold(-needPrice);

        myMoneyPlusText.gameObject.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(true);
        myMoneyPlusText.color = Color.red;
        myMoneyPlusText.text = "-" + MoneyUnitString.ToCurrencyString(needPrice);

        switch(number)
        {
            case 0:
                if(playerDataBase.AttendanceCount == 1)
                {
                    FirebaseAnalytics.LogEvent(GameStateManager.instance.NickName + " : Upgrade_Screen");
                }
                else
                {
                    FirebaseAnalytics.LogEvent("Upgrade_Screen");
                }
                break;
            case 1:
                if (playerDataBase.AttendanceCount == 1)
                {
                    FirebaseAnalytics.LogEvent(GameStateManager.instance.NickName + " : Upgrade_Button");
                }
                else
                {
                    FirebaseAnalytics.LogEvent("Upgrade_Button");
                }
                break;
        }    

        if (Random.Range(0, 100f) >= 100 - success)
        {
            if (isDef)
            {
                UseDefTicket();
            }

            if (successX3 > 0)
            {
                if (successX3 >= Random.Range(0, 100))
                {
                    if (level + 3 >= maxLevel - 1)
                    {
                        level += 1;
                    }
                    else
                    {
                        level += 3;
                    }

                    if (!changeFoodManager.changeFoodView.activeInHierarchy)
                    {
                        NotionManager.instance.UseNotion(NotionType.SuccessUpgradeX3);
                    }
                }
                else
                {
                    level += 1;
                }
            }
            else
            {
                if (successX2 > 0)
                {
                    if (successX2 >= Random.Range(0, 100))
                    {
                        if (level + 2 >= maxLevel - 1)
                        {
                            level += 1;
                        }
                        else
                        {
                            level += 2;
                        }

                        if (!changeFoodManager.changeFoodView.activeInHierarchy)
                        {
                            NotionManager.instance.UseNotion(NotionType.SuccessUpgradeX2);
                        }
                    }
                    else
                    {
                        level += 1;
                    }
                }
                else
                {
                    level += 1;
                }
            }

            GameStateManager.instance.UpgradeCount += 1;
            playerDataBase.UpgradeCount += 1;

            if (!isExp)
            {
                playerDataBase.Exp += expUp;
                levelManager.Initialize();
            }

            if (level + 1 >= maxLevel)
            {
                level -= 1;
                StartCoroutine(MaxLevelUpgradeSuccessCoroution());
            }
            else
            {
                if ((level + 1) % 5 == 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Upgrade5);

                    if (GameStateManager.instance.Effect)
                    {
                        level1UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(false);
                        level1UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
                        level1UpParticle[(int)GameStateManager.instance.ParticleType].Play();

                        level5UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(false);
                        level5UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
                        level5UpParticle[(int)GameStateManager.instance.ParticleType].Play();
                    }
                }
                else
                {
                    if (GameStateManager.instance.Effect)
                    {
                        level1UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(false);
                        level1UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
                        level1UpParticle[(int)GameStateManager.instance.ParticleType].Play();
                    }

                    SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                }

                SaveFoodLevel(level);
                UpgradeInitialize();
                CheckFoodState();
            }
        }
        else
        {
            if(GameStateManager.instance.GameType == GameType.Story)
            {
                if (100 - destoryPercent >= Random.Range(0, 100f))
                {
                    //일반모드에서 파괴 안됨
                    level -= 1;
                    LevelDown();

                    CheckFoodLevel();
                    CheckFoodState();
                    UpgradeInitialize();

                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.FailUpgrade);

                    return;
                }
                else
                {
                    //일반모드에서 파괴 됨
                    if (isDef)
                    {
                        UseDefTicket();

                        NotionManager.instance.UseNotion4(NotionType.DefDestroyNotion);
                        return;
                    }
                    else
                    {
                        if (defDestroy > 0)
                        {
                            if (defDestroy >= Random.Range(0, 100))
                            {
                                playerDataBase.DefDestroyCount += 1;
                                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyCount", playerDataBase.DefDestroyCount);

                                SoundManager.instance.PlaySFX(GameSfxType.Shield);
                                NotionManager.instance.UseNotion4(NotionType.DefDestroyNotion);

                                FirebaseAnalytics.LogEvent("Defense_Destroy");

                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                if (isDef)
                {
                    UseDefTicket();

                    SoundManager.instance.PlaySFX(GameSfxType.Shield);
                    NotionManager.instance.UseNotion4(NotionType.DefDestroyNotion);
                    return;
                }
                else
                {
                    if (defDestroy > 0)
                    {
                        if (defDestroy >= Random.Range(0, 100))
                        {
                            level -= 1;
                            LevelDown();

                            CheckFoodLevel();
                            CheckFoodState();
                            UpgradeInitialize();

                            playerDataBase.DefDestroyCount += 1;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyCount", playerDataBase.DefDestroyCount);

                            SoundManager.instance.PlaySFX(GameSfxType.Shield);
                            NotionManager.instance.UseNotion4(NotionType.DefDestroyNotion);

                            FirebaseAnalytics.LogEvent("Defense_Destroy");

                            return;
                        }
                    }
                }
            }


            if (level >= 9 && maxLevel >= 20)
            {
                if (recoverTicketPercent >= Random.Range(0, 100f))
                {
                    PortionManager.instance.GetRepairTickets(1);
                    Debug.LogError("Get Repair Ticket");
                }
            }

            if (GameStateManager.instance.Effect)
            {
                bombAllParticle.gameObject.SetActive(false);
                bombAllParticle.gameObject.SetActive(true);
                bombAllParticle.Play();

                bombParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(false);
                bombParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
                bombParticle[(int)GameStateManager.instance.ParticleType].Play();
            }

            if (level >= 50 && GameStateManager.instance.Recover && GameStateManager.instance.GameType == GameType.Rank)
            {
                recoverLevel = ((int)(level * 0.5f)) - 1;

                recoverManager.FoodInitialize(GameStateManager.instance.RankFoodType, level);
            }

            DestoryFood();
            CheckFoodState();
            UpgradeInitialize();

            if (!GameStateManager.instance.FirstFail)
            {
                tutorialManager.Next2();
            }

            if(!GameStateManager.instance.FirstDestory)
            {
                GameStateManager.instance.DestroyCount += 1;
                if (GameStateManager.instance.DestroyCount >= 10)
                {
                    GameStateManager.instance.DestroyCount = 0;
                    tutorialManager.Next3();
                }
            }

            SoundManager.instance.PlaySFX(GameSfxType.UpgradeFail);
            if (!changeFoodManager.changeFoodView.activeInHierarchy && !GameStateManager.instance.YoutubeVideo)
            {
                NotionManager.instance.UseNotion(NotionType.FailUpgrade);
            }
        }

        if (feverFillamount.gameObject.activeInHierarchy)
        {
            if (!feverMode && level + 1 < maxLevel)
            {
                feverCount += feverCountPlus;

                CheckFever();
            }
        }

        isUpgradeDelay = true;
        Invoke("WaitUpgradeDelay", delay);
    }

    void LevelDown()
    {
        GameStateManager.instance.FoodLevel[(int)GameStateManager.instance.FoodType] -= 1;
    }

    IEnumerator MaxLevelUpgradeSuccessCoroution()
    {
        inGameUI.SetActive(false);

        level += 1;
        SaveFoodLevel(level);

        cameraController.GoToC();

        if(feverMode)
        {
            yield return maxLevelSecond_Fever;
        }
        else
        {
            yield return maxLevelSecond;
        }

        CheckFoodState();
        CheckFoodState_MaxLevel();

        SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);

        if (!changeFoodManager.changeFoodView.activeInHierarchy)
        {
            NotionManager.instance.UseNotion2(NotionType.MaxLevel);
        }

        if (GameStateManager.instance.Effect)
        {
            level1UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(false);
            level1UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
            level1UpParticle[(int)GameStateManager.instance.ParticleType].Play();

            level5UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(false);
            level5UpParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
            level5UpParticle[(int)GameStateManager.instance.ParticleType].Play();

            levelMaxParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(false);
            levelMaxParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
            levelMaxParticle[(int)GameStateManager.instance.ParticleType].Play();
        }

        if(feverMode)
        {
            yield return buffUpgradeSecond_Fever;
        }
        else
        {
            yield return buffUpgradeSecond;
        }

        cameraController.GoToB();

        if (feverMode)
        {
            yield return buffUpgradeSecond_Fever;
        }
        else
        {
            yield return buffUpgradeSecond;
        }

        inGameUI.SetActive(true);

        MaxLevelUpgradeSuccess();

        CheckAuto();
    }

    void SaveFoodLevel(int level)
    {
        if(GameStateManager.instance.GameType == GameType.Story)
        {
            GameStateManager.instance.FoodLevel[(int)GameStateManager.instance.FoodType] = level;
        }
        else
        {
            GameStateManager.instance.RankFoodLevel[(int)GameStateManager.instance.RankFoodType] = level;
        }
    }

    public void RecoverFood(RankFoodType type)
    {
        GameStateManager.instance.RankFoodLevel[(int)type] = recoverLevel;

        ChangeRankFood(type);
    }
    public void SetParticle(bool check)
    {
        if(check)
        {
            if(feverMode)
            {
                yummyTimeParticle.gameObject.SetActive(true);
                yummyTime2Particle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
            }

            if (isRareFood)
            {
                speicalFoodParticle.gameObject.SetActive(false);
                speicalFoodParticle.gameObject.SetActive(true);
                speicalFoodParticle.Play();
            }
        }
        else
        {
            yummyTimeParticle.gameObject.SetActive(false);
            yummyTime2Particle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(false);

            speicalFoodParticle.gameObject.SetActive(false);
            rareFood.SetActive(false);
        }
    }

    void DestoryFood()
    {
        level = 0;

        GameStateManager.instance.FoodLevel[(int)GameStateManager.instance.FoodType] = 0;
    }

    void CheckFever()
    {
        feverFillamount.fillAmount = feverCount * 1.0f / feverMaxCount * 1.0f;
        feverText.text = LocalizationManager.instance.GetString("FeverGauge") + " : " + ((feverCount * 1.0f / feverMaxCount * 1.0f) * 100).ToString("N1") + "%";

        if (feverCount >= feverMaxCount)
        {
            feverMode = true;

            cameraController.smoothTime = 0.2f;

            feverEffect.SetActive(true);
            backButton.SetActive(false);

            successText.GetText().color = Color.red;

            successPlus += feverPlus;

            UpgradeInitialize();

            StartCoroutine(FeverModeCoroution());

            SoundManager.instance.PlaySFX(GameSfxType.Fever_In);
            SoundManager.instance.PlayFever();
            NotionManager.instance.UseNotion(NotionType.FeverNotion);
        }
    }

    IEnumerator FeverModeCoroution()
    {
        truckAnimator[(int)GameStateManager.instance.TruckType].SetBool("YummyTime", true);
        characterAnimator[(int)GameStateManager.instance.CharacterType].SetBool("YummyTime", true);
        animalAnimator[(int)GameStateManager.instance.AnimalType].SetBool("YummyTime", true);
        butterflyAnimator[(int)GameStateManager.instance.ButterflyType].SetBool("YummyTime", true);

        for(int i = 0; i < foodArrayList.Count; i ++)
        {
            if(foodArrayList[i].gameObject.activeInHierarchy)
            {
                foodArrayList[i].FeverOn();
            }
        }

        if (GameStateManager.instance.Effect)
        {
            yummyTimeParticle.gameObject.SetActive(false);
            yummyTimeParticle.gameObject.SetActive(true);
            yummyTime2Particle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
        }

        if (GameStateManager.instance.Vibration)
        {
            Handheld.Vibrate();
        }

        feverText.enabled = false;

        feverCount = 0;
        GameStateManager.instance.FeverCount = 0;

        GameStateManager.instance.YummyTimeCount += 1;
        playerDataBase.YummyTimeCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("YummyTimeCount", playerDataBase.YummyTimeCount);

        currentTime = 0f;
        fillAmount = 0;

        portionScaleAnim[3].StopAnim();

        while (currentTime < feverTime)
        {
            fillAmount = Mathf.Lerp(1.0f, 0, currentTime / feverTime);
            fillAmount = Mathf.Clamp01(fillAmount);

            feverFillamount.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        cameraController.smoothTime = 0.3f;

        portionScaleAnim[3].PlayAnim();

        yummyTimeParticle.gameObject.SetActive(false);
        yummyTime2Particle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(false);

        feverMode = false;
        feverFillamount.fillAmount = 0;

        successPlus -= feverPlus;

        feverText.enabled = true;
        feverText.text = LocalizationManager.instance.GetString("FeverGauge") + "  0%";

        successText.GetText().color = Color.green;

        UpgradeInitialize();

        feverEffect.SetActive(false);
        backButton.SetActive(true);

        SoundManager.instance.StopFever();

        truckAnimator[(int)GameStateManager.instance.TruckType].SetBool("YummyTime", false);
        characterAnimator[(int)GameStateManager.instance.CharacterType].SetBool("YummyTime", false);
        animalAnimator[(int)GameStateManager.instance.AnimalType].SetBool("YummyTime", false);
        butterflyAnimator[(int)GameStateManager.instance.ButterflyType].SetBool("YummyTime", false);

        for (int i = 0; i < foodArrayList.Count; i++)
        {
            if (foodArrayList[i].gameObject.activeInHierarchy)
            {
                foodArrayList[i].FeverOff();
            }
        }
    }

    void MaxLevelUpgradeSuccess()
    {
        GameStateManager.instance.DestroyCount = 0;

        if (!GameStateManager.instance.FirstSuccess)
        {
            tutorialManager.Next1();
        }

        if (playerDataBase.island_Total_Data.island_Max_Datas[(int)GameStateManager.instance.IslandType].GetValue(GameStateManager.instance.FoodType) == 0)
        {
            if(GameStateManager.instance.FoodType != FoodType.Food1)
            {
                changeFoodManager.ChangeFoodMoveArrow(GameStateManager.instance.FoodType + 1);

                changeFoodAlarmObj.SetActive(true);
                moveArrow3.SetActive(true);
            }
        }

        lockManager.UnLocked((int)GameStateManager.instance.FoodType + 1);

        if (playerDataBase.NextFoodNumber == (int)GameStateManager.instance.FoodType)
        {
            playerDataBase.NextFoodNumber = (int)GameStateManager.instance.FoodType + 1;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
        }
        
        playerDataBase.island_Total_Data.island_Max_Datas[(int)GameStateManager.instance.IslandType].SetValue(GameStateManager.instance.FoodType, 1);

        playerData.Clear();
        playerData.Add("Island_Total_Data", JsonUtility.ToJson(playerDataBase.island_Total_Data));
        PlayfabManager.instance.SetPlayerData(playerData);

        if((int)GameStateManager.instance.FoodType > 0 && (((int)GameStateManager.instance.FoodType + 1) % GameStateManager.instance.Island) == 0)
        {
            if (playerDataBase.IslandNumber <= ((int)GameStateManager.instance.FoodType + 1) / GameStateManager.instance.Island) //섬 개방
            {
                playerDataBase.IslandNumber = (((int)GameStateManager.instance.FoodType + 1) / GameStateManager.instance.Island);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);

#if !UNITY_EDITOR
                if (playerDataBase.TestAccount == 0)
                {
                    playerDataBase.IslandNumber_Ranking = (((int)GameStateManager.instance.FoodType + 1) / GameStateManager.instance.Island);
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber_Ranking", playerDataBase.IslandNumber_Ranking);
                }
#endif

                islandManager.NewIsland(playerDataBase.IslandNumber);

                Debug.Log("새로운 섬 개방!");
            }
        }

        changeFoodManager.CheckProficiency();
        UpgradeInitialize();
    }

    public void SellButton(int number)
    {
        if (number < 2)
        {
            if (GameStateManager.instance.AutoUpgrade || buff4)
            {
                return;
            }

            if (isUpgradeDelay) return;
        }

        if (level == 0) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        moveArrow2.SetActive(false);

        if (level >= 9 && playerDataBase.InGameTutorial == 0)
        {
            changeFoodAlarmObj.SetActive(true);
            moveArrow3.SetActive(true);

            changeFoodManager.ChangeFoodMoveArrow(FoodType.Food2);

            lockManager.ChangeFoodTutorial();
        }

        if(sellPriceTip > 0)
        {
            if(sellPriceTip >= Random.Range(0f, 100f))
            {
                sellPrice = sellPrice + (int)(sellPrice * 0.15f);

                if (!changeFoodManager.changeFoodView.activeInHierarchy)
                {
                    NotionManager.instance.UseNotion(NotionType.SuccessSellX2);
                }
            }
            else
            {
                if (!changeFoodManager.changeFoodView.activeInHierarchy)
                {
                    NotionManager.instance.UseNotion(NotionType.SuccessSell);
                }
            }
        }
        else
        {
            if (!changeFoodManager.changeFoodView.activeInHierarchy)
            {
                NotionManager.instance.UseNotion(NotionType.SuccessSell);
            }
        }

        if(isRareFood)
        {
            if(level >= maxLevel - 1)
            {
                CheckRareFood();
            }

            Debug.LogError("Sell Rare Food");

            FirebaseAnalytics.LogEvent("Sell_RareFood");
        }

        PlayfabManager.instance.UpdateSellPriceGold(sellPrice);
        PlayfabManager.instance.moneyAnimation.PlusMoney(sellPrice);
        GameStateManager.instance.TodayGold += sellPrice;

        if(playerDataBase.GuideIndex < 27)
        {
            GameStateManager.instance.GetSellGold += sellPrice;
        }

        if (level >= 9)
        {
            playerDataBase.SellCount += 1;
            GameStateManager.instance.SellCount += 1;

            if (gifticon)
            {
                if (Random.Range(0, 100f) < eventTicketPercent)
                {
                    if (playerDataBase.EventTicket >= 1000) return;

                    PortionManager.instance.GetEventTicket(1);

                    playerDataBase.EventTicketCount += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventTicketCount", playerDataBase.EventTicketCount);

                    Debug.LogError("Get Event Ticket");
                }
            }
        }

        OFFSpeicalFood();

        if (!GameStateManager.instance.AutoUpgrade && !buffAutoUpgrade)
        {
            speicalFoodCount += 1;

            if (speicalFoodCount >= speicalFoodNeedCount && level >= 9)
            {
                if (rareFoodPercent >= Random.Range(0, 100f))
                {
                    speicalFoodCount = 0;

                    isRareFood = true;

                    SoundManager.instance.PlaySFX(GameSfxType.RareFoodOpen);
                    NotionManager.instance.UseNotion2(NotionType.SpeicalFoodNotion);

                    if (GameStateManager.instance.Effect)
                    {
                        speicalFoodParticle.gameObject.SetActive(false);
                        speicalFoodParticle.gameObject.SetActive(true);
                        speicalFoodParticle.Play();
                        rareFood.SetActive(true);
                    }

                    Debug.Log("Open Rare Food");

                    FirebaseAnalytics.LogEvent("Open_RareFood");
                }
            }
        }

        if (GameStateManager.instance.Effect)
        {
            for (int i = 0; i < level1UpParticle.Length; i++)
            {
                sellParticle[i].gameObject.SetActive(false);
            }

            sellParticle[(int)GameStateManager.instance.ParticleType].gameObject.SetActive(true);
        }

        myMoneyPlusText.gameObject.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(true);
        myMoneyPlusText.color = Color.green;
        myMoneyPlusText.text = "+" + MoneyUnitString.ToCurrencyString(sellPrice);

        FirebaseAnalytics.LogEvent("SellFood : Lv." + level);

        DestoryFood();
        CheckFoodState();
        UpgradeInitialize();

        SoundManager.instance.PlaySFX(GameSfxType.Sell);

        isUpgradeDelay = true;
        Invoke("WaitSellDelay", delay);
    }

    void CheckRareFood()
    {
        playerDataBase.island_Total_Data.island_Rare_Datas[(int)GameStateManager.instance.IslandType].SetValue(GameStateManager.instance.FoodType, 1);

        playerData.Clear();
        playerData.Add("Island_Total_Data", JsonUtility.ToJson(playerDataBase.island_Total_Data));
        PlayfabManager.instance.SetPlayerData(playerData);

        FirebaseAnalytics.LogEvent("RareFood_" + GameStateManager.instance.IslandType + " : " + GameStateManager.instance.FoodType);

        Debug.Log("Success Rare Food");
    }

    public void CheckDefTicket()
    {
        if (GameStateManager.instance.YoutubeVideo) return;

        if(level >= 1 && level + 1 < maxLevel && playerDataBase.LockTutorial > 1)
        {
            defTicketObj.SetActive(true);

            defTicketText.text = MoneyUnitString.ToCurrencyString(playerDataBase.DefDestroyTicket) + "/1";

            if (playerDataBase.DefDestroyTicket <= 0)
            {
                if (isDef)
                {
                    defDestroy -= defDestroyPlus;
                }

                isDef = false;
                checkMark.SetActive(false);
            }
        }
        else
        {
            if(isDef)
            {
                defDestroy -= defDestroyPlus;
            }

            isDef = false;
            checkMark.SetActive(false);

            defTicketObj.SetActive(false);
        }

        if (inGameUI.gameObject.activeInHierarchy)
        {
            if (defDestroy >= 100)
            {
                defDestroy = 100;
            }

            defDestroyText.localizationName = "DefDestroyPercent";
            defDestroyText.plusText = " : " + defDestroy.ToString("N2") + "%";
            defDestroyText.ReLoad();
        }
    }

    public void UseDefTicket()
    {
        playerDataBase.DefDestroyTicket -= 1;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicket", playerDataBase.DefDestroyTicket);

        CheckDefTicket();
    }

    public void SetDefTicket()
    {
        if (level + 1 >= maxLevel)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.MaxLevel);
            return;
        }

        if (!isDef)
        {
            if (playerDataBase.DefDestroyTicket > 0)
            {
                defDestroy += defDestroyPlus;

                isDef = true;
                checkMark.SetActive(true);

                SoundManager.instance.PlaySFX(GameSfxType.Click);
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Shield);
                NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                return;
            }
        }
        else
        {
            defDestroy -= defDestroyPlus;

            isDef = false;
            checkMark.SetActive(false);

            SoundManager.instance.PlaySFX(GameSfxType.Click);
        }

        UpgradeInitialize();
    }

    public void CheckPortion()
    {
        if (inGameUI.activeInHierarchy)
        {
            portionText1.text = MoneyUnitString.ToCurrencyString(playerDataBase.Portion1);
            portionText2.text = MoneyUnitString.ToCurrencyString(playerDataBase.Portion2);
            portionText3.text = MoneyUnitString.ToCurrencyString(playerDataBase.Portion3);
            portionText4.text = MoneyUnitString.ToCurrencyString(playerDataBase.Portion4);
            portionText5.text = MoneyUnitString.ToCurrencyString(playerDataBase.Portion5);
            portionText6.text = MoneyUnitString.ToCurrencyString(playerDataBase.Portion6);

            if (playerDataBase.Portion1 == 0)
            {
                if(!GameStateManager.instance.Portion1Ad)
                {
                    portionAd1.SetActive(true);;
                }
            }
            else
            {
                portionAd1.SetActive(false);
            }

            if (playerDataBase.Portion2 == 0)
            {
                if (!GameStateManager.instance.Portion2Ad)
                {
                    portionAd2.SetActive(true); ;
                }
            }
            else
            {
                portionAd2.SetActive(false);
            }

            if (playerDataBase.Portion3 == 0)
            {
                if (!GameStateManager.instance.Portion3Ad)
                {
                    portionAd3.SetActive(true); ;
                }
            }
            else
            {
                portionAd3.SetActive(false);
            }

            if (playerDataBase.Portion4 == 0)
            {
                if (!GameStateManager.instance.Portion4Ad)
                {
                    portionAd4.SetActive(true); ;
                }
            }
            else
            {
                portionAd4.SetActive(false);
            }

            if (playerDataBase.Portion5 == 0)
            {
                if (!GameStateManager.instance.Portion5Ad)
                {
                    portionAd5.SetActive(true); ;
                }
            }
            else
            {
                portionAd5.SetActive(false);
            }

            //if (playerDataBase.Portion6 == 0)
            //{
            //    portionText6.text = "-";
            //}
            //else
            //{
            //    portionAd6.SetActive(false);
            //}

            if (guideMissionManager.guideMissonView.gameObject.activeInHierarchy)
            {
                guideMissionManager.Initialize();
            }
        }
    }

    public void UseSources(int number)
    {
        if (!clickDelay) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        switch (number)
        {
            case 0:
                if(!portion1)
                {
                    if(playerDataBase.Portion1 > 0)
                    {
                        portion1 = true;

                        needPricePlus += portion1Value;
                        UpgradeInitialize();

                        playerDataBase.Portion1 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);

                        playerDataBase.UseSauceCount += 1;
                        GameStateManager.instance.UseSauce += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSauceCount);

                        playerDataBase.UseSauce1 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSauce1", playerDataBase.UseSauce1);

                        StartCoroutine(PortionCoroution1());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion2(NotionType.UsePortionNotion1);

                        FirebaseAnalytics.LogEvent("UseSauce1");
                    }
                    else
                    {
                        if(!GameStateManager.instance.Portion1Ad)
                        {
                            OpenPortionAdView(0);
                        }

                        SoundManager.instance.PlaySFX(GameSfxType.NotSources);
                        NotionManager.instance.UseNotion(NotionType.LowPortion);
                    }
                }               
                break;
            case 1:
                if (!portion2)
                {
                    if (playerDataBase.Portion2 > 0)
                    {
                        portion2 = true;

                        sellPricePlus += portion2Value;
                        UpgradeInitialize();

                        playerDataBase.Portion2 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);

                        playerDataBase.UseSauceCount += 1;
                        GameStateManager.instance.UseSauce += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSauceCount);

                        playerDataBase.UseSauce2 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSauce2", playerDataBase.UseSauce2);

                        StartCoroutine(PortionCoroution2());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion2(NotionType.UsePortionNotion2);

                        FirebaseAnalytics.LogEvent("UseSauce2");
                    }
                    else
                    {
                        if (!GameStateManager.instance.Portion2Ad)
                        {
                            OpenPortionAdView(1);
                        }

                        SoundManager.instance.PlaySFX(GameSfxType.NotSources);
                        NotionManager.instance.UseNotion(NotionType.LowPortion);
                    }
                }
                break;
            case 2:
                if (!portion3)
                {
                    if (playerDataBase.Portion3 > 0)
                    {
                        portion3 = true;

                        successPlus += portion3Value;
                        UpgradeInitialize();

                        playerDataBase.Portion3 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);

                        playerDataBase.UseSauceCount += 1;
                        GameStateManager.instance.UseSauce += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSauceCount);

                        playerDataBase.UseSauce3 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSauce3", playerDataBase.UseSauce3);

                        StartCoroutine(PortionCoroution3());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion2(NotionType.UsePortionNotion3);

                        FirebaseAnalytics.LogEvent("UseSauce3");
                    }
                    else
                    {
                        if (!GameStateManager.instance.Portion3Ad)
                        {
                            OpenPortionAdView(2);
                        }

                        SoundManager.instance.PlaySFX(GameSfxType.NotSources);
                        NotionManager.instance.UseNotion(NotionType.LowPortion);
                    }
                }
                break;
            case 3:
                if (!feverMode && feverFillamount.gameObject.activeInHierarchy)
                {
                    if (playerDataBase.Portion4 > 0)
                    {
                        feverCount += (feverMaxCount * ((portion4Value * 0.01f) + portion4Plus));
                        CheckFever();

                        playerDataBase.Portion4 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);

                        playerDataBase.UseSauceCount += 1;
                        GameStateManager.instance.UseSauce += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSauceCount);

                        playerDataBase.UseSauce4 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSauce4", playerDataBase.UseSauce4);

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion2(NotionType.UsePortionNotion4);

                        FirebaseAnalytics.LogEvent("UseSauce4");
                    }
                    else
                    {
                        if (!GameStateManager.instance.Portion4Ad)
                        {
                            OpenPortionAdView(3);
                        }

                        SoundManager.instance.PlaySFX(GameSfxType.NotSources);
                        NotionManager.instance.UseNotion(NotionType.LowPortion);
                    }
                }
                break;
            case 4:
                if (!portion5)
                {
                    if (playerDataBase.Portion5 > 0)
                    {
                        portion5 = true;

                        defDestroy += portion5Value;
                        UpgradeInitialize();

                        playerDataBase.Portion5 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);

                        playerDataBase.UseSauceCount += 1;
                        GameStateManager.instance.UseSauce += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSauceCount);

                        playerDataBase.UseSauce5 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSauce5", playerDataBase.UseSauce5);

                        StartCoroutine(PortionCoroution5());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion2(NotionType.UsePortionNotion5);

                        FirebaseAnalytics.LogEvent("UseSauce5");
                    }
                    else
                    {
                        if (!GameStateManager.instance.Portion5Ad)
                        {
                            OpenPortionAdView(4);
                        }

                        SoundManager.instance.PlaySFX(GameSfxType.NotSources);
                        NotionManager.instance.UseNotion(NotionType.LowPortion);
                    }
                }
                break;
            case 5:
                if (!portion6)
                {
                    if (playerDataBase.Portion6 > 0)
                    {
                        portion6 = true;

                        needPricePlus += portion1Value;
                        sellPricePlus += portion2Value;
                        successPlus += portion3Value;
                        feverCount += (feverMaxCount * ((portion4Value * 0.01f) + portion4Plus));
                        defDestroy += portion5Value;
                        CheckFever();

                        UpgradeInitialize();

                        playerDataBase.Portion6 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion6", playerDataBase.Portion6);

                        playerDataBase.UseSauceCount += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSauceCount);

                        StartCoroutine(PortionCoroution6());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion2(NotionType.UsePortionNotion6);

                        FirebaseAnalytics.LogEvent("UseSauce6");
                    }
                    else
                    {
                        //OpenPortionAdView(5);

                        SoundManager.instance.PlaySFX(GameSfxType.NotSources);
                        NotionManager.instance.UseNotion(NotionType.LowPortion);
                    }
                }
                break;
        }

        CheckPortion();
    }

    IEnumerator PortionCoroution1()
    {
        currentTime1 = 0f;

        portionScaleAnim[0].StopAnim();

        while (currentTime1 < portion1Time)
        {
            fillAmount1 = Mathf.Lerp(1.0f, 0, currentTime1 / portion1Time);
            fillAmount1 = Mathf.Clamp01(fillAmount1);

            portionFillamount1.fillAmount = fillAmount1;

            currentTime1 += Time.deltaTime;

            yield return null;
        }

        portionScaleAnim[0].PlayAnim();

        portion1 = false;
        portionFillamount1.fillAmount = 0;

        needPricePlus -= portion1Value;
        UpgradeInitialize();
    }

    IEnumerator PortionCoroution2()
    {
        currentTime2 = 0f;

        portionScaleAnim[1].StopAnim();

        while (currentTime2 < portion2Time)
        {
            fillAmount2 = Mathf.Lerp(1.0f, 0, currentTime2 / portion2Time);
            fillAmount2 = Mathf.Clamp01(fillAmount2);

            portionFillamount2.fillAmount = fillAmount2;

            currentTime2 += Time.deltaTime;

            yield return null;
        }

        portionScaleAnim[1].PlayAnim();

        portion2 = false;
        portionFillamount2.fillAmount = 0;

        sellPricePlus -= portion2Value;
        UpgradeInitialize();
    }

    IEnumerator PortionCoroution3()
    {
        currentTime3 = 0f;

        portionScaleAnim[2].StopAnim();

        while (currentTime3 < portion3Time)
        {
            fillAmount3 = Mathf.Lerp(1.0f, 0, currentTime3 / portion3Time);
            fillAmount3 = Mathf.Clamp01(fillAmount3);

            portionFillamount3.fillAmount = fillAmount3;

            currentTime3 += Time.deltaTime;

            yield return null;
        }

        portionScaleAnim[2].PlayAnim();

        portion3 = false;
        portionFillamount3.fillAmount = 0;

        successPlus -= portion3Value;
        UpgradeInitialize();
    }

    IEnumerator PortionCoroution5()
    {
        currentTime5 = 0f;

        portionScaleAnim[4].StopAnim();

        while (currentTime5 < portion5Time)
        {
            fillAmount5 = Mathf.Lerp(1.0f, 0, currentTime5 / portion5Time);

            fillAmount5 = Mathf.Clamp01(fillAmount5);

            portionFillamount5.fillAmount = fillAmount5;

            currentTime5 += Time.deltaTime;

            yield return null;
        }

        portionScaleAnim[4].PlayAnim();

        portion5 = false;
        portionFillamount5.fillAmount = 0;

        defDestroy -= portion5Value;
        UpgradeInitialize();
    }

    IEnumerator PortionCoroution6()
    {
        currentTime6 = 0f;

        portionScaleAnim[5].StopAnim();

        while (currentTime6 < portion6Time)
        {
            fillAmount6 = Mathf.Lerp(1.0f, 0, currentTime6 / portion6Time);
            fillAmount6 = Mathf.Clamp01(fillAmount6);

            portionFillamount6.fillAmount = fillAmount6;

            currentTime6 += Time.deltaTime;

            yield return null;
        }

        portionScaleAnim[5].PlayAnim();

        portion6 = false;
        portionFillamount6.fillAmount = 0;

        needPricePlus -= portion1Value;
        sellPricePlus -= portion2Value;
        successPlus -= portion3Value;
        defDestroy -= portion5Value;
        UpgradeInitialize();
    }


    public void OnBuff(int number)
    {
        buffScaleAnim[number].StopAnim();

        switch (number)
        {
            case 0:
                buffImage[0].color = buff1Color;

                buff1 = true;

                sellPricePlus += buff1Value;
                UpgradeInitialize();
                break;
            case 1:
                buffImage[1].color = buff2Color;

                buff2 = true;

                defDestroy += buff2Value;

                if (defDestroy >= 100)
                {
                    defDestroy = 100;
                }

                defDestroyText.localizationName = "DefDestroyPercent";
                defDestroyText.plusText = " : " + defDestroy.ToString("N2") + "%";
                defDestroyText.ReLoad();

                UpgradeInitialize();
                break;
            case 2:
                buffImage[2].color = buff3Color;

                buff3 = true;

                successX2 += buff3Value;

                if (successX2 >= 100)
                {
                    successX2 = 100;
                }

                successX2Text.localizationName = "SuccessX2Percent";
                successX2Text.plusText = " : " + successX2.ToString("N1") + "%";
                successX2Text.ReLoad();

                UpgradeInitialize();
                break;
            case 3:
                buffImage[3].color = buff4Color;

                buff4 = true;

                if (!buffAutoUpgrade)
                {
                    buffAutoUpgrade = true;
                    StartCoroutine(BuffAutoUpgradeCoroution());
                }
                break;
        }

        if (guideMissionManager.guideMissonView.gameObject.activeInHierarchy)
        {
            guideMissionManager.Initialize();
        }
    }

    public void OffBuff(int number)
    {
        buffScaleAnim[number].PlayAnim();

        buffImage[number].color = buffGrayColor;

        switch (number)
        {
            case 0:
                buff1 = false;

                sellPricePlus -= buff1Value;
                UpgradeInitialize();
                break;
            case 1:
                buff2 = false;

                defDestroy -= buff2Value;

                if (defDestroy >= 100)
                {
                    defDestroy = 100;
                }

                defDestroyText.localizationName = "DefDestroyPercent";
                defDestroyText.plusText = " : " + defDestroy.ToString("N2") + "%";
                defDestroyText.ReLoad();

                UpgradeInitialize();
                break;
            case 2:
                buff3 = false;

                successX2 -= buff3Value;

                if (successX2 >= 100)
                {
                    successX2 = 100;
                }

                successX2Text.localizationName = "SuccessX2Percent";
                successX2Text.plusText = " : " + successX2.ToString("N1") + "%";
                successX2Text.ReLoad();

                UpgradeInitialize();
                break;
            case 3:
                buff4 = false;

                buffAutoUpgrade = false;
                break;
        }
    }

    public void OpenPortionAdView(int number)
    {
        if(!portionAdView.activeInHierarchy)
        {
            portionAdView.SetActive(true);

            portionAd = number;

            switch (number)
            {
                case 0:
                    portionReceiveContent.Initialize(RewardType.Portion1, 5);
                    break;
                case 1:
                    portionReceiveContent.Initialize(RewardType.Portion2, 5);
                    break;
                case 2:
                    portionReceiveContent.Initialize(RewardType.Portion3, 5);
                    break;
                case 3:
                    portionReceiveContent.Initialize(RewardType.Portion4, 5);
                    break;
                case 4:
                    portionReceiveContent.Initialize(RewardType.Portion5, 3);
                    break;
            }
        }
        else
        {
            portionAdView.SetActive(false);
        }
    }

    public void ChangeMode()
    {
        if(GameStateManager.instance.GameType == GameType.Story)
        {
            GameStart(1);
        }
        else
        {
            GameStart(0);
        }
    }


    public void WatchAd()
    {
        switch(portionAd)
        {
            case 0:
                GoogleAdsManager.instance.admobReward_Portion.ShowAd(13);
                break;
            case 1:
                GoogleAdsManager.instance.admobReward_Portion.ShowAd(14);
                break;
            case 2:
                GoogleAdsManager.instance.admobReward_Portion.ShowAd(15);
                break;
            case 3:
                GoogleAdsManager.instance.admobReward_Portion.ShowAd(16);
                break;
            case 4:
                GoogleAdsManager.instance.admobReward_Portion.ShowAd(17);
                break;
        }
    }

    public void SuccessWatchAd(int number)
    {
        portionAdView.SetActive(false);

        switch(number)
        {
            case 0:
                PortionManager.instance.GetPortion(0, 5);

                GameStateManager.instance.Portion1Ad = true;
                break;
            case 1:
                PortionManager.instance.GetPortion(1, 5);

                GameStateManager.instance.Portion2Ad = true;
                break;
            case 2:
                PortionManager.instance.GetPortion(2, 5);

                GameStateManager.instance.Portion3Ad = true;
                break;
            case 3:
                PortionManager.instance.GetPortion(3, 5);

                GameStateManager.instance.Portion4Ad = true;
                break;
            case 4:
                PortionManager.instance.GetPortion(4, 3);

                GameStateManager.instance.Portion5Ad = true;
                break;
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessWatchAd);

        CheckPortion();
    }

    public void Reincarnation()
    {
        islandImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];

        CheckFoodLevel();
        CheckFoodState();
    }

    public void OpenLoginView()
    {
        if (!loginView.activeSelf)
        {
            loginView.SetActive(true);

            loginButtonArray[0].SetActive(false);
            loginButtonArray[1].SetActive(false);
            loginButtonArray[2].SetActive(false);

#if UNITY_EDITOR || UNITY_EDITOR_OSX
            loginButtonArray[0].SetActive(true);
#elif UNITY_ANDROID
            loginButtonArray[1].SetActive(true);
#elif UNITY_IOS
            loginButtonArray[2].SetActive(true);
#endif

            if (GameStateManager.instance.StoreType == StoreType.OneStore)
            {
                loginButtonArray[0].SetActive(true);
                loginButtonArray[1].SetActive(false);
            }
        }
        else
        {
            loginView.SetActive(false);
        }
    }

    public void OpenDeleteAccount()
    {
        if (!deleteAccountView.activeSelf)
        {
            deleteAccountView.SetActive(true);
        }
        else
        {
            deleteAccountView.SetActive(false);
        }
    }

    public void GuestLogin()
    {
        PlayfabManager.instance.OnClickGuestLogin();
    }

    public void GoogleLogin()
    {
        PlayfabManager.instance.OnClickGoogleLogin();
    }

    public void AppleLogin()
    {
        PlayfabManager.instance.OnClickAppleLogin();
    }

    public void OpenAppReview()
    {
        appReview.SetActive(true);
    }

    public void CloseAppReview()
    {
        appReview.SetActive(false);
    }

    public void OnNeedUpdate()
    {
        checkUpdate.SetActive(true);
    }

    public void OpenUpdate()
    {
#if UNITY_EDITOR
        checkUpdate.SetActive(false);
#elif UNITY_ANDROID
        StartCoroutine(CheckForUpdate());
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/kr/app/food-truck-evolution/id6466390705");
#endif

        FirebaseAnalytics.LogEvent("Open_Update : " + Application.version);
    }

#if UNITY_ANDROID
    IEnumerator CheckForUpdate()
    {
        yield return new WaitForSeconds(0.5f);

        AppUpdateManager appUpdateManager = new AppUpdateManager();

        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();

        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult,appUpdateOptions);
                
                while(!startUpdateRequest.IsDone)
                {
                    if(startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        Debug.Log("업데이트 다운로드 진행중");

                    }
                    else if(startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        Debug.Log("다운로드가 완료");
                    }

                    yield return null;
                }

                var result = appUpdateManager.CompleteUpdate();

                while(!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                }

                yield return (int)startUpdateRequest.Status;
            }
            else if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                Debug.Log("업데이트가 없습니다");
            }
        }
        else
        {
            Debug.Log("업데이트 에러");
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.whilili.foodtruck");
        }
    }
#endif

    public void OpenURL()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.whilili.foodtruck");
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/kr/app/food-truck-evolution/id6466390705");
#endif

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);

        FirebaseAnalytics.LogEvent("Open_AppReview");

        CloseAppReview();
    }

    public void OpenReview()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.whilili.foodtruck");
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/kr/app/food-truck-evolution/id6466390705");
#endif

        FirebaseAnalytics.LogEvent("Open_Event_AppReview");
    }

    public void FeedBack()
    {
        if(GameStateManager.instance.Language == LanguageType.Korean)
        {
            Application.OpenURL("https://forms.gle/RtZM83MWko6aJR5c6");
        }
        else
        {
            Application.OpenURL("https://forms.gle/dvMRSQkPJpmm9iJM9");
        }

        FirebaseAnalytics.LogEvent("Open_FeedBack");
    }

    public void OpenPrivacypolicyURL()
    {
        Application.OpenURL("https://sites.google.com/view/whilili-privacypolicy");

        FirebaseAnalytics.LogEvent("Open_Privacypolicy");
    }

    public void OpenTermsURL()
    {
        Application.OpenURL("https://sites.google.com/view/whilili-terms");

        FirebaseAnalytics.LogEvent("Open_Terms");
    }

    public void Agree()
    {
        GameStateManager.instance.Privacypolicy = true;

        privacypolicyView.SetActive(false);
    }

    public void Gender_Male()
    {
        gender = 1;

        genderButton[0].sprite = buttonImg[1];
        genderButton[1].sprite = buttonImg[0];
        genderButton[2].sprite = buttonImg[0];
        genderButton[3].sprite = buttonImg[0];

        genderButton[4].sprite = buttonImg[1];
    }

    public void Gender_Female()
    {
        gender = 2;

        genderButton[0].sprite = buttonImg[0];
        genderButton[1].sprite = buttonImg[1];
        genderButton[2].sprite = buttonImg[0];
        genderButton[3].sprite = buttonImg[0];

        genderButton[4].sprite = buttonImg[1];
    }

    public void Gender_DontKnow()
    {
        gender = 3;

        genderButton[0].sprite = buttonImg[0];
        genderButton[1].sprite = buttonImg[0];
        genderButton[2].sprite = buttonImg[1];
        genderButton[3].sprite = buttonImg[0];

        genderButton[4].sprite = buttonImg[1];
    }

    public void Gender_Other()
    {
        gender = 4;

        genderButton[0].sprite = buttonImg[0];
        genderButton[1].sprite = buttonImg[0];
        genderButton[2].sprite = buttonImg[0];
        genderButton[3].sprite = buttonImg[1];

        genderButton[4].sprite = buttonImg[1];
    }

    public void Gender_Clear()
    {
        if (gender == 0) return;

        GameStateManager.instance.Gender = true;

        genderView.SetActive(false);

        switch(gender)
        {
            case 0:
                FirebaseAnalytics.LogEvent("Sex_Male");
                break;
            case 1:
                FirebaseAnalytics.LogEvent("Sex_Female");
                break;
            case 2:
                FirebaseAnalytics.LogEvent("Sex_DontKnow");
                break;
            case 3:
                FirebaseAnalytics.LogEvent("Sex_Other");
                break;
        }

        playerDataBase.Gender = gender;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Gender", playerDataBase.Gender);
    }

    public void Decline()
    {
        Application.Quit();
    }

    void WaitUpgradeDelay()
    {
        isUpgradeDelay = false;
    }

    void WaitSellDelay()
    {
        isUpgradeDelay = false;
    }


    void CheckBankruptcy()
    {
        if(playerDataBase.Coin < 1000)
        {
            bankruptcyView.SetActive(true);

            if(GameStateManager.instance.Bankruptcy < 1)
            {
                bankReceiveContent.Initialize(RewardType.Gold, 100000);
            }
            else
            {
                bankReceiveContent.Initialize(RewardType.Gold, 10000);
            }

            FirebaseAnalytics.LogEvent("Open_Bankruptcy");
        }
    }

    public void ReceiveBankruptcy()
    {
        bankruptcyView.SetActive(false);

        if (GameStateManager.instance.Bankruptcy < 1)
        {
            PlayfabManager.instance.UpdateSellPriceGold(100000);
            PlayfabManager.instance.moneyAnimation.PlusMoney(100000);
        }
        else
        {
            PlayfabManager.instance.UpdateSellPriceGold(10000);
            PlayfabManager.instance.moneyAnimation.PlusMoney(10000);
        }

        FirebaseAnalytics.LogEvent("Clear_Bankruptcy : " + GameStateManager.instance.Bankruptcy);

        SoundManager.instance.PlaySFX(GameSfxType.GetMoney);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        GameStateManager.instance.Bankruptcy += 1;
    }

    public void OpenDeveloperMode()
    {
        if(!testMode.activeInHierarchy)
        {
            testMode.SetActive(true);
            testModeText.text = "Cheat Mode OFF";
        }
        else
        {
            testMode.SetActive(false);
            testModeText.text = "Cheat Mode ON";
        }
    }


    public void GetIsland()
    {
        playerDataBase.IslandNumber = System.Enum.GetValues(typeof(IslandType)).Length - 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);
    }

    public void GetFood()
    {
        playerDataBase.NextFoodNumber = System.Enum.GetValues(typeof(FoodType)).Length - 1;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
    }

    public void GetMaxLevel()
    {
        playerDataBase.Skill1 = 500;
        playerDataBase.Skill2 = 500;
        playerDataBase.Skill3 = 500;
        playerDataBase.Skill4 = 500;
        playerDataBase.Skill5 = 500;
        playerDataBase.Skill6 = 500;
        playerDataBase.Skill7 = 500;
        playerDataBase.Skill8 = 500;
        playerDataBase.Skill9 = 500;
        playerDataBase.Skill10 = 100;
        playerDataBase.Skill11 = 500;
        playerDataBase.Skill12 = 500;
        playerDataBase.Skill13 = 500;
        playerDataBase.Skill14 = 100;
        playerDataBase.Skill15 = 100;
        playerDataBase.Skill16 = 100;
        playerDataBase.Skill17 = 100;
        playerDataBase.Skill18 = 100;
        playerDataBase.Skill19 = 100;

        playerDataBase.Treasure1 = 200;
        playerDataBase.Treasure2 = 200;
        playerDataBase.Treasure3 = 200;
        playerDataBase.Treasure4 = 200;
        playerDataBase.Treasure5 = 200;
        playerDataBase.Treasure6 = 200;
        playerDataBase.Treasure7 = 200;
        playerDataBase.Treasure8 = 200;
        playerDataBase.Treasure9 = 200;
        playerDataBase.Treasure10 = 200;
        playerDataBase.Treasure11 = 200;
        playerDataBase.Treasure12 = 200;
        playerDataBase.Treasure13 = 200;
        playerDataBase.Treasure14 = 200;
        playerDataBase.Treasure15 = 200;

        playerDataBase.Level = 300;
        playerDataBase.CastleLevel = 300;
        playerDataBase.Proficiency = 100;
    }

    public void GetZeroLevel()
    {
        playerDataBase.Skill1 = 0;
        playerDataBase.Skill2 = 0;
        playerDataBase.Skill3 = 0;
        playerDataBase.Skill4 = 0;
        playerDataBase.Skill5 = 0;
        playerDataBase.Skill6 = 0;
        playerDataBase.Skill7 = 0;
        playerDataBase.Skill8 = 0;
        playerDataBase.Skill9 = 0;
        playerDataBase.Skill10 = 0;
        playerDataBase.Skill11 = 0;
        playerDataBase.Skill12 = 0;
        playerDataBase.Skill13 = 0;
        playerDataBase.Skill14 = 0;
        playerDataBase.Skill15 = 0;
        playerDataBase.Skill16 = 0;
        playerDataBase.Skill17 = 0;
        playerDataBase.Skill18 = 0;
        playerDataBase.Skill19 = 0;

        playerDataBase.Treasure1 = 0;
        playerDataBase.Treasure2 = 0;
        playerDataBase.Treasure3 = 0;
        playerDataBase.Treasure4 = 0;
        playerDataBase.Treasure5 = 0;
        playerDataBase.Treasure6 = 0;
        playerDataBase.Treasure7 = 0;
        playerDataBase.Treasure8 = 0;
        playerDataBase.Treasure9 = 0;
        playerDataBase.Treasure10 = 0;
        playerDataBase.Treasure11 = 0;
        playerDataBase.Treasure12 = 0;
        playerDataBase.Treasure13 = 0;
        playerDataBase.Treasure14 = 0;
        playerDataBase.Treasure15 = 0;
    }

    public void GetResetReward()
    {
        playerDataBase.AttendanceCheck = false;
        playerDataBase.WelcomeCheck = false;

        playerDataBase.resetInfo = new ResetInfo();

        GameStateManager.instance.UpgradeCount = 0;
        GameStateManager.instance.SellCount = 0;
        GameStateManager.instance.UseSauce = 0;
        GameStateManager.instance.OpenChestBox = 0;
        GameStateManager.instance.YummyTimeCount = 0;
        GameStateManager.instance.ChestBoxCount = 0;

        playerDataBase.PlayTimeCount = 0;
        playerDataBase.RankEventCount = 0;

        GameStateManager.instance.Portion1Ad = false;
        GameStateManager.instance.Portion2Ad = false;
        GameStateManager.instance.Portion3Ad = false;
        GameStateManager.instance.Portion4Ad = false;
        GameStateManager.instance.Portion5Ad = false;
    }

    public void GetGold()
    {
        PlayfabManager.instance.UpdateAddGold(1000000000);
    }

    public void GetCrystal()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1000000);
    }

    public void GetPortion()
    {
        PortionManager.instance.GetAllPortion(999);

        playerDataBase.Portion5 += 999;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
    }

    public void GetTicket()
    {
        PortionManager.instance.GetBuffTickets(999);
        PortionManager.instance.GetDefTickets(999);
        PortionManager.instance.GetRepairTickets(999);
    }

    public void GetDungeonKey()
    {
        playerDataBase.DungeonKey1 += 999;
        playerDataBase.DungeonKey2 += 999;
        playerDataBase.DungeonKey3 += 999;
        playerDataBase.DungeonKey4 += 999;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey1", playerDataBase.DungeonKey1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey2", playerDataBase.DungeonKey2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey3", playerDataBase.DungeonKey3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DungeonKey4", playerDataBase.DungeonKey4);
    }

    public void GetPoint()
    {
        playerDataBase.RankPoint += 100000;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

        playerDataBase.AbilityPoint += 100000;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AbilityPoint", playerDataBase.AbilityPoint);

        playerDataBase.ChallengePoint += 100000;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("ChallengePoint", playerDataBase.ChallengePoint);
    }

    public void GetGourmetLevel()
    {
        playerDataBase.GourmetLevel += 1000000;
    }

    public void GetUnLocked()
    {
        lockManager.UnLocked(8);
    }

    public void GetExp()
    {
        playerDataBase.Exp += 1000000;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);

        levelManager.Initialize();

        CheckLocked();
    }

    public void PortionInfo(int number)
    {
        switch(number)
        {
            case 0:
                NotionManager.instance.UseNotion(NotionType.Portion1_Info);
                break;
            case 1:
                NotionManager.instance.UseNotion(NotionType.Portion2_Info);
                break;
            case 2:
                NotionManager.instance.UseNotion(NotionType.Portion3_Info);
                break;
            case 3:
                NotionManager.instance.UseNotion(NotionType.Portion4_Info);
                break;
            case 4:
                NotionManager.instance.UseNotion(NotionType.Portion5_Info);
                break;
            case 5:
                NotionManager.instance.UseNotion(NotionType.PortionInfo6);
                break;
        }
    }

    public void TipInfo()
    {
        NotionManager.instance.UseNotion2(Color.yellow, LocalizationManager.instance.GetString("SellPriceX2Up") + " : " + sellPriceTip.ToString("N1") +"%"
            + "\n(" + LocalizationManager.instance.GetString("SellPriceX2Up_Info")+")");
    }

    public void LockedInfo(int number)
    {
        SoundManager.instance.PlaySFX(GameSfxType.Wrong);

        switch(number)
        {
            case 0:
                NotionManager.instance.UseNotion(NotionType.UnLockedNotion1);
                break;
            case 1:
                NotionManager.instance.UseNotion(NotionType.UnLockedNotion2);
                break;
            case 2:
                NotionManager.instance.UseNotion(NotionType.UnLockedNotion3);
                break;
            case 3:
                NotionManager.instance.UseNotion(NotionType.UnLockedNotion4);
                break;
            case 4:
                NotionManager.instance.UseNotion(NotionType.UnLockedNotion5);
                break;
        }
    }

    public void GetUpgradeCount()
    {
        playerDataBase.UpgradeCount += 100000;

        //PlayfabManager.instance.UpdatePlayerStatisticsInsert("UpgradeCount", playerDataBase.UpgradeCount);
    }

    public void CheckLocked()
    {
        treasureLocked.SetActive(true);
        rankLocked.SetActive(true);
        inGameRankLocked.SetActive(true);

        if (playerDataBase.Level > 4)
        {
            treasureLocked.SetActive(false);
        }

        if (playerDataBase.Level > 9)
        {
            rankLocked.SetActive(false);
            inGameRankLocked.SetActive(false);
        }
    }

    public void OpenRankingNoticeView()
    {
        if(!rankingNoticeView.activeInHierarchy)
        {
            rankingNoticeView.SetActive(true);
        }
        else
        {
            rankingNoticeView.SetActive(false);
        }
    }

    public void DeveloperON()
    {
        GameStateManager.instance.Developer = true;

        rareFoodPercent = 100;
    }

    public void DeveloperOff()
    {
        GameStateManager.instance.Developer = false;

        rareFoodPercent = 10;
    }

    [Button]
    public void OpenSupportPackage()
    {
        shopManager.OpenPackage(PackageType.Package7);
    }

    public void GetEnableAllItem()
    {
        playerDataBase.Truck2 = 1;
        playerDataBase.Truck3 = 1;
        playerDataBase.Truck4 = 1;
        playerDataBase.Truck6 = 1;
        playerDataBase.Truck7 = 1;
        playerDataBase.Truck8 = 1;
        playerDataBase.Truck9 = 1;
        playerDataBase.Truck10 = 1;

        playerDataBase.Animal2 = 1;
        playerDataBase.Animal3 = 1;
        playerDataBase.Animal4 = 1;
        playerDataBase.Animal5 = 1;
        playerDataBase.Animal6 = 1;
        playerDataBase.Animal7 = 1;
        playerDataBase.Animal8 = 1;

        playerDataBase.Butterfly2 = 1;
        playerDataBase.Butterfly3 = 1;
        playerDataBase.Butterfly4 = 1;
        playerDataBase.Butterfly5 = 1;
        playerDataBase.Butterfly6 = 1;
        playerDataBase.Butterfly7 = 1;
        playerDataBase.Butterfly8 = 1;
        playerDataBase.Butterfly9 = 1;
        playerDataBase.Butterfly10 = 1;
        playerDataBase.Butterfly11 = 1;
        playerDataBase.Butterfly12 = 1;
        playerDataBase.Butterfly13 = 1;
        playerDataBase.Butterfly14 = 1;
        playerDataBase.Butterfly15 = 1;
        playerDataBase.Butterfly16 = 1;
        playerDataBase.Butterfly17 = 1;
        playerDataBase.Butterfly18 = 1;
        playerDataBase.Butterfly19 = 1;
        playerDataBase.Butterfly20 = 1;
        playerDataBase.Butterfly21 = 1;
        playerDataBase.Butterfly22 = 1;
        playerDataBase.Butterfly23 = 1;
        playerDataBase.Butterfly24 = 1;
        playerDataBase.Butterfly25 = 1;
        playerDataBase.Butterfly26 = 1;
        playerDataBase.Butterfly27 = 1;
        playerDataBase.Butterfly28 = 1;

        playerDataBase.Character2 = 1;
        playerDataBase.Character3 = 1;
        playerDataBase.Character4 = 1;
        playerDataBase.Character5 = 1;
        playerDataBase.Character6 = 1;
        playerDataBase.Character7 = 1;
        playerDataBase.Character8 = 1;
        playerDataBase.Character9 = 1;
        playerDataBase.Character10 = 1;
        playerDataBase.Character11 = 1;
        playerDataBase.Character12 = 1;
        playerDataBase.Character13 = 1;
        playerDataBase.Character14 = 1;
        playerDataBase.Character15 = 1;
        playerDataBase.Character16 = 1;
        playerDataBase.Character17 = 1;
        playerDataBase.Character18 = 1;
        playerDataBase.Character19 = 1;
        playerDataBase.Character20 = 1;
        playerDataBase.Character21 = 1;

        playerDataBase.Totems2 = 1;
        playerDataBase.Totems3 = 1;
        playerDataBase.Totems4 = 1;
        playerDataBase.Totems5 = 1;
        playerDataBase.Totems6 = 1;
        playerDataBase.Totems7 = 1;
        playerDataBase.Totems8 = 1;
        playerDataBase.Totems9 = 1;
        playerDataBase.Totems10 = 1;
        playerDataBase.Totems11 = 1;
        playerDataBase.Totems12 = 1;

        for(int i = 0; i < playerDataBase.Bucket.Length; i ++)
        {
            playerDataBase.Bucket[i] = 1;
        }

        for (int i = 0; i < playerDataBase.Chair.Length; i++)
        {
            playerDataBase.Chair[i] = 1;
        }

        for (int i = 0; i < playerDataBase.Tube.Length; i++)
        {
            playerDataBase.Tube[i] = 1;
        }

        for (int i = 0; i < playerDataBase.Surfboard.Length; i++)
        {
            playerDataBase.Surfboard[i] = 1;
        }

        for (int i = 0; i < playerDataBase.Umbrella.Length; i++)
        {
            playerDataBase.Umbrella[i] = 1;
        }
    }

    public void GetDisableAllItem()
    {
        playerDataBase.Truck1 = 1;
        playerDataBase.Truck2 = 0;
        playerDataBase.Truck3 = 0;
        playerDataBase.Truck4 = 0;
        playerDataBase.Truck6 = 0;
        playerDataBase.Truck7 = 0;
        playerDataBase.Truck8 = 0;
        playerDataBase.Truck9 = 0;
        playerDataBase.Truck10 = 0;

        playerDataBase.Animal1 = 1;
        playerDataBase.Animal2 = 0;
        playerDataBase.Animal3 = 0;
        playerDataBase.Animal4 = 0;
        playerDataBase.Animal5 = 0;
        playerDataBase.Animal6 = 0;
        playerDataBase.Animal7 = 0;
        playerDataBase.Animal8 = 0;

        playerDataBase.Butterfly1 = 1;
        playerDataBase.Butterfly2 = 0;
        playerDataBase.Butterfly3 = 0;
        playerDataBase.Butterfly4 = 0;
        playerDataBase.Butterfly5 = 0;
        playerDataBase.Butterfly6 = 0;
        playerDataBase.Butterfly7 = 0;
        playerDataBase.Butterfly8 = 0;
        playerDataBase.Butterfly9 = 0;
        playerDataBase.Butterfly10 = 0;
        playerDataBase.Butterfly11 = 0;
        playerDataBase.Butterfly12 = 0;
        playerDataBase.Butterfly13 = 0;
        playerDataBase.Butterfly14 = 0;
        playerDataBase.Butterfly15 = 0;
        playerDataBase.Butterfly16 = 0;
        playerDataBase.Butterfly17 = 0;
        playerDataBase.Butterfly18 = 0;
        playerDataBase.Butterfly19 = 0;
        playerDataBase.Butterfly20 = 0;
        playerDataBase.Butterfly21 = 0;
        playerDataBase.Butterfly22 = 0;
        playerDataBase.Butterfly23 = 0;
        playerDataBase.Butterfly24 = 0;
        playerDataBase.Butterfly25 = 0;
        playerDataBase.Butterfly26 = 0;
        playerDataBase.Butterfly27 = 0;
        playerDataBase.Butterfly28 = 0;

        playerDataBase.Character1 = 1;
        playerDataBase.Character2 = 0;
        playerDataBase.Character3 = 0;
        playerDataBase.Character4 = 0;
        playerDataBase.Character5 = 0;
        playerDataBase.Character6 = 0;
        playerDataBase.Character7 = 0;
        playerDataBase.Character8 = 0;
        playerDataBase.Character9 = 0;
        playerDataBase.Character10 = 0;
        playerDataBase.Character11 = 0;
        playerDataBase.Character12 = 0;
        playerDataBase.Character13 = 0;
        playerDataBase.Character14 = 0;
        playerDataBase.Character15 = 0;
        playerDataBase.Character16 = 0;
        playerDataBase.Character17 = 0;
        playerDataBase.Character18 = 0;
        playerDataBase.Character19 = 0;
        playerDataBase.Character20 = 0;
        playerDataBase.Character21 = 0;

        playerDataBase.Totems1 = 1;
        playerDataBase.Totems2 = 0;
        playerDataBase.Totems3 = 0;
        playerDataBase.Totems4 = 0;
        playerDataBase.Totems5 = 0;
        playerDataBase.Totems6 = 0;
        playerDataBase.Totems7 = 0;
        playerDataBase.Totems8 = 0;
        playerDataBase.Totems9 = 0;
        playerDataBase.Totems10 = 0;
        playerDataBase.Totems11 = 0;
        playerDataBase.Totems12 = 0;

        for (int i = 0; i < playerDataBase.Bucket.Length; i++)
        {
            playerDataBase.Bucket[i] = 0;
        }

        for (int i = 0; i < playerDataBase.Chair.Length; i++)
        {
            playerDataBase.Chair[i] = 0;
        }

        for (int i = 0; i < playerDataBase.Tube.Length; i++)
        {
            playerDataBase.Tube[i] = 0;
        }

        for (int i = 0; i < playerDataBase.Surfboard.Length; i++)
        {
            playerDataBase.Surfboard[i] = 0;
        }

        for (int i = 0; i < playerDataBase.Umbrella.Length; i++)
        {
            playerDataBase.Umbrella[i] = 0;
        }
    }

    public void DeleteAllItem()
    {
        playerDataBase.Portion1 = 0;
        playerDataBase.Portion2 = 0;
        playerDataBase.Portion3 = 0;
        playerDataBase.Portion4 = 0;
        playerDataBase.Portion5 = 0;
        playerDataBase.Portion6 = 0;

        playerDataBase.DungeonKey1 = 0;
        playerDataBase.DungeonKey2 = 0;
        playerDataBase.DungeonKey3 = 0;
        playerDataBase.DungeonKey4 = 0;

        playerDataBase.BuffCount = 0;
        playerDataBase.SkillTicket = 0;
        playerDataBase.RecoverTicket = 0;

        playerDataBase.DefDestroyTicket = 0;
        playerDataBase.DefDestroyTicketPiece = 0;

        playerDataBase.RankPoint = 0;
        playerDataBase.ChallengePoint = 0;
        playerDataBase.AbilityPoint = 0;
    }
}
