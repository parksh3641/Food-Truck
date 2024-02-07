using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using Firebase.Analytics;

public class AdmobReward : MonoBehaviour
{
    private RewardedAd rewardedAd;

    public string androidUnitId;
    public string iosUnitId;

    string adUnitId;

    public ShopManager shopManager;
    public ChestBoxManager chestBoxManager;
    public BuffManager buffManager;
    public ReincarnationManager reincarnationManager;
    public OfflineManager offlineManager;
    public QuestManager questManager;
    public TreasureManager treasureManager;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }


    void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });

#if UNITY_ANDROID
        adUnitId = androidUnitId;
#elif UNITY_IOS
        adUnitId = iosUnitId;
#else
        adUnitId = "unexpected_platform";
#endif

        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }


    public void ShowAd(int number)
    {
        if (playerDataBase.RemoveAds)
        {
            switch (number)
            {
                case 0:
                    shopManager.SuccessWatchAd();
                    break;
                case 1:
                    shopManager.SuccessWatchAd_Portion();
                    break;
                case 2:
                    chestBoxManager.SuccessWatchAd();
                    break;
                case 3:
                    buffManager.SuccessWatchAd();
                    break;
                case 4:
                    reincarnationManager.SuccessWatchAd();
                    break;
                case 5:
                    offlineManager.SuccessWatchAd();
                    break;
                case 6:
                    questManager.SuccessWatchAd();
                    break;
                case 7:
                    treasureManager.SuccessWatchAd();
                    break;
                case 8:
                    shopManager.SuccessWatchAd_Crystal();
                    break;
            }
        }
        else
        {
            const string rewardMsg =
                "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    Debug.Log("Ad Watch Success!");

                    switch (number)
                    {
                        case 0:
                            shopManager.SuccessWatchAd();
                            break;
                        case 1:
                            shopManager.SuccessWatchAd_Portion();
                            break;
                        case 2:
                            chestBoxManager.SuccessWatchAd();
                            break;
                        case 3:
                            buffManager.SuccessWatchAd();
                            break;
                        case 4:
                            reincarnationManager.SuccessWatchAd();
                            break;
                        case 5:
                            offlineManager.SuccessWatchAd();
                            break;
                        case 6:
                            questManager.SuccessWatchAd();
                            break;
                        case 7:
                            treasureManager.SuccessWatchAd();
                            break;
                        case 8:
                            shopManager.SuccessWatchAd_Crystal();
                            break;
                    }

                    LoadRewardedAd();

                    playerDataBase.AdCount += 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("AdCount", playerDataBase.AdCount);

                    FirebaseAnalytics.LogEvent("Watch Ad");

                    //Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                });
            }
            else
            {
                LoadRewardedAd();

                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.CancelWatchAd);
            }
        }
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += (null);
        {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }
}