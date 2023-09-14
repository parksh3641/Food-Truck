using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopView;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;

    [Space]
    [Title("ScrollView")]
    public GameObject[] shopArray;

    public RectTransform shopRectTransform;

    [Space]
    [Title("Item")]
    public ShopContent[] shopContents;

    [Space]
    [Title("Truck")]
    public GameObject[] mainTruckArray;
    public GameObject[] shopTruckArray;

    public GameObject buyObj;
    public GameObject selectObj;

    public GameObject[] buyRmObj;
    public LocalizationContent[] buyRmText;

    public Text selectText;

    [Space]
    public LocalizationContent truckTitleText;
    public LocalizationContent truckEffectText;
    public LocalizationContent truckInfoText;

    public Text priceText;

    public Text dailyShopCountText;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    private int truckIndex = 0;
    private int index = -1;

    bool hold = false;
    bool isDelay = false;
    bool isTimer = false;

    TruckInfo truckInfo = new TruckInfo();

    List<string> itemList = new List<string>();

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    public ResetManager resetManager;

    PlayerDataBase playerDataBase;
    TruckDataBase truckDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (truckDataBase == null) truckDataBase = Resources.Load("TruckDataBase") as TruckDataBase;

        shopView.SetActive(false);
    }

    private void Start()
    {
        for(int i = 0; i < shopArray.Length; i ++)
        {
            shopArray[i].SetActive(false);
        }

        for(int i = 0; i < mainTruckArray.Length; i ++)
        {
            mainTruckArray[i].SetActive(false);
        }

        mainTruckArray[(int)GameStateManager.instance.TruckType].SetActive(true);

        isTimer = true;
        dailyShopCountText.text = "";
        StartCoroutine(DailyShopTimer());

        shopRectTransform.sizeDelta = new Vector2(0, -999);
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

                ItemInitialize();
            }

            if (!GameStateManager.instance.DailyReward)
            {
                shopContents[0].SetLocked(false);
            }

            if (!GameStateManager.instance.DailyAdsReward)
            {
                shopContents[1].SetLocked(false);
            }
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

        for(int i = 0; i < topMenuImgArray.Length; i ++)
        {
            topMenuImgArray[i].sprite = topMenuSpriteArray[0];
            shopArray[i].gameObject.SetActive(false);
        }

        topMenuImgArray[number].sprite = topMenuSpriteArray[1];
        shopArray[number].gameObject.SetActive(true);

        switch(number)
        {
            case 1:
                TruckInitialize();
                break;
        }    
    }

    void ItemInitialize()
    {
        shopContents[0].Initialize(ItemType.DailyReward, BuyType.Free, this);
        shopContents[1].Initialize(ItemType.AdReward_Gold, BuyType.Ad, this);
        shopContents[2].Initialize(ItemType.DefDestroyTicket, BuyType.Coin, this);
        shopContents[3].Initialize(ItemType.GoldShop1, BuyType.Rm, this);
        shopContents[4].Initialize(ItemType.GoldShop2, BuyType.Rm, this);
        shopContents[5].Initialize(ItemType.GoldShop3, BuyType.Rm, this);
    }

    void TruckInitialize()
    {
        for(int i = 0; i < shopTruckArray.Length; i ++)
        {
            shopTruckArray[i].gameObject.SetActive(false);
        }

        shopTruckArray[truckIndex].gameObject.SetActive(true);
        shopTruckArray[truckIndex].transform.localRotation = Quaternion.Euler(0, 220, 0);

        truckInfo = truckDataBase.GetTruckInfo(TruckType.Bread + truckIndex);

        truckTitleText.localizationName = (TruckType.Bread + truckIndex).ToString() + "Truck";
        truckEffectText.localizationName = truckInfo.truckEffect.ToString();
        truckEffectText.plusText = " +" + truckInfo.effectNumber.ToString() + "%";
        truckInfoText.localizationName = (TruckType.Bread + truckIndex) + "TruckInfo";

        truckTitleText.ReLoad();
        truckEffectText.ReLoad();
        truckInfoText.ReLoad();

        priceText.text = MoneyUnitString.ToCurrencyString(truckInfo.price);

        hold = false;

        switch (truckInfo.truckType)
        {
            case TruckType.Bread:
                hold = true;
                break;
            case TruckType.Chips:
                if(playerDataBase.ChipsTruck >= 1)
                {
                    hold = true;
                }
                break;
            case TruckType.Donut:
                if (playerDataBase.DonutTruck >= 1)
                {
                    hold = true;
                }
                break;
            case TruckType.Hamburger:
                if (playerDataBase.HamburgerTruck >= 1)
                {
                    hold = true;
                }
                break;
            case TruckType.Hotdog:
                if (playerDataBase.HotdogTruck >= 1)
                {
                    hold = true;
                }
                break;
            case TruckType.Icecream:
                if (playerDataBase.IcecreamTruck >= 1)
                {
                    hold = true;
                }
                break;
            case TruckType.Lemonade:
                if (playerDataBase.LemonadeTruck >= 1)
                {
                    hold = true;
                }
                break;
            case TruckType.Noodles:
                if (playerDataBase.NoodlesTruck >= 1)
                {
                    hold = true;
                }
                break;
            case TruckType.Pizza:
                if (playerDataBase.PizzaTruck >= 1)
                {
                    hold = true;
                }
                break;
            case TruckType.Sushi:
                if (playerDataBase.SushiTruck >= 1)
                {
                    hold = true;
                }
                break;
        }

        if(hold)
        {
            buyObj.SetActive(false);
            selectObj.SetActive(true);

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
            buyObj.SetActive(true);
            selectObj.SetActive(false);

            if(truckIndex > 0)
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

    public void RightButton()
    {
        if(truckIndex + 1 < shopTruckArray.Length)
        {
            truckIndex += 1;

            TruckInitialize();
        }
    }

    public void LeftButton()
    {
        if(truckIndex - 1 >= 0)
        {
            truckIndex -= 1;

            TruckInitialize();
        }
    }

    public void Selected()
    {
        if(GameStateManager.instance.TruckType == truckInfo.truckType)
        {
            return;
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.ChangeTruckNotion);

        GameStateManager.instance.TruckType = truckInfo.truckType;

        for (int i = 0; i < mainTruckArray.Length; i++)
        {
            mainTruckArray[i].SetActive(false);
        }

        mainTruckArray[(int)GameStateManager.instance.TruckType].SetActive(true);

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

            buyObj.SetActive(false);
            selectObj.SetActive(true);
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
    void RmDelay()
    {
        for (int i = 0; i < buyRmObj.Length; i++)
        {
            buyRmObj[i].SetActive(false);
        }

        buyObj.SetActive(false);
        selectObj.SetActive(true);
        selectText.text = LocalizationManager.instance.GetString("Select");
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

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 100000);

                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessReward);

                break;
            case ItemType.AdReward_Gold:
                if (GameStateManager.instance.DailyAdsReward) return;

                GoogleAdsManager.instance.admobReward.ShowAd(0);

                break;
            case ItemType.DefDestroyTicket:
                if(playerDataBase.Coin >= 5000000)
                {
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, 5000000);

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
        GameStateManager.instance.DailyAdsReward = true;

        shopContents[1].SetLocked(true);

        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 500000);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessWatchAd);
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

    public void BuyPurchase(int number)
    {
        switch (number)
        {
            case 0:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 1000000);
                break;
            case 1:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 3300000);
                break;
            case 2:
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Coin, 5500000);
                break;
        }

        SoundManager.instance.PlaySFX(GameSfxType.GetMoney);

        NotionManager.instance.UseNotion(NotionType.SuccessBuy);
    }

    public void Failed()
    {
        SoundManager.instance.PlaySFX(GameSfxType.Wrong);

        NotionManager.instance.UseNotion(NotionType.CancelPurchase);
    }
}
