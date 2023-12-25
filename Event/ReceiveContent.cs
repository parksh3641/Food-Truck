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

        //rankBackgroundArray = imageDataBase.GetRankBackgroundArray();
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
    }
}
