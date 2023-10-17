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
    public Text signText;

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


    [Space]
    [Title("Upgrade")]
    public GameObject mainUI;
    public GameObject inGameUI;
    public GameObject languageUI;
    public GameObject nextFoodUI;


    [Space]
    [Title("Portion")]
    public Text portionText1, portionText2, portionText3, portionText4;
    public Image portionFillamount1, portionFillamount2, portionFillamount3, portionFillamount4;
    private bool portion1, portion2, portion3, portion4;
    private int portionTime = 30;
    private int portionPlus = 1;

    public Text titleText;
    public Text needText;
    public Text priceText;
    public Text successText;

    public GameObject defTicketObj;
    public Text defTicketText;
    public GameObject checkMark;

    [Space]
    [Title("Fever")]
    public Image feverFillamount;

    public GameObject feverEffect;
    public GameObject backButton;

    public Text feverText;

    private int feverCount = 0;
    private int feverMaxCount = 100;
    private int feverTime = 30;
    private int feverPlus = 3;

    private bool feverMode = false;


    public int need = 0;
    public int price = 0;
    public float success = 0;
    private float successPortion = 0;
    private float successFever = 0;

    public int level = 0;
    public int nextLevel = 0;

    private int adCount = 0;

    public bool isDelay_Camera = false;
    public bool isDelay = false;
    public bool isDef = false;

    public bool nextFood = false;

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

    public ParticleSystem levelUpParticle;
    public ParticleSystem bombPartice;

    public TutorialManager tutorialManager;

    UpgradeDataBase upgradeDataBase;
    PlayerDataBase playerDataBase;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    private void Awake()
    {
        instance = this;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (upgradeDataBase == null) upgradeDataBase = Resources.Load("UpgradeDataBase") as UpgradeDataBase;

        //versionText.text = "v" + Application.version;

        goldText.text = "0";
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

        nextFoodUI.SetActive(false);

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

        portionFillamount1.fillAmount = 0;
        portionFillamount2.fillAmount = 0;
        portionFillamount3.fillAmount = 0;
        portionFillamount4.fillAmount = 0;

        defTicketObj.SetActive(false);

        bankruptcyView.SetActive(false);
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
    }

    public void SuccessLogin()
    {
        checkInternet.SetActive(false);
        loginView.SetActive(false);

        feverText.text = LocalizationManager.instance.GetString("FeverNotion") + " 0%";

        switch (GameStateManager.instance.FoodType)
        {
            case FoodType.Hamburger:
                level = GameStateManager.instance.HamburgerLevel;
                nextLevel = GameStateManager.instance.HamburgerLevel / 5;
                break;
            case FoodType.Sandwich:
                level = GameStateManager.instance.SandwichLevel;
                nextLevel = GameStateManager.instance.SandwichLevel / 5;
                break;
            case FoodType.SnackLab:
                level = GameStateManager.instance.SnackLabLevel;
                nextLevel = GameStateManager.instance.SnackLabLevel / 5;
                break;
            case FoodType.Drink:
                level = GameStateManager.instance.DrinkLevel;
                nextLevel = GameStateManager.instance.DrinkLevel / 5;
                break;
            case FoodType.Pizza:
                level = GameStateManager.instance.PizzaLevel;
                nextLevel = GameStateManager.instance.PizzaLevel / 5;
                break;
            case FoodType.Donut:
                level = GameStateManager.instance.DonutLevel;
                nextLevel = GameStateManager.instance.DonutLevel / 5;
                break;
        }

        CheckFoodState();

        isDelay_Camera = true;

        if(playerDataBase.RemoveAds)
        {
            GoogleAdsManager.instance.admobBanner.DestroyAd();
        }

        PlayfabManager.instance.GetTitleInternalData("Coupon", CheckCoupon);

        if(!GameStateManager.instance.Tutorial)
        {
            tutorialManager.TutorialStart();
        }
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
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
        }
    }

    public void RenewalVC()
    {
        goldText.text = MoneyUnitString.ToCurrencyString(playerDataBase.Coin);
    }

    public void GameStart()
    {
        if (!isDelay_Camera) return;

        isDelay_Camera = false;

        mainUI.SetActive(false);
        inGameUI.SetActive(true);
        languageUI.SetActive(false);

        nextFood = false;

        upgradeFood = upgradeDataBase.GetUpgradeFood(GameStateManager.instance.FoodType);

        //nextLevel = GameStateManager.instance.Level / 5;

        UpgradeInitialize();

        CheckDefTicket();

        cameraController.GoToB();

        CheckPortion();
    }

    public void GameStop()
    {
        if (!isDelay_Camera) return;

        isDelay_Camera = false;

        mainUI.SetActive(true);
        inGameUI.SetActive(false);
        languageUI.SetActive(true);

        cameraController.GoToA();
    }

    public void Initialize()
    {
        signText.text = GameStateManager.instance.NickName;

        RenewalVC();

        if (playerDataBase.NextFoodNumber >= 1)
        {
            nextFoodUI.SetActive(true);
        }
        else
        {
            nextFoodUI.SetActive(false);
        }
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
                nextLevel = GameStateManager.instance.HamburgerLevel / 5;
                break;
            case FoodType.Sandwich:
                level = GameStateManager.instance.SandwichLevel;
                nextLevel = GameStateManager.instance.SandwichLevel / 5;
                break;
            case FoodType.SnackLab:
                level = GameStateManager.instance.SnackLabLevel;
                nextLevel = GameStateManager.instance.SnackLabLevel / 5;
                break;
            case FoodType.Drink:
                level = GameStateManager.instance.DrinkLevel;
                nextLevel = GameStateManager.instance.DrinkLevel / 5;
                break;
            case FoodType.Pizza:
                level = GameStateManager.instance.PizzaLevel;
                nextLevel = GameStateManager.instance.PizzaLevel / 5;
                break;
            case FoodType.Donut:
                level = GameStateManager.instance.DonutLevel;
                nextLevel = GameStateManager.instance.DonutLevel / 5;
                break;
        }

        CheckFoodState();

        UpgradeInitialize();

        Debug.Log(GameStateManager.instance.FoodType.ToString() + " Level Up");
    }

    public void UpgradeInitialize()
    {
        need = upgradeFood.GetNeed(level);
        price = upgradeFood.GetPrice(level);
        success = upgradeFood.GetSuccess(level) + ((int)GameStateManager.instance.TruckType * 0.5f);

        if (portion1)
        {
            need -= (int)(need * 0.3f);
        }

        if (portion2)
        {
            price += (int)(price * 0.3f);
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

        success += successPortion + successFever;

        if (success >= 100)
        {
            success = 100;
        }

        titleText.text = LocalizationManager.instance.GetString(GameStateManager.instance.FoodType.ToString()) + "  +" + (level + 1) + " / " + upgradeFood.maxLevel;

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
            titleText.color = new Color(0, 150 / 255f, 1);
        }
        else if (level >= 4)
        {
            titleText.color = new Color(1, 150 / 255f, 0);
        }
        else
        {
            titleText.color = Color.white;
        }


        successText.text = LocalizationManager.instance.GetString("SuccessPercent") + " : " + success + "%";
        needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice") + "</size>\n" + MoneyUnitString.ToCurrencyString(need);

        if (successPortion > 0 || successFever > 0)
        {
            successText.text += " (+" + (successPortion + successFever) + "%)";
        }

        if (portion1)
        {
            needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice") + " (-30%)</size>\n" + MoneyUnitString.ToCurrencyString(need);
        }
        else
        {
            needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice") + "</size>\n" + MoneyUnitString.ToCurrencyString(need);
        }

        if (portion2)
        {
            priceText.text = "<size=45>" + LocalizationManager.instance.GetString("NowPrice") + " (+30%)</size>\n" + MoneyUnitString.ToCurrencyString(price);
        }
        else
        {
            priceText.text = "<size=45>" + LocalizationManager.instance.GetString("NowPrice") + "</size>\n" + MoneyUnitString.ToCurrencyString(price);
        }

        if (GameStateManager.instance.Developer) success = 100;

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

        switch (GameStateManager.instance.FoodType)
        {
            case FoodType.Hamburger:
                hamburgerArray[nextLevel].gameObject.SetActive(true);
                hamburgerArray[nextLevel].Initialize(GameStateManager.instance.HamburgerLevel - (5 * nextLevel));
                if(feverMode)
                {
                    hamburgerArray[nextLevel].FeverOn();
                }
                break;
            case FoodType.Sandwich:
                sandwichArray[nextLevel].gameObject.SetActive(true);
                sandwichArray[nextLevel].Initialize(GameStateManager.instance.SandwichLevel - (5 * nextLevel));
                if (feverMode)
                {
                    sandwichArray[nextLevel].FeverOn();
                }
                break;
            case FoodType.SnackLab:
                snackLabArray[nextLevel].gameObject.SetActive(true);
                snackLabArray[nextLevel].Initialize(GameStateManager.instance.SnackLabLevel - (5 * nextLevel));
                if (feverMode)
                {
                    snackLabArray[nextLevel].FeverOn();
                }
                break;
            case FoodType.Drink:
                drinkArray[nextLevel].gameObject.SetActive(true);
                drinkArray[nextLevel].Initialize(GameStateManager.instance.DrinkLevel - (5 * nextLevel));
                if (feverMode)
                {
                    drinkArray[nextLevel].FeverOn();
                }
                break;
            case FoodType.Pizza:
                pizzaArray[nextLevel].gameObject.SetActive(true);
                pizzaArray[nextLevel].Initialize(GameStateManager.instance.PizzaLevel - (5 * nextLevel));
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
                    donutArray[nextLevel].FeverOn();
                }
                break;
        }
    }

    void CheckFoodLevelUp()
    {
        switch (GameStateManager.instance.FoodType)
        {
            case FoodType.Hamburger:
                hamburgerArray[nextLevel].LevelUp();
                break;
            case FoodType.Sandwich:
                sandwichArray[nextLevel].LevelUp();
                break;
            case FoodType.SnackLab:
                snackLabArray[nextLevel].LevelUp();
                break;
            case FoodType.Drink:
                drinkArray[nextLevel].LevelUp();
                break;
            case FoodType.Pizza:
                pizzaArray[nextLevel].LevelUp();
                break;
            case FoodType.Donut:
                if(donutArray.Length - 1 < nextLevel)
                {

                }
                else
                {
                    donutArray[nextLevel].LevelUp();
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

                            if(playerDataBase.DonutLevel < level)
                            {
                                playerDataBase.DonutLevel = level + 1;

                                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DonutLevel", playerDataBase.DonutLevel);
                            }

                            break;
                    }

                    CheckFoodLevelUp();

                    UpgradeInitialize();

                    if(level + 1 >= upgradeFood.maxLevel)
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);

                        MaxLevelUpgradeSuccess();
                    }
                    else
                    {
                        if(level + 1 >= 10)
                        {
                            switch (GameStateManager.instance.FoodType)
                            {
                                case FoodType.Hamburger:
                                    if(playerDataBase.NextFoodNumber == 0 && !nextFood)
                                    {
                                        nextFood = true;

                                        playerDataBase.HamburgerMaxValue += 1;
                                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("HamburgerMaxValue", playerDataBase.HamburgerMaxValue);

                                        if (playerDataBase.HamburgerMaxValue == 1)
                                        {
                                            playerDataBase.NextFoodNumber += 1;

                                            nextFoodUI.SetActive(true);
                                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                                            changeFoodAlarmObj.SetActive(true);
                                        }
                                    }
                                    break;
                                case FoodType.Sandwich:
                                    if(playerDataBase.NextFoodNumber == 1 && !nextFood)
                                    {
                                        nextFood = true;

                                        playerDataBase.SandwichMaxValue += 1;
                                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SandwichMaxValue", playerDataBase.SandwichMaxValue);

                                        if (playerDataBase.SandwichMaxValue == 1)
                                        {
                                            playerDataBase.NextFoodNumber += 1;
                                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                                            changeFoodAlarmObj.SetActive(true);
                                        }
                                    }
                                    break;
                                case FoodType.SnackLab:
                                    if (playerDataBase.NextFoodNumber == 2 && !nextFood)
                                    {
                                        nextFood = true;

                                        playerDataBase.SnackLabMaxValue += 1;
                                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SnackLabMaxValue", playerDataBase.SnackLabMaxValue);

                                        if (playerDataBase.SnackLabMaxValue == 1)
                                        {
                                            playerDataBase.NextFoodNumber += 1;
                                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                                            changeFoodAlarmObj.SetActive(true);
                                        }
                                    }
                                    break;
                                case FoodType.Drink:
                                    if (playerDataBase.NextFoodNumber == 3 && !nextFood)
                                    {
                                        nextFood = true;

                                        playerDataBase.DrinkMaxValue += 1;
                                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DrinkMaxValue", playerDataBase.DrinkMaxValue);

                                        if (playerDataBase.DrinkMaxValue == 1)
                                        {
                                            playerDataBase.NextFoodNumber += 1;
                                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                                            changeFoodAlarmObj.SetActive(true);
                                        }
                                    }
                                    break;
                                case FoodType.Pizza:
                                    if (playerDataBase.NextFoodNumber == 4 && !nextFood)
                                    {
                                        nextFood = true;

                                        playerDataBase.PizzaMaxValue += 1;
                                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("PizzaMaxValue", playerDataBase.PizzaMaxValue);

                                        if (playerDataBase.PizzaMaxValue == 1)
                                        {
                                            playerDataBase.NextFoodNumber += 1;
                                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                                            changeFoodAlarmObj.SetActive(true);
                                        }
                                    }
                                    break;
                                case FoodType.Donut:
                                    if (playerDataBase.NextFoodNumber == 5 && !nextFood)
                                    {
                                        nextFood = true;

                                        playerDataBase.DonutMaxValue += 1;
                                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DonutMaxValue", playerDataBase.DonutMaxValue);

                                        if (playerDataBase.DonutMaxValue == 1)
                                        {
                                            playerDataBase.NextFoodNumber += 1;
                                            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);

                                            changeFoodAlarmObj.SetActive(true);
                                        }
                                    }
                                    break;
                            }
                        }

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

                    NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);
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

        adCount += 1;

        if (adCount >= 150)
        {
            adCount = 0;

            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || playerDataBase.RemoveAds)
            {

            }
            else
            {
                GoogleAdsManager.instance.admobScreen.ShowAd();
            }
        }

        if (!feverMode && level + 1 < upgradeFood.maxLevel)
        {
            feverCount += 1;

            CheckFever();
        }

        isDelay = true;
        Invoke("WaitDelay", 0.3f);
    }

    void CheckFever()
    {
        feverFillamount.fillAmount = feverCount * 1.0f / feverMaxCount * 1.0f;

        feverText.text = LocalizationManager.instance.GetString("FeverNotion") + " " + ((feverCount * 1.0f / feverMaxCount * 1.0f) * 100) + "%";

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

        for(int i = 0; i < mainTruckArray.Length; i ++)
        {
            if(mainTruckArray[i].gameObject.activeInHierarchy)
            {
                mainTruckArray[i].enabled = true;
            }
        }

        float currentTime = 0f;

        while(currentTime < feverTime)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / feverTime);

            feverText.text = LocalizationManager.instance.GetString("FeverNotion") + " " + (fillAmount * 100).ToString("N0") + "%";

            fillAmount = Mathf.Clamp01(fillAmount);

            feverFillamount.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        feverMode = false;

        feverCount = 0;
        feverFillamount.fillAmount = 0;

        successText.color = Color.green;

        UpgradeInitialize();

        feverEffect.SetActive(false);
        backButton.SetActive(true);

        SoundManager.instance.StopFever();

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

                break;
            case FoodType.Sandwich:
                playerDataBase.GourmetLevel += 10;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                break;
            case FoodType.SnackLab:
                playerDataBase.GourmetLevel += 100;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                break;
            case FoodType.Drink:
                playerDataBase.GourmetLevel += 150;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                break;
            case FoodType.Pizza:
                playerDataBase.GourmetLevel += 150;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                break;
            case FoodType.Donut:
                //playerDataBase.GourmetLevel += 10000;
                //PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);

                break;
        }

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
        }

        CheckFoodState();

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, price);

        UpgradeInitialize();

        SoundManager.instance.PlaySFX(GameSfxType.Sell);

        NotionManager.instance.UseNotion(NotionType.SuccessSell);
    }

    public void CheckDefTicket()
    {
        if(level >= 10 && level + 1 < upgradeFood.maxLevel)
        {
            defTicketObj.SetActive(true);

            defTicketText.text = playerDataBase.DefDestroyTicket + "/1";

            if (playerDataBase.DefDestroyTicket <= 0)
            {
                isDef = false;
                checkMark.SetActive(false);
            }
        }
        else
        {
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
                NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                return;
            }

            checkMark.SetActive(true);
        }
        else
        {
            isDef = false;
            checkMark.SetActive(false);
        }
    }

    void CheckPortion()
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

                        if (level + 1 < upgradeFood.maxLevel)
                        {
                            need -= (int)(need * 0.3f);
                            needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice") + " (-30%)</size>\n" + MoneyUnitString.ToCurrencyString(need);
                        }

                        playerDataBase.Portion1 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);

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

                        price += (int)(price * 0.3f);
                        priceText.text = "<size=45>" + LocalizationManager.instance.GetString("NowPrice") + " (+30%)</size>\n" + MoneyUnitString.ToCurrencyString(price);

                        playerDataBase.Portion2 -= 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);

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

                        feverCount += (int)(feverMaxCount * 0.5f);

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
        }

        CheckPortion();
    }

    IEnumerator PortionCoroution1()
    {
        float currentTime = 0f;

        while (currentTime < portionTime)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portionTime);

            fillAmount = Mathf.Clamp01(fillAmount);

            portionFillamount1.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        portion1 = false;
        portionFillamount1.fillAmount = 0;

        if (level + 1 < upgradeFood.maxLevel)
        {
            need = upgradeFood.GetNeed(level);
            needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice") + "</size>\n" + MoneyUnitString.ToCurrencyString(need);
        }
    }

    IEnumerator PortionCoroution2()
    {
        float currentTime = 0f;

        while (currentTime < portionTime)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portionTime);

            fillAmount = Mathf.Clamp01(fillAmount);

            portionFillamount2.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        portion2 = false;
        portionFillamount2.fillAmount = 0;

        price = upgradeFood.GetPrice(level);
        priceText.text = "<size=45>" + LocalizationManager.instance.GetString("NowPrice") + "</size>\n" + MoneyUnitString.ToCurrencyString(price);
    }

    IEnumerator PortionCoroution3()
    {
        float currentTime = 0f;

        while (currentTime < portionTime)
        {
            float fillAmount = Mathf.Lerp(1.0f, 0, currentTime / portionTime);

            fillAmount = Mathf.Clamp01(fillAmount);

            portionFillamount3.fillAmount = fillAmount;

            currentTime += Time.deltaTime;

            yield return null;
        }

        portion3 = false;
        portionFillamount3.fillAmount = 0;

        UpgradeInitialize();
    }

    public void OpenLoginView()
    {
        if (!loginView.activeSelf)
        {
            loginView.SetActive(true);

            loginButtonArray[0].SetActive(false);
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
