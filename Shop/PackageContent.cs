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

                receiveContents[0].Initialize(RewardType.Gold, 6000000);
                receiveContents[1].Initialize(RewardType.Crystal, 600);
                receiveContents[2].Initialize(RewardType.PortionSet, 10);
                receiveContents[3].Initialize(RewardType.EventTicket, 10);
                receiveContents[4].gameObject.SetActive(false);
                break;
            case PackageType.Package2:
                iapButton.productId = "shop.foodtruck.package2"; //20000

                receiveContents[0].Initialize(RewardType.Gold, 180000000); //3000
                receiveContents[1].Initialize(RewardType.Crystal, 8500); //14000
                receiveContents[2].Initialize(RewardType.PortionSet, 40); //3000
                receiveContents[3].Initialize(RewardType.EventTicket, 50);
                receiveContents[4].gameObject.SetActive(false);
                break;
            case PackageType.Package3:
                iapButton.productId = "shop.foodtruck.package3"; //60000

                receiveContents[0].Initialize(RewardType.Gold, 600000000); //10000
                receiveContents[1].Initialize(RewardType.Crystal, 30000); //50000
                receiveContents[2].Initialize(RewardType.PortionSet, 100); //10000
                receiveContents[3].Initialize(RewardType.EventTicket, 100);
                receiveContents[4].gameObject.SetActive(false);
                break;
            case PackageType.Package4:
                iapButton.productId = "shop.foodtruck.package4"; //100000

                receiveContents[0].Initialize(RewardType.Gold, 600000000); //10000
                receiveContents[1].Initialize(RewardType.Crystal, 42000); //70000
                receiveContents[2].Initialize(RewardType.DefDestroyTicket, 200); //20000
                receiveContents[3].Initialize(RewardType.EventTicket, 200);
                receiveContents[4].gameObject.SetActive(false);
                break;
            case PackageType.Package5: //한정 패키지
                iapButton.productId = "shop.foodtruck.package5";

                receiveContents[0].Initialize(RewardType.Gold, 12000000);
                receiveContents[1].Initialize(RewardType.Crystal, 1200);
                receiveContents[2].Initialize(RewardType.PortionSet, 20);
                receiveContents[3].Initialize(RewardType.EventTicket, 10);
                receiveContents[4].gameObject.SetActive(false);

                BuyLimitDate();
                break;
            case PackageType.Package6:
                iapButton.productId = "shop.foodtruck.package6";

                receiveContents[0].Initialize(RewardType.RemoveAds, -1);
                receiveContents[1].Initialize(RewardType.GoldX2, -1);
                receiveContents[2].Initialize(RewardType.AutoUpgrade, -1);
                receiveContents[3].Initialize(RewardType.EventTicket, 200);
                receiveContents[4].gameObject.SetActive(false);
                break;
            case PackageType.Package7: //서포트 패키지
                iapButton.productId = "shop.foodtruck.package7";

                receiveContents[0].Initialize(RewardType.Gold, 6000000);
                receiveContents[1].Initialize(RewardType.Crystal, 600);
                receiveContents[2].Initialize(RewardType.PortionSet, 10);
                receiveContents[3].Initialize(RewardType.EventTicket, 10);
                receiveContents[4].gameObject.SetActive(false);
                break;
        }

        priceText.localizationName = type.ToString();
        priceText.ReLoad();

        bestText.text = LocalizationManager.instance.GetString(type + "_Info");
    }

    public void BuyLimitDate()
    {
        if (playerDataBase.FirstDate.Length > 9)
        {
            playerDataBase.FirstDate = playerDataBase.FirstDate.Substring(1, playerDataBase.FirstDate.Length - 1);
        }

        if (playerDataBase.FirstServerDate.Length > 9)
        {
            playerDataBase.FirstServerDate = playerDataBase.FirstServerDate.Substring(1, playerDataBase.FirstServerDate.Length - 1);
        }

        if (playerDataBase.FirstDate[0] == '0')
        {
            playerDataBase.FirstDate = "1" + playerDataBase.FirstDate;
        }

        if (playerDataBase.FirstServerDate[0] == '0')
        {
            playerDataBase.FirstServerDate = "1" + playerDataBase.FirstServerDate;
        }

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
            shopManager.OffLimitPackage();
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
