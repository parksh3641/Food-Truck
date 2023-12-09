using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopContent : MonoBehaviour
{
    public ItemType itemType = ItemType.DailyReward;
    public BuyType buyType = BuyType.Free;

    public Image icon;

    public LocalizationContent titleText;
    public Text rewardText;
    public LocalizationContent infoText;

    public GameObject buyFreeObj;
    public GameObject[] buyRmObj;
    public LocalizationContent[] buyRmText;

    public GameObject buyAdObj;
    public GameObject buyCoinObj;
    public GameObject buyCrystalObj;
    public GameObject buyExchangeObj;
    public GameObject lockedObj;

    public Text buyCoinText;
    public Text buyCrystalText;

    public GameObject bestObj;
    public Text bestText;


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

        buyFreeObj.SetActive(false);

        for(int i = 0; i < buyRmObj.Length; i ++)
        {
            buyRmObj[i].SetActive(false);
        }

        buyAdObj.SetActive(false);
        buyCoinObj.SetActive(false);
        buyCrystalObj.SetActive(false);
        buyExchangeObj.SetActive(false);

        bestObj.SetActive(false);

        switch (buyType)
        {
            case BuyType.Free:
                buyFreeObj.SetActive(true);
                break;
            case BuyType.Rm:
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
        }

        switch (itemType)
        {
            case ItemType.DailyReward:
                rewardText.text = MoneyUnitString.ToCurrencyString(100000);
                break;
            case ItemType.AdReward_Gold:
                rewardText.text = MoneyUnitString.ToCurrencyString(200000) + " ~\n" + MoneyUnitString.ToCurrencyString(1000000);
                break;
            case ItemType.DefDestroyTicket:
                lockedObj.SetActive(false);

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(100);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.DefDestroyTicket;
                infoText.ReLoad();

                break;
            case ItemType.GoldShop1:
                lockedObj.SetActive(false);

                rewardText.text = MoneyUnitString.ToCurrencyString(1000000);

                buyCrystalText.text = "120";
                break;
            case ItemType.GoldShop2:
                lockedObj.SetActive(false);

                rewardText.text = MoneyUnitString.ToCurrencyString(10000000);

                buyCrystalText.text = "1,200";
                break;
            case ItemType.GoldShop3:
                lockedObj.SetActive(false);

                rewardText.text = MoneyUnitString.ToCurrencyString(100000000);

                buyCrystalText.text = "12,000";
                break;
            case ItemType.AdReward_Portion:
                rewardText.text = "";

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                break;
            case ItemType.RemoveAds:
                lockedObj.SetActive(false);

                buyRmObj[3].SetActive(true);
                buyRmText[3].localizationName = itemType + "_Price";
                buyRmText[3].ReLoad();

                break;
            case ItemType.PortionSet1:
                lockedObj.SetActive(false);

                buyRmObj[4].SetActive(true);
                buyRmText[4].localizationName = itemType + "_Price";
                buyRmText[4].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                break;
            case ItemType.PortionSet2:
                lockedObj.SetActive(false);

                buyRmObj[5].SetActive(true);
                buyRmText[5].localizationName = itemType + "_Price";
                buyRmText[5].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                bestObj.SetActive(true);
                bestText.text = "BEST";

                break;
            case ItemType.PortionSet3:
                lockedObj.SetActive(false);

                buyRmObj[6].SetActive(true);
                buyRmText[6].localizationName = itemType + "_Price";
                buyRmText[6].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                break;
            case ItemType.DailyReward_Portion:
                rewardText.text = "";

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();
                break;
            case ItemType.GoldX2:
                lockedObj.SetActive(false);

                buyRmObj[7].SetActive(true);
                buyRmText[7].localizationName = itemType + "_Price";
                buyRmText[7].ReLoad();

                rewardText.text = "+100%";
                break;
            case ItemType.CrystalShop1:
                lockedObj.SetActive(false);

                buyRmObj[8].SetActive(true);
                buyRmText[8].localizationName = itemType + "_Price";
                buyRmText[8].ReLoad();

                rewardText.text = "80";

                break;
            case ItemType.CrystalShop2:
                lockedObj.SetActive(false);

                buyRmObj[9].SetActive(true);
                buyRmText[9].localizationName = itemType + "_Price";
                buyRmText[9].ReLoad();

                rewardText.text = "500";

                break;
            case ItemType.CrystalShop3:
                lockedObj.SetActive(false);

                buyRmObj[10].SetActive(true);
                buyRmText[10].localizationName = itemType + "_Price";
                buyRmText[10].ReLoad();

                rewardText.text = "1,200";

                bestObj.SetActive(true);
                bestText.text = "BEST";
                break;
            case ItemType.CrystalShop4:
                lockedObj.SetActive(false);

                buyRmObj[11].SetActive(true);
                buyRmText[11].localizationName = itemType + "_Price";
                buyRmText[11].ReLoad();

                rewardText.text = "2,500";
                break;
            case ItemType.CrystalShop5:
                lockedObj.SetActive(false);

                buyRmObj[12].SetActive(true);
                buyRmText[12].localizationName = itemType + "_Price";
                buyRmText[12].ReLoad();

                rewardText.text = "6,500";

                break;
            case ItemType.CrystalShop6:
                lockedObj.SetActive(false);

                buyRmObj[13].SetActive(true);
                buyRmText[13].localizationName = itemType + "_Price";
                buyRmText[13].ReLoad();

                rewardText.text = "14,000";
                break;
            case ItemType.Portion1:
                lockedObj.SetActive(false);

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(3);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion1;
                infoText.ReLoad();
                break;
            case ItemType.Portion2:
                lockedObj.SetActive(false);

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(10);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion2;
                infoText.ReLoad();
                break;
            case ItemType.Portion3:
                lockedObj.SetActive(false);

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(10);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion3;
                infoText.ReLoad();
                break;
            case ItemType.Portion4:
                lockedObj.SetActive(false);

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(5);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion4;
                infoText.ReLoad();
                break;
            case ItemType.Portion5:
                lockedObj.SetActive(false);

                buyCrystalText.text = MoneyUnitString.ToCurrencyString(10);

                titleText.plusText = " x1";

                infoText.localizationName = "Hold";
                infoText.plusText = " : " + playerDataBase.Portion5;
                infoText.ReLoad();
                break;
            case ItemType.DefDestroyTicketSlices:
                lockedObj.SetActive(false);

                titleText.plusText = " x1";

                infoText.localizationName = "HoldPiece";
                infoText.plusText = " : " + playerDataBase.DefDestroyTicketPiece + "/10";
                infoText.ReLoad();
                break;
            case ItemType.DefDestroyTicketPiece:
                rewardText.text = "x1";
                break;
            case ItemType.BuffTicketSet1:
                lockedObj.SetActive(false);

                buyRmObj[14].SetActive(true);
                buyRmText[14].localizationName = itemType.ToString();
                buyRmText[14].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();
                break;
            case ItemType.BuffTicketSet2:
                lockedObj.SetActive(false);

                buyRmObj[15].SetActive(true);
                buyRmText[15].localizationName = itemType.ToString();
                buyRmText[15].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                bestObj.SetActive(true);
                bestText.text = "BEST";
                break;
            case ItemType.BuffTicketSet3:
                lockedObj.SetActive(false);

                buyRmObj[16].SetActive(true);
                buyRmText[16].localizationName = itemType.ToString();
                buyRmText[16].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();
                break;
            case ItemType.DefTicketSet1:
                lockedObj.SetActive(false);

                buyRmObj[17].SetActive(true);
                buyRmText[17].localizationName = itemType.ToString();
                buyRmText[17].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();
                break;
            case ItemType.DefTicketSet2:
                lockedObj.SetActive(false);

                buyRmObj[18].SetActive(true);
                buyRmText[18].localizationName = itemType.ToString();
                buyRmText[18].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();
                break;
            case ItemType.DefTicketSet3:
                lockedObj.SetActive(false);

                buyRmObj[19].SetActive(true);
                buyRmText[19].localizationName = itemType.ToString();
                buyRmText[19].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();

                bestObj.SetActive(true);
                bestText.text = "BEST";
                break;
            case ItemType.SuperOffline:
                lockedObj.SetActive(false);

                buyRmObj[20].SetActive(true);
                buyRmText[20].localizationName = itemType.ToString();
                buyRmText[20].ReLoad();

                infoText.localizationName = itemType + "_Info";
                infoText.ReLoad();
                break;
        }

        titleText.ReLoad();
    }

    public void Click()
    {
        shopManager.BuyItem(itemType);
    }

    public void BuyPurchase(int index)
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
}
