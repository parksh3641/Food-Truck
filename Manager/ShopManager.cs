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
    public static ShopManager instance;

    public GameObject shopView;
    public GameObject speicalShopView;

    public GameObject shopAlarm;

    public GameObject martAlarm;
    public GameObject[] speicalAlarm;
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
    public Text abilityPointText;

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

    public GameObject mainBucket;
    public GameObject[] mainBucketArray;
    public GameObject[] shopBucketArray;

    public GameObject mainChair;
    public GameObject[] mainChairArray;
    public GameObject[] mainChairArray2;
    public GameObject[] shopChairArray;

    public GameObject mainTube;
    public GameObject[] mainTubeArray;
    public GameObject[] shopTubeArray;

    public GameObject mainSurfboard;
    public GameObject[] mainSurfboardArray;
    public GameObject[] shopSurfboardArray;

    public GameObject mainUmbrella;
    public GameObject[] mainUmbrellaArray;
    public GameObject[] mainUmbrellaArray2;
    public GameObject[] shopUmbrellaArray;

    public GameObject buyButton;
    public GameObject buySpeical;
    public Text buySpeicalText;
    public GameObject selectObj;
    public GameObject levelUpObj;
    public GameObject selectCheckMarkObj;

    public Text selectText;
    public Text priceText;
    public Text crystalText;
    public Text eventText;
    public Text levelUpText;

    public GameObject goldButton;
    public GameObject crystalButton;
    public GameObject eventButton;

    public Text levelText;
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
    private int bucketIndex = 0;
    private int chairIndex = 0;
    private int tubeIndex = 0;
    private int surfboardIndex = 0;
    private int umbrellaIndex = 0;

    bool hold = false;
    bool buy = false;
    bool isDelay = false;
    bool isPackageDelay = false;

    private long price_Gold = 0;
    private long price_Crystal = 0;
    private int price_LevelUp = 0;
    private int exchange = 10000;

    private long lowCoin, needExchangeCrystal = 0;

    private int level = 0;

    public GuideMissionManager guideMissionManager;

    CharacterInfo characterInfo = new CharacterInfo();
    TruckInfo truckInfo = new TruckInfo();
    AnimalInfo animalInfo = new AnimalInfo();
    ButterflyInfo butterflyInfo = new ButterflyInfo();
    TotemsInfo totemsInfo = new TotemsInfo();
    BucketInfo bucketInfo = new BucketInfo();
    ChairInfo chairInfo = new ChairInfo();
    TubeInfo tubeInfo = new TubeInfo();
    SurfboardInfo surfboardInfo = new SurfboardInfo();
    UmbrellaInfo umbrellaInfo = new UmbrellaInfo();

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
    EtcDataBase etcDataBase;

    Dictionary<string, string> customData = new Dictionary<string, string>();

    private void Awake()
    {
        instance = this;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        if (characterDataBase == null) characterDataBase = Resources.Load("CharacterDataBase") as CharacterDataBase;
        if (truckDataBase == null) truckDataBase = Resources.Load("TruckDataBase") as TruckDataBase;
        if (animalDataBase == null) animalDataBase = Resources.Load("AnimalDataBase") as AnimalDataBase;
        if (butterflyDataBase == null) butterflyDataBase = Resources.Load("ButterflyDataBase") as ButterflyDataBase;
        if (totemsDataBase == null) totemsDataBase = Resources.Load("TotemsDataBase") as TotemsDataBase;
        if (etcDataBase == null) etcDataBase = Resources.Load("EtcDataBase") as EtcDataBase;

        shopView.SetActive(false);
        speicalShopView.SetActive(false);
        changeMoneyView.SetActive(false);

        dailyShopCountText.text = "";

        package.SetActive(false);
        shopAlarm.SetActive(true);
        martAlarm.SetActive(true);

        packageBuyIcon.SetActive(false);

        for (int i = 0; i < speicalAlarm.Length; i++)
        {
            speicalAlarm[i].SetActive(true);
        }

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

        for (int i = 0; i < mainBucketArray.Length; i++)
        {
            mainBucketArray[i].SetActive(false);
        }

        for (int i = 0; i < mainChairArray.Length; i++)
        {
            mainChairArray[i].SetActive(false);
            mainChairArray2[i].SetActive(false);
        }

        for (int i = 0; i < mainTubeArray.Length; i++)
        {
            mainTubeArray[i].SetActive(false);
        }

        for (int i = 0; i < mainSurfboardArray.Length; i++)
        {
            mainSurfboardArray[i].SetActive(false);
        }

        for (int i = 0; i < mainUmbrellaArray.Length; i++)
        {
            mainUmbrellaArray[i].SetActive(false);
            mainUmbrellaArray2[i].SetActive(false);
        }
    }

    private void Start()
    {
        for(int i = 0; i < shopRectTransform.Length; i ++)
        {
            shopRectTransform[i].anchoredPosition = new Vector2(0, -9999);
        }
    }

    [Button]
    public void Initialize()
    {
        if (GameStateManager.instance.YoutubeVideo)
        {
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

            for (int i = 0; i < mainBucketArray.Length; i++)
            {
                mainBucketArray[i].SetActive(false);
            }

            for (int i = 0; i < mainChairArray.Length; i++)
            {
                mainChairArray[i].SetActive(false);
                mainChairArray2[i].SetActive(false);
            }

            for (int i = 0; i < mainTubeArray.Length; i++)
            {
                mainTubeArray[i].SetActive(false);
            }

            for (int i = 0; i < mainSurfboardArray.Length; i++)
            {
                mainSurfboardArray[i].SetActive(false);
            }

            for (int i = 0; i < mainUmbrellaArray.Length; i++)
            {
                mainUmbrellaArray[i].SetActive(false);
                mainUmbrellaArray2[i].SetActive(false);
            }

            int number = Random.Range(0, Enum.GetValues(typeof(CharacterType)).Length - 1);
            int number2 = Random.Range(0, Enum.GetValues(typeof(TruckType)).Length - 1);
            int number3 = Random.Range(0, Enum.GetValues(typeof(AnimalType)).Length - 1);
            int number4 = Random.Range(0, Enum.GetValues(typeof(ButterflyType)).Length - 1);
            int number5 = Random.Range(0, Enum.GetValues(typeof(TotemsType)).Length - 1);
            int number6 = Random.Range(0, Enum.GetValues(typeof(BucketType)).Length - 1);
            int number7 = Random.Range(0, Enum.GetValues(typeof(ChairType)).Length - 1);
            int number8 = Random.Range(0, Enum.GetValues(typeof(TubeType)).Length - 1);
            int number9 = Random.Range(0, Enum.GetValues(typeof(SurfboardType)).Length - 1);
            int number10 = Random.Range(0, Enum.GetValues(typeof(UmbrellaType)).Length - 1);

            GameStateManager.instance.CharacterType = CharacterType.Character1 + number;
            GameStateManager.instance.TruckType = TruckType.Truck1 + number2;
            GameStateManager.instance.AnimalType = AnimalType.Animal1 + number3;
            GameStateManager.instance.ButterflyType = ButterflyType.Butterfly1 + number4;
            GameStateManager.instance.TotemsType = TotemsType.Totems1 + number5;
            GameStateManager.instance.BucketType = BucketType.Bucket1 + number6;
            GameStateManager.instance.ChairType = ChairType.Chair1 + number7;
            GameStateManager.instance.TubeType = TubeType.Tube1 + number8;
            GameStateManager.instance.SurfboardType = SurfboardType.Surfboard1 + number9;
            GameStateManager.instance.UmbrellaType = UmbrellaType.Umbrella1 + number10;

            mainCharacterArray[number].SetActive(true);
            mainTruckArray[number2].SetActive(true);
            mainAnimalArray[number3].SetActive(true);
            mainButterflyArray[number4].SetActive(true);
            mainTotemsArray[number5].SetActive(true);

            mainBucketArray[number6].SetActive(true);

            mainChairArray[number7].SetActive(true);
            mainChairArray2[number7].SetActive(true);

            mainTubeArray[number8].SetActive(true);
            mainSurfboardArray[number9].SetActive(true);

            mainUmbrellaArray[number10].SetActive(true);
            mainUmbrellaArray2[number10].SetActive(true);

            GameStateManager.instance.IslandType = IslandType.Island1 + Random.Range(1, System.Enum.GetValues(typeof(IslandType)).Length - 1);
            GameStateManager.instance.FoodType = FoodType.Food1 + Random.Range(0, System.Enum.GetValues(typeof(FoodType)).Length - 1);

            GameStateManager.instance.FeverCount = 28000;

            GameManager.instance.Initialize();

            Debug.LogError("유튜브 업로드 세팅");
        }
        else
        {
            mainCharacterArray[(int)GameStateManager.instance.CharacterType].SetActive(true);
            mainTruckArray[(int)GameStateManager.instance.TruckType].SetActive(true);
            mainAnimalArray[(int)GameStateManager.instance.AnimalType].SetActive(true);
            mainButterflyArray[(int)GameStateManager.instance.ButterflyType].SetActive(true);
            mainTotemsArray[(int)GameStateManager.instance.TotemsType].SetActive(true);

            mainBucketArray[(int)GameStateManager.instance.BucketType].SetActive(true);

            mainChairArray[(int)GameStateManager.instance.ChairType].SetActive(true);
            mainChairArray2[(int)GameStateManager.instance.ChairType].SetActive(true);

            mainTubeArray[(int)GameStateManager.instance.TubeType].SetActive(true);
            mainSurfboardArray[(int)GameStateManager.instance.SurfboardType].SetActive(true);

            mainUmbrellaArray[(int)GameStateManager.instance.UmbrellaType].SetActive(true);
            mainUmbrellaArray2[(int)GameStateManager.instance.UmbrellaType].SetActive(true);
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

            if (playerDataBase.InGameTutorial == 1 && GameStateManager.instance.StoreType != StoreType.OneStore)
            {
                OpenLimitPackage();
            }
        }
    }

    public void SetAlarm()
    {
        martAlarm.SetActive(true);
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

    public void OpenShopItemView()
    {
        shopView.SetActive(false);

        OpenShopView();

        if (shopView.activeInHierarchy)
        {
            ChangeTopToggle(2);
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
        if (!shopView.activeInHierarchy)
        {
            shopView.SetActive(true);

            GameManager.instance.RenewalVC();

            martAlarm.SetActive(false);

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

            rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                //shopContents[7].Initialize(ItemType.RemoveAds, BuyType.Rm, this);
                shopContents[7].gameObject.SetActive(false);
                shopContents[11].Initialize(ItemType.DailyReward_Portion, BuyType.Free, this);
                shopContents[12].Initialize(ItemType.GoldX2, BuyType.Rm, this);
                shopContents[12].gameObject.SetActive(false);
                shopContents[24].Initialize(ItemType.DefDestroyTicketSlices, BuyType.Exchange, this);
                shopContents[25].Initialize(ItemType.DefDestroyTicketPiece, BuyType.Free, this);
                shopContents[32].Initialize(ItemType.SuperOffline, BuyType.Rm, this);
                shopContents[34].Initialize(ItemType.AutoUpgrade, BuyType.Rm, this);
                shopContents[34].gameObject.SetActive(false);
                shopContents[35].Initialize(ItemType.AutoPresent, BuyType.Rm, this);

                if (playerDataBase.resetInfo.dailyReward == 0)
                {
                    shopContents[0].SetLocked(false);
                }
                else
                {
                    shopContents[0].SetLocked(true);
                }

                if (playerDataBase.resetInfo.dailyReward_Portion == 0)
                {
                    shopContents[11].SetLocked(false);
                }
                else
                {
                    shopContents[11].SetLocked(true);
                }

                if (playerDataBase.resetInfo.dailyReward_DefTicket == 0)
                {
                    shopContents[25].SetLocked(false);
                }
                else
                {
                    shopContents[25].SetLocked(true);
                }

                if (playerDataBase.resetInfo.dailyAdsReward == 0)
                {
                    shopContents[1].SetLocked(false);
                }
                else
                {
                    shopContents[1].SetLocked(true);
                }

                if (playerDataBase.resetInfo.dailyAdsReward2 == 0)
                {
                    shopContents[6].SetLocked(false);
                }
                else
                {
                    shopContents[6].SetLocked(true);
                }


                //if (playerDataBase.RemoveAds)
                //{
                //    shopContents[7].gameObject.SetActive(false);
                //}

                //if(playerDataBase.GoldX2)
                //{
                //    shopContents[12].gameObject.SetActive(false);
                //}

                if (playerDataBase.SuperOffline)
                {
                    shopContents[32].gameObject.SetActive(false);
                }

                //if (playerDataBase.AutoUpgrade)
                //{
                //    shopContents[34].gameObject.SetActive(false);
                //}

                if (playerDataBase.AutoPresent)
                {
                    shopContents[35].gameObject.SetActive(false);
                }

                break;
            case 1:
                FirebaseAnalytics.LogEvent("Open_Shop_Package");

                packageThanks.SetActive(false);

                packageAlarm[0].SetActive(false);

                if (GameStateManager.instance.StoreType == StoreType.OneStore)
                {
                    packageContents[0].gameObject.SetActive(false);
                    packageContents[1].gameObject.SetActive(false);
                    packageContents[2].gameObject.SetActive(false);
                    packageContents[3].gameObject.SetActive(false);
                    packageContents[4].gameObject.SetActive(false);
                    packageContents[5].gameObject.SetActive(false);
                    packageContents[8].gameObject.SetActive(false);
                    packageContents[9].gameObject.SetActive(false);
                }

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

                if (playerDataBase.Package8 == 1)
                {
                    packageContents[6].gameObject.SetActive(false);
                }
                else
                {
                    packageContents[6].Initialize(PackageType.Package8, this);
                }

                if (playerDataBase.Package9 == 1)
                {
                    packageContents[7].gameObject.SetActive(false);
                }
                else
                {
                    packageContents[7].Initialize(PackageType.Package9, this);
                }

                if (playerDataBase.Package10)
                {
                    packageContents[8].gameObject.SetActive(false);
                }
                else
                {
                    packageContents[8].Initialize(PackageType.Package10, this);
                }

                if (playerDataBase.Package11)
                {
                    packageContents[9].gameObject.SetActive(false);
                }
                else
                {
                    packageContents[9].Initialize(PackageType.Package11, this);
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

                if (playerDataBase.resetInfo.dailyReward_Crystal == 0)
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
                if (playerDataBase.resetInfo.dailyReward == 1) return;

                ResetManager.instance.SetResetInfo(ResetType.DailyReward);

                shopContents[0].SetLocked(true);

                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessReward);

                PlayfabManager.instance.UpdateSellPriceGold(StageManager.GetGold());
                PlayfabManager.instance.moneyAnimation.PlusMoney(StageManager.GetGold());
                break;
            case ItemType.AdReward_Gold:
                GoogleAdsManager.instance.admobReward_Gold.ShowAd(0);

                break;
            case ItemType.DefDestroyTicket:
                if (playerDataBase.RankPoint >= 1000)
                {
                    playerDataBase.RankPoint -= 1000;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                changeMoneyReceiveContent.Initialize(RewardType.Gold, 500000);
                changeMoneyText.text = MoneyUnitString.ToCurrencyString(60);

                break;
            case ItemType.GoldShop2:
                changeMoneyIndex = 1;
                OpenChangeMoneyView();
                changeMoneyReceiveContent.Initialize(RewardType.Gold2, 4500000);
                changeMoneyText.text = MoneyUnitString.ToCurrencyString(500);

                break;
            case ItemType.GoldShop3:
                changeMoneyIndex = 2;
                OpenChangeMoneyView();
                changeMoneyReceiveContent.Initialize(RewardType.Gold3, 10000000);
                changeMoneyText.text = MoneyUnitString.ToCurrencyString(1200);

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
                if (playerDataBase.resetInfo.dailyReward_Portion == 1) return;

                ResetManager.instance.SetResetInfo(ResetType.DailyReward_Portion);

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
                if (playerDataBase.RankPoint >= 500)
                {
                    playerDataBase.RankPoint -= 500;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                if (playerDataBase.RankPoint >= 500)
                {
                    playerDataBase.RankPoint -= 500;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                if (playerDataBase.RankPoint >= 500)
                {
                    playerDataBase.RankPoint -= 500;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                if (playerDataBase.RankPoint >= 500)
                {
                    playerDataBase.RankPoint -= 500;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                if (playerDataBase.RankPoint >= 750)
                {
                    playerDataBase.RankPoint -= 750;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                if (playerDataBase.DefDestroyTicketPiece >= 3)
                {
                    playerDataBase.DefDestroyTicketPiece -= 3;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicketPiece", playerDataBase.DefDestroyTicketPiece);

                    PortionManager.instance.GetDefTickets(1);

                    shopContents[24].Initialize(ItemType.DefDestroyTicketSlices, BuyType.Exchange, this);
                    shopContents[25].Initialize(ItemType.DefDestroyTicketPiece, BuyType.Free, this);

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
                if (playerDataBase.resetInfo.dailyReward_DefTicket == 1) return;

                ResetManager.instance.SetResetInfo(ResetType.DailyReward_DefTicket);

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

                    PortionManager.instance.GetBuffTickets(5);

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

                    PortionManager.instance.GetBuffTickets(10);

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
                //if (playerDataBase.Crystal >= 6000)
                //{
                //    if (playerDataBase.AutoUpgrade) return;

                //    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 6000);

                //    PlayfabManager.instance.PurchaseAutoUpgrade();

                //    Invoke("ContentDelay4", 0.5f);

                //    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                //    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                //}
                //else
                //{
                //    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                //    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                //}
                break;
            case ItemType.AutoPresent:
                //if (playerDataBase.Crystal >= 3000)
                //{
                //    if (playerDataBase.AutoPresent) return;

                //    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 3000);

                //    PlayfabManager.instance.PurchaseAutoPresent();

                //    Invoke("ContentDelay5", 0.5f);

                //    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                //    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                //}
                //else
                //{
                //    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                //    NotionManager.instance.UseNotion(NotionType.LowCrystal);
                //}
                break;
            case ItemType.BuffTicket:
                if (playerDataBase.RankPoint >= 2000)
                {
                    playerDataBase.RankPoint -= 2000;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                if (playerDataBase.RankPoint >= 1000)
                {
                    playerDataBase.RankPoint -= 1000;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                if (playerDataBase.RankPoint >= 50)
                {
                    playerDataBase.RankPoint -= 50;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
                if (playerDataBase.RankPoint >= 500)
                {
                    playerDataBase.RankPoint -= 500;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

                    rankPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.RankPoint);

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
            case ItemType.AbilityPoint:
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
        Invoke("Delay", 0.4f);
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
                if (playerDataBase.Crystal >= 60)
                {
                    PlayfabManager.instance.UpdateSellPriceGold(500000);
                    PlayfabManager.instance.moneyAnimation.PlusMoney(500000);
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 60);

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
                if (playerDataBase.Crystal >= 500)
                {
                    PlayfabManager.instance.UpdateSellPriceGold(4500000);
                    PlayfabManager.instance.moneyAnimation.PlusMoney(4500000);
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 500);

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
                if (playerDataBase.Crystal >= 1200)
                {
                    PlayfabManager.instance.UpdateSellPriceGold(10000000);
                    PlayfabManager.instance.moneyAnimation.PlusMoney(10000000);
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
            case 3:
                if(playerDataBase.Crystal >= needExchangeCrystal)
                {
                    PlayfabManager.instance.UpdateSellPriceGold((int)lowCoin);
                    PlayfabManager.instance.moneyAnimation.PlusMoney((int)lowCoin);
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, (int)needExchangeCrystal);

                    Debug.Log("부족한 골드만큼 구매 완료");

                    changeMoneyView.SetActive(false);

                    SoundManager.instance.PlaySFX(GameSfxType.Purchase);
                    NotionManager.instance.UseNotion(NotionType.SuccessBuy);
                }
                else
                {
                    changeMoneyView.SetActive(false);

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
        if (playerDataBase.resetInfo.dailyAdsReward == 1) return;

        ResetManager.instance.SetResetInfo(ResetType.DailyAdsReward);

        shopContents[1].SetLocked(true);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessWatchAd);

        PlayfabManager.instance.UpdateSellPriceGold(StageManager.GetGold() * 10);
        PlayfabManager.instance.moneyAnimation.PlusMoney(StageManager.GetGold() * 10);
    }

    public void SuccessWatchAd_Portion()
    {
        if (playerDataBase.resetInfo.dailyAdsReward2 == 1) return;

        ResetManager.instance.SetResetInfo(ResetType.DailyAdsReward2);

        shopContents[6].SetLocked(true);

        PortionManager.instance.GetRandomPortion(10);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }

    public void SuccessWatchAd_Crystal()
    {
        if (playerDataBase.resetInfo.dailyReward_Crystal == 1) return;

        ResetManager.instance.SetResetInfo(ResetType.DailyReward_Crystal);

        shopContents[33].SetLocked(true);

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 30);

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

            abilityPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.AbilityPoint);

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

                    shopAnimalArray[animalIndex].GetComponent<Animator>().SetBool("YummyTime", false);

                    FirebaseAnalytics.LogEvent("Open_Pet");
                    break;
                case 1:
                    for (int i = 0; i < shopTruckArray.Length; i++)
                    {
                        shopTruckArray[i].transform.localRotation = Quaternion.Euler(0, 210, 0);
                    }

                    shopTruckArray[truckIndex].GetComponent<Animator>().SetBool("YummyTime", false);

                    FirebaseAnalytics.LogEvent("Open_FoodTruck");
                    break;
                case 2:
                    for (int i = 0; i < shopCharacterArray.Length; i++)
                    {
                        shopCharacterArray[i].transform.localRotation = Quaternion.Euler(0, 210, 0);
                    }

                    shopCharacterArray[characterIndex].GetComponent<Animator>().SetBool("YummyTime", false);

                    FirebaseAnalytics.LogEvent("Open_Character");
                    break;
                case 3:
                    for (int i = 0; i < shopButterflyArray.Length; i++)
                    {
                        shopButterflyArray[i].transform.localRotation = Quaternion.Euler(0, 210, 0);
                    }

                    shopButterflyArray[butterflyIndex].GetComponent<Animator>().SetBool("YummyTime", false);

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
                    for (int i = 0; i < shopBucketArray.Length; i++)
                    {
                        shopBucketArray[i].transform.localRotation = Quaternion.Euler(-100, 0, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_Bucket");
                    break;
                case 6:
                    for (int i = 0; i < shopChairArray.Length; i++)
                    {
                        shopChairArray[i].transform.localRotation = Quaternion.Euler(-90, 150, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_Chair");
                    break;
                case 7:
                    for (int i = 0; i < shopTubeArray.Length; i++)
                    {
                        shopTubeArray[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_Tube");
                    break;
                case 8:
                    for (int i = 0; i < shopSurfboardArray.Length; i++)
                    {
                        shopSurfboardArray[i].transform.localRotation = Quaternion.Euler(-90, 0, 150);
                    }

                    FirebaseAnalytics.LogEvent("Open_Surfboard");
                    break;
                case 9:
                    for (int i = 0; i < shopUmbrellaArray.Length; i++)
                    {
                        shopUmbrellaArray[i].transform.localRotation = Quaternion.Euler(-100, 0, 0);
                    }

                    FirebaseAnalytics.LogEvent("Open_Umbrella");
                    break;
            }
        }
        else
        {
            guideMissionManager.Initialize();

            GameManager.instance.CheckPercent();

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

        //if (number == 3)
        //{
        //    if (playerDataBase.Level < 10)
        //    {
        //        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        //        NotionManager.instance.UseNotion3(Color.yellow, LocalizationManager.instance.GetString("BufferflyLocked"));
        //        return;
        //    }
        //}
        //else if (number == 4)
        //{
        //    if (playerDataBase.Level < 15)
        //    {
        //        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        //        NotionManager.instance.UseNotion3(Color.yellow, LocalizationManager.instance.GetString("TotemLocked"));
        //        return;
        //    }
        //}

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
        mainBucket.SetActive(false);
        mainChair.SetActive(false);
        mainTube.SetActive(false);
        mainSurfboard.SetActive(false);
        mainUmbrella.SetActive(false);

        yummyButton.SetActive(true);

        leftButton.SetActive(true);
        rightButton.SetActive(true);

        speicalAlarm[number].SetActive(false);

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

                if (bucketIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (bucketIndex >= shopBucketArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                BucketInitialize();
                break;
            case 6:
                yummyButton.SetActive(false);

                if (chairIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (chairIndex >= shopChairArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                ChairInitialize();
                break;
            case 7:
                yummyButton.SetActive(false);

                if (tubeIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (tubeIndex >= shopTubeArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                TubeInitialize();
                break;
            case 8:
                yummyButton.SetActive(false);

                if (surfboardIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (surfboardIndex >= shopSurfboardArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                SurfboardInitialize();
                break;
            case 9:
                yummyButton.SetActive(false);

                if (umbrellaIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (umbrellaIndex >= shopUmbrellaArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                UmbrellaInitialize();
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
        effectText.plusText = " : +" + MoneyUnitString.ToCurrencyString((long)characterInfo.effectNumber).ToString() + "%";

        titleText.localizationName = "ChangeCharacter";
        titleText.plusText = "  ( " + (characterIndex + 1) + " / " + shopCharacterArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " +  ((((playerDataBase.GetCharacterNumber() + 1) * 1.0f) / (shopCharacterArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = "Character" + (characterIndex + 1) + "_Info";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = characterInfo.price;
        price_Crystal = price_Gold / exchange;
        //price_Crystal = Mathf.RoundToInt(price_Crystal2 / 100.0f) * 100;

        if(price_Crystal < 100)
        {
            price_Crystal = 100;
        }

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        passiveText.text = "";

        level = playerDataBase.GetCharacter_AbilityLevel((int)characterInfo.characterType);

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
        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";

        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }

                price_LevelUp = characterDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(characterDataBase.retentionEffect.ToString()) + " +"
+ (level * characterDataBase.retentionValue).ToString("N1") + "%";
            }

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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(true);
            crystalButton.SetActive(false);
            eventButton.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }

            if (characterInfo.characterType == CharacterType.Character21)
            {
                buyButton.SetActive(false);
                buySpeical.SetActive(true);
                buySpeicalText.text = LocalizationManager.instance.GetString("BuyKakoTalk");
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

        truckInfo = truckDataBase.GetTruckInfo(TruckType.Truck1 + truckIndex);

        nameText.localizationName = (TruckType.Truck1 + truckIndex).ToString();
        //passiveText.text = "";

        effectText.localizationName = truckInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + MoneyUnitString.ToCurrencyString((long)truckInfo.effectNumber).ToString() + "%";

        titleText.localizationName = "ChangeTruck";
        titleText.plusText = "  ( " + (truckIndex + 1) + " / " + shopTruckArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetTruckNumber() + 1) * 1.0f) / (shopTruckArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = (TruckType.Truck1 + truckIndex) + "Info";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = truckInfo.price;
        price_Crystal = truckInfo.price;

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);
        eventText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket) + "/" + MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        level = playerDataBase.GetTruck_AbilityLevel((int)truckInfo.truckType);

        switch (truckInfo.truckType)
        {
            case TruckType.Truck1:
                hold = true;
                break;
            case TruckType.Truck2:
                if (playerDataBase.Truck2 >= 1)
                {
                    hold = true;
                }

                buy = true;
                break;
            case TruckType.Truck3:
                if (playerDataBase.Truck3 >= 1)
                {
                    hold = true;
                }

                if(playerDataBase.Truck2 >= 1)
                {
                    buy = true;
                }
                break;
            case TruckType.Truck4:
                if (playerDataBase.Truck4 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Truck3 >= 1)
                {
                    buy = true;
                }
                break;
            case TruckType.Truck5:
                if (playerDataBase.Truck5 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Truck4 >= 1)
                {
                    buy = true;
                }
                break;
            case TruckType.Truck6:
                if (playerDataBase.Truck6 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Truck5 >= 1)
                {
                    buy = true;
                }
                break;
            case TruckType.Truck7:
                if (playerDataBase.Truck7 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Truck6 >= 1)
                {
                    buy = true;
                }
                break;
            case TruckType.Truck8:
                if (playerDataBase.Truck8 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Truck7 >= 1)
                {
                    buy = true;
                }
                break;
            case TruckType.Truck9:
                if (playerDataBase.Truck9 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Truck8 >= 1)
                {
                    buy = true;
                }
                break;
            case TruckType.Truck10:
                if (playerDataBase.Truck10 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Truck9 >= 1)
                {
                    buy = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }

                price_LevelUp = truckDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(truckDataBase.retentionEffect.ToString()) + " +"
+ (level * truckDataBase.retentionValue).ToString("N1") + "%";
            }

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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(true);
            crystalButton.SetActive(false);
            eventButton.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }
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

        animalInfo = animalDataBase.GetAnimalInfo(AnimalType.Animal1 + animalIndex);

        nameText.localizationName = (AnimalType.Animal1 + animalIndex).ToString();
        //passiveText.text = "";

        effectText.localizationName = animalInfo.passiveEffect.ToString();
        effectText.plusText = " +" + MoneyUnitString.ToCurrencyString((long)animalInfo.effectNumber).ToString();

        titleText.localizationName = "ChangeAnimal";
        titleText.plusText = "  ( " + (animalIndex + 1) + " / " + shopAnimalArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetAnimalNumber() + 1) * 1.0f) / (shopAnimalArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = (AnimalType.Animal1 + animalIndex) + "Info";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = animalInfo.price;
        price_Crystal = animalInfo.price;

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);
        eventText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket) + "/" + MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        level = playerDataBase.GetAnimal_AbilityLevel((int)animalInfo.animalType);

        switch (animalInfo.animalType)
        {
            case AnimalType.Animal1:
                hold = true;
                break;
            case AnimalType.Animal2:
                if (playerDataBase.Animal2 >= 1)
                {
                    hold = true;
                }

                buy = true;
                break;
            case AnimalType.Animal3:
                if (playerDataBase.Animal3 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Animal2 >= 1)
                {
                    buy = true;
                }
                break;
            case AnimalType.Animal4:
                if (playerDataBase.Animal4 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Animal3 >= 1)
                {
                    buy = true;
                }
                break;
            case AnimalType.Animal5:
                if (playerDataBase.Animal5 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Animal4 >= 1)
                {
                    buy = true;
                }
                break;
            case AnimalType.Animal6:
                if (playerDataBase.Animal6 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Animal5 >= 1)
                {
                    buy = true;
                }
                break;
            case AnimalType.Animal7:
                if (playerDataBase.Animal7 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Animal6 >= 1)
                {
                    buy = true;
                }
                break;
            case AnimalType.Animal8:
                if (playerDataBase.Animal8 >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Animal7 >= 1)
                {
                    buy = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";

        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }


                price_LevelUp = animalDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(animalDataBase.retentionEffect.ToString()) + " +"
+ (level * animalDataBase.retentionValue).ToString("N1") + "%";
            }

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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(true);
            crystalButton.SetActive(false);
            eventButton.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }
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
        effectText.plusText = " : +" + MoneyUnitString.ToCurrencyString((long)butterflyInfo.effectNumber).ToString() + "%";

        titleText.localizationName = "ChangeButterfly";
        titleText.plusText = "  ( " + (butterflyIndex + 1) + " / " + shopButterflyArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetButterflyNumber() + 1) * 1.0f) / (shopButterflyArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = " ";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = butterflyInfo.price;
        price_Crystal = butterflyInfo.price;

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);
        eventText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket) + "/" + MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        level = playerDataBase.GetButterfly_AbilityLevel((int)butterflyInfo.butterflyType);

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

        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }


                price_LevelUp = butterflyDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(butterflyDataBase.retentionEffect.ToString()) + " +"
+ (level * butterflyDataBase.retentionValue).ToString("N1") + "%";
            }

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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(true);
            crystalButton.SetActive(false);
            eventButton.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }
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
        effectText.plusText = " : +" + MoneyUnitString.ToCurrencyString((long)totemsInfo.effectNumber).ToString();

        titleText.localizationName = "ChangeTotems";
        titleText.plusText = "  ( " + (totemsIndex + 1) + " / " + shopTotemsArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetTotemsNumber() + 1) * 1.0f) / (shopTotemsArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = " ";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = totemsInfo.price;
        price_Crystal = totemsInfo.price;

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);
        eventText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket) + "/" + MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        level = playerDataBase.GetTotems_AbilityLevel((int)totemsInfo.totemsType);

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

        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }


                price_LevelUp = totemsDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(totemsDataBase.retentionEffect.ToString()) + " +"
+ (level * totemsDataBase.retentionValue).ToString("N1") + "%";
            }

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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(true);
            crystalButton.SetActive(false);
            eventButton.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }
        }
    }

    void BucketInitialize()
    {
        mainBucket.SetActive(true);

        for (int i = 0; i < shopBucketArray.Length; i++)
        {
            shopBucketArray[i].gameObject.SetActive(false);
        }

        shopBucketArray[bucketIndex].gameObject.SetActive(true);
        shopBucketArray[bucketIndex].transform.localRotation = Quaternion.Euler(-100, 0, 0);

        bucketInfo = etcDataBase.GetBucketInfo(BucketType.Bucket1 + bucketIndex);

        nameText.localizationName = (BucketType.Bucket1 + bucketIndex).ToString();

        effectText.localizationName = bucketInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + MoneyUnitString.ToCurrencyString((long)bucketInfo.effectNumber).ToString() + "%";

        titleText.localizationName = "Bucket";
        titleText.plusText = "  ( " + (bucketIndex + 1) + " / " + shopBucketArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetBucketNumber() + 1) * 1.0f) / (shopBucketArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = " ";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = bucketInfo.price;
        price_Crystal = bucketInfo.price;

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);
        eventText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket) + "/" + MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        level = playerDataBase.GetBucket_AbilityLevel((int)bucketInfo.bucketType);

        switch (bucketInfo.bucketType)
        {
            case BucketType.Bucket1:
                hold = true;
                break;
            case BucketType.Bucket2:
                if (playerDataBase.Bucket[1] >= 1)
                {
                    hold = true;
                }

                buy = true;
                break;
            case BucketType.Bucket3:
                if (playerDataBase.Bucket[2] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Bucket[1] >= 1)
                {
                    buy = true;
                }
                break;
            case BucketType.Bucket4:
                if (playerDataBase.Bucket[3] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Bucket[2] >= 1)
                {
                    buy = true;
                }
                break;
            case BucketType.Bucket5:
                if (playerDataBase.Bucket[4] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Bucket[3] >= 1)
                {
                    buy = true;
                }
                break;
            case BucketType.Bucket6:
                if (playerDataBase.Bucket[5] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Bucket[4] >= 1)
                {
                    buy = true;
                }
                break;
            case BucketType.Bucket7:
                if (playerDataBase.Bucket[6] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Bucket[5] >= 1)
                {
                    buy = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }

                price_LevelUp = etcDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(etcDataBase.bucketInfoList[bucketIndex].retentionEffect.ToString()) + " +"
+ (level * etcDataBase.bucketInfoList[bucketIndex].retentionValue).ToString("N1") + "%";
            }

            if (GameStateManager.instance.BucketType.Equals(bucketInfo.bucketType))
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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(true);
            crystalButton.SetActive(false);
            eventButton.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }
        }
    }

    void ChairInitialize()
    {
        mainChair.SetActive(true);

        for (int i = 0; i < shopChairArray.Length; i++)
        {
            shopChairArray[i].gameObject.SetActive(false);
        }

        shopChairArray[chairIndex].gameObject.SetActive(true);
        shopChairArray[chairIndex].transform.localRotation = Quaternion.Euler(-90, 150, 0);

        chairInfo = etcDataBase.GetChairInfo(ChairType.Chair1 + chairIndex);

        nameText.localizationName = (ChairType.Chair1 + chairIndex).ToString();

        effectText.localizationName = chairInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + MoneyUnitString.ToCurrencyString((long)chairInfo.effectNumber).ToString() + "%";

        titleText.localizationName = "Chair";
        titleText.plusText = "  ( " + (chairIndex + 1) + " / " + shopChairArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetChairNumber() + 1) * 1.0f) / (shopChairArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = " ";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = chairInfo.price;
        price_Crystal = chairInfo.price;

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);
        eventText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket) + "/" + MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        level = playerDataBase.GetChair_AbilityLevel((int)chairInfo.chairType);

        switch (chairInfo.chairType)
        {
            case ChairType.Chair1:
                hold = true;
                break;
            case ChairType.Chair2:
                if (playerDataBase.Chair[1] >= 1)
                {
                    hold = true;
                }

                buy = true;
                break;
            case ChairType.Chair3:
                if (playerDataBase.Chair[2] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Chair[1] >= 1)
                {
                    buy = true;
                }
                break;
            case ChairType.Chair4:
                if (playerDataBase.Chair[3] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Chair[2] >= 1)
                {
                    buy = true;
                }
                break;
            case ChairType.Chair5:
                if (playerDataBase.Chair[4] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Chair[3] >= 1)
                {
                    buy = true;
                }
                break;
            case ChairType.Chair6:
                if (playerDataBase.Chair[5] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Chair[4] >= 1)
                {
                    buy = true;
                }
                break;
            case ChairType.Chair7:
                if (playerDataBase.Chair[6] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Chair[5] >= 1)
                {
                    buy = true;
                }
                break;
            case ChairType.Chair8:
                if (playerDataBase.Chair[7] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Chair[6] >= 1)
                {
                    buy = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }

                price_LevelUp = etcDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(etcDataBase.chairInfoList[chairIndex].retentionEffect.ToString()) + " +"
+ (level * etcDataBase.chairInfoList[chairIndex].retentionValue).ToString("N1") + "%";
            }

            if (GameStateManager.instance.ChairType.Equals(chairInfo.chairType))
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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(false);
            crystalButton.SetActive(true);
            eventButton.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }
        }
    }

    void TubeInitialize()
    {
        mainTube.SetActive(true);

        for (int i = 0; i < shopTubeArray.Length; i++)
        {
            shopTubeArray[i].gameObject.SetActive(false);
        }

        shopTubeArray[tubeIndex].gameObject.SetActive(true);
        shopTubeArray[tubeIndex].transform.localRotation = Quaternion.Euler(0, 0, 0);

        tubeInfo = etcDataBase.GetTubeInfo(TubeType.Tube1 + tubeIndex);

        nameText.localizationName = (TubeType.Tube1 + tubeIndex).ToString();

        effectText.localizationName = tubeInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + MoneyUnitString.ToCurrencyString((long)tubeInfo.effectNumber).ToString() + "%";

        titleText.localizationName = "Tube";
        titleText.plusText = "  ( " + (tubeIndex + 1) + " / " + shopTubeArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetTubeNumber() + 1) * 1.0f) / (shopTubeArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = " ";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = tubeInfo.price;
        price_Crystal = tubeInfo.price;

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);
        eventText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket) + "/" + MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        level = playerDataBase.GetTube_AbilityLevel((int)tubeInfo.tubeType);

        switch (tubeInfo.tubeType)
        {
            case TubeType.Tube1:
                hold = true;
                break;
            case TubeType.Tube2:
                if (playerDataBase.Tube[1] >= 1)
                {
                    hold = true;
                }

                buy = true;
                break;
            case TubeType.Tube3:
                if (playerDataBase.Tube[2] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Tube[1] >= 1)
                {
                    buy = true;
                }
                break;
            case TubeType.Tube4:
                if (playerDataBase.Tube[3] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Tube[2] >= 1)
                {
                    buy = true;
                }
                break;
            case TubeType.Tube5:
                if (playerDataBase.Tube[4] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Tube[3] >= 1)
                {
                    buy = true;
                }
                break;
            case TubeType.Tube6:
                if (playerDataBase.Tube[5] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Tube[4] >= 1)
                {
                    buy = true;
                }
                break;
            case TubeType.Tube7:
                if (playerDataBase.Tube[6] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Tube[5] >= 1)
                {
                    buy = true;
                }
                break;
            case TubeType.Tube8:
                if (playerDataBase.Tube[7] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Tube[6] >= 1)
                {
                    buy = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }

                price_LevelUp = etcDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(etcDataBase.tubeInfoList[tubeIndex].retentionEffect.ToString()) + " +"
+ (level * etcDataBase.tubeInfoList[tubeIndex].retentionValue).ToString("N1") + "%";
            }

            if (GameStateManager.instance.TubeType.Equals(tubeInfo.tubeType))
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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(false);
            crystalButton.SetActive(true);
            eventButton.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }
        }
    }

    void SurfboardInitialize()
    {
        mainSurfboard.SetActive(true);

        for (int i = 0; i < shopSurfboardArray.Length; i++)
        {
            shopSurfboardArray[i].gameObject.SetActive(false);
        }

        shopSurfboardArray[surfboardIndex].gameObject.SetActive(true);
        shopSurfboardArray[surfboardIndex].transform.localRotation = Quaternion.Euler(-90, 0, 150);

        surfboardInfo = etcDataBase.GetSurfboardInfo(SurfboardType.Surfboard1 + surfboardIndex);

        nameText.localizationName = (SurfboardType.Surfboard1 + surfboardIndex).ToString();

        effectText.localizationName = surfboardInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + MoneyUnitString.ToCurrencyString((long)surfboardInfo.effectNumber).ToString() + "%";

        titleText.localizationName = "Surfboard";
        titleText.plusText = "  ( " + (surfboardIndex + 1) + " / " + shopSurfboardArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetSurfboardNumber() + 1) * 1.0f) / (shopSurfboardArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = " ";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = surfboardInfo.price;
        price_Crystal = surfboardInfo.price;

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);
        eventText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket) + "/" + MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        level = playerDataBase.GetSurfboard_AbilityLevel((int)surfboardInfo.surfboardType);

        switch (surfboardInfo.surfboardType)
        {
            case SurfboardType.Surfboard1:
                hold = true;
                break;
            case SurfboardType.Surfboard2:
                if (playerDataBase.Surfboard[1] >= 1)
                {
                    hold = true;
                }

                buy = true;
                break;
            case SurfboardType.Surfboard3:
                if (playerDataBase.Surfboard[2] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Surfboard[1] >= 1)
                {
                    buy = true;
                }
                break;
            case SurfboardType.Surfboard4:
                if (playerDataBase.Surfboard[3] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Surfboard[2] >= 1)
                {
                    buy = true;
                }
                break;
            case SurfboardType.Surfboard5:
                if (playerDataBase.Surfboard[4] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Surfboard[3] >= 1)
                {
                    buy = true;
                }
                break;
            case SurfboardType.Surfboard6:
                if (playerDataBase.Surfboard[5] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Surfboard[4] >= 1)
                {
                    buy = true;
                }
                break;
            case SurfboardType.Surfboard7:
                if (playerDataBase.Surfboard[6] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Surfboard[5] >= 1)
                {
                    buy = true;
                }
                break;
            case SurfboardType.Surfboard8:
                if (playerDataBase.Surfboard[7] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Surfboard[6] >= 1)
                {
                    buy = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }

                price_LevelUp = etcDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(etcDataBase.surfboardInfoList[surfboardIndex].retentionEffect.ToString()) + " +"
+ (level * etcDataBase.surfboardInfoList[surfboardIndex].retentionValue).ToString("N1") + "%";
            }

            if (GameStateManager.instance.SurfboardType.Equals(surfboardInfo.surfboardType))
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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(false);
            crystalButton.SetActive(false);
            eventButton.SetActive(true);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }
        }
    }

    void UmbrellaInitialize()
    {
        mainUmbrella.SetActive(true);

        for (int i = 0; i < shopUmbrellaArray.Length; i++)
        {
            shopUmbrellaArray[i].gameObject.SetActive(false);
        }

        shopUmbrellaArray[umbrellaIndex].gameObject.SetActive(true);
        shopUmbrellaArray[umbrellaIndex].transform.localRotation = Quaternion.Euler(-100, 0, 0);

        umbrellaInfo = etcDataBase.GetUmbrellaInfo(UmbrellaType.Umbrella1 + umbrellaIndex);

        nameText.localizationName = (UmbrellaType.Umbrella1 + umbrellaIndex).ToString();

        effectText.localizationName = umbrellaInfo.passiveEffect.ToString();
        effectText.plusText = " : +" + MoneyUnitString.ToCurrencyString((long)umbrellaInfo.effectNumber).ToString() + "%";

        titleText.localizationName = "Umbrella";
        titleText.plusText = "  ( " + (umbrellaIndex + 1) + " / " + shopUmbrellaArray.Length + " )\n<size=10>"
            + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetUmbrellaNumber() + 1) * 1.0f) / (shopUmbrellaArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
        titleText.ReLoad();

        infoText.localizationName = " ";

        nameText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        price_Gold = umbrellaInfo.price;
        price_Crystal = umbrellaInfo.price;

        priceText.text = MoneyUnitString.ToCurrencyString(price_Gold);
        crystalText.text = MoneyUnitString.ToCurrencyString(price_Crystal);
        eventText.text = MoneyUnitString.ToCurrencyString(playerDataBase.EventTicket) + "/" + MoneyUnitString.ToCurrencyString(price_Crystal);

        hold = false;
        buy = false;

        level = playerDataBase.GetUmbrella_AbilityLevel((int)umbrellaInfo.umbrellaType);

        switch (umbrellaInfo.umbrellaType)
        {
            case UmbrellaType.Umbrella1:
                hold = true;
                break;
            case UmbrellaType.Umbrella2:
                if (playerDataBase.Umbrella[1] >= 1)
                {
                    hold = true;
                }

                buy = true;
                break;
            case UmbrellaType.Umbrella3:
                if (playerDataBase.Umbrella[2] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Umbrella[1] >= 1)
                {
                    buy = true;
                }
                break;
            case UmbrellaType.Umbrella4:
                if (playerDataBase.Umbrella[3] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Umbrella[2] >= 1)
                {
                    buy = true;
                }
                break;
            case UmbrellaType.Umbrella5:
                if (playerDataBase.Umbrella[4] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Umbrella[3] >= 1)
                {
                    buy = true;
                }
                break;
            case UmbrellaType.Umbrella6:
                if (playerDataBase.Umbrella[5] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Umbrella[4] >= 1)
                {
                    buy = true;
                }
                break;
            case UmbrellaType.Umbrella7:
                if (playerDataBase.Umbrella[6] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Umbrella[5] >= 1)
                {
                    buy = true;
                }
                break;
            case UmbrellaType.Umbrella8:
                if (playerDataBase.Umbrella[7] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Umbrella[6] >= 1)
                {
                    buy = true;
                }
                break;
            case UmbrellaType.Umbrella9:
                if (playerDataBase.Umbrella[8] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Umbrella[7] >= 1)
                {
                    buy = true;
                }
                break;
            case UmbrellaType.Umbrella10:
                if (playerDataBase.Umbrella[9] >= 1)
                {
                    hold = true;
                }

                if (playerDataBase.Umbrella[8] >= 1)
                {
                    buy = true;
                }
                break;
        }

#if UNITY_EDITOR
        buy = true;
#endif

        passiveText.text = "";

        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
        if (hold)
        {
            selectObj.SetActive(true);
            levelUpObj.SetActive(true);
            buyButton.SetActive(false);
            buySpeical.SetActive(false);
            selectCheckMarkObj.SetActive(false);

            if (level > 9)
            {
                levelText.text = "★★★★★★★★★★";

                levelUpObj.SetActive(false);
            }
            else
            {
                switch (level)
                {
                    case 0:
                        levelText.text = "☆☆☆☆☆☆☆☆☆☆";
                        break;
                    case 1:
                        levelText.text = "★☆☆☆☆☆☆☆☆☆";
                        break;
                    case 2:
                        levelText.text = "★★☆☆☆☆☆☆☆☆";
                        break;
                    case 3:
                        levelText.text = "★★★☆☆☆☆☆☆☆";
                        break;
                    case 4:
                        levelText.text = "★★★★☆☆☆☆☆☆";
                        break;
                    case 5:
                        levelText.text = "★★★★★☆☆☆☆☆";
                        break;
                    case 6:
                        levelText.text = "★★★★★★☆☆☆☆";
                        break;
                    case 7:
                        levelText.text = "★★★★★★★☆☆☆";
                        break;
                    case 8:
                        levelText.text = "★★★★★★★★☆☆";
                        break;
                    case 9:
                        levelText.text = "★★★★★★★★★☆";
                        break;

                }

                price_LevelUp = etcDataBase.GetRetentionPrice(level);

                levelUpText.text = price_LevelUp.ToString();
            }

            if (level > 0)
            {
                passiveText.text = LocalizationManager.instance.GetString("IconEffect") + " : "
+ LocalizationManager.instance.GetString(etcDataBase.umbrellaInfoList[umbrellaIndex].retentionEffect.ToString()) + " +"
+ (level * etcDataBase.umbrellaInfoList[umbrellaIndex].retentionValue).ToString("N1") + "%";
            }

            if (GameStateManager.instance.UmbrellaType.Equals(umbrellaInfo.umbrellaType))
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
            levelUpObj.SetActive(false);
            buyButton.SetActive(true);
            buySpeical.SetActive(false);
            goldButton.SetActive(true);
            crystalButton.SetActive(false);
            eventButton.SetActive(false);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
                crystalText.text = LocalizationManager.instance.GetString("NotPurchase");
                eventText.text = LocalizationManager.instance.GetString("NotPurchase");
            }

            buyButton.SetActive(false);
            buySpeical.SetActive(true);
            buySpeicalText.text = LocalizationManager.instance.GetString("BuySeasonPass");
        }
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
                if (bucketIndex + 1 < shopBucketArray.Length)
                {
                    bucketIndex += 1;

                    BucketInitialize();
                }

                if (bucketIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (bucketIndex >= shopBucketArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }
                break;
            case 6:
                if (chairIndex + 1 < shopChairArray.Length)
                {
                    chairIndex += 1;

                    ChairInitialize();
                }

                if (chairIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (chairIndex >= shopChairArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }
                break;
            case 7:
                if (tubeIndex + 1 < shopTubeArray.Length)
                {
                    tubeIndex += 1;

                    TubeInitialize();
                }

                if (tubeIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (tubeIndex >= shopTubeArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }
                break;
            case 8:
                if (surfboardIndex + 1 < shopSurfboardArray.Length)
                {
                    surfboardIndex += 1;

                    SurfboardInitialize();
                }

                if (surfboardIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (surfboardIndex >= shopSurfboardArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }
                break;
            case 9:
                if (umbrellaIndex + 1 < shopUmbrellaArray.Length)
                {
                    umbrellaIndex += 1;

                    UmbrellaInitialize();
                }

                if (umbrellaIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (umbrellaIndex >= shopUmbrellaArray.Length - 1)
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
                if (bucketIndex - 1 >= 0)
                {
                    bucketIndex -= 1;

                    BucketInitialize();
                }

                if (bucketIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (bucketIndex >= shopBucketArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 6:
                if (chairIndex - 1 >= 0)
                {
                    chairIndex -= 1;

                    ChairInitialize();
                }

                if (chairIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (chairIndex >= shopChairArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 7:
                if (tubeIndex - 1 >= 0)
                {
                    tubeIndex -= 1;

                    TubeInitialize();
                }

                if (tubeIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (tubeIndex >= shopTubeArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 8:
                if (surfboardIndex - 1 >= 0)
                {
                    surfboardIndex -= 1;

                    SurfboardInitialize();
                }

                if (surfboardIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (surfboardIndex >= shopSurfboardArray.Length - 1)
                {
                    rightButton.SetActive(false);
                }

                break;
            case 9:
                if (umbrellaIndex - 1 >= 0)
                {
                    umbrellaIndex -= 1;

                    UmbrellaInitialize();
                }

                if (umbrellaIndex == 0)
                {
                    leftButton.SetActive(false);
                }

                if (umbrellaIndex >= shopUmbrellaArray.Length - 1)
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

                AnimalInitialize();
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

                TruckInitialize();
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

                CharacterInitialize();
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

                ButterflyInitialize();
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

                TotemsInitialize();
                break;
            case 5:
                if (GameStateManager.instance.BucketType == bucketInfo.bucketType)
                {
                    return;
                }

                GameStateManager.instance.BucketType = bucketInfo.bucketType;

                for (int i = 0; i < mainBucketArray.Length; i++)
                {
                    mainBucketArray[i].SetActive(false);
                }

                mainBucketArray[(int)GameStateManager.instance.BucketType].SetActive(true);
                break;
            case 6:
                if (GameStateManager.instance.ChairType == chairInfo.chairType)
                {
                    return;
                }

                GameStateManager.instance.ChairType = chairInfo.chairType;

                for (int i = 0; i < mainChairArray.Length; i++)
                {
                    mainChairArray[i].SetActive(false);
                    mainChairArray2[i].SetActive(false);
                }

                mainChairArray[(int)GameStateManager.instance.ChairType].SetActive(true);
                mainChairArray2[(int)GameStateManager.instance.ChairType].SetActive(true);
                break;
            case 7:
                if (GameStateManager.instance.TubeType == tubeInfo.tubeType)
                {
                    return;
                }

                GameStateManager.instance.TubeType = tubeInfo.tubeType;

                for (int i = 0; i < mainTubeArray.Length; i++)
                {
                    mainTubeArray[i].SetActive(false);
                }

                mainTubeArray[(int)GameStateManager.instance.TubeType].SetActive(true);
                break;
            case 8:
                if (GameStateManager.instance.SurfboardType == surfboardInfo.surfboardType)
                {
                    return;
                }

                GameStateManager.instance.SurfboardType = surfboardInfo.surfboardType;

                for (int i = 0; i < mainSurfboardArray.Length; i++)
                {
                    mainSurfboardArray[i].SetActive(false);
                }

                mainSurfboardArray[(int)GameStateManager.instance.SurfboardType].SetActive(true);
                break;
            case 9:
                if (GameStateManager.instance.UmbrellaType == umbrellaInfo.umbrellaType)
                {
                    return;
                }

                GameStateManager.instance.UmbrellaType = umbrellaInfo.umbrellaType;

                for (int i = 0; i < mainUmbrellaArray.Length; i++)
                {
                    mainUmbrellaArray[i].SetActive(false);
                    mainUmbrellaArray2[i].SetActive(false);
                }

                mainUmbrellaArray[(int)GameStateManager.instance.UmbrellaType].SetActive(true);
                mainUmbrellaArray2[(int)GameStateManager.instance.UmbrellaType].SetActive(true);
                break;
        }

        NotionManager.instance.UseNotion(NotionType.ChangeNotion);
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

        switch (number)
        {
            case 0:
                GameManager.instance.RenewalVC();

                if (playerDataBase.Coin < price_Gold)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowCoin);

                    LowCoin(price_Gold);

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
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, (int)price_Crystal);
                }
                break;
            case 2:
                if (playerDataBase.EventTicket < price_Crystal)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.LowEventTicket);

                    return;
                }
                else
                {
                    playerDataBase.EventTicket -= (int)price_Crystal;

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventTicket", playerDataBase.EventTicket);
                }
                break;
        }

        switch (speicalIndex)
        {
            case 0:
                itemList.Clear();
                itemList.Add(animalInfo.animalType.ToString());

                PlayfabManager.instance.GrantItemToUser("Animal", itemList);

                FirebaseAnalytics.LogEvent("Buy_Animal : " + animalInfo.animalType.ToString());

                switch (animalInfo.animalType)
                {
                    case AnimalType.Animal1:
                        break;
                    case AnimalType.Animal2:
                        playerDataBase.Animal2 = 1;
                        break;
                    case AnimalType.Animal3:
                        playerDataBase.Animal3 = 1;
                        break;
                    case AnimalType.Animal4:
                        playerDataBase.Animal4 = 1;
                        break;
                    case AnimalType.Animal5:
                        playerDataBase.Animal5 = 1;
                        break;
                    case AnimalType.Animal6:
                        playerDataBase.Animal6 = 1;
                        break;
                    case AnimalType.Animal7:
                        playerDataBase.Animal7 = 1;
                        break;
                    case AnimalType.Animal8:
                        playerDataBase.Animal8 = 1;
                        break;
                }

                titleText.plusText = "  ( " + (animalIndex + 1) + " / " + shopAnimalArray.Length + " )\n<size=10>"
    + LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetAnimalNumber() + 1) * 1.0f) / (shopAnimalArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();

                break;
            case 1:
                itemList.Clear();
                itemList.Add(truckInfo.truckType.ToString());

                PlayfabManager.instance.GrantItemToUser("Truck", itemList);

                FirebaseAnalytics.LogEvent("Buy_Truck : " + truckInfo.truckType.ToString());

                switch (truckInfo.truckType)
                {
                    case TruckType.Truck1:
                        break;
                    case TruckType.Truck2:
                        playerDataBase.Truck2 = 1;
                        break;
                    case TruckType.Truck3:
                        playerDataBase.Truck3 = 1;
                        break;
                    case TruckType.Truck4:
                        playerDataBase.Truck4 = 1;
                        break;
                    case TruckType.Truck5:
                        playerDataBase.Truck5 = 1;
                        break;
                    case TruckType.Truck6:
                        playerDataBase.Truck6 = 1;
                        break;
                    case TruckType.Truck7:
                        playerDataBase.Truck7 = 1;
                        break;
                    case TruckType.Truck8:
                        playerDataBase.Truck8 = 1;
                        break;
                    case TruckType.Truck9:
                        playerDataBase.Truck9 = 1;
                        break;
                    case TruckType.Truck10:
                        playerDataBase.Truck10 = 1;
                        break;
                }

                titleText.plusText = "  ( " + (truckIndex + 1) + " / " + shopTruckArray.Length + " )\n<size=10>"
+ LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetTruckNumber() + 1) * 1.0f) / (shopTruckArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();

                break;
            case 2:
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

                titleText.plusText = "  ( " + (characterIndex + 1) + " / " + shopCharacterArray.Length + " )\n<size=10>"
+ LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetCharacterNumber() + 1) * 1.0f) / (shopCharacterArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();

                break;
            case 3:
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

                titleText.plusText = "  ( " + (butterflyIndex + 1) + " / " + shopButterflyArray.Length + " )\n<size=10>"
+ LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetButterflyNumber() + 1) * 1.0f) / (shopButterflyArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();

                break;
            case 4:
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

                titleText.plusText = "  ( " + (totemsIndex + 1) + " / " + shopTotemsArray.Length + " )\n<size=10>"
+ LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetTotemsNumber() + 1) * 1.0f) / (shopTotemsArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();
                break;
            case 5:
                itemList.Clear();
                itemList.Add(bucketInfo.bucketType.ToString());

                PlayfabManager.instance.GrantItemToUser("Bucket", itemList);

                FirebaseAnalytics.LogEvent("Buy_Bucket : " + bucketInfo.bucketType.ToString());

                playerDataBase.Bucket[(int)bucketInfo.bucketType] = 1;

                titleText.plusText = "  ( " + (bucketIndex + 1) + " / " + shopBucketArray.Length + " )\n<size=10>"
+ LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetBucketNumber() + 1) * 1.0f) / (shopBucketArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();
                break;
            case 6:
                itemList.Clear();
                itemList.Add(chairInfo.chairType.ToString());

                PlayfabManager.instance.GrantItemToUser("Chair", itemList);

                FirebaseAnalytics.LogEvent("Buy_Chair : " + chairInfo.chairType.ToString());

                playerDataBase.Chair[(int)chairInfo.chairType] = 1;

                titleText.plusText = "  ( " + (chairIndex + 1) + " / " + shopChairArray.Length + " )\n<size=10>"
+ LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetChairNumber() + 1) * 1.0f) / (shopChairArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();
                break;
            case 7:
                itemList.Clear();
                itemList.Add(tubeInfo.tubeType.ToString());

                PlayfabManager.instance.GrantItemToUser("Tube", itemList);

                FirebaseAnalytics.LogEvent("Buy_Tube : " + tubeInfo.tubeType.ToString());

                playerDataBase.Tube[(int)tubeInfo.tubeType] = 1;

                titleText.plusText = "  ( " + (tubeIndex + 1) + " / " + shopTubeArray.Length + " )\n<size=10>"
+ LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetTubeNumber() + 1) * 1.0f) / (shopTubeArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();
                break;
            case 8:
                itemList.Clear();
                itemList.Add(surfboardInfo.surfboardType.ToString());

                PlayfabManager.instance.GrantItemToUser("Surfboard", itemList);

                FirebaseAnalytics.LogEvent("Buy_Surfboard : " + surfboardInfo.surfboardType.ToString());

                playerDataBase.Surfboard[(int)surfboardInfo.surfboardType] = 1;

                titleText.plusText = "  ( " + (surfboardIndex + 1) + " / " + shopSurfboardArray.Length + " )\n<size=10>"
+ LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetSurfboardNumber() + 1) * 1.0f) / (shopSurfboardArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();
                break;
            case 9:
                itemList.Clear();
                itemList.Add(umbrellaInfo.umbrellaType.ToString());

                PlayfabManager.instance.GrantItemToUser("Umbrella", itemList);

                FirebaseAnalytics.LogEvent("Buy_Umbrella : " + umbrellaInfo.umbrellaType.ToString());

                playerDataBase.Umbrella[(int)umbrellaInfo.umbrellaType] = 1;

                titleText.plusText = "  ( " + (umbrellaIndex + 1) + " / " + shopUmbrellaArray.Length + " )\n<size=10>"
+ LocalizationManager.instance.GetString("Collect") + " : " + ((((playerDataBase.GetUmbrellaNumber() + 1) * 1.0f) / (shopUmbrellaArray.Length * 1.0f)) * 100f).ToString("N1") + "%</size>";
                titleText.ReLoad();
                break;
        }

        selectObj.SetActive(true);
        levelUpObj.SetActive(true);
        buyButton.SetActive(false);

        selectText.text = LocalizationManager.instance.GetString("Select");

        //SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        //NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        Selected();
    }

    public void BuyTruckToRm(int number)
    {
        TruckType truckType = TruckType.Truck1 + number + 1;

        itemList.Clear();
        itemList.Add(truckType.ToString());

        PlayfabManager.instance.GrantItemToUser("Truck", itemList);

        switch (truckType)
        {
            case TruckType.Truck1:
                break;
            case TruckType.Truck2:
                playerDataBase.Truck2 = 1;
                break;
            case TruckType.Truck3:
                playerDataBase.Truck3 = 1;
                break;
            case TruckType.Truck4:
                playerDataBase.Truck4 = 1;
                break;
            case TruckType.Truck5:
                playerDataBase.Truck5 = 1;
                break;
            case TruckType.Truck6:
                playerDataBase.Truck6 = 1;
                break;
            case TruckType.Truck7:
                playerDataBase.Truck7 = 1;
                break;
            case TruckType.Truck8:
                playerDataBase.Truck8 = 1;
                break;
            case TruckType.Truck9:
                playerDataBase.Truck9 = 1;
                break;
            case TruckType.Truck10:
                playerDataBase.Truck10 = 1;
                break;
        }

        Invoke("RmDelay", 0.5f);

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);
    }

    public void BuyAnimalToRm(int number)
    {
        AnimalType animalType = AnimalType.Animal1 + number + 1;

        itemList.Clear();
        itemList.Add(animalType.ToString());

        PlayfabManager.instance.GrantItemToUser("Animal", itemList);

        switch (animalType)
        {
            case AnimalType.Animal1:
                break;
            case AnimalType.Animal2:
                playerDataBase.Animal2 = 1;
                break;
            case AnimalType.Animal3:
                playerDataBase.Animal3 = 1;
                break;
            case AnimalType.Animal4:
                playerDataBase.Animal4 = 1;
                break;
            case AnimalType.Animal5:
                playerDataBase.Animal5 = 1;
                break;
            case AnimalType.Animal6:
                playerDataBase.Animal6 = 1;
                break;
            case AnimalType.Animal7:
                playerDataBase.Animal7 = 1;
                break;
            case AnimalType.Animal8:
                playerDataBase.Animal8 = 1;
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
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 4500);

                playerDataBase.BuyCrystal += 4500;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack2");
                break;
            case 10:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 12000);

                playerDataBase.BuyCrystal += 12000;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack3");
                break;
            case 11:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 30000);

                playerDataBase.BuyCrystal += 30000;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack4");
                break;
            case 12:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 99000);

                playerDataBase.BuyCrystal += 99000;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuyCrystal", playerDataBase.BuyCrystal);

                FirebaseAnalytics.LogEvent("Buy_Purchase : CrystalPack5");
                break;
            case 13:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 240000);

                playerDataBase.BuyCrystal += 240000;
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
                if (playerDataBase.Package1) yield break;

                playerDataBase.Package1 = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package1", 1);
                Invoke("PackageDelay1", 0.5f);

                PlayfabManager.instance.UpdateAddGold(6000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 600);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(10);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(1000);
                break;
            case PackageType.Package2:
                if (playerDataBase.Package2) yield break;

                playerDataBase.Package2 = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package2", 1);
                Invoke("PackageDelay2", 0.5f);

                PlayfabManager.instance.UpdateAddGold(12000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1200);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(20);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(2000);
                break;
            case PackageType.Package3:
                if (playerDataBase.Package3) yield break;

                playerDataBase.Package3 = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package3", 1);
                Invoke("PackageDelay3", 0.5f);

                PlayfabManager.instance.PurchaseAutoUpgrade();

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 4500);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(50);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(5000);
                break;
            case PackageType.Package4:
                if (playerDataBase.Package4) yield break;

                playerDataBase.Package4 = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package4", 1);
                Invoke("PackageDelay4", 0.5f);

                PlayfabManager.instance.PurchaseGoldX2();

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 18000);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(50);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(10000);
                break;
            case PackageType.Package5: //한정 패키지
                if (playerDataBase.Package5) yield break;

                playerDataBase.Package5 = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package5", 1);
                Invoke("PackageDelay5", 0.5f);

                PlayfabManager.instance.UpdateAddGold(12000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1200);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(20);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(1000);
                break;
            case PackageType.Package6: //울트라 패키지
                if (playerDataBase.Package6) yield break;

                playerDataBase.Package6 = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package6", 1);
                Invoke("PackageDelay6", 0.5f);

                PlayfabManager.instance.PurchaseRemoveAd();

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 12000);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(10);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(5000);
                break;
            case PackageType.Package7: //서포트 패키지
                if (playerDataBase.Package7 == 1) yield break;

                playerDataBase.Package7 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package7", playerDataBase.Package7);
                Invoke("PackageDelay7", 0.5f);

                PlayfabManager.instance.UpdateAddGold(6000000);

                yield return waitForSeconds;

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 600);

                yield return waitForSeconds;

                PortionManager.instance.GetAllPortion(10);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(1000);
                break;
            case PackageType.Package8:
                if (playerDataBase.Package8 == 1) yield break;

                playerDataBase.Package8 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package8", playerDataBase.Package8);
                packageContents[6].gameObject.SetActive(false);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);

                break;
            case PackageType.Package9:
                if (playerDataBase.Package9 == 1) yield break;

                playerDataBase.Package9 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package9", playerDataBase.Package9);
                packageContents[7].gameObject.SetActive(false);

                TreasureManager.instance.OpenTreasure(11);

                break;
            case PackageType.Package10:
                if (playerDataBase.Package10) yield break;

                playerDataBase.Package10 = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package10", 1);
                Invoke("PackageDelay10", 0.5f);

                PlayfabManager.instance.PurchaseSuperExp();

                yield return waitForSeconds;

                playerDataBase.Treasure1Count += 100;
                playerDataBase.Treasure7Count += 100;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure1Count", playerDataBase.Treasure1Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure7Count", playerDataBase.Treasure7Count);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(5000);


                break;
            case PackageType.Package11:
                if (playerDataBase.Package11) yield break;

                playerDataBase.Package11 = true;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Package11", 1);
                Invoke("PackageDelay11", 0.5f);

                PlayfabManager.instance.PurchaseSuperKitchen();

                yield return waitForSeconds;

                playerDataBase.Treasure13Count += 50;
                playerDataBase.Treasure14Count += 50;

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure13Count", playerDataBase.Treasure13Count);
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure14Count", playerDataBase.Treasure14Count);

                yield return waitForSeconds;

                PortionManager.instance.GetEventTicket(2000);

                break;
            case PackageType.Package12:
                if (playerDataBase.Package12) yield break;


                break;
        }

        FirebaseAnalytics.LogEvent("Buy_Package : " + type.ToString());

        //if (playerDataBase.Package1 && playerDataBase.Package2 && playerDataBase.Package3 && playerDataBase.Package4
        //     && playerDataBase.Package5 && playerDataBase.Package6)
        //{
        //    packageThanks.SetActive(true);
        //}
    }

    public void LowCoin(long number) //얼마만큼 부족한지?
    {
        GameManager.instance.RenewalVC();

        lowCoin = number - playerDataBase.Coin;
        needExchangeCrystal = lowCoin / 10000;

        changeMoneyIndex = 3;
        OpenChangeMoneyView();
        changeMoneyReceiveContent.Initialize(RewardType.Gold, 0);
        changeMoneyReceiveContent.Initialize(lowCoin);
        changeMoneyText.text = MoneyUnitString.ToCurrencyString(needExchangeCrystal);
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

    void PackageDelay10()
    {
        packageContents[8].gameObject.SetActive(false);
    }

    void PackageDelay11()
    {
        packageContents[9].gameObject.SetActive(false);
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
        ReceiveInfoManager.instance.OpenReceiveInfo(RewardType.RankPoint);
    }

    public void OpenAbilityPointInfo()
    {
        ReceiveInfoManager.instance.OpenReceiveInfo(RewardType.AbilityPoint);
    }

    public void LevelUpButton()
    {
        if (isDelay) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

#if UNITY_EDITOR
        price_LevelUp = 0;
#endif

        if (playerDataBase.AbilityPoint < price_LevelUp)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowPoint);
            return;
        }

        playerDataBase.AbilityPoint -= price_LevelUp;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("AbilityPoint", playerDataBase.AbilityPoint);

        abilityPointText.text = MoneyUnitString.ToCurrencyString(playerDataBase.AbilityPoint);

        if (level + 1 < 10)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Upgrade5);
            NotionManager.instance.UseNotion4(NotionType.SuccessUpgrade);
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);
            NotionManager.instance.UseNotion4(NotionType.MaxLevel);
        }

        level += 1;

        switch (speicalIndex)
        {
            case 0:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetAnimal_Rare((int)animalInfo.animalType).ToString());
                customData.Add("Level", "0");

                playerDataBase.animalItemInstance[(int)animalInfo.animalType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.animalItemInstance[(int)animalInfo.animalType].instanceId, customData);

                AnimalInitialize();
                break;
            case 1:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetTruck_Rare((int)truckInfo.truckType).ToString());
                customData.Add("Level", "0");

                playerDataBase.truckItemInstance[(int)truckInfo.truckType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.truckItemInstance[(int)truckInfo.truckType].instanceId, customData);

                TruckInitialize();
                break;
            case 2:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetCharacter_Rare((int)characterInfo.characterType).ToString());
                customData.Add("Level", "0");

                playerDataBase.characterItemInstance[(int)characterInfo.characterType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.characterItemInstance[(int)characterInfo.characterType].instanceId, customData);

                CharacterInitialize();
                break;
            case 3:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetButterfly_Rare((int)butterflyInfo.butterflyType).ToString());
                customData.Add("Level", "0");

                playerDataBase.butterflyItemInstance[(int)butterflyInfo.butterflyType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.butterflyItemInstance[(int)butterflyInfo.butterflyType].instanceId, customData);

                ButterflyInitialize();
                break;
            case 4:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetTotems_Rare((int)totemsInfo.totemsType).ToString());
                customData.Add("Level", "0");

                playerDataBase.totemsItemInstance[(int)totemsInfo.totemsType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.totemsItemInstance[(int)totemsInfo.totemsType].instanceId, customData);

                TotemsInitialize();
                break;
            case 5:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetBucket_Rare((int)bucketInfo.bucketType).ToString());
                customData.Add("Level", "0");

                playerDataBase.bucketItemInstance[(int)bucketInfo.bucketType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.bucketItemInstance[(int)bucketInfo.bucketType].instanceId, customData);

                BucketInitialize();
                break;
            case 6:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetChair_Rare((int)chairInfo.chairType).ToString());
                customData.Add("Level", "0");

                playerDataBase.chairItemInstance[(int)chairInfo.chairType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.chairItemInstance[(int)chairInfo.chairType].instanceId, customData);

                ChairInitialize();
                break;
            case 7:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetTube_Rare((int)tubeInfo.tubeType).ToString());
                customData.Add("Level", "0");

                playerDataBase.tubeItemInstance[(int)tubeInfo.tubeType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.tubeItemInstance[(int)tubeInfo.tubeType].instanceId, customData);

                TubeInitialize();
                break;
            case 8:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetSurfboard_Rare((int)surfboardInfo.surfboardType).ToString());
                customData.Add("Level", "0");

                playerDataBase.surfboardItemInstance[(int)surfboardInfo.surfboardType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.surfboardItemInstance[(int)surfboardInfo.surfboardType].instanceId, customData);

                SurfboardInitialize();
                break;
            case 9:
                customData.Clear();
                customData.Add("AbilityLevel", level.ToString());
                customData.Add("Rare", playerDataBase.GetUmbrella_Rare((int)umbrellaInfo.umbrellaType).ToString());
                customData.Add("Level", "0");

                playerDataBase.umbrellaItemInstance[(int)umbrellaInfo.umbrellaType].abilityLevel += 1;

                PlayfabManager.instance.SetInventoryCustomData(playerDataBase.umbrellaItemInstance[(int)umbrellaInfo.umbrellaType].instanceId, customData);

                UmbrellaInitialize();
                break;
        }

        GourmetManager.instance.Initialize();

        isDelay = true;
        Invoke("Delay", 0.4f);
    }

    public void ComingSoon()
    {
        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        NotionManager.instance.UseNotion(NotionType.ComingSoon);
    }
}
