using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonPassManager : MonoBehaviour
{
    public GameObject seasonPassView;

    public RectTransform seasonPassTrasnform;

    public GameObject seasonPassBuyButton;
    public GameObject seasonPassing;

    public GameObject alarm;

    public Text timerText;

    public Text scoreText;
    public Text levelText;
    public Image fillAmount;

    public SeasonContent[] seasonContents;

    private int score = 0;
    private int level = 0;
    private int goal = 0;

    string localization_Reset = "";
    string localization_Days = "";
    string localization_Hours = "";
    string localization_Minutes = "";

    DateTime f, g;
    TimeSpan h;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    Dictionary<string, string> playerData = new Dictionary<string, string>();
    List<string> rewardID = new List<string>();

    string freeProgress = "";
    string paidProgress = "";


    PlayerDataBase playerDataBase;
    SeasonDataBase seasonDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (seasonDataBase == null) seasonDataBase = Resources.Load("SeasonDataBase") as SeasonDataBase;

        seasonPassView.SetActive(false);

        seasonPassBuyButton.SetActive(false);
        seasonPassing.SetActive(false);

        alarm.SetActive(true);

        seasonPassTrasnform.anchoredPosition = new Vector2(0, -9999);
    }

    public void OpenSeasonView()
    {
        if (!seasonPassView.activeInHierarchy)
        {
            seasonPassView.SetActive(true);

            if (playerDataBase.SeasonPassDay == DateTime.Today.ToString("yyyyMMdd")) //시즌 패스 기간이 지나면 초기화 해야함
            {
                EndSeason();
            }

            if(!playerDataBase.SeasonPass)
            {
                seasonPassBuyButton.SetActive(true);
                seasonPassing.SetActive(false);
            }
            else
            {
                seasonPassBuyButton.SetActive(false);
                seasonPassing.SetActive(true);
            }

            CheckSeasonPass();

            alarm.SetActive(false);

            localization_Reset = LocalizationManager.instance.GetString("Reset");
            localization_Days = LocalizationManager.instance.GetString("Days");
            localization_Hours = LocalizationManager.instance.GetString("Hours");
            localization_Minutes = LocalizationManager.instance.GetString("Minutes");

            timerText.text = "";
            f = DateTime.Now;
            g = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
            StartCoroutine(TimerCoroution());

        }
        else
        {
            StopAllCoroutines();

            SaveSeasonPass();

            seasonPassView.SetActive(false);
        }
    }

    IEnumerator TimerCoroution()
    {
        if (timerText.gameObject.activeInHierarchy)
        {
            h = g - f;

            if(h.Days > 0)
            {
                timerText.text = localization_Reset + " : " + h.Days.ToString("D2") + localization_Days + " " + h.Hours.ToString("D2") + localization_Hours;
            }
            else
            {
                timerText.text = localization_Reset + " : " + h.Hours.ToString("D2") + localization_Hours + " " + h.Minutes.ToString("D2") + localization_Minutes;
            }

            if (playerDataBase.SeasonPassDay == DateTime.Today.ToString("yyyyMMdd"))
            {
                EndSeason();
                yield break;
            }
        }

        yield return waitForSeconds;
        StartCoroutine(TimerCoroution());
    }

    public void CheckSeasonPass() //현재 시즌 패스 체크
    {
        for (int i = 0; i < seasonContents.Length; i++)
        {
            seasonContents[i].Initialize(i, this);
        }

        score = 0;
        level = 0;
        goal = 0;

        score = playerDataBase.SeasonPassLevel;
        level = score / 500;
        goal = ((level + 1) * 500);

        if(level > 29)
        {
            level = 30;
        }

        scoreText.text = playerDataBase.SeasonPassLevel + " / " + goal.ToString();
        levelText.text = (level + 1).ToString();

        fillAmount.fillAmount = score * 1.0f / goal;

        for (int i = 0; i < level; i++) //열린 것 중에서 획득했는지 여부
        {
            if (playerDataBase.GetSeasonPass(SeasonPassType.Free, i) == false)
            {
                seasonContents[i].UnLockFree();
            }
            else
            {
                seasonContents[i].CheckMarkFree();
            }

            if (playerDataBase.SeasonPass) //시즌 패스를 구매했을 경우
            {
                if (playerDataBase.GetSeasonPass(SeasonPassType.Pass, i) == false)
                {
                    seasonContents[i].UnLockPass();
                }
                else
                {
                    seasonContents[i].CheckMarkPass();
                }
            }
        }
    }


    public void EndSeason() //시즌 패스 초기화
    {
        seasonPassBuyButton.SetActive(true);
        seasonPassing.SetActive(false);

        playerDataBase.SeasonPass = false;
        playerDataBase.SeasonPassDay = "";
        playerDataBase.SeasonPassLevel = 0;
        playerDataBase.FreeSeasonPassData = "000000000000000000000000000000";
        playerDataBase.PassSeasonPassData = "000000000000000000000000000000";

        StartCoroutine(SaveSeasonPassCoroution());

        CheckSeasonPass();
    }

    IEnumerator SaveSeasonPassCoroution()
    {
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SeasonPass", 0);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SeasonPassDay", 0);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SeasonPassLevel", 0);

        yield return waitForSeconds;

        playerData.Add("SeasonPass_Free", playerDataBase.FreeSeasonPassData);
        PlayfabManager.instance.SetPlayerData(playerData);

        playerData.Add("SeasonPass_Pass", playerDataBase.PassSeasonPassData);
        PlayfabManager.instance.SetPlayerData(playerData);
    }

    public void BuySeasonPass() //시즌 패스 구입
    {
        seasonPassing.SetActive(true);

        SoundManager.instance.PlaySFX(GameSfxType.Purchase);
        NotionManager.instance.UseNotion(NotionType.SuccessBuy);

        PlayfabManager.instance.PurchaseSeasonPass();

        CheckSeasonPass();

        Invoke("Delay", 1.0f);
    }

    void Delay()
    {
        seasonPassBuyButton.SetActive(false);
    }

    public void BuyFail() //구매 실패
    {
        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        NotionManager.instance.UseNotion(NotionType.CancelPurchase);
    }

    private void OnApplicationQuit()
    {
        SaveSeasonPass();
    }

    public void SaveSeasonPass()
    {
        playerData.Clear();

        freeProgress = playerDataBase.FreeSeasonPassData;

        if (freeProgress.Length < 29)
        {
            freeProgress = "000000000000000000000000000000";
        }

        playerData.Add("SeasonPass_Free", freeProgress);
        PlayfabManager.instance.SetPlayerData(playerData);

        playerData.Clear();

        paidProgress = playerDataBase.PassSeasonPassData;

        if (paidProgress.Length < 29)
        {
            paidProgress = "000000000000000000000000000000";
        }

        playerData.Add("SeasonPass_Pass", paidProgress);
        PlayfabManager.instance.SetPlayerData(playerData);

        Debug.Log("Save Season Pass");
    }
}
