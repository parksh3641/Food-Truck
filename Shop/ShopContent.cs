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

    public GameObject buyFreeObj;
    public GameObject[] buyRmObj;
    public LocalizationContent[] buyRmText;

    public GameObject buyAdObj;
    public GameObject buyCoinObj;

    public GameObject lockedObj;

    public Text buyCoinText;


    Sprite[] itemArray;

    ShopManager shopManager;

    ImageDataBase imageDataBase;


    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

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
        }

        switch (itemType)
        {
            case ItemType.DailyReward:
                rewardText.text = MoneyUnitString.ToCurrencyString(100000);
                break;
            case ItemType.AdReward_Gold:
                rewardText.text = MoneyUnitString.ToCurrencyString(1000000);
                break;
            case ItemType.DefDestroyTicket:
                lockedObj.SetActive(false);

                buyCoinText.text = MoneyUnitString.ToCurrencyString(5000000);

                titleText.plusText = " x1";
                break;
            case ItemType.GoldShop1:
                lockedObj.SetActive(false);

                rewardText.text = MoneyUnitString.ToCurrencyString(1000000);

                buyRmObj[0].SetActive(true);
                buyRmText[0].localizationName = itemType + "_Price";
                buyRmText[0].ReLoad();
                break;
            case ItemType.GoldShop2:
                lockedObj.SetActive(false);

                rewardText.text = MoneyUnitString.ToCurrencyString(5500000);

                buyRmObj[1].SetActive(true);
                buyRmText[1].localizationName = itemType + "_Price";
                buyRmText[1].ReLoad();
                break;
            case ItemType.GoldShop3:
                lockedObj.SetActive(false);

                rewardText.text = MoneyUnitString.ToCurrencyString(12000000);

                buyRmObj[2].SetActive(true);
                buyRmText[2].localizationName = itemType + "_Price";
                buyRmText[2].ReLoad();
                break;
            case ItemType.AdReward_Potion:
                break;
            case ItemType.AdReward_DefDestroyTicket:
                break;
        }

        titleText.ReLoad();
    }

    public void Click()
    {
        shopManager.BuyItem(itemType, buyType);
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
