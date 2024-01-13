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

    void Initialize(RewardType rewardType)
    {
        titleText.text = LocalizationManager.instance.GetString(rewardType.ToString());

        icon.sprite = rewardArray[(int)rewardType];

        switch (rewardType)
        {
            case RewardType.Gold:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.DefDestroyTicket:
                mainBackground.sprite = rankBackgroundArray[3];
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
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.Gold3:
                mainBackground.sprite = rankBackgroundArray[0];
                break;
            case RewardType.RankPoint:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.RepairTicket:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.RemoveAds:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.GoldX2:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.AutoUpgrade:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.AutoPresent:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
        }

        infoText.text = LocalizationManager.instance.GetString(rewardType.ToString() + "_Info");
    }
}
