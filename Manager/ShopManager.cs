using Firebase.Analytics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    public GameObject shopView;
    public GameObject speicalShopView;

    public GameObject alarm;
    public GameObject dailyAlarm;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;

    [Space]
    [Title("SpeicalTopMenu")]
    public Image[] speicalTopMenuImgArray;
    public Sprite[] speicalTopMenuSpriteArray;

    [Space]
    [Title("ScrollView")]
    public GameObject[] shopArray;
    public GameObject[] speicalShopArray;
    public RectTransform shopRectTransform;

    [Space]
    [Title("Item")]
    public ShopContent[] shopContents;

    [Space]
    [Title("Truck")]
    public GameObject mainTruck;
    public GameObject[] mainTruckArray;
    public GameObject[] shopTruckArray;

    public GameObject mainAnimal;
    public GameObject[] mainAnimalArray;
    public GameObject[] shopAnimalArray;

    public GameObject buyToCoinButton;
    public GameObject selectObj;

    public GameObject buyRmTruck;
    public GameObject[] buyRmObj;
    public LocalizationContent[] buyRmText;

    public GameObject buyRmAnimal;
    public GameObject[] buyRmAnimalObj;
    public LocalizationContent[] buyRmAnimalText;

    public Text selectText;
    public Text priceText;

    public LocalizationContent titleText;
    public LocalizationContent effectText;
    public Text passiveText;
    public LocalizationContent infoText;





    public Text dailyShopCountText;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    private int index = -1;
    private int speicalIndex = -1;

    private int truckIndex = 0;
    private int animalIndex = 0;

    bool hold = false;
    bool buy = false;
    bool isDelay = false;
    bool isTimer = false;

    TruckInfo truckInfo = new TruckInfo();
    AnimalInfo animalInfo = new AnimalInfo();

    List<string> itemList = new List<string>();

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    public ResetManager resetManager;

    PlayerDataBase playerDataBase;
    TruckDataBase truckDataBase;
    AnimalDataBase animalDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (truckDataBase == null) truckDataBase = Resources.Load("TruckDataBase") as TruckDataBase;
        if (animalDataBase == null) animalDataBase = Resources.Load("AnimalDataBase") as AnimalDataBase;

        shopView.SetActive(false);
        speicalShopView.SetActive(false);

        alarm.SetActive(false);
        dailyAlarm.SetActive(false);
    }

    private void Start()
    {
        for(int i = 0; i < shopArray.Length; i ++)
        {
            shopArray[i].SetActive(false);
        }

        //for (int i = 0; i < speicalShopArray.Length; i++)
        //{
        //    speicalShopArray[i].SetActive(false);
        //}

        for (int i = 0; i < mainTruckArray.Length; i ++)
        {
            mainTruckArray[i].SetActive(false);
        }

        for (int i = 0; i < mainAnimalArray.Length; i++)
        {
            mainAnimalArray[i].SetActive(false);
        }

        mainTruckArray[(int)GameStateManager.instance.TruckType].SetActive(true);
        mainAnimalArray[(int)GameStateManager.instance.AnimalType].SetActive(true);

        isTimer = true;
        dailyShopCountText.text = "";
        StartCoroutine(DailyShopTimer());

        shopRectTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void SetAlarm()
    {
        alarm.SetActive(true);
        dailyAlarm.SetActive(true);
    }

    public void OpenShopView()
    {
        if(!shopView.activeInHierarchy)
        {
            shopView.SetActive(true);

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                resetManager.Initialize();
            }

            if (!isTimer)
            {
                isTimer = true;
                StartCoroutine(DailyShopTimer());
            }

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            if (index == - 1)
            {
                ChangeTopToggle(0);
            }

            FirebaseAnalytics.LogEvent("OpenShop");
        }
        else
        {
            shopView.SetActive(false);
        }
    }

    public void ChangeTopToggle(int number)
    {
        if (index == number) return;

        index = number;

        for (int i = 0; i < topMenuImgArray.Length; i++)
        {
            topMenuImgArray[i].sprite = topMenuSpriteArray[0];
            shopArray[i].gameObject.SetActive(false);
        }

        topMenuImgArray[number].sprite = topMenuSpriteArray[1];
        shopArray[number].gameObject.SetActive(true);

        switch (number)
        {
            case 0:
                shopContents[0].Initialize(ItemType.DailyReward, BuyType.Free, this);
                shopContents[1].Initialize(ItemType.AdReward_Gold, BuyType.Ad, this);
                shopContents[2].Initialize(ItemType.DefDestroyTicket, BuyType.Coin, this);
                shopContents[6].Initialize(ItemType.AdReward_Potion, BuyType.Ad, this);
                shopContents[7].Initialize(ItemType.RemoveAds, BuyType.Rm, this);
                shopContents[11].Initialize(ItemType.DailyReward_Portion, BuyType.Free, this);
                shopContents[12].Initialize(ItemType.GoldX2, BuyType.Rm, this);

                if (!GameStateManager.instance.DailyReward)
                {
                    shopContents[0].SetLocked(false);
                }

                if (!GameStateManager.instance.DailyReward_Portion)
                {
                    shopContents[11].SetLocked(false);
                }

                if (!GameStateManager.instance.DailyAdsReward)
                {
                    shopContents[1].SetLocked(false);
                }

                if (!GameStateManager.instance.DailyAdsReward2)
                {
                    shopContents[6].SetLocked(false);
                }

                if (playerDataBase.RemoveAds)
                {
                    shopContents[7].gameObject.SetActive(false);
                }

                if(playerDataBase.GoldX2)
                {
                    shopContents[12].gameObject.SetActive(false);
                }

                break;
            case 1:
                shopContents[8].Initialize(ItemType.PortionSet1, BuyType.Rm, this);
                shopContents[9].Initialize(ItemType.PortionSet2, BuyType.Rm, this);
                shopContents[10].Initialize(ItemType.PortionSet3, BuyType.Rm, this);

                break;
            case 2:
                shopContents[3].Initialize(ItemType.GoldShop1, BuyType.Rm, this);
                shopContents[4].Initialize(ItemType.GoldShop2, BuyType.Rm, this);
                shopContents[5].Initialize(ItemType.GoldShop3, BuyType.Rm, this);
                break;
        }
    }

    public void BuyItem(ItemType item, BuyType buy)
    {
        if (isDelay) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        switch (item)
        {
            case ItemType.DailyReward:
                if (GameStateManager.instance.DailyReward) return;

                GameStateManager.instance.DailyReward = true;

                shopContents[0].SetLocked(true);

                int random = Random.Range(100000, 200001);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, random);

                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessReward);

                break;
            case ItemType.AdReward_Gold:
                GoogleAdsManager.instance.admobReward_Gold.ShowAd(0);

                break;
            case ItemType.DefDestroyTicket:
                if (playerDataBase.Coin >= 1000000)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, 1000000);

                    playerDataBase.DefDestroyTicket += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicket", playerDataBase.DefDestroyTicket);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCoin);
                }
                break;
            case ItemType.GoldShop1:
                break;
            case ItemType.GoldShop2:
                break;
            case ItemType.GoldShop3:
                break;
            case ItemType.AdReward_Potion:
                GoogleAdsManager.instance.admobReward_Portion.ShowAd(1);
                break;
            case ItemType.RemoveAds:
                break;
            case ItemType.PortionSet1:
                break;
            case ItemType.PortionSet2:
                break;
            case ItemType.PortionSet3:
                break;
            case ItemType.DailyReward_Portion:

                if (GameStateManager.instance.DailyReward_Portion) return;

                GameStateManager.instance.DailyReward_Portion = true;

                shopContents[11].SetLocked(true);

                switch(Random.Range(0, 4))
                {
                    case 0:
                        playerDataBase.Portion1 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
                        break;
                    case 1:
                        playerDataBase.Portion2 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
                        break;
                    case 2:
                        playerDataBase.Portion3 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
                        break;
                    case 3:
                        playerDataBase.Portion4 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
                        break;
                }

                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessReward);
                break;
        }

        isDelay = true;
        Invoke("Delay", 0.2f);
    }

    void Delay()
    {
        isDelay = false;
    }

    public void SuccessWatchAd()
    {
        alarm.SetActive(false);
        dailyAlarm.SetActive(false);

        GameStateManager.instance.DailyAdsReward = true;

        shopContents[1].SetLocked(true);

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 2000000);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessWatchAd);
    }

    public void SuccessWatchAd_Portion()
    {
        GameStateManager.instance.DailyAdsReward2 = true;

        shopContents[6].SetLocked(true);

        playerDataBase.Portion1 += 2;
        playerDataBase.Portion2 += 2;
        playerDataBase.Portion3 += 2;
        playerDataBase.Portion4 += 2;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }

    IEnumerator DailyShopTimer()
    {
        if (dailyShopCountText.gameObject.activeInHierarchy)
        {
            System.DateTime f = System.DateTime.Now;
            System.DateTime g = System.DateTime.Today.AddDays(1);
            System.TimeSpan h = g - f;

            dailyShopCountText.text = localization_Reset + " : " + h.Hours.ToString("D2") + localization_Hours + " " + h.Minutes.ToString("D2") + localization_Minutes;

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                isTimer = false;
                ResetManager.instance.Initialize();
                yield break;
            }
        }

        yield return waitForSeconds;
        StartCoroutine(DailyShopTimer());
    }

    public void OpenSpeicalShopView()
    {
        if (!speicalShopView.activeInHierarchy)
        {
            speicalShopView.SetActive(true);

            if (speicalIndex == -1)
            {
                ChangeSpeicalTopToggle(0);
            }

            FirebaseAnalytics.LogEvent("OpenSpeicalShop");
        }
        else
        {
            speicalShopView.SetActive(false);
        }
    }

    public void ChangeSpeicalTopToggle(int number)
    {
        if (speicalIndex == number) return;

        speicalIndex = number;

        for (int i = 0; i < speicalTopMenuImgArray.Length; i++)
        {
            speicalTopMenuImgArray[i].sprite = speicalTopMenuSpriteArray[0];
            //speicalShopArray[i].gameObject.SetActive(false);
        }

        speicalTopMenuImgArray[number].sprite = speicalTopMenuSpriteArray[1];
        //speicalShopArray[number].gameObject.SetActive(true);

        mainTruck.SetActive(false);
        mainAnimal.SetActive(false);

        buyRmTruck.SetActive(false);
        buyRmAnimal.SetActive(false);

        switch (number)
        {
            case 0:
                TruckInitialize();
                break;
            case 1:
                AnimalInitialize();
                break;
        }
    }

    void TruckInitialize()
    {
        mainTruck.SetActive(true);
        buyRmTruck.SetActive(true);

        for (int i = 0; i < shopTruckArray.Length; i++)
        {
            shopTruckArray[i].gameObject.SetActive(false);
        }

        shopTruckArray[truckIndex].gameObject.SetActive(true);
        shopTruckArray[truckIndex].transform.localRotation = Quaternion.Euler(0, 220, 0);

        truckInfo = truckDataBase.GetTruckInfo(TruckType.Bread + truckIndex);

        titleText.localizationName = (TruckType.Bread + truckIndex).ToString() + "Truck";

        if (truckIndex == 0)
        {
            effectText.localizationName = "None";
            effectText.plusText = "";
            passiveText.text = "";
        }
        else
        {
            effectText.localizationName = truckInfo.passiveEffect.ToString();
            effectText.plusText = " : +" + truckInfo.effectNumber.ToString() + "%";
            passiveText.text = LocalizationManager.instance.GetString("Passive") + " : " + LocalizationManager.instance.GetString("NeedPrice") + " -1%";
        }

        infoText.localizationName = (TruckType.Bread + truckIndex) + "TruckInfo";

        titleText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        priceText.text = MoneyUnitString.ToCurrencyString(truckInfo.price);

        hold = false;
        buy = false;

        switch (truckInfo.truckType)
        {
            case TruckType.Bread:
                hold = true;
                break;
            case TruckType.Chips:
                if (playerDataBase.ChipsTruck >= 1)
                {
                    hold = true;
                }

                buy = true;

                break;
            case TruckType.Donut:
                if (playerDataBase.DonutTruck >= 1)
                {
                    hold = true;
                }

                if(playerDataBase.ChipsTruck >= 1)
                {
                    buy = true;
                }

                break;
            case TruckType.Hamburger:
                if (playerDataBase.HamburgerTruck >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.DonutTruck >= 1)
                {
                    buy = true;
                }

                break;
            case TruckType.Hotdog:
                if (playerDataBase.HotdogTruck >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.HamburgerTruck >= 1)
                {
                    buy = true;
                }

                break;
            case TruckType.Icecream:
                if (playerDataBase.IcecreamTruck >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.HotdogTruck >= 1)
                {
                    buy = true;
                }

                break;
            case TruckType.Lemonade:
                if (playerDataBase.LemonadeTruck >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.IcecreamTruck >= 1)
                {
                    buy = true;
                }

                break;
            case TruckType.Noodles:
                if (playerDataBase.NoodlesTruck >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.LemonadeTruck >= 1)
                {
                    buy = true;
                }

                break;
            case TruckType.Pizza:
                if (playerDataBase.PizzaTruck >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.NoodlesTruck >= 1)
                {
                    buy = true;
                }

                break;
            case TruckType.Sushi:
                if (playerDataBase.SushiTruck >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.PizzaTruck >= 1)
                {
                    buy = true;
                }

                break;
        }

        if (hold)
        {
            selectObj.SetActive(true);
            buyToCoinButton.SetActive(false);

            for (int i = 0; i < buyRmObj.Length; i++)
            {
                buyRmObj[i].SetActive(false);
            }

            if (GameStateManager.instance.TruckType.Equals(truckInfo.truckType))
            {
                selectText.text = LocalizationManager.instance.GetString("Selected");
            }
            else
            {
                selectText.text = LocalizationManager.instance.GetString("Select");
            }
        }
        else
        {
            selectObj.SetActive(false);
            buyToCoinButton.SetActive(true);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
            }

            if (truckIndex > 0)
            {
                for (int i = 0; i < buyRmObj.Length; i++)
                {
                    buyRmObj[i].SetActive(false);
                }

                buyRmObj[truckIndex - 1].SetActive(true);
                buyRmText[truckIndex - 1].localizationName = truckInfo.truckType + "_Price";
                buyRmText[truckIndex - 1].ReLoad();
            }
        }
    }

    void AnimalInitialize()
    {
        mainAnimal.SetActive(true);
        buyRmAnimal.SetActive(true);

        for (int i = 0; i < shopAnimalArray.Length; i++)
        {
            shopAnimalArray[i].gameObject.SetActive(false);
        }

        shopAnimalArray[animalIndex].gameObject.SetActive(true);
        shopAnimalArray[animalIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);

        animalInfo = animalDataBase.GetAnimalInfo(AnimalType.Colobus + animalIndex);

        titleText.localizationName = (AnimalType.Colobus + animalIndex).ToString();

        if (animalIndex == 0)
        {
            effectText.localizationName = "None";
            effectText.plusText = "";
            passiveText.text = "";
        }
        else
        {
            effectText.localizationName = animalInfo.passiveEffect.ToString();
            effectText.plusText = " : +" + animalInfo.effectNumber.ToString() + "%";
            passiveText.text = LocalizationManager.instance.GetString("Passive") + " : " + LocalizationManager.instance.GetString("NowPrice") + " +1%";
        }

        infoText.localizationName = (AnimalType.Colobus + animalIndex) + "Info";

        titleText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        priceText.text = MoneyUnitString.ToCurrencyString(animalInfo.price);

        hold = false;
        buy = false;

        switch (animalInfo.animalType)
        {
            case AnimalType.Colobus:
                hold = true;

                break;
            case AnimalType.Gecko:
                if (playerDataBase.GeckoAnimal >= 1)
                {
                    hold = true;
                }

                buy = true;

                break;
            case AnimalType.Herring:
                if (playerDataBase.HerringAnimal >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.GeckoAnimal >= 1)
                {
                    buy = true;
                }

                break;
            case AnimalType.Muskrat:
                if (playerDataBase.MuskratAnimal >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.HerringAnimal >= 1)
                {
                    buy = true;
                }

                break;
            case AnimalType.Pudu:
                if (playerDataBase.PuduAnimal >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.MuskratAnimal >= 1)
                {
                    buy = true;
                }

                break;
            case AnimalType.Sparrow:
                if (playerDataBase.SparrowAnimal >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.PuduAnimal >= 1)
                {
                    buy = true;
                }

                break;
            case AnimalType.Squid:
                if (playerDataBase.SquidAnimal >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.SparrowAnimal >= 1)
                {
                    buy = true;
                }

                break;
            case AnimalType.Taipan:
                if (playerDataBase.TaipanAnimal >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.SquidAnimal >= 1)
                {
                    buy = true;
                }

                break;
        }

        if (hold)
        {
            selectObj.SetActive(true);
            buyToCoinButton.SetActive(false);

            for (int i = 0; i < buyRmAnimalObj.Length; i++)
            {
                buyRmAnimalObj[i].SetActive(false);
            }

            if (GameStateManager.instance.AnimalType.Equals(animalInfo.animalType))
            {
                selectText.text = LocalizationManager.instance.GetString("Selected");
            }
            else
            {
                selectText.text = LocalizationManager.instance.GetString("Select");
            }
        }
        else
        {
            selectObj.SetActive(false);
            buyToCoinButton.SetActive(true);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
            }

            if (animalIndex > 0)
            {
                for (int i = 0; i < buyRmAnimalObj.Length; i++)
                {
                    buyRmAnimalObj[i].SetActive(false);
                }

                buyRmAnimalObj[animalIndex - 1].SetActive(true);
                buyRmAnimalText[animalIndex - 1].localizationName = animalInfo.animalType + "_Price";
                buyRmAnimalText[animalIndex - 1].ReLoad();
            }
        }
    }

    public void RightButton()
    {

        switch(speicalIndex)
        {
            case 0:
                if (truckIndex + 1 < shopTruckArray.Length)
                {
                    truckIndex += 1;

                    TruckInitialize();
                }
                break;
            case 1:
                if (animalIndex + 1 < shopAnimalArray.Length)
                {
                    animalIndex += 1;

                    AnimalInitialize();
                }
                break;
        }
    }

    public void LeftButton()
    {
        switch (speicalIndex)
        {
            case 0:
                if (truckIndex - 1 >= 0)
                {
                    truckIndex -= 1;

                    TruckInitialize();
                }
                break;
            case 1:
                if (animalIndex - 1 >= 0)
                {
                    animalIndex -= 1;

                    AnimalInitialize();
                }
                break;
        }
    }

    public void Selected()
    {
        switch(speicalIndex)
        {
            case 0:
                if (GameStateManager.instance.TruckType == truckInfo.truckType)
                {
                    return;
                }

                GameStateManager.instance.TruckType = truckInfo.truckType;

                for (int i = 0; i < mainTruckArray.Length; i++)
                {
                    mainTruckArray[i].SetActive(false);
                }

                mainTruckArray[(int)GameStateManager.instance.TruckType].SetActive(true);

                NotionManager.instance.UseNotion(NotionType.ChangeTruckNotion);
                break;
            case 1:
                if (GameStateManager.instance.AnimalType == animalInfo.animalType)
                {
                    return;
                }

                GameStateManager.instance.AnimalType = animalInfo.animalType;

                for (int i = 0; i < mainAnimalArray.Length; i++)
                {
                    mainAnimalArray[i].SetActive(false);
                }

                mainAnimalArray[(int)GameStateManager.instance.AnimalType].SetActive(true);

                NotionManager.instance.UseNotion(NotionType.ChangeAnimalNotion);
                break;
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);

        selectText.text = LocalizationManager.instance.GetString("Selected");
    }

    public void BuyTruckToCoin()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if(!buy)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NotPurchaseNotion);
            return;
        }

        switch(speicalIndex)
        {
            case 0:
                if (playerDataBase.Coin >= truckInfo.price)
                {
                    itemList.Clear();
                    itemList.Add(truckInfo.truckType.ToString());

                    PlayfabManager.instance.GrantItemToUser("Truck", itemList);
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, truckInfo.price);

                    switch (truckInfo.truckType)
                    {
                        case TruckType.Bread:
                            break;
                        case TruckType.Chips:
                            playerDataBase.ChipsTruck = 1;
                            break;
                        case TruckType.Donut:
                            playerDataBase.DonutTruck = 1;
                            break;
                        case TruckType.Hamburger:
                            playerDataBase.HamburgerTruck = 1;
                            break;
                        case TruckType.Hotdog:
                            playerDataBase.HotdogTruck = 1;
                            break;
                        case TruckType.Icecream:
                            playerDataBase.IcecreamTruck = 1;
                            break;
                        case TruckType.Lemonade:
                            playerDataBase.LemonadeTruck = 1;
                            break;
                        case TruckType.Noodles:
                            playerDataBase.NoodlesTruck = 1;
                            break;
                        case TruckType.Pizza:
                            playerDataBase.PizzaTruck = 1;
                            break;
                        case TruckType.Sushi:
                            playerDataBase.SushiTruck = 1;
                            break;
                    }

                    selectObj.SetActive(true);
                    buyToCoinButton.SetActive(false);
                    selectText.text = LocalizationManager.instance.GetString("Select");

                    for (int i = 0; i < buyRmObj.Length; i++)
                    {
                        buyRmObj[i].SetActive(false);
                    }

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCoin);
                }
                break;
            case 1:
                if (playerDataBase.Coin >= animalInfo.price)
                {
                    itemList.Clear();
                    itemList.Add(animalInfo.animalType.ToString());

                    PlayfabManager.instance.GrantItemToUser("Animal", itemList);
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, animalInfo.price);

                    switch (animalInfo.animalType)
                    {
                        case AnimalType.Colobus:
                            break;
                        case AnimalType.Gecko:
                            playerDataBase.GeckoAnimal = 1;
                            break;
                        case AnimalType.Herring:
                            playerDataBase.HerringAnimal = 1;
                            break;
                        case AnimalType.Muskrat:
                            playerDataBase.MuskratAnimal = 1;
                            break;
                        case AnimalType.Pudu:
                            playerDataBase.PuduAnimal = 1;
                            break;
                        case AnimalType.Sparrow:
                            playerDataBase.SparrowAnimal = 1;
                            break;
                        case AnimalType.Squid:
                            playerDataBase.SquidAnimal = 1;
                            break;
                        case AnimalType.Taipan:
                            playerDataBase.TaipanAnimal = 1;
                            break;
                    }

                    selectObj.SetActive(true);
                    buyToCoinButton.SetActive(false);
                    selectText.text = LocalizationManager.instance.GetString("Select");

                    for (int i = 0; i < buyRmAnimalObj.Length; i++)
                    {
                        buyRmAnimalObj[i].SetActive(false);
                    }

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCoin);
                }
                break;
        }
    }

    public void BuyTruckToRm(int number)
    {
        TruckType truckType = TruckType.Bread + number + 1;

        itemList.Clear();
        itemList.Add(truckType.ToString());

        PlayfabManager.instance.GrantItemToUser("Truck", itemList);

        switch (truckType)
        {
            case TruckType.Bread:
                break;
            case TruckType.Chips:
                playerDataBase.ChipsTruck = 1;
                break;
            case TruckType.Donut:
                playerDataBase.DonutTruck = 1;
                break;
            case TruckType.Hamburger:
                playerDataBase.HamburgerTruck = 1;
                break;
            case TruckType.Hotdog:
                playerDataBase.HotdogTruck = 1;
                break;
            case TruckType.Icecream:
                playerDataBase.IcecreamTruck = 1;
                break;
            case TruckType.Lemonade:
                playerDataBase.LemonadeTruck = 1;
                break;
            case TruckType.Noodles:
                playerDataBase.NoodlesTruck = 1;
                break;
            case TruckType.Pizza:
                playerDataBase.PizzaTruck = 1;
                break;
            case TruckType.Sushi:
                playerDataBase.SushiTruck = 1;
                break;
        }

        Invoke("RmDelay", 0.5f);

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);
    }

    public void BuyAnimalToRm(int number)
    {
        AnimalType animalType = AnimalType.Colobus + number + 1;

        itemList.Clear();
        itemList.Add(animalType.ToString());

        PlayfabManager.instance.GrantItemToUser("Animal", itemList);

        switch (animalType)
        {
            case AnimalType.Colobus:
                break;
            case AnimalType.Gecko:
                playerDataBase.GeckoAnimal = 1;
                break;
            case AnimalType.Herring:
                playerDataBase.HerringAnimal = 1;
                break;
            case AnimalType.Muskrat:
                playerDataBase.MuskratAnimal = 1;
                break;
            case AnimalType.Pudu:
                playerDataBase.PuduAnimal = 1;
                break;
            case AnimalType.Sparrow:
                playerDataBase.SparrowAnimal = 1;
                break;
            case AnimalType.Squid:
                playerDataBase.SquidAnimal = 1;
                break;
            case AnimalType.Taipan:
                playerDataBase.TaipanAnimal = 1;
                break;
        }

        Invoke("RmDelay", 0.5f);

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);
    }

    void RmDelay()
    {
        for (int i = 0; i < buyRmObj.Length; i++)
        {
            buyRmObj[i].SetActive(false);
        }

        for (int i = 0; i < buyRmAnimalObj.Length; i++)
        {
            buyRmAnimalObj[i].SetActive(false);
        }

        selectObj.SetActive(true);
        buyToCoinButton.SetActive(false);
        selectText.text = LocalizationManager.instance.GetString("Select");
    }

    public void BuyPurchase(int number)
    {
        switch (number)
        {
            case 0:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 1000000);

                SoundManager.instance.PlaySFX(GameSfxType.GetMoney);
                NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                break;
            case 1:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 5500000);

                SoundManager.instance.PlaySFX(GameSfxType.GetMoney);
                NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                break;
            case 2:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 10000000);

                SoundManager.instance.PlaySFX(GameSfxType.GetMoney);
                NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                break;
            case 3:
                PlayfabManager.instance.PurchaseRemoveAd();

                Invoke("ContentDelay", 0.5f);

                SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                break;
            case 4:
                playerDataBase.Portion1 += 10;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);

                playerDataBase.Portion2 += 10;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);

                playerDataBase.Portion3 += 10;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);

                playerDataBase.Portion4 += 10;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);


                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                break;
            case 5:
                playerDataBase.Portion1 += 25;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);

                playerDataBase.Portion2 += 25;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);

                playerDataBase.Portion3 += 25;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);

                playerDataBase.Portion4 += 25;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);


                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                break;
            case 6:
                playerDataBase.Portion1 += 50;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);

                playerDataBase.Portion2 += 50;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);

                playerDataBase.Portion3 += 50;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);

                playerDataBase.Portion4 += 50;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);


                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                break;
            case 7:
                PlayfabManager.instance.PurchaseGoldX2();

                Invoke("ContentDelay2", 0.5f);

                SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                break;
        }
    }

    void ContentDelay()
    {
        shopContents[7].gameObject.SetActive(false);
    }

    void ContentDelay2()
    {
        shopContents[12].gameObject.SetActive(false);
    }

    public void Failed()
    {
        SoundManager.instance.PlaySFX(GameSfxType.Wrong);

        NotionManager.instance.UseNotion(NotionType.CancelPurchase);
    }
}
