using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureManager : MonoBehaviour
{
    public static TreasureManager instance;

    public GameObject treasureView;
    public GameObject treasureInfoView;

    public LocalizationContent[] treasureInfoText;

    public GameObject mainAlarm;
    public GameObject alarm;
    public GameObject ingameAlarm;

    public RectTransform treasureRectTransform;

    public Text treasureText;
    public Text treasure1Text;
    public Text treasure2Text;

    public GameObject treasureAdLockedObj;

    public GameObject treasureRewardView;

    public GameObject treasureButton;
    public GameObject treasureOneMoreButton;

    public TreasureContent[] treasureContents;

    public ReceiveContent[] receiveContents;

    private Queue indexQueue = new Queue();
    private int[] indexArray = new int[11];

    private int index = 0;
    private int price = 30;
    private bool oneMore = false;

    private float legendaryPercent = 2.0f;
    private float epicPercent = 4.0f;
    private float rarePercent = 4.0f;
    private float normalPercent = 4.0f;

    private int number = 0;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
    WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.5f);

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        instance = this;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        treasureView.SetActive(false);
        treasureInfoView.SetActive(false);

        treasureAdLockedObj.SetActive(false);
        treasureRewardView.SetActive(false);
        treasureButton.SetActive(false);

        treasure1Text.text = price.ToString();
        treasure2Text.text = (price * 10).ToString();

        mainAlarm.SetActive(false);
        alarm.SetActive(false);
        ingameAlarm.SetActive(false);

        treasureRectTransform.anchoredPosition = new Vector2(0, -9999);
    }

    public void Initialize()
    {
        if(playerDataBase.DailyTreasureReward == 0)
        {
            mainAlarm.SetActive(true);
            alarm.SetActive(true);
            ingameAlarm.SetActive(true);
        }
    }

    public void OpenTreasureView()
    {
        if (!treasureView.activeInHierarchy)
        {
            treasureAdLockedObj.SetActive(true);

            if (playerDataBase.AttendanceDay == System.DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            GameManager.instance.RenewalVC();

            if (playerDataBase.DailyTreasureReward == 0)
            {
                treasureAdLockedObj.SetActive(false);
            }

            treasureView.SetActive(true);
            treasureRewardView.SetActive(false);
            treasureButton.SetActive(false);

            CheckInitialize();

            FirebaseAnalytics.LogEvent("Open_Treasure");
        }
        else
        {
            treasureView.SetActive(false);
        }
    }

    public void OpenTreasureInfoView()
    {
        if (!treasureInfoView.activeInHierarchy)
        {
            treasureInfoView.SetActive(true);

            treasureInfoText[0].localizationName = "LegendaryPercent";
            treasureInfoText[0].plusText = " - " + ((legendaryPercent / System.Enum.GetValues(typeof(TreasureType)).Length) * 100).ToString("N2") + "%";

            treasureInfoText[1].localizationName = "EpicPercent";
            treasureInfoText[1].plusText = " - " + ((epicPercent / System.Enum.GetValues(typeof(TreasureType)).Length) * 100).ToString("N2") + "%";

            treasureInfoText[2].localizationName = "RarePercent";
            treasureInfoText[2].plusText = " - " + ((rarePercent / System.Enum.GetValues(typeof(TreasureType)).Length) * 100).ToString("N2") + "%";

            treasureInfoText[3].localizationName = "NormalPercent";
            treasureInfoText[3].plusText = " - " + ((normalPercent / System.Enum.GetValues(typeof(TreasureType)).Length) * 100).ToString("N2") + "%";

            treasureInfoText[0].ReLoad();
            treasureInfoText[1].ReLoad();
            treasureInfoText[2].ReLoad();
            treasureInfoText[3].ReLoad();

            FirebaseAnalytics.LogEvent("Open_TreasureInfo");
        }
        else
        {
            treasureInfoView.SetActive(false);
        }
    }

    void CheckInitialize()
    {
        for(int i = 0; i < treasureContents.Length; i ++)
        {
            treasureContents[i].Initialize(TreasureType.Treasure1 + i, this);
        }
    }

    public void WatchAd()
    {
        if (playerDataBase.DailyTreasureReward == 1) return;

        GoogleAdsManager.instance.admobReward_Treasure.ShowAd(7);
    }

    public void SuccessWatchAd()
    {
        playerDataBase.DailyTreasureReward = 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyTreasureReward", playerDataBase.DailyTreasureReward);

        mainAlarm.SetActive(false);
        alarm.SetActive(false);
        ingameAlarm.SetActive(false);

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

        if (indexQueue.Count > 0) return;

        if (playerDataBase.Crystal < price)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowCrystal);
            return;
        }

        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price);

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        oneMore = false;

        OpenTreasure(1);

        FirebaseAnalytics.LogEvent("Open_Treasure1");
    }

    public void OpenTreasure2()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (indexQueue.Count > 0) return;

        if (playerDataBase.Crystal < price * 10)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowCrystal);
            return;
        }

        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price * 10);

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        oneMore = true;

        OpenTreasure(11);

        FirebaseAnalytics.LogEvent("Open_Treasure11");
    }

    public void OpenTreasure2_OneMore()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (indexQueue.Count > 0) return;

        if (playerDataBase.Crystal < price * 9)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowCrystal);
            return;
        }

        PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, price * 9);

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        oneMore = true;

        OpenTreasure(11);

        FirebaseAnalytics.LogEvent("Open_Treasure11");
    }

    public void OpenTreasure(int number)
    {
        StartCoroutine(OpenTreasureCoroution(number));
    }

    IEnumerator OpenTreasureCoroution(int count)
    {
        treasureRewardView.SetActive(true);
        treasureButton.SetActive(false);

        treasureText.text = MoneyUnitString.ToCurrencyString(playerDataBase.Crystal);

        playerDataBase.TreasureCount += count;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("TreasureCount", playerDataBase.TreasureCount);

        for (int i = 0; i < receiveContents.Length; i++)
        {
            receiveContents[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < count; i++)
        {
            index = Random.Range(0, System.Enum.GetValues(typeof(TreasureType)).Length);

            indexArray[i] = index;
            indexQueue.Enqueue(index);
        }

        StartCoroutine(SaveCoroution());

        yield return waitForSeconds2;

        for (int i = 0; i < count; i++)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Click);

            receiveContents[i].gameObject.SetActive(true);

            switch (indexArray[i])
            {
                case 0:
                    receiveContents[i].Initialize(RewardType.Treasure1, -1);
                    break;
                case 1:
                    receiveContents[i].Initialize(RewardType.Treasure2, -1);
                    break;
                case 2:
                    receiveContents[i].Initialize(RewardType.Treasure3, -1);
                    break;
                case 3:
                    receiveContents[i].Initialize(RewardType.Treasure4, -1);
                    break;
                case 4:
                    receiveContents[i].Initialize(RewardType.Treasure5, -1);
                    break;
                case 5:
                    receiveContents[i].Initialize(RewardType.Treasure6, -1);
                    break;
                case 6:
                    receiveContents[i].Initialize(RewardType.Treasure7, -1);
                    break;
                case 7:
                    receiveContents[i].Initialize(RewardType.Treasure8, -1);
                    break;
                case 8:
                    receiveContents[i].Initialize(RewardType.Treasure9, -1);
                    break;
                case 9:
                    receiveContents[i].Initialize(RewardType.Treasure10, -1);
                    break;
                case 10:
                    receiveContents[i].Initialize(RewardType.Treasure11, -1);
                    break;
                case 11:
                    receiveContents[i].Initialize(RewardType.Treasure12, -1);
                    break;
                case 12:
                    receiveContents[i].Initialize(RewardType.Treasure13, -1);
                    break;
                case 13:
                    receiveContents[i].Initialize(RewardType.Treasure14, -1);
                    break;
            }

            yield return waitForSeconds;
        }

        if(treasureView.activeInHierarchy)
        {
            Initialize();
        }

        yield return waitForSeconds2;

        CheckInitialize();

        treasureButton.SetActive(true);
        treasureOneMoreButton.SetActive(oneMore);
    }

    IEnumerator SaveCoroution()
    {
        number = 0;

        while (true)
        {
            if(number < 3)
            {
                if(indexQueue.Count > 0)
                {
                    GetTreasure((int)indexQueue.Dequeue());
                    number++;
                    continue;
                }
                else
                {
                    break;
                }
            }
            else
            {
                number = 0;
                yield return waitForSeconds2;
                continue;
            }
        }
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
            case 6:
                playerDataBase.Treasure7Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure7Count", playerDataBase.Treasure7Count);
                break;
            case 7:
                playerDataBase.Treasure8Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure8Count", playerDataBase.Treasure8Count);
                break;
            case 8:
                playerDataBase.Treasure9Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure9Count", playerDataBase.Treasure9Count);
                break;
            case 9:
                playerDataBase.Treasure10Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure10Count", playerDataBase.Treasure10Count);
                break;
            case 10:
                playerDataBase.Treasure11Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure11Count", playerDataBase.Treasure11Count);
                break;
            case 11:
                playerDataBase.Treasure12Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure12Count", playerDataBase.Treasure12Count);
                break;
            case 12:
                playerDataBase.Treasure13Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure13Count", playerDataBase.Treasure13Count);
                break;
            case 13:
                playerDataBase.Treasure14Count += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Treasure14Count", playerDataBase.Treasure14Count);
                break;
        }
    }

    public void CloseTreasure()
    {
        oneMore = false;

        treasureRewardView.SetActive(false);
    }
}
