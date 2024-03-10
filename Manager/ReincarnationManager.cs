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

            FirebaseAnalytics.LogEvent("OpenChallenge");
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

        if (playerDataBase.IslandNumber > 0)
        {
            point += 50;

            if (playerDataBase.NextFoodNumber2 > 7)
            {
                point += 100;
            }

            if (playerDataBase.NextFoodNumber3 > 5)
            {
                point += 150;
            }

            if (playerDataBase.NextFoodNumber4 > 7)
            {
                point += 250;
            }

            lockedObj.SetActive(false);
            lockedAdObj.SetActive(false);
        }
        else
        {
            buttonScaleAnim.StopAnim();
        }

        plus = 0;
        plus += playerDataBase.Skill11 * 0.5f;
        plus += playerDataBase.Treasure10 * 1;

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

        point = point + (point * (0.005f * playerDataBase.Skill11));
        point = point + (point * (0.01f * playerDataBase.Treasure10));

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

        SoundManager.instance.PlaySFX(GameSfxType.Success);

        number = 0;
        StartCoroutine(ReincarnationCoroution());
    }

    public void Ad()
    {
        if (point == 0) return;

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
        FirebaseAnalytics.LogEvent("ChallengeClear");

        fadeInOut.FadeOut();

        yield return waitForSeconds;

        SoundManager.instance.ResetBGM();

        reincarnationView.SetActive(false);

        GameStateManager.instance.IslandType = IslandType.Island1;
        GameStateManager.instance.FoodType = FoodType.Food1;
        GameStateManager.instance.CandyType = CandyType.Candy1;
        GameStateManager.instance.JapaneseFoodType = JapaneseFoodType.JapaneseFood1;
        GameStateManager.instance.DessertType = DessertType.Dessert1;

        GameStateManager.instance.Food1Level = 0;
        GameStateManager.instance.Food2Level = 0;
        GameStateManager.instance.Food3Level = 0;
        GameStateManager.instance.Food4Level = 0;
        GameStateManager.instance.Food5Level = 0;
        GameStateManager.instance.Food6Level = 0;
        GameStateManager.instance.Food7Level = 0;

        GameStateManager.instance.Candy1Level = 0;
        GameStateManager.instance.Candy2Level = 0;
        GameStateManager.instance.Candy3Level = 0;
        GameStateManager.instance.Candy4Level = 0;
        GameStateManager.instance.Candy5Level = 0;
        GameStateManager.instance.Candy6Level = 0;
        GameStateManager.instance.Candy7Level = 0;
        GameStateManager.instance.Candy8Level = 0;
        GameStateManager.instance.Candy9Level = 0;

        GameStateManager.instance.JapaneseFood1Level = 0;
        GameStateManager.instance.JapaneseFood2Level = 0;
        GameStateManager.instance.JapaneseFood3Level = 0;
        GameStateManager.instance.JapaneseFood4Level = 0;
        GameStateManager.instance.JapaneseFood5Level = 0;
        GameStateManager.instance.JapaneseFood6Level = 0;
        GameStateManager.instance.JapaneseFood7Level = 0;

        GameStateManager.instance.Dessert1Level = 0;
        GameStateManager.instance.Dessert2Level = 0;
        GameStateManager.instance.Dessert3Level = 0;
        GameStateManager.instance.Dessert4Level = 0;
        GameStateManager.instance.Dessert5Level = 0;
        GameStateManager.instance.Dessert6Level = 0;
        GameStateManager.instance.Dessert7Level = 0;
        GameStateManager.instance.Dessert8Level = 0;
        GameStateManager.instance.Dessert9Level = 0;

        playerDataBase.IslandNumber = 0;
        playerDataBase.ReincarnationCount += 1;

        playerDataBase.NextFoodNumber = 0;
        playerDataBase.NextFoodNumber2 = 0;
        playerDataBase.NextFoodNumber3 = 0;
        playerDataBase.NextFoodNumber4 = 0;

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
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber2", playerDataBase.NextFoodNumber2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber3", playerDataBase.NextFoodNumber3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("NextFoodNumber4", playerDataBase.NextFoodNumber4);
    }
}
