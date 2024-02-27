using Firebase.Analytics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class OfflineManager : MonoBehaviour
{
    public GameObject offlineView;

    public GameObject alarm;

    [Title("Lock")]
    public ButtonScaleAnimation button;
    public GameObject quickLockObj;
    public GameObject lockObj;
    public Text lockTimerText;


    [Title("Text")]
    public Text castleLevelText;
    public Text levelUpCostText;
    public Text timerText;
    public Text coinText;
    public Text expText;

    public Text adCoinText;
    public Text adExpText;

    private int timer = 0;

    private int addCoin = 0;
    private int addExp = 0;
    private int addCrystal = 0;

    private int saveCoin = 0;
    private int saveExp = 0;

    private bool isDelay = false;
    private bool first = false;

    string localization_Time;
    string localization_Hours = "";
    string localization_Minutes = "";
    string localization_Seconds = "";

    public ReceiveContent[] receiveContents;

    public MoneyAnimation moneyAnimation;


    DateTime time; //적립을 한 시점
    DateTime serverTime; //최대 적립 날짜
    TimeSpan timeSpan;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    public LevelManager levelManager;
    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        offlineView.SetActive(false);
        alarm.SetActive(false);
    }

    public void RewardStart()
    {
        StopAllCoroutines();
        StartCoroutine(TimerCoroution());
    }

    public void OpenCastleView()
    {
        if (!offlineView.activeInHierarchy)
        {
            if (playerDataBase.AttendanceDay == System.DateTime.Today.ToString("yyyyMMdd"))
            {
                ResetManager.instance.Initialize();
            }

            offlineView.SetActive(true);

            timerText.text = "";

            localization_Time = LocalizationManager.instance.GetString("RewardTime");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");
            localization_Seconds = LocalizationManager.instance.GetString("Seconds");

            if (playerDataBase.DailyCastleReward == 0)
            {
                quickLockObj.SetActive(false);
            }
            else
            {
                quickLockObj.SetActive(true);
                button.StopAnim();
            }

            CheckCastle();

            FirebaseAnalytics.LogEvent("OpenOffline");
        }
        else
        {
            offlineView.SetActive(false);

            first = false;
        }
    }

    public void Initialize()
    {
        if (playerDataBase.LockTutorial >= 6)
        {
            if (playerDataBase.CastleDate.Length > 1)
            {
                time = DateTime.ParseExact(DateTime.Now.ToString("yyyy") + playerDataBase.CastleDate.Substring(1, playerDataBase.CastleDate.Length - 1), "yyyyMMddHHmm", CultureInfo.CurrentCulture);
                serverTime = DateTime.ParseExact(DateTime.Now.ToString("yyyy") + playerDataBase.CastleServerDate.Substring(1, playerDataBase.CastleServerDate.Length - 1), "yyyyMMddHHmm", CultureInfo.CurrentCulture);

            }
            else
            {
                playerDataBase.CastleDate = DateTime.Now.ToString("MMddHHmm");

                time = DateTime.Now;

                if(playerDataBase.SuperOffline)
                {
                    playerDataBase.CastleServerDate = DateTime.Now.AddDays(2).ToString("MMddHHmm");
                    serverTime = DateTime.Now.AddDays(2);
                }
                else
                {
                    playerDataBase.CastleServerDate = DateTime.Now.AddDays(1).ToString("MMddHHmm");
                    serverTime = DateTime.Now.AddDays(1);
                }

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("CastleDate", int.Parse("1" + playerDataBase.CastleDate));
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("CastleServerDate", int.Parse("1" + playerDataBase.CastleServerDate));
            }

            StartCoroutine(TimerCoroution());

            timeSpan = DateTime.Now - time;

            if (timeSpan.TotalMinutes >= 10)
            {
                OnSetAlarm();
            }

            OpenCastleView();

            first = true;
        }
    }

    public void CheckCastle()
    {
        if(playerDataBase.CastleLevel > playerDataBase.Level)
        {
            playerDataBase.CastleLevel = playerDataBase.Level;
        }

        castleLevelText.text = LocalizationManager.instance.GetString("CastleLevel") + " : " + playerDataBase.CastleLevel + " / " + playerDataBase.Level;

        addCrystal = (playerDataBase.CastleLevel + 1) * 5;
        addCoin = 100000 + playerDataBase.CastleLevel * 5000;
        addExp = 1000 + playerDataBase.CastleLevel * 50;

        addCoin += (int)(addCoin * (playerDataBase.Treasure5 * 0.003f));
        addExp += (int)(addExp * (playerDataBase.Treasure5 * 0.003f));

        if(playerDataBase.SuperOffline)
        {
            addCoin += (int)(addCoin * 0.1f);
            addExp += (int)(addExp * 0.1f);
        }

        if(addCrystal >= 50)
        {
            addCrystal = 50;
        }

        levelUpCostText.text = addCrystal.ToString();
        coinText.text = MoneyUnitString.ToCurrencyString(addCoin) + "\n/" + localization_Hours + "  (+5000)";
        expText.text = MoneyUnitString.ToCurrencyString(addExp) + "\n/" + localization_Hours + "  (+50)";

        adCoinText.text = MoneyUnitString.ToCurrencyString(addCoin * 12);
        adExpText.text = MoneyUnitString.ToCurrencyString(addExp * 12);

        receiveContents[0].Initialize(RewardType.Gold, saveCoin);
        receiveContents[1].Initialize(RewardType.Exp, saveExp);

        if (DateTime.Compare(DateTime.Now, serverTime) == 1)
        {
            if (playerDataBase.SuperOffline)
            {
                timerText.text = localization_Time + " : " + "48" + localization_Hours + " " + "00" + localization_Minutes;

                CheckReward(60 * 48);
            }
            else
            {
                timerText.text = localization_Time + " : " + "24" + localization_Hours + " " + "00" + localization_Minutes;

                CheckReward(60 * 24);
            }

            lockObj.SetActive(false);
        }
    }

    public void CheckReward(double minute)
    {
        saveCoin = (int)Mathf.Round((addCoin / (60 * (1.0f))) * (float)minute);
        saveExp = (int)Mathf.Round((addExp / (60 * (1.0f))) * (float)minute);

        if (receiveContents[0].gameObject.activeInHierarchy)
        {
            receiveContents[0].Initialize(RewardType.Gold, saveCoin);
            receiveContents[1].Initialize(RewardType.Exp, saveExp);
        }
    }


    public void LevelUpButton()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (isDelay) return;

        if (playerDataBase.CastleLevel < playerDataBase.Level)
        {
            if (playerDataBase.Crystal >= addCrystal)
            {
                PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Crystal, addCrystal);

                playerDataBase.CastleLevel += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("CastleLevel", playerDataBase.CastleLevel);

                CheckCastle();

                SoundManager.instance.PlaySFX(GameSfxType.Upgrade1);
                NotionManager.instance.UseNotion(NotionType.SuccessUpgrade);

                GourmetManager.instance.Initialize();
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.LowCrystal);
            }
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.MaxLevel);
        }

        isDelay = true;
        Invoke("Delay", 0.4f);
    }

    void Delay()
    {
        isDelay = false;
    }

    public void ReceiveAdButton()
    {
        if (playerDataBase.DailyCastleReward == 1) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        GoogleAdsManager.instance.admobReward_Delivery.ShowAd(5);
    }

    public void ReceiveButton()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if (!lockObj.activeInHierarchy)
        {
            playerDataBase.CastleDate = DateTime.Now.ToString("MMddHHmm");

            time = DateTime.Now;
            if (playerDataBase.SuperOffline)
            {
                playerDataBase.CastleServerDate = DateTime.Now.AddDays(2).ToString("MMddHHmm");
                serverTime = DateTime.Now.AddDays(2);
            }
            else
            {
                playerDataBase.CastleServerDate = DateTime.Now.AddDays(1).ToString("MMddHHmm");
                serverTime = DateTime.Now.AddDays(1);
            }

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("CastleDate", int.Parse("1" + playerDataBase.CastleDate));
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("CastleServerDate", int.Parse("1" + playerDataBase.CastleServerDate));

            PortionManager.instance.GetExp(saveExp);
            PlayfabManager.instance.UpdateAddGold(saveCoin);


            StopAllCoroutines();
            StartCoroutine(TimerCoroution());

            OnCheckAlarm();

            SoundManager.instance.PlaySFX(GameSfxType.QuestReward);
            NotionManager.instance.UseNotion(NotionType.SuccessReward);

            if(first)
            {
                first = false;
                OpenCastleView();
            }
        }
    }

    IEnumerator TimerCoroution()
    {
        if (DateTime.Compare(DateTime.Now, serverTime) == -1)
        {
            timeSpan = DateTime.Now - time;

            if (timeSpan.TotalMinutes > 0)
            {
                CheckReward(timeSpan.TotalMinutes);
            }

            if (timeSpan.TotalSeconds < 0)
            {
                Debug.LogError("적립 시간이 마이너스입니다");

                playerDataBase.CastleDate = DateTime.Now.ToString("MMddHHmm");

                time = DateTime.Now;
                if (playerDataBase.SuperOffline)
                {
                    playerDataBase.CastleServerDate = DateTime.Now.AddDays(2).ToString("MMddHHmm");
                    serverTime = DateTime.Now.AddDays(2);
                }
                else
                {
                    playerDataBase.CastleServerDate = DateTime.Now.AddDays(1).ToString("MMddHHmm");
                    serverTime = DateTime.Now.AddDays(1);
                }

                PlayfabManager.instance.UpdatePlayerStatisticsInsert("CastleDate", int.Parse("1" + playerDataBase.CastleDate));
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("CastleServerDate", int.Parse("1" + playerDataBase.CastleServerDate));
            }
        }
        else
        {
            yield break;
        }

        if (timeSpan.Hours > 0)
        {
            timerText.text = localization_Time + " : " + timeSpan.Hours.ToString("D2") + localization_Hours + " " + timeSpan.Minutes.ToString("D2") + localization_Minutes;
        }
        else
        {
            if (timeSpan.Minutes == 0)
            {
                timerText.text = localization_Time + " : " + timeSpan.Seconds.ToString("D2") + localization_Seconds;
            }
            else
            {
                timerText.text = localization_Time + " : " + timeSpan.Minutes.ToString("D2") + localization_Minutes + " " + timeSpan.Seconds.ToString("D2") + localization_Seconds;
            }
        }

        if (timeSpan.Minutes >= 10 || timeSpan.Hours >= 1)
        {
            lockObj.SetActive(false);
        }
        else
        {
            lockObj.SetActive(true);

            if (timeSpan.Seconds == 0)
            {
                lockTimerText.text = (10 - timeSpan.Minutes).ToString("D2") + ":00";
            }
            else if (timeSpan.Seconds == 60)
            {
                lockTimerText.text = (9 - timeSpan.Minutes).ToString("D2") + ":00";
            }
            else
            {
                lockTimerText.text = (9 - timeSpan.Minutes).ToString("D2") + ":" + (60 - timeSpan.Seconds).ToString("D2");
            }
        }

        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
    }

    public void SuccessWatchAd()
    {
        playerDataBase.DailyCastleReward = 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DailyCastleReward", playerDataBase.DailyCastleReward);

        quickLockObj.SetActive(true);

        button.StopAnim();

        int quickCoin = addCoin * 12;
        int quickExp = addExp * 12;

        PortionManager.instance.GetExp(quickExp);
        PlayfabManager.instance.UpdateAddGold(quickCoin);

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessWatchAd);
    }

    public void OnSetAlarm()
    {
        alarm.SetActive(true);
    }

    public void OnCheckAlarm()
    {
        alarm.SetActive(false);
    }
}

