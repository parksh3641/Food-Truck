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

        titleText.text = LocalizationManager.instance.GetString(type.ToString());

        //for (int i = 0; i < receiveContents.Length; i++)
        //{
        //    receiveContents[i].gameObject.SetActive(false);
        //}

        switch (type)
        {
            case PackageType.Package1:
                receiveContents[0].Initialize(RewardType.Gold, 5000000);
                receiveContents[1].Initialize(RewardType.Crystal, 50);
                receiveContents[2].Initialize(RewardType.PortionSet, 5);
                receiveContents[3].Initialize(RewardType.BuffTickets, 10);
                break;
            case PackageType.Package2:
                receiveContents[0].Initialize(RewardType.Gold, 50000000);
                receiveContents[1].Initialize(RewardType.Crystal, 500);
                receiveContents[2].Initialize(RewardType.PortionSet, 40);
                receiveContents[3].Initialize(RewardType.BuffTickets, 50);
                break;
            case PackageType.Package3:
                receiveContents[0].Initialize(RewardType.Gold, 100000000);
                receiveContents[1].Initialize(RewardType.Crystal, 3000);
                receiveContents[2].Initialize(RewardType.PortionSet, 150);
                receiveContents[3].Initialize(RewardType.BuffTickets, 100);
                break;
            case PackageType.Package4:
                receiveContents[0].Initialize(RewardType.Gold, 200000000);
                receiveContents[1].Initialize(RewardType.Crystal, 5000);
                receiveContents[2].Initialize(RewardType.BuffTickets, 100);
                receiveContents[3].Initialize(RewardType.DefDestroyTicket, 100);
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
