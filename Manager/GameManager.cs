using Firebase.Analytics;
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

    public GameObject privacypolicyView;
    public GameObject genderView;

    public Sprite[] buttonImg;
    public Image[] genderButton;

    public GameObject coupon;
    public GameObject deleteAccount;

    public GameObject changeFoodAlarmObj;

    public Image changeFoodImg;
    public Image islandImg;

    public GameObject portion6Obj;

    public LocalizationContent bestRankLevelText;

    public GameObject butterflyLocked;
    public GameObject rankLocked;
    public GameObject treasureLocked;

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

    [Space]
    [Title("Buff")]
    public Text buff1Text;
    public Text buff2Text;
    public Text buff3Text;

    private int buff1Value = 50;
    private int buff2Value = 15;
    private int buff3Value = 10;

    private bool buff1 = false;
    private bool buff2 = false;
    private bool buff3 = false;
    private bool buff4 = false;

    [Space]
    [Title("Christmas")]
    public GameObject christmasSnow;

    [Space]
    [Title("Truck")]
    public Animator[] mainTruckArray;

    [Space]
    [Title("Food")]
    public FoodContent[] hamburgerArray;
    public FoodContent[] sandwichArray;
    public FoodContent[] snackLabArray;
    public FoodContent[] drinkArray;
    public FoodContent[] pizzaArray;
    public FoodContent[] donutArray;
    public FoodContent[] friesArray;
    public FoodContent[] ribsArray;

    [Space]
    [Title("Candy")]
    public FoodContent[] candy1Array;
    public FoodContent[] candy2Array;
    public FoodContent[] candy3Array;
    public FoodContent[] candy4Array;
    public FoodContent[] candy5Array;
    public FoodContent[] candy6Array;
    public FoodContent[] candy7Array;
    public FoodContent[] candy8Array;
    public FoodContent[] candy9Array;
    public FoodContent[] candy10Array;

    [Space]
    [Title("Japanese Food")]
    public FoodContent[] japaneseFood1Array;
    public FoodContent[] japaneseFood2Array;
    public FoodContent[] japaneseFood3Array;
    public FoodContent[] japaneseFood4Array;
    public FoodContent[] japaneseFood5Array;
    public FoodContent[] japaneseFood6Array;
    public FoodContent[] japaneseFood7Array;
    public FoodContent[] japaneseFood8Array;

    [Space]
    [Title("Dessert")]
    public FoodContent[] dessert1Array;
    public FoodContent[] dessert2Array;
    public FoodContent[] dessert3Array;
    public FoodContent[] dessert4Array;
    public FoodContent[] dessert5Array;
    public FoodContent[] dessert6Array;
    public FoodContent[] dessert7Array;
    public FoodContent[] dessert8Array;
    public FoodContent[] dessert9Array;
    public FoodContent[] dessert10Array;

    [Space]
    public List<FoodContent> foodArrayList = new List<FoodContent>();

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

    public Text myMoneyPlusText;

    public Text portionText1, portionText2, portionText3, portionText4, portionText5, portionText6;
    public Image portionFillamount1, portionFillamount2, portionFillamount3, portionFillamount4, portionFillamount5, portionFillamount6;
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
    private float feverMaxCount = 400;
    private float feverTime = 0;
    private float feverPlus = 3;

    private float defDestroy = 0;
    private float defDestroyPlus = 100;

    private int need = 0;
    private float needPlus = 0;

    private int sellPrice = 0;
    private float sellPricePlus = 0;

    private float sellPriceTip = 0;
    private int expUp = 0;

    private float success = 0;
    private float successPlus = 0;

    private float successX2 = 0;

    private float speicalFoodCount = 0;
    private float speicalFoodNeedCount = 1;
    private float speicalFoodPlus = 10f;

    private float portion1Time = 0;
    private float portion2Time = 0;
    private float portion3Time = 0;
    private float portion4Plus = 0;
    private float portion5Time = 0;
    private float portion6Time = 0;

    private int portion1Value = 40;
    private int portion2Value = 20;
    private int portion3Value = 3;
    private int portion4Value = 40;
    private int portion5Value = 10;
    private int portion6Value = 0;

    private int level = 0;
    private int nextLevel = 0;
    private int maxLevel = 0;
    private int recoverLevel = 0;
    private int playTime = 0;

    private int supportCount = 0;
    private int supportMaxCount = 999;

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
    protected float repairTicket = 10.0f;
    protected float eventTicket = 3.0f;

    private bool clickDelay = false;
    private bool isReady = false;
    private bool auto = false;
    private bool buffAutoUpgrade = false;
    private bool speicalFood = false;
    private bool isWeekend = false;

    private bool checkInGame = false;

    public GameObject buff4Obj;

    [Space]
    [Title("Bankruptcy")]
    public GameObject bankruptcyView;
    public Text bankruptcyText;

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

    UpgradeFood upgradeFood;
    UpgradeCandy upgradeCandy;
    UpgradeJapaneseFood upgradeJapaneseFood;
    UpgradeDessert upgradeDessert;

    Sprite[] islandArray;

    public ParticleSystem[] level1UpParticle;
    public ParticleSystem[] level5UpParticle;
    public ParticleSystem[] levelMaxParticle;
    public ParticleSystem bombAllPartice;
    public ParticleSystem[] bombPartice;
    public ParticleSystem yummyTimeParticle;
    public ParticleSystem[] yummyTime2Particle;
    public ParticleSystem speicalFoodParticle;

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

    UpgradeDataBase upgradeDataBase;
    PlayerDataBase playerDataBase;
    CharacterDataBase characterDataBase;
    TruckDataBase truckDataBase;
    AnimalDataBase animalDataBase;
    ButterflyDataBase butterflyDataBase;
    TotemsDataBase totemsDataBase;
    FlowerDataBase flowerDataBase;

    IslandDataBase islandDataBase;
    ImageDataBase imageDataBase;
    LevelDataBase levelDataBase;

    WaitForSeconds serverSeconds = new WaitForSeconds(2.0f);
    WaitForSeconds timerSeconds = new WaitForSeconds(1.0f);
    WaitForSeconds firstSeconds = new WaitForSeconds(0.5f);
    WaitForSeconds autoUpgradeSecond = new WaitForSeconds(0.4f);
    WaitForSeconds buffUpgradeSecond = new WaitForSeconds(0.5f);

    private float delay = 0.2f;

    DateTime currentDate = DateTime.Now;
    DateTime decemberStart;
    DateTime decemberEnd;

    private void Awake()
    {
        instance = this;

        QualitySettings.SetQualityLevel(2);

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
        if (flowerDataBase == null) flowerDataBase = Resources.Load("FlowerDataBase") as FlowerDataBase;

        islandArray = imageDataBase.GetIslandArray();

        goldText.text = "0";
        crystalText.text = "0";
        signText.text = "0";

        loginView.SetActive(false);
        deleteAccountView.SetActive(false);
        appReview.SetActive(false);
        checkInternet.SetActive(false);
        rankingNoticeView.SetActive(false);

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
            bombAllPartice.gameObject.SetActive(false);
            bombPartice[i].gameObject.SetActive(false);
            yummyTime2Particle[i].gameObject.SetActive(false);
        }

        yummyTimeParticle.gameObject.SetActive(false);
        speicalFoodParticle.gameObject.SetActive(false);

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

        defTicketObj.SetActive(false);
        bankruptcyView.SetActive(false);
        privacypolicyView.SetActive(false);
        genderView.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(false);
        testMode.SetActive(false);

        checkUpdate.SetActive(false);

        christmasSnow.SetActive(false);

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
            if (inGameUI.activeInHierarchy)
            {
                GameStateManager.instance.FeverCount = feverCount;
            }

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
            GameStateManager.instance.Recorder = false;
#endif

        if (!GameStateManager.instance.Privacypolicy)
        {
            privacypolicyView.SetActive(true);
        }

        if (GameStateManager.instance.BackgroundEffect)
        {
            if (currentDate >= decemberStart || currentDate <= decemberEnd)
            {
                christmasSnow.SetActive(true);

                Debug.LogError("12월 달이라 눈이 내리기 시작합니다.");
            }
        }
    }

    public void BackgroundEffect(bool check)
    {
        if (check)
        {
            if (currentDate >= decemberStart || currentDate <= decemberEnd)
            {
                christmasSnow.SetActive(true);
            }
        }
        else
        {
            christmasSnow.SetActive(false);
        }
    }

    public void SuccessLogin()
    {
        if (Application.systemLanguage == SystemLanguage.Korean)
        {
            PlayfabManager.instance.SetProfileLanguage("ko");
        }
        else
        {
            PlayfabManager.instance.SetProfileLanguage("en");
        }

        if (!GameStateManager.instance.Gender && playerDataBase.Gender == 0)
        {
            genderView.SetActive(true);
        }

        isReady = false;
        isDelay_Camera = true;

        checkInternet.SetActive(false);
        loginView.SetActive(false);

        CheckPurchase();

        SetFoodContent();

        buff1Text.text = "+" + buff1Value.ToString() + "%";
        buff2Text.text = "+" + buff2Value.ToString() + "%";
        buff3Text.text = "+" + buff3Value.ToString() + "%";

        islandImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];

        feverText.text = LocalizationManager.instance.GetString("FeverGauge") + "  0%";

        isExp = levelManager.CheckMaxLevel();

        nowExp = playerDataBase.Exp;
        nowUpgradeCount = playerDataBase.UpgradeCount;
        nowSellCount = playerDataBase.SellCount;

        CheckFood();
        CheckFoodState();

        if (playerDataBase.InGameTutorial == 0)
        {
            tutorialManager.TutorialStart();
        }

        levelManager.Initialize();

        bestRankLevelText.localizationName = "TotalLevel";
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

        changeFoodManager.CheckProficiency();
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

        supportCount = GameStateManager.instance.SupportCount;

        if(GameStateManager.instance.SaveGold > 0)
        {
            PlayfabManager.instance.UpdateAddGold((int)GameStateManager.instance.SaveGold);
        }
        else if(GameStateManager.instance.SaveGold < 0)
        {
            PlayfabManager.instance.UpdateSubtractGold((int)GameStateManager.instance.SaveGold);
        }

        GameStateManager.instance.SaveGold = 0;

        Invoke("ServerDelay", 1.0f);
    }

    void ServerDelay()
    {
        isReady = true;

        PlayfabManager.instance.GetTitleInternalData("Coupon", CheckCoupon);

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Version", int.Parse(Application.version.Replace(".", "")));

#if UNITY_ANDROID
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OS", 0);
#elif UNITY_IOS
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OS", 1);
#else
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("OS", 2);
#endif
    }

    public void CheckPurchase()
    {
        removeAds.SetActive(false);
        goldx2.SetActive(false);
        superOffline.SetActive(false);
        autoUpgrade.SetActive(false);
        autoPresent.SetActive(false);

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
    }

    void SetFoodContent()
    {
        for (int i = 0; i < hamburgerArray.Length; i++)
        {
            foodArrayList.Add(hamburgerArray[i]);
        }
        for (int i = 0; i < sandwichArray.Length; i++)
        {
            foodArrayList.Add(sandwichArray[i]);
        }
        for (int i = 0; i < snackLabArray.Length; i++)
        {
            foodArrayList.Add(snackLabArray[i]);
        }
        for (int i = 0; i < drinkArray.Length; i++)
        {
            foodArrayList.Add(drinkArray[i]);
        }
        for (int i = 0; i < pizzaArray.Length; i++)
        {
            foodArrayList.Add(pizzaArray[i]);
        }
        for (int i = 0; i < donutArray.Length; i++)
        {
            foodArrayList.Add(donutArray[i]);
        }
        for (int i = 0; i < friesArray.Length; i++)
        {
            foodArrayList.Add(friesArray[i]);
        }
        for (int i = 0; i < ribsArray.Length; i++)
        {
            foodArrayList.Add(ribsArray[i]);
        }

        for (int i = 0; i < candy1Array.Length; i++)
        {
            foodArrayList.Add(candy1Array[i]);
        }
        for (int i = 0; i < candy2Array.Length; i++)
        {
            foodArrayList.Add(candy2Array[i]);
        }
        for (int i = 0; i < candy3Array.Length; i++)
        {
            foodArrayList.Add(candy3Array[i]);
        }
        for (int i = 0; i < candy4Array.Length; i++)
        {
            foodArrayList.Add(candy4Array[i]);
        }
        for (int i = 0; i < candy5Array.Length; i++)
        {
            foodArrayList.Add(candy5Array[i]);
        }
        for (int i = 0; i < candy6Array.Length; i++)
        {
            foodArrayList.Add(candy6Array[i]);
        }
        for (int i = 0; i < candy7Array.Length; i++)
        {
            foodArrayList.Add(candy7Array[i]);
        }
        for (int i = 0; i < candy8Array.Length; i++)
        {
            foodArrayList.Add(candy8Array[i]);
        }
        for (int i = 0; i < candy9Array.Length; i++)
        {
            foodArrayList.Add(candy9Array[i]);
        }
        for (int i = 0; i < candy10Array.Length; i++)
        {
            foodArrayList.Add(candy10Array[i]);
        }

        for (int i = 0; i < japaneseFood1Array.Length; i++)
        {
            foodArrayList.Add(japaneseFood1Array[i]);
        }
        for (int i = 0; i < japaneseFood2Array.Length; i++)
        {
            foodArrayList.Add(japaneseFood2Array[i]);
        }
        for (int i = 0; i < japaneseFood3Array.Length; i++)
        {
            foodArrayList.Add(japaneseFood3Array[i]);
        }
        for (int i = 0; i < japaneseFood4Array.Length; i++)
        {
            foodArrayList.Add(japaneseFood4Array[i]);
        }
        for (int i = 0; i < japaneseFood5Array.Length; i++)
        {
            foodArrayList.Add(japaneseFood5Array[i]);
        }
        for (int i = 0; i < japaneseFood6Array.Length; i++)
        {
            foodArrayList.Add(japaneseFood6Array[i]);
        }
        for (int i = 0; i < japaneseFood7Array.Length; i++)
        {
            foodArrayList.Add(japaneseFood7Array[i]);
        }
        for (int i = 0; i < japaneseFood8Array.Length; i++)
        {
            foodArrayList.Add(japaneseFood8Array[i]);
        }

        for (int i = 0; i < dessert1Array.Length; i++)
        {
            foodArrayList.Add(dessert1Array[i]);
        }
        for (int i = 0; i < dessert2Array.Length; i++)
        {
            foodArrayList.Add(dessert2Array[i]);
        }
        for (int i = 0; i < dessert3Array.Length; i++)
        {
            foodArrayList.Add(dessert3Array[i]);
        }
        for (int i = 0; i < dessert4Array.Length; i++)
        {
            foodArrayList.Add(dessert4Array[i]);
        }
        for (int i = 0; i < dessert5Array.Length; i++)
        {
            foodArrayList.Add(dessert5Array[i]);
        }
        for (int i = 0; i < dessert6Array.Length; i++)
        {
            foodArrayList.Add(dessert6Array[i]);
        }
        for (int i = 0; i < dessert7Array.Length; i++)
        {
            foodArrayList.Add(dessert7Array[i]);
        }
        for (int i = 0; i < dessert8Array.Length; i++)
        {
            foodArrayList.Add(dessert8Array[i]);
        }
        for (int i = 0; i < dessert9Array.Length; i++)
        {
            foodArrayList.Add(dessert9Array[i]);
        }
        for (int i = 0; i < dessert10Array.Length; i++)
        {
            foodArrayList.Add(dessert10Array[i]);
        }
    }

    void CheckFood()
    {
        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Food1:
                        level = GameStateManager.instance.Food1Level;
                        nextLevel = (GameStateManager.instance.Food1Level + 1) / 5;

                        if (hamburgerArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.Food2:
                        level = GameStateManager.instance.Food2Level;
                        nextLevel = (GameStateManager.instance.Food2Level + 1) / 5;

                        if (sandwichArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.Food3:
                        level = GameStateManager.instance.Food3Level;
                        nextLevel = (GameStateManager.instance.Food3Level + 1) / 5;

                        if (snackLabArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.Food4:
                        level = GameStateManager.instance.Food4Level;
                        nextLevel = (GameStateManager.instance.Food4Level + 1) / 5;

                        if (drinkArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.Food5:
                        level = GameStateManager.instance.Food5Level;
                        nextLevel = (GameStateManager.instance.Food5Level + 1) / 5;

                        if (pizzaArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.Food6:
                        level = GameStateManager.instance.Food6Level;
                        nextLevel = (GameStateManager.instance.Food6Level + 1) / 5;

                        if (donutArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }

                        break;
                    case FoodType.Food7:
                        level = GameStateManager.instance.Food7Level;
                        nextLevel = (GameStateManager.instance.Food7Level + 1) / 5;

                        if (friesArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }

                        break;
                    case FoodType.Ribs:
                        level = GameStateManager.instance.Food8Level;
                        nextLevel = (GameStateManager.instance.Food8Level + 1) / 5;

                        if (ribsArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }

                        break;
                }
                break;
            case IslandType.Island2:
                switch (GameStateManager.instance.CandyType)
                {
                    case CandyType.Candy1:
                        level = GameStateManager.instance.Candy1Level;
                        nextLevel = (GameStateManager.instance.Candy1Level + 1) / 5;

                        if (candy1Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case CandyType.Candy2:
                        level = GameStateManager.instance.Candy2Level;
                        nextLevel = (GameStateManager.instance.Candy2Level + 1) / 5;

                        if (candy2Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case CandyType.Candy3:
                        level = GameStateManager.instance.Candy3Level;
                        nextLevel = (GameStateManager.instance.Candy3Level + 1) / 5;

                        if (candy3Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case CandyType.Candy4:
                        level = GameStateManager.instance.Candy4Level;
                        nextLevel = (GameStateManager.instance.Candy4Level + 1) / 5;

                        if (candy4Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case CandyType.Candy5:
                        level = GameStateManager.instance.Candy5Level;
                        nextLevel = (GameStateManager.instance.Candy5Level + 1) / 5;

                        if (candy5Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case CandyType.Candy6:
                        level = GameStateManager.instance.Candy6Level;
                        nextLevel = (GameStateManager.instance.Candy6Level + 1) / 5;

                        if (candy6Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case CandyType.Candy7:
                        level = GameStateManager.instance.Candy7Level;
                        nextLevel = (GameStateManager.instance.Candy7Level + 1) / 5;

                        if (candy7Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case CandyType.Candy8:
                        level = GameStateManager.instance.Candy8Level;
                        nextLevel = (GameStateManager.instance.Candy8Level + 1) / 5;

                        if (candy8Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case CandyType.Candy9:
                        level = GameStateManager.instance.Candy9Level;
                        nextLevel = (GameStateManager.instance.Candy9Level + 1) / 5;

                        if (candy9Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case CandyType.Chocolate:
                        level = GameStateManager.instance.Candy10Level;
                        nextLevel = (GameStateManager.instance.Candy10Level + 1) / 5;

                        if (candy10Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                }
                break;
            case IslandType.Island3:
                switch (GameStateManager.instance.JapaneseFoodType)
                {
                    case JapaneseFoodType.JapaneseFood1:
                        level = GameStateManager.instance.JapaneseFood1Level;
                        nextLevel = (GameStateManager.instance.JapaneseFood1Level + 1) / 5;

                        if (japaneseFood1Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood2:
                        level = GameStateManager.instance.JapaneseFood2Level;
                        nextLevel = (GameStateManager.instance.JapaneseFood2Level + 1) / 5;

                        if (japaneseFood2Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood3:
                        level = GameStateManager.instance.JapaneseFood3Level;
                        nextLevel = (GameStateManager.instance.JapaneseFood3Level + 1) / 5;

                        if (japaneseFood3Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood4:
                        level = GameStateManager.instance.JapaneseFood4Level;
                        nextLevel = (GameStateManager.instance.JapaneseFood4Level + 1) / 5;

                        if (japaneseFood4Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood5:
                        level = GameStateManager.instance.JapaneseFood5Level;
                        nextLevel = (GameStateManager.instance.JapaneseFood5Level + 1) / 5;

                        if (japaneseFood5Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood6:
                        level = GameStateManager.instance.JapaneseFood6Level;
                        nextLevel = (GameStateManager.instance.JapaneseFood6Level + 1) / 5;

                        if (japaneseFood6Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood7:
                        level = GameStateManager.instance.JapaneseFood7Level;
                        nextLevel = (GameStateManager.instance.JapaneseFood7Level + 1) / 5;

                        if (japaneseFood7Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case JapaneseFoodType.Ramen:
                        level = GameStateManager.instance.JapaneseFood8Level;
                        nextLevel = (GameStateManager.instance.JapaneseFood8Level + 1) / 5;

                        if (japaneseFood8Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                }
                break;
            case IslandType.Island4:
                switch (GameStateManager.instance.DessertType)
                {
                    case DessertType.Dessert1:
                        level = GameStateManager.instance.Dessert1Level;
                        nextLevel = (GameStateManager.instance.Dessert1Level + 1) / 5;

                        if (dessert1Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case DessertType.Dessert2:
                        level = GameStateManager.instance.Dessert2Level;
                        nextLevel = (GameStateManager.instance.Dessert2Level + 1) / 5;

                        if (dessert2Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case DessertType.Dessert3:
                        level = GameStateManager.instance.Dessert3Level;
                        nextLevel = (GameStateManager.instance.Dessert3Level + 1) / 5;

                        if (dessert3Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case DessertType.Dessert4:
                        level = GameStateManager.instance.Dessert4Level;
                        nextLevel = (GameStateManager.instance.Dessert4Level + 1) / 5;

                        if (dessert4Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case DessertType.Dessert5:
                        level = GameStateManager.instance.Dessert5Level;
                        nextLevel = (GameStateManager.instance.Dessert5Level + 1) / 5;

                        if (dessert5Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case DessertType.Dessert6:
                        level = GameStateManager.instance.Dessert6Level;
                        nextLevel = (GameStateManager.instance.Dessert6Level + 1) / 5;

                        if (dessert6Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case DessertType.Dessert7:
                        level = GameStateManager.instance.Dessert7Level;
                        nextLevel = (GameStateManager.instance.Dessert7Level + 1) / 5;

                        if (dessert7Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case DessertType.Dessert8:
                        level = GameStateManager.instance.Dessert8Level;
                        nextLevel = (GameStateManager.instance.Dessert8Level + 1) / 5;

                        if (dessert8Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case DessertType.Dessert9:
                        level = GameStateManager.instance.Dessert9Level;
                        nextLevel = (GameStateManager.instance.Dessert9Level + 1) / 5;

                        if (dessert9Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case DessertType.FruitSkewers:
                        level = GameStateManager.instance.Dessert10Level;
                        nextLevel = (GameStateManager.instance.Dessert10Level + 1) / 5;

                        if (dessert10Array.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                }
                break;
        }
    }

    IEnumerator FirstDelay()
    {
        yield return firstSeconds;

        PortionManager.instance.GetAllPortion(3);

        yield return firstSeconds;

        PortionManager.instance.GetPortion(4,3);

        CheckPortion();

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

            Debug.LogError("1분 지남");
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

        mainUI.SetActive(false);
        inGameUI.SetActive(true);

        GameStateManager.instance.GameType = GameType.Story + number;

        rankingNoticeButton.SetActive(false);

        if (GameStateManager.instance.GameType == GameType.Story)
        {
            switch (GameStateManager.instance.IslandType)
            {
                case IslandType.Island1:
                    if (GameStateManager.instance.FoodType == FoodType.Ribs)
                    {
                        ChangeFood(FoodType.Food1);
                    }
                    break;
                case IslandType.Island2:
                    if (GameStateManager.instance.CandyType == CandyType.Chocolate)
                    {
                        ChangeCandy(CandyType.Candy1);
                    }
                    break;
                case IslandType.Island3:
                    if (GameStateManager.instance.JapaneseFoodType == JapaneseFoodType.Ramen)
                    {
                        ChangeJapaneseFood(JapaneseFoodType.JapaneseFood1);
                    }
                    break;
                case IslandType.Island4:
                    if (GameStateManager.instance.DessertType == DessertType.FruitSkewers)
                    {
                        ChangeDessert(DessertType.Dessert1);
                    }
                    break;
            }

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

            rankingNoticeButton.SetActive(true);

            switch (GameStateManager.instance.IslandType)
            {
                case IslandType.Island1:
                    ChangeFood(FoodType.Ribs);
                    break;
                case IslandType.Island2:
                    ChangeCandy(CandyType.Chocolate);
                    break;
                case IslandType.Island3:
                    ChangeJapaneseFood(JapaneseFoodType.Ramen);
                    break;
                case IslandType.Island4:
                    ChangeDessert(DessertType.FruitSkewers);
                    break;
            }

            if (!GameStateManager.instance.RankingNotice)
            {
                OpenRankingNoticeView();

                GameStateManager.instance.RankingNotice = true;
            }

            FirebaseAnalytics.LogEvent("RankingMode");
        }

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

        if(!playerDataBase.AutoUpgrade)
        {
            GameStateManager.instance.AutoUpgrade = false;
        }
        else
        {
            buff4Obj.SetActive(false);
            OffBuff(3);
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
    }

    public void GameStart_Dungeon()
    {
        if (!isDelay_Camera) return;

        if(inGameUI.activeSelf)
        {
            checkInGame = true;
        }
        else
        {
            checkInGame = false;

            isDelay_Camera = false;
            cameraController.GoToB();
        }

        island.SetActive(false);
        dungeon.SetActive(true);

        mainUI.SetActive(false);
        inGameUI.SetActive(false);
        dungeonUI.SetActive(true);
    }

    public void GameStop_Dungeon()
    {
        if (!isDelay_Camera) return;

        if(!checkInGame)
        {
            isDelay_Camera = false;
            cameraController.GoToA();

            mainUI.SetActive(true);
            inGameUI.SetActive(false);
            dungeonUI.SetActive(false);
        }
        else
        {
            mainUI.SetActive(false);
            inGameUI.SetActive(true);
            dungeonUI.SetActive(false);
        }

        island.SetActive(true);
        dungeon.SetActive(false);
    }

    public void CheckPercent()
    {
        if (!inGameUI.activeInHierarchy) return;

        successPlus = 0;
        successX2 = 0;
        needPlus = 0;
        sellPricePlus = 0;
        sellPriceTip = 0;
        defDestroy = 0;
        expUp = 0;

        changeFoodImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];

        successPlus += characterDataBase.GetCharacterEffect(playerDataBase.GetCharacterHighNumber());
        successPlus += playerDataBase.Skill7 * 0.1f;
        successPlus += levelDataBase.GetLevel(playerDataBase.Exp) * 0.05f;
        successPlus += playerDataBase.Treasure1 * 0.2f;
        successPlus += playerDataBase.Advancement * 0.1f;

        successX2 += totemsDataBase.GetTotemsEffect(playerDataBase.GetTotemsHighNumber());
        successX2 += playerDataBase.Treasure3 * 0.2f;

        sellPricePlus += truckDataBase.GetTruckEffect(playerDataBase.GetTruckHighNumber());
        sellPricePlus += playerDataBase.Skill8 * 0.4f;
        sellPricePlus += playerDataBase.Proficiency * 1;
        sellPricePlus += playerDataBase.Treasure7 * 0.8f;
        sellPricePlus += playerDataBase.Advancement * 0.4f;
        sellPricePlus += playerDataBase.GetIconHoldNumber() * 0.2f;

        if (IsWeekend())
        {
            sellPricePlus += 30;

            isWeekend = true;
        }

        sellPriceTip += 0;
        sellPriceTip += playerDataBase.Skill14 * 0.3f; //팁 확률
        sellPriceTip += playerDataBase.Treasure8 * 0.6f;

        expUp += (int)animalDataBase.GetAnimalEffect(playerDataBase.GetAnimalHighNumber());

        defDestroy += butterflyDataBase.GetButterflyEffect(playerDataBase.GetButterflyHighNumber());
        defDestroy += playerDataBase.Skill9 * 0.05f;
        defDestroy += playerDataBase.Treasure2 * 0.1f;
        defDestroy += playerDataBase.Advancement * 0.05f;

        needPlus += playerDataBase.Skill10 * 0.5f;

        upgradeFood = upgradeDataBase.GetUpgradeFood(GameStateManager.instance.FoodType);
        upgradeCandy = upgradeDataBase.GetUpgradeCandy(GameStateManager.instance.CandyType);
        upgradeJapaneseFood = upgradeDataBase.GetUpgradeJapaneseFood(GameStateManager.instance.JapaneseFoodType);
        upgradeDessert = upgradeDataBase.GetUpgradeDessert(GameStateManager.instance.DessertType);

        feverCount = GameStateManager.instance.FeverCount;

        feverTime = 30;
        feverTime += (30 * (0.003f * playerDataBase.Skill1));
        feverTime += (30 * (0.004f * playerDataBase.Treasure9));

        feverMaxCount = 400 - (300 * (0.003f * playerDataBase.Skill2));
        feverPlus = 3 + (3 * (0.01f * playerDataBase.Skill3));

        portion1Time = 30 + (30 * (0.003f * playerDataBase.Skill4)) + (30 * (0.006f * playerDataBase.Treasure6));
        portion2Time = 30 + (30 * (0.003f * playerDataBase.Skill5)) + (30 * (0.006f * playerDataBase.Treasure6));
        portion3Time = 30 + (30 * (0.003f * playerDataBase.Skill6)) + (30 * (0.006f * playerDataBase.Treasure6));
        portion4Plus = 0.003f * playerDataBase.Skill12;
        portion5Time = 30 + (30 * (0.003f * playerDataBase.Skill13)) + (30 * (0.006f * playerDataBase.Treasure6));

        if (playerDataBase.GoldX2)
        {
            sellPricePlus += 100;
        }

        if (feverMode)
        {
            successPlus += feverPlus;
            defDestroy += 5;
        }

        if (portion1)
        {
            needPlus += portion1Value;
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
            if(!GameStateManager.instance.Pause)
            {
                upgradeButtonAnim.AutoClick();
                UpgradeButton(2);
            }
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
            if (!GameStateManager.instance.Pause)
            {
                upgradeButtonAnim.AutoClick();
                UpgradeButton(2);
            }
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

        if (GameStateManager.instance.GameType == GameType.Rank)
        {
            if (GameStateManager.instance.Food8Level + 1 > playerDataBase.RankLevel1)
            {
                playerDataBase.RankLevel1 = GameStateManager.instance.Food8Level + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel1", playerDataBase.RankLevel1);
            }

            if (GameStateManager.instance.Candy10Level + 1 > playerDataBase.RankLevel2)
            {
                playerDataBase.RankLevel2 = GameStateManager.instance.Candy10Level + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel2", playerDataBase.RankLevel2);
            }

            if (GameStateManager.instance.JapaneseFood8Level + 1 > playerDataBase.RankLevel3)
            {
                playerDataBase.RankLevel3 = GameStateManager.instance.JapaneseFood8Level + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel3", playerDataBase.RankLevel3);
            }

            if (GameStateManager.instance.Dessert10Level + 1 > playerDataBase.RankLevel4)
            {
                playerDataBase.RankLevel4 = GameStateManager.instance.Dessert10Level + 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel4", playerDataBase.RankLevel4);
            }

            yield return serverSeconds;

            switch (season)
            {
                case 0:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel)
                    {
                        playerDataBase.TotalLevel = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel", playerDataBase.TotalLevel);
                    }
                    break;
                case 1:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_1)
                    {
                        playerDataBase.TotalLevel_1 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_1", playerDataBase.TotalLevel_1);
                    }
                    break;
                case 2:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_2)
                    {
                        playerDataBase.TotalLevel_2 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_2", playerDataBase.TotalLevel_2);
                    }
                    break;
                case 3:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_3)
                    {
                        playerDataBase.TotalLevel_1 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_3", playerDataBase.TotalLevel_3);
                    }
                    break;
                case 4:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_4)
                    {
                        playerDataBase.TotalLevel_1 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_4", playerDataBase.TotalLevel_4);
                    }
                    break;
                case 5:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_5)
                    {
                        playerDataBase.TotalLevel_1 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_5", playerDataBase.TotalLevel_5);
                    }
                    break;
                case 6:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_6)
                    {
                        playerDataBase.TotalLevel_1 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_6", playerDataBase.TotalLevel_6);
                    }
                    break;
                case 7:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_7)
                    {
                        playerDataBase.TotalLevel_1 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_7", playerDataBase.TotalLevel_7);
                    }
                    break;
                case 8:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_1)
                    {
                        playerDataBase.TotalLevel_8 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_8", playerDataBase.TotalLevel_8);
                    }
                    break;
                case 9:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_9)
                    {
                        playerDataBase.TotalLevel_1 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_9", playerDataBase.TotalLevel_9);
                    }
                    break;
                case 10:
                    if (playerDataBase.RankLevel1 + playerDataBase.RankLevel2 + playerDataBase.RankLevel3 + playerDataBase.RankLevel4
    > playerDataBase.TotalLevel_10)
                    {
                        playerDataBase.TotalLevel_1 = playerDataBase.RankLevel1 + playerDataBase.RankLevel2
                            + playerDataBase.RankLevel3 + playerDataBase.RankLevel4;

                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel_10", playerDataBase.TotalLevel_10);
                    }
                    break;
            }
        }

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
            homeButton.SetActive(false);
            moveArrow1.SetActive(true);

            playerDataBase.FirstReward = 1;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("FirstReward", 1);

            PlayfabManager.instance.UpdateAddGold(100000);
            //PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 10);

            SoundManager.instance.PlaySFX(GameSfxType.Sell);

            StartCoroutine(FirstDelay());
        }
    }

    void OFFSpeicalFood()
    {
        speicalFood = false;
        speicalFoodParticle.gameObject.SetActive(false);
        rareFood.SetActive(false);
    }

    public void ChangeIsland(IslandType type)
    {
        GameStateManager.instance.IslandType = type;

        islandImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];
        changeFoodImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];

        OFFSpeicalFood();

        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                if (GameStateManager.instance.FoodType == FoodType.Ribs)
                {
                    GameStateManager.instance.FoodType = FoodType.Food1;
                }

                ChangeFood(GameStateManager.instance.FoodType);
                break;
            case IslandType.Island2:
                if (GameStateManager.instance.CandyType == CandyType.Chocolate)
                {
                    GameStateManager.instance.CandyType = CandyType.Candy1;
                }

                ChangeCandy(GameStateManager.instance.CandyType);
                break;
            case IslandType.Island3:
                if (GameStateManager.instance.JapaneseFoodType == JapaneseFoodType.Ramen)
                {
                    GameStateManager.instance.JapaneseFoodType = JapaneseFoodType.JapaneseFood1;
                }

                ChangeJapaneseFood(GameStateManager.instance.JapaneseFoodType);
                break;
            case IslandType.Island4:
                if (GameStateManager.instance.DessertType == DessertType.FruitSkewers)
                {
                    GameStateManager.instance.DessertType = DessertType.Dessert1;
                }

                ChangeDessert(GameStateManager.instance.DessertType);
                break;
        }

        if (GameStateManager.instance.Effect)
        {
            if (feverMode)
            {
                for (int i = 0; i < level1UpParticle.Length; i++)
                {
                    yummyTime2Particle[i].gameObject.SetActive(false);
                }

                yummyTime2Particle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
            }
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeIslandNotion);

        CheckFoodState();
        UpgradeInitialize();
    }

    public void ChangeFood(FoodType type)
    {
        GameStateManager.instance.FoodType = type;

        OFFSpeicalFood();

        if (type == FoodType.Food4)
        {
            if (!playerDataBase.AppReview)
            {
                OpenAppReview();

                playerDataBase.AppReview = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("AppReview", 1);
            }
        }

        upgradeFood = upgradeDataBase.GetUpgradeFood(GameStateManager.instance.FoodType);

        CheckFood();
        CheckFoodState();
        UpgradeInitialize();
    }

    public void ChangeCandy(CandyType type)
    {
        GameStateManager.instance.CandyType = type;

        OFFSpeicalFood();

        upgradeCandy = upgradeDataBase.GetUpgradeCandy(GameStateManager.instance.CandyType);

        CheckFood();
        CheckFoodState();
        UpgradeInitialize();
    }

    public void ChangeJapaneseFood(JapaneseFoodType type)
    {
        GameStateManager.instance.JapaneseFoodType = type;

        OFFSpeicalFood();

        upgradeJapaneseFood = upgradeDataBase.GetUpgradeJapaneseFood(GameStateManager.instance.JapaneseFoodType);

        CheckFood();
        CheckFoodState();
        UpgradeInitialize();
    }

    public void ChangeDessert(DessertType type)
    {
        GameStateManager.instance.DessertType = type;

        OFFSpeicalFood();

        upgradeDessert = upgradeDataBase.GetUpgradeDessert(GameStateManager.instance.DessertType);

        CheckFood();
        CheckFoodState();
        UpgradeInitialize();
    }

    public void UpgradeInitialize()
    {
        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                maxLevel = upgradeFood.maxLevel;

                if (level + 1 > maxLevel)
                {
                    level = maxLevel - 1;
                }

                sellPrice = upgradeDataBase.GetPrice(level, defaultSellPrice);
                sellPrice += (int)(sellPrice * (playerDataBase.Island1Level * 0.01f));
                break;
            case IslandType.Island2:
                maxLevel = upgradeCandy.maxLevel;

                if (level + 1 > maxLevel)
                {
                    level = maxLevel - 1;
                }

                sellPrice = upgradeDataBase.GetPrice(level, defaultSellPrice);
                sellPrice += (int)(sellPrice * (0.2f + (playerDataBase.Island2Level * 0.01f)));
                break;
            case IslandType.Island3:
                maxLevel = upgradeJapaneseFood.maxLevel;

                if (level + 1 > maxLevel)
                {
                    level = maxLevel - 1;
                }

                sellPrice = upgradeDataBase.GetPrice(level, defaultSellPrice);
                sellPrice += (int)(sellPrice * (0.4f + (playerDataBase.Island3Level * 0.01f)));
                break;
            case IslandType.Island4:
                maxLevel = upgradeDessert.maxLevel;

                if (level + 1 > maxLevel)
                {
                    level = maxLevel - 1;
                }

                sellPrice = upgradeDataBase.GetPrice(level, defaultSellPrice);
                sellPrice += (int)(sellPrice * (0.6f + (playerDataBase.Island4Level * 0.01f)));
                break;
        }

        //sellPrice = sellPrice + (int)(sellPrice * 0.3f); //임시로 30% 증가

        if(speicalFood)
        {
            sellPrice += (int)(sellPrice * 1.5f);
        }

        recoverLevel = ((int)(maxLevel * 0.5f)) - 1;

        need = upgradeDataBase.GetNeed(level, defaultNeed);
        success = upgradeDataBase.GetSuccess(level);

        if (needPlus > 0)
        {
            need -= Mathf.CeilToInt((need * (0.01f * needPlus)));

            if(needPlus >= 100)
            {
                need = 0;
            }
        }

        if (sellPricePlus > 0)
        {
            sellPrice += Mathf.CeilToInt((sellPrice * (0.01f * sellPricePlus)));
        }

        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                titleText.localizationName = GameStateManager.instance.FoodType.ToString();
                break;
            case IslandType.Island2:
                titleText.localizationName = GameStateManager.instance.CandyType.ToString();
                break;
            case IslandType.Island3:
                titleText.localizationName = GameStateManager.instance.JapaneseFoodType.ToString();
                break;
            case IslandType.Island4:
                titleText.localizationName = GameStateManager.instance.DessertType.ToString();
                break;
        }

        titleText.plusText = " ( " + (level + 1) + " / " + maxLevel + " )";

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:
                highLevelText.localizationName = " ";
                highLevelText.plusText = "";
                break;
            case GameType.Rank:
                switch (GameStateManager.instance.IslandType)
                {
                    case IslandType.Island1:
                        if (GameStateManager.instance.Food8Level > playerDataBase.RankLevel1)
                        {
                            highLevelText.localizationName = "Best";
                            highLevelText.plusText = " : " + (GameStateManager.instance.Food8Level + 1);
                        }
                        else
                        {
                            highLevelText.localizationName = "Best";
                            highLevelText.plusText = " : " + playerDataBase.RankLevel1;
                        }

                        break;
                    case IslandType.Island2:
                        if (GameStateManager.instance.Candy10Level > playerDataBase.RankLevel2)
                        {
                            highLevelText.localizationName = "Best";
                            highLevelText.plusText = " : " + (GameStateManager.instance.Candy10Level + 1);
                        }
                        else
                        {
                            highLevelText.localizationName = "Best";
                            highLevelText.plusText = " : " + playerDataBase.RankLevel2;
                        }
                        break;
                    case IslandType.Island3:
                        if (GameStateManager.instance.JapaneseFood8Level > playerDataBase.RankLevel3)
                        {
                            highLevelText.localizationName = "Best";
                            highLevelText.plusText = " : " + (GameStateManager.instance.JapaneseFood8Level + 1);
                        }
                        else
                        {
                            highLevelText.localizationName = "Best";
                            highLevelText.plusText = " : " + playerDataBase.RankLevel3;
                        }
                        break;
                    case IslandType.Island4:
                        if (GameStateManager.instance.Dessert10Level > playerDataBase.RankLevel4)
                        {
                            highLevelText.localizationName = "Best";
                            highLevelText.plusText = " : " + (GameStateManager.instance.Dessert10Level + 1);
                        }
                        else
                        {
                            highLevelText.localizationName = "Best";
                            highLevelText.plusText = " : " + playerDataBase.RankLevel4;
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
        else if(level >= 24)
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

        if(level == 0)
        {
            notSell.SetActive(true);
        }

        success += successPlus;

        if (success >= 100)
        {
            success = 100;
        }

        if (GameStateManager.instance.Developer) success = 100;

        successText.localizationName = "SuccessPercent";
        successText.plusText = " : " + success.ToString("N1") + "%";

        if (successPlus > 0)
        {
            successText.plusText += " (+" + (successPlus).ToString("N1") + "%)";
        }

        needText.localizationName = "NeedPrice";
        needText.plusText = "";

        if (needPlus > 0)
        {
            needText.plusText += " (-" + (needPlus.ToString("N1")) + "%)\n" + MoneyUnitString.ToCurrencyString(need);
        }
        else
        {
            needText.plusText += "\n" + MoneyUnitString.ToCurrencyString(need);
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

        defDestroyText.localizationName = "DefDestroyPercent";
        defDestroyText.plusText = " : " + defDestroy.ToString("N2") + "%";

        successX2Text.localizationName = "SuccessX2Percent";
        successX2Text.plusText = " : " + successX2.ToString("N1") + "%";

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

        guideMissionManager.Initialize();
    }

    void CheckFoodState()
    {
        for(int i = 0; i < foodArrayList.Count; i ++)
        {
            foodArrayList[i].gameObject.SetActive(false);
        }
        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Food1:
                        if (hamburgerArray.Length - 1 < nextLevel)
                        {
                            hamburgerArray[hamburgerArray.Length - 1].gameObject.SetActive(true);
                            hamburgerArray[hamburgerArray.Length - 1].Initialize(5);
                            hamburgerArray[hamburgerArray.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            hamburgerArray[nextLevel].gameObject.SetActive(true);
                            hamburgerArray[nextLevel].Initialize(GameStateManager.instance.Food1Level - (5 * nextLevel));
                            hamburgerArray[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case FoodType.Food2:
                        if (sandwichArray.Length - 1 < nextLevel)
                        {
                            sandwichArray[sandwichArray.Length - 1].gameObject.SetActive(true);
                            sandwichArray[sandwichArray.Length - 1].Initialize(5);
                            sandwichArray[sandwichArray.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            sandwichArray[nextLevel].gameObject.SetActive(true);
                            sandwichArray[nextLevel].Initialize(GameStateManager.instance.Food2Level - (5 * nextLevel));
                            sandwichArray[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case FoodType.Food3:
                        if (snackLabArray.Length - 1 < nextLevel)
                        {
                            snackLabArray[snackLabArray.Length - 1].gameObject.SetActive(true);
                            snackLabArray[snackLabArray.Length - 1].Initialize(5);
                            snackLabArray[snackLabArray.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            snackLabArray[nextLevel].gameObject.SetActive(true);
                            snackLabArray[nextLevel].Initialize(GameStateManager.instance.Food3Level - (5 * nextLevel));
                            snackLabArray[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case FoodType.Food4:
                        if (drinkArray.Length - 1 < nextLevel)
                        {
                            drinkArray[drinkArray.Length - 1].gameObject.SetActive(true);
                            drinkArray[drinkArray.Length - 1].Initialize(5);
                            drinkArray[drinkArray.Length - 1].SpeicalFood(speicalFood);

                        }
                        else
                        {
                            drinkArray[nextLevel].gameObject.SetActive(true);
                            drinkArray[nextLevel].Initialize(GameStateManager.instance.Food4Level - (5 * nextLevel));
                            drinkArray[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case FoodType.Food5:
                        if (pizzaArray.Length - 1 < nextLevel)
                        {
                            pizzaArray[pizzaArray.Length - 1].gameObject.SetActive(true);
                            pizzaArray[pizzaArray.Length - 1].Initialize(5);
                            pizzaArray[pizzaArray.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            pizzaArray[nextLevel].gameObject.SetActive(true);
                            pizzaArray[nextLevel].Initialize(GameStateManager.instance.Food5Level - (5 * nextLevel));
                            pizzaArray[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case FoodType.Food6:
                        if (donutArray.Length - 1 < nextLevel)
                        {
                            donutArray[donutArray.Length - 1].gameObject.SetActive(true);
                            donutArray[donutArray.Length - 1].Initialize(5);
                            donutArray[donutArray.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            donutArray[nextLevel].gameObject.SetActive(true);
                            donutArray[nextLevel].Initialize(GameStateManager.instance.Food6Level - (5 * nextLevel));
                            donutArray[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case FoodType.Food7:
                        if (friesArray.Length - 1 < nextLevel)
                        {
                            friesArray[friesArray.Length - 1].gameObject.SetActive(true);
                            friesArray[friesArray.Length - 1].Initialize(5);
                            friesArray[friesArray.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            friesArray[nextLevel].gameObject.SetActive(true);
                            friesArray[nextLevel].Initialize(GameStateManager.instance.Food7Level - (5 * nextLevel));
                            friesArray[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case FoodType.Ribs:
                        if (ribsArray.Length - 1 < nextLevel)
                        {
                            ribsArray[ribsArray.Length - 1].gameObject.SetActive(true);
                            ribsArray[ribsArray.Length - 1].Initialize(5);
                            ribsArray[ribsArray.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            ribsArray[nextLevel].gameObject.SetActive(true);
                            ribsArray[nextLevel].Initialize(GameStateManager.instance.Food8Level - (5 * nextLevel));
                            ribsArray[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                }
                break;
            case IslandType.Island2:
                switch (GameStateManager.instance.CandyType)
                {
                    case CandyType.Candy1:
                        if (candy1Array.Length - 1 < nextLevel)
                        {
                            candy1Array[candy1Array.Length - 1].gameObject.SetActive(true);
                            candy1Array[candy1Array.Length - 1].Initialize(5);
                            candy1Array[candy1Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy1Array[nextLevel].gameObject.SetActive(true);
                            candy1Array[nextLevel].Initialize(GameStateManager.instance.Candy1Level - (5 * nextLevel));
                            candy1Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case CandyType.Candy2:
                        if (candy2Array.Length - 1 < nextLevel)
                        {
                            candy2Array[candy2Array.Length - 1].gameObject.SetActive(true);
                            candy2Array[candy2Array.Length - 1].Initialize(5);
                            candy2Array[candy2Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy2Array[nextLevel].gameObject.SetActive(true);
                            candy2Array[nextLevel].Initialize(GameStateManager.instance.Candy2Level - (5 * nextLevel));
                            candy2Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case CandyType.Candy3:
                        if (candy3Array.Length - 1 < nextLevel)
                        {
                            candy3Array[candy3Array.Length - 1].gameObject.SetActive(true);
                            candy3Array[candy3Array.Length - 1].Initialize(5);
                            candy3Array[candy3Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy3Array[nextLevel].gameObject.SetActive(true);
                            candy3Array[nextLevel].Initialize(GameStateManager.instance.Candy3Level - (5 * nextLevel));
                            candy3Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case CandyType.Candy4:
                        if (candy4Array.Length - 1 < nextLevel)
                        {
                            candy4Array[candy4Array.Length - 1].gameObject.SetActive(true);
                            candy4Array[candy4Array.Length - 1].Initialize(5);
                            candy4Array[candy4Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy4Array[nextLevel].gameObject.SetActive(true);
                            candy4Array[nextLevel].Initialize(GameStateManager.instance.Candy4Level - (5 * nextLevel));
                            candy4Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case CandyType.Candy5:
                        if (candy5Array.Length - 1 < nextLevel)
                        {
                            candy5Array[candy5Array.Length - 1].gameObject.SetActive(true);
                            candy5Array[candy5Array.Length - 1].Initialize(5);
                            candy5Array[candy5Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy5Array[nextLevel].gameObject.SetActive(true);
                            candy5Array[nextLevel].Initialize(GameStateManager.instance.Candy5Level - (5 * nextLevel));
                            candy5Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case CandyType.Candy6:
                        if (candy6Array.Length - 1 < nextLevel)
                        {
                            candy6Array[candy6Array.Length - 1].gameObject.SetActive(true);
                            candy6Array[candy6Array.Length - 1].Initialize(5);
                            candy6Array[candy6Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy6Array[nextLevel].gameObject.SetActive(true);
                            candy6Array[nextLevel].Initialize(GameStateManager.instance.Candy6Level - (5 * nextLevel));
                            candy6Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case CandyType.Candy7:
                        if (candy7Array.Length - 1 < nextLevel)
                        {
                            candy7Array[candy7Array.Length - 1].gameObject.SetActive(true);
                            candy7Array[candy7Array.Length - 1].Initialize(5);
                            candy7Array[candy7Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy7Array[nextLevel].gameObject.SetActive(true);
                            candy7Array[nextLevel].Initialize(GameStateManager.instance.Candy7Level - (5 * nextLevel));
                            candy7Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case CandyType.Candy8:
                        if (candy8Array.Length - 1 < nextLevel)
                        {
                            candy8Array[candy8Array.Length - 1].gameObject.SetActive(true);
                            candy8Array[candy8Array.Length - 1].Initialize(5);
                            candy8Array[candy8Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy8Array[nextLevel].gameObject.SetActive(true);
                            candy8Array[nextLevel].Initialize(GameStateManager.instance.Candy8Level - (5 * nextLevel));
                            candy8Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case CandyType.Candy9:
                        if (candy9Array.Length - 1 < nextLevel)
                        {
                            candy9Array[candy9Array.Length - 1].gameObject.SetActive(true);
                            candy9Array[candy9Array.Length - 1].Initialize(5);
                            candy9Array[candy9Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy9Array[nextLevel].gameObject.SetActive(true);
                            candy9Array[nextLevel].Initialize(GameStateManager.instance.Candy9Level - (5 * nextLevel));
                            candy9Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case CandyType.Chocolate:
                        if (candy10Array.Length - 1 < nextLevel)
                        {
                            candy10Array[candy10Array.Length - 1].gameObject.SetActive(true);
                            candy10Array[candy10Array.Length - 1].Initialize(5);
                            candy10Array[candy10Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            candy10Array[nextLevel].gameObject.SetActive(true);
                            candy10Array[nextLevel].Initialize(GameStateManager.instance.Candy10Level - (5 * nextLevel));
                            candy10Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                }
                break;
            case IslandType.Island3:
                switch (GameStateManager.instance.JapaneseFoodType)
                {
                    case JapaneseFoodType.JapaneseFood1:
                        if (japaneseFood1Array.Length - 1 < nextLevel)
                        {
                            japaneseFood1Array[japaneseFood1Array.Length - 1].gameObject.SetActive(true);
                            japaneseFood1Array[japaneseFood1Array.Length - 1].Initialize(5);
                            japaneseFood1Array[japaneseFood1Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            japaneseFood1Array[nextLevel].gameObject.SetActive(true);
                            japaneseFood1Array[nextLevel].Initialize(GameStateManager.instance.JapaneseFood1Level - (5 * nextLevel));
                            japaneseFood1Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood2:
                        if (japaneseFood2Array.Length - 1 < nextLevel)
                        {
                            japaneseFood2Array[japaneseFood2Array.Length - 1].gameObject.SetActive(true);
                            japaneseFood2Array[japaneseFood2Array.Length - 1].Initialize(5);
                            japaneseFood2Array[japaneseFood2Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            japaneseFood2Array[nextLevel].gameObject.SetActive(true);
                            japaneseFood2Array[nextLevel].Initialize(GameStateManager.instance.JapaneseFood2Level - (5 * nextLevel));
                            japaneseFood2Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood3:
                        if (japaneseFood3Array.Length - 1 < nextLevel)
                        {
                            japaneseFood3Array[japaneseFood3Array.Length - 1].gameObject.SetActive(true);
                            japaneseFood3Array[japaneseFood3Array.Length - 1].Initialize(5);
                            japaneseFood3Array[japaneseFood3Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            japaneseFood3Array[nextLevel].gameObject.SetActive(true);
                            japaneseFood3Array[nextLevel].Initialize(GameStateManager.instance.JapaneseFood3Level - (5 * nextLevel));
                            japaneseFood3Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood4:
                        if (japaneseFood4Array.Length - 1 < nextLevel)
                        {
                            japaneseFood4Array[japaneseFood4Array.Length - 1].gameObject.SetActive(true);
                            japaneseFood4Array[japaneseFood4Array.Length - 1].Initialize(5);
                            japaneseFood4Array[japaneseFood4Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            japaneseFood4Array[nextLevel].gameObject.SetActive(true);
                            japaneseFood4Array[nextLevel].Initialize(GameStateManager.instance.JapaneseFood4Level - (5 * nextLevel));
                            japaneseFood4Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood5:
                        if (japaneseFood5Array.Length - 1 < nextLevel)
                        {
                            japaneseFood5Array[japaneseFood5Array.Length - 1].gameObject.SetActive(true);
                            japaneseFood5Array[japaneseFood5Array.Length - 1].Initialize(5);
                            japaneseFood5Array[japaneseFood5Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            japaneseFood5Array[nextLevel].gameObject.SetActive(true);
                            japaneseFood5Array[nextLevel].Initialize(GameStateManager.instance.JapaneseFood5Level - (5 * nextLevel));
                            japaneseFood5Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood6:
                        if (japaneseFood6Array.Length - 1 < nextLevel)
                        {
                            japaneseFood6Array[japaneseFood6Array.Length - 1].gameObject.SetActive(true);
                            japaneseFood6Array[japaneseFood6Array.Length - 1].Initialize(5);
                            japaneseFood6Array[japaneseFood6Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            japaneseFood6Array[nextLevel].gameObject.SetActive(true);
                            japaneseFood6Array[nextLevel].Initialize(GameStateManager.instance.JapaneseFood6Level - (5 * nextLevel));
                            japaneseFood6Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood7:
                        if (japaneseFood7Array.Length - 1 < nextLevel)
                        {
                            japaneseFood7Array[japaneseFood7Array.Length - 1].gameObject.SetActive(true);
                            japaneseFood7Array[japaneseFood7Array.Length - 1].Initialize(5);
                            japaneseFood7Array[japaneseFood7Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            japaneseFood7Array[nextLevel].gameObject.SetActive(true);
                            japaneseFood7Array[nextLevel].Initialize(GameStateManager.instance.JapaneseFood7Level - (5 * nextLevel));
                            japaneseFood7Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case JapaneseFoodType.Ramen:
                        if (japaneseFood8Array.Length - 1 < nextLevel)
                        {
                            japaneseFood8Array[japaneseFood8Array.Length - 1].gameObject.SetActive(true);
                            japaneseFood8Array[japaneseFood8Array.Length - 1].Initialize(5);
                            japaneseFood8Array[japaneseFood8Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            japaneseFood8Array[nextLevel].gameObject.SetActive(true);
                            japaneseFood8Array[nextLevel].Initialize(GameStateManager.instance.JapaneseFood8Level - (5 * nextLevel));
                            japaneseFood8Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                }
                break;
            case IslandType.Island4:
                switch (GameStateManager.instance.DessertType)
                {
                    case DessertType.Dessert1:
                        if (dessert1Array.Length - 1 < nextLevel)
                        {
                            dessert1Array[dessert1Array.Length - 1].gameObject.SetActive(true);
                            dessert1Array[dessert1Array.Length - 1].Initialize(5);
                            dessert1Array[dessert1Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert1Array[nextLevel].gameObject.SetActive(true);
                            dessert1Array[nextLevel].Initialize(GameStateManager.instance.Dessert1Level - (5 * nextLevel));
                            dessert1Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case DessertType.Dessert2:
                        if (dessert2Array.Length - 1 < nextLevel)
                        {
                            dessert2Array[dessert2Array.Length - 1].gameObject.SetActive(true);
                            dessert2Array[dessert2Array.Length - 1].Initialize(5);
                            dessert2Array[dessert2Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert2Array[nextLevel].gameObject.SetActive(true);
                            dessert2Array[nextLevel].Initialize(GameStateManager.instance.Dessert2Level - (5 * nextLevel));
                            dessert2Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case DessertType.Dessert3:
                        if (dessert3Array.Length - 1 < nextLevel)
                        {
                            dessert3Array[dessert3Array.Length - 1].gameObject.SetActive(true);
                            dessert3Array[dessert3Array.Length - 1].Initialize(5);
                            dessert3Array[dessert3Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert3Array[nextLevel].gameObject.SetActive(true);
                            dessert3Array[nextLevel].Initialize(GameStateManager.instance.Dessert3Level - (5 * nextLevel));
                            dessert3Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case DessertType.Dessert4:
                        if (dessert4Array.Length - 1 < nextLevel)
                        {
                            dessert4Array[dessert4Array.Length - 1].gameObject.SetActive(true);
                            dessert4Array[dessert4Array.Length - 1].Initialize(5);
                            dessert4Array[dessert4Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert4Array[nextLevel].gameObject.SetActive(true);
                            dessert4Array[nextLevel].Initialize(GameStateManager.instance.Dessert4Level - (5 * nextLevel));
                            dessert4Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case DessertType.Dessert5:
                        if (dessert5Array.Length - 1 < nextLevel)
                        {
                            dessert5Array[dessert5Array.Length - 1].gameObject.SetActive(true);
                            dessert5Array[dessert5Array.Length - 1].Initialize(5);
                            dessert5Array[dessert5Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert5Array[nextLevel].gameObject.SetActive(true);
                            dessert5Array[nextLevel].Initialize(GameStateManager.instance.Dessert5Level - (5 * nextLevel));
                            dessert5Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case DessertType.Dessert6:
                        if (dessert6Array.Length - 1 < nextLevel)
                        {
                            dessert6Array[dessert6Array.Length - 1].gameObject.SetActive(true);
                            dessert6Array[dessert6Array.Length - 1].Initialize(5);
                            dessert6Array[dessert6Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert6Array[nextLevel].gameObject.SetActive(true);
                            dessert6Array[nextLevel].Initialize(GameStateManager.instance.Dessert6Level - (5 * nextLevel));
                            dessert6Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case DessertType.Dessert7:
                        if (dessert7Array.Length - 1 < nextLevel)
                        {
                            dessert7Array[dessert7Array.Length - 1].gameObject.SetActive(true);
                            dessert7Array[dessert7Array.Length - 1].Initialize(5);
                            dessert7Array[dessert7Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert7Array[nextLevel].gameObject.SetActive(true);
                            dessert7Array[nextLevel].Initialize(GameStateManager.instance.Dessert7Level - (5 * nextLevel));
                            dessert7Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case DessertType.Dessert8:
                        if (dessert8Array.Length - 1 < nextLevel)
                        {
                            dessert8Array[dessert8Array.Length - 1].gameObject.SetActive(true);
                            dessert8Array[dessert8Array.Length - 1].Initialize(5);
                            dessert8Array[dessert8Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert8Array[nextLevel].gameObject.SetActive(true);
                            dessert8Array[nextLevel].Initialize(GameStateManager.instance.Dessert8Level - (5 * nextLevel));
                            dessert8Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case DessertType.Dessert9:
                        if (dessert9Array.Length - 1 < nextLevel)
                        {
                            dessert9Array[dessert9Array.Length - 1].gameObject.SetActive(true);
                            dessert9Array[dessert9Array.Length - 1].Initialize(5);
                            dessert9Array[dessert9Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert9Array[nextLevel].gameObject.SetActive(true);
                            dessert9Array[nextLevel].Initialize(GameStateManager.instance.Dessert9Level - (5 * nextLevel));
                            dessert9Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                    case DessertType.FruitSkewers:
                        if (dessert10Array.Length - 1 < nextLevel)
                        {
                            dessert10Array[dessert10Array.Length - 1].gameObject.SetActive(true);
                            dessert10Array[dessert10Array.Length - 1].Initialize(5);
                            dessert10Array[dessert10Array.Length - 1].SpeicalFood(speicalFood);
                        }
                        else
                        {
                            dessert10Array[nextLevel].gameObject.SetActive(true);
                            dessert10Array[nextLevel].Initialize(GameStateManager.instance.Dessert10Level - (5 * nextLevel));
                            dessert10Array[nextLevel].SpeicalFood(speicalFood);
                        }
                        break;
                }
                break;
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

    void CheckFoodLevelUp()
    {
        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Food1:
                        if (hamburgerArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            hamburgerArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Food2:
                        if (sandwichArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            sandwichArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Food3:
                        if (snackLabArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            snackLabArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Food4:
                        if (drinkArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            drinkArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Food5:
                        if (pizzaArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            pizzaArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Food6:
                        if (donutArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            donutArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Food7:
                        if (friesArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            friesArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Ribs:
                        if (ribsArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            ribsArray[nextLevel].LevelUp();
                        }
                        break;
                }
                break;
            case IslandType.Island2:
                switch (GameStateManager.instance.CandyType)
                {
                    case CandyType.Candy1:
                        if (candy1Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy1Array[nextLevel].LevelUp();
                        }
                        break;
                    case CandyType.Candy2:
                        if (candy2Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy2Array[nextLevel].LevelUp();
                        }
                        break;
                    case CandyType.Candy3:
                        if (candy3Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy3Array[nextLevel].LevelUp();
                        }
                        break;
                    case CandyType.Candy4:
                        if (candy4Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy4Array[nextLevel].LevelUp();
                        }
                        break;
                    case CandyType.Candy5:
                        if (candy5Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy5Array[nextLevel].LevelUp();
                        }
                        break;
                    case CandyType.Candy6:
                        if (candy6Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy6Array[nextLevel].LevelUp();
                        }
                        break;
                    case CandyType.Candy7:
                        if (candy7Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy7Array[nextLevel].LevelUp();
                        }
                        break;
                    case CandyType.Candy8:
                        if (candy8Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy8Array[nextLevel].LevelUp();
                        }
                        break;
                    case CandyType.Candy9:
                        if (candy9Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy9Array[nextLevel].LevelUp();
                        }
                        break;
                    case CandyType.Chocolate:
                        if (candy10Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            candy10Array[nextLevel].LevelUp();
                        }
                        break;
                }
                break;
            case IslandType.Island3:
                switch (GameStateManager.instance.JapaneseFoodType)
                {
                    case JapaneseFoodType.JapaneseFood1:
                        if (japaneseFood1Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            japaneseFood1Array[nextLevel].LevelUp();
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood2:
                        if (japaneseFood2Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            japaneseFood2Array[nextLevel].LevelUp();
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood3:
                        if (japaneseFood3Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            japaneseFood3Array[nextLevel].LevelUp();
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood4:
                        if (japaneseFood4Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            japaneseFood4Array[nextLevel].LevelUp();
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood5:
                        if (japaneseFood5Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            japaneseFood5Array[nextLevel].LevelUp();
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood6:
                        if (japaneseFood6Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            japaneseFood6Array[nextLevel].LevelUp();
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood7:
                        if (japaneseFood7Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            japaneseFood7Array[nextLevel].LevelUp();
                        }
                        break;
                    case JapaneseFoodType.Ramen:
                        if (japaneseFood8Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            japaneseFood8Array[nextLevel].LevelUp();
                        }
                        break;
                }
                break;
            case IslandType.Island4:
                switch (GameStateManager.instance.DessertType)
                {
                    case DessertType.Dessert1:
                        if (dessert1Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert1Array[nextLevel].LevelUp();
                        }
                        break;
                    case DessertType.Dessert2:
                        if (dessert2Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert2Array[nextLevel].LevelUp();
                        }
                        break;
                    case DessertType.Dessert3:
                        if (dessert3Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert3Array[nextLevel].LevelUp();
                        }
                        break;
                    case DessertType.Dessert4:
                        if (dessert4Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert4Array[nextLevel].LevelUp();
                        }
                        break;
                    case DessertType.Dessert5:
                        if (dessert5Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert5Array[nextLevel].LevelUp();
                        }
                        break;
                    case DessertType.Dessert6:
                        if (dessert6Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert6Array[nextLevel].LevelUp();
                        }
                        break;
                    case DessertType.Dessert7:
                        if (dessert7Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert7Array[nextLevel].LevelUp();
                        }
                        break;
                    case DessertType.Dessert8:
                        if (dessert8Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert8Array[nextLevel].LevelUp();
                        }
                        break;
                    case DessertType.Dessert9:
                        if (dessert9Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert9Array[nextLevel].LevelUp();
                        }
                        break;
                    case DessertType.FruitSkewers:
                        if (dessert10Array.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            dessert10Array[nextLevel].LevelUp();
                        }
                        break;
                }
                break;
        }
    }

    public void UpgradeButton(int number)
    {
        if(number < 2)
        {
            if(GameStateManager.instance.AutoUpgrade && number == 0)
            {
                return;
            }

            if(buff4)
            {
                return;
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

        if (playerDataBase.Coin < need)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowCoin);
            return;
        }

        if(supportCount >= supportMaxCount)
        {
            supportCount = 0;

            shopManager.OpenPackage(PackageType.Package7);
        }
        else
        {
            supportCount += 1;
        }

        PlayfabManager.instance.UpdateSellPriceGold(-need);

        myMoneyPlusText.gameObject.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(true);
        myMoneyPlusText.color = Color.red;
        myMoneyPlusText.text = "-" + MoneyUnitString.ToCurrencyString(need);

        switch(number)
        {
            case 0:
                FirebaseAnalytics.LogEvent("Upgrade_Screen");
                break;
            case 1:
                FirebaseAnalytics.LogEvent("Upgrade_Button");
                break;
        }    

        if (Random.Range(0, 100f) >= 100 - success)
        {
            if (isDef)
            {
                UseDefTicket();
            }

            if(successX2 > 0)
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
                    if (!changeFoodManager.changeFoodView.activeInHierarchy)
                    {
                        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);
                    }
                }
            }
            else
            {
                level += 1;
                if (!changeFoodManager.changeFoodView.activeInHierarchy)
                {
                    NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);
                }
            }

            GameStateManager.instance.UpgradeCount += 1;
            playerDataBase.UpgradeCount += 1;

            if (!isExp)
            {
                playerDataBase.Exp += 10 + expUp;
            }

            switch (GameStateManager.instance.IslandType)
            {
                case IslandType.Island1:
                    switch (GameStateManager.instance.FoodType)
                    {
                        case FoodType.Food1:
                            GameStateManager.instance.Food1Level = level;
                            break;
                        case FoodType.Food2:
                            GameStateManager.instance.Food2Level = level;
                            break;
                        case FoodType.Food3:
                            GameStateManager.instance.Food3Level = level;
                            break;
                        case FoodType.Food4:
                            GameStateManager.instance.Food4Level = level;
                            break;
                        case FoodType.Food5:
                            GameStateManager.instance.Food5Level = level;
                            break;
                        case FoodType.Food6:
                            GameStateManager.instance.Food6Level = level;
                            break;
                        case FoodType.Food7:
                            GameStateManager.instance.Food7Level = level;
                            break;
                        case FoodType.Ribs:
                            GameStateManager.instance.Food8Level = level;
                            break;
                    }
                    break;
                case IslandType.Island2:
                    switch (GameStateManager.instance.CandyType)
                    {
                        case CandyType.Candy1:
                            GameStateManager.instance.Candy1Level = level;
                            break;
                        case CandyType.Candy2:
                            GameStateManager.instance.Candy2Level = level;
                            break;
                        case CandyType.Candy3:
                            GameStateManager.instance.Candy3Level = level;
                            break;
                        case CandyType.Candy4:
                            GameStateManager.instance.Candy4Level = level;
                            break;
                        case CandyType.Candy5:
                            GameStateManager.instance.Candy5Level = level;
                            break;
                        case CandyType.Candy6:
                            GameStateManager.instance.Candy6Level = level;
                            break;
                        case CandyType.Candy7:
                            GameStateManager.instance.Candy7Level = level;
                            break;
                        case CandyType.Candy8:
                            GameStateManager.instance.Candy8Level = level;
                            break;
                        case CandyType.Candy9:
                            GameStateManager.instance.Candy9Level = level;
                            break;
                        case CandyType.Chocolate:
                            GameStateManager.instance.Candy10Level = level;
                            break;
                    }
                    break;
                case IslandType.Island3:
                    switch (GameStateManager.instance.JapaneseFoodType)
                    {
                        case JapaneseFoodType.JapaneseFood1:
                            GameStateManager.instance.JapaneseFood1Level = level;
                            break;
                        case JapaneseFoodType.JapaneseFood2:
                            GameStateManager.instance.JapaneseFood2Level = level;
                            break;
                        case JapaneseFoodType.JapaneseFood3:
                            GameStateManager.instance.JapaneseFood3Level = level;
                            break;
                        case JapaneseFoodType.JapaneseFood4:
                            GameStateManager.instance.JapaneseFood4Level = level;
                            break;
                        case JapaneseFoodType.JapaneseFood5:
                            GameStateManager.instance.JapaneseFood5Level = level;
                            break;
                        case JapaneseFoodType.JapaneseFood6:
                            GameStateManager.instance.JapaneseFood6Level = level;
                            break;
                        case JapaneseFoodType.JapaneseFood7:
                            GameStateManager.instance.JapaneseFood7Level = level;
                            break;
                        case JapaneseFoodType.Ramen:
                            GameStateManager.instance.JapaneseFood8Level = level;
                            break;
                    }
                    break;
                case IslandType.Island4:
                    switch (GameStateManager.instance.DessertType)
                    {
                        case DessertType.Dessert1:
                            GameStateManager.instance.Dessert1Level = level;
                            break;
                        case DessertType.Dessert2:
                            GameStateManager.instance.Dessert2Level = level;
                            break;
                        case DessertType.Dessert3:
                            GameStateManager.instance.Dessert3Level = level;
                            break;
                        case DessertType.Dessert4:
                            GameStateManager.instance.Dessert4Level = level;
                            break;
                        case DessertType.Dessert5:
                            GameStateManager.instance.Dessert5Level = level;
                            break;
                        case DessertType.Dessert6:
                            GameStateManager.instance.Dessert6Level = level;
                            break;
                        case DessertType.Dessert7:
                            GameStateManager.instance.Dessert7Level = level;
                            break;
                        case DessertType.Dessert8:
                            GameStateManager.instance.Dessert8Level = level;
                            break;
                        case DessertType.Dessert9:
                            GameStateManager.instance.Dessert9Level = level;
                            break;
                        case DessertType.FruitSkewers:
                            GameStateManager.instance.Dessert10Level = level;
                            break;
                    }
                    break;
            }

            levelManager.Initialize();

            CheckFoodLevelUp();
            UpgradeInitialize();

            if (level + 1 >= maxLevel)
            {
                MaxLevelUpgradeSuccess();

                SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);
                if (!changeFoodManager.changeFoodView.activeInHierarchy)
                {
                    NotionManager.instance.UseNotion(NotionType.MaxLevel);
                }

                if (GameStateManager.instance.Effect)
                {
                    level1UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(false);
                    level1UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
                    level1UpParticle[(int)GameStateManager.instance.IslandType].Play();

                    level5UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(false);
                    level5UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
                    level5UpParticle[(int)GameStateManager.instance.IslandType].Play();

                    levelMaxParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(false);
                    levelMaxParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
                    levelMaxParticle[(int)GameStateManager.instance.IslandType].Play();
                }
            }
            else
            {
                if ((level + 1) % 5 == 0)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Upgrade5);

                    nextLevel++;
                    CheckFoodState();

                    if (GameStateManager.instance.Effect)
                    {
                        level1UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(false);
                        level1UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
                        level1UpParticle[(int)GameStateManager.instance.IslandType].Play();

                        level5UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(false);
                        level5UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
                        level5UpParticle[(int)GameStateManager.instance.IslandType].Play();
                    }
                }
                else
                {
                    if (GameStateManager.instance.Effect)
                    {
                        level1UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(false);
                        level1UpParticle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
                        level1UpParticle[(int)GameStateManager.instance.IslandType].Play();
                    }

                    SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                }
            }
        }
        else
        {
            if (GameStateManager.instance.Vibration)
            {
                Handheld.Vibrate();
            }

            if (isDef)
            {
                UseDefTicket();

                NotionManager.instance.UseNotion(NotionType.DefDestroyNotion);
                return;
            }
            else
            {
                if (maxLevel >= 20)
                {
                    if (Random.Range(0, 100f) < repairTicket)
                    {
                        PortionManager.instance.GetRecoverTickets(1);

                        Debug.LogError("복구 열쇠 획득");
                    }
                }

                if (defDestroy > 0)
                {
                    if (defDestroy >= Random.Range(0, 100))
                    {
                        level -= 1;

                        switch (GameStateManager.instance.IslandType)
                        {
                            case IslandType.Island1:
                                switch (GameStateManager.instance.FoodType)
                                {
                                    case FoodType.Food1:
                                        GameStateManager.instance.Food1Level -= 1;
                                        break;
                                    case FoodType.Food2:
                                        GameStateManager.instance.Food2Level -= 1;
                                        break;
                                    case FoodType.Food3:
                                        GameStateManager.instance.Food3Level -= 1;
                                        break;
                                    case FoodType.Food4:
                                        GameStateManager.instance.Food4Level -= 1;
                                        break;
                                    case FoodType.Food5:
                                        GameStateManager.instance.Food5Level -= 1;
                                        break;
                                    case FoodType.Food6:
                                        GameStateManager.instance.Food6Level -= 1;
                                        break;
                                    case FoodType.Food7:
                                        GameStateManager.instance.Food7Level -= 1;
                                        break;
                                    case FoodType.Ribs:
                                        GameStateManager.instance.Food8Level -= 1;
                                        break;
                                }
                                break;
                            case IslandType.Island2:
                                switch (GameStateManager.instance.CandyType)
                                {
                                    case CandyType.Candy1:
                                        GameStateManager.instance.Candy1Level -= 1;
                                        break;
                                    case CandyType.Candy2:
                                        GameStateManager.instance.Candy2Level -= 1;
                                        break;
                                    case CandyType.Candy3:
                                        GameStateManager.instance.Candy3Level -= 1;
                                        break;
                                    case CandyType.Candy4:
                                        GameStateManager.instance.Candy4Level -= 1;
                                        break;
                                    case CandyType.Candy5:
                                        GameStateManager.instance.Candy5Level -= 1;
                                        break;
                                    case CandyType.Candy6:
                                        GameStateManager.instance.Candy6Level -= 1;
                                        break;
                                    case CandyType.Candy7:
                                        GameStateManager.instance.Candy7Level -= 1;
                                        break;
                                    case CandyType.Candy8:
                                        GameStateManager.instance.Candy8Level -= 1;
                                        break;
                                    case CandyType.Candy9:
                                        GameStateManager.instance.Candy9Level -= 1;
                                        break;
                                    case CandyType.Chocolate:
                                        GameStateManager.instance.Candy10Level -= 1;
                                        break;
                                }
                                break;
                            case IslandType.Island3:
                                switch (GameStateManager.instance.JapaneseFoodType)
                                {
                                    case JapaneseFoodType.JapaneseFood1:
                                        GameStateManager.instance.JapaneseFood1Level -= 1;
                                        break;
                                    case JapaneseFoodType.JapaneseFood2:
                                        GameStateManager.instance.JapaneseFood2Level -= 1;
                                        break;
                                    case JapaneseFoodType.JapaneseFood3:
                                        GameStateManager.instance.JapaneseFood3Level -= 1;
                                        break;
                                    case JapaneseFoodType.JapaneseFood4:
                                        GameStateManager.instance.JapaneseFood4Level -= 1;
                                        break;
                                    case JapaneseFoodType.JapaneseFood5:
                                        GameStateManager.instance.JapaneseFood5Level -= 1;
                                        break;
                                    case JapaneseFoodType.JapaneseFood6:
                                        GameStateManager.instance.JapaneseFood6Level -= 1;
                                        break;
                                    case JapaneseFoodType.JapaneseFood7:
                                        GameStateManager.instance.JapaneseFood7Level -= 1;
                                        break;
                                    case JapaneseFoodType.Ramen:
                                        GameStateManager.instance.JapaneseFood8Level -= 1;
                                        break;
                                }
                                break;
                            case IslandType.Island4:
                                switch (GameStateManager.instance.DessertType)
                                {
                                    case DessertType.Dessert1:
                                        GameStateManager.instance.Dessert1Level -= 1;
                                        break;
                                    case DessertType.Dessert2:
                                        GameStateManager.instance.Dessert2Level -= 1;
                                        break;
                                    case DessertType.Dessert3:
                                        GameStateManager.instance.Dessert3Level -= 1;
                                        break;
                                    case DessertType.Dessert4:
                                        GameStateManager.instance.Dessert4Level -= 1;
                                        break;
                                    case DessertType.Dessert5:
                                        GameStateManager.instance.Dessert5Level -= 1;
                                        break;
                                    case DessertType.Dessert6:
                                        GameStateManager.instance.Dessert6Level -= 1;
                                        break;
                                    case DessertType.Dessert7:
                                        GameStateManager.instance.Dessert7Level -= 1;
                                        break;
                                    case DessertType.Dessert8:
                                        GameStateManager.instance.Dessert8Level -= 1;
                                        break;
                                    case DessertType.Dessert9:
                                        GameStateManager.instance.Dessert9Level -= 1;
                                        break;
                                    case DessertType.FruitSkewers:
                                        GameStateManager.instance.Dessert10Level -= 1;
                                        break;
                                }
                                break;
                        }

                        CheckFood();
                        CheckFoodState();
                        UpgradeInitialize();

                        SoundManager.instance.PlaySFX(GameSfxType.Shield);
                        NotionManager.instance.UseNotion4(NotionType.DefDestroyNotion);

                        return;
                    }
                }
            }

            if (GameStateManager.instance.Effect)
            {
                bombAllPartice.gameObject.SetActive(false);
                bombAllPartice.gameObject.SetActive(true);
                bombAllPartice.Play();

                bombPartice[(int)GameStateManager.instance.IslandType].gameObject.SetActive(false);
                bombPartice[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
                bombPartice[(int)GameStateManager.instance.IslandType].Play();
            }

            if(maxLevel >= 30 && level >= recoverLevel && !GameStateManager.instance.AutoUpgrade)
            {
                switch (GameStateManager.instance.IslandType)
                {
                    case IslandType.Island1:
                        recoverManager.FoodInitialize(GameStateManager.instance.FoodType, maxLevel);
                        break;
                    case IslandType.Island2:
                        recoverManager.CandyInitialize(GameStateManager.instance.CandyType, maxLevel);
                        break;
                    case IslandType.Island3:
                        recoverManager.JapaneseFoodInitialize(GameStateManager.instance.JapaneseFoodType, maxLevel);
                        break;
                    case IslandType.Island4:
                        recoverManager.DessertInitialize(GameStateManager.instance.DessertType, maxLevel);
                        break;
                }
            }

            DestoryFood();

            CheckFoodState();
            UpgradeInitialize();


            SoundManager.instance.PlaySFX(GameSfxType.UpgradeFail);
            if (!changeFoodManager.changeFoodView.activeInHierarchy)
            {
                NotionManager.instance.UseNotion(NotionType.FailUpgrade);
            }
        }

        if (feverFillamount.gameObject.activeInHierarchy)
        {
            if (!feverMode && level + 1 < maxLevel)
            {
                feverCount += 1;

                CheckFever();
            }
        }

        isUpgradeDelay = true;
        Invoke("WaitUpgradeDelay", delay);
    }

    public void RecoverFood(FoodType type)
    {
        switch (type)
        {
            case FoodType.Food1:
                GameStateManager.instance.Food1Level = recoverLevel;
                break;
            case FoodType.Food2:
                GameStateManager.instance.Food2Level = recoverLevel;
                break;
            case FoodType.Food3:
                GameStateManager.instance.Food3Level = recoverLevel;
                break;
            case FoodType.Food4:
                GameStateManager.instance.Food4Level = recoverLevel;
                break;
            case FoodType.Food5:
                GameStateManager.instance.Food5Level = recoverLevel;
                break;
            case FoodType.Food6:
                GameStateManager.instance.Food6Level = recoverLevel;
                break;
            case FoodType.Food7:
                GameStateManager.instance.Food7Level = recoverLevel;
                break;
            case FoodType.Ribs:
                GameStateManager.instance.Food8Level = recoverLevel;
                break;
        }

        ChangeFood(type);
    }

    public void RecoverCandy(CandyType type)
    {
        switch (type)
        {
            case CandyType.Candy1:
                GameStateManager.instance.Candy1Level = recoverLevel;
                break;
            case CandyType.Candy2:
                GameStateManager.instance.Candy2Level = recoverLevel;
                break;
            case CandyType.Candy3:
                GameStateManager.instance.Candy3Level = recoverLevel;
                break;
            case CandyType.Candy4:
                GameStateManager.instance.Candy4Level = recoverLevel;
                break;
            case CandyType.Candy5:
                GameStateManager.instance.Candy5Level = recoverLevel;
                break;
            case CandyType.Candy6:
                GameStateManager.instance.Candy6Level = recoverLevel;
                break;
            case CandyType.Candy7:
                GameStateManager.instance.Candy7Level = recoverLevel;
                break;
            case CandyType.Candy8:
                GameStateManager.instance.Candy8Level = recoverLevel;
                break;
            case CandyType.Candy9:
                GameStateManager.instance.Candy9Level = recoverLevel;
                break;
            case CandyType.Chocolate:
                GameStateManager.instance.Candy10Level = recoverLevel;
                break;
        }

        ChangeCandy(type);
    }

    public void RecoverJapanese(JapaneseFoodType type)
    {
        switch (type)
        {
            case JapaneseFoodType.JapaneseFood1:
                GameStateManager.instance.JapaneseFood1Level = recoverLevel;
                break;
            case JapaneseFoodType.JapaneseFood2:
                GameStateManager.instance.JapaneseFood2Level = recoverLevel;
                break;
            case JapaneseFoodType.JapaneseFood3:
                GameStateManager.instance.JapaneseFood3Level = recoverLevel;
                break;
            case JapaneseFoodType.JapaneseFood4:
                GameStateManager.instance.JapaneseFood4Level = recoverLevel;
                break;
            case JapaneseFoodType.JapaneseFood5:
                GameStateManager.instance.JapaneseFood5Level = recoverLevel;
                break;
            case JapaneseFoodType.JapaneseFood6:
                GameStateManager.instance.JapaneseFood6Level = recoverLevel;
                break;
            case JapaneseFoodType.JapaneseFood7:
                GameStateManager.instance.JapaneseFood7Level = recoverLevel;
                break;
            case JapaneseFoodType.Ramen:
                GameStateManager.instance.JapaneseFood8Level = recoverLevel;
                break;
        }

        ChangeJapaneseFood(type);
    }

    public void RecoverDessert(DessertType type)
    {
        switch (type)
        {
            case DessertType.Dessert1:
                GameStateManager.instance.Dessert1Level = recoverLevel;
                break;
            case DessertType.Dessert2:
                GameStateManager.instance.Dessert2Level = recoverLevel;
                break;
            case DessertType.Dessert3:
                GameStateManager.instance.Dessert3Level = recoverLevel;
                break;
            case DessertType.Dessert4:
                GameStateManager.instance.Dessert4Level = recoverLevel;
                break;
            case DessertType.Dessert5:
                GameStateManager.instance.Dessert5Level = recoverLevel;
                break;
            case DessertType.Dessert6:
                GameStateManager.instance.Dessert6Level = recoverLevel;
                break;
            case DessertType.Dessert7:
                GameStateManager.instance.Dessert7Level = recoverLevel;
                break;
            case DessertType.Dessert8:
                GameStateManager.instance.Dessert8Level = recoverLevel;
                break;
            case DessertType.Dessert9:
                GameStateManager.instance.Dessert9Level = recoverLevel;
                break;
            case DessertType.FruitSkewers:
                GameStateManager.instance.Dessert10Level = recoverLevel;
                break;
        }

        ChangeDessert(type);
    }

    public void SetParticle(bool check)
    {
        if(check)
        {
            if(feverMode)
            {
                yummyTimeParticle.gameObject.SetActive(true);
                yummyTime2Particle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
            }

            if (speicalFood)
            {
                speicalFoodParticle.gameObject.SetActive(false);
                speicalFoodParticle.gameObject.SetActive(true);
                speicalFoodParticle.Play();
            }
        }
        else
        {
            yummyTimeParticle.gameObject.SetActive(false);
            yummyTime2Particle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(false);

            speicalFoodParticle.gameObject.SetActive(false);
            rareFood.SetActive(false);
        }
    }

    void DestoryFood()
    {
        level = 0;
        nextLevel = 0;

        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Food1:
                        GameStateManager.instance.Food1Level = 0;
                        break;
                    case FoodType.Food2:
                        GameStateManager.instance.Food2Level = 0;
                        break;
                    case FoodType.Food3:
                        GameStateManager.instance.Food3Level = 0;
                        break;
                    case FoodType.Food4:
                        GameStateManager.instance.Food4Level = 0;
                        break;
                    case FoodType.Food5:
                        GameStateManager.instance.Food5Level = 0;
                        break;
                    case FoodType.Food6:
                        GameStateManager.instance.Food6Level = 0;
                        break;
                    case FoodType.Food7:
                        GameStateManager.instance.Food7Level = 0;
                        break;
                    case FoodType.Ribs:
                        GameStateManager.instance.Food8Level = 0;
                        break;
                }
                break;
            case IslandType.Island2:
                switch (GameStateManager.instance.CandyType)
                {
                    case CandyType.Candy1:
                        GameStateManager.instance.Candy1Level = 0;
                        break;
                    case CandyType.Candy2:
                        GameStateManager.instance.Candy2Level = 0;
                        break;
                    case CandyType.Candy3:
                        GameStateManager.instance.Candy3Level = 0;
                        break;
                    case CandyType.Candy4:
                        GameStateManager.instance.Candy4Level = 0;
                        break;
                    case CandyType.Candy5:
                        GameStateManager.instance.Candy5Level = 0;
                        break;
                    case CandyType.Candy6:
                        GameStateManager.instance.Candy6Level = 0;
                        break;
                    case CandyType.Candy7:
                        GameStateManager.instance.Candy7Level = 0;
                        break;
                    case CandyType.Candy8:
                        GameStateManager.instance.Candy8Level = 0;
                        break;
                    case CandyType.Candy9:
                        GameStateManager.instance.Candy9Level = 0;
                        break;
                    case CandyType.Chocolate:
                        GameStateManager.instance.Food8Level = 0;
                        break;
                }
                break;
            case IslandType.Island3:
                switch (GameStateManager.instance.JapaneseFoodType)
                {
                    case JapaneseFoodType.JapaneseFood1:
                        GameStateManager.instance.JapaneseFood1Level = 0;
                        break;
                    case JapaneseFoodType.JapaneseFood2:
                        GameStateManager.instance.JapaneseFood2Level = 0;
                        break;
                    case JapaneseFoodType.JapaneseFood3:
                        GameStateManager.instance.JapaneseFood3Level = 0;
                        break;
                    case JapaneseFoodType.JapaneseFood4:
                        GameStateManager.instance.JapaneseFood4Level = 0;
                        break;
                    case JapaneseFoodType.JapaneseFood5:
                        GameStateManager.instance.JapaneseFood5Level = 0;
                        break;
                    case JapaneseFoodType.JapaneseFood6:
                        GameStateManager.instance.JapaneseFood6Level = 0;
                        break;
                    case JapaneseFoodType.JapaneseFood7:
                        GameStateManager.instance.JapaneseFood7Level = 0;
                        break;
                    case JapaneseFoodType.Ramen:
                        GameStateManager.instance.JapaneseFood8Level = 0;
                        break;
                }
                break;
            case IslandType.Island4:
                switch (GameStateManager.instance.DessertType)
                {
                    case DessertType.Dessert1:
                        GameStateManager.instance.Dessert1Level = 0;
                        break;
                    case DessertType.Dessert2:
                        GameStateManager.instance.Dessert2Level = 0;
                        break;
                    case DessertType.Dessert3:
                        GameStateManager.instance.Dessert3Level = 0;
                        break;
                    case DessertType.Dessert4:
                        GameStateManager.instance.Dessert4Level = 0;
                        break;
                    case DessertType.Dessert5:
                        GameStateManager.instance.Dessert5Level = 0;
                        break;
                    case DessertType.Dessert6:
                        GameStateManager.instance.Dessert6Level = 0;
                        break;
                    case DessertType.Dessert7:
                        GameStateManager.instance.Dessert7Level = 0;
                        break;
                    case DessertType.Dessert8:
                        GameStateManager.instance.Dessert8Level = 0;
                        break;
                    case DessertType.Dessert9:
                        GameStateManager.instance.Dessert9Level = 0;
                        break;
                    case DessertType.FruitSkewers:
                        GameStateManager.instance.Dessert10Level = 0;
                        break;
                }
                break;
        }
    }

    void CheckFever()
    {
        feverFillamount.fillAmount = feverCount * 1.0f / feverMaxCount * 1.0f;
        feverText.text = LocalizationManager.instance.GetString("FeverGauge") + " : " + ((feverCount * 1.0f / feverMaxCount * 1.0f) * 100).ToString("N1") + "%";

        if (feverCount >= feverMaxCount)
        {
            feverMode = true;

            feverEffect.SetActive(true);
            backButton.SetActive(false);

            successText.GetText().color = Color.red;

            successPlus += feverPlus;
            defDestroy += 5;

            UpgradeInitialize();

            StartCoroutine(FeverCoroution());

            SoundManager.instance.PlaySFX(GameSfxType.Fever_In);
            SoundManager.instance.PlayFever();
            NotionManager.instance.UseNotion(NotionType.FeverNotion);
        }
    }

    IEnumerator FeverCoroution()
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
            yummyTime2Particle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(true);
        }

        feverText.enabled = false;

        feverCount = 0;
        GameStateManager.instance.FeverCount = 0;

        GameStateManager.instance.YummyTimeCount += 1;

        playerDataBase.YummyTimeCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("YummyTimeCount", playerDataBase.YummyTimeCount);

        float currentTime = 0f;
        float fillAmount = 0;

        while (currentTime < feverTime)
        {
            fillAmount = Mathf.Lerp(1.0f, 0, currentTime / feverTime);

            fillAmount = Mathf.Clamp01(fillAmount);

            feverFillamount.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        yummyTimeParticle.gameObject.SetActive(false);
        yummyTime2Particle[(int)GameStateManager.instance.IslandType].gameObject.SetActive(false);

        feverMode = false;
        feverFillamount.fillAmount = 0;

        defDestroy -= 5;

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

    void MaxLevelUpgradeSuccess() //만렙
    {
        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Food1:
                        playerDataBase.HamburgerMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("HamburgerMaxValue", playerDataBase.HamburgerMaxValue);

                        if (playerDataBase.NextFoodNumber == 0)
                        {
                            playerDataBase.NextFoodNumber = 1;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);

                            if(playerDataBase.InGameTutorial == 1)
                            {
                                moveArrow3.SetActive(true);
                            }

                            //if (playerDataBase.HamburgerMaxValue == 1)
                            //{
                            //    tutorialManager.Next1();
                            //}
                        }

                        lockManager.UnLocked(1);

                        break;
                    case FoodType.Food2:
                        playerDataBase.SandwichMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SandwichMaxValue", playerDataBase.SandwichMaxValue);

                        if (playerDataBase.NextFoodNumber == 1)
                        {
                            playerDataBase.NextFoodNumber = 2;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);

                            homeButton.SetActive(true);
                        }

                        lockManager.UnLocked(2);

                        break;
                    case FoodType.Food3:
                        playerDataBase.SnackLabMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SnackLabMaxValue", playerDataBase.SnackLabMaxValue);

                        if (playerDataBase.NextFoodNumber == 2)
                        {
                            playerDataBase.NextFoodNumber = 3;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }

                        lockManager.UnLocked(3);

                        break;
                    case FoodType.Food4:
                        playerDataBase.DrinkMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DrinkMaxValue", playerDataBase.DrinkMaxValue);

                        if (playerDataBase.NextFoodNumber == 3)
                        {
                            playerDataBase.NextFoodNumber = 4;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);

                            //if (playerDataBase.DrinkMaxValue == 1)
                            //{
                            //    tutorialManager.Next2();
                            //}
                        }

                        lockManager.UnLocked(4);

                        break;
                    case FoodType.Food5:
                        playerDataBase.PizzaMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("PizzaMaxValue", playerDataBase.PizzaMaxValue);

                        if (playerDataBase.NextFoodNumber == 4)
                        {
                            playerDataBase.NextFoodNumber = 5;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }

                        lockManager.UnLocked(5);

                        break;
                    case FoodType.Food6:
                        playerDataBase.DonutMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DonutMaxValue", playerDataBase.DonutMaxValue);

                        if (playerDataBase.NextFoodNumber == 5)
                        {
                            playerDataBase.NextFoodNumber = 6;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }

                        lockManager.UnLocked(6);

                        break;
                    case FoodType.Food7:
                        playerDataBase.FriesMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("FriesMaxValue", playerDataBase.FriesMaxValue);

                        if (playerDataBase.IslandNumber <= 0)
                        {
                            playerDataBase.IslandNumber = 1;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);

                            //if (playerDataBase.Candy1MaxValue == 0)
                            //{
                            //    tutorialManager.Next3();
                            //}
                        }

                        lockManager.UnLocked(7);

                        break;
                    case FoodType.Ribs:

                        break;
                }

                //Debug.Log(GameStateManager.instance.FoodType + " : Max Level!");

                break;
            case IslandType.Island2:
                switch (GameStateManager.instance.CandyType)
                {
                    case CandyType.Candy1:
                        playerDataBase.Candy1MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy1MaxValue", playerDataBase.Candy1MaxValue);

                        if (playerDataBase.NextFoodNumber2 == 0)
                        {
                            playerDataBase.NextFoodNumber2 = 1;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }

                        break;
                    case CandyType.Candy2:
                        playerDataBase.Candy2MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy2MaxValue", playerDataBase.Candy2MaxValue);

                        if (playerDataBase.NextFoodNumber2 == 1)
                        {
                            playerDataBase.NextFoodNumber2 = 2;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }

                        break;
                    case CandyType.Candy3:
                        playerDataBase.Candy3MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy3MaxValue", playerDataBase.Candy3MaxValue);

                        if (playerDataBase.NextFoodNumber2 == 2)
                        {
                            playerDataBase.NextFoodNumber2 = 3;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case CandyType.Candy4:
                        playerDataBase.Candy4MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy4MaxValue", playerDataBase.Candy4MaxValue);

                        if (playerDataBase.NextFoodNumber2 == 3)
                        {
                            playerDataBase.NextFoodNumber2 = 4;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case CandyType.Candy5:
                        playerDataBase.Candy5MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy5MaxValue", playerDataBase.Candy5MaxValue);

                        if (playerDataBase.NextFoodNumber2 == 4)
                        {
                            playerDataBase.NextFoodNumber2 = 5;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case CandyType.Candy6:
                        playerDataBase.Candy6MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy6MaxValue", playerDataBase.Candy6MaxValue);

                        if (playerDataBase.NextFoodNumber2 == 5)
                        {
                            playerDataBase.NextFoodNumber2 = 6;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case CandyType.Candy7:
                        playerDataBase.Candy7MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy7MaxValue", playerDataBase.Candy7MaxValue);

                        if (playerDataBase.NextFoodNumber2 == 6)
                        {
                            playerDataBase.NextFoodNumber2 = 7;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case CandyType.Candy8:
                        playerDataBase.Candy8MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy8MaxValue", playerDataBase.Candy8MaxValue);

                        if (playerDataBase.NextFoodNumber2 == 7)
                        {
                            playerDataBase.NextFoodNumber2 = 8;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case CandyType.Candy9:
                        playerDataBase.Candy9MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Candy9MaxValue", playerDataBase.Candy9MaxValue);

                        if (playerDataBase.IslandNumber <= 1)
                        {
                            playerDataBase.IslandNumber = 2;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);
                        }

                        break;
                    case CandyType.Chocolate:

                        break;
                }

                Debug.Log(GameStateManager.instance.CandyType + " : Max Level!");

                break;
            case IslandType.Island3:
                switch (GameStateManager.instance.JapaneseFoodType)
                {
                    case JapaneseFoodType.JapaneseFood1:
                        playerDataBase.JapaneseFood1MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("JapaneseFood1MaxValue", playerDataBase.JapaneseFood1MaxValue);

                        if (playerDataBase.NextFoodNumber3 == 0)
                        {
                            playerDataBase.NextFoodNumber3 = 1;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber3", playerDataBase.NextFoodNumber3);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood2:
                        playerDataBase.JapaneseFood2MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("JapaneseFood2MaxValue", playerDataBase.JapaneseFood2MaxValue);

                        if (playerDataBase.NextFoodNumber3 == 1)
                        {
                            playerDataBase.NextFoodNumber3 = 2;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber3", playerDataBase.NextFoodNumber3);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood3:
                        playerDataBase.JapaneseFood3MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("JapaneseFood3MaxValue", playerDataBase.JapaneseFood3MaxValue);

                        if (playerDataBase.NextFoodNumber3 == 2)
                        {
                            playerDataBase.NextFoodNumber3 = 3;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber3", playerDataBase.NextFoodNumber3);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood4:
                        playerDataBase.JapaneseFood4MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("JapaneseFood4MaxValue", playerDataBase.JapaneseFood4MaxValue);

                        if (playerDataBase.NextFoodNumber3 == 3)
                        {
                            playerDataBase.NextFoodNumber3 = 4;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber3", playerDataBase.NextFoodNumber3);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood5:
                        playerDataBase.JapaneseFood5MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("JapaneseFood5MaxValue", playerDataBase.JapaneseFood5MaxValue);

                        if (playerDataBase.NextFoodNumber3 == 4)
                        {
                            playerDataBase.NextFoodNumber3 = 5;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber3", playerDataBase.NextFoodNumber3);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood6:
                        playerDataBase.JapaneseFood6MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("JapaneseFood6MaxValue", playerDataBase.JapaneseFood6MaxValue);

                        if (playerDataBase.NextFoodNumber3 == 5)
                        {
                            playerDataBase.NextFoodNumber3 = 6;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber3", playerDataBase.NextFoodNumber3);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case JapaneseFoodType.JapaneseFood7:
                        playerDataBase.JapaneseFood7MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("JapaneseFood7MaxValue", playerDataBase.JapaneseFood7MaxValue);

                        if (playerDataBase.IslandNumber <= 2)
                        {
                            playerDataBase.IslandNumber = 3;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);
                        }

                        break;
                    case JapaneseFoodType.Ramen:

                        break;
                }

                Debug.Log(GameStateManager.instance.JapaneseFoodType + " : Max Level!");

                break;
            case IslandType.Island4:
                switch (GameStateManager.instance.DessertType)
                {
                    case DessertType.Dessert1:
                        playerDataBase.Dessert1MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dessert1MaxValue", playerDataBase.Dessert1MaxValue);

                        if (playerDataBase.NextFoodNumber4 == 0)
                        {
                            playerDataBase.NextFoodNumber4 = 1;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case DessertType.Dessert2:
                        playerDataBase.Dessert2MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dessert2MaxValue", playerDataBase.Dessert2MaxValue);

                        if (playerDataBase.NextFoodNumber4 == 1)
                        {
                            playerDataBase.NextFoodNumber4 = 2;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case DessertType.Dessert3:
                        playerDataBase.Dessert3MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dessert3MaxValue", playerDataBase.Dessert3MaxValue);

                        if (playerDataBase.NextFoodNumber4 == 2)
                        {
                            playerDataBase.NextFoodNumber4 = 3;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case DessertType.Dessert4:
                        playerDataBase.Dessert4MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dessert4MaxValue", playerDataBase.Dessert4MaxValue);

                        if (playerDataBase.NextFoodNumber4 == 3)
                        {
                            playerDataBase.NextFoodNumber4 = 4;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case DessertType.Dessert5:
                        playerDataBase.Dessert5MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dessert5MaxValue", playerDataBase.Dessert5MaxValue);

                        if (playerDataBase.NextFoodNumber4 == 4)
                        {
                            playerDataBase.NextFoodNumber4 = 5;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case DessertType.Dessert6:
                        playerDataBase.Dessert6MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dessert6MaxValue", playerDataBase.Dessert6MaxValue);

                        if (playerDataBase.NextFoodNumber4 == 5)
                        {
                            playerDataBase.NextFoodNumber4 = 6;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case DessertType.Dessert7:
                        playerDataBase.Dessert7MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dessert7MaxValue", playerDataBase.Dessert7MaxValue);

                        if (playerDataBase.NextFoodNumber4 == 6)
                        {
                            playerDataBase.NextFoodNumber4 = 7;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case DessertType.Dessert8:
                        playerDataBase.Dessert8MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dessert8MaxValue", playerDataBase.Dessert8MaxValue);

                        if (playerDataBase.NextFoodNumber4 == 7)
                        {
                            playerDataBase.NextFoodNumber4 = 8;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);

                            changeFoodAlarmObj.SetActive(true);
                            moveArrow3.SetActive(true);
                        }
                        break;
                    case DessertType.Dessert9:
                        playerDataBase.Dessert9MaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Dessert9MaxValue", playerDataBase.Dessert9MaxValue);

                        //if (playerDataBase.IslandNumber <= 3)
                        //{
                        //    playerDataBase.IslandNumber = 4;
                        //    PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);
                        //}

                        break;
                    case DessertType.FruitSkewers:
                        break;
                }

                Debug.Log(GameStateManager.instance.DessertType + " : Max Level!");

                break;
        }

        FirebaseAnalytics.LogEvent("MaxLevel");

        //questManager.CheckGoal();
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

        if (playerDataBase.InGameTutorial == 0)
        {
            moveArrow2.SetActive(false);
            moveArrow3.SetActive(true);

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

        if(speicalFood)
        {
            Debug.LogError("특별 음식 판매");

            PortionManager.instance.GetIslandCount((int)GameStateManager.instance.IslandType, Random.Range(1 + (level / 10), 10 + (level / 5)));
        }

        PlayfabManager.instance.UpdateSellPriceGold(sellPrice);
        PlayfabManager.instance.moneyAnimation.PlusMoney(sellPrice);

        if(playerDataBase.GuideIndex < 23)
        {
            GameStateManager.instance.GetSellGold += sellPrice;
        }

        if (level >= 9)
        {
            playerDataBase.SellCount += 1;
            GameStateManager.instance.SellCount += 1;

            if (Random.Range(0, 100f) < eventTicket)
            {
                PortionManager.instance.GetEventTicket(1);

                playerDataBase.EventTicketCount += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventTicketCount", playerDataBase.EventTicketCount);

                Debug.LogError("이벤트 티켓 획득");
            }
        }

        OFFSpeicalFood();

        speicalFoodCount += 1;

        if (speicalFoodCount >= speicalFoodNeedCount)
        {
            speicalFoodCount = 0;

            if (Random.Range(0, 100f) < speicalFoodPlus)
            {
                speicalFood = true;

                Debug.LogError("레어 음식 등장!");

                SoundManager.instance.PlaySFX(GameSfxType.ChestBox);

                if (!changeFoodManager.changeFoodView.activeInHierarchy)
                {
                    NotionManager.instance.UseNotion3(NotionType.SpeicalFoodNotion);
                }

                if(GameStateManager.instance.Effect)
                {
                    speicalFoodParticle.gameObject.SetActive(false);
                    speicalFoodParticle.gameObject.SetActive(true);
                    speicalFoodParticle.Play();
                    rareFood.SetActive(true);
                }
            }
        }

        myMoneyPlusText.gameObject.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(true);
        myMoneyPlusText.color = Color.green;
        myMoneyPlusText.text = "+" + MoneyUnitString.ToCurrencyString(sellPrice);

        DestoryFood();
        CheckFoodState();
        UpgradeInitialize();

        SoundManager.instance.PlaySFX(GameSfxType.Sell);

        FirebaseAnalytics.LogEvent("SellFood");

        isUpgradeDelay = true;
        Invoke("WaitSellDelay", delay);
    }

    public void CheckDefTicket()
    {
        if(level >= 1 && level + 1 < maxLevel && playerDataBase.LockTutorial > 1)
        {
            defTicketObj.SetActive(true);

            defTicketText.text = playerDataBase.DefDestroyTicket + "/1";

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

        defDestroyText.localizationName = "DefDestroyPercent";
        defDestroyText.plusText = " : " + defDestroy.ToString("N2") + "%";
        defDestroyText.ReLoad();
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
            if (playerDataBase.Portion1 == 0)
            {
                portionText1.text = "-";
            }
            else
            {
                portionText1.text = playerDataBase.Portion1.ToString();
            }

            if (playerDataBase.Portion2 == 0)
            {
                portionText2.text = "-";
            }
            else
            {
                portionText2.text = playerDataBase.Portion2.ToString();
            }

            if (playerDataBase.Portion3 == 0)
            {
                portionText3.text = "-";
            }
            else
            {
                portionText3.text = playerDataBase.Portion3.ToString();
            }

            if (playerDataBase.Portion4 == 0)
            {
                portionText4.text = "-";
            }
            else
            {
                portionText4.text = playerDataBase.Portion4.ToString();
            }

            if (playerDataBase.Portion5 == 0)
            {
                portionText5.text = "-";
            }
            else
            {
                portionText5.text = playerDataBase.Portion5.ToString();
            }

            if (playerDataBase.Portion6 == 0)
            {
                portionText6.text = "-";
            }
            else
            {
                portionText6.text = playerDataBase.Portion6.ToString();
            }

            guideMissionManager.Initialize();
        }
    }

    public void UseSources(int number)
    {
        if (!clickDelay) return;

        switch(number)
        {
            case 0:
                if(!portion1)
                {
                    if(playerDataBase.Portion1 > 0)
                    {
                        portion1 = true;

                        needPlus += portion1Value;
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
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion1);

                        FirebaseAnalytics.LogEvent("UsePortion1");
                    }
                    else
                    {
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
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion2);

                        FirebaseAnalytics.LogEvent("UsePortion2");
                    }
                    else
                    {
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
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion3);

                        FirebaseAnalytics.LogEvent("UsePortion3");
                    }
                    else
                    {
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
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion4);

                        FirebaseAnalytics.LogEvent("UsePortion4");
                    }
                    else
                    {
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
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion5);

                        FirebaseAnalytics.LogEvent("UsePortion5");
                    }
                    else
                    {
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

                        needPlus += 30;
                        sellPricePlus += 10;
                        successPlus += 1;
                        feverCount = feverMaxCount;
                        CheckFever();
                        defDestroy += 10;

                        UpgradeInitialize();

                        playerDataBase.Portion6 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion6", playerDataBase.Portion6);

                        playerDataBase.UseSauceCount += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSauceCount);

                        StartCoroutine(PortionCoroution6());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion6);

                        FirebaseAnalytics.LogEvent("UsePortion6");
                    }
                    else
                    {
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
        float currentTime = 0f;

        while (currentTime < portion1Time)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion1Time);

            fillAmount = Mathf.Clamp01(fillAmount);

            portionFillamount1.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        portion1 = false;
        portionFillamount1.fillAmount = 0;

        needPlus -= 30;
        UpgradeInitialize();
    }

    IEnumerator PortionCoroution2()
    {
        float currentTime = 0f;

        while (currentTime < portion2Time)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion2Time);

            fillAmount = Mathf.Clamp01(fillAmount);

            portionFillamount2.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        portion2 = false;
        portionFillamount2.fillAmount = 0;

        sellPricePlus -= 10;
        UpgradeInitialize();
    }

    IEnumerator PortionCoroution3()
    {
        float currentTime = 0f;

        while (currentTime < portion3Time)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion3Time);

            fillAmount = Mathf.Clamp01(fillAmount);

            portionFillamount3.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        portion3 = false;
        portionFillamount3.fillAmount = 0;

        successPlus -= 1;
        UpgradeInitialize();
    }

    IEnumerator PortionCoroution5()
    {
        float currentTime = 0f;

        while (currentTime < portion5Time)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion5Time);

            fillAmount = Mathf.Clamp01(fillAmount);

            portionFillamount5.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        portion5 = false;
        portionFillamount5.fillAmount = 0;

        defDestroy -= 5;
        UpgradeInitialize();
    }

    IEnumerator PortionCoroution6()
    {
        float currentTime = 0f;

        while (currentTime < portion6Time)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion6Time);

            fillAmount = Mathf.Clamp01(fillAmount);

            portionFillamount6.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        portion6 = false;
        portionFillamount6.fillAmount = 0;

        needPlus -= 30;
        sellPricePlus -= 10;
        successPlus -= 1;
        defDestroy -= 10;
        UpgradeInitialize();
    }


    public void OnBuff(int number)
    {
        switch(number)
        {
            case 0:
                buff1 = true;

                sellPricePlus += buff1Value;
                UpgradeInitialize();
                break;
            case 1:
                buff2 = true;

                defDestroy += buff2Value;

                defDestroyText.localizationName = "DefDestroyPercent";
                defDestroyText.plusText = " : " + defDestroy.ToString("N2") + "%";
                defDestroyText.ReLoad();
                break;
            case 2:
                buff3 = true;

                successX2 += buff3Value;

                successX2Text.localizationName = "SuccessX2Percent";
                successX2Text.plusText = " : " + successX2.ToString("N1") + "%";
                successX2Text.ReLoad();
                break;
            case 3:
                buff4 = true;

                if (!buffAutoUpgrade)
                {
                    buffAutoUpgrade = true;
                    StartCoroutine(BuffAutoUpgradeCoroution());
                }
                break;
        }

        guideMissionManager.Initialize();
    }

    public void OffBuff(int number)
    {
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

                defDestroyText.localizationName = "DefDestroyPercent";
                defDestroyText.plusText = " : " + defDestroy.ToString("N2") + "%";
                defDestroyText.ReLoad();
                break;
            case 2:
                buff3 = false;

                successX2 += buff3Value;

                successX2Text.localizationName = "SuccessX2Percent";
                successX2Text.plusText = " : " + successX2.ToString("N1") + "%";
                successX2Text.ReLoad();
                break;
            case 3:
                buff4 = false;

                buffAutoUpgrade = false;
                break;
        }
    }

    public void Reincarnation()
    {
        islandImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];

        CheckFood();
        CheckFoodState();
    }

    public void OpenLoginView()
    {
        if (!loginView.activeSelf)
        {
            loginView.SetActive(true);

            loginButtonArray[0].SetActive(true);
            loginButtonArray[1].SetActive(false);
            loginButtonArray[2].SetActive(false);

#if UNITY_EDITOR
            loginButtonArray[0].SetActive(true);
#elif UNITY_ANDROID
            loginButtonArray[1].SetActive(true);
#elif UNITY_IOS
            loginButtonArray[2].SetActive(true);
#endif
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
#if UNITY_ANDROID || UNITY_EDITOR
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.whilili.foodtruck");
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/us/app/food-truck-evolution/id6466390705");
#endif
    }

    //public void MoreGame()
    //{
    //    FirebaseAnalytics.LogEvent("MoreGame");

    //    Application.OpenURL("https://play.google.com/store/apps/dev?id=6063135311448213232");
    //}

    public void OpenURL()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.whilili.foodtruck");
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/us/app/food-truck-evolution/id6466390705");
#endif

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);

        FirebaseAnalytics.LogEvent("OpenAppReview");

        CloseAppReview();
    }

    public void OpenReview()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.whilili.foodtruck");
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/us/app/food-truck-evolution/id6466390705");
#endif

        FirebaseAnalytics.LogEvent("OpenAppReviewEvent");
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

        FirebaseAnalytics.LogEvent("FeedBack");
    }

    public void OpenPrivacypolicyURL()
    {
        Application.OpenURL("https://sites.google.com/view/whilili-privacypolicy");

        FirebaseAnalytics.LogEvent("Privacypolicy");
    }

    public void OpenTermsURL()
    {
        Application.OpenURL("https://sites.google.com/view/whilili-terms");

        FirebaseAnalytics.LogEvent("Terms");
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
        if(GameStateManager.instance.Food1Level <= 1 && GameStateManager.instance.Food2Level <= 1 && GameStateManager.instance.Food3Level <= 1
            && GameStateManager.instance.Food4Level <= 1 && GameStateManager.instance.Food5Level <= 1 && GameStateManager.instance.Food6Level <= 1
            && GameStateManager.instance.Food7Level <= 1 && GameStateManager.instance.Candy1Level <= 1 && GameStateManager.instance.Candy2Level <= 1
            && GameStateManager.instance.Candy3Level <= 1 && GameStateManager.instance.Candy4Level <= 1 && GameStateManager.instance.Candy5Level <= 1
            && GameStateManager.instance.Candy6Level <= 1 && GameStateManager.instance.Candy7Level <= 1 && GameStateManager.instance.Candy8Level <= 1
            && GameStateManager.instance.Candy9Level <= 1 && GameStateManager.instance.JapaneseFood1Level <= 1 && GameStateManager.instance.JapaneseFood2Level <= 1
            && GameStateManager.instance.JapaneseFood3Level <= 1 && GameStateManager.instance.JapaneseFood4Level <= 1
            && GameStateManager.instance.JapaneseFood5Level <= 1 && GameStateManager.instance.JapaneseFood6Level <= 1
            && GameStateManager.instance.JapaneseFood7Level <= 1 && GameStateManager.instance.Dessert1Level <= 1 && GameStateManager.instance.Dessert2Level <= 1
            && GameStateManager.instance.Dessert3Level <= 1 && GameStateManager.instance.Dessert4Level <= 1 && GameStateManager.instance.Dessert5Level <= 1
            && GameStateManager.instance.Dessert6Level <= 1 && GameStateManager.instance.Dessert7Level <= 1 && GameStateManager.instance.Dessert8Level <= 1
            && GameStateManager.instance.Dessert9Level <= 1
            && playerDataBase.Coin < 10000)
        {
            bankruptcyView.SetActive(true);

            if(GameStateManager.instance.Bankruptcy < 1)
            {
                bankruptcyText.text = MoneyUnitString.ToCurrencyString(100000);
            }
            else
            {
                bankruptcyText.text = MoneyUnitString.ToCurrencyString(30000);
            }
        }
    }

    public void ReceiveBankruptcy()
    {
        bankruptcyView.SetActive(false);

        if (GameStateManager.instance.Bankruptcy < 1)
        {
            PlayfabManager.instance.UpdateAddGold(100000);
        }
        else
        {
            PlayfabManager.instance.UpdateAddGold(30000);
        }

        SoundManager.instance.PlaySFX(GameSfxType.GetMoney);

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
        playerDataBase.IslandNumber = 3;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);
    }

    public void GetFood()
    {
        playerDataBase.NextFoodNumber = 6;
        playerDataBase.NextFoodNumber2 = 8;
        playerDataBase.NextFoodNumber3 = 6;
        playerDataBase.NextFoodNumber4 = 8;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber3", playerDataBase.NextFoodNumber3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);
    }

    public void GetMaxLevel()
    {
        playerDataBase.Skill1 = 100;
        playerDataBase.Skill2 = 100;
        playerDataBase.Skill3 = 100;
        playerDataBase.Skill4 = 100;
        playerDataBase.Skill5 = 100;
        playerDataBase.Skill6 = 100;
        playerDataBase.Skill7 = 100;
        playerDataBase.Skill8 = 100;
        playerDataBase.Skill9 = 100;
        playerDataBase.Skill10 = 100;
        playerDataBase.Skill11 = 100;
        playerDataBase.Skill12 = 100;
        playerDataBase.Skill13 = 100;
        playerDataBase.Skill14 = 100;

        playerDataBase.Treasure1 = 100;
        playerDataBase.Treasure2 = 100;
        playerDataBase.Treasure3 = 100;
        playerDataBase.Treasure4 = 100;
        playerDataBase.Treasure5 = 100;
        playerDataBase.Treasure6 = 100;
        playerDataBase.Treasure7 = 100;
        playerDataBase.Treasure8 = 100;
        playerDataBase.Treasure9 = 100;
        playerDataBase.Treasure10 = 100;
        playerDataBase.Treasure11 = 100;
        playerDataBase.Treasure12 = 100;


        playerDataBase.Level = 100;
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

        playerDataBase.Treasure1 = 0;
        playerDataBase.Treasure2 = 0;
        playerDataBase.Treasure3 = 0;
        playerDataBase.Treasure4 = 0;
        playerDataBase.Treasure5 = 0;
        playerDataBase.Treasure6 = 0;
        playerDataBase.Treasure7 = 0;
        playerDataBase.Treasure8 = 0;
        playerDataBase.Treasure9 = 0;
    }

    public void GetResetReward()
    {
        playerDataBase.AttendanceCheck = false;
        playerDataBase.WelcomeCheck = false;

        playerDataBase.DailyReward = 0;
        playerDataBase.DailyReward_Portion = 0;
        playerDataBase.DailyReward_DefTicket = 0;
        playerDataBase.DailyReward_Crystal = 0;
        playerDataBase.DailyAdsReward = 0;
        playerDataBase.DailyAdsReward2 = 0;
        playerDataBase.DailyCastleReward = 0;
        playerDataBase.DailyQuestReward = 0;
        playerDataBase.DailyTreasureReward = 0;
        playerDataBase.DailyDungeonKey1 = 0;
        playerDataBase.DailyDungeonKey2 = 0;
        playerDataBase.DailyDungeonKey3 = 0;
        playerDataBase.DailyDungeonKey4 = 0;

        GameStateManager.instance.UpgradeCount = 0;
        GameStateManager.instance.SellCount = 0;
        GameStateManager.instance.UseSauce = 0;
        GameStateManager.instance.OpenChestBox = 0;
        GameStateManager.instance.YummyTimeCount = 0;

        GameStateManager.instance.ChestBoxCount = 0;

        playerDataBase.PlayTimeCount = 0;
        playerDataBase.RankEventCount = 0;
    }

    public void GetGold()
    {
        PlayfabManager.instance.UpdateAddGold(100000000);
    }

    public void GetCrystal()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100000);
    }

    public void GetPortion()
    {
        PortionManager.instance.GetAllPortion(999);

        playerDataBase.Portion5 += 999;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
    }

    public void GetBuffTicket()
    {
        PortionManager.instance.GetBuffTickets(999);
    }

    public void GetDefTicket()
    {
        PortionManager.instance.GetDefTickets(999);
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

    public void GetAbilityPoint()
    {
        playerDataBase.AbilityPoint += 10000;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AbilityPoint", playerDataBase.AbilityPoint);
    }

    public void GetGourmetLevel()
    {
        playerDataBase.GourmetLevel += 1000000;
    }

    public void GetUnLocked()
    {
        lockManager.UnLocked(7);
    }

    public void GetExp()
    {
        playerDataBase.Exp += 100000;

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
        NotionManager.instance.UseNotion(NotionType.TipInfoNotion);
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
        }
    }

    public void GetUpgradeCount()
    {
        playerDataBase.UpgradeCount += Random.Range(5000, 10001);

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UpgradeCount", playerDataBase.UpgradeCount);
    }

    public void CheckLocked()
    {
        butterflyLocked.SetActive(false);
        treasureLocked.SetActive(true);
        rankLocked.SetActive(true);

        //if (levelDataBase.GetLevel(playerDataBase.Exp) > 1)
        //{
        //    butterflyLocked.SetActive(false);
        //}

        if (playerDataBase.Level > 4)
        {
            treasureLocked.SetActive(false);
        }

        if (playerDataBase.Level > 7)
        {
            rankLocked.SetActive(false);
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

        speicalFoodPlus = 100;
    }

    public void DeveloperOff()
    {
        GameStateManager.instance.Developer = false;

        speicalFoodPlus = 10;
    }

    public void GetEnableAllItem()
    {
        playerDataBase.ChipsTruck = 1;
        playerDataBase.DonutTruck = 1;
        playerDataBase.HamburgerTruck = 1;
        playerDataBase.IcecreamTruck = 1;
        playerDataBase.LemonadeTruck = 1;
        playerDataBase.NoodlesTruck = 1;
        playerDataBase.PizzaTruck = 1;
        playerDataBase.SushiTruck = 1;

        playerDataBase.GeckoAnimal = 1;
        playerDataBase.HerringAnimal = 1;
        playerDataBase.MuskratAnimal = 1;
        playerDataBase.PuduAnimal = 1;
        playerDataBase.SparrowAnimal = 1;
        playerDataBase.SquidAnimal = 1;
        playerDataBase.TaipanAnimal = 1;

        playerDataBase.Butterfly1 = 1;
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

        playerDataBase.Character1 = 1;
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

        playerDataBase.Totems1 = 1;
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
    }

    public void GetDisableAllItem()
    {
        playerDataBase.ChipsTruck = 0;
        playerDataBase.DonutTruck = 0;
        playerDataBase.HamburgerTruck = 0;
        playerDataBase.IcecreamTruck = 0;
        playerDataBase.LemonadeTruck = 0;
        playerDataBase.NoodlesTruck = 0;
        playerDataBase.PizzaTruck = 0;
        playerDataBase.SushiTruck = 0;

        playerDataBase.GeckoAnimal = 0;
        playerDataBase.HerringAnimal = 0;
        playerDataBase.MuskratAnimal = 0;
        playerDataBase.PuduAnimal = 0;
        playerDataBase.SparrowAnimal = 0;
        playerDataBase.SquidAnimal = 0;
        playerDataBase.TaipanAnimal = 0;

        playerDataBase.Butterfly1 = 0;
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

        playerDataBase.Character1 = 0;
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

        playerDataBase.Totems1 = 0;
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
        playerDataBase.DefDestroyTicket = 0;
        playerDataBase.DefDestroyTicketPiece = 0;
    }
}
