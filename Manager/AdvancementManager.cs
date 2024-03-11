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

    public Image nowClassImg;
    public Image nextClassImg;
    public Image mainScreenImg;

    public Text needText;
    public Text needText2;
    public Text needText3;

    public Text nowValueText;
    public Text nextValueText;

    public GameObject[] checkMarks;

    public Image[] needImg;

    public GameObject lockedObj;

    public Image advenceLevelUpButton;

    private int nowNeed1 = 0;
    private int nowNeed2 = 0;
    private int nowNeed3 = 0;

    private int need1 = 5;
    private int need2 = 50000;
    private int need3 = 3;

    private float nowValue1 = 0;
    private float nowValue2 = 0;
    private float nowValue3 = 0;

    private float value1 = 0.4f;
    private float value2 = 0.1f;
    private float value3 = 0.05f;

    private bool isActive = false;
    private bool isDelay = false;

    Sprite sp;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        lockedObj.SetActive(false);
    }

    public void Initialize()
    {
        chefType = ChefType.Cook1_1 + playerDataBase.Advancement;

        mainScreenImg.sprite = GetAdvencementImg(chefType);

        mainScreenText.localizationName = chefType.ToString().Substring(0, 5);
        mainScreenText.plusText = "  <color=#FFFF00>" + chefType.ToString().Substring(6, 1) + "</color>";
        mainScreenText.ReLoad();
    }

    public void OpenView()
    {
        chefType = ChefType.Cook1_1 + playerDataBase.Advancement;
        nextChefType = ChefType.Cook1_1 + playerDataBase.Advancement + 1;

        nowClassImg.sprite = GetAdvencementImg(chefType);
        nextClassImg.sprite = GetAdvencementImg(nextChefType);

        if (chefType.ToString().Length == 7)
        {
            nowAdvencementText.text = LocalizationManager.instance.GetString(chefType.ToString().Substring(0, 5)) + "  <color=#FFFF00>" +
    chefType.ToString().Substring(6, 1) + "</color>";
        }
        else
        {
            nowAdvencementText.text = LocalizationManager.instance.GetString(chefType.ToString().Substring(0, 6)) + "  <color=#FFFF00>" +
    chefType.ToString().Substring(7, 1) + "</color>";
        }

        if(nextChefType.ToString().Length == 7)
        {
            nextAdvencementText.text = LocalizationManager.instance.GetString(nextChefType.ToString().Substring(0, 5)) + "  <color=#FFFF00>" +
    nextChefType.ToString().Substring(6, 1) + "</color>";
        }
        else
        {
            nextAdvencementText.text = LocalizationManager.instance.GetString(nextChefType.ToString().Substring(0, 6)) + "  <color=#FFFF00>" +
    nextChefType.ToString().Substring(7, 1) + "</color>";
        }


        nowNeed1 = (playerDataBase.Advancement + 1) * need1;
        nowNeed2 = (playerDataBase.Advancement + 1) * need2;
        nowNeed3 = (playerDataBase.Advancement + 1) * need3;

        needText.text = LocalizationManager.instance.GetString("Level") + "\n(" + playerDataBase.Level + "/" + nowNeed1 +")";
        needText2.text = LocalizationManager.instance.GetString("GourmetScore") + "\n(" + MoneyUnitString.ToCurrencyString(playerDataBase.GourmetLevel) + "/" + MoneyUnitString.ToCurrencyString(nowNeed2) + ")";
        needText3.text = LocalizationManager.instance.GetString("Proficiency") + "\n(" + playerDataBase.Proficiency + "/" + nowNeed3 + ")";

        for (int i = 0; i < checkMarks.Length; i ++)
        {
            checkMarks[i].gameObject.SetActive(false);
            needImg[i].color = Color.red;
        }

        if(playerDataBase.Level >= nowNeed1)
        {
            checkMarks[0].gameObject.SetActive(true);

            needImg[0].color = Color.green;
        }

        if (playerDataBase.GourmetLevel >= nowNeed2)
        {
            checkMarks[1].gameObject.SetActive(true);

            needImg[1].color = Color.green;
        }

        if (playerDataBase.Proficiency >= nowNeed3)
        {
            checkMarks[2].gameObject.SetActive(true);

            needImg[2].color = Color.green;
        }

        if(playerDataBase.Level >= nowNeed1 && playerDataBase.GourmetLevel >= nowNeed2 && playerDataBase.Proficiency >= nowNeed3)
        {
            lockedObj.SetActive(false);

            isActive = true;
        }
        else
        {
            lockedObj.SetActive(true);

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

        isDelay = true;
        Invoke("Delay", 0.4f);

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

        FirebaseAnalytics.LogEvent("LevelUp_Advencement");
    }

    void Delay()
    {
        isDelay = false;
    }

    public Sprite GetAdvencementImg(ChefType chefType)
    {
        switch (chefType)
        {
            case ChefType.Cook1_1:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook1_2:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook1_3:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook1_4:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook2_1:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook2_2:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook2_3:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook2_4:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook3_1:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook3_2:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook3_3:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook3_4:
                sp = imageDataBase.GetAdvancementArray(0);
                break;
            case ChefType.Cook4_1:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook4_2:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook4_3:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook4_4:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook5_1:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook5_2:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook5_3:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook5_4:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook6_1:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook6_2:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook6_3:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook6_4:
                sp = imageDataBase.GetAdvancementArray(1);
                break;
            case ChefType.Cook7_1:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook7_2:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook7_3:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook7_4:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook8_1:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook8_2:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook8_3:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook8_4:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook9_1:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook9_2:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook9_3:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook9_4:
                sp = imageDataBase.GetAdvancementArray(2);
                break;
            case ChefType.Cook10_1:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook10_2:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook10_3:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook10_4:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook11_1:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook11_2:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook11_3:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook11_4:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook12_1:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook12_2:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook12_3:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook12_4:
                sp = imageDataBase.GetAdvancementArray(3);
                break;
            case ChefType.Cook13_1:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook13_2:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook13_3:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook13_4:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook14_1:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook14_2:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook14_3:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook14_4:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook15_1:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook15_2:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook15_3:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
            case ChefType.Cook15_4:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
        }

        return sp;
    }
}
