using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject checkUpdate;

    public CameraController cameraController;

    public Text goldText;
    public Text crystalText;
    public Text signText;

    public GameObject goldx2;
    public GameObject removeAds;

    public GameObject privacypolicyView;

    public GameObject coupon;
    public GameObject deleteAccount;

    public GameObject changeFoodAlarmObj;

    public Image changeFoodImg;
    public Image islandImg;

    public LocalizationContent islandText;

    public LocalizationContent bestRankLevelText;

    public GameObject butterflyLocked;
    public GameObject rankLocked;
    public GameObject treasureLocked;

    public GameObject notUpgrade;
    public GameObject notSell;

    public GameObject testModeButton;
    public Text testModeText;
    public GameObject testMode;

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
    [Title("Animal")]
    public Animator[] characterAnimator;
    public Animator[] animalAnimator;
    public Animator[] butterflyAnimator;

    [Space]
    [Title("Upgrade")]
    public GameObject mainUI;
    public GameObject inGameUI;
    public GameObject languageUI;

    public Text myMoneyPlusText;

    public Text portionText1, portionText2, portionText3, portionText4, portionText5, portionText6;
    public Image portionFillamount1, portionFillamount2, portionFillamount3, portionFillamount4, portionFillamount5, portionFillamount6;
    private bool portion1, portion2, portion3, portion4, portion5, portion6;

    public Text titleText;
    public Text highLevelText;
    public Text needText;
    public Text priceText;
    public Text successText;
    public Text defDestroyText;
    public Text successX2Text;

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

    public int need = 0;
    private float needPlus = 0;

    public int sellPrice = 0;
    private float sellPricePlus = 0;

    private float sellPriceTip = 0;
    private int expUp = 0;

    public float success = 0;
    private float successPlus = 0;

    private float successX2 = 0;

    private float portion1Time = 0;
    private float portion2Time = 0;
    private float portion3Time = 0;
    private float portion4Plus = 0;
    private float portion5Time = 0;
    private float portion6Time = 30;

    public int level = 0;
    public int nextLevel = 0;

    private int maxLevel = 0;

    public bool isDelay_Camera = false;
    public bool isDelay = false;
    public bool isDef = false;
    private bool buff1 = false;
    private bool buff2 = false;

    private bool feverMode = false;

    private int nowExp = 0;
    private int nowUpgradeCount = 0;
    private int nowSellCount = 0;

    private int defaultNeed = 150;
    private int defaultSellPrice = 2000;

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

    Sprite[] islandArray;

    public ParticleSystem lightParticle;
    public ParticleSystem levelUpParticle;
    public ParticleSystem bombPartice;

    public TutorialManager tutorialManager;
    public LockManager lockManager;
    public QuestManager questManager;
    public LevelManager levelManager;
    public ChangeFoodManager changeFoodManager;

    UpgradeDataBase upgradeDataBase;
    PlayerDataBase playerDataBase;
    CharacterDataBase characterDataBase;
    TruckDataBase truckDataBase;
    AnimalDataBase animalDataBase;
    ButterflyDataBase butterflyDataBase;
    IslandDataBase islandDataBase;
    ImageDataBase imageDataBase;
    LevelDataBase levelDataBase;

    WaitForSeconds waitForSeconds = new WaitForSeconds(3.0f);

    private void Awake()
    {
        instance = this;

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

        islandArray = imageDataBase.GetIslandArray();

        goldText.text = "0";
        crystalText.text = "0";
        signText.text = "0";

        loginView.SetActive(false);
        deleteAccountView.SetActive(false);
        appReview.SetActive(false);
        checkInternet.SetActive(false);

        loginButtonArray[0].SetActive(false);
        loginButtonArray[1].SetActive(false);
        loginButtonArray[2].SetActive(false);

        mainUI.SetActive(true);
        inGameUI.SetActive(false);

        isDelay = false;
        isDef = false;

        checkMark.SetActive(false);

        feverMode = false;
        feverCount = 0;
        feverFillamount.fillAmount = 0;

        feverEffect.SetActive(false);

        levelUpParticle.gameObject.SetActive(false);
        bombPartice.gameObject.SetActive(false);

        portion1 = false;
        portion2 = false;
        portion3 = false;
        portion4 = false;
        portion5 = false;
        portion6 = false;

        portionFillamount1.fillAmount = 0;
        portionFillamount2.fillAmount = 0;
        portionFillamount3.fillAmount = 0;
        portionFillamount4.fillAmount = 0;
        portionFillamount5.fillAmount = 0;
        portionFillamount6.fillAmount = 0;

        defTicketObj.SetActive(false);
        bankruptcyView.SetActive(false);
        privacypolicyView.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(false);
        lightParticle.gameObject.SetActive(false);
        testMode.SetActive(false);

        checkUpdate.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        if (inGameUI.activeInHierarchy)
        {
            GameStateManager.instance.FeverCount = feverCount;
        }
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
#endif

        if (!GameStateManager.instance.Privacypolicy)
        {
            privacypolicyView.SetActive(true);
        }
    }

    public void SuccessLogin()
    {
        removeAds.SetActive(false);
        goldx2.SetActive(false);

        if (playerDataBase.RemoveAds)
        {
            removeAds.SetActive(true);
        }

        if (playerDataBase.GoldX2)
        {
            goldx2.SetActive(true);
        }

        checkInternet.SetActive(false);
        loginView.SetActive(false);

        islandImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];
        islandText.localizationName = GameStateManager.instance.IslandType.ToString();
        islandText.ReLoad();

        feverText.text = LocalizationManager.instance.GetString("FeverGauge") + "  0%";

        nowExp = playerDataBase.Exp;
        nowUpgradeCount = playerDataBase.UpgradeCount;
        nowSellCount = playerDataBase.SellCount;
        StartCoroutine(DelayCoroution());

        CheckFood();
        CheckFoodState();

        isDelay_Camera = true;

        PlayfabManager.instance.GetTitleInternalData("Coupon", CheckCoupon);

        if(!GameStateManager.instance.Tutorial)
        {
            tutorialManager.TutorialStart();
        }

        levelManager.Initialize();

        bestRankLevelText.localizationName = "Best";
        bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel;
        bestRankLevelText.ReLoad();

        CheckLocked();

        testModeButton.SetActive(false);

        if(playerDataBase.TestAccount > 0)
        {
            testModeButton.SetActive(true);
            testModeText.text = "Developer Mode ON";
        }

#if UNITY_EDITOR
        testModeButton.SetActive(true);
#endif

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Version", int.Parse(Application.version.Replace(".","")));

        changeFoodManager.CheckProficiency();

        questManager.CheckingAlarm();
    }

    void CheckFood()
    {
        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Hamburger:
                        level = GameStateManager.instance.HamburgerLevel;
                        nextLevel = (GameStateManager.instance.HamburgerLevel + 1) / 5;

                        if (hamburgerArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.Sandwich:
                        level = GameStateManager.instance.SandwichLevel;
                        nextLevel = (GameStateManager.instance.SandwichLevel + 1) / 5;

                        if (sandwichArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.SnackLab:
                        level = GameStateManager.instance.SnackLabLevel;
                        nextLevel = (GameStateManager.instance.SnackLabLevel + 1) / 5;

                        if (snackLabArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.Drink:
                        level = GameStateManager.instance.DrinkLevel;
                        nextLevel = (GameStateManager.instance.DrinkLevel + 1) / 5;

                        if (drinkArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.Pizza:
                        level = GameStateManager.instance.PizzaLevel;
                        nextLevel = (GameStateManager.instance.PizzaLevel + 1) / 5;

                        if (pizzaArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }
                        break;
                    case FoodType.Donut:
                        level = GameStateManager.instance.DonutLevel;
                        nextLevel = (GameStateManager.instance.DonutLevel + 1) / 5;

                        if (donutArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }

                        break;
                    case FoodType.Fries:
                        level = GameStateManager.instance.FriesLevel;
                        nextLevel = (GameStateManager.instance.FriesLevel + 1) / 5;

                        if (friesArray.Length - 1 < nextLevel)
                        {
                            nextLevel -= 1;
                        }

                        break;
                    case FoodType.Ribs:
                        level = GameStateManager.instance.RibsLevel;
                        nextLevel = (GameStateManager.instance.RibsLevel + 1) / 5;

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
                        level = GameStateManager.instance.ChocolateLevel;
                        nextLevel = (GameStateManager.instance.ChocolateLevel + 1) / 5;

                        if (candy10Array.Length - 1 < nextLevel)
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
        playerDataBase.Portion1 += 10;
        playerDataBase.Portion2 += 10;
        playerDataBase.Portion3 += 10;
        playerDataBase.Portion4 += 10;

        playerDataBase.BuffTickets += 2;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);

        yield return new WaitForSeconds(0.5f);

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuffTickets", playerDataBase.BuffTickets);

        CheckPortion();
    }

    void CheckCoupon(bool check)
    {
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
        playerDataBase.Coin = playerDataBase.CoinA + (playerDataBase.CoinB * 100000000);

        goldText.text = MoneyUnitString.ToCurrencyString(playerDataBase.Coin);
        crystalText.text = MoneyUnitString.ToCurrencyString(playerDataBase.Crystal);
    }

    public void GameStart(int number)
    {
        if (!isDelay_Camera) return;

        isDelay_Camera = false;

        GameStateManager.instance.GameType = GameType.Story + number;

        if(GameStateManager.instance.GameType == GameType.Story)
        {
            switch (GameStateManager.instance.IslandType)
            {
                case IslandType.Island1:
                    if(GameStateManager.instance.FoodType == FoodType.Ribs)
                    {
                        ChangeFood(FoodType.Hamburger);
                    }
                    break;
                case IslandType.Island2:
                    if (GameStateManager.instance.CandyType == CandyType.Chocolate)
                    {
                        ChangeCandy(CandyType.Candy1);
                    }
                    break;
                case IslandType.Island3:
                    break;
                case IslandType.Island4:
                    break;
            }

            FirebaseAnalytics.LogEvent("NormalMode");
        }
        else
        {
            switch (GameStateManager.instance.IslandType)
            {
                case IslandType.Island1:
                    ChangeFood(FoodType.Ribs);
                    break;
                case IslandType.Island2:
                    ChangeCandy(CandyType.Chocolate);
                    break;
                case IslandType.Island3:
                    break;
                case IslandType.Island4:
                    break;
            }

            FirebaseAnalytics.LogEvent("RankingMode");
        }

        mainUI.SetActive(false);
        inGameUI.SetActive(true);
        languageUI.SetActive(false);

        isDef = false;
        checkMark.SetActive(false);

        successPlus = 0;
        successX2 = 0;
        needPlus = 0;
        sellPricePlus = 0;
        sellPriceTip = 0;
        defDestroy = 0;
        expUp = 0;

        //changeFoodImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];

        if (GameStateManager.instance.CharacterType > CharacterType.Character1)
        {
            successPlus += characterDataBase.GetCharacterEffect(GameStateManager.instance.CharacterType);
        }

        successPlus += playerDataBase.Skill7 * 0.1f;
        successPlus += levelDataBase.GetLevel(playerDataBase.Exp) * 0.1f;
        successPlus += playerDataBase.Treasure1 * 0.2f;

        successX2 += playerDataBase.Treasure3 * 0.1f;

        if (GameStateManager.instance.TruckType > TruckType.Bread)
        {
            sellPricePlus += truckDataBase.GetTruckEffect(GameStateManager.instance.TruckType);
        }

        sellPricePlus += playerDataBase.Skill8 * 0.2f;
        sellPricePlus += playerDataBase.Proficiency * 1;

        sellPriceTip = playerDataBase.Skill14 * 0.5f; //ÆÁ È®·ü

        if (GameStateManager.instance.AnimalType > AnimalType.Colobus)
        {
            expUp += (int)animalDataBase.GetAnimalEffect(GameStateManager.instance.AnimalType);
        }

        if (GameStateManager.instance.ButterflyType > ButterflyType.Butterfly1)
        {
            defDestroy += butterflyDataBase.GetButterflyEffect(GameStateManager.instance.ButterflyType);
        }

        defDestroy += playerDataBase.Skill9 * 0.05f;
        defDestroy += playerDataBase.Treasure2 * 0.1f;

        needPlus += playerDataBase.Skill10 * 0.3f;

        upgradeFood = upgradeDataBase.GetUpgradeFood(GameStateManager.instance.FoodType);
        upgradeCandy = upgradeDataBase.GetUpgradeCandy(GameStateManager.instance.CandyType);

        feverCount = GameStateManager.instance.FeverCount;

        feverTime = 30 + (30 * (0.003f * playerDataBase.Skill1));

        feverMaxCount = 300 - (300 * (0.003f * playerDataBase.Skill2));
        feverPlus = 3 + (3 * (0.01f * playerDataBase.Skill3));

        portion1Time = 30 + (30 * (0.003f * playerDataBase.Skill4)) + (30 * (0.005f * playerDataBase.Treasure6));
        portion2Time = 30 + (30 * (0.003f * playerDataBase.Skill5)) + (30 * (0.005f * playerDataBase.Treasure6));
        portion3Time = 30 + (30 * (0.003f * playerDataBase.Skill6)) + (30 * (0.005f * playerDataBase.Treasure6));
        portion4Plus = 0.03f * playerDataBase.Skill12;
        portion5Time = 30 + (30 * (0.003f * playerDataBase.Skill13)) + (30 * (0.005f * playerDataBase.Treasure6));

        if (playerDataBase.GoldX2)
        {
            sellPricePlus += 100;
        }

        if (portion1)
        {
            needPlus += 30;
        }

        if (portion2)
        {
            sellPricePlus += 10;
        }

        if (portion3)
        {
            successPlus += 1;
        }

        if (feverMode)
        {
            successPlus += feverPlus;
            defDestroy += 5;
        }

        if (portion5)
        {
            defDestroy += 5;
        }

        if (buff1)
        {
            sellPricePlus += 30;
        }

        if (buff2)
        {
            defDestroy += 10;
        }

        CheckFever();
        CheckDefTicket();
        CheckPortion();

        UpgradeInitialize();

        //questManager.Initialize();

        cameraController.GoToB();
    }

    IEnumerator DelayCoroution()
    {
        yield return waitForSeconds;

        if (playerDataBase.Exp > nowExp)
        {
            nowExp = playerDataBase.Exp;

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);

            levelManager.Initialize();
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

        yield return waitForSeconds;

        if (GameStateManager.instance.RibsLevel + 1 > playerDataBase.RankLevel1)
        {
            playerDataBase.RankLevel1 = GameStateManager.instance.RibsLevel + 1;

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel1", playerDataBase.RankLevel1);
        }

        if (GameStateManager.instance.ChocolateLevel + 1 > playerDataBase.RankLevel2)
        {
            playerDataBase.RankLevel2 = GameStateManager.instance.ChocolateLevel + 1;

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankLevel2", playerDataBase.RankLevel2);
        }

        if(playerDataBase.RankLevel1 + playerDataBase.RankLevel2 > playerDataBase.TotalLevel)
        {
            playerDataBase.TotalLevel = playerDataBase.RankLevel1 + playerDataBase.RankLevel2;

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel", playerDataBase.TotalLevel);
        }

        StartCoroutine(DelayCoroution());
    }

    public void GameStop()
    {
        if (!isDelay_Camera) return;

        GameStateManager.instance.FeverCount = feverCount;

        isDelay_Camera = false;

        mainUI.SetActive(true);
        inGameUI.SetActive(false);
        languageUI.SetActive(true);

        cameraController.GoToA();

        bestRankLevelText.localizationName = "Best";
        bestRankLevelText.plusText = " : Lv." + playerDataBase.TotalLevel;
        bestRankLevelText.ReLoad();

        CheckLocked();

        questManager.CheckingAlarm();

        //StopAllCoroutines();
    }

    public void Initialize()
    {
        signText.text = GameStateManager.instance.NickName;

        if (playerDataBase.FirstReward == 0)
        {
            playerDataBase.FirstReward = 1;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("FirstReward", 1);

            PlayfabManager.instance.UpdateAddGold(1000000);
            PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 10);

            StartCoroutine(FirstDelay());
        }
        else
        {
            RenewalVC();
        }

        SuccessLogin();
    }

    public void ChangeIsland(IslandType type)
    {
        GameStateManager.instance.IslandType = type;

        islandImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];
        islandText.localizationName = GameStateManager.instance.IslandType.ToString();
        islandText.ReLoad();

        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                ChangeFood(GameStateManager.instance.FoodType);
                break;
            case IslandType.Island2:
                ChangeCandy(GameStateManager.instance.CandyType);
                break;
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeIslandNotion);

        CheckFoodState();

        UpgradeInitialize();
    }

    public void ChangeFood(FoodType type)
    {
        GameStateManager.instance.FoodType = type;

        upgradeFood = upgradeDataBase.GetUpgradeFood(GameStateManager.instance.FoodType);

        CheckFood();
        CheckFoodState();
        UpgradeInitialize();
    }

    public void ChangeCandy(CandyType type)
    {
        GameStateManager.instance.CandyType = type;

        upgradeCandy = upgradeDataBase.GetUpgradeCandy(GameStateManager.instance.CandyType);

        CheckFood();
        CheckFoodState();
        UpgradeInitialize();
    }

    public void UpgradeInitialize()
    {
        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                need = upgradeDataBase.GetNeed(level, defaultNeed);
                sellPrice = upgradeDataBase.GetPrice(level, defaultSellPrice);
                success = upgradeDataBase.GetSuccess(level);

                maxLevel = upgradeFood.maxLevel;

                break;
            case IslandType.Island2:
                need = upgradeDataBase.GetNeed(level, defaultNeed);
                sellPrice = upgradeDataBase.GetPrice(level, defaultSellPrice);
                sellPrice = sellPrice + (int)(sellPrice * 0.2f);
                success = upgradeDataBase.GetSuccess(level);

                maxLevel = upgradeCandy.maxLevel;

                break;
        }

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
                titleText.text = LocalizationManager.instance.GetString(GameStateManager.instance.FoodType.ToString()) +
    " ( " + (level + 1) + " / " + maxLevel + " )";
                break;
            case IslandType.Island2:
                titleText.text = LocalizationManager.instance.GetString(GameStateManager.instance.CandyType.ToString()) +
    " ( " + (level + 1) + " / " + maxLevel + " )";
                break;
        }

        switch (GameStateManager.instance.GameType)
        {
            case GameType.Story:
                highLevelText.text = "";
                break;
            case GameType.Rank:
                switch (GameStateManager.instance.IslandType)
                {
                    case IslandType.Island1:
                        if(GameStateManager.instance.RibsLevel > playerDataBase.RankLevel1)
                        {
                            highLevelText.text = LocalizationManager.instance.GetString("Best") + " : " + (GameStateManager.instance.RibsLevel + 1);
                        }
                        else
                        {
                            highLevelText.text = LocalizationManager.instance.GetString("Best") + " : " + playerDataBase.RankLevel1;
                        }

                        break;
                    case IslandType.Island2:
                        if (GameStateManager.instance.ChocolateLevel > playerDataBase.RankLevel2)
                        {
                            highLevelText.text = LocalizationManager.instance.GetString("Best") + " : " + (GameStateManager.instance.ChocolateLevel + 1);
                        }
                        else
                        {
                            highLevelText.text = LocalizationManager.instance.GetString("Best") + " : " + playerDataBase.RankLevel2;
                        }
                        break;
                    case IslandType.Island3:
                        break;
                    case IslandType.Island4:
                        break;
                }
                break;
        }

        if (level >= 34)
        {
            titleText.color = Color.white;
        }
        else if (level >= 29)
        {
            titleText.color = Color.red;
        }
        else if(level >= 24)
        {
            titleText.color = Color.green;
        }
        else if (level >= 19)
        {
            titleText.color = Color.yellow;
        }
        else if (level >= 14)
        {
            titleText.color = new Color(1, 0, 1);
        }
        else if (level >= 9)
        {
            titleText.color = new Color(0, 200 / 255f, 1);
        }
        else if (level >= 4)
        {
            titleText.color = new Color(1, 200 / 255f, 0);
        }
        else
        {
            titleText.color = Color.white;
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

        successText.text = LocalizationManager.instance.GetString("SuccessPercent") + " : " + success.ToString("N1") + "%";

        if (successPlus > 0)
        {
            successText.text += " (+" + (successPlus).ToString("N1") + "%)";
        }

        needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice");

        if(needPlus > 0)
        {
            needText.text += " (-" + (needPlus.ToString("N1")) + "%)</size>\n" + MoneyUnitString.ToCurrencyString(need);
        }
        else
        {
            needText.text += "</size>\n" + MoneyUnitString.ToCurrencyString(need);
        }

        priceText.text = "<size=45>" + LocalizationManager.instance.GetString("NowPrice");


        if (sellPricePlus > 0)
        {
            priceText.text += " (+" + sellPricePlus.ToString("N1") + "%)</size>\n" + MoneyUnitString.ToCurrencyString(sellPrice);
        }
        else
        {
            priceText.text += "</size>\n" + MoneyUnitString.ToCurrencyString(sellPrice);
        }

        defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N2") + "%";

        successX2Text.text = LocalizationManager.instance.GetString("SuccessX2Percent") + " : " + successX2.ToString("N1") + "%";

        CheckDefTicket();

        CheckBankruptcy();

        notUpgrade.SetActive(false);

        if (level + 1 >= maxLevel)
        {
            notUpgrade.SetActive(true);
            defTicketObj.SetActive(false);
            successText.text = LocalizationManager.instance.GetString("MaxLevel");
            needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice") + "</size>\n-";
        }
    }

    void CheckFoodState()
    {
        for (int i = 0; i < hamburgerArray.Length; i++)
        {
            hamburgerArray[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < sandwichArray.Length; i++)
        {
            sandwichArray[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < snackLabArray.Length; i++)
        {
            snackLabArray[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < drinkArray.Length; i++)
        {
            drinkArray[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < pizzaArray.Length; i++)
        {
            pizzaArray[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < donutArray.Length; i++)
        {
            donutArray[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < friesArray.Length; i++)
        {
            friesArray[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < ribsArray.Length; i++)
        {
            ribsArray[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy1Array.Length; i++)
        {
            candy1Array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy2Array.Length; i++)
        {
            candy2Array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy3Array.Length; i++)
        {
            candy3Array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy4Array.Length; i++)
        {
            candy4Array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy5Array.Length; i++)
        {
            candy5Array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy6Array.Length; i++)
        {
            candy6Array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy7Array.Length; i++)
        {
            candy7Array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy8Array.Length; i++)
        {
            candy8Array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy9Array.Length; i++)
        {
            candy9Array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < candy10Array.Length; i++)
        {
            candy10Array[i].gameObject.SetActive(false);
        }

        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Hamburger:
                        if (hamburgerArray.Length - 1 < nextLevel)
                        {
                            hamburgerArray[hamburgerArray.Length - 1].gameObject.SetActive(true);
                            hamburgerArray[hamburgerArray.Length - 1].Initialize(5);
                        }
                        else
                        {
                            hamburgerArray[nextLevel].gameObject.SetActive(true);
                            hamburgerArray[nextLevel].Initialize(GameStateManager.instance.HamburgerLevel - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (hamburgerArray.Length - 1 < nextLevel)
                            {
                                hamburgerArray[hamburgerArray.Length - 1].FeverOn();
                            }
                            else
                            {
                                hamburgerArray[nextLevel].FeverOn();
                            }
                        }
                        break;
                    case FoodType.Sandwich:
                        if (sandwichArray.Length - 1 < nextLevel)
                        {
                            sandwichArray[sandwichArray.Length - 1].gameObject.SetActive(true);
                            sandwichArray[sandwichArray.Length - 1].Initialize(5);
                        }
                        else
                        {
                            sandwichArray[nextLevel].gameObject.SetActive(true);
                            sandwichArray[nextLevel].Initialize(GameStateManager.instance.SandwichLevel - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (sandwichArray.Length - 1 < nextLevel)
                            {
                                sandwichArray[sandwichArray.Length - 1].FeverOn();
                            }
                            else
                            {
                                sandwichArray[nextLevel].FeverOn();
                            }
                        }
                        break;
                    case FoodType.SnackLab:
                        if (snackLabArray.Length - 1 < nextLevel)
                        {
                            snackLabArray[snackLabArray.Length - 1].gameObject.SetActive(true);
                            snackLabArray[snackLabArray.Length - 1].Initialize(5);
                        }
                        else
                        {
                            snackLabArray[nextLevel].gameObject.SetActive(true);
                            snackLabArray[nextLevel].Initialize(GameStateManager.instance.SnackLabLevel - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (snackLabArray.Length - 1 < nextLevel)
                            {
                                snackLabArray[snackLabArray.Length - 1].FeverOn();
                            }
                            else
                            {
                                snackLabArray[nextLevel].FeverOn();
                            }
                        }
                        break;
                    case FoodType.Drink:
                        if (drinkArray.Length - 1 < nextLevel)
                        {
                            drinkArray[drinkArray.Length - 1].gameObject.SetActive(true);
                            drinkArray[drinkArray.Length - 1].Initialize(5);
                        }
                        else
                        {
                            drinkArray[nextLevel].gameObject.SetActive(true);
                            drinkArray[nextLevel].Initialize(GameStateManager.instance.DrinkLevel - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (drinkArray.Length - 1 < nextLevel)
                            {
                                drinkArray[drinkArray.Length - 1].FeverOn();
                            }
                            else
                            {
                                drinkArray[nextLevel].FeverOn();
                            }
                        }
                        break;
                    case FoodType.Pizza:
                        if (pizzaArray.Length - 1 < nextLevel)
                        {
                            pizzaArray[pizzaArray.Length - 1].gameObject.SetActive(true);
                            pizzaArray[pizzaArray.Length - 1].Initialize(5);
                        }
                        else
                        {
                            pizzaArray[nextLevel].gameObject.SetActive(true);
                            pizzaArray[nextLevel].Initialize(GameStateManager.instance.PizzaLevel - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (pizzaArray.Length - 1 < nextLevel)
                            {
                                pizzaArray[pizzaArray.Length - 1].FeverOn();
                            }
                            else
                            {
                                pizzaArray[nextLevel].FeverOn();
                            }
                        }

                        break;
                    case FoodType.Donut:
                        if (donutArray.Length - 1 < nextLevel)
                        {
                            donutArray[donutArray.Length - 1].gameObject.SetActive(true);
                            donutArray[donutArray.Length - 1].Initialize(5);
                        }
                        else
                        {
                            donutArray[nextLevel].gameObject.SetActive(true);
                            donutArray[nextLevel].Initialize(GameStateManager.instance.DonutLevel - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (donutArray.Length - 1 < nextLevel)
                            {
                                donutArray[donutArray.Length - 1].FeverOn();
                            }
                            else
                            {
                                donutArray[nextLevel].FeverOn();
                            }
                        }
                        break;
                    case FoodType.Fries:
                        if (friesArray.Length - 1 < nextLevel)
                        {
                            friesArray[friesArray.Length - 1].gameObject.SetActive(true);
                            friesArray[friesArray.Length - 1].Initialize(5);
                        }
                        else
                        {
                            friesArray[nextLevel].gameObject.SetActive(true);
                            friesArray[nextLevel].Initialize(GameStateManager.instance.FriesLevel - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (friesArray.Length - 1 < nextLevel)
                            {
                                friesArray[friesArray.Length - 1].FeverOn();
                            }
                            else
                            {
                                friesArray[0].FeverOn();
                            }
                        }
                        break;
                    case FoodType.Ribs:
                        if (ribsArray.Length - 1 < nextLevel)
                        {
                            ribsArray[ribsArray.Length - 1].gameObject.SetActive(true);
                            ribsArray[ribsArray.Length - 1].Initialize(5);
                        }
                        else
                        {
                            ribsArray[nextLevel].gameObject.SetActive(true);
                            ribsArray[nextLevel].Initialize(GameStateManager.instance.RibsLevel - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (ribsArray.Length - 1 < nextLevel)
                            {
                                ribsArray[ribsArray.Length - 1].FeverOn();
                            }
                            else
                            {
                                ribsArray[0].FeverOn();
                            }
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
                        }
                        else
                        {
                            candy1Array[nextLevel].gameObject.SetActive(true);
                            candy1Array[nextLevel].Initialize(GameStateManager.instance.Candy1Level - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy1Array.Length - 1 < nextLevel)
                            {
                                candy1Array[candy1Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy1Array[0].FeverOn();
                            }
                        }
                        break;
                    case CandyType.Candy2:
                        if (candy2Array.Length - 1 < nextLevel)
                        {
                            candy2Array[candy2Array.Length - 1].gameObject.SetActive(true);
                            candy2Array[candy2Array.Length - 1].Initialize(5);
                        }
                        else
                        {
                            candy2Array[nextLevel].gameObject.SetActive(true);
                            candy2Array[nextLevel].Initialize(GameStateManager.instance.Candy2Level - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy2Array.Length - 1 < nextLevel)
                            {
                                candy2Array[candy2Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy2Array[0].FeverOn();
                            }
                        }
                        break;
                    case CandyType.Candy3:
                        if (candy3Array.Length - 1 < nextLevel)
                        {
                            candy3Array[candy3Array.Length - 1].gameObject.SetActive(true);
                            candy3Array[candy3Array.Length - 1].Initialize(5);
                        }
                        else
                        {
                            candy3Array[nextLevel].gameObject.SetActive(true);
                            candy3Array[nextLevel].Initialize(GameStateManager.instance.Candy3Level - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy3Array.Length - 1 < nextLevel)
                            {
                                candy3Array[candy3Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy3Array[0].FeverOn();
                            }
                        }
                        break;
                    case CandyType.Candy4:
                        if (candy4Array.Length - 1 < nextLevel)
                        {
                            candy4Array[candy4Array.Length - 1].gameObject.SetActive(true);
                            candy4Array[candy4Array.Length - 1].Initialize(5);
                        }
                        else
                        {
                            candy4Array[nextLevel].gameObject.SetActive(true);
                            candy4Array[nextLevel].Initialize(GameStateManager.instance.Candy4Level - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy4Array.Length - 1 < nextLevel)
                            {
                                candy4Array[candy4Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy4Array[0].FeverOn();
                            }
                        }
                        break;
                    case CandyType.Candy5:
                        if (candy5Array.Length - 1 < nextLevel)
                        {
                            candy5Array[candy5Array.Length - 1].gameObject.SetActive(true);
                            candy5Array[candy5Array.Length - 1].Initialize(5);
                        }
                        else
                        {
                            candy5Array[nextLevel].gameObject.SetActive(true);
                            candy5Array[nextLevel].Initialize(GameStateManager.instance.Candy5Level - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy5Array.Length - 1 < nextLevel)
                            {
                                candy5Array[candy5Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy5Array[0].FeverOn();
                            }
                        }
                        break;
                    case CandyType.Candy6:
                        if (candy6Array.Length - 1 < nextLevel)
                        {
                            candy6Array[candy6Array.Length - 1].gameObject.SetActive(true);
                            candy6Array[candy6Array.Length - 1].Initialize(5);
                        }
                        else
                        {
                            candy6Array[nextLevel].gameObject.SetActive(true);
                            candy6Array[nextLevel].Initialize(GameStateManager.instance.Candy6Level - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy6Array.Length - 1 < nextLevel)
                            {
                                candy6Array[candy6Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy6Array[0].FeverOn();
                            }
                        }
                        break;
                    case CandyType.Candy7:
                        if (candy7Array.Length - 1 < nextLevel)
                        {
                            candy7Array[candy7Array.Length - 1].gameObject.SetActive(true);
                            candy7Array[candy7Array.Length - 1].Initialize(5);
                        }
                        else
                        {
                            candy7Array[nextLevel].gameObject.SetActive(true);
                            candy7Array[nextLevel].Initialize(GameStateManager.instance.Candy7Level - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy7Array.Length - 1 < nextLevel)
                            {
                                candy7Array[candy7Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy7Array[0].FeverOn();
                            }
                        }
                        break;
                    case CandyType.Candy8:
                        if (candy8Array.Length - 1 < nextLevel)
                        {
                            candy8Array[candy8Array.Length - 1].gameObject.SetActive(true);
                            candy8Array[candy8Array.Length - 1].Initialize(5);
                        }
                        else
                        {
                            candy8Array[nextLevel].gameObject.SetActive(true);
                            candy8Array[nextLevel].Initialize(GameStateManager.instance.Candy8Level - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy8Array.Length - 1 < nextLevel)
                            {
                                candy8Array[candy8Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy8Array[0].FeverOn();
                            }
                        }
                        break;
                    case CandyType.Candy9:
                        if (candy9Array.Length - 1 < nextLevel)
                        {
                            candy9Array[candy9Array.Length - 1].gameObject.SetActive(true);
                            candy9Array[candy9Array.Length - 1].Initialize(5);
                        }
                        else
                        {
                            candy9Array[nextLevel].gameObject.SetActive(true);
                            candy9Array[nextLevel].Initialize(GameStateManager.instance.Candy9Level - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy9Array.Length - 1 < nextLevel)
                            {
                                candy9Array[candy9Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy9Array[0].FeverOn();
                            }
                        }
                        break;
                    case CandyType.Chocolate:
                        if (candy10Array.Length - 1 < nextLevel)
                        {
                            candy10Array[candy10Array.Length - 1].gameObject.SetActive(true);
                            candy10Array[candy10Array.Length - 1].Initialize(5);
                        }
                        else
                        {
                            candy10Array[nextLevel].gameObject.SetActive(true);
                            candy10Array[nextLevel].Initialize(GameStateManager.instance.ChocolateLevel - (5 * nextLevel));
                        }

                        if (feverMode)
                        {
                            if (candy10Array.Length - 1 < nextLevel)
                            {
                                candy10Array[candy10Array.Length - 1].FeverOn();
                            }
                            else
                            {
                                candy10Array[0].FeverOn();
                            }
                        }
                        break;
                }
                break;
        }
    }

    void CheckFoodLevelUp()
    {
        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Hamburger:
                        if (hamburgerArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            hamburgerArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Sandwich:
                        if (sandwichArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            sandwichArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.SnackLab:
                        if (snackLabArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            snackLabArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Drink:
                        if (drinkArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            drinkArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Pizza:
                        if (pizzaArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            pizzaArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Donut:
                        if (donutArray.Length - 1 < nextLevel)
                        {

                        }
                        else
                        {
                            donutArray[nextLevel].LevelUp();
                        }
                        break;
                    case FoodType.Fries:
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
        }
    }

    public void UpgradeButton()
    {
        if (isDelay) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (level + 1 >= maxLevel)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.MaxLevel);
            return;
        }

        if (playerDataBase.Coin < need)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowCoin);
            return;
        }

        PlayfabManager.instance.UpdateSubtractGold(need);

        myMoneyPlusText.gameObject.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(true);
        myMoneyPlusText.color = Color.red;
        myMoneyPlusText.text = "-" + MoneyUnitString.ToCurrencyString(need);

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

                    NotionManager.instance.UseNotion(NotionType.SuccessUpgradeX2);
                }
                else
                {
                    level += 1;
                    NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);
                }
            }
            else
            {
                level += 1;
                NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);
            }

            playerDataBase.UpgradeCount += 1;
            playerDataBase.Exp += 10 + expUp;

            //questManager.CheckGoal();

            switch (GameStateManager.instance.IslandType)
            {
                case IslandType.Island1:
                    switch (GameStateManager.instance.FoodType)
                    {
                        case FoodType.Hamburger:
                            GameStateManager.instance.HamburgerLevel = level;
                            break;
                        case FoodType.Sandwich:
                            GameStateManager.instance.SandwichLevel = level;
                            break;
                        case FoodType.SnackLab:
                            GameStateManager.instance.SnackLabLevel = level;
                            break;
                        case FoodType.Drink:
                            GameStateManager.instance.DrinkLevel = level;
                            break;
                        case FoodType.Pizza:
                            GameStateManager.instance.PizzaLevel = level;
                            break;
                        case FoodType.Donut:
                            GameStateManager.instance.DonutLevel = level;
                            break;
                        case FoodType.Fries:
                            GameStateManager.instance.FriesLevel = level;
                            break;
                        case FoodType.Ribs:
                            GameStateManager.instance.RibsLevel = level;
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
                            GameStateManager.instance.ChocolateLevel = level;
                            break;
                    }
                    break;
            }

            CheckFoodLevelUp();
            UpgradeInitialize();

            if (level + 1 >= maxLevel)
            {
                SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);
                NotionManager.instance.UseNotion(NotionType.MaxLevel);

                MaxLevelUpgradeSuccess();
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
                        levelUpParticle.gameObject.SetActive(false);
                        levelUpParticle.gameObject.SetActive(true);
                        levelUpParticle.Play();
                    }
                }
                else
                {
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
                                    case FoodType.Hamburger:
                                        GameStateManager.instance.HamburgerLevel -= 1;
                                        break;
                                    case FoodType.Sandwich:
                                        GameStateManager.instance.SandwichLevel -= 1;
                                        break;
                                    case FoodType.SnackLab:
                                        GameStateManager.instance.SnackLabLevel -= 1;
                                        break;
                                    case FoodType.Drink:
                                        GameStateManager.instance.DrinkLevel -= 1;
                                        break;
                                    case FoodType.Pizza:
                                        GameStateManager.instance.PizzaLevel -= 1;
                                        break;
                                    case FoodType.Donut:
                                        GameStateManager.instance.DonutLevel -= 1;
                                        break;
                                    case FoodType.Fries:
                                        GameStateManager.instance.FriesLevel -= 1;
                                        break;
                                    case FoodType.Ribs:
                                        GameStateManager.instance.RibsLevel -= 1;
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
                                        GameStateManager.instance.ChocolateLevel -= 1;
                                        break;
                                }
                                break;
                        }

                        CheckFood();
                        CheckFoodState();
                        UpgradeInitialize();

                        SoundManager.instance.PlaySFX(GameSfxType.Shield);
                        NotionManager.instance.UseNotion(NotionType.DefDestroyNotion);

                        return;
                    }
                }
            }

            if (GameStateManager.instance.Effect)
            {
                bombPartice.gameObject.SetActive(false);
                bombPartice.gameObject.SetActive(true);
                bombPartice.Play();
            }

            DestoryFood();

            CheckFoodState();
            UpgradeInitialize();

            SoundManager.instance.PlaySFX(GameSfxType.UpgradeFail);
            NotionManager.instance.UseNotion(NotionType.FailUpgrade);
        }

        if (feverFillamount.gameObject.activeInHierarchy)
        {
            if (!feverMode && level + 1 < maxLevel)
            {
                feverCount += 1;

                CheckFever();
            }
        }

        isDelay = true;
        Invoke("WaitDelay", 0.4f);
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
                    case FoodType.Hamburger:
                        GameStateManager.instance.HamburgerLevel = 0;
                        break;
                    case FoodType.Sandwich:
                        GameStateManager.instance.SandwichLevel = 0;
                        break;
                    case FoodType.SnackLab:
                        GameStateManager.instance.SnackLabLevel = 0;
                        break;
                    case FoodType.Drink:
                        GameStateManager.instance.DrinkLevel = 0;
                        break;
                    case FoodType.Pizza:
                        GameStateManager.instance.PizzaLevel = 0;
                        break;
                    case FoodType.Donut:
                        GameStateManager.instance.DonutLevel = 0;
                        break;
                    case FoodType.Fries:
                        GameStateManager.instance.FriesLevel = 0;
                        break;
                    case FoodType.Ribs:
                        GameStateManager.instance.RibsLevel = 0;
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
                        GameStateManager.instance.RibsLevel = 0;
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

            GameStateManager.instance.Pause = false;

            feverEffect.SetActive(true);
            backButton.SetActive(false);

            successText.color = Color.red;

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
        characterAnimator[(int)GameStateManager.instance.CharacterType].SetBool("YummyTime", true);
        animalAnimator[(int)GameStateManager.instance.AnimalType].SetBool("YummyTime", true);
        butterflyAnimator[(int)GameStateManager.instance.ButterflyType].SetBool("YummyTime", true);

        for (int i = 0; i < mainTruckArray.Length; i++)
        {
            if (mainTruckArray[i].gameObject.activeInHierarchy)
            {
                mainTruckArray[i].enabled = true;
            }
        }

        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                for (int i = 0; i < hamburgerArray.Length; i++)
                {
                    if (hamburgerArray[i].gameObject.activeInHierarchy)
                    {
                        hamburgerArray[i].FeverOn();
                    }
                }

                for (int i = 0; i < sandwichArray.Length; i++)
                {
                    if (sandwichArray[i].gameObject.activeInHierarchy)
                    {
                        sandwichArray[i].FeverOn();
                    }
                }

                for (int i = 0; i < snackLabArray.Length; i++)
                {
                    if (snackLabArray[i].gameObject.activeInHierarchy)
                    {
                        snackLabArray[i].FeverOn();
                    }
                }

                for (int i = 0; i < drinkArray.Length; i++)
                {
                    if (drinkArray[i].gameObject.activeInHierarchy)
                    {
                        drinkArray[i].FeverOn();
                    }
                }

                for (int i = 0; i < pizzaArray.Length; i++)
                {
                    if (pizzaArray[i].gameObject.activeInHierarchy)
                    {
                        pizzaArray[i].FeverOn();
                    }
                }

                for (int i = 0; i < donutArray.Length; i++)
                {
                    if (donutArray[i].gameObject.activeInHierarchy)
                    {
                        donutArray[i].FeverOn();
                    }
                }

                for (int i = 0; i < friesArray.Length; i++)
                {
                    if (friesArray[i].gameObject.activeInHierarchy)
                    {
                        friesArray[i].FeverOn();
                    }
                }

                for (int i = 0; i < ribsArray.Length; i++)
                {
                    if (ribsArray[i].gameObject.activeInHierarchy)
                    {
                        ribsArray[i].FeverOn();
                    }
                }

                break;
            case IslandType.Island2:
                for(int i = 0; i < candy1Array.Length; i ++)
                {
                    if (candy1Array[i].gameObject.activeInHierarchy)
                    {
                        candy1Array[i].FeverOn();
                    }
                }

                for (int i = 0; i < candy2Array.Length; i++)
                {
                    if (candy2Array[i].gameObject.activeInHierarchy)
                    {
                        candy2Array[i].FeverOn();
                    }
                }

                for (int i = 0; i < candy3Array.Length; i++)
                {
                    if (candy3Array[i].gameObject.activeInHierarchy)
                    {
                        candy3Array[i].FeverOn();
                    }
                }

                for (int i = 0; i < candy4Array.Length; i++)
                {
                    if (candy4Array[i].gameObject.activeInHierarchy)
                    {
                        candy4Array[i].FeverOn();
                    }
                }

                for (int i = 0; i < candy5Array.Length; i++)
                {
                    if (candy5Array[i].gameObject.activeInHierarchy)
                    {
                        candy5Array[i].FeverOn();
                    }
                }

                for (int i = 0; i < candy6Array.Length; i++)
                {
                    if (candy6Array[i].gameObject.activeInHierarchy)
                    {
                        candy6Array[i].FeverOn();
                    }
                }

                for (int i = 0; i < candy7Array.Length; i++)
                {
                    if (candy7Array[i].gameObject.activeInHierarchy)
                    {
                        candy7Array[i].FeverOn();
                    }
                }

                for (int i = 0; i < candy8Array.Length; i++)
                {
                    if (candy8Array[i].gameObject.activeInHierarchy)
                    {
                        candy8Array[i].FeverOn();
                    }
                }

                for (int i = 0; i < candy9Array.Length; i++)
                {
                    if (candy9Array[i].gameObject.activeInHierarchy)
                    {
                        candy9Array[i].FeverOn();
                    }
                }

                for (int i = 0; i < candy10Array.Length; i++)
                {
                    if (candy10Array[i].gameObject.activeInHierarchy)
                    {
                        candy10Array[i].FeverOn();
                    }
                }
                break;
        }

        if (GameStateManager.instance.Effect)
        {
            lightParticle.gameObject.SetActive(true);
        }

        feverText.enabled = false;

        feverCount = 0;
        GameStateManager.instance.FeverCount = 0;

        playerDataBase.FeverModeCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("FeverModeCount", playerDataBase.FeverModeCount);

        //questManager.CheckGoal();

        float currentTime = 0f;
        float fillAmount = 0;

        while (currentTime < feverTime)
        {
            if (!GameStateManager.instance.Pause)
            {
                fillAmount = Mathf.Lerp(1.0f, 0, currentTime / feverTime);

                fillAmount = Mathf.Clamp01(fillAmount);

                feverFillamount.fillAmount = fillAmount;

                currentTime += Time.deltaTime;
            }

            yield return null;
        }

        lightParticle.gameObject.SetActive(false);

        feverMode = false;
        feverFillamount.fillAmount = 0;

        defDestroy -= 5;

        successPlus -= feverPlus;

        feverText.enabled = true;
        feverText.text = LocalizationManager.instance.GetString("FeverGauge") + "  0%";

        successText.color = Color.green;

        UpgradeInitialize();

        feverEffect.SetActive(false);
        backButton.SetActive(true);

        SoundManager.instance.StopFever();

        characterAnimator[(int)GameStateManager.instance.CharacterType].SetBool("YummyTime", false);
        animalAnimator[(int)GameStateManager.instance.AnimalType].SetBool("YummyTime", false);
        butterflyAnimator[(int)GameStateManager.instance.ButterflyType].SetBool("YummyTime", false);

        for (int i = 0; i < mainTruckArray.Length; i++)
        {
            if (mainTruckArray[i].gameObject.activeInHierarchy)
            {
                mainTruckArray[i].enabled = false;
            }
        }

        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                for (int i = 0; i < hamburgerArray.Length; i++)
                {
                    if (hamburgerArray[i].gameObject.activeInHierarchy)
                    {
                        hamburgerArray[i].FeverOff();
                    }
                }

                for (int i = 0; i < sandwichArray.Length; i++)
                {
                    if (sandwichArray[i].gameObject.activeInHierarchy)
                    {
                        sandwichArray[i].FeverOff();
                    }
                }

                for (int i = 0; i < snackLabArray.Length; i++)
                {
                    if (snackLabArray[i].gameObject.activeInHierarchy)
                    {
                        snackLabArray[i].FeverOff();
                    }
                }

                for (int i = 0; i < drinkArray.Length; i++)
                {
                    if (drinkArray[i].gameObject.activeInHierarchy)
                    {
                        drinkArray[i].FeverOff();
                    }
                }

                for (int i = 0; i < pizzaArray.Length; i++)
                {
                    if (pizzaArray[i].gameObject.activeInHierarchy)
                    {
                        pizzaArray[i].FeverOff();
                    }
                }

                for (int i = 0; i < donutArray.Length; i++)
                {
                    if (donutArray[i].gameObject.activeInHierarchy)
                    {
                        donutArray[i].FeverOff();
                    }
                }

                for (int i = 0; i < friesArray.Length; i++)
                {
                    if (friesArray[i].gameObject.activeInHierarchy)
                    {
                        friesArray[i].FeverOff();
                    }
                }

                for (int i = 0; i < ribsArray.Length; i++)
                {
                    if (ribsArray[i].gameObject.activeInHierarchy)
                    {
                        ribsArray[i].FeverOff();
                    }
                }

                break;
            case IslandType.Island2:
                for (int i = 0; i < candy1Array.Length; i++)
                {
                    if (candy1Array[i].gameObject.activeInHierarchy)
                    {
                        candy1Array[i].FeverOff();
                    }
                }

                for (int i = 0; i < candy2Array.Length; i++)
                {
                    if (candy2Array[i].gameObject.activeInHierarchy)
                    {
                        candy2Array[i].FeverOff();
                    }
                }

                for (int i = 0; i < candy3Array.Length; i++)
                {
                    if (candy3Array[i].gameObject.activeInHierarchy)
                    {
                        candy3Array[i].FeverOff();
                    }
                }

                for (int i = 0; i < candy4Array.Length; i++)
                {
                    if (candy4Array[i].gameObject.activeInHierarchy)
                    {
                        candy4Array[i].FeverOff();
                    }
                }

                for (int i = 0; i < candy5Array.Length; i++)
                {
                    if (candy5Array[i].gameObject.activeInHierarchy)
                    {
                        candy5Array[i].FeverOff();
                    }
                }

                for (int i = 0; i < candy6Array.Length; i++)
                {
                    if (candy6Array[i].gameObject.activeInHierarchy)
                    {
                        candy6Array[i].FeverOff();
                    }
                }

                for (int i = 0; i < candy7Array.Length; i++)
                {
                    if (candy7Array[i].gameObject.activeInHierarchy)
                    {
                        candy7Array[i].FeverOff();
                    }
                }

                for (int i = 0; i < candy8Array.Length; i++)
                {
                    if (candy8Array[i].gameObject.activeInHierarchy)
                    {
                        candy8Array[i].FeverOff();
                    }
                }

                for (int i = 0; i < candy9Array.Length; i++)
                {
                    if (candy9Array[i].gameObject.activeInHierarchy)
                    {
                        candy9Array[i].FeverOff();
                    }
                }

                for (int i = 0; i < candy10Array.Length; i++)
                {
                    if (candy10Array[i].gameObject.activeInHierarchy)
                    {
                        candy10Array[i].FeverOff();
                    }
                }
                break;
        }
    }

    void MaxLevelUpgradeSuccess()
    {
        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Hamburger:
                        playerDataBase.HamburgerMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("HamburgerMaxValue", playerDataBase.HamburgerMaxValue);

                        if (playerDataBase.NextFoodNumber == 0)
                        {
                            playerDataBase.NextFoodNumber = 1;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);

                            if(playerDataBase.ReincarnationCount == 0)
                            {
                                tutorialManager.NextFood();
                            }
                        }

                        lockManager.UnLocked(1);

                        break;
                    case FoodType.Sandwich:
                        playerDataBase.SandwichMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SandwichMaxValue", playerDataBase.SandwichMaxValue);

                        if (playerDataBase.NextFoodNumber == 1)
                        {
                            playerDataBase.NextFoodNumber = 2;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                        }

                        lockManager.UnLocked(2);

                        break;
                    case FoodType.SnackLab:
                        playerDataBase.SnackLabMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SnackLabMaxValue", playerDataBase.SnackLabMaxValue);

                        if (playerDataBase.NextFoodNumber == 2)
                        {
                            playerDataBase.NextFoodNumber = 3;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                        }

                        lockManager.UnLocked(3);

                        break;
                    case FoodType.Drink:
                        playerDataBase.DrinkMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DrinkMaxValue", playerDataBase.DrinkMaxValue);

                        if (playerDataBase.NextFoodNumber == 3)
                        {
                            playerDataBase.NextFoodNumber = 4;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                        }

                        lockManager.UnLocked(4);

                        break;
                    case FoodType.Pizza:
                        playerDataBase.PizzaMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("PizzaMaxValue", playerDataBase.PizzaMaxValue);

                        if (playerDataBase.NextFoodNumber == 4)
                        {
                            playerDataBase.NextFoodNumber = 5;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                        }

                        lockManager.UnLocked(5);

                        break;
                    case FoodType.Donut:
                        playerDataBase.DonutMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DonutMaxValue", playerDataBase.DonutMaxValue);

                        if (playerDataBase.NextFoodNumber == 5)
                        {
                            playerDataBase.NextFoodNumber = 6;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                            changeFoodAlarmObj.SetActive(true);
                        }

                        lockManager.UnLocked(6);

                        break;
                    case FoodType.Fries:
                        playerDataBase.FriesMaxValue += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("FriesMaxValue", playerDataBase.FriesMaxValue);

                        if (playerDataBase.IslandNumber <= 0)
                        {
                            playerDataBase.IslandNumber = 1;
                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);
                        }

                        lockManager.UnLocked(7);

                        break;
                    case FoodType.Ribs:

                        break;
                }

                Debug.Log(GameStateManager.instance.FoodType + " : Max Level!");

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
        }

        //questManager.CheckGoal();
        changeFoodManager.CheckProficiency();
        UpgradeInitialize();
    }

    public void SellButton()
    {
        if (level == 0) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if(level >= 9)
        {
            playerDataBase.SellCount += 1;

            //questManager.CheckGoal();
        }

        level = 0;
        nextLevel = 0;

        switch (GameStateManager.instance.IslandType)
        {
            case IslandType.Island1:
                switch (GameStateManager.instance.FoodType)
                {
                    case FoodType.Hamburger:
                        GameStateManager.instance.HamburgerLevel = 0;
                        break;
                    case FoodType.Sandwich:
                        GameStateManager.instance.SandwichLevel = 0;
                        break;
                    case FoodType.SnackLab:
                        GameStateManager.instance.SnackLabLevel = 0;
                        break;
                    case FoodType.Drink:
                        GameStateManager.instance.DrinkLevel = 0;
                        break;
                    case FoodType.Pizza:
                        GameStateManager.instance.PizzaLevel = 0;
                        break;
                    case FoodType.Donut:
                        GameStateManager.instance.DonutLevel = 0;
                        break;
                    case FoodType.Fries:
                        GameStateManager.instance.FriesLevel = 0;
                        break;
                    case FoodType.Ribs:
                        GameStateManager.instance.RibsLevel = 0;
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
                        GameStateManager.instance.ChocolateLevel = 0;
                        break;
                }
                break;
        }

        CheckFoodState();

        if(sellPriceTip > 0)
        {
            if(sellPriceTip >= Random.Range(0f, 100f))
            {
                sellPrice = sellPrice + (int)(sellPrice * 0.15f);

                NotionManager.instance.UseNotion(NotionType.SuccessSellX2);
            }
            else
            {
                NotionManager.instance.UseNotion(NotionType.SuccessSell);
            }
        }
        else
        {
            NotionManager.instance.UseNotion(NotionType.SuccessSell);
        }

        PlayfabManager.instance.UpdateAddGold(sellPrice);

        myMoneyPlusText.gameObject.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(true);
        myMoneyPlusText.color = Color.green;
        myMoneyPlusText.text = "+" + MoneyUnitString.ToCurrencyString(sellPrice);

        UpgradeInitialize();

        SoundManager.instance.PlaySFX(GameSfxType.Sell);
    }

    public void CheckDefTicket()
    {
        if(level >= 1 && level + 1 < maxLevel)
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

        defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N2") + "%";
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
        }

        UpgradeInitialize();
    }

    public void CheckPortion()
    {
        if(playerDataBase.Portion1 == 0)
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

        //questManager.CheckGoal();
    }

    public void UseSources(int number)
    {
        switch(number)
        {
            case 0:
                if(!portion1)
                {
                    if(playerDataBase.Portion1 > 0)
                    {
                        portion1 = true;

                        needPlus += 30;
                        UpgradeInitialize();

                        playerDataBase.Portion1 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

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

                        sellPricePlus += 10;
                        UpgradeInitialize();

                        playerDataBase.Portion2 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

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

                        successPlus += 1;
                        UpgradeInitialize();

                        playerDataBase.Portion3 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

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
                        feverCount += (feverMaxCount * (0.25f + portion4Plus));
                        CheckFever();

                        playerDataBase.Portion4 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

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

                        defDestroy += 5;
                        UpgradeInitialize();

                        playerDataBase.Portion5 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

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

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

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
            if(!GameStateManager.instance.Pause)
            {
                float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion1Time);

                fillAmount = Mathf.Clamp01(fillAmount);

                portionFillamount1.fillAmount = fillAmount;

                currentTime += Time.deltaTime;
            }    

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
            if (!GameStateManager.instance.Pause)
            {
                float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion2Time);

                fillAmount = Mathf.Clamp01(fillAmount);

                portionFillamount2.fillAmount = fillAmount;

                currentTime += Time.deltaTime;
            }

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
            if (!GameStateManager.instance.Pause)
            {
                float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion3Time);

                fillAmount = Mathf.Clamp01(fillAmount);

                portionFillamount3.fillAmount = fillAmount;

                currentTime += Time.deltaTime;
            }

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
            if (!GameStateManager.instance.Pause)
            {
                float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion5Time);

                fillAmount = Mathf.Clamp01(fillAmount);

                portionFillamount5.fillAmount = fillAmount;

                currentTime += Time.deltaTime;
            }

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
            if (!GameStateManager.instance.Pause)
            {
                float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portion6Time);

                fillAmount = Mathf.Clamp01(fillAmount);

                portionFillamount6.fillAmount = fillAmount;

                currentTime += Time.deltaTime;
            }

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
        if(number == 0)
        {
            buff1 = true;

            sellPricePlus += 30;
            UpgradeInitialize();
        }
        else
        {
            buff2 = true;

            defDestroy += 10;
            defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N2") + "%";
        }
    }

    public void OffBuff(int number)
    {
        if (number == 0)
        {
            buff1 = false;

            sellPricePlus -= 30;
            UpgradeInitialize();
        }
        else
        {
            buff2 = false;

            defDestroy -= 10;
            defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N2") + "%";
        }
    }


    public void Reincarnation()
    {
        islandImg.sprite = islandArray[(int)GameStateManager.instance.IslandType];
        islandText.localizationName = GameStateManager.instance.IslandType.ToString();
        islandText.ReLoad();

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

        FirebaseAnalytics.LogEvent("OpenAppReview");

        CloseAppReview();
    }

    public void FeedBack()
    {
        FirebaseAnalytics.LogEvent("FeedBack");

        Application.OpenURL("https://forms.gle/RtZM83MWko6aJR5c6");
    }

    public void OpenPrivacypolicyURL()
    {
        Application.OpenURL("https://sites.google.com/view/whilili-privacypolicy");
    }

    public void OpenTermsURL()
    {
        Application.OpenURL("https://sites.google.com/view/whilili-terms");
    }

    public void Agree()
    {
        GameStateManager.instance.Privacypolicy = true;

        privacypolicyView.SetActive(false);
    }

    public void Decline()
    {
        Application.Quit();
    }

    void WaitDelay()
    {
        isDelay = false;
    }

    void CheckBankruptcy()
    {
        if(GameStateManager.instance.HamburgerLevel <= 1 && GameStateManager.instance.SandwichLevel <= 1 && GameStateManager.instance.SnackLabLevel <= 1
            && GameStateManager.instance.DrinkLevel <= 1 && GameStateManager.instance.PizzaLevel <= 1 && GameStateManager.instance.DonutLevel <= 1
            && GameStateManager.instance.FriesLevel <= 1 && GameStateManager.instance.Candy1Level <= 1 && GameStateManager.instance.Candy2Level <= 1
            && GameStateManager.instance.Candy3Level <= 1 && GameStateManager.instance.Candy4Level <= 1 && GameStateManager.instance.Candy5Level <= 1
            && GameStateManager.instance.Candy6Level <= 1 && GameStateManager.instance.Candy7Level <= 1 && GameStateManager.instance.Candy8Level <= 1
            && GameStateManager.instance.Candy9Level <= 1
            && playerDataBase.Coin < 10000)
        {
            bankruptcyView.SetActive(true);

            if(GameStateManager.instance.Bankruptcy < 1)
            {
                bankruptcyText.text = MoneyUnitString.ToCurrencyString(1000000);
            }
            else
            {
                bankruptcyText.text = MoneyUnitString.ToCurrencyString(100000);
            }
        }
    }

    public void ReceiveBankruptcy()
    {
        bankruptcyView.SetActive(false);

        if (GameStateManager.instance.Bankruptcy < 1)
        {
            PlayfabManager.instance.UpdateAddGold(1000000);
        }
        else
        {
            PlayfabManager.instance.UpdateAddGold(100000);
        }

        SoundManager.instance.PlaySFX(GameSfxType.GetMoney);
        GameStateManager.instance.Bankruptcy += 1;
    }

    public void OpenDeveloperMode()
    {
        if(!testMode.activeInHierarchy)
        {
            testMode.SetActive(true);
            testModeText.text = "Developer Mode OFF";
        }
        else
        {
            testMode.SetActive(false);
            testModeText.text = "Developer Mode ON";
        }
    }


    public void GetIsland()
    {
        playerDataBase.IslandNumber = 4;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);
    }

    public void GetFood()
    {
        playerDataBase.NextFoodNumber = 10;
        playerDataBase.NextFoodNumber2 = 10;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);
    }

    public void GetReceipe()
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
        //playerDataBase.Skill14 = 100;

        playerDataBase.Treasure1 = 100;
        playerDataBase.Treasure2 = 100;
        playerDataBase.Treasure3 = 100;
        playerDataBase.Treasure4 = 100;
        playerDataBase.Treasure5 = 100;
        playerDataBase.Treasure6 = 100;
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
        playerDataBase.Portion1 += 100;
        playerDataBase.Portion2 += 100;
        playerDataBase.Portion3 += 100;
        playerDataBase.Portion4 += 100;
        playerDataBase.Portion5 += 100;
        playerDataBase.Portion6 += 100;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion6", playerDataBase.Portion6);
    }

    public void GetDefTicket()
    {
        playerDataBase.DefDestroyTicket += 100;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicket", playerDataBase.DefDestroyTicket);
    }

    public void GetUnLocked()
    {
        lockManager.UnLocked(7);
    }

    public void GetExp()
    {
        playerDataBase.Exp += 10000;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);

        levelManager.Initialize();

        CheckLocked();
    }

    public void PortionInfo(int number)
    {
        switch(number)
        {
            case 0:
                NotionManager.instance.UseNotion(NotionType.PortionInfo1);
                break;
            case 1:
                NotionManager.instance.UseNotion(NotionType.PortionInfo2);
                break;
            case 2:
                NotionManager.instance.UseNotion(NotionType.PortionInfo3);
                break;
            case 3:
                NotionManager.instance.UseNotion(NotionType.PortionInfo4);
                break;
            case 4:
                NotionManager.instance.UseNotion(NotionType.PortionInfo5);
                break;
            case 5:
                NotionManager.instance.UseNotion(NotionType.PortionInfo6);
                break;
        }
    }

    public void GetUpgradeCount()
    {
        playerDataBase.UpgradeCount += Random.Range(500, 1001);

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UpgradeCount", playerDataBase.UpgradeCount);
    }

    public void CheckLocked()
    {
        butterflyLocked.SetActive(true);
        rankLocked.SetActive(true);
        treasureLocked.SetActive(true);

        if (levelDataBase.GetLevel(playerDataBase.Exp) > 4)
        {
            butterflyLocked.SetActive(false);
        }


        if (levelDataBase.GetLevel(playerDataBase.Exp) > 9)
        {
            rankLocked.SetActive(false);
        }

        if (levelDataBase.GetLevel(playerDataBase.Exp) > 14)
        {
            treasureLocked.SetActive(false);
        }
    }
}
