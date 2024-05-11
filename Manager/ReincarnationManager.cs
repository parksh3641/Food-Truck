using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReincarnationManager : MonoBehaviour
{
    public GameObject reincarnationView;

    public GameObject alarm;

    public ReceiveContent receiveContent;

    public Text countText;
    public Text passiveText;
    public Text adText;

    public GameObject lockedObj;
    public GameObject lockedAdObj;
    public ButtonScaleAnimation buttonScaleAnim;


    private float oldPoint = 0;
    private float point = 0;
    private float plus = 0;
    private int number = 0;

    public FadeInOut fadeInOut;

    public TutorialManager tutorialManager;
    public GameManager gameManager;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1f);

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        reincarnationView.SetActive(false);

        alarm.SetActive(true);
    }

    public void OpenReincarnationView()
    {
        if(!reincarnationView.activeInHierarchy)
        {
            reincarnationView.SetActive(true);

            Initialize();

            alarm.SetActive(false);

            FirebaseAnalytics.LogEvent("Open_Challenge");
        }
        else
        {
            reincarnationView.SetActive(false);
        }
    }

    void Initialize()
    {
        point = 0;

        lockedObj.SetActive(true);
        lockedAdObj.SetActive(true);

        point = playerDataBase.NextFoodNumber * 10;

        if (point > 0)
        {
            lockedObj.SetActive(false);
            lockedAdObj.SetActive(false);
        }
        else
        {
            buttonScaleAnim.StopAnim();
        }

        plus = 0;
        plus += playerDataBase.Skill11 * 0.5f;
        plus += playerDataBase.Treasure10 * 0.5f;
        plus += playerDataBase.GetEquipValue(EquipType.Equip_Index_10);

        //if(crystal > 0)
        //{
        //    crystalText.text = MoneyUnitString.ToCurrencyString((int)crystal).ToString();
        //}
        //else
        //{
        //    crystalText.text = "0";
        //    passiveText.text = "";
        //}

        oldPoint = point;

        point += point * (plus * 0.01f);

        receiveContent.Initialize(RewardType.ChallengePoint, (int)point);

        countText.text = LocalizationManager.instance.GetString("Reincarnation_Count") + " : " + playerDataBase.ReincarnationCount;

        adText.text = "+" + MoneyUnitString.ToCurrencyString((int)point * 2).ToString();

        if (point > 0 && plus > 0)
        {
            passiveText.text = MoneyUnitString.ToCurrencyString((int)oldPoint).ToString();
            passiveText.text += "  (+" + plus.ToString() + "%) = " + MoneyUnitString.ToCurrencyString((int)point).ToString();
        }
        else
        {
            passiveText.text = "";
        }
    }

    public void Free()
    {
        if (point == 0) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        SoundManager.instance.PlaySFX(GameSfxType.Success);

        number = 0;
        StartCoroutine(ReincarnationCoroution());
    }

    public void Ad()
    {
        if (point == 0) return;

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        GoogleAdsManager.instance.admobReward_ReincarnationX2.ShowAd(4);
    }

    public void SuccessWatchAd()
    {
        number = 1;
        StartCoroutine(ReincarnationCoroution());

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }


    IEnumerator ReincarnationCoroution()
    {
        FirebaseAnalytics.LogEvent("Clear_Challenge");

        fadeInOut.FadeOut();

        yield return waitForSeconds;

        SoundManager.instance.ResetBGM();

        reincarnationView.SetActive(false);

        GameStateManager.instance.IslandType = IslandType.Island1;
        GameStateManager.instance.FoodType = FoodType.Food1;

        for(int i = 0; i < GameStateManager.instance.FoodLevel.Length; i ++)
        {
            GameStateManager.instance.FoodLevel[i] = 0;
        }

        playerDataBase.IslandNumber = 0;
        playerDataBase.ReincarnationCount += 1;

        playerDataBase.NextFoodNumber = 0;

        playerDataBase.YummyTimeCount = 0;
        GameStateManager.instance.YummyTimeCount = 0;

        GameManager.instance.Reincarnation();
        tutorialManager.Reincarnation();

        yield return waitForSeconds;

        if (number == 0)
        {
            PortionManager.instance.GetChallengePoint((int)point);
        }
        else
        {
            PortionManager.instance.GetChallengePoint((int)point * 2);
        }

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("IslandNumber", playerDataBase.IslandNumber);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("ReincarnationCount", playerDataBase.ReincarnationCount);

        yield return waitForSeconds;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber", playerDataBase.NextFoodNumber);
    }
}
