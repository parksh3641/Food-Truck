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
            case RewardType.BuffTickets:
                mainBackground.sprite = rankBackgroundArray[2];
                break;
            case RewardType.Portion6:
                mainBackground.sprite = rankBackgroundArray[3];
                break;
            case RewardType.SkillTickets:
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
        }
    }
}
