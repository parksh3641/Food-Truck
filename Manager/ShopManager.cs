using Firebase.Analytics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    public GameObject shopView;
    public GameObject speicalShopView;

    public GameObject shopAlarm;
    public GameObject shopIngameAlarm;


    public GameObject martAlarm;
    public GameObject martIngameAlarm;
    public GameObject[] packageAlarm;

    public GameObject goldx2;
    public GameObject removeAds;
    public GameObject superOffline;
    public GameObject autoUpgrade;
    public GameObject autoPresent;

    public GameObject packageThanks;

    public GameObject leftButton;
    public GameObject rightButton;

    public Text rankPointText;

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
    public RectTransform[] shopRectTransform;

    [Space]
    [Title("Content")]
    public ShopContent[] shopContents;
    public PackageContent[] packageContents;

    public GameObject package;
    public PackageContent packageContent;
    public GameObject packageBuyIcon;


    [Space]
    public GameObject mainAnimal;
    public GameObject[] mainAnimalArray;
    public GameObject[] shopAnimalArray;

    public GameObject mainTruck;
    public GameObject[] mainTruckArray;
    public GameObject[] shopTruckArray;

    public GameObject mainCharacter;
    public GameObject[] mainCharacterArray;
    public GameObject[] shopCharacterArray;

    public GameObject mainButterfly;
    public GameObject[] mainButterflyArray;
    public GameObject[] shopButterflyArray;

    public GameObject mainTotems;
    public GameObject[] mainTotemsArray;
    public GameObject[] shopTotemsArray;

    public GameObject mainFlower;
    public GameObject[] mainFlowerArray;
    public GameObject[] shopFlowerArray;

    public GameObject buyButton;
    public GameObject buySpeical;
    public GameObject selectObj;
    public GameObject selectCheckMarkObj;

    public Text selectText;
    public Text priceText;
    public Text crystalText;

    public GameObject crystalButton;

    public LocalizationContent titleText;
    public LocalizationContent nameText;
    public LocalizationContent effectText;
    public Text passiveText;
    public LocalizationContent infoText;
    public GameObject yummyButton;

    public Text dailyShopCountText;

    [Space]
    [Title("ChangeMoney")]
    public GameObject changeMoneyView;
    public ReceiveContent changeMoneyReceiveContent;
    public Text changeMoneyText;

    private int changeMoneyIndex = 0;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    private int index = -1;
    private int speicalIndex = -1;

    private int characterIndex = 0;
    private int truckIndex = 0;
    private int animalIndex = 0;
    private int butterflyIndex = 0;
    private int totemsIndex = 0;
    private int flowerIndex = 0;

    private int needCrystal = 0;

    bool hold = false;
    bool buy = false;
    bool isDelay = false;
    bool isTimer = false;
    bool isPackageDelay = false;

    private int price_Gold = 0;
    private int price_Crystal = 0;
    private int exchagne = 100000;

    public GuideMissionManager guideMissionManager;

    CharacterInfo characterInfo = new CharacterInfo();
    TruckInfo truckInfo = new TruckInfo();
    AnimalInfo animalInfo = new AnimalInfo();
    ButterflyInfo butterflyInfo = new ButterflyInfo();
    TotemsInfo totemsInfo = new TotemsInfo();
    FlowerInfo flowerInfo = new FlowerInfo();

    List<string> itemList = new List<string>();

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    DateTime f, g;
    TimeSpan h;

    Vector3 normalPos = new Vector3(-40, 1.1f, -3.5f);
    Vector3 beforePos = new Vector3(-44, 1.1f, 2);
    Vector3 afterPos = new Vector3(-36, 1.1f, 2);

    Vector3 normalPos_Truck = new Vector3(-39, 1, -1.5f);
    Vector3 beforePos_Truck = new Vector3(-45, 1, 3);
    Vector3 afterPos_Truck = new Vector3(-35, 1, 3);

    Vector3 normalPos_Butterfly = new Vector3(-40, 2.8f, -3.5f);
    Vector3 beforePos_Butterfly = new Vector3(-45, 2.8f, 5);
    Vector3 afterPos_Butterfly = new Vector3(-35, 2.8f, 5);

    DateTime time;
    DateTime serverTime;
    TimeSpan timeSpan;

    PlayerDataBase playerDataBase;
    CharacterDataBase characterDataBase;
    TruckDataBase truckDataBase;
    AnimalDataBase animalDataBase;
    ButterflyDataBase butterflyDataBase;
    TotemsDataBase totemsDataBase;
    FlowerDataBase flowerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        if (characterDataBase == null) characterDataBase = Resources.Load("CharacterDataBase") as CharacterDataBase;
        if (truckDataBase == null) truckDataBase = Resources.Load("TruckDataBase") as TruckDataBase;
        if (animalDataBase == null) animalDataBase = Resources.Load("AnimalDataBase") as AnimalDataBase;
        if (butterflyDataBase == null) butterflyDataBase = Resources.Load("ButterflyDataBase") as ButterflyDataBase;
        if (totemsDataBase == null) totemsDataBase = Resources.Load("TotemsDataBase") as TotemsDataBase;
        if (flowerDataBase == null) flowerDataBase = Resources.Load("FlowerDataBase") as FlowerDataBase;

        shopView.SetActive(false);
        speicalShopView.SetActive(false);
        changeMoneyView.SetActive(false);

        dailyShopCountText.text = "";

        package.SetActive(false);

        shopAlarm.SetActive(true);
        shopIngameAlarm.SetActive(true);

        martAlarm.SetActive(true);
        martIngameAlarm.SetActive(true);

        packageBuyIcon.SetActive(false);

        for (int i = 0; i < packageAlarm.Length; i ++)
        {
            packageAlarm[i].SetActive(true);
        }

        for (int i = 0; i < shopArray.Length; i++)
        {
            shopArray[i].SetActive(false);
        }

        for (int i = 0; i < mainCharacterArray.Length; i++)
        {
            mainCharacterArray[i].SetActive(false);
        }

        for (int i = 0; i < mainTruckArray.Length; i++)
        {
            mainTruckArray[i].SetActive(false);
        }

        for (int i = 0; i < mainAnimalArray.Length; i++)
        {
            mainAnimalArray[i].SetActive(false);
        }

        for (int i = 0; i < mainButterflyArray.Length; i++)
        {
            mainButterflyArray[i].SetActive(false);
        }

        for (int i = 0; i < mainTotemsArray.Length; i++)
        {
            mainTotemsArray[i].SetActive(false);
        }

        for (int i = 0; i < mainFlowerArray.Length; i++)
        {
            mainFlowerArray[i].SetActive(false);
        }
    }

    private void Start()
    {
        for(int i = 0; i < shopRectTransform.Length; i ++)
        {
            shopRectTransform[i].anchoredPosition = new Vector2(0, -9999);
        }
    }

    public void Initialize()
    {
        if (GameStateManager.instance.YoutubeVideo)
        {
            int number = Random.Range(0, Enum.GetValues(typeof(CharacterType)).Length - 1);
            int number2 = Random.Range(0, Enum.GetValues(typeof(TruckType)).Length - 1);
            int number3 = Random.Range(0, Enum.GetValues(typeof(AnimalType)).Length - 1);
            int number4 = Random.Range(0, Enum.GetValues(typeof(ButterflyType)).Length - 1);
            int number5 = Random.Range(0, Enum.GetValues(typeof(TotemsType)).Length - 1);

            GameStateManager.instance.CharacterType = CharacterType.Character1 + number;
            GameStateManager.instance.TruckType = TruckType.Bread + number2;
            GameStateManager.instance.AnimalType = AnimalType.Colobus + number3;
            GameStateManager.instance.ButterflyType = ButterflyType.Butterfly1 + number4;
            GameStateManager.instance.TotemsType = TotemsType.Totems1 + number5;

            mainCharacterArray[number].SetActive(true);
            mainTruckArray[number2].SetActive(true);
            mainAnimalArray[number3].SetActive(true);
            mainButterflyArray[number4].SetActive(true);
            mainTotemsArray[number5].SetActive(true);

            GameStateManager.instance.IslandType = IslandType.Island1 + Random.Range(1, 4);
            GameStateManager.instance.FoodType = FoodType.Food1 + Random.Range(0, 7);
            GameStateManager.instance.CandyType = CandyType.Candy1 + Random.Range(0, 9);
            GameStateManager.instance.JapaneseFoodType = JapaneseFoodType.JapaneseFood1 + Random.Range(0, 7);
            GameStateManager.instance.DessertType = DessertType.Dessert1 + Random.Range(0, 9);

            GameStateManager.instance.FeverCount = 280;

            Debug.LogError("유튜브 업로드 세팅");
        }
        else
        {
            mainCharacterArray[(int)GameStateManager.instance.CharacterType].SetActive(true);
            mainTruckArray[(int)GameStateManager.instance.TruckType].SetActive(true);
            mainAnimalArray[(int)GameStateManager.instance.AnimalType].SetActive(true);
            mainButterflyArray[(int)GameStateManager.instance.ButterflyType].SetActive(true);
            mainTotemsArray[(int)GameStateManager.instance.TotemsType].SetActive(true);
            //mainFlowerArray[(int)GameStateManager.instance.FlowerType].SetActive(true);
        }

        if (!playerDataBase.Package5)
        {
            if (playerDataBase.FirstDate.Length < 1)
            {
                playerDataBase.FirstDate = "1" + DateTime.Now.ToString("MMddHHmm");
                playerDataBase.FirstServerDate = "1" + DateTime.Now.AddDays(3).ToString("MMddHHmm");

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("FirstDate", int.Parse(playerDataBase.FirstDate));
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("FirstServerDate", int.Parse(playerDataBase.FirstServerDate));

                Debug.Log("한정 패키지 구매 날짜 설정");
            }

            if (playerDataBase.InGameTutorial == 1)
            {
                OpenLimitPackage();
            }
        }
    }

    public void SetAlarm()
    {
        martAlarm.SetActive(true);
        martIngameAlarm.SetActive(true);
    }

    public void OpenShopCoinView()
    {
        shopView.SetActive(false);

        OpenShopView();

        if (shopView.activeInHierarchy)
        {
            ChangeTopToggle(3);
        }
    }

    public void OpenShopCrystalView()
    {
        shopView.SetActive(false);

        OpenShopView();

        if (shopView.activeInHierarchy)
        {
            ChangeTopToggle(3);
        }
    }

    public void OpenPackageView()
    {
        shopView.SetActive(false);

        OpenShopView();

        if (shopView.activeInHierarchy)
        {
            ChangeTopToggle(1);
        }
    }

    public void OpenShopView()
    {
        if(!shopView.activeInHierarchy)
        {
            shopView.SetActive(true);

            GameManager.instance.RenewalVC();

            martAlarm.SetActive(false);
            martIngameAlarm.SetActive(false);

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            dailyShopCountText.text = "";
            f = DateTime.Now;
            g = DateTime.Today.AddDays(1);
            StartCoroutine(TimerCoroution());

            if (index == - 1)
            {
                ChangeTopToggle(0);
            }

            rankPointText.text = playerDataBase.RankPoint.ToString();

            FirebaseAnalytics.LogEvent("Open_Shop");
        }
        else
        {
            StopAllCoroutines();

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
                FirebaseAnalytics.LogEvent("Open_Shop_Daily");

                shopContents[0].Initialize(ItemType.DailyReward, BuyType.Free, this);
                shopContents[1].Initialize(ItemType.AdReward_Gold, BuyType.Ad, this);
                //shopContents[2].Initialize(ItemType.DefDestroyTicket, BuyType.Crystal, this);
                shopContents[2].gameObject.SetActive(false);
                shopContents[6].Initialize(ItemType.AdReward_Portion, BuyType.Ad, this);
                shopContents[7].Initialize(ItemType.RemoveAds, BuyType.Rm, this);
                shopContents[11].Initialize(ItemType.DailyReward_Portion, BuyType.Free, this);
                shopContents[12].Initialize(ItemType.GoldX2, BuyType.Rm, this);
                shopContents[24].Initialize(ItemType.DefDestroyTicketSlices, BuyType.Exchange, this);
                shopContents[25].Initialize(ItemType.DefDestroyTicketPiece, BuyType.Free, this);
                shopContents[32].Initialize(ItemType.SuperOffline, BuyType.Rm, this);
                shopContents[34].Initialize(ItemType.AutoUpgrade, BuyType.Rm, this);
                shopContents[35].Initialize(ItemType.AutoPresent, BuyType.Rm, this);

                if (playerDataBase.DailyReward == 0)
                {
                    shopContents[0].SetLocked(false);
                }
                else
                {
                    shopContents[0].SetLocked(true);
                }

                if (playerDataBase.DailyReward_Portion == 0)
                {
                    shopContents[11].SetLocked(false);
                }
                else
                {
                    shopContents[11].SetLocked(true);
                }

                if (playerDataBase.DailyReward_DefTicket == 0)
                {
                    shopContents[25].SetLocked(false);
                }
                else
                {
                    shopContents[25].SetLocked(true);
                }

                if (playerDataBase.DailyAdsReward == 0)
                {
                    shopContents[1].SetLocked(false);
                }
                else
                {
                    shopContents[1].SetLocked(true);
                }

                if (playerDataBase.DailyAdsReward2 == 0)
                {
                    shopContents[6].SetLocked(false);
                }
                else
                {
                    shopContents[6].SetLocked(true);
                }

                if (playerDataBase.RemoveAds)
                {
                    shopContents[7].gameObject.SetActive(false);
                }

                if(playerDataBase.GoldX2)
                {
                    shopContents[12].gameObject.SetActive(false);
                }

                if(playerDataBase.SuperOffline)
                {
                    shopContents[32].gameObject.SetActive(false);
                }

                if (playerDataBase.AutoUpgrade)
                {
                    shopContents[34].gameObject.SetActive(false);
                }

                if (playerDataBase.AutoPresent)
                {
                    shopContents[35].gameObject.SetActive(false);
                }

                break;
            case 1:
                FirebaseAnalytics.LogEvent("Open_Shop_Package");

                packageThanks.SetActive(false);

                packageAlarm[0].SetActive(false);

                if (packageContents[0].gameObject.activeSelf)
                {
                    packageContents[0].Initialize(PackageType.Package5, this);
                }

                if (playerDataBase.Package1)
                {
                    packageContents[1].gameObject.SetActive(false);
                }
                else
                {
                    packageContents[1].Initialize(PackageType.Package1, this);
                }

                if (playerDataBase.Package2)
                {
                    packageContents[2].gameObject.SetActive(false);
                }
                else
                {
                    packageContents[2].Initialize(PackageType.Package2, this);
                }

                if (playerDataBase.Package3)
                {
                    packageContents[3].gameObject.SetActive(false);
                }
                else
                {
                    packageContents[3].Initialize(PackageType.Package3, this);
                }

                if (playerDataBase.Package4)
                {
                    packageContents[4].gameObject.SetActive(false);
                }
                else
                {
                    packageContents[4].Initialize(PackageType.Package4, this);
                }

                if (playerDataBase.Package6)
                {
                    packageContents[5].gameObject.SetActive(false);
                }
                else
                {
                    packageContents[5].Initialize(PackageType.Package6, this);
                }

                //if (playerDataBase.Package1 && playerDataBase.Package2 && playerDataBase.Package3 && playerDataBase.Package4
                //    && playerDataBase.Package5 && playerDataBase.Package6)
                //{
                //    packageThanks.SetActive(true);
                //}

                break;
            case 2:
                FirebaseAnalytics.LogEvent("Open_Shop_Item");

                packageAlarm[3].SetActive(false);

                shopContents[8].Initialize(ItemType.PortionSet1, BuyType.Crystal, this);
                shopContents[9].Initialize(ItemType.PortionSet2, BuyType.Crystal, this);
                shopContents[10].Initialize(ItemType.PortionSet3, BuyType.Crystal, this);

                shopContents[26].Initialize(ItemType.BuffTicketSet1, BuyType.Crystal, this);
                shopContents[27].Initialize(ItemType.BuffTicketSet2, BuyType.Crystal, this);
                shopContents[28].Initialize(ItemType.BuffTicketSet3, BuyType.Crystal, this);

                shopContents[29].Initialize(ItemType.DefTicketSet1, BuyType.Crystal, this);
                shopContents[30].Initialize(ItemType.DefTicketSet2, BuyType.Crystal, this);
                shopContents[31].Initialize(ItemType.DefTicketSet3, BuyType.Crystal, this);

                break;
            case 3:
                FirebaseAnalytics.LogEvent("Open_Shop_Crystal");

                packageAlarm[1].SetActive(false);

                shopContents[33].Initialize(ItemType.AdReward_Crystal, BuyType.Ad, this);

                shopContents[13].Initialize(ItemType.CrystalShop1, BuyType.Rm, this);
                shopContents[14].Initialize(ItemType.CrystalShop2, BuyType.Rm, this);
                shopContents[15].Initialize(ItemType.CrystalShop3, BuyType.Rm, this);
                shopContents[16].Initialize(ItemType.CrystalShop4, BuyType.Rm, this);
                shopContents[17].Initialize(ItemType.CrystalShop5, BuyType.Rm, this);
                shopContents[18].Initialize(ItemType.CrystalShop6, BuyType.Rm, this);

                if (playerDataBase.DailyReward_Crystal == 0)
                {
                    shopContents[33].SetLocked(false);
                }
                else
                {
                    shopContents[33].SetLocked(true);
                }

                shopContents[3].Initialize(ItemType.GoldShop1, BuyType.Crystal, this);
                shopContents[4].Initialize(ItemType.GoldShop2, BuyType.Crystal, this);
                shopContents[5].Initialize(ItemType.GoldShop3, BuyType.Crystal, this);
                shopContents[3].gameObject.SetActive(false);
                shopContents[4].gameObject.SetActive(false);
                shopContents[5].gameObject.SetActive(false);
                break;
            case 4:
                FirebaseAnalytics.LogEvent("Open_Shop_Ranking");

                packageAlarm[2].SetActive(false);

                shopContents[19].Initialize(ItemType.Portion1, BuyType.RankPoint, this);
                shopContents[20].Initialize(ItemType.Portion2, BuyType.RankPoint, this);
                shopContents[21].Initialize(ItemType.Portion3, BuyType.RankPoint, this);
                shopContents[22].Initialize(ItemType.Portion4, BuyType.RankPoint, this);
                shopContents[23].Initialize(ItemType.Portion5, BuyType.RankPoint, this);
                shopContents[36].Initialize(ItemType.BuffTicket, BuyType.RankPoint, this);
                shopContents[37].Initialize(ItemType.SkillTicket, BuyType.RankPoint, this);
                shopContents[38].Initialize(ItemType.RepairTicket, BuyType.RankPoint, this);
                shopContents[39].Initialize(ItemType.RepairTicket10, BuyType.RankPoint, this);
                shopContents[40].Initialize(ItemType.DefDestroyTicket, BuyType.RankPoint, this);


                break;
        }
    }

    public void BuyItem(ItemType item)
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
                if (playerDataBase.DailyReward == 1) return;

                playerDataBase.DailyReward = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyReward", playerDataBase.DailyReward);

                shopContents[0].SetLocked(true);

                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessReward);

                if (playerDataBase.Dessert1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(1000000);
                    return;
                }

                if (playerDataBase.JapaneseFood1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(500000);
                    return;
                }

                if (playerDataBase.Candy1MaxValue > 0)
                {
                    PlayfabManager.instance.UpdateAddGold(300000);
                    return;
                }

                PlayfabManager.instance.UpdateAddGold(100000);

                break;
            case ItemType.AdReward_Gold:
                GoogleAdsManager.instance.admobReward_Gold.ShowAd(0);

                break;
            case ItemType.DefDestroyTicket:
                if (playerDataBase.RankPoint >= 300)
                {
                    playerDataBase.RankPoint -= 300;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetDefTickets(1);

                    shopContents[40].Initialize(ItemType.DefDestroyTicket, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.GoldShop1:
                changeMoneyIndex = 0;
                OpenChangeMoneyView();
                changeMoneyReceiveContent.Initialize(RewardType.Gold, 1000000);
                changeMoneyText.text = MoneyUnitString.ToCurrencyString(120);

                break;
            case ItemType.GoldShop2:
                changeMoneyIndex = 1;
                OpenChangeMoneyView();
                changeMoneyReceiveContent.Initialize(RewardType.Gold2, 10000000);
                changeMoneyText.text = MoneyUnitString.ToCurrencyString(1200);

                break;
            case ItemType.GoldShop3:
                changeMoneyIndex = 2;
                OpenChangeMoneyView();
                changeMoneyReceiveContent.Initialize(RewardType.Gold3, 100000000);
                changeMoneyText.text = MoneyUnitString.ToCurrencyString(12000);

                break;
            case ItemType.AdReward_Portion:
                GoogleAdsManager.instance.admobReward_Portion.ShowAd(1);
                break;
            case ItemType.RemoveAds:
                break;
            case ItemType.PortionSet1:
                if (playerDataBase.Crystal >= 600)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 600);

                    PortionManager.instance.GetAllPortion(10);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.PortionSet2:
                if (playerDataBase.Crystal >= 1200)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 1200);

                    PortionManager.instance.GetAllPortion(20);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.PortionSet3:
                if (playerDataBase.Crystal >= 1800)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 1800);

                    PortionManager.instance.GetAllPortion(40);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.DailyReward_Portion:
                if (playerDataBase.DailyReward_Portion == 1) return;

                playerDataBase.DailyReward_Portion = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyReward_Portion", playerDataBase.DailyReward_Portion);

                shopContents[11].SetLocked(true);

                PortionManager.instance.GetRandomPortion(1);

                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessReward);
                break;
            case ItemType.GoldX2:
                break;
            case ItemType.CrystalShop1:
                break;
            case ItemType.CrystalShop2:
                break;
            case ItemType.CrystalShop3:
                break;
            case ItemType.CrystalShop4:
                break;
            case ItemType.CrystalShop5:
                break;
            case ItemType.CrystalShop6:
                break;
            case ItemType.Portion1:
                if (playerDataBase.RankPoint >= 100)
                {
                    playerDataBase.RankPoint -= 100;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetPortion(0, 1);

                    shopContents[19].Initialize(ItemType.Portion1, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.Portion2:
                if (playerDataBase.RankPoint >= 100)
                {
                    playerDataBase.RankPoint -= 100;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetPortion(1, 1);

                    shopContents[20].Initialize(ItemType.Portion2, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.Portion3:
                if (playerDataBase.RankPoint >= 100)
                {
                    playerDataBase.RankPoint -= 100;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetPortion(2, 1);

                    shopContents[21].Initialize(ItemType.Portion3, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.Portion4:
                if (playerDataBase.RankPoint >= 100)
                {
                    playerDataBase.RankPoint -= 100;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetPortion(3, 1);

                    shopContents[22].Initialize(ItemType.Portion4, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.Portion5:
                if (playerDataBase.RankPoint >= 150)
                {
                    playerDataBase.RankPoint -= 150;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetPortion(4, 1);

                    shopContents[23].Initialize(ItemType.Portion5, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.DefDestroyTicketSlices:
                if (playerDataBase.DefDestroyTicketPiece >= 5)
                {
                    playerDataBase.DefDestroyTicketPiece -= 5;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicketPiece", playerDataBase.DefDestroyTicketPiece);

                    PortionManager.instance.GetDefTickets(1);

                    //shopContents[2].Initialize(ItemType.DefDestroyTicket, BuyType.Coin, this);
                    shopContents[24].Initialize(ItemType.DefDestroyTicketSlices, BuyType.Exchange, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                }
                break;
            case ItemType.DefDestroyTicketPiece:
                if (playerDataBase.DailyReward_DefTicket == 1) return;

                playerDataBase.DailyReward_DefTicket = 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyReward_DefTicket", playerDataBase.DailyReward_DefTicket);

                PortionManager.instance.GetDefTicketPiece(1);

                shopContents[24].Initialize(ItemType.DefDestroyTicketSlices, BuyType.Exchange, this);
                shopContents[25].SetLocked(true);

                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessReward);
                break;
            case ItemType.BuffTicketSet1:
                if (playerDataBase.Crystal >= 600)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 600);

                    PortionManager.instance.GetBuffTickets(20);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.BuffTicketSet2:
                if (playerDataBase.Crystal >= 1200)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 1200);

                    PortionManager.instance.GetBuffTickets(40);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.BuffTicketSet3:
                if (playerDataBase.Crystal >= 1800)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 1800);

                    PortionManager.instance.GetBuffTickets(80);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.DefTicketSet1:
                if (playerDataBase.Crystal >= 1800)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 1800);

                    PortionManager.instance.GetDefTickets(30);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.DefTicketSet2:
                if (playerDataBase.Crystal >= 3600)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 3600);

                    PortionManager.instance.GetDefTickets(60);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.DefTicketSet3:
                if (playerDataBase.Crystal >= 6000)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 6000);

                    PortionManager.instance.GetDefTickets(120);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.SuperOffline:
                break;
            case ItemType.AdReward_Crystal:
                GoogleAdsManager.instance.admobReward_Portion.ShowAd(8);
                break;
            case ItemType.AutoUpgrade:
                if (playerDataBase.Crystal >= 6000)
                {
                    if (playerDataBase.AutoUpgrade) return;

                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 6000);

                    PlayfabManager.instance.PurchaseAutoUpgrade();

                    Invoke("ContentDelay4", 0.5f);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.AutoPresent:
                if (playerDataBase.Crystal >= 3000)
                {
                    if (playerDataBase.AutoPresent) return;

                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 3000);

                    PlayfabManager.instance.PurchaseAutoPresent();

                    Invoke("ContentDelay5", 0.5f);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case ItemType.BuffTicket:
                if (playerDataBase.RankPoint >= 1000)
                {
                    playerDataBase.RankPoint -= 1000;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetBuffTickets(1);

                    shopContents[36].Initialize(ItemType.BuffTicket, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.SkillTicket:
                if (playerDataBase.RankPoint >= 500)
                {
                    playerDataBase.RankPoint -= 500;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetSkillTickets(1);

                    shopContents[37].Initialize(ItemType.SkillTicket, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.RepairTicket:
                if (playerDataBase.RankPoint >= 20)
                {
                    playerDataBase.RankPoint -= 20;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetRepairTickets(1);

                    shopContents[38].Initialize(ItemType.RepairTicket, BuyType.RankPoint, this);
                    shopContents[39].Initialize(ItemType.RepairTicket10, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.RepairTicket10:
                if (playerDataBase.RankPoint >= 200)
                {
                    playerDataBase.RankPoint -= 200;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = playerDataBase.RankPoint.ToString();

                    PortionManager.instance.GetRepairTickets(10);

                    shopContents[38].Initialize(ItemType.RepairTicket, BuyType.RankPoint, this);
                    shopContents[39].Initialize(ItemType.RepairTicket10, BuyType.RankPoint, this);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowPoint);
                }
                break;
            case ItemType.EquipExp:
                break;
            case ItemType.DungeonKey1:
                break;
            case ItemType.DungeonKey2:
                break;
            case ItemType.DungeonKey3:
                break;
            case ItemType.DungeonKey4:
                break;
        }

        GameManager.instance.CheckPortion();
        GameManager.instance.CheckDefTicket();

        FirebaseAnalytics.LogEvent("Buy_Item : " + item.ToString());

        isDelay = true;
        Invoke("Delay", 0.2f);
    }

    void Delay()
    {
        isDelay = false;
    }

    public void BuyCoin()
    {
        switch(changeMoneyIndex)
        {
            case 0:
                if (playerDataBase.Crystal >= 120)
                {
                    PlayfabManager.instance.UpdateAddGold(1000000);
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 120);

                    OpenChangeMoneyView();

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case 1:
                if (playerDataBase.Crystal >= 1200)
                {
                    PlayfabManager.instance.UpdateAddGold(10000000);
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 1200);

                    OpenChangeMoneyView();

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
            case 2:
                if (playerDataBase.Crystal >= 12000)
                {
                    PlayfabManager.instance.UpdateAddGold(100000000);
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 12000);

                    OpenChangeMoneyView();

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                }
                break;
        }
    }

    public void OpenChangeMoneyView()
    {
        if(!changeMoneyView.activeInHierarchy)
        {
            changeMoneyView.SetActive(true);
        }
        else
        {
            changeMoneyView.SetActive(false);
        }
    }

    public void SuccessWatchAd()
    {
        if (playerDataBase.DailyAdsReward == 1) return;

        playerDataBase.DailyAdsReward = 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyAdsReward", playerDataBase.DailyAdsReward);

        shopContents[1].SetLocked(true);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessWatchAd);

        if (playerDataBase.Dessert1MaxValue > 0)
        {
            PlayfabManager.instance.UpdateAddGold(10000000);
            return;
        }

        if (playerDataBase.JapaneseFood1MaxValue > 0)
        {
            PlayfabManager.instance.UpdateAddGold(5000000);
            return;
        }

        if (playerDataBase.Candy1MaxValue > 0)
        {
            PlayfabManager.instance.UpdateAddGold(3000000);
            return;
        }

        PlayfabManager.instance.UpdateAddGold(1000000);
    }

    public void SuccessWatchAd_Portion()
    {
        if (playerDataBase.DailyAdsReward2 == 1) return;

        playerDataBase.DailyAdsReward2 = 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyAdsReward2", playerDataBase.DailyAdsReward2);

        shopContents[6].SetLocked(true);

        PortionManager.instance.GetRandomPortion(10);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }

    public void SuccessWatchAd_Crystal()
    {
        if (playerDataBase.DailyReward_Crystal == 1) return;

        playerDataBase.DailyReward_Crystal = 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyReward_Crystal", playerDataBase.DailyReward_Crystal);

        shopContents[33].SetLocked(true);

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessWatchAd);
    }

    IEnumerator TimerCoroution()
    {
        if (dailyShopCountText.gameObject.activeInHierarchy)
        {
            h = g - f;

            dailyShopCountText.text = localization_Reset + " : " + h.Hours.ToString("D2") + localization_Hours + " " + h.Minutes.ToString("D2") + localization_Minutes;

            if (playerDataBase.AttendanceDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
                yield break;
            }
        }

        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
    }

    public void OpenSpeicalShop_Guide(int number)
    {
        speicalShopView.SetActive(true);

        ChangeSpeicalTopToggle(number);
    }

    public void OpenSpeicalShop()
    {
        if (!speicalShopView.activeInHierarchy)
        {
            speicalShopView.SetActive(true);

            shopAlarm.SetActive(false);
            shopIngameAlarm.SetActive(false);

            if (speicalIndex == -1)
            {
                ChangeSpeicalTopToggle(0);
            }

            switch (speicalIndex)
            {
                case 0:
                    for(int i = 0; i < shopAnimalArray.Length; i ++)
                    {
                        shopAnimalArray[i].transform.localRotation = Quaternion.Euler(0, 210, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_Pet");
                    break;
                case 1:
                    for (int i = 0; i < shopTruckArray.Length; i++)
                    {
                        shopTruckArray[i].transform.localRotation = Quaternion.Euler(0, 210, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_FoodTruck");
                    break;
                case 2:
                    for (int i = 0; i < shopCharacterArray.Length; i++)
                    {
                        shopCharacterArray[i].transform.localRotation = Quaternion.Euler(0, 210, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_Character");
                    break;
                case 3:
                    for (int i = 0; i < shopButterflyArray.Length; i++)
                    {
                        shopButterflyArray[i].transform.localRotation = Quaternion.Euler(0, 210, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_Butterfly");
                    break;
                case 4:
                    for (int i = 0; i < shopTotemsArray.Length; i++)
                    {
                        shopTotemsArray[i].transform.localRotation = Quaternion.Euler(0, 210, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_Totems");
                    break;
                case 5:
                    for (int i = 0; i < shopFlowerArray.Length; i++)
                    {
                        shopFlowerArray[i].transform.localRotation = Quaternion.Euler(0, 210, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_Flower");
                    break;
            }
        }
        else
        {
            if (guideMissionManager.guideMissonView.gameObject.activeInHierarchy)
            {
                guideMissionManager.Initialize();
            }

            speicalShopView.SetActive(false);
        }
    }

    public void OpenSpeicalShopView(int number)
    {
        if (!speicalShopView.activeInHierarchy)
        {
            speicalShopView.SetActive(true);

            ChangeSpeicalTopToggle(number);
        }
        else
        {
            if(guideMissionManager.guideMissonView.gameObject.activeInHierarchy)
            {
                guideMissionManager.Initialize();
            }

            speicalShopView.SetActive(false);
        }
    }

    public void ChangeSpeicalTopToggle(int number)
    {
        if (speicalIndex == number) return;

        if (number == 3)
        {
            if (playerDataBase.Level < 15)
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion3(Color.yellow, LocalizationManager.instance.GetString("BufferflyLocked"));
                return;
            }
        }
        else if (number == 4)
        {
            if (playerDataBase.Level < 20)
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion3(Color.yellow, LocalizationManager.instance.GetString("TotemLocked"));
                return;
            }
        }

        speicalIndex = number;

        for (int i = 0; i < speicalTopMenuImgArray.Length; i++)
        {
            speicalTopMenuImgArray[i].sprite = speicalTopMenuSpriteArray[0];
        }

        speicalTopMenuImgArray[number].sprite = speicalTopMenuSpriteArray[1];

        mainCharacter.SetActive(false);
        mainTruck.SetActive(false);
        mainAnimal.SetActive(false);
        mainButterfly.SetActive(false);
        mainTotems.SetActive(false);
        mainFlower.SetActive(false);

        yummyButton.SetActive(true);

        leftButton.SetActive(true);
        rightButton.SetActive(true);

        switch (number)
        {
            case 0:
                if (animalIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (animalIndex >= shopAnimalArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                AnimalInitialize();
                break;
            case 1:
                if (truckIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (truckIndex >= shopTruckArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                TruckInitialize();
                break;
            case 2:
                if (characterIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (characterIndex >= shopCharacterArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                CharacterInitialize();
                break;
            case 3:
                if (butterflyIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (butterflyIndex >= shopButterflyArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                ButterflyInitialize();
                break;
            case 4:
                yummyButton.SetActive(false);

                if (totemsIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (totemsIndex >= shopTotemsArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                TotemsInitialize();
                break;
            case 5:
                yummyButton.SetActive(false);

                if (flowerIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (flowerIndex >= shopFlowerArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                FlowerInitialize();
                break;
        }
    }


    void CharacterInitialize()
    {
        mainCharacter.SetActive(true);

        for (int i = 0; i < shopCharacterArray.Length; i++)
        {
            shopCharacterArray[i].gameObject.SetActive(false);
        }

        shopCharacterArray[characterIndex].gameObject.SetActive(true);
        shopCharacterArray[characterIndex].transform.position = normalPos;
        shopCharacterArray[characterIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);

        if (characterIndex == 0)
        {
            shopCharacterArray[characterIndex + 1].gameObject.SetActive(true);
            shopCharacterArray[characterIndex + 1].transform.position = afterPos;
            shopCharacterArray[characterIndex + 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
        }
        else if (characterIndex > 0)
        {
            if (characterIndex == shopCharacterArray.Length - 1)
            {
                shopCharacterArray[characterIndex - 1].gameObject.SetActive(true);
                shopCharacterArray[characterIndex - 1].transform.position = beforePos;
                shopCharacterArray[characterIndex - 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
            }
            else
            {
                shopCharacterArray[characterIndex - 1].gameObject.SetActive(true);
                shopCharacterArray[characterIndex - 1].transform.position = beforePos;
                shopCharacterArray[characterIndex - 1].transform.localRotation = Quaternion.Euler(0, 210, 0);

                shopCharacterArray[characterIndex + 1].gameObject.SetActive(true);
                shopCharacterArray[characterIndex + 1].transform.position = afterPos;
                shopCharacterArray[characterIndex + 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
            }
        }


        characterInfo = characterDataBase.GetCharacterInfo(CharacterType.Character1 + characterIndex);

        nameText.localizationName =  "Character" + (characterIndex + 1);
        //passiveText.text = "";

        effectText.localizationName = characterInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + characterInfo.effectNumber.ToString() + "%";

        titleText.localizationName = "ChangeCharacter";
        titleText.plusText = "\n<size=10>( " + (characterIndex + 1) + " / " + shopCharacterArray.Length + " ) "
            + ((((playerDataBase.GetCharacterNumber() + 1) * 1.0f) / (shopCharacterArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = "Character" + (characterIndex + 1) + "_Info";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = characterInfo.price;
        price_Crystal = price_Gold / exchagne;
        price_Crystal = Mathf.RoundToInt(price_Crystal / 100.0f) * 100;

        if(price_Crystal < 100)
        {
            price_Crystal = 100;
        }

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        switch (characterInfo.characterType)
        {
            case CharacterType.Character1:
                hold = true;
                break;
            case CharacterType.Character2:
                if (playerDataBase.Character2 >= 1)
                {
                    hold = true;
                }

                buy = true;

                break;
            case CharacterType.Character3:
                if (playerDataBase.Character3 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character2 >= 1)
                {
                    buy = true;
                }

                break;
            case CharacterType.Character4:
                if (playerDataBase.Character4 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character3 >= 1)
                {
                    buy = true;
                }

                break;
            case CharacterType.Character5:
                if (playerDataBase.Character5 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character4 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character6:
                if (playerDataBase.Character6 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character5 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character7:
                if (playerDataBase.Character7 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character6 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character8:
                if (playerDataBase.Character8 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character7 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character9:
                if (playerDataBase.Character9 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character8 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character10:
                if (playerDataBase.Character10 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character9 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character11:
                if (playerDataBase.Character11 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character10 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character12:
                if (playerDataBase.Character12 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character11 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character13:
                if (playerDataBase.Character13 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character12 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character14:
                if (playerDataBase.Character14 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character13 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character15:
                if (playerDataBase.Character15 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character14 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character16:
                if (playerDataBase.Character16 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character15 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character17:
                if (playerDataBase.Character17 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character16 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character18:
                if (playerDataBase.Character18 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character17 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character19:
                if (playerDataBase.Character19 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character18 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character20:
                if (playerDataBase.Character20 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Character19 >= 1)
                {
                    buy = true;
                }
                break;
            case CharacterType.Character21:
                if (playerDataBase.Character21 >= 1)
                {
                    hold = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        if (hold)
        {
            selectObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (GameStateManager.instance.CharacterType.Equals(characterInfo.characterType))
            {
                selectText.text = LocalizationManager.instance.GetString("Selected");

                selectCheckMarkObj.SetActive(true);
            }
            else
            {
                selectText.text = LocalizationManager.instance.GetString("Select");
            }
        }
        else
        {
            selectObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
            }

            crystalButton.SetActive(true);

            if (characterInfo.characterType == CharacterType.Character21)
            {
                buyButton.SetActive(false);
                buySpeical.SetActive(true);
            }
        }
    }

    void TruckInitialize()
    {
        mainTruck.SetActive(true);

        for (int i = 0; i < shopTruckArray.Length; i++)
        {
            shopTruckArray[i].gameObject.SetActive(false);
        }

        shopTruckArray[truckIndex].gameObject.SetActive(true);
        shopTruckArray[truckIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);

        truckInfo = truckDataBase.GetTruckInfo(TruckType.Bread + truckIndex);

        nameText.localizationName = (TruckType.Bread + truckIndex).ToString() + "Truck";
        //passiveText.text = "";

        effectText.localizationName = truckInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + truckInfo.effectNumber.ToString() + "%";

        titleText.localizationName = "ChangeTruck";
        titleText.plusText = "\n<size=10>( " + (truckIndex + 1) + " / " + shopTruckArray.Length + " ) "
            + ((((playerDataBase.GetFoodTruckNumber() + 1) * 1.0f) / (shopTruckArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = (TruckType.Bread + truckIndex) + "TruckInfo";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = truckInfo.price;
        price_Crystal = price_Gold / exchagne;
        price_Crystal = Mathf.RoundToInt(price_Crystal / 100.0f) * 100;

        if (price_Crystal < 100)
        {
            price_Crystal = 100;
        }

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);

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

#if UNITY_EDITOR
        buy = true;
#endif

        if (hold)
        {
            selectObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (GameStateManager.instance.TruckType.Equals(truckInfo.truckType))
            {
                selectText.text = LocalizationManager.instance.GetString("Selected");

                selectCheckMarkObj.SetActive(true);
            }
            else
            {
                selectText.text = LocalizationManager.instance.GetString("Select");
            }
        }
        else
        {
            selectObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
            }

            crystalButton.SetActive(true);
        }
    }

    void AnimalInitialize()
    {
        mainAnimal.SetActive(true);

        for (int i = 0; i < shopAnimalArray.Length; i++)
        {
            shopAnimalArray[i].gameObject.SetActive(false);
        }

        shopAnimalArray[animalIndex].gameObject.SetActive(true);
        shopAnimalArray[animalIndex].transform.position = normalPos;
        shopAnimalArray[animalIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);

        if(animalIndex == 2) //청어 키 높이
        {
            shopAnimalArray[animalIndex].transform.position += new Vector3(0, 0.9f, 0);
        }

        if(animalIndex == 0)
        {
            shopAnimalArray[animalIndex + 1].gameObject.SetActive(true);
            shopAnimalArray[animalIndex + 1].transform.position = afterPos;
            shopAnimalArray[animalIndex + 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
        }
        else if(animalIndex > 0)
        {
            if(animalIndex == shopAnimalArray.Length - 1)
            {
                shopAnimalArray[animalIndex - 1].gameObject.SetActive(true);
                shopAnimalArray[animalIndex - 1].transform.position = beforePos;
                shopAnimalArray[animalIndex - 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
            }
            else
            {
                shopAnimalArray[animalIndex - 1].gameObject.SetActive(true);
                shopAnimalArray[animalIndex - 1].transform.position = beforePos;
                shopAnimalArray[animalIndex - 1].transform.localRotation = Quaternion.Euler(0, 210, 0);

                shopAnimalArray[animalIndex + 1].gameObject.SetActive(true);
                shopAnimalArray[animalIndex + 1].transform.position = afterPos;
                shopAnimalArray[animalIndex + 1].transform.localRotation = Quaternion.Euler(0, 210, 0);

                if (animalIndex - 1 == 2) //청어 키 높이
                {
                    shopAnimalArray[animalIndex - 1].transform.position += new Vector3(0, 0.9f, 0);
                }

                if (animalIndex + 1 == 2) //청어 키 높이
                {
                    shopAnimalArray[animalIndex + 1].transform.position += new Vector3(0, 0.9f, 0);
                }
            }
        }

        animalInfo = animalDataBase.GetAnimalInfo(AnimalType.Colobus + animalIndex);

        nameText.localizationName = (AnimalType.Colobus + animalIndex).ToString();
        //passiveText.text = "";

        effectText.localizationName = animalInfo.passiveEffect.ToString();
        effectText.plusText = " +" + animalInfo.effectNumber.ToString();

        titleText.localizationName = "ChangeAnimal";
        titleText.plusText = "\n<size=10>( " + (animalIndex + 1) + " / " + shopAnimalArray.Length +" ) "
            + ((((playerDataBase.GetAnimalNumber() + 1) * 1.0f) / (shopAnimalArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = (AnimalType.Colobus + animalIndex) + "Info";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = animalInfo.price;
        price_Crystal = price_Gold / exchagne;
        price_Crystal = Mathf.RoundToInt(price_Crystal / 100.0f) * 100;

        if (price_Crystal < 100)
        {
            price_Crystal = 100;
        }

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);

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

#if UNITY_EDITOR
        buy = true;
#endif

        if (hold)
        {
            selectObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (GameStateManager.instance.AnimalType.Equals(animalInfo.animalType))
            {
                selectText.text = LocalizationManager.instance.GetString("Selected");

                selectCheckMarkObj.SetActive(true);
            }
            else
            {
                selectText.text = LocalizationManager.instance.GetString("Select");
            }
        }
        else
        {
            selectObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
            }

            crystalButton.SetActive(true);
        }
    }

    void ButterflyInitialize()
    {
        mainButterfly.SetActive(true);

        for (int i = 0; i < shopButterflyArray.Length; i++)
        {
            shopButterflyArray[i].gameObject.SetActive(false);
        }

        shopButterflyArray[butterflyIndex].gameObject.SetActive(true);
        shopButterflyArray[butterflyIndex].transform.position = normalPos_Butterfly;
        shopButterflyArray[butterflyIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);

        if (butterflyIndex == 0)
        {
            shopButterflyArray[butterflyIndex + 1].gameObject.SetActive(true);
            shopButterflyArray[butterflyIndex + 1].transform.position = afterPos_Butterfly;
            shopButterflyArray[butterflyIndex + 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
        }
        else if (butterflyIndex > 0)
        {
            if (butterflyIndex == shopButterflyArray.Length - 1)
            {
                shopButterflyArray[butterflyIndex - 1].gameObject.SetActive(true);
                shopButterflyArray[butterflyIndex - 1].transform.position = beforePos_Butterfly;
                shopButterflyArray[butterflyIndex - 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
            }
            else
            {
                shopButterflyArray[butterflyIndex - 1].gameObject.SetActive(true);
                shopButterflyArray[butterflyIndex - 1].transform.position = beforePos_Butterfly;
                shopButterflyArray[butterflyIndex - 1].transform.localRotation = Quaternion.Euler(0, 210, 0);

                shopButterflyArray[butterflyIndex + 1].gameObject.SetActive(true);
                shopButterflyArray[butterflyIndex + 1].transform.position = afterPos_Butterfly;
                shopButterflyArray[butterflyIndex + 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
            }
        }

        butterflyInfo = butterflyDataBase.GetButterflyInfo(ButterflyType.Butterfly1 + butterflyIndex);

        nameText.localizationName = "Butterfly" + (butterflyIndex + 1);
        //passiveText.text = "";
        
        effectText.localizationName = butterflyInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + butterflyInfo.effectNumber.ToString() + "%";

        titleText.localizationName = "ChangeButterfly";
        titleText.plusText = "\n<size=10>( " + (butterflyIndex + 1) + " / " + shopButterflyArray.Length + " ) "
            + ((((playerDataBase.GetButterflyNumber() + 1) * 1.0f) / (shopButterflyArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = " ";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = butterflyInfo.price;
        price_Crystal = price_Gold / exchagne;
        price_Crystal = Mathf.RoundToInt(price_Crystal / 100.0f) * 100;

        if (price_Crystal < 100)
        {
            price_Crystal = 100;
        }

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        switch (butterflyInfo.butterflyType)
        {
            case ButterflyType.Butterfly1:
                hold = true;
                break;
            case ButterflyType.Butterfly2:
                if (playerDataBase.Butterfly2 >= 1)
                {
                    hold = true;
                }

                buy = true;
                break;
            case ButterflyType.Butterfly3:
                if (playerDataBase.Butterfly3 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly2 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly4:
                if (playerDataBase.Butterfly4 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly3 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly5:
                if (playerDataBase.Butterfly5 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly4 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly6:
                if (playerDataBase.Butterfly6 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly5 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly7:
                if (playerDataBase.Butterfly7 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly6 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly8:
                if (playerDataBase.Butterfly8 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly7 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly9:
                if (playerDataBase.Butterfly9 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly8 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly10:
                if (playerDataBase.Butterfly10 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly9 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly11:
                if (playerDataBase.Butterfly11 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly10 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly12:
                if (playerDataBase.Butterfly12 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly11 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly13:
                if (playerDataBase.Butterfly13 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly12 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly14:
                if (playerDataBase.Butterfly14 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly13 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly15:
                if (playerDataBase.Butterfly15 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly14 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly16:
                if (playerDataBase.Butterfly16 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly15 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly17:
                if (playerDataBase.Butterfly17 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly16 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly18:
                if (playerDataBase.Butterfly18 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly17 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly19:
                if (playerDataBase.Butterfly19 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly18 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly20:
                if (playerDataBase.Butterfly20 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly19 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly21:
                if (playerDataBase.Butterfly21 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly20 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly22:
                if (playerDataBase.Butterfly22 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly21 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly23:
                if (playerDataBase.Butterfly23 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly22 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly24:
                if (playerDataBase.Butterfly24 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly23 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly25:
                if (playerDataBase.Butterfly25 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly24 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly26:
                if (playerDataBase.Butterfly26 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly25 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly27:
                if (playerDataBase.Butterfly27 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly26 >= 1)
                {
                    buy = true;
                }
                break;
            case ButterflyType.Butterfly28:
                if (playerDataBase.Butterfly28 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Butterfly27 >= 1)
                {
                    buy = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        if (hold)
        {
            selectObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (GameStateManager.instance.ButterflyType.Equals(butterflyInfo.butterflyType))
            {
                selectText.text = LocalizationManager.instance.GetString("Selected");

                selectCheckMarkObj.SetActive(true);
            }
            else
            {
                selectText.text = LocalizationManager.instance.GetString("Select");
            }
        }
        else
        {
            selectObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
            }

            crystalButton.SetActive(true);
        }
    }

    void TotemsInitialize()
    {
        mainTotems.SetActive(true);

        for (int i = 0; i < shopTotemsArray.Length; i++)
        {
            shopTotemsArray[i].gameObject.SetActive(false);
        }

        shopTotemsArray[totemsIndex].gameObject.SetActive(true);
        shopTotemsArray[totemsIndex].transform.position = new Vector3(-40, shopTotemsArray[totemsIndex].transform.position.y, -3);
        shopTotemsArray[totemsIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);

        if (totemsIndex == 0)
        {
            shopTotemsArray[totemsIndex + 1].gameObject.SetActive(true);
            shopTotemsArray[totemsIndex + 1].transform.position = new Vector3(-35, shopTotemsArray[totemsIndex + 1].transform.position.y, 2);
            shopTotemsArray[totemsIndex + 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
        }
        else if (totemsIndex > 0)
        {
            if (totemsIndex == shopTotemsArray.Length - 1)
            {
                shopTotemsArray[totemsIndex - 1].gameObject.SetActive(true);
                shopTotemsArray[totemsIndex - 1].transform.position = new Vector3(-45, shopTotemsArray[totemsIndex - 1].transform.position.y, 2);
                shopTotemsArray[totemsIndex - 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
            }
            else
            {
                shopTotemsArray[totemsIndex - 1].gameObject.SetActive(true);
                shopTotemsArray[totemsIndex - 1].transform.position = new Vector3(-45, shopTotemsArray[totemsIndex - 1].transform.position.y, 2);
                shopTotemsArray[totemsIndex - 1].transform.localRotation = Quaternion.Euler(0, 210, 0);

                shopTotemsArray[totemsIndex + 1].gameObject.SetActive(true);
                shopTotemsArray[totemsIndex + 1].transform.position = new Vector3(-35, shopTotemsArray[totemsIndex + 1].transform.position.y, 2);
                shopTotemsArray[totemsIndex + 1].transform.localRotation = Quaternion.Euler(0, 210, 0);
            }
        }


        totemsInfo = totemsDataBase.GetTotemsInfo(TotemsType.Totems1 + totemsIndex);

        nameText.localizationName = (TotemsType.Totems1 + totemsIndex).ToString();
        //passiveText.text = "";

        effectText.localizationName = totemsInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + totemsInfo.effectNumber.ToString() + "%";

        titleText.localizationName = "ChangeTotems";
        titleText.plusText = "\n<size=10>( " + (totemsIndex + 1) + " / " + shopTotemsArray.Length + " ) "
            + ((((playerDataBase.GetTotemsNumber() + 1) * 1.0f) / (shopTotemsArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = " ";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = totemsInfo.price;
        price_Crystal = price_Gold / exchagne;
        price_Crystal = Mathf.RoundToInt(price_Crystal / 100.0f) * 100;

        if (price_Crystal < 100)
        {
            price_Crystal = 100;
        }

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        switch (totemsInfo.totemsType)
        {
            case TotemsType.Totems1:
                hold = true;
                break;
            case TotemsType.Totems2:
                if (playerDataBase.Totems2 >= 1)
                {
                    hold = true;
                }

                buy = true;
                break;
            case TotemsType.Totems3:
                if (playerDataBase.Totems3 >= 1)
                {
                    hold = true;
                }

                if(playerDataBase.Totems2 >= 1)
                {
                    buy = true;
                }
                break;
            case TotemsType.Totems4:
                if (playerDataBase.Totems4 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Totems3 >= 1)
                {
                    buy = true;
                }
                break;
            case TotemsType.Totems5:
                if (playerDataBase.Totems5 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Totems4 >= 1)
                {
                    buy = true;
                }
                break;
            case TotemsType.Totems6:
                if (playerDataBase.Totems6 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Totems5 >= 1)
                {
                    buy = true;
                }
                break;
            case TotemsType.Totems7:
                if (playerDataBase.Totems7 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Totems6 >= 1)
                {
                    buy = true;
                }
                break;
            case TotemsType.Totems8:
                if (playerDataBase.Totems8 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Totems7 >= 1)
                {
                    buy = true;
                }
                break;
            case TotemsType.Totems9:
                if (playerDataBase.Totems9 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Totems8 >= 1)
                {
                    buy = true;
                }
                break;
            case TotemsType.Totems10:
                if (playerDataBase.Totems10 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Totems9 >= 1)
                {
                    buy = true;
                }
                break;
            case TotemsType.Totems11:
                if (playerDataBase.Totems11 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Totems10 >= 1)
                {
                    buy = true;
                }
                break;
            case TotemsType.Totems12:
                if (playerDataBase.Totems12 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Totems11 >= 1)
                {
                    buy = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        if (hold)
        {
            selectObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (GameStateManager.instance.TotemsType.Equals(totemsInfo.totemsType))
            {
                selectText.text = LocalizationManager.instance.GetString("Selected");

                selectCheckMarkObj.SetActive(true);
            }
            else
            {
                selectText.text = LocalizationManager.instance.GetString("Select");
            }
        }
        else
        {
            selectObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
            }

            crystalButton.SetActive(true);
        }
    }

    void FlowerInitialize()
    {

    }

    public void RightButton()
    {
        leftButton.SetActive(true);
        rightButton.SetActive(true);

        switch (speicalIndex)
        {
            case 0:
                if (animalIndex + 1 < shopAnimalArray.Length)
                {
                    animalIndex += 1;

                    AnimalInitialize();
                }

                if (animalIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (animalIndex >= shopAnimalArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 1:
                if (truckIndex + 1 < shopTruckArray.Length)
                {
                    truckIndex += 1;

                    TruckInitialize();
                }

                if (truckIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (truckIndex >= shopTruckArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 2:
                if (characterIndex + 1 < shopCharacterArray.Length)
                {
                    characterIndex += 1;

                    CharacterInitialize();
                }

                if (characterIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (characterIndex >= shopCharacterArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 3:
                if (butterflyIndex + 1 < shopButterflyArray.Length)
                {
                    butterflyIndex += 1;

                    ButterflyInitialize();
                }

                if (butterflyIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (butterflyIndex >= shopButterflyArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 4:
                if (totemsIndex + 1 < shopTotemsArray.Length)
                {
                    totemsIndex += 1;

                    TotemsInitialize();
                }

                if (totemsIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (totemsIndex >= shopTotemsArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }
                break;
            case 5:
                if (flowerIndex + 1 < shopFlowerArray.Length)
                {
                    flowerIndex += 1;

                    FlowerInitialize();
                }

                if (flowerIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (flowerIndex >= shopFlowerArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }
                break;
        }
    }

    public void LeftButton()
    {
        leftButton.SetActive(true);
        rightButton.SetActive(true);

        switch (speicalIndex)
        {
            case 0:
                if (animalIndex - 1 >= 0)
                {
                    animalIndex -= 1;

                    AnimalInitialize();
                }

                if (animalIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (animalIndex >= shopAnimalArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 1:
                if (truckIndex - 1 >= 0)
                {
                    truckIndex -= 1;

                    TruckInitialize();
                }

                if (truckIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (truckIndex >= shopTruckArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 2:
                if (characterIndex - 1 >= 0)
                {
                    characterIndex -= 1;

                    CharacterInitialize();
                }

                if (characterIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (characterIndex >= shopCharacterArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 3:
                if (butterflyIndex - 1 >= 0)
                {
                    butterflyIndex -= 1;

                    ButterflyInitialize();
                }

                if (butterflyIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (butterflyIndex >= shopButterflyArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 4:
                if (totemsIndex - 1 >= 0)
                {
                    totemsIndex -= 1;

                    TotemsInitialize();
                }

                if (totemsIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (totemsIndex >= shopTotemsArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 5:
                if (flowerIndex - 1 >= 0)
                {
                    flowerIndex -= 1;

                    FlowerInitialize();
                }

                if (flowerIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (flowerIndex >= shopFlowerArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
        }
    }

    public void Selected()
    {
        switch(speicalIndex)
        {
            case 0:
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
            case 1:
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
            case 2:
                if (GameStateManager.instance.CharacterType == characterInfo.characterType)
                {
                    return;
                }

                GameStateManager.instance.CharacterType = characterInfo.characterType;

                for (int i = 0; i < mainCharacterArray.Length; i++)
                {
                    mainCharacterArray[i].SetActive(false);
                }

                mainCharacterArray[(int)GameStateManager.instance.CharacterType].SetActive(true);

                NotionManager.instance.UseNotion(NotionType.ChangeCharacterNotion);
                break;
            case 3:
                if (GameStateManager.instance.ButterflyType == butterflyInfo.butterflyType)
                {
                    return;
                }

                GameStateManager.instance.ButterflyType = butterflyInfo.butterflyType;

                for (int i = 0; i < mainButterflyArray.Length; i++)
                {
                    mainButterflyArray[i].SetActive(false);
                }

                mainButterflyArray[(int)GameStateManager.instance.ButterflyType].SetActive(true);

                NotionManager.instance.UseNotion(NotionType.ChangeButterflyNotion);
                break;
            case 4:
                if (GameStateManager.instance.TotemsType == totemsInfo.totemsType)
                {
                    return;
                }

                GameStateManager.instance.TotemsType = totemsInfo.totemsType;

                for (int i = 0; i < mainTotemsArray.Length; i++)
                {
                    mainTotemsArray[i].SetActive(false);
                }

                mainTotemsArray[(int)GameStateManager.instance.TotemsType].SetActive(true);

                NotionManager.instance.UseNotion(NotionType.ChangeTotemsNotion);
                break;
            case 5:
                if (GameStateManager.instance.FlowerType == flowerInfo.flowerType)
                {
                    return;
                }

                GameStateManager.instance.FlowerType = flowerInfo.flowerType;

                for (int i = 0; i < mainFlowerArray.Length; i++)
                {
                    mainFlowerArray[i].SetActive(false);
                }

                mainFlowerArray[(int)GameStateManager.instance.FlowerType].SetActive(true);

                NotionManager.instance.UseNotion(NotionType.ChangeFlowerNotion);
                break;
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);

        selectText.text = LocalizationManager.instance.GetString("Selected");

        selectCheckMarkObj.SetActive(true);

        GourmetManager.instance.Initialize();
    }

    public void BuyItem(int number)
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (!buy)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NotPurchaseNotion);
            return;
        }

        switch (speicalIndex)
        {
            case 0:
                switch (number)
                {
                    case 0:
                        GameManager.instance.RenewalVC();

                        if (playerDataBase.Coin < price_Gold)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractGold(price_Gold);
                        }
                        break;
                    case 1:
                        if (playerDataBase.Crystal < price_Crystal)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCrystal);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price_Crystal);
                        }
                        break;
                }

                itemList.Clear();
                itemList.Add(animalInfo.animalType.ToString());

                PlayfabManager.instance.GrantItemToUser("Animal", itemList);

                FirebaseAnalytics.LogEvent("Buy_Animal : " + animalInfo.animalType.ToString());

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

                titleText.plusText = "\n<size=10>( " + (animalIndex + 1) + " / " + shopAnimalArray.Length + " ) "
    + ((((playerDataBase.GetAnimalNumber() + 1) * 1.0f) / (shopAnimalArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
                titleText.ReLoad();

                break;
            case 1:
                switch(number)
                {
                    case 0:
                        GameManager.instance.RenewalVC();

                        if (playerDataBase.Coin < price_Gold)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractGold(price_Gold);
                        }
                        break;
                    case 1:
                        if (playerDataBase.Crystal < price_Crystal)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCrystal);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price_Crystal);
                        }
                        break;
                }

                itemList.Clear();
                itemList.Add(truckInfo.truckType.ToString());

                PlayfabManager.instance.GrantItemToUser("Truck", itemList);

                FirebaseAnalytics.LogEvent("Buy_Truck : " + truckInfo.truckType.ToString());

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

                titleText.plusText = "\n<size=10>( " + (truckIndex + 1) + " / " + shopTruckArray.Length + " ) "
+ ((((playerDataBase.GetFoodTruckNumber() + 1) * 1.0f) / (shopTruckArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
                titleText.ReLoad();

                break;
            case 2:
                switch (number)
                {
                    case 0:
                        GameManager.instance.RenewalVC();

                        if (playerDataBase.Coin < price_Gold)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractGold(price_Gold);
                        }
                        break;
                    case 1:
                        if (playerDataBase.Crystal < price_Crystal)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCrystal);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price_Crystal);
                        }
                        break;
                }

                itemList.Clear();
                itemList.Add(characterInfo.characterType.ToString());

                PlayfabManager.instance.GrantItemToUser("Character", itemList);

                FirebaseAnalytics.LogEvent("Buy_Character : " + characterInfo.characterType.ToString());

                switch (characterInfo.characterType)
                {
                    case CharacterType.Character1:
                        break;
                    case CharacterType.Character2:
                        playerDataBase.Character2 = 1;
                        break;
                    case CharacterType.Character3:
                        playerDataBase.Character3 = 1;
                        break;
                    case CharacterType.Character4:
                        playerDataBase.Character4 = 1;
                        break;
                    case CharacterType.Character5:
                        playerDataBase.Character5 = 1;
                        break;
                    case CharacterType.Character6:
                        playerDataBase.Character6 = 1;
                        break;
                    case CharacterType.Character7:
                        playerDataBase.Character7 = 1;
                        break;
                    case CharacterType.Character8:
                        playerDataBase.Character8 = 1;
                        break;
                    case CharacterType.Character9:
                        playerDataBase.Character9 = 1;
                        break;
                    case CharacterType.Character10:
                        playerDataBase.Character10 = 1;
                        break;
                    case CharacterType.Character11:
                        playerDataBase.Character11 = 1;
                        break;
                    case CharacterType.Character12:
                        playerDataBase.Character12 = 1;
                        break;
                    case CharacterType.Character13:
                        playerDataBase.Character13 = 1;
                        break;
                    case CharacterType.Character14:
                        playerDataBase.Character14 = 1;
                        break;
                    case CharacterType.Character15:
                        playerDataBase.Character15 = 1;
                        break;
                    case CharacterType.Character16:
                        playerDataBase.Character16 = 1;
                        break;
                    case CharacterType.Character17:
                        playerDataBase.Character17 = 1;
                        break;
                    case CharacterType.Character18:
                        playerDataBase.Character18 = 1;
                        break;
                    case CharacterType.Character19:
                        playerDataBase.Character19 = 1;
                        break;
                    case CharacterType.Character20:
                        playerDataBase.Character20 = 1;
                        break;
                }

                titleText.plusText = "\n<size=10>( " + (characterIndex + 1) + " / " + shopCharacterArray.Length + " ) "
+ ((((playerDataBase.GetCharacterNumber() + 1) * 1.0f) / (shopCharacterArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
                titleText.ReLoad();

                break;
            case 3:
                switch (number)
                {
                    case 0:
                        GameManager.instance.RenewalVC();

                        if (playerDataBase.Coin < price_Gold)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractGold(price_Gold);
                        }
                        break;
                    case 1:
                        if (playerDataBase.Crystal < price_Crystal)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCrystal);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price_Crystal);
                        }
                        break;
                }

                itemList.Clear();
                itemList.Add(butterflyInfo.butterflyType.ToString());

                PlayfabManager.instance.GrantItemToUser("Butterfly", itemList);

                FirebaseAnalytics.LogEvent("Buy_Butterfly : " + butterflyInfo.butterflyType.ToString());

                switch (butterflyInfo.butterflyType)
                {
                    case ButterflyType.Butterfly1:
                        break;
                    case ButterflyType.Butterfly2:
                        playerDataBase.Butterfly2 = 1;
                        break;
                    case ButterflyType.Butterfly3:
                        playerDataBase.Butterfly3 = 1;
                        break;
                    case ButterflyType.Butterfly4:
                        playerDataBase.Butterfly4 = 1;
                        break;
                    case ButterflyType.Butterfly5:
                        playerDataBase.Butterfly5 = 1;
                        break;
                    case ButterflyType.Butterfly6:
                        playerDataBase.Butterfly6 = 1;
                        break;
                    case ButterflyType.Butterfly7:
                        playerDataBase.Butterfly7 = 1;
                        break;
                    case ButterflyType.Butterfly8:
                        playerDataBase.Butterfly8 = 1;
                        break;
                    case ButterflyType.Butterfly9:
                        playerDataBase.Butterfly9 = 1;
                        break;
                    case ButterflyType.Butterfly10:
                        playerDataBase.Butterfly10 = 1;
                        break;
                    case ButterflyType.Butterfly11:
                        playerDataBase.Butterfly11 = 1;
                        break;
                    case ButterflyType.Butterfly12:
                        playerDataBase.Butterfly12 = 1;
                        break;
                    case ButterflyType.Butterfly13:
                        playerDataBase.Butterfly13 = 1;
                        break;
                    case ButterflyType.Butterfly14:
                        playerDataBase.Butterfly14 = 1;
                        break;
                    case ButterflyType.Butterfly15:
                        playerDataBase.Butterfly15 = 1;
                        break;
                    case ButterflyType.Butterfly16:
                        playerDataBase.Butterfly16 = 1;
                        break;
                    case ButterflyType.Butterfly17:
                        playerDataBase.Butterfly17 = 1;
                        break;
                    case ButterflyType.Butterfly18:
                        playerDataBase.Butterfly18 = 1;
                        break;
                    case ButterflyType.Butterfly19:
                        playerDataBase.Butterfly19 = 1;
                        break;
                    case ButterflyType.Butterfly20:
                        playerDataBase.Butterfly20 = 1;
                        break;
                    case ButterflyType.Butterfly21:
                        playerDataBase.Butterfly21 = 1;
                        break;
                    case ButterflyType.Butterfly22:
                        playerDataBase.Butterfly22 = 1;
                        break;
                    case ButterflyType.Butterfly23:
                        playerDataBase.Butterfly23 = 1;
                        break;
                    case ButterflyType.Butterfly24:
                        playerDataBase.Butterfly24 = 1;
                        break;
                    case ButterflyType.Butterfly25:
                        playerDataBase.Butterfly25 = 1;
                        break;
                    case ButterflyType.Butterfly26:
                        playerDataBase.Butterfly26 = 1;
                        break;
                    case ButterflyType.Butterfly27:
                        playerDataBase.Butterfly27 = 1;
                        break;
                    case ButterflyType.Butterfly28:
                        playerDataBase.Butterfly28 = 1;
                        break;
                }

                titleText.plusText = "\n<size=10>( " + (butterflyIndex + 1) + " / " + shopButterflyArray.Length + " ) "
+ ((((playerDataBase.GetButterflyNumber() + 1) * 1.0f) / (shopButterflyArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
                titleText.ReLoad();

                break;
            case 4:
                switch (number)
                {
                    case 0:
                        GameManager.instance.RenewalVC();

                        if (playerDataBase.Coin < price_Gold)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractGold(price_Gold);
                        }
                        break;
                    case 1:
                        if (playerDataBase.Crystal < price_Crystal)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCrystal);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price_Crystal);
                        }
                        break;
                }

                itemList.Clear();
                itemList.Add(totemsInfo.totemsType.ToString());

                PlayfabManager.instance.GrantItemToUser("Totems", itemList);

                FirebaseAnalytics.LogEvent("Buy_Totems : " + totemsInfo.totemsType.ToString());

                switch (totemsInfo.totemsType)
                {
                    case TotemsType.Totems1:
                        playerDataBase.Totems1 = 1;
                        break;
                    case TotemsType.Totems2:
                        playerDataBase.Totems2 = 1;
                        break;
                    case TotemsType.Totems3:
                        playerDataBase.Totems3 = 1;
                        break;
                    case TotemsType.Totems4:
                        playerDataBase.Totems4 = 1;
                        break;
                    case TotemsType.Totems5:
                        playerDataBase.Totems5 = 1;
                        break;
                    case TotemsType.Totems6:
                        playerDataBase.Totems6 = 1;
                        break;
                    case TotemsType.Totems7:
                        playerDataBase.Totems7 = 1;
                        break;
                    case TotemsType.Totems8:
                        playerDataBase.Totems8 = 1;
                        break;
                    case TotemsType.Totems9:
                        playerDataBase.Totems9 = 1;
                        break;
                    case TotemsType.Totems10:
                        playerDataBase.Totems10 = 1;
                        break;
                    case TotemsType.Totems11:
                        playerDataBase.Totems11 = 1;
                        break;
                    case TotemsType.Totems12:
                        playerDataBase.Totems12 = 1;
                        break;
                }

                titleText.plusText = "\n<size=10>( " + (totemsIndex + 1) + " / " + shopTotemsArray.Length + " ) "
+ ((((playerDataBase.GetTotemsNumber() + 1) * 1.0f) / (shopTotemsArray.Length * 1.0f)) * 100f).ToString("N2") + "%</size>";
                titleText.ReLoad();
                break;
            case 5:

                break;
        }

        selectObj.SetActive(true);
        buyButton.SetActive(false);;      

        selectText.text = LocalizationManager.instance.GetString("Select");

        //SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        //NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        Selected();
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
        selectObj.SetActive(true);
        buyButton.SetActive(false);
        
        selectText.text = LocalizationManager.instance.GetString("Select");
    }

    public void BuyPurchase(int number)
    {
        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        switch (number)
        {
            case 3:
                if (playerDataBase.RemoveAds) return;

                PlayfabManager.instance.PurchaseRemoveAd();

                NotionManager.instance.UseNotion(NotionType.SuccessRemoveAds);

                Invoke("ContentDelay", 0.5f);
                break;
            case 4:
                PortionManager.instance.GetAllPortion(10);
                break;
            case 5:
                PortionManager.instance.GetAllPortion(20);
                break;
            case 6:
                PortionManager.instance.GetAllPortion(40);
                break;
            case 7:
                if (playerDataBase.GoldX2) return;

                PlayfabManager.instance.PurchaseGoldX2();

                Invoke("ContentDelay2", 0.5f);
                break;
            case 8:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 600);

                playerDataBase.BuyCrystal += 600;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack1");
                break;
            case 9:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 3600);

                playerDataBase.BuyCrystal += 3600;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack2");
                break;
            case 10:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 8400);

                playerDataBase.BuyCrystal += 8400;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack3");
                break;
            case 11:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 19200);

                playerDataBase.BuyCrystal += 19200;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack4");
                break;
            case 12:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 59400);

                playerDataBase.BuyCrystal += 59400;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack5");
                break;
            case 13:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 120000);

                playerDataBase.BuyCrystal += 120000;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack6");
                break;
            case 14:
                PortionManager.instance.GetBuffTickets(10);
                break;
            case 15:
                PortionManager.instance.GetBuffTickets(20);
                break;
            case 16:
                PortionManager.instance.GetBuffTickets(40);
                break;
            case 17:
                PortionManager.instance.GetDefTickets(30);
                break;
            case 18:
                PortionManager.instance.GetDefTickets(60);
                break;
            case 19:
                PortionManager.instance.GetDefTickets(120);
                break;
            case 20:
                if (playerDataBase.SuperOffline) return;

                PlayfabManager.instance.PurchaseSuperOffline();

                Invoke("ContentDelay3", 0.5f);
                break;
            case 21:
                if (playerDataBase.AutoUpgrade) return;

                PlayfabManager.instance.PurchaseAutoUpgrade();

                Invoke("ContentDelay4", 0.5f);
                break;
            case 22:
                if (playerDataBase.AutoPresent) return;

                PlayfabManager.instance.PurchaseAutoPresent();

                Invoke("ContentDelay5", 0.5f);
                break;
        }

        //shopContents[19].Initialize(ItemType.Portion1, BuyType.Crystal, this);
        //shopContents[20].Initialize(ItemType.Portion2, BuyType.Crystal, this);
        //shopContents[21].Initialize(ItemType.Portion3, BuyType.Crystal, this);
        //shopContents[22].Initialize(ItemType.Portion4, BuyType.Crystal, this);
        //shopContents[23].Initialize(ItemType.Portion5, BuyType.Crystal, this);

        GameManager.instance.CheckPortion();
        GameManager.instance.CheckDefTicket();
    }

    public void BuyPackage(PackageType type)
    {
        StartCoroutine(BuyPackageCoroution(type));
    }

    IEnumerator BuyPackageCoroution(PackageType type)
    {
        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        switch (type)
        {
            case PackageType.Package1:
                playerDataBase.Package1 = true;
                Invoke("PackageDelay1", 0.5f);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package1", 1);

                PlayfabManager.instance.UpdateAddGold(6000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 600);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(10);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(10);
                break;
            case PackageType.Package2:
                playerDataBase.Package2 = true;
                Invoke("PackageDelay2", 0.5f);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package2", 1);

                PlayfabManager.instance.UpdateAddGold(180000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 8500);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(40);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(50);
                break;
            case PackageType.Package3:
                playerDataBase.Package3 = true;
                Invoke("PackageDelay3", 0.5f);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package3", 1);

                PlayfabManager.instance.UpdateAddGold(600000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 30000);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(100);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(100);
                break;
            case PackageType.Package4:
                playerDataBase.Package4 = true;
                Invoke("PackageDelay4", 0.5f);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package4", 1);

                PlayfabManager.instance.UpdateAddGold(600000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 42000);

                yield return waitForSeconds;

                PortionManager.instance.GetDefTickets(200);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(200);
                break;
            case PackageType.Package5: //한정 패키지
                playerDataBase.Package5 = true;
                Invoke("PackageDelay5", 0.5f);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package5", 1);

                PlayfabManager.instance.UpdateAddGold(12000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1200);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(20);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(10);
                break;
            case PackageType.Package6: //울트라 패키지
                playerDataBase.Package6 = true;
                Invoke("PackageDelay6", 0.5f);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package6", 1);

                PlayfabManager.instance.PurchaseRemoveAd();

                yield return waitForSeconds;

                PlayfabManager.instance.PurchaseGoldX2();

                yield return waitForSeconds;

                PlayfabManager.instance.PurchaseAutoUpgrade();

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(200);
                break;
            case PackageType.Package7: //서포트 패키지
                playerDataBase.Package7 += 1;
                Invoke("PackageDelay7", 0.5f);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package7", playerDataBase.Package7);

                PlayfabManager.instance.UpdateAddGold(6000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 600);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(10);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(10);
                break;
        }

        FirebaseAnalytics.LogEvent("Buy_Package : " + type.ToString());

        //if (playerDataBase.Package1 && playerDataBase.Package2 && playerDataBase.Package3 && playerDataBase.Package4
        //     && playerDataBase.Package5 && playerDataBase.Package6)
        //{
        //    packageThanks.SetActive(true);
        //}
    }

    void ContentDelay()
    {
        shopContents[7].gameObject.SetActive(false);

        removeAds.SetActive(true);
    }

    void ContentDelay2()
    {
        shopContents[12].gameObject.SetActive(false);

        goldx2.SetActive(true);
    }

    void ContentDelay3()
    {
        shopContents[32].gameObject.SetActive(false);

        superOffline.SetActive(true);
    }

    void ContentDelay4()
    {
        shopContents[34].gameObject.SetActive(false);

        autoUpgrade.SetActive(true);
    }

    void ContentDelay5()
    {
        shopContents[35].gameObject.SetActive(false);

        autoPresent.SetActive(true);
    }

    void PackageDelay1()
    {
        packageContents[1].gameObject.SetActive(false);
    }

    void PackageDelay2()
    {
        packageContents[2].gameObject.SetActive(false);
    }

    void PackageDelay3()
    {
        packageContents[3].gameObject.SetActive(false);
    }

    void PackageDelay4()
    {
        packageContents[4].gameObject.SetActive(false);
    }

    void PackageDelay5()
    {
        packageContents[0].gameObject.SetActive(false);
    }

    void PackageDelay6()
    {
        packageContents[5].gameObject.SetActive(false);
    }

    void PackageDelay7()
    {
        package.SetActive(false);
    }

    public void Failed()
    {
        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        NotionManager.instance.UseNotion(NotionType.CancelPurchase);
    }

    public void DanceMotion()
    {
        switch(speicalIndex)
        {
            case 0:
                shopAnimalArray[animalIndex].GetComponent<Animator>().SetBool("YummyTime", true);
                break;
            case 1:
                shopTruckArray[truckIndex].GetComponent<Animator>().SetBool("YummyTime", true);
                break;
            case 2:
                shopCharacterArray[characterIndex].GetComponent<Animator>().SetBool("YummyTime", true);
                break;
            case 3:
                shopButterflyArray[butterflyIndex].GetComponent<Animator>().SetBool("YummyTime", true);
                break;
        }
    }

    public void OpenPackage(PackageType type)
    {
        package.SetActive(true);

        packageContent.Initialize(type, this);

        isPackageDelay = true;
        Invoke("PackageDelay", 2.0f);
    }

    void PackageDelay()
    {
        isPackageDelay = false;
    }

    public void ClosePackage()
    {
        if (isPackageDelay) return;

        package.SetActive(false);
    }

    public void OpenLimitPackage()
    {
        packageBuyIcon.SetActive(true);

        OpenPackage(PackageType.Package5);
    }

    public void OffLimitPackage()
    {
        packageContents[0].gameObject.SetActive(false);

        package.SetActive(false);

        packageBuyIcon.SetActive(false);
    }

    public void OpenRankPointInfo()
    {
        ReceiveInfoManager.instance.OpenReceiveInfo(RewardType.RankPoint, 2);
    }
}
