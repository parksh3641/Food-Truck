using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveContent : MonoBehaviour
{
    RewardType rewardType = RewardType.Gold;

    public Image mainBackground;
    public Image icon;
    public Text countText;

    ImageDataBase imageDataBase;

    Sprite[] rewardArray;
    Sprite[] rankBackgroundArray;

    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        rewardArray = imageDataBase.GetRewardArray();

        rankBackgroundArray = imageDataBase.GetRankBackgroundArray();
    }

    public void Initialize(RewardType type, int count)
    {
        icon.sprite = rewardArray[(int)type];

        rewardType = type;

        countText.text = MoneyUnitString.ToCurrencyString(count);

        if (count == -1)
        {
            countText.text = "";
        }

        mainBackground.sprite = rankBackgroundArray[0];

        switch (type)
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
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Gold3:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.RankPoint:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.RepairTicket:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.RemoveAds:
                mainBackground.sprite = rankBackgroundArray[3];
                countText.text = LocalizationManager.instance.GetString("RemoveAds");
                countText.alignment = TextAnchor.MiddleCenter;
                break;
            case RewardType.GoldX2:
                mainBackground.sprite = rankBackgroundArray[3];
                countText.text = LocalizationManager.instance.GetString("GoldX2");
                countText.alignment = TextAnchor.MiddleCenter;
                break;
            case RewardType.AutoUpgrade:
                mainBackground.sprite = rankBackgroundArray[3];
                countText.text = LocalizationManager.instance.GetString("AutoUpgrade");
                countText.alignment = TextAnchor.MiddleCenter;
                break;
            case RewardType.AutoPresent:
                mainBackground.sprite = rankBackgroundArray[3];
                countText.text = LocalizationManager.instance.GetString("AutoPresent");
                countText.alignment = TextAnchor.MiddleCenter;
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
                mainBackground.sprite = rankBackgroundArray[1];
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
                break;
            case RewardType.Icon_Ranking2:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.Icon_Ranking3:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.Icon_Ranking4:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.SliverBox:
                mainBackground.sprite = rankBackgroundArray[1];
                break;
            case RewardType.GoldBox:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.EventTicket:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
        }
    }

    public void OpenInfo()
    {
        ReceiveInfoManager.instance.OpenReceiveInfo(rewardType, mainBackground.sprite);
    }
}
