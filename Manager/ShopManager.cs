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
    public GameObject mainCharacter;
    public GameObject[] mainCharacterArray;
    public GameObject[] shopCharacterArray;

    public GameObject mainTruck;
    public GameObject[] mainTruckArray;
    public GameObject[] shopTruckArray;

    public GameObject mainAnimal;
    public GameObject[] mainAnimalArray;
    public GameObject[] shopAnimalArray;

    public GameObject mainButterfly;
    public GameObject[] mainButterflyArray;
    public GameObject[] shopButterflyArray;

    public GameObject buyToCoinButton;
    public GameObject buyToCrystalButton;
    public GameObject selectObj;

    public Text selectText;
    public Text priceText;
    public Text crystalText;

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

    private int characterIndex = 0;
    private int truckIndex = 0;
    private int animalIndex = 0;
    private int butterflyIndex = 0;

    bool hold = false;
    bool buy = false;
    bool isDelay = false;
    bool isTimer = false;

    CharacterInfo characterInfo = new CharacterInfo();
    TruckInfo truckInfo = new TruckInfo();
    AnimalInfo animalInfo = new AnimalInfo();
    ButterflyInfo butterflyInfo = new ButterflyInfo();

    List<string> itemList = new List<string>();

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    public ResetManager resetManager;

    PlayerDataBase playerDataBase;

    CharacterDataBase characterDataBase;
    TruckDataBase truckDataBase;
    AnimalDataBase animalDataBase;
    ButterflyDataBase butterflyDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        if (characterDataBase == null) characterDataBase = Resources.Load("CharacterDataBase") as CharacterDataBase;
        if (truckDataBase == null) truckDataBase = Resources.Load("TruckDataBase") as TruckDataBase;
        if (animalDataBase == null) animalDataBase = Resources.Load("AnimalDataBase") as AnimalDataBase;
        if (butterflyDataBase == null) butterflyDataBase = Resources.Load("ButterflyDataBase") as ButterflyDataBase;

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

        for (int i = 0; i < mainCharacterArray.Length; i++)
        {
            mainCharacterArray[i].SetActive(false);
        }

        for (int i = 0; i < mainTruckArray.Length; i ++)
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

        mainCharacterArray[(int)GameStateManager.instance.CharacterType].SetActive(true);
        mainTruckArray[(int)GameStateManager.instance.TruckType].SetActive(true);
        mainAnimalArray[(int)GameStateManager.instance.AnimalType].SetActive(true);
        mainButterflyArray[(int)GameStateManager.instance.ButterflyType].SetActive(true);

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
                shopContents[6].Initialize(ItemType.AdReward_Portion, BuyType.Ad, this);
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
                if (playerDataBase.Coin >= 10000000)
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
            case ItemType.AdReward_Portion:
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

                switch(Random.Range(0, 5))
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
                    case 4:
                        playerDataBase.Portion5 += 1;
                        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
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

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 3000000);

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
            else
            {
                switch (speicalIndex)
                {
                    case 0:
                        shopCharacterArray[characterIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);
                        break;
                    case 1:
                        shopTruckArray[truckIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);
                        break;
                    case 2:
                        shopAnimalArray[animalIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);
                        break;
                    case 3:
                        shopButterflyArray[butterflyIndex].transform.localRotation = Quaternion.Euler(0, 0, 0);
                        break;
                }
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
        }

        speicalTopMenuImgArray[number].sprite = speicalTopMenuSpriteArray[1];

        mainCharacter.SetActive(false);
        mainTruck.SetActive(false);
        mainAnimal.SetActive(false);
        mainButterfly.SetActive(false);

        switch (number)
        {
            case 0:
                CharacterInitialize();
                break;
            case 1:
                TruckInitialize();
                break;
            case 2:
                AnimalInitialize();
                break;
            case 3:
                ButterflyInitialize();
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
        shopCharacterArray[characterIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);

        characterInfo = characterDataBase.GetCharacterInfo(CharacterType.Character1 + characterIndex);

        titleText.localizationName =  "Character" + (characterIndex + 1);
        passiveText.text = "";

        if (characterIndex == 0)
        {
            effectText.localizationName = "None";
            effectText.plusText = "";
            //passiveText.text = "";
        }
        else
        {
            effectText.localizationName = characterInfo.passiveEffect.ToString();
            effectText.plusText = " : +" + characterInfo.effectNumber.ToString() + "%";
            //passiveText.text = LocalizationManager.instance.GetString("Passive") + " : " + LocalizationManager.instance.GetString("NeedPrice") + " -1%";
        }

        infoText.localizationName = "Character" + (characterIndex + 1) + "_Info";

        titleText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        priceText.text = MoneyUnitString.ToCurrencyString(characterInfo.price);
        crystalText.text = MoneyUnitString.ToCurrencyString(characterInfo.crystal);

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
        }

        if (hold)
        {
            selectObj.SetActive(true);
            buyToCoinButton.SetActive(false);
            buyToCrystalButton.SetActive(false);

            if (GameStateManager.instance.CharacterType.Equals(characterInfo.characterType))
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
            buyToCrystalButton.SetActive(true);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
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

        titleText.localizationName = (TruckType.Bread + truckIndex).ToString() + "Truck";
        passiveText.text = "";

        if (truckIndex == 0)
        {
            effectText.localizationName = "None";
            effectText.plusText = "";
            //passiveText.text = "";
        }
        else
        {
            effectText.localizationName = truckInfo.passiveEffect.ToString();
            effectText.plusText = " : +" + truckInfo.effectNumber.ToString() + "%";
            //passiveText.text = LocalizationManager.instance.GetString("Passive") + " : " + LocalizationManager.instance.GetString("NeedPrice") + " -1%";
        }

        infoText.localizationName = (TruckType.Bread + truckIndex) + "TruckInfo";

        titleText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        priceText.text = MoneyUnitString.ToCurrencyString(truckInfo.price);
        crystalText.text = MoneyUnitString.ToCurrencyString(truckInfo.crystal);

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
            buyToCrystalButton.SetActive(false);

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
            buyToCrystalButton.SetActive(true);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
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
        shopAnimalArray[animalIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);

        animalInfo = animalDataBase.GetAnimalInfo(AnimalType.Colobus + animalIndex);

        titleText.localizationName = (AnimalType.Colobus + animalIndex).ToString();
        passiveText.text = "";

        if (animalIndex == 0)
        {
            effectText.localizationName = "None";
            effectText.plusText = "";
            //passiveText.text = "";
        }
        else
        {
            effectText.localizationName = animalInfo.passiveEffect.ToString();
            effectText.plusText = " : +" + animalInfo.effectNumber.ToString() + "%";
            passiveText.text = LocalizationManager.instance.GetString("SellPriceX2Up_Info");
        }

        infoText.localizationName = (AnimalType.Colobus + animalIndex) + "Info";

        titleText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        priceText.text = MoneyUnitString.ToCurrencyString(animalInfo.price);
        crystalText.text = MoneyUnitString.ToCurrencyString(animalInfo.crystal);

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
            buyToCrystalButton.SetActive(false);

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
            buyToCrystalButton.SetActive(true);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
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
        shopButterflyArray[butterflyIndex].transform.localRotation = Quaternion.Euler(0, 210, 0);

        butterflyInfo = butterflyDataBase.GetButterflyInfo(ButterflyType.Butterfly1 + butterflyIndex);

        titleText.localizationName = "Butterfly" + (butterflyIndex + 1);
        passiveText.text = "";

        if (butterflyIndex == 0)
        {
            effectText.localizationName = "None";
            effectText.plusText = "";
            //passiveText.text = "";
        }
        else
        {
            effectText.localizationName = butterflyInfo.passiveEffect.ToString();
            effectText.plusText = " : +" + butterflyInfo.effectNumber.ToString() + "%";
            //passiveText.text = LocalizationManager.instance.GetString("Passive") + " : " + LocalizationManager.instance.GetString("NeedPrice") + " -1%";
        }

        infoText.localizationName = "Butterfly" + (butterflyIndex + 1) + "_Info";

        titleText.ReLoad();
        effectText.ReLoad();
        infoText.ReLoad();

        priceText.text = MoneyUnitString.ToCurrencyString(butterflyInfo.price);
        crystalText.text = MoneyUnitString.ToCurrencyString(butterflyInfo.crystal);

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

        if (hold)
        {
            selectObj.SetActive(true);
            buyToCoinButton.SetActive(false);
            buyToCrystalButton.SetActive(false);

            if (GameStateManager.instance.ButterflyType.Equals(butterflyInfo.butterflyType))
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
            buyToCrystalButton.SetActive(true);

            if (!buy)
            {
                priceText.text = LocalizationManager.instance.GetString("NotPurchase");
            }
        }
    }

    public void RightButton()
    {

        switch(speicalIndex)
        {
            case 0:
                if (characterIndex + 1 < shopCharacterArray.Length)
                {
                    characterIndex += 1;

                    CharacterInitialize();
                }
                break;
            case 1:
                if (truckIndex + 1 < shopTruckArray.Length)
                {
                    truckIndex += 1;

                    TruckInitialize();
                }
                break;
            case 2:
                if (animalIndex + 1 < shopAnimalArray.Length)
                {
                    animalIndex += 1;

                    AnimalInitialize();
                }
                break;
            case 3:
                if (butterflyIndex + 1 < shopButterflyArray.Length)
                {
                    butterflyIndex += 1;

                    ButterflyInitialize();
                }
                break;
        }
    }

    public void LeftButton()
    {
        switch (speicalIndex)
        {
            case 0:
                if (characterIndex - 1 >= 0)
                {
                    characterIndex -= 1;

                    CharacterInitialize();
                }
                break;
            case 1:
                if (truckIndex - 1 >= 0)
                {
                    truckIndex -= 1;

                    TruckInitialize();
                }
                break;
            case 2:
                if (animalIndex - 1 >= 0)
                {
                    animalIndex -= 1;

                    AnimalInitialize();
                }
                break;
            case 3:
                if (butterflyIndex - 1 >= 0)
                {
                    butterflyIndex -= 1;

                    ButterflyInitialize();
                }
                break;
        }
    }

    public void Selected()
    {
        switch(speicalIndex)
        {
            case 0:
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
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);

        selectText.text = LocalizationManager.instance.GetString("Selected");
    }

    public void BuyItem(int number)
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (number == 0)
        {
            if (!buy)
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.NotPurchaseNotion);
                return;
            }
        }

        switch(speicalIndex)
        {
            case 0:
                switch (number)
                {
                    case 0:
                        if (playerDataBase.Coin < characterInfo.price)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, characterInfo.price);
                        }
                        break;
                    case 1:
                        if (playerDataBase.Crystal < characterInfo.crystal)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCrystal);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, characterInfo.crystal);
                        }
                        break;
                }

                itemList.Clear();
                itemList.Add(characterInfo.characterType.ToString());

                PlayfabManager.instance.GrantItemToUser("Character", itemList);

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

                break;
            case 1:
                switch(number)
                {
                    case 0:
                        if (playerDataBase.Coin < truckInfo.price)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, truckInfo.price);
                        }
                        break;
                    case 1:
                        if (playerDataBase.Crystal < truckInfo.crystal)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCrystal);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, truckInfo.crystal);
                        }
                        break;
                }

                itemList.Clear();
                itemList.Add(truckInfo.truckType.ToString());

                PlayfabManager.instance.GrantItemToUser("Truck", itemList);

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

                break;
            case 2:
                switch (number)
                {
                    case 0:
                        if (playerDataBase.Coin < animalInfo.price)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, animalInfo.price);
                        }
                        break;
                    case 1:
                        if (playerDataBase.Crystal < animalInfo.crystal)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCrystal);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, animalInfo.crystal);
                        }
                        break;
                }

                itemList.Clear();
                itemList.Add(animalInfo.animalType.ToString());

                PlayfabManager.instance.GrantItemToUser("Animal", itemList);

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

                break;
            case 3:
                switch (number)
                {
                    case 0:
                        if (playerDataBase.Coin < butterflyInfo.price)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, butterflyInfo.price);
                        }
                        break;
                    case 1:
                        if (playerDataBase.Crystal < butterflyInfo.crystal)
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCrystal);

                            return;
                        }
                        else
                        {
                            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, butterflyInfo.crystal);
                        }
                        break;
                }

                itemList.Clear();
                itemList.Add(butterflyInfo.butterflyType.ToString());

                PlayfabManager.instance.GrantItemToUser("Butterfly", itemList);

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

                break;
        }

        selectObj.SetActive(true);
        buyToCoinButton.SetActive(false);
        buyToCrystalButton.SetActive(false);

        selectText.text = LocalizationManager.instance.GetString("Select");

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);
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
        buyToCoinButton.SetActive(false);
        buyToCrystalButton.SetActive(false);

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
