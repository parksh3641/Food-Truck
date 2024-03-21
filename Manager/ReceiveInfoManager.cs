using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveInfoManager : MonoBehaviour
{
    public static ReceiveInfoManager instance;

    public GameObject receiveInfoView;

    public Image mainBackground;
    public Image icon;
    public GameObject effect;

    [Space]
    public Text titleText;
    public Text infoText;

    ImageDataBase imageDataBase;

    Sprite[] rewardArray;
    Sprite[] itemArray;
    Sprite[] rankBackgroundArray;

    private void Awake()
    {
        instance = this;

        receiveInfoView.SetActive(false);

        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        rewardArray = imageDataBase.GetRewardArray();
        itemArray = imageDataBase.GetItemArray();
        rankBackgroundArray = imageDataBase.GetRankBackgroundArray();
    }


    public void CloseReceiveInfo()
    {
        receiveInfoView.SetActive(false);
    }


    public void OpenReceiveInfo(RewardType rewardType)
    {
        if (!receiveInfoView.activeInHierarchy)
        {
            receiveInfoView.SetActive(true);

            Initialize(rewardType);
        }
        else
        {
            receiveInfoView.SetActive(false);
        }
    }

    public void OpenReceiveInfo(ItemType itemType)
    {
        if (!receiveInfoView.activeInHierarchy)
        {
            receiveInfoView.SetActive(true);

            Initialize(itemType);
        }
        else
        {
            receiveInfoView.SetActive(false);
        }
    }

    void Initialize(RewardType rewardType)
    {
        titleText.text = LocalizationManager.instance.GetString(rewardType.ToString());

        icon.sprite = rewardArray[(int)rewardType];

        infoText.text = LocalizationManager.instance.GetString(rewardType.ToString() + "_Info");

        effect.SetActive(false);

        switch (rewardType)
        {
            case RewardType.Gold:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.DefDestroyTicket:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.Portion1:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.Portion2:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.Portion3:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.Portion4:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.PortionSet:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.Crystal:
                mainBackground.sprite = rankBackgroundArray[1];
                effect.SetActive(true);
                break;
            case RewardType.Exp:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.Treasure1:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Treasure2:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Treasure3:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Treasure4:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.Treasure5:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.Treasure6:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.Portion5:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Treasure7:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Treasure8:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.Treasure9:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.TreasureBox:
                mainBackground.sprite = rankBackgroundArray[2];
                effect.SetActive(true);
                break;
            case RewardType.DefDestroyTicketPiece:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.BuffTicket:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Portion6:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.SkillTicket:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Treasure10:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.Treasure11:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.Treasure12:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.Gold2:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Gold3:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.RankPoint:
                mainBackground.sprite = rankBackgroundArray[2];
                effect.SetActive(true);
                break;
            case RewardType.RepairTicket:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.RemoveAds:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.GoldX2:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.AutoUpgrade:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.AutoPresent:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.Island1_Heart:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.Island2_Heart:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.Island3_Heart:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.Island4_Heart:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.SpeicalCharacter:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.AbilityPoint:
                mainBackground.sprite = rankBackgroundArray[2];
                effect.SetActive(true);
                break;
            case RewardType.DungeonKey1:
                break;
            case RewardType.DungeonKey2:
                break;
            case RewardType.DungeonKey3:
                break;
            case RewardType.DungeonKey4:
                break;
            case RewardType.Icon_Ranking1:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.Icon_Ranking2:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.Icon_Ranking3:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.Icon_Ranking4:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.SliverBox:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.GoldBox:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.EventTicket:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.ChallengePoint:
                mainBackground.sprite = rankBackgroundArray[2];
                effect.SetActive(true);
                break;
            case RewardType.Icon_Attendance:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.Treasure13:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case RewardType.Treasure14:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
        }
    }

    void Initialize(ItemType itemType)
    {
        titleText.text = LocalizationManager.instance.GetString(itemType.ToString());

        icon.sprite = itemArray[(int)itemType];

        infoText.text = LocalizationManager.instance.GetString(itemType.ToString() + "_Info");

        if(infoText.text.Equals(itemType.ToString() + "_Info"))
        {
            infoText.text = LocalizationManager.instance.GetString(itemType.ToString());
        }

        effect.SetActive(false);

        switch (itemType)
        {
            case ItemType.DailyReward:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case ItemType.AdReward_Gold:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.DefDestroyTicket:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.GoldShop1:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case ItemType.GoldShop2:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.GoldShop3:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case ItemType.AdReward_Portion:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.RemoveAds:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.PortionSet1:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.PortionSet2:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.PortionSet3:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.DailyReward_Portion:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.GoldX2:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop1:
                mainBackground.sprite = rankBackgroundArray[1];
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop2:
                mainBackground.sprite = rankBackgroundArray[1];
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop3:
                mainBackground.sprite = rankBackgroundArray[1];
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop4:
                mainBackground.sprite = rankBackgroundArray[1];
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop5:
                mainBackground.sprite = rankBackgroundArray[1];
                effect.SetActive(true);
                break;
            case ItemType.CrystalShop6:
                mainBackground.sprite = rankBackgroundArray[1];
                effect.SetActive(true);
                break;
            case ItemType.Portion1:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.Portion2:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.Portion3:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.Portion4:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.Portion5:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.DefDestroyTicketSlices: //파괴 방어권 교환
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.DefDestroyTicketPiece:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case ItemType.BuffTicketSet1:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.BuffTicketSet2:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.BuffTicketSet3:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case ItemType.DefTicketSet1:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.DefTicketSet2:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.DefTicketSet3:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.SuperOffline:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.AdReward_Crystal:
                mainBackground.sprite = rankBackgroundArray[1];
                effect.SetActive(true);
                break;
            case ItemType.AutoUpgrade:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.AutoPresent:
                mainBackground.sprite = rankBackgroundArray[3];
                effect.SetActive(true);
                break;
            case ItemType.BuffTicket:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case ItemType.SkillTicket:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case ItemType.RepairTicket:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case ItemType.RepairTicket10:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case ItemType.AbilityPoint:
                mainBackground.sprite = rankBackgroundArray[0];
                effect.SetActive(true);
                break;
            case ItemType.DungeonKey1:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case ItemType.DungeonKey2:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case ItemType.DungeonKey3:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case ItemType.DungeonKey4:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
        }
    }
}
