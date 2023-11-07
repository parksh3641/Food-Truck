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

    public CameraController cameraController;

    public Text goldText;
    public Text crystalText;
    public Text signText;

    public GameObject privacypolicyView;

    public GameObject coupon;
    public GameObject deleteAccount;

    public GameObject changeFoodAlarmObj;

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

    //public Text myMoneyMinusText;
    public Text myMoneyPlusText;


    [Space]
    [Title("Portion")]
    public Text portionText1, portionText2, portionText3, portionText4, portionText5;
    public Image portionFillamount1, portionFillamount2, portionFillamount3, portionFillamount4, portionFillamount5;
    private bool portion1, portion2, portion3, portion4, portion5;

    public Text titleText;
    public Text needText;
    public Text priceText;
    public Text successText;
    public Text defDestroyText;

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
    private float feverTime = 30;
    private float feverPlus = 3;

    private float defDestroy = 0;
    private float defDestroyPlus = 100;

    public int need = 0;
    private float needPlus = 0;

    public int sellPrice = 0;
    private float sellPricePlus = 0;

    private float sellPriceX2 = 0;

    public float success = 0;
    private float successPortion = 0;
    private float successFever = 0;
    private float successPlus = 0;

    private float portion1Time = 30;
    private float portion2Time = 30;
    private float portion3Time = 30;
    private float portion5Time = 30;

    private int portionPlus = 1;

    public int level = 0;
    public int nextLevel = 0;

    private int adCount = 0;

    public bool isDelay_Camera = false;
    public bool isDelay = false;
    public bool isDef = false;

    public bool nextFood = false;

    private bool feverMode = false;

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

    public ParticleSystem lightParticle;
    public ParticleSystem levelUpParticle;
    public ParticleSystem bombPartice;

    public TutorialManager tutorialManager;
    public LockManager lockManager;
    public QuestManager questManager;

    UpgradeDataBase upgradeDataBase;
    PlayerDataBase playerDataBase;

    CharacterDataBase characterDataBase;
    TruckDataBase truckDataBase;
    AnimalDataBase animalDataBase;
    ButterflyDataBase butterflyDataBase;

    IslandDataBase islandDataBase;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

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

        //versionText.text = "v" + Application.version;

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

        portionFillamount1.fillAmount = 0;
        portionFillamount2.fillAmount = 0;
        portionFillamount3.fillAmount = 0;
        portionFillamount4.fillAmount = 0;
        portionFillamount5.fillAmount = 0;

        defTicketObj.SetActive(false);

        bankruptcyView.SetActive(false);

        privacypolicyView.SetActive(false);

        myMoneyPlusText.gameObject.SetActive(false);

        lightParticle.gameObject.SetActive(false);
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
        if(GameStateManager.instance.AutoLogin)
        {
            if (!PlayfabManager.instance.isActive)
            {
                if (NetworkConnect.instance.CheckConnectInternet())
                {
                    PlayfabManager.instance.Login();
                }
                else
                {
                    checkInternet.SetActive(true);
                }
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

        myMoneyPlusText.gameObject.SetActive(false);
    }

    public void SuccessLogin()
    {
        checkInternet.SetActive(false);
        loginView.SetActive(false);

        feverText.text = LocalizationManager.instance.GetString("FeverGauge") + "  0%";

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
                break;
            case FoodType.Fries:
                level = GameStateManager.instance.FriesLevel;
                nextLevel = (GameStateManager.instance.FriesLevel + 1) / 5;
                break;
        }

        CheckFoodState();

        isDelay_Camera = true;

        PlayfabManager.instance.GetTitleInternalData("Coupon", CheckCoupon);

        if(!GameStateManager.instance.Tutorial)
        {
            tutorialManager.TutorialStart();
        }
    }

    IEnumerator FirstDelay()
    {
        playerDataBase.Portion1 += 5;
        playerDataBase.Portion2 += 5;
        playerDataBase.Portion3 += 5;
        playerDataBase.Portion4 += 5;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);

        yield return new WaitForSeconds(0.5f);

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);

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
        goldText.text = MoneyUnitString.ToCurrencyString(playerDataBase.Coin);
        crystalText.text = MoneyUnitString.ToCurrencyString(playerDataBase.Crystal);
    }

    public void GameStart()
    {
        if (!isDelay_Camera) return;

        isDelay_Camera = false;

        mainUI.SetActive(false);
        inGameUI.SetActive(true);
        languageUI.SetActive(false);

        nextFood = false;

        successPlus = 0;
        needPlus = 0;
        sellPricePlus = 0;
        sellPriceX2 = 0;
        defDestroy = 0;


        if (GameStateManager.instance.CharacterType > CharacterType.Character1)
        {
            successPlus = characterDataBase.GetCharacterEffect(GameStateManager.instance.CharacterType);
        }

        if (GameStateManager.instance.TruckType > TruckType.Bread)
        {
            sellPricePlus = truckDataBase.GetTruckEffect(GameStateManager.instance.TruckType);
        }

        if (GameStateManager.instance.AnimalType > AnimalType.Colobus)
        {
            sellPriceX2 = animalDataBase.GetAnimalEffect(GameStateManager.instance.AnimalType);
        }

        if (GameStateManager.instance.ButterflyType > ButterflyType.Butterfly1)
        {
            defDestroy = butterflyDataBase.GetButterflyEffect(GameStateManager.instance.ButterflyType);
        }


        upgradeFood = upgradeDataBase.GetUpgradeFood(GameStateManager.instance.FoodType);

        feverCount = GameStateManager.instance.FeverCount;

        CheckFever();

        feverTime = 30 + (30 * (0.001f * (playerDataBase.Skill1 + 1)));
        feverMaxCount = 200 - (200 * (0.001f * (playerDataBase.Skill2 + 1)));
        feverPlus = 3 + (3 * (0.001f * (playerDataBase.Skill3 + 1)));

        portion1Time = 30 + (30 * (0.001f * (playerDataBase.Skill4 + 1)));
        portion2Time = 30 + (30 * (0.001f * (playerDataBase.Skill5 + 1)));
        portion3Time = 30 + (30 * (0.001f * (playerDataBase.Skill6 + 1)));

        //needPlus += playerDataBase.GetTruckNumber();
        //sellPricePlus += playerDataBase.GetAnimalNumber();

        if(playerDataBase.GoldX2)
        {
            sellPricePlus += 50;
        }

        UpgradeInitialize();

        CheckDefTicket();

        CheckPortion();

        questManager.Initialize();

        cameraController.GoToB();
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
    }

    public void Initialize()
    {
        signText.text = GameStateManager.instance.NickName;

        if (playerDataBase.FirstReward == 0)
        {
            playerDataBase.FirstReward = 1;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("FirstReward", 1);

            PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 1000000);

            StartCoroutine(FirstDelay());
        }
        else
        {
            RenewalVC();
        }

        SuccessLogin();
    }

    public void ChangeFood(FoodType type)
    {
        if (GameStateManager.instance.FoodType == type) return;

        nextFood = false;

        GameStateManager.instance.FoodType = type;

        upgradeFood = upgradeDataBase.GetUpgradeFood(GameStateManager.instance.FoodType);

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

                break;
            case FoodType.Fries:
                level = GameStateManager.instance.FriesLevel;
                nextLevel = (GameStateManager.instance.FriesLevel + 1) / 5;

                break;
        }

        CheckFoodState();

        UpgradeInitialize();

        Debug.Log(GameStateManager.instance.FoodType.ToString() + " Level Up");
    }

    public void UpgradeInitialize()
    {
        need = upgradeFood.GetNeed(level);
        sellPrice = upgradeFood.GetPrice(level);
        success = upgradeFood.GetSuccess(level);

        if (needPlus > 0)
        {
            need -= Mathf.CeilToInt((need * (0.01f * needPlus)));
        }

        if (sellPricePlus > 0)
        {
            sellPrice += Mathf.CeilToInt((sellPrice * (0.01f * sellPricePlus)));
        }

        if (portion3)
        {
            successPortion = portionPlus;
        }
        else
        {
            successPortion = 0;
        }

        if (feverMode)
        {
            successFever = feverPlus;
        }
        else
        {
            successFever = 0;
        }

        titleText.text = LocalizationManager.instance.GetString(GameStateManager.instance.FoodType.ToString()) + " ( +" + (level + 1) + " / " + upgradeFood.maxLevel +" )";

        if(level >= 29)
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

        success += successPortion + successFever + successPlus;

        if (success >= 100)
        {
            success = 100;
        }

        if (GameStateManager.instance.Developer) success = 100;

        successText.text = LocalizationManager.instance.GetString("SuccessPercent") + " : " + success.ToString("N1") + "%";

        if (successPortion > 0 || successFever > 0 || successPlus > 0)
        {
            successText.text += " (+" + (successPortion + successFever + successPlus).ToString("N1") + "%)";
        }

        success += successPortion + successFever + successPlus;

        needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice");

        if(needPlus > 0)
        {
            needText.text += " (-" + (needPlus) + "%)</size>\n" + MoneyUnitString.ToCurrencyString(need);
        }
        else
        {
            needText.text += "</size>\n" + MoneyUnitString.ToCurrencyString(need);
        }

        priceText.text = "<size=45>" + LocalizationManager.instance.GetString("NowPrice");


        if (sellPricePlus > 0)
        {
            priceText.text += " (+" + (sellPricePlus) + "%)</size>\n" + MoneyUnitString.ToCurrencyString(sellPrice);
        }
        else
        {
            priceText.text += "</size>\n" + MoneyUnitString.ToCurrencyString(sellPrice);
        }

        defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N1") + "%";

        CheckDefTicket();

        CheckBankruptcy();

        if (level + 1 >= upgradeFood.maxLevel)
        {
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
                    hamburgerArray[nextLevel].FeverOn();
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
                    sandwichArray[nextLevel].FeverOn();
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
                    snackLabArray[nextLevel].FeverOn();
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
                    drinkArray[nextLevel].FeverOn();
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
                    pizzaArray[nextLevel].FeverOn();
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
        }
    }

    void CheckFoodLevelUp()
    {
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

        if (level + 1 < upgradeFood.maxLevel)
        {
            if(playerDataBase.Coin >= need)
            {
                PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, need);

                myMoneyPlusText.gameObject.SetActive(false);
                myMoneyPlusText.gameObject.SetActive(true);
                myMoneyPlusText.color = Color.red;
                myMoneyPlusText.text = "-" + MoneyUnitString.ToCurrencyString(need);

                float random = Random.Range(0, 100f);

                if(random >= 100 - success)
                {
                    if (isDef)
                    {
                        UseDefTicket();
                    }

                    level += 1;

                    playerDataBase.UpgradeCount += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("UpgradeCount", playerDataBase.UpgradeCount);

                    questManager.CheckGoal();

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
                    }

                    CheckFoodLevelUp();

                    UpgradeInitialize();

                    if(level + 1 >= upgradeFood.maxLevel)
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

                        NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);
                    }
                }
                else
                {
                    if(GameStateManager.instance.Vibration)
                    {
                        Handheld.Vibrate();
                    }

                    if (GameStateManager.instance.Effect)
                    {
                        bombPartice.gameObject.SetActive(false);
                        bombPartice.gameObject.SetActive(true);
                        bombPartice.Play();
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
                                if(level >= 29)
                                {
                                    level -= 1;

                                    CheckFoodLevelUp();

                                    UpgradeInitialize();
                                }

                                NotionManager.instance.UseNotion(NotionType.DefDestroyNotion);

                                return;
                            }
                        }
                    }


                    level = 0;
                    nextLevel = 0;

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
                    }

                    CheckFoodState();
                    UpgradeInitialize();

                    SoundManager.instance.PlaySFX(GameSfxType.UpgradeFail);
                    NotionManager.instance.UseNotion(NotionType.FailUpgrade);
                }
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.LowCoin);

                return;
            }
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.MaxLevel);

            return;
        }

        //adCount += 1;

        //if (adCount >= GameStateManager.instance.AdCount)
        //{
        //    adCount = 0;

        //    if(!playerDataBase.RemoveAds)
        //    {
        //        GoogleAdsManager.instance.admobScreen.ShowAd();
        //    }
        //}

        if (feverFillamount.gameObject.activeInHierarchy)
        {
            if (!feverMode && level + 1 < upgradeFood.maxLevel)
            {
                feverCount += 1;

                CheckFever();
            }
        }

        isDelay = true;
        Invoke("WaitDelay", 0.3f);
    }

    void CheckFever()
    {
        feverFillamount.fillAmount = feverCount * 1.0f / feverMaxCount * 1.0f;
        feverText.text = LocalizationManager.instance.GetString("FeverGauge") + "  " + ((feverCount * 1.0f / feverMaxCount * 1.0f) * 100).ToString("N1") + "%";

        if (feverCount >= feverMaxCount)
        {
            feverMode = true;

            feverEffect.SetActive(true);
            backButton.SetActive(false);

            successText.color = Color.red;

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

        for (int i = 0; i < hamburgerArray.Length; i++)
        {
            if(hamburgerArray[i].gameObject.activeInHierarchy)
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

        for (int i = 0; i < mainTruckArray.Length; i ++)
        {
            if(mainTruckArray[i].gameObject.activeInHierarchy)
            {
                mainTruckArray[i].enabled = true;
            }
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

        questManager.CheckGoal();

        float currentTime = 0f;

        while(currentTime < feverTime)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / feverTime);

            //feverText.text = LocalizationManager.instance.GetString("FeverNotion") + " : " + (fillAmount * 100).ToString("N0") + "%";

            fillAmount = Mathf.Clamp01(fillAmount);

            feverFillamount.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        lightParticle.gameObject.SetActive(false);

        feverMode = false;

        feverFillamount.fillAmount = 0;

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

        for (int i = 0; i < mainTruckArray.Length; i++)
        {
            if (mainTruckArray[i].gameObject.activeInHierarchy)
            {
                mainTruckArray[i].enabled = false;
            }
        }
    }

    void MaxLevelUpgradeSuccess()
    {
        switch (GameStateManager.instance.FoodType)
        {
            case FoodType.Hamburger:
                playerDataBase.GourmetLevel += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                playerDataBase.HamburgerMaxValue += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("HamburgerMaxValue", playerDataBase.HamburgerMaxValue);

                if (playerDataBase.NextFoodNumber == 0 && !nextFood)
                {
                    nextFood = true;

                    if (playerDataBase.HamburgerMaxValue == 1)
                    {
                        playerDataBase.NextFoodNumber += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                        lockManager.UnLocked(1);

                        changeFoodAlarmObj.SetActive(true);
                    }
                }

                break;
            case FoodType.Sandwich:
                playerDataBase.GourmetLevel += 10;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                playerDataBase.SandwichMaxValue += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("SandwichMaxValue", playerDataBase.SandwichMaxValue);

                if (playerDataBase.NextFoodNumber == 1 && !nextFood)
                {
                    nextFood = true;

                    if (playerDataBase.SandwichMaxValue == 1)
                    {
                        playerDataBase.NextFoodNumber += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                        lockManager.UnLocked(2);

                        changeFoodAlarmObj.SetActive(true);
                    }
                }

                break;
            case FoodType.SnackLab:
                playerDataBase.GourmetLevel += 100;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                playerDataBase.SnackLabMaxValue += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("SnackLabMaxValue", playerDataBase.SnackLabMaxValue);

                if (playerDataBase.NextFoodNumber == 2 && !nextFood)
                {
                    nextFood = true;

                    if (playerDataBase.SnackLabMaxValue == 1)
                    {
                        playerDataBase.NextFoodNumber += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                        lockManager.UnLocked(3);

                        changeFoodAlarmObj.SetActive(true);
                    }
                }

                break;
            case FoodType.Drink:
                playerDataBase.GourmetLevel += 150;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                playerDataBase.DrinkMaxValue += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DrinkMaxValue", playerDataBase.DrinkMaxValue);

                if (playerDataBase.NextFoodNumber == 3 && !nextFood)
                {
                    nextFood = true;

                    if (playerDataBase.DrinkMaxValue == 1)
                    {
                        playerDataBase.NextFoodNumber += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                        lockManager.UnLocked(4);

                        changeFoodAlarmObj.SetActive(true);
                    }
                }

                break;
            case FoodType.Pizza:
                playerDataBase.GourmetLevel += 150;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                playerDataBase.PizzaMaxValue += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("PizzaMaxValue", playerDataBase.PizzaMaxValue);

                if (playerDataBase.NextFoodNumber == 4 && !nextFood)
                {
                    nextFood = true;

                    if (playerDataBase.PizzaMaxValue == 1)
                    {
                        playerDataBase.NextFoodNumber += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                        changeFoodAlarmObj.SetActive(true);
                    }
                }

                break;
            case FoodType.Donut:
                playerDataBase.GourmetLevel += 200;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                playerDataBase.DonutMaxValue += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DonutMaxValue", playerDataBase.DonutMaxValue);

                if (playerDataBase.NextFoodNumber == 5 && !nextFood)
                {
                    nextFood = true;

                    if (playerDataBase.DonutMaxValue == 1)
                    {
                        playerDataBase.NextFoodNumber += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                        changeFoodAlarmObj.SetActive(true);
                    }
                }

                break;
            case FoodType.Fries:
                playerDataBase.GourmetLevel += 250;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                playerDataBase.FriesMaxValue += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("FriesMaxValue", playerDataBase.FriesMaxValue);

                if (playerDataBase.NextFoodNumber == 6 && !nextFood)
                {
                    nextFood = true;

                    if (playerDataBase.FriesMaxValue == 1)
                    {
                        playerDataBase.NextFoodNumber += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                        changeFoodAlarmObj.SetActive(true);
                    }
                }
                break;
        }

        questManager.CheckGoal();

        Debug.Log(GameStateManager.instance.FoodType + " : Max Level!");
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
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("SellCount", playerDataBase.SellCount);

            questManager.CheckGoal();
        }

        nextFood = false;

        level = 0;
        nextLevel = 0;

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
        }

        CheckFoodState();

        if(sellPriceX2 > 0)
        {
            if(sellPriceX2 >= Random.Range(0f,1f))
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

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, sellPrice);

        myMoneyPlusText.gameObject.SetActive(false);
        myMoneyPlusText.gameObject.SetActive(true);
        myMoneyPlusText.color = Color.green;
        myMoneyPlusText.text = "+" + MoneyUnitString.ToCurrencyString(sellPrice);

        UpgradeInitialize();

        SoundManager.instance.PlaySFX(GameSfxType.Sell);
    }

    public void CheckDefTicket()
    {
        if(level >= 10 && level + 1 < upgradeFood.maxLevel)
        {
            defTicketObj.SetActive(true);

            defTicketText.text = playerDataBase.DefDestroyTicket + "/1";

            if (playerDataBase.DefDestroyTicket <= 0)
            {
                if (isDef)
                {
                    defDestroy -= defDestroyPlus;

                    if (GameStateManager.instance.AnimalType > AnimalType.Colobus)
                    {
                        defDestroy = (float)GameStateManager.instance.AnimalType * 2f;
                    }

                    defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N1") + "%";
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

                if (GameStateManager.instance.AnimalType > AnimalType.Colobus)
                {
                    defDestroy = (float)GameStateManager.instance.AnimalType * 2f;
                }

                defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N1") + "%";
            }

            isDef = false;
            checkMark.SetActive(false);

            defTicketObj.SetActive(false);
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
        if (level + 1 >= upgradeFood.maxLevel)
        {
            return;
        }

        if (!isDef)
        {
            if (playerDataBase.DefDestroyTicket > 0)
            {
                isDef = true;
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                return;
            }

            defDestroy += defDestroyPlus;

            checkMark.SetActive(true);
        }
        else
        {
            defDestroy -= defDestroyPlus;

            if (GameStateManager.instance.AnimalType > AnimalType.Colobus)
            {
                defDestroy = (float)GameStateManager.instance.AnimalType * 2f;
            }

            isDef = false;
            checkMark.SetActive(false);
        }

        if(defDestroy >= 100)
        {
            defDestroy = 100;
        }

        defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N1") + "%";
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

        questManager.CheckGoal();
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

                        need -= (int)(need * (0.01f * needPlus));

                        if (level + 1 < upgradeFood.maxLevel)
                        {
                            needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice");

                            if (needPlus > 0)
                            {
                                needText.text += " (-" + (needPlus) + "%)</size>\n" + MoneyUnitString.ToCurrencyString(need);
                            }
                            else
                            {
                                needText.text += "</size>\n" + MoneyUnitString.ToCurrencyString(need);
                            }
                        }

                        playerDataBase.Portion1 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

                        StartCoroutine(PortionCoroution1());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion1);
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

                        sellPrice += (int)(sellPrice * (0.01f * sellPricePlus));

                        priceText.text = "<size=45>" + LocalizationManager.instance.GetString("NowPrice");

                        if (sellPricePlus > 0)
                        {
                            priceText.text += " (+" + (sellPricePlus) + "%)</size>\n" + MoneyUnitString.ToCurrencyString(sellPrice);
                        }
                        else
                        {
                            priceText.text += "</size>\n" + MoneyUnitString.ToCurrencyString(sellPrice);
                        }

                        playerDataBase.Portion2 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

                        StartCoroutine(PortionCoroution2());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion2);
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

                        playerDataBase.Portion3 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

                        UpgradeInitialize();

                        StartCoroutine(PortionCoroution3());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion3);
                    }
                    else
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.NotSources);
                        NotionManager.instance.UseNotion(NotionType.LowPortion);
                    }
                }
                break;
            case 3:
                if (!feverMode)
                {
                    if (playerDataBase.Portion4 > 0)
                    {
                        playerDataBase.Portion4 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

                        feverCount += (feverMaxCount * 0.5f);

                        CheckFever();

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion4);
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

                        defDestroy += 20;
                        defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N1") + "%";

                        playerDataBase.Portion5 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);

                        playerDataBase.UseSources += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UseSources", playerDataBase.UseSources);

                        StartCoroutine(PortionCoroution5());

                        SoundManager.instance.PlaySFX(GameSfxType.UseSources);
                        NotionManager.instance.UseNotion(NotionType.UsePortionNotion5);
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

        need -= (int)(need * (0.01f * needPlus));

        if (level + 1 < upgradeFood.maxLevel)
        {
            needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice");

            if (needPlus > 0)
            {
                needText.text += " (-" + (needPlus) + "%)</size>\n" + MoneyUnitString.ToCurrencyString(need);
            }
            else
            {
                needText.text += "</size>\n" + MoneyUnitString.ToCurrencyString(need);
            }
        }
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

        sellPricePlus -= 30;

        sellPrice += (int)(sellPrice * (0.01f * sellPricePlus));

        priceText.text = "<size=45>" + LocalizationManager.instance.GetString("NowPrice");

        if (sellPricePlus > 0)
        {
            priceText.text += " (+" + (sellPricePlus) + "%)</size>\n" + MoneyUnitString.ToCurrencyString(sellPrice);
        }
        else
        {
            priceText.text += "</size>\n" + MoneyUnitString.ToCurrencyString(sellPrice);
        }
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

        defDestroy -= 20;
        defDestroyText.text = LocalizationManager.instance.GetString("DefDestroyPercent") + " : " + defDestroy.ToString("N1") + "%";
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

    public void MoreGame()
    {
        FirebaseAnalytics.LogEvent("MoreGame");

        Application.OpenURL("https://play.google.com/store/apps/dev?id=6063135311448213232");
    }

    public void OpenURL()
    {
        FirebaseAnalytics.LogEvent("OpenAppReview");

#if UNITY_ANDROID || UNITY_EDITOR
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.whilili.foodtruck");
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/us/app/food-truck-evolution/id6466390705");
#endif

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
        if(GameStateManager.instance.HamburgerLevel <= 2 && GameStateManager.instance.SandwichLevel <= 2 && GameStateManager.instance.SnackLabLevel <= 2
            && GameStateManager.instance.DrinkLevel <= 2 && GameStateManager.instance.PizzaLevel <= 2 && GameStateManager.instance.DonutLevel <= 2
            && playerDataBase.Coin < 1600)
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
            PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 1000000);
        }
        else
        {
            PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 100000);
        }
    }
}
