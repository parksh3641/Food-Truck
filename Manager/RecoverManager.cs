using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoverManager : MonoBehaviour
{
    public GameObject recoverView;

    public Image beforeIcon;
    public Image afterIcon;
    public Text afterLevelText;

    public Text countText;

    public Text needText;
    public Text crystalText;

    private int need = 0;

    private int maxLevel = 0;

    RankFoodType rankFoodType;

    Sprite[] rankFoodIconArray;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;

    private void Awake()
    {
        recoverView.SetActive(false);

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        rankFoodIconArray = imageDataBase.GetRankFoodIconArray();
    }

    public void OpenRecoverView()
    {
        if(!recoverView.activeInHierarchy)
        {
            recoverView.SetActive(true);

            countText.text = playerDataBase.RecoverTicket.ToString();

            FirebaseAnalytics.LogEvent("Open_Recover");
        }
        else
        {
            recoverView.SetActive(false);
        }
    }

    public void Recover(int number)
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (number == 0)
        {
            if (playerDataBase.RecoverTicket < need)
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                return;
            }

            playerDataBase.RecoverTicket -= need;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("RecoverTicket", playerDataBase.RecoverTicket);
        }
        else
        {
            if (playerDataBase.Crystal < need * 5)
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.LowItemNotion);
                return;
            }

            PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, need * 5);
        }

        SuccessRecover();
    }

    void SuccessRecover()
    {
        OpenRecoverView();

        GameManager.instance.RecoverFood(rankFoodType);

        playerDataBase.RecoverCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RecoverCount", playerDataBase.RecoverCount);

        FirebaseAnalytics.LogEvent("Clear_Recover");

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.RecoverNotion);
    }

    public void FoodInitialize(RankFoodType type, int level)
    {
        OpenRecoverView();

        rankFoodType = type;

        maxLevel = level;

        Initialize();
    }
    void Initialize()
    {
        beforeIcon.sprite = rankFoodIconArray[(int)rankFoodType];
        afterIcon.sprite = rankFoodIconArray[(int)rankFoodType];

        need = 5;
        need += ((maxLevel - 50) / 5);

        afterLevelText.text = "Lv. " + ((int)(maxLevel * 0.5f)).ToString();
        needText.text = need.ToString();

        crystalText.text = (need * 5).ToString();
    }

    public void OpenRepairTicketInfo()
    {
        ReceiveInfoManager.instance.OpenReceiveInfo(RewardType.RepairTicket);
    }
}
