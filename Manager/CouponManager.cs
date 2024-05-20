using Firebase.Analytics;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CouponInfo
{
    public List<int> couponList = new List<int>(); //¸Å´Þ ÄíÆù
    public List<int> couponList2 = new List<int>();
    public List<int> couponList3 = new List<int>();

    public List<int> spCouponList = new List<int>(); //Æ¯º° ÄíÆù

    public void Initialize()
    {
        if(couponList.Count <= 0)
        {
            for (int i = 0; i < 999; i++)
            {
                couponList.Add(0);
                couponList2.Add(0);
                couponList3.Add(0);
                spCouponList.Add(0);
            }
        }

        for (int i = 0; i < couponList.Count; i++)
        {
            couponList[i] = 0;
            couponList2[i] = 0;
            couponList3[i] = 0;
            spCouponList[i] = 0;
        }
    }

    public void SaveServerData(CouponInfo info)
    {
        for (int i = 0; i < info.couponList.Count; i++)
        {
            couponList[i] = info.couponList[i];
        }

        for (int i = 0; i < info.couponList2.Count; i++)
        {
            couponList2[i] = info.couponList2[i];
        }

        for (int i = 0; i < info.couponList3.Count; i++)
        {
            couponList3[i] = info.couponList3[i];
        }

        for (int i = 0; i < info.spCouponList.Count; i++)
        {
            spCouponList[i] = info.spCouponList[i];
        }
    }
}

public class CouponManager : MonoBehaviour
{
    public GameObject couponView;
    public GameObject couponRewardView;

    public ReceiveContent[] receiveContents;

    public InputField inputFieldText;

    List<string> itemList = new List<string>();

    List<string> couponList = new List<string>();
    List<string> couponList2 = new List<string>();
    List<string> couponList3 = new List<string>();
    List<string> spCouponList = new List<string>();

    PlayerDataBase playerDataBase;

    private Dictionary<string, string> playerData = new Dictionary<string, string>();

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        couponView.SetActive(false);
        couponRewardView.SetActive(false);

        for (int i = 0; i < receiveContents.Length; i++)
        {
            receiveContents[i].gameObject.SetActive(false);
        }

        Initialize();
    }

    void Initialize()
    {
        couponList.Clear();
        couponList2.Clear();
        couponList3.Clear();
        spCouponList.Clear();

        couponList.Add("QLH0C6N4");
        couponList.Add("XZ6LPHV3");
        couponList.Add("1YSWKW4I");
        couponList.Add("AZO8P80O");
        couponList.Add("IF3FVXK6");
        couponList.Add("5L1R7W3D");
        couponList.Add("X5CKGV64");
        couponList.Add("S1NY5UID");
        couponList.Add("1TJGXS6N");
        couponList.Add("9PFD4BX6");
        couponList.Add("9VKXA6MD");
        couponList.Add("XGMHXC2S");

        couponList2.Add("WUXBHREL");
        couponList2.Add("S4ACKOCF");
        couponList2.Add("O6EJ7GHK");
        couponList2.Add("PFS8U615");
        couponList2.Add("O8M3KH6N");
        couponList2.Add("SFH2XLFB");
        couponList2.Add("B7DH0ZO6");
        couponList2.Add("IPO9AMX7");
        couponList2.Add("5A1KN32G");
        couponList2.Add("95SI95HN");
        couponList2.Add("WTI7J7X7");
        couponList2.Add("Y9I7DS2S");
        couponList2.Add("FH080ARL");
        couponList2.Add("1I2NGMH3");
        couponList2.Add("5OT3GN6T");
        couponList2.Add("7BNCLJDO");
        couponList2.Add("EPZLZWT0");
        couponList2.Add("5PN52UKT");
        couponList2.Add("GSXDOGOR");
        couponList2.Add("G4ORTELG");

        couponList3.Add("9OC084O7");
        couponList3.Add("9V5KSTZS");
        couponList3.Add("Y8S4GEYL");
        couponList3.Add("KD3RLLRM");
        couponList3.Add("W28QBAW0");
        couponList3.Add("6OZZF3LS");
        couponList3.Add("0W5MKRDD");
        couponList3.Add("49OV5RFW");
        couponList3.Add("X6Y6Q41T");
        couponList3.Add("QZD6BWJG");
        couponList3.Add("72VM3X5A");
        couponList3.Add("T05WMOUZ");
        couponList3.Add("KHGSV9SA");
        couponList3.Add("1CD3URRV");
        couponList3.Add("RP1IR73G");
        couponList3.Add("DY9QWLDO");
        couponList3.Add("5O4TG15M");
        couponList3.Add("8IJZ8AC5");
        couponList3.Add("B34PGQB6");
        couponList3.Add("AEHNUFWO");

        spCouponList.Add("2MXODNF2");
        spCouponList.Add("VYXPT7XK");
        spCouponList.Add("GVSCTBHY");
        spCouponList.Add("NU8S29PZ");
        spCouponList.Add("7DLB4AB0");
        spCouponList.Add("HF518C0O");
        spCouponList.Add("MPGCP8BN");
        spCouponList.Add("48SMEWCD");
        spCouponList.Add("J97DC3ON");
        spCouponList.Add("RCWGTK3P");
        spCouponList.Add("I8KJYXUM");
        spCouponList.Add("WHILILI");
        spCouponList.Add("20231119");
    }

    public void OpenCouponView()
    {
        if (!couponView.activeInHierarchy)
        {
            couponView.SetActive(true);

            inputFieldText.text = "";

            FirebaseAnalytics.LogEvent("Open_Coupon");
        }
        else
        {
            couponView.SetActive(false);
        }
    }

    public void RedeemCouponCode()
    {
        if(inputFieldText.text.Length == 0)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.CouponNotion3);
            return;
        }

        //if (inputFieldText.text.Contains("-"))
        //{
        //    inputFieldText.text.ToLower();

        //    GetCoupon();
        //}
        //else
        //{
        //}

        for(int i = 0; i < receiveContents.Length; i ++)
        {
            receiveContents[i].gameObject.SetActive(false);
        }

        if(inputFieldText.text.ToUpper().Equals("YUMMYOPEN"))
        {
            if (playerDataBase.couponInfo.couponList[0] == 0)
            {
                playerDataBase.couponInfo.couponList[0] = 1;

                PlayfabManager.instance.UpdateSellPriceGold(1000000);
                PlayfabManager.instance.moneyAnimation.PlusMoney(1000000);

                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 500);

                couponRewardView.SetActive(true);
                receiveContents[0].gameObject.SetActive(true);
                receiveContents[1].gameObject.SetActive(true);

                receiveContents[0].Initialize(RewardType.Gold, 1000000);
                receiveContents[1].Initialize(RewardType.Crystal, 500);

                FirebaseAnalytics.LogEvent("Clear_WelcomeCoupon");

                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessReward);

                return;
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.CouponNotion2);
            }
        }

        if (inputFieldText.text.ToUpper().Equals("YUMMYRUSH"))
        {
            if (DateTime.Now >= new DateTime(2024, 02, 29))
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.CouponNotion4);
                return;
            }

            if (playerDataBase.couponInfo.spCouponList[998] == 0)
            {
                playerDataBase.couponInfo.spCouponList[998] = 1;

                PlayfabManager.instance.UpdateAddGold(5000000);
                PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1000);

                couponRewardView.SetActive(true);
                receiveContents[0].gameObject.SetActive(true);
                receiveContents[1].gameObject.SetActive(true);

                receiveContents[0].Initialize(RewardType.Gold, 5000000);
                receiveContents[1].Initialize(RewardType.Crystal, 1000);

                if (playerDataBase.Character21 == 0)
                {
                    playerDataBase.Character21 = 1;

                    itemList.Clear();
                    itemList.Add(CharacterType.Character21.ToString());

                    PlayfabManager.instance.GrantItemToUser("Character", itemList);
                }

                SoundManager.instance.PlaySFX(GameSfxType.Success);
                NotionManager.instance.UseNotion(NotionType.SuccessReward);
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.CouponNotion2);
            }
        }


        for (int i = 0; i < couponList.Count; i++)
        {
            if (inputFieldText.text.ToUpper().Equals(couponList[i]))
            {
                if(playerDataBase.couponInfo.couponList[i] == 0)
                {
                    playerDataBase.couponInfo.couponList[i] = 1;

                    GetReward();
                    return;
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                    return;
                }
            }
        }

        for (int i = 0; i < couponList2.Count; i++)
        {
            if (inputFieldText.text.ToUpper().Equals(couponList2[i]))
            {
                if (playerDataBase.couponInfo.couponList2[i] == 0)
                {
                    playerDataBase.couponInfo.couponList2[i] = 1;

                    GetReward2();
                    return;
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                    return;
                }
            }
        }

        for (int i = 0; i < couponList3.Count; i++)
        {
            if (inputFieldText.text.ToUpper().Equals(couponList3[i]))
            {
                if (playerDataBase.couponInfo.couponList3[i] == 0)
                {
                    playerDataBase.couponInfo.couponList3[i] = 1;

                    GetReward3();
                    return;
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                    return;
                }
            }
        }

        for (int i = 0; i < spCouponList.Count; i++)
        {
            if (inputFieldText.text.ToUpper().Equals(spCouponList[i]))
            {
                if (playerDataBase.couponInfo.spCouponList[i] == 0)
                {
                    playerDataBase.couponInfo.spCouponList[i] = 1;

                    GetSpeicalReward();
                    return;
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                    return;
                }
            }
        }

        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        NotionManager.instance.UseNotion(NotionType.CouponNotion3);
    }

    public void GetReward()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 500);
        PortionManager.instance.GetAllPortion(2);

        couponRewardView.SetActive(true);
        receiveContents[0].gameObject.SetActive(true);
        receiveContents[1].gameObject.SetActive(true);

        receiveContents[0].Initialize(RewardType.Crystal, 500);
        receiveContents[1].Initialize(RewardType.PortionSet, 2);

        FirebaseAnalytics.LogEvent("Reward_Coupon");

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        playerData.Clear();
        playerData.Add("CouponInfo", JsonUtility.ToJson(playerDataBase.couponInfo));
        PlayfabManager.instance.SetPlayerData(playerData);
    }

    public void GetReward2()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);

        couponRewardView.SetActive(true);
        receiveContents[0].gameObject.SetActive(true);
        receiveContents[1].gameObject.SetActive(false);

        receiveContents[0].Initialize(RewardType.Crystal, 100);

        FirebaseAnalytics.LogEvent("Reward_Coupon2");

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        playerData.Clear();
        playerData.Add("CouponInfo", JsonUtility.ToJson(playerDataBase.couponInfo));
        PlayfabManager.instance.SetPlayerData(playerData);
    }

    public void GetReward3()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 200);

        couponRewardView.SetActive(true);
        receiveContents[0].gameObject.SetActive(true);
        receiveContents[1].gameObject.SetActive(false);

        receiveContents[0].Initialize(RewardType.Crystal, 200);

        FirebaseAnalytics.LogEvent("Reward_Coupon3");

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        playerData.Clear();
        playerData.Add("CouponInfo", JsonUtility.ToJson(playerDataBase.couponInfo));
        PlayfabManager.instance.SetPlayerData(playerData);
    }

    public void GetSpeicalReward()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1000);
        PortionManager.instance.GetDefTickets(30);

        couponRewardView.SetActive(true);
        receiveContents[0].gameObject.SetActive(true);
        receiveContents[1].gameObject.SetActive(true);

        receiveContents[0].Initialize(RewardType.Crystal, 1000);
        receiveContents[1].Initialize(RewardType.DefDestroyTicket, 30);

        FirebaseAnalytics.LogEvent("Reward_Coupon_Speical");

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        playerData.Clear();
        playerData.Add("CouponInfo", JsonUtility.ToJson(playerDataBase.couponInfo));
        PlayfabManager.instance.SetPlayerData(playerData);
    }

    public void CloseCouponReward()
    {
        couponRewardView.SetActive(false);
    }

    void GetCoupon()
    {
        var primaryCatalogName = "Coupon"; // In your game, this should just be a constant matching your primary catalog
        var request = new RedeemCouponRequest
        {
            CatalogVersion = primaryCatalogName,
            CouponCode = inputFieldText.text // This comes from player input, in this case, one of the coupon codes generated above
        };
        PlayFabClientAPI.RedeemCoupon(request, OnCouponRedeemed, OnCouponRedeemError);
    }

    private void OnCouponRedeemed(RedeemCouponResult result)
    {
        if (result.Request != null)
        {
            Debug.Log("Coupon redeemed successfully!");

            //mailBoxManager.GetUserInventoryCoupon();

            SoundManager.instance.PlaySFX(GameSfxType.Success);
            NotionManager.instance.UseNotion(NotionType.CouponNotion1);
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.CouponNotion3);
        }
    }

    private void OnCouponRedeemError(PlayFabError error)
    {
        Debug.LogError("Error redeeming coupon code: " + error.ErrorMessage);

        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        NotionManager.instance.UseNotion(NotionType.CouponNotion3);
    }
}
