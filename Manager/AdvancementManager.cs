using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancementManager : MonoBehaviour
{
    ChefType chefType = ChefType.Cook1_1;
    ChefType nextChefType = ChefType.Cook1_1;

    public LocalizationContent mainScreenText; //°£ÆÇ ÄªÈ£

    public Text nowAdvencementText; //Áö±Ý ÄªÈ£
    public Text nextAdvencementText; //´ÙÀ½ ÄªÈ£

    public Text needText;
    public Text needText2;
    public Text needText3;

    public Text nowValueText;
    public Text nextValueText;

    public GameObject[] checkMarks;

    public Sprite[] buttonImg;

    public Image advenceLevelUpButton;

    private int nowNeed1 = 0;
    private int nowNeed2 = 0;
    private int nowNeed3 = 0;

    private int need1 = 5;
    private int need2 = 50000;
    private int need3 = 2;

    private float nowValue1 = 0;
    private float nowValue2 = 0;
    private float nowValue3 = 0;

    private float value1 = 0.4f;
    private float value2 = 0.1f;
    private float value3 = 0.05f;

    private bool isActive = false;
    private bool isDelay = false;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void Initialize()
    {
        chefType = ChefType.Cook1_1 + playerDataBase.Advancement;

        mainScreenText.localizationName = chefType.ToString().Substring(0, 5);
        mainScreenText.plusText = " " + chefType.ToString().Substring(6, 1);
        mainScreenText.ReLoad();
    }

    public void OpenView()
    {
        chefType = ChefType.Cook1_1 + playerDataBase.Advancement;
        nextChefType = ChefType.Cook1_1 + playerDataBase.Advancement + 1;

        nowAdvencementText.text = LocalizationManager.instance.GetString(chefType.ToString().Substring(0, 5)) + " <color=#FFC800>" +
            chefType.ToString().Substring(6, 1) + "</color>";
        nextAdvencementText.text = LocalizationManager.instance.GetString(nextChefType.ToString().Substring(0, 5)) + " <color=#FFC800>" +
            nextChefType.ToString().Substring(6, 1) + "</color>";

        nowNeed1 = (playerDataBase.Advancement + 1) * need1;
        nowNeed2 = (playerDataBase.Advancement + 1) * need2;
        nowNeed3 = (playerDataBase.Advancement + 1) * need3;

        needText.text = LocalizationManager.instance.GetString("Level") + "\n(" + playerDataBase.Level + "/" + nowNeed1 +")";
        needText2.text = LocalizationManager.instance.GetString("GourmetScore") + "\n(" + MoneyUnitString.ToCurrencyString(playerDataBase.GourmetLevel) + "/" + MoneyUnitString.ToCurrencyString(nowNeed2) + ")";
        needText3.text = LocalizationManager.instance.GetString("Proficiency") + "\n(" + playerDataBase.Proficiency + "/" + nowNeed3 + ")";

        for (int i = 0; i < checkMarks.Length; i ++)
        {
            checkMarks[i].gameObject.SetActive(false);
        }

        if(playerDataBase.Level >= nowNeed1)
        {
            checkMarks[0].gameObject.SetActive(true);
        }

        if (playerDataBase.GourmetLevel >= nowNeed2)
        {
            checkMarks[1].gameObject.SetActive(true);
        }

        if (playerDataBase.Proficiency >= nowNeed3)
        {
            checkMarks[2].gameObject.SetActive(true);
        }

        if(playerDataBase.Level >= nowNeed1 && playerDataBase.GourmetLevel >= nowNeed2 && playerDataBase.Proficiency >= nowNeed3)
        {
            advenceLevelUpButton.sprite = buttonImg[1];

            isActive = true;
        }
        else
        {
            advenceLevelUpButton.sprite = buttonImg[0];

            isActive = false;
        }

        nowValue1 = playerDataBase.Advancement * value1;
        nowValue2 = playerDataBase.Advancement * value2;
        nowValue3 = playerDataBase.Advancement * value3;

        nowValueText.text = LocalizationManager.instance.GetString("NowPrice") + " : <color=#FFFF00>+" + nowValue1.ToString("N1") + "%</color>\n"
            + LocalizationManager.instance.GetString("SuccessPercent") + " : <color=#FFFF00>+" + nowValue2.ToString("N1") + "%</color>\n"
            + LocalizationManager.instance.GetString("DefDestroyPercent") + " : <color=#FFFF00>+" + nowValue3.ToString("N2") + "%</color>";

        nowValue1 = (playerDataBase.Advancement + 1) * value1;
        nowValue2 = (playerDataBase.Advancement + 1) * value2;
        nowValue3 = (playerDataBase.Advancement + 1) * value3;

        nextValueText.text = LocalizationManager.instance.GetString("NowPrice") + " : <color=#FFFF00>+" + nowValue1.ToString("N1") + "%</color>\n"
    + LocalizationManager.instance.GetString("SuccessPercent") + " : <color=#FFFF00>+" + nowValue2.ToString("N1") + "%</color>\n"
    + LocalizationManager.instance.GetString("DefDestroyPercent") + " : <color=#FFFF00>+" + nowValue3.ToString("N2") + "%</color>";

    }

    public void AdvencementLevelUp()
    {
        if (isDelay) return;

        if (!isActive)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NotEnoughConditions);

            return;
        }

        playerDataBase.Advancement += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Advancement", playerDataBase.Advancement);

        SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);
        NotionManager.instance.UseNotion(NotionType.SuccessPromotion);

        OpenView();

        Initialize();

        GameManager.instance.CheckPercent();

        isDelay = true;
        Invoke("Delay", 0.4f);

        FirebaseAnalytics.LogEvent("AdvencementLevelUp");
    }

    void Delay()
    {
        isDelay = false;
    }

}
