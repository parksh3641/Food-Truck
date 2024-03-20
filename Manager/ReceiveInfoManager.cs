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
    Sprite[] rankBackgroundArray;

    private void Awake()
    {
        instance = this;

        receiveInfoView.SetActive(false);

        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        rewardArray = imageDataBase.GetRewardArray();
        rankBackgroundArray = imageDataBase.GetRankBackgroundArray();
    }


    public void CloseReceiveInfo()
    {
        receiveInfoView.SetActive(false);
    }


    public void OpenReceiveInfo(RewardType rewardType, Sprite image)
    {
        if (!receiveInfoView.activeInHierarchy)
        {
            receiveInfoView.SetActive(true);

            Initialize(rewardType, image);
        }
        else
        {
            receiveInfoView.SetActive(false);
        }
    }

    public void OpenReceiveInfo(RewardType rewardType, int number)
    {
        if (!receiveInfoView.activeInHierarchy)
        {
            receiveInfoView.SetActive(true);

            Initialize(rewardType, rankBackgroundArray[number]);
        }
        else
        {
            receiveInfoView.SetActive(false);
        }
    }

    void Initialize(RewardType rewardType, Sprite image)
    {
        titleText.text = LocalizationManager.instance.GetString(rewardType.ToString());

        icon.sprite = rewardArray[(int)rewardType];

        mainBackground.sprite = image;

        infoText.text = LocalizationManager.instance.GetString(rewardType.ToString() + "_Info");

        effect.SetActive(false);

        switch (rewardType)
        {
            case RewardType.Gold:
                break;
            case RewardType.DefDestroyTicket:
                effect.SetActive(true);
                break;
            case RewardType.Portion1:
                break;
            case RewardType.Portion2:
                break;
            case RewardType.Portion3:
                break;
            case RewardType.Portion4:
                break;
            case RewardType.PortionSet:
                break;
            case RewardType.Crystal:
                effect.SetActive(true);
                break;
            case RewardType.Exp:
                break;
            case RewardType.Treasure1:
                effect.SetActive(true);
                break;
            case RewardType.Treasure2:
                effect.SetActive(true);
                break;
            case RewardType.Treasure3:
                effect.SetActive(true);
                break;
            case RewardType.Treasure4:
                break;
            case RewardType.Treasure5:
                break;
            case RewardType.Treasure6:
                break;
            case RewardType.Portion5:
                break;
            case RewardType.Treasure7:
                effect.SetActive(true);
                break;
            case RewardType.Treasure8:
                break;
            case RewardType.Treasure9:
                break;
            case RewardType.TreasureBox:
                effect.SetActive(true);
                break;
            case RewardType.DefDestroyTicketPiece:
                break;
            case RewardType.BuffTicket:
                break;
            case RewardType.Portion6:
                break;
            case RewardType.SkillTicket:
                break;
            case RewardType.Treasure10:
                break;
            case RewardType.Treasure11:
                break;
            case RewardType.Treasure12:
                break;
            case RewardType.Gold2:
                break;
            case RewardType.Gold3:
                break;
            case RewardType.RankPoint:
                effect.SetActive(true);
                break;
            case RewardType.RepairTicket:
                break;
            case RewardType.RemoveAds:
                effect.SetActive(true);
                break;
            case RewardType.GoldX2:
                effect.SetActive(true);
                break;
            case RewardType.AutoUpgrade:
                effect.SetActive(true);
                break;
            case RewardType.AutoPresent:
                effect.SetActive(true);
                break;
            case RewardType.Island1_Heart:
                break;
            case RewardType.Island2_Heart:
                break;
            case RewardType.Island3_Heart:
                break;
            case RewardType.Island4_Heart:
                break;
            case RewardType.SpeicalCharacter:
                break;
            case RewardType.AbilityPoint:
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
                effect.SetActive(true);
                break;
            case RewardType.Icon_Ranking2:
                effect.SetActive(true);
                break;
            case RewardType.Icon_Ranking3:
                effect.SetActive(true);
                break;
            case RewardType.Icon_Ranking4:
                effect.SetActive(true);
                break;
            case RewardType.SliverBox:
                break;
            case RewardType.GoldBox:
                break;
            case RewardType.EventTicket:
                effect.SetActive(true);
                break;
            case RewardType.ChallengePoint:
                effect.SetActive(true);
                break;
            case RewardType.Icon_Attendance:
                effect.SetActive(true);
                break;
        }
    }
}
