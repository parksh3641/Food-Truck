using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ShopContent : MonoBehaviour
{
    public ItemType itemType = ItemType.DailyReward;
    public BuyType buyType = BuyType.Free;
    public int index = 0;

    public Image icon;

    public LocalizationContent titleText;
    public Text rewardText;
    public LocalizationContent infoText;

    public CodelessIAPButton iapButton;
    public LocalizationContent priceText;

    public GameObject buyRMObj;
    public GameObject buyFreeObj;
    public GameObject buyAdObj;
    public GameObject buyCoinObj;
    public GameObject buyCrystalObj;
    public GameObject buyExchangeObj;
    public GameObject buyRankPointObj;
    public GameObject lockedObj;

    public GameObject effect;

    public Text buyCoinText;
    public Text buyCrystalText;
    public Text buyRankPointText;

    public GameObject bestObj;
    public Text bestText;

    private bool isDelay = false;

    Sprite[] itemArray;

    ShopManager shopManager;

    ImageDataBase imageDataBase;
    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        itemArray = imageDataBase.GetItemArray();

        lockedObj.SetActive(true);
    }

    public void Initialize(ItemType item, BuyType buy, ShopManager manager)
    {
        itemType = item;
        buyType = buy;

        shopManager = manager;

        icon.sprite = itemArray[(int)itemType];

        titleText.localizationName = itemType.ToString();
        titleText.ReLoad();

        rewardText.text = "";

        index = 0;

        buyRMObj.SetActive(false);
        buyFreeObj.SetActive(false);
        buyAdObj.SetActive(false);
        buyCoinObj.SetActive(false);
        buyCrystalObj.SetActive(false);
        buyExchangeObj.SetActive(false);
        buyRankPointObj.SetActive(false);

        bestObj.SetActive(false);
        effect.SetActive(false);

        switch (buyType)
        {
            case BuyType.Free:
                buyFreeObj.SetActive(true);
                break;
            case BuyType.Rm:
                buyRMObj.SetActive(true);
                break;
            case BuyType.Ad:
                buyAdObj.SetActive(true);
                break;
            case BuyType.Coin:
                buyCoinObj.SetActive(true);
                break;
            case BuyType.Crystal:
                buyCrystalObj.SetActive(true);
                break;
            case BuyType.Exchange:
                buyExchangeObj.SetActive(true);
                break;
            case BuyType.RankPoint:
                buyRankPointObj.SetActive(true);
                break;
        }

        switch (itemType)
        {
            case ItemType.DailyReward:
                rewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(100000) + "</size>";

                if (playerDataBase.Candy1MaxValue > 0)
                {
                    rewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(300000) + "</size>";
                }

                if (playerDataBase.JapaneseFood1MaxValue > 0)
                {
                    rewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(500000) + "</size>";
                }

                if (playerDataBase.Dessert1MaxValue > 0)
                {
                    rewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(1000000) + "</size>";
                }
                //effect.SetActive(true);
                break;
            case ItemType.AdReward_Gold:
                rewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(1000000) + "</size>";

                if(playerDataBase.Candy1MaxValue > 0)
                {
                    rewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(3000000) + "</size>";
                }

                if(playerDataBase.JapaneseFood1MaxValue > 0)
                {
                    rewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(5000000) + "</size>";
                }

                if(playerDataBase.Dessert1MaxValue > 0)
                {
                    rewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(10000000) + "</size>";
                }
                effect.SetActive(true);
                break;
            case ItemType.DefDestroyTicket:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(300);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.DefDestroyTicket;
                infoText.ReLoad();

                break;
            case ItemType.GoldShop1:
                lockedObj.SetActive(false);

                rewardText.text = MoneyUnitString.ToCurrencyString(1000000);

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(120);
                break;
            case ItemType.GoldShop2:
                lockedObj.SetActive(false);

                rewardText.text = MoneyUnitString.ToCurrencyString(10000000);

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(1200);
                break;
            case ItemType.GoldShop3:
                lockedObj.SetActive(false);

                rewardText.text = MoneyUnitString.ToCurrencyString(100000000);

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(12000);
                break;
            case ItemType.AdReward_Portion:
                rewardText.text = "";

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();
                //effect.SetActive(true);
                break;
            case ItemType.RemoveAds:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.removeads";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                infoText.localizationName = "Unlimited";
                infoText.ReLoad();

                index = 3;
                effect.SetActive(true);
                break;
            case ItemType.PortionSet1:
                lockedObj.SetActive(false);

                //iapButton.productId = "shop.foodtruck.portionset1";

                //priceText.localizationName = itemType + "_Price";
                //priceText.ReLoad();

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(600);

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                index = 4;
                break;
            case ItemType.PortionSet2:
                lockedObj.SetActive(false);

                //iapButton.productId = "shop.foodtruck.portionset2";

                //priceText.localizationName = itemType + "_Price";
                //priceText.ReLoad();

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(1200);

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                index = 5;
                break;
            case ItemType.PortionSet3:
                lockedObj.SetActive(false);

                //iapButton.productId = "shop.foodtruck.portionset3";

                //priceText.localizationName = itemType + "_Price";
                //priceText.ReLoad();

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(1800);

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                bestObj.SetActive(true);
                bestText.text = "BEST";

                index = 6;
                break;
            case ItemType.DailyReward_Portion:
                rewardText.text = "";

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();
                //effect.SetActive(true);
                break;
            case ItemType.GoldX2:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.goldx2";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                rewardText.text = LocalizationManager.instance.GetString("NowPrice") + " +100%";

                index = 7;
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop1:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.crystal1";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                rewardText.text = MoneyUnitString.ToCurrencyString(600);

                index = 8;
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop2:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.crystal2";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                rewardText.text = MoneyUnitString.ToCurrencyString(3000) + "\n<size=12>" + LocalizationManager.instance.GetString("Bonus") + " +" + MoneyUnitString.ToCurrencyString(600) + "</size>";

                bestObj.SetActive(true);
                bestText.text = "+20%";

                index = 9;
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop3:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.crystal3";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                rewardText.text = MoneyUnitString.ToCurrencyString(6000) + "\n<size=12>" + LocalizationManager.instance.GetString("Bonus") + " +" + MoneyUnitString.ToCurrencyString(2400) + "</size>";

                bestObj.SetActive(true);
                bestText.text = "+40%";

                index = 10;
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop4:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.crystal4";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                rewardText.text = MoneyUnitString.ToCurrencyString(12000) + "\n<size=12>" + LocalizationManager.instance.GetString("Bonus") + " +" + MoneyUnitString.ToCurrencyString(7200) + "</size>";

                bestObj.SetActive(true);
                bestText.text = "+60%";

                index = 11;
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop5:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.crystal5";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                rewardText.text = MoneyUnitString.ToCurrencyString(33000) + "\n<size=12>" + LocalizationManager.instance.GetString("Bonus") + " +" + MoneyUnitString.ToCurrencyString(26400) + "</size>";

                bestObj.SetActive(true);
                bestText.text = "+80%";

                index = 12;
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop6:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.crystal6";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                rewardText.text = MoneyUnitString.ToCurrencyString(60000) + "\n<size=12>" + LocalizationManager.instance.GetString("Bonus") + " +" + MoneyUnitString.ToCurrencyString(60000) + "</size>";

                bestObj.SetActive(true);
                bestText.text = "+100%";

                index = 13;
                effect.SetActive(true);
                break;
            case ItemType.Portion1:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(100);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion1;
                infoText.ReLoad();
                break;
            case ItemType.Portion2:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(100);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion2;
                infoText.ReLoad();
                break;
            case ItemType.Portion3:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(100);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion3;
                infoText.ReLoad();
                break;
            case ItemType.Portion4:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(100);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion4;
                infoText.ReLoad();
                break;
            case ItemType.Portion5:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(150);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion5;
                infoText.ReLoad();
                break;
            case ItemType.DefDestroyTicketSlices:
                lockedObj.SetActive(false);

                titleText.plusText = " x1";

                infoText.localizationName = "HoldPiece";
                infoText.plusText = " : " + playerDataBase.DefDestroyTicketPiece + "/5";
                infoText.ReLoad();

                effect.SetActive(true);
                break;
            case ItemType.DefDestroyTicketPiece:
                lockedObj.SetActive(false);

                titleText.plusText = " x1";

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(60);

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.DefDestroyTicketPiece;
                infoText.ReLoad();
                break;
            case ItemType.BuffTicketSet1:
                lockedObj.SetActive(false);

                //iapButton.productId = "shop.foodtruck.buff1";

                //priceText.localizationName = itemType + "_Price";
                //priceText.ReLoad();

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(600);

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                index = 14;
                break;
            case ItemType.BuffTicketSet2:
                lockedObj.SetActive(false);

                //iapButton.productId = "shop.foodtruck.buff2";

                //priceText.localizationName = itemType + "_Price";
                //priceText.ReLoad();

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(1200);

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                index = 15;
                break;
            case ItemType.BuffTicketSet3:
                lockedObj.SetActive(false);

                //iapButton.productId = "shop.foodtruck.buff3";

                //priceText.localizationName = itemType + "_Price";
                //priceText.ReLoad();

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(1800);

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                index = 16;

                bestObj.SetActive(true);
                bestText.text = "BEST";
                break;
            case ItemType.DefTicketSet1:
                lockedObj.SetActive(false);

                //iapButton.productId = "shop.foodtruck.defticket1";

                //priceText.localizationName = itemType + "_Price";
                //priceText.ReLoad();

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(1800);

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                index = 17;
                break;
            case ItemType.DefTicketSet2:
                lockedObj.SetActive(false);

                //iapButton.productId = "shop.foodtruck.defticket2";

                //priceText.localizationName = itemType + "_Price";
                //priceText.ReLoad();

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(3600);

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                index = 18;
                break;
            case ItemType.DefTicketSet3:
                lockedObj.SetActive(false);

                //iapButton.productId = "shop.foodtruck.defticket3";

                //priceText.localizationName = itemType + "_Price";
                //priceText.ReLoad();

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(6000);

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                bestObj.SetActive(true);
                bestText.text = "BEST";

                index = 19;
                break;
            case ItemType.SuperOffline:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.superoffline";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                index = 20;
                effect.SetActive(true);
                break;
            case ItemType.AdReward_Crystal:
                rewardText.text = "<size=18>" + MoneyUnitString.ToCurrencyString(100) + "</size>";
                effect.SetActive(true);
                break;
            case ItemType.AutoUpgrade:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.autoupgrade";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                infoText.localizationName = itemType + "_Info2";
                infoText.ReLoad();

                index = 21;
                effect.SetActive(true);
                break;
            case ItemType.AutoPresent:
                lockedObj.SetActive(false);

                iapButton.productId = "shop.foodtruck.autopresent";

                priceText.localizationName = itemType + "_Price";
                priceText.ReLoad();

                infoText.localizationName = itemType + "_Info2";
                infoText.ReLoad();

                index = 22;
                effect.SetActive(true);
                break;
            case ItemType.BuffTicket:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(1000);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.BuffTicket;
                infoText.ReLoad();
                break;
            case ItemType.SkillTicket:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(500);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.SkillTicket;
                infoText.ReLoad();
                break;
            case ItemType.RepairTicket:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(20);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.RecoverTicket;
                infoText.ReLoad();
                break;
            case ItemType.RepairTicket10:
                lockedObj.SetActive(false);

                buyRankPointText.text = MoneyUnitString.ToCurrencyString(200);

                titleText.plusText = " x10";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.RecoverTicket;
                infoText.ReLoad();
                break;
        }

        titleText.ReLoad();
    }

    public void Click()
    {
        if (isDelay) return;

        shopManager.BuyItem(itemType);

        isDelay = true;
        Invoke("Delay", 0.5f);
    }

    public void BuyPurchase()
    {
        shopManager.BuyPurchase(index);
    }

    public void Failed()
    {
        shopManager.Failed();
    }

    public void SetLocked(bool check)
    {
        lockedObj.SetActive(check);
    }

    void Delay()
    {
        isDelay = false;
    }
}
