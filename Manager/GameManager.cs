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
    public Text versionText;

    public Text signText;

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

    public Text titleText;
    public Text needText;
    public Text priceText;
    public Text successText;

    public Text defTicketText;
    public GameObject checkMark;


    public int need = 0;
    public int price = 0;
    public float success = 0;

    public int level = 0;
    public int nextLevel = 0;

    private int adCount = 0;

    public bool isDelay_Camera = false;
    public bool isDelay = false;
    public bool isDef = false;

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

    public UpgradeDataBase upgradeDataBase;
    public PlayerDataBase playerDataBase;

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
    }

    public void SuccessLogin()
    {
        checkInternet.SetActive(false);
        loginView.SetActive(false);

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

        upgradeFood = upgradeDataBase.GetUpgradeFood(GameStateManager.instance.FoodType);

        //nextLevel = GameStateManager.instance.Level / 5;

        UpgradeInitialize();

        CheckDefTicket();

        cameraController.GoToB();
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
        price = upgradeFood.GetPrice(level); ;
        success = upgradeFood.GetSuccess(level) + ((int)GameStateManager.instance.TruckType * 0.5f);

        if(success >= 100)
        {
            success = 100;
        }

        titleText.text = LocalizationManager.instance.GetString(GameStateManager.instance.FoodType.ToString()) + "  +" + (level + 1);
        successText.text = LocalizationManager.instance.GetString("SuccessPercent") + " : " + success + "%";
        needText.text = "<size=45>" + LocalizationManager.instance.GetString("NeedPrice") + "</size>\n" + MoneyUnitString.ToCurrencyString(need);
        priceText.text = "<size=45>" + LocalizationManager.instance.GetString("NowPrice") + "</size>\n" + MoneyUnitString.ToCurrencyString(price);

        if (GameStateManager.instance.Developer) success = 100;

        if (level + 1 >= upgradeFood.maxLevel)
        {
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
                break;
            case FoodType.Sandwich:
                sandwichArray[nextLevel].gameObject.SetActive(true);
                sandwichArray[nextLevel].Initialize(GameStateManager.instance.SandwichLevel - (5 * nextLevel));
                break;
            case FoodType.SnackLab:
                snackLabArray[nextLevel].gameObject.SetActive(true);
                snackLabArray[nextLevel].Initialize(GameStateManager.instance.SnackLabLevel - (5 * nextLevel));
                break;
            case FoodType.Drink:
                drinkArray[nextLevel].gameObject.SetActive(true);
                drinkArray[nextLevel].Initialize(GameStateManager.instance.DrinkLevel - (5 * nextLevel));
                break;
            case FoodType.Pizza:
                pizzaArray[nextLevel].gameObject.SetActive(true);
                pizzaArray[nextLevel].Initialize(GameStateManager.instance.PizzaLevel - (5 * nextLevel));
                break;
            case FoodType.Donut:
                donutArray[nextLevel].gameObject.SetActive(true);
                donutArray[nextLevel].Initialize(GameStateManager.instance.DonutLevel - (5 * nextLevel));
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
                donutArray[nextLevel].LevelUp();
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
                    }

                    CheckFoodLevelUp();

                    UpgradeInitialize();

                    if(level + 1 >= upgradeFood.maxLevel)
                    {
                        SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);

                        StartCoroutine(MaxLevelUpgradeSuccess());
                    }
                    else
                    {
                        if ((level + 1) % 5 == 0)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Upgrade5);

                            nextLevel++;
                            CheckFoodState();
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
            }


        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.MaxLevel);
        }

        adCount += 1;

        if (adCount >= 50)
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

        isDelay = true;
        Invoke("WaitDelay", 0.2f);
    }

    IEnumerator MaxLevelUpgradeSuccess()
    {
        switch (GameStateManager.instance.FoodType)
        {
            case FoodType.Hamburger:
                playerDataBase.HamburgerMaxValue += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("HamburgerMaxValue", playerDataBase.HamburgerMaxValue);

                yield return waitForSeconds;

                if (playerDataBase.HamburgerMaxValue == 1)
                {
                    playerDataBase.NextFoodNumber += 1;

                    nextFoodUI.SetActive(true);

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
                }
                break;
            case FoodType.Sandwich:
                playerDataBase.SandwichMaxValue += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("SandwichMaxValue", playerDataBase.SandwichMaxValue);

                yield return waitForSeconds;

                if (playerDataBase.SandwichMaxValue == 1)
                {
                    playerDataBase.NextFoodNumber += 1;

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
                }
                break;
            case FoodType.SnackLab:
                playerDataBase.SnackLabMaxValue += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("SnackLabMaxValue", playerDataBase.SnackLabMaxValue);

                yield return waitForSeconds;

                if (playerDataBase.SnackLabMaxValue == 1)
                {
                    playerDataBase.NextFoodNumber += 1;

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
                }
                break;
            case FoodType.Drink:
                playerDataBase.DrinkMaxValue += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DrinkMaxValue", playerDataBase.DrinkMaxValue);

                yield return waitForSeconds;

                if (playerDataBase.DrinkMaxValue == 1)
                {
                    playerDataBase.NextFoodNumber += 1;

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
                }
                break;
            case FoodType.Pizza:
                playerDataBase.PizzaMaxValue += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("PizzaMaxValue", playerDataBase.PizzaMaxValue);

                yield return waitForSeconds;

                if (playerDataBase.PizzaMaxValue == 1)
                {
                    playerDataBase.NextFoodNumber += 1;

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
                }
                break;
            case FoodType.Donut:
                playerDataBase.DonutMaxValue += 1;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DonutMaxValue", playerDataBase.DonutMaxValue);

                yield return waitForSeconds;

                if (playerDataBase.DonutMaxValue == 1)
                {
                    playerDataBase.NextFoodNumber += 1;

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
                }
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
        defTicketText.text = playerDataBase.DefDestroyTicket + "/1";

        if(playerDataBase.DefDestroyTicket <= 0)
        {
            isDef = false;
            checkMark.SetActive(false);
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
        if(!isDef)
        {
            if(playerDataBase.DefDestroyTicket > 0)
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
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.whilili.foodtruck");
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
}
