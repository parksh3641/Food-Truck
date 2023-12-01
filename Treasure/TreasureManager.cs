using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    public GameObject treasureView;

    public GameObject treasureAdLockedObj;

    public GameObject treasureRewardView;

    public GameObject treasureButton;

    public TreasureContent[] treasureContents;

    public ReceiveContent[] receiveContents;

    private int index = 0;
    private int price = 20;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
    WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.5f);

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        treasureView.SetActive(false);
        treasureAdLockedObj.SetActive(false);
        treasureRewardView.SetActive(false);
        treasureButton.SetActive(false);
    }


    public void OpenTreasureView()
    {
        if (!treasureView.activeInHierarchy)
        {
            if (playerDataBase.AttendanceDay == System.DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            if(GameStateManager.instance.DailyTreasureReward)
            {
                treasureAdLockedObj.SetActive(true);
            }

            treasureView.SetActive(true);
            treasureRewardView.SetActive(false);
            treasureButton.SetActive(false);

            Initialize();

            FirebaseAnalytics.LogEvent("OpenTreasure");
        }
        else
        {
            treasureView.SetActive(false);
        }
    }

    void Initialize()
    {
        for(int i = 0; i < treasureContents.Length; i ++)
        {
            treasureContents[i].Initialize(TreasureType.Treasure1 + i, this);
        }
    }

    public void WatchAd()
    {
        if (GameStateManager.instance.DailyTreasureReward) return;

        GoogleAdsManager.instance.admobReward_Treasure.ShowAd(7);
    }

    public void SuccessWatchAd()
    {
        GameStateManager.instance.DailyTreasureReward = true;

        treasureAdLockedObj.SetActive(true);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessWatchAd);

        OpenTreasure(1);
    }

    public void OpenTreasure1()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (playerDataBase.Crystal < price)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowCrystal);
            return;
        }

        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price);

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        OpenTreasure(1);
    }

    public void OpenTreasure2()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (playerDataBase.Crystal < price * 10)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowCrystal);
            return;
        }

        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price * 10);

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        OpenTreasure(11);
    }

    public void OpenTreasure(int number)
    {
        StartCoroutine(OpenTreasureCoroution(number));
    }

    IEnumerator OpenTreasureCoroution(int count)
    {
        treasureRewardView.SetActive(true);
        treasureButton.SetActive(false);

        playerDataBase.TreasureCount += count;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TreasureCount", playerDataBase.TreasureCount);

        for (int i = 0; i < receiveContents.Length; i++)
        {
            receiveContents[i].gameObject.SetActive(false);
        }

        yield return waitForSeconds2;

        for (int i = 0; i < count; i++)
        {
            index = Random.Range(0, System.Enum.GetValues(typeof(TreasureType)).Length);

            SoundManager.instance.PlaySFX(GameSfxType.Click);

            GetTreasure(index);

            receiveContents[i].gameObject.SetActive(true);

            switch (index)
            {
                case 0:
                    receiveContents[i].Initialize(RewardType.Treasure1, 1);
                    break;
                case 1:
                    receiveContents[i].Initialize(RewardType.Treasure2, 1);
                    break;
                case 2:
                    receiveContents[i].Initialize(RewardType.Treasure3, 1);
                    break;
                case 3:
                    receiveContents[i].Initialize(RewardType.Treasure4, 1);
                    break;
                case 4:
                    receiveContents[i].Initialize(RewardType.Treasure5, 1);
                    break;
                case 5:
                    receiveContents[i].Initialize(RewardType.Treasure6, 1);
                    break;
            }

            yield return waitForSeconds;
        }

        Initialize();

        yield return waitForSeconds2;

        treasureButton.SetActive(true);
    }

    void GetTreasure(int number)
    {
        switch(number)
        {
            case 0:
                playerDataBase.Treasure1Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure1Count", playerDataBase.Treasure1Count);
                break;
            case 1:
                playerDataBase.Treasure2Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure2Count", playerDataBase.Treasure2Count);
                break;
            case 2:
                playerDataBase.Treasure3Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure3Count", playerDataBase.Treasure3Count);
                break;
            case 3:
                playerDataBase.Treasure4Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure4Count", playerDataBase.Treasure4Count);
                break;
            case 4:
                playerDataBase.Treasure5Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure5Count", playerDataBase.Treasure5Count);
                break;
            case 5:
                playerDataBase.Treasure6Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure6Count", playerDataBase.Treasure6Count);
                break;
        }
    }

    public void CloseTreasure()
    {
        treasureRewardView.SetActive(false);
    }
}
