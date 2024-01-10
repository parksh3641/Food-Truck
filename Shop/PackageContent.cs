using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageContent : MonoBehaviour
{
    public PackageType packageType = PackageType.Package1;

    public Text titleText;

    public ReceiveContent[] receiveContents;

    public GameObject[] buyRmObj;
    public LocalizationContent[] beforePriceText;
    public LocalizationContent[] nowPriceText;

    public Text bestText;

    ShopManager shopManager;

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void Initialize(PackageType type, ShopManager manager)
    {
        shopManager = manager;

        packageType = type;

        titleText.text = LocalizationManager.instance.GetString(type.ToString());

        //for (int i = 0; i < receiveContents.Length; i++)
        //{
        //    receiveContents[i].gameObject.SetActive(false);
        //}

        switch (type)
        {
            case PackageType.Package1:
                receiveContents[0].Initialize(RewardType.Gold, 3000000);
                receiveContents[1].Initialize(RewardType.Crystal, 600);
                receiveContents[2].Initialize(RewardType.PortionSet, 10);
                receiveContents[3].Initialize(RewardType.BuffTicket, 10);
                break;
            case PackageType.Package2:
                receiveContents[0].Initialize(RewardType.Gold, 10000000);
                receiveContents[1].Initialize(RewardType.Crystal, 8000);
                receiveContents[2].Initialize(RewardType.PortionSet, 40);
                receiveContents[3].Initialize(RewardType.BuffTicket, 60);
                break;
            case PackageType.Package3:
                receiveContents[0].Initialize(RewardType.Gold, 20000000);
                receiveContents[1].Initialize(RewardType.Crystal, 25000);
                receiveContents[2].Initialize(RewardType.PortionSet, 120);
                receiveContents[3].Initialize(RewardType.BuffTicket, 120);
                break;
            case PackageType.Package4:
                receiveContents[0].Initialize(RewardType.Gold, 60000000);
                receiveContents[1].Initialize(RewardType.Crystal, 40000);
                receiveContents[2].Initialize(RewardType.BuffTicket, 240);
                receiveContents[3].Initialize(RewardType.DefDestroyTicket, 200);
                break;
            case PackageType.Package5:
                receiveContents[0].Initialize(RewardType.Gold, 6000000);
                receiveContents[1].Initialize(RewardType.Crystal, 1200);
                receiveContents[2].Initialize(RewardType.PortionSet, 20);
                receiveContents[3].Initialize(RewardType.BuffTicket, 20);
                break;
            case PackageType.Package6:
                receiveContents[0].Initialize(RewardType.RemoveAds, -1);
                receiveContents[1].Initialize(RewardType.GoldX2, -1);
                receiveContents[2].Initialize(RewardType.AutoUpgrade, -1);
                receiveContents[3].Initialize(RewardType.AutoPresent, -1);
                break;
        }

        for (int i = 0; i < buyRmObj.Length; i++)
        {
            buyRmObj[i].SetActive(false);
        }

        buyRmObj[(int)type].SetActive(true);

        beforePriceText[(int)type].localizationName = type.ToString() +"_Before";
        nowPriceText[(int)type].localizationName = type.ToString();

        beforePriceText[(int)type].ReLoad();
        nowPriceText[(int)type].ReLoad();

        bestText.text = LocalizationManager.instance.GetString(type + "_Info");
    }

    public void BuyPurchase()
    {
        shopManager.BuyPackage(packageType);
    }

    public void Failed()
    {
        shopManager.Failed();
    }

}
