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

        //for (int i = 0; i < receiveContents.Length; i ++)
        //{
        //    receiveContents[i].gameObject.SetActive(false);
        //}
    }

    public void Initialize(PackageType type, ShopManager manager)
    {
        shopManager = manager;

        titleText.text = LocalizationManager.instance.GetString(type.ToString());

        switch (type)
        {
            case PackageType.Package1:
                receiveContents[0].Initialize(RewardType.Gold, 10);
                receiveContents[1].Initialize(RewardType.Crystal, 10);
                receiveContents[2].Initialize(RewardType.Portion1, 10);
                receiveContents[3].Initialize(RewardType.Portion2, 10);
                break;
            case PackageType.Package2:
                receiveContents[0].Initialize(RewardType.Gold, 20);
                receiveContents[1].Initialize(RewardType.Crystal, 20);
                receiveContents[2].Initialize(RewardType.Portion1, 20);
                receiveContents[3].Initialize(RewardType.Portion2, 20);
                break;
            case PackageType.Package3:
                receiveContents[0].Initialize(RewardType.Gold, 30);
                receiveContents[1].Initialize(RewardType.Crystal, 30);
                receiveContents[2].Initialize(RewardType.Portion1, 30);
                receiveContents[3].Initialize(RewardType.Portion2, 30);
                break;
            case PackageType.Package4:
                receiveContents[0].Initialize(RewardType.Gold, 40);
                receiveContents[1].Initialize(RewardType.Crystal, 40);
                receiveContents[2].Initialize(RewardType.Portion1, 40);
                receiveContents[3].Initialize(RewardType.Portion2, 40);
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
