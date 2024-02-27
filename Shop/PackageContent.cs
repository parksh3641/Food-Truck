using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class PackageContent : MonoBehaviour
{
    public PackageType packageType = PackageType.Package1;

    public Text titleText;

    public ReceiveContent[] receiveContents;

    public CodelessIAPButton iapButton;
    public LocalizationContent priceText;

    public Text bestText;
    public Text timerText;

    string localization_Time = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";
    string localization_Seconds = "";

    DateTime time;
    DateTime serverTime;
    TimeSpan timeSpan;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

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

        if(timerText != null)
        {
            timerText.text = "";
        }

        //for (int i = 0; i < receiveContents.Length; i++)
        //{
        //    receiveContents[i].gameObject.SetActive(false);
        //}

        switch (type)
        {
            case PackageType.Package1:
                iapButton.productId = "shop.foodtruck.package1";

                receiveContents[0].Initialize(RewardType.Gold, 3000000);
                receiveContents[1].Initialize(RewardType.Crystal, 600);
                receiveContents[2].Initialize(RewardType.PortionSet, 10);
                receiveContents[3].Initialize(RewardType.BuffTicket, 10);
                break;
            case PackageType.Package2:
                iapButton.productId = "shop.foodtruck.package2";

                receiveContents[0].Initialize(RewardType.Gold, 10000000);
                receiveContents[1].Initialize(RewardType.Crystal, 8000);
                receiveContents[2].Initialize(RewardType.PortionSet, 40);
                receiveContents[3].Initialize(RewardType.BuffTicket, 40);
                break;
            case PackageType.Package3:
                iapButton.productId = "shop.foodtruck.package3";

                receiveContents[0].Initialize(RewardType.Gold, 20000000);
                receiveContents[1].Initialize(RewardType.Crystal, 25000);
                receiveContents[2].Initialize(RewardType.PortionSet, 60);
                receiveContents[3].Initialize(RewardType.BuffTicket, 60);
                break;
            case PackageType.Package4:
                iapButton.productId = "shop.foodtruck.package4";

                receiveContents[0].Initialize(RewardType.Gold, 60000000);
                receiveContents[1].Initialize(RewardType.Crystal, 50000);
                receiveContents[2].Initialize(RewardType.BuffTicket, 120);
                receiveContents[3].Initialize(RewardType.DefDestroyTicket, 100);
                break;
            case PackageType.Package5:
                iapButton.productId = "shop.foodtruck.package5";

                receiveContents[0].Initialize(RewardType.Gold, 6000000);
                receiveContents[1].Initialize(RewardType.Crystal, 1200);
                receiveContents[2].Initialize(RewardType.PortionSet, 20);
                receiveContents[3].Initialize(RewardType.BuffTicket, 20);
                break;
            case PackageType.Package6:
                iapButton.productId = "shop.foodtruck.package6";

                receiveContents[0].Initialize(RewardType.RemoveAds, -1);
                receiveContents[1].Initialize(RewardType.GoldX2, -1);
                receiveContents[2].Initialize(RewardType.AutoUpgrade, -1);
                receiveContents[3].Initialize(RewardType.AutoPresent, -1);
                break;
            case PackageType.Package7:
                iapButton.productId = "shop.foodtruck.package7";

                receiveContents[0].Initialize(RewardType.Gold, 3000000);
                receiveContents[1].Initialize(RewardType.Crystal, 600);
                receiveContents[2].Initialize(RewardType.PortionSet, 10);
                receiveContents[3].Initialize(RewardType.BuffTicket, 10);
                break;
        }

        priceText.localizationName = type.ToString();
        priceText.ReLoad();

        bestText.text = LocalizationManager.instance.GetString(type + "_Info");
    }

    public void BuyLimitDate()
    {
        time = DateTime.ParseExact(DateTime.Now.ToString("yyyy") + playerDataBase.FirstDate.Substring(1, playerDataBase.FirstDate.Length - 1), "yyyyMMddHHmm", CultureInfo.CurrentCulture);
        serverTime = DateTime.ParseExact(DateTime.Now.ToString("yyyy") + playerDataBase.FirstServerDate.Substring(1, playerDataBase.FirstServerDate.Length - 1), "yyyyMMddHHmm", CultureInfo.CurrentCulture);

        localization_Time = LocalizationManager.instance.GetString("LimitDayTime");
        localization_Days = LocalizationManager.instance.GetString("Days");
        localization_Hours = LocalizationManager.instance.GetString("Hours");
        localization_Minutes = LocalizationManager.instance.GetString("Minutes");
        localization_Seconds = LocalizationManager.instance.GetString("Seconds");

        StopAllCoroutines();
        StartCoroutine(TimerCoroution());
    }

    IEnumerator TimerCoroution()
    {
        if (DateTime.Compare(DateTime.Now, serverTime) == -1)
        {
            timeSpan = serverTime - DateTime.Now;

            if (timeSpan.Days > 0)
            {
                timerText.text = localization_Time + "\n" + timeSpan.Days.ToString("D2") + localization_Days + " " + timeSpan.Hours.ToString("D2") + localization_Hours;
            }
            else
            {
                if (timeSpan.Hours > 0)
                {
                    timerText.text = localization_Time + "\n" + timeSpan.Hours.ToString("D2") + localization_Hours + " " + timeSpan.Minutes.ToString("D2") + localization_Minutes;
                }
                else
                {
                    if (timeSpan.Minutes == 0)
                    {
                        timerText.text = localization_Time + "\n" + timeSpan.Seconds.ToString("D2") + localization_Seconds;
                    }
                    else
                    {
                        timerText.text = localization_Time + "\n" + timeSpan.Minutes.ToString("D2") + localization_Minutes + " " + timeSpan.Seconds.ToString("D2") + localization_Seconds;
                    }
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
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
