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
    }
}
