using PlayFab;
using PlayFab.ClientModels;
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
            case "YUM7979A":
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
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "YUM58NA":
                if (playerDataBase.Coupon2 == 0)
                {
                    playerDataBase.Coupon2 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon2", 1);

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
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "YUM11DBA":
                if (playerDataBase.Coupon3 == 0)
                {
                    playerDataBase.Coupon3 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon3", 1);

                    PlayfabManager.instance.UpdateAddGold(500000);

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

                    receiveContents[0].Initialize(RewardType.Gold, 500000);
                    receiveContents[1].Initialize(RewardType.Portion1, 5);
                    receiveContents[2].Initialize(RewardType.Portion2, 5);
                    receiveContents[3].Initialize(RewardType.Portion3, 5);
                    receiveContents[4].Initialize(RewardType.Portion4, 5);

                    SoundManager.instance.PlaySFX(GameSfxType.Success);
                    NotionManager.instance.UseNotion(NotionType.SuccessReward);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "YUM11DBB":
                if (playerDataBase.Coupon4 == 0)
                {
                    playerDataBase.Coupon4 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon4", 1);

                    PlayfabManager.instance.UpdateAddGold(3000000);

                    playerDataBase.Portion1 += 10;
                    playerDataBase.Portion2 += 10;
                    playerDataBase.Portion3 += 10;
                    playerDataBase.Portion4 += 10;

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

                    receiveContents[0].Initialize(RewardType.Gold, 3000000);
                    receiveContents[1].Initialize(RewardType.Portion1, 10);
                    receiveContents[2].Initialize(RewardType.Portion2, 10);
                    receiveContents[3].Initialize(RewardType.Portion3, 10);
                    receiveContents[4].Initialize(RewardType.Portion4, 10);

                    SoundManager.instance.PlaySFX(GameSfxType.Success);
                    NotionManager.instance.UseNotion(NotionType.SuccessReward);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.CouponNotion2);
                }
                break;
            case "YUMMYRUSH":
                if (playerDataBase.Coupon5 == 0)
                {
                    playerDataBase.Coupon5 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon5", 1);

                    PlayfabManager.instance.UpdateAddGold(1000000);
                    PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);

                    playerDataBase.DefDestroyTicket += 5;
                    playerDataBase.Portion5 += 10;

                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicket", playerDataBase.DefDestroyTicket);
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);

                    couponRewardView.SetActive(true);
                    receiveContents[0].gameObject.SetActive(true);
                    receiveContents[1].gameObject.SetActive(true);
                    receiveContents[2].gameObject.SetActive(true);
                    receiveContents[3].gameObject.SetActive(true);

                    receiveContents[0].Initialize(RewardType.Gold, 1000000);
                    receiveContents[1].Initialize(RewardType.Crystal, 100);
                    receiveContents[2].Initialize(RewardType.DefDestroyTicket, 5);
                    receiveContents[3].Initialize(RewardType.Portion5, 10);

                    if(playerDataBase.Character21 != 0)
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
            case "WHILILI":
                if (playerDataBase.Coupon6 == 0)
                {
                    playerDataBase.Coupon6 = 1;
                    PlayfabManager.instance.UpdatePlayerStatisticsInsert("Coupon6", 1);

                    PlayfabManager.instance.UpdateAddGold(1000000);
                    PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, 50);

                    couponRewardView.SetActive(true);
                    receiveContents[0].gameObject.SetActive(true);
                    receiveContents[1].gameObject.SetActive(true);

                    receiveContents[0].Initialize(RewardType.Gold, 1000000);
                    receiveContents[1].Initialize(RewardType.Crystal, 50);

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
