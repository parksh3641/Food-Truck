using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CouponManager : MonoBehaviour
{
    public GameObject couponView;
    public GameObject couponRewardView;

    public ReceiveContent[] receiveContents;

    public InputField inputFieldText;

    List<string> itemList = new List<string>();

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        couponView.SetActive(false);
        couponRewardView.SetActive(false);

        for (int i = 0; i < receiveContents.Length; i++)
        {
            receiveContents[i].gameObject.SetActive(false);
        }
    }

    public void OpenCouponView()
    {
        if (!couponView.activeInHierarchy)
        {
            couponView.SetActive(true);

            inputFieldText.text = "";

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


        switch (inputFieldText.text)
        {
            case "YUMMYOPEN":
                //if (System.DateTime.Now >= new System.DateTime(2023, 10, 1))
                //{
                //}
                //else
                //{
                //    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                //    NotionManager.instance.UseNotion(NotionType.CouponNotion3);
                //}
                if (playerDataBase.Coupon1 == 0)
                {
                    playerDataBase.Coupon1 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon1", 1);

                    PlayfabManager.instance.UpdateAddGold(1000000);
                    PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);

                    playerDataBase.Portion1 += 5;
                    playerDataBase.Portion2 += 5;
                    playerDataBase.Portion3 += 5;
                    playerDataBase.Portion4 += 5;

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);

                    couponRewardView.SetActive(true);
                    receiveContents[0].gameObject.SetActive(true);
                    receiveContents[1].gameObject.SetActive(true);
                    receiveContents[2].gameObject.SetActive(true);
                    receiveContents[3].gameObject.SetActive(true);
                    receiveContents[4].gameObject.SetActive(true);
                    receiveContents[5].gameObject.SetActive(true);

                    receiveContents[0].Initialize(RewardType.Gold, 1000000);
                    receiveContents[1].Initialize(RewardType.Crystal, 300);
                    receiveContents[2].Initialize(RewardType.Portion1, 5);
                    receiveContents[3].Initialize(RewardType.Portion2, 5);
                    receiveContents[4].Initialize(RewardType.Portion3, 5);
                    receiveContents[5].Initialize(RewardType.Portion4, 5);

                    SoundManager.instance.PlaySFX(GameSfxType.Success);
                    NotionManager.instance.UseNotion(NotionType.SuccessReward);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "QLH0C6N4":
                if (playerDataBase.Coupon2 == 0)
                {
                    playerDataBase.Coupon2 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon2", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "XZ6LPHV3":
                if (playerDataBase.Coupon3 == 0)
                {
                    playerDataBase.Coupon3 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon3", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "1YSWKW4I":
                if (playerDataBase.Coupon4 == 0)
                {
                    playerDataBase.Coupon4 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon4", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "AZO8P80O":
                if (playerDataBase.Coupon5 == 0)
                {
                    playerDataBase.Coupon5 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon5", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "IF3FVXK6":
                if (playerDataBase.Coupon6 == 0)
                {
                    playerDataBase.Coupon6 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon6", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "5L1R7W3D":
                if (playerDataBase.Coupon7 == 0)
                {
                    playerDataBase.Coupon7 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon7", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "X5CKGV64":
                if (playerDataBase.Coupon8 == 0)
                {
                    playerDataBase.Coupon8 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon8", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "S1NY5UID":
                if (playerDataBase.Coupon9 == 0)
                {
                    playerDataBase.Coupon9 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon9", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "1TJGXS6N":
                if (playerDataBase.Coupon10 == 0)
                {
                    playerDataBase.Coupon10 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon10", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "9PFD4BX6":
                if (playerDataBase.Coupon11 == 0)
                {
                    playerDataBase.Coupon11 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon11", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "9VKXA6MD":
                if (playerDataBase.Coupon12 == 0)
                {
                    playerDataBase.Coupon12 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon12", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "XGMHXC2S":
                if (playerDataBase.Coupon13 == 0)
                {
                    playerDataBase.Coupon13 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon13", 1);

                    GetReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "2MXODNF2":
                if (playerDataBase.SpCoupon1 == 0)
                {
                    playerDataBase.SpCoupon1 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon1", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "VYXPT7XK":
                if (playerDataBase.SpCoupon2 == 0)
                {
                    playerDataBase.SpCoupon2 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon2", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "GVSCTBHY":
                if (playerDataBase.SpCoupon3 == 0)
                {
                    playerDataBase.SpCoupon3 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon3", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "NU8S29PZ":
                if (playerDataBase.SpCoupon4 == 0)
                {
                    playerDataBase.SpCoupon4 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon4", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "7DLB4AB0":
                if (playerDataBase.SpCoupon5 == 0)
                {
                    playerDataBase.SpCoupon5 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon5", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "HF518C0O":
                if (playerDataBase.SpCoupon6 == 0)
                {
                    playerDataBase.SpCoupon6 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon6", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "MPGCP8BN":
                if (playerDataBase.SpCoupon7 == 0)
                {
                    playerDataBase.SpCoupon7 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon7", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "48SMEWCD":
                if (playerDataBase.SpCoupon8 == 0)
                {
                    playerDataBase.SpCoupon8 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon8", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "J97DC3ON":
                if (playerDataBase.SpCoupon9 == 0)
                {
                    playerDataBase.SpCoupon9 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon9", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "RCWGTK3P":
                if (playerDataBase.SpCoupon10 == 0)
                {
                    playerDataBase.SpCoupon10 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon10", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "I8KJYXUM":
                if (playerDataBase.SpCoupon11 == 0)
                {
                    playerDataBase.SpCoupon11 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon11", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "WHILILI":
                if (playerDataBase.SpCoupon12 == 0)
                {
                    playerDataBase.SpCoupon12 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon12", 1);

                    GetSpeicalReward();
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "YUMMYRUSH":
                if (DateTime.Now >= new DateTime(2023, 12, 31))
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion4);
                    return;
                }

                if (playerDataBase.SpCoupon13 == 0)
                {
                    playerDataBase.SpCoupon13 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("SpCoupon13", 1);

                    PlayfabManager.instance.UpdateAddGold(1000000);
                    PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1000);

                    playerDataBase.DefDestroyTicket += 10;
                    playerDataBase.Portion5 += 10;

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicket", playerDataBase.DefDestroyTicket);
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);

                    couponRewardView.SetActive(true);
                    receiveContents[0].gameObject.SetActive(true);
                    receiveContents[1].gameObject.SetActive(true);
                    receiveContents[2].gameObject.SetActive(true);
                    receiveContents[3].gameObject.SetActive(true);

                    receiveContents[0].Initialize(RewardType.Gold, 2000000);
                    receiveContents[1].Initialize(RewardType.Crystal, 100);
                    receiveContents[2].Initialize(RewardType.DefDestroyTicket, 10);
                    receiveContents[3].Initialize(RewardType.Portion5, 10);

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
                break;

            default:
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.CouponNotion3);
                break;
        }
    }

    public void GetReward()
    {
        PlayfabManager.instance.UpdateAddGold(1000000);

        playerDataBase.Portion1 += 5;
        playerDataBase.Portion2 += 5;
        playerDataBase.Portion3 += 5;
        playerDataBase.Portion4 += 5;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);

        couponRewardView.SetActive(true);
        receiveContents[0].gameObject.SetActive(true);
        receiveContents[1].gameObject.SetActive(true);
        receiveContents[2].gameObject.SetActive(true);
        receiveContents[3].gameObject.SetActive(true);
        receiveContents[4].gameObject.SetActive(true);

        receiveContents[0].Initialize(RewardType.Gold, 1000000);
        receiveContents[1].Initialize(RewardType.Portion1, 5);
        receiveContents[2].Initialize(RewardType.Portion2, 5);
        receiveContents[3].Initialize(RewardType.Portion3, 5);
        receiveContents[4].Initialize(RewardType.Portion4, 5);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }

    public void GetSpeicalReward()
    {
        PlayfabManager.instance.UpdateAddGold(1000000);
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);

        couponRewardView.SetActive(true);
        receiveContents[0].gameObject.SetActive(true);
        receiveContents[1].gameObject.SetActive(true);

        receiveContents[0].Initialize(RewardType.Gold, 1000000);
        receiveContents[1].Initialize(RewardType.Crystal, 100);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
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
