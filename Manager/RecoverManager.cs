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

    private int index = 0;
    private int need = 0;

    private int maxLevel = 0;

    FoodType foodType;
    CandyType candyType;
    JapaneseFoodType japaneseFoodType;
    DessertType dessertType;

    Sprite[] foodChangeArray;
    Sprite[] candyArray;
    Sprite[] japaneseArray;
    Sprite[] dessertArray;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;

    private void Awake()
    {
        recoverView.SetActive(false);

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        foodChangeArray = imageDataBase.GetFoodChangeArray();
        candyArray = imageDataBase.GetCandyArray();
        japaneseArray = imageDataBase.GetJapaneseFoodArray();
        dessertArray = imageDataBase.GetDessertArray();
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

        switch (index)
        {
            case 0:
                GameManager.instance.RecoverFood(foodType);

                break;
            case 1:
                GameManager.instance.RecoverCandy(candyType);

                break;
            case 2:
                GameManager.instance.RecoverJapanese(japaneseFoodType);

                break;
            case 3:
                GameManager.instance.RecoverDessert(dessertType);

                break;
        }

        playerDataBase.RecoverCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RecoverCount", playerDataBase.RecoverCount);

        FirebaseAnalytics.LogEvent("Clear_Recover");

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.RecoverNotion);
    }

    public void FoodInitialize(FoodType type, int level)
    {
        OpenRecoverView();

        index = 0;

        foodType = type;

        maxLevel = level;

        Initialize();
    }

    public void CandyInitialize(CandyType type, int level)
    {
        OpenRecoverView();

        index = 1;

        candyType = type;

        maxLevel = level;

        Initialize();
    }

    public void JapaneseFoodInitialize(JapaneseFoodType type, int level)
    {
        OpenRecoverView();

        index = 2;

        japaneseFoodType = type;

        maxLevel = level;

        Initialize();
    }

    public void DessertInitialize(DessertType type, int level)
    {
        OpenRecoverView();

        index = 3;

        dessertType = type;

        maxLevel = level;

        Initialize();
    }

    void Initialize()
    {
        switch(index)
        {
            case 0:
                beforeIcon.sprite = foodChangeArray[(int)foodType];
                afterIcon.sprite = foodChangeArray[(int)foodType];
                break;
            case 1:
                beforeIcon.sprite = candyArray[(int)candyType];
                afterIcon.sprite = candyArray[(int)candyType];
                break;
            case 2:
                beforeIcon.sprite = japaneseArray[(int)japaneseFoodType];
                afterIcon.sprite = japaneseArray[(int)japaneseFoodType];
                break;
            case 3:
                beforeIcon.sprite = dessertArray[(int)dessertType];
                afterIcon.sprite = dessertArray[(int)dessertType];
                break;
        }

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
