using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancementManager : MonoBehaviour
{
    ChefType chefType = ChefType.Cook1_1;
    ChefType nextChefType = ChefType.Cook1_1;

    public LocalizationContent mainScreenText; //간판 칭호

    public Text nowAdvencementText; //지금 칭호
    public Text nextAdvencementText; //다음 칭호

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

    public GameObject titleLockedObj;
    public GameObject lockedObj;

    public Image advenceLevelUpButton;

    public GameObject levelUpAnim;
    public CanvasGroup canvasGroup;

    private int nowNeed1 = 0;
    private int nowNeed2 = 0;
    private int nowNeed3 = 0;

    private int need1 = 5;
    private int need2 = 15000;
    private int need3 = 3;

    private float nowValue1 = 0;
    private float nowValue2 = 0;
    private float nowValue3 = 0;

    private float value1 = 0.5f;
    private float value2 = 1f;
    private float value3 = 0.25f;

    private bool isMax = false;
    private bool isActive = false;
    private bool isDelay = false;

    private float duration = 0.7f;
    private float currentTime;

    Sprite sp;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        titleLockedObj.SetActive(true);
        lockedObj.SetActive(true);

        levelUpAnim.SetActive(false);
    }

    public void Initialize()
    {
        if (playerDataBase.Advancement > Enum.GetValues(typeof(ChefType)).Length - 2)
        {
            playerDataBase.Advancement = Enum.GetValues(typeof(ChefType)).Length - 2;
        }

        chefType = ChefType.Cook1_1 + playerDataBase.Advancement;

        mainScreenImg.sprite = GetAdvencementImg(chefType);

        mainScreenText.localizationName = chefType.ToString().Substring(0, 5);
        mainScreenText.plusText = "  <color=#FFFF00>" + chefType.ToString().Substring(6, 1) + "</color>";
        mainScreenText.ReLoad();
    }

    public void OpenView()
    {
        if (playerDataBase.Advancement > Enum.GetValues(typeof(ChefType)).Length - 2)
        {
            playerDataBase.Advancement = Enum.GetValues(typeof(ChefType)).Length - 2;

            isMax = true;

            Debug.Log("요리사 진급 최대치 입니다.");
        }
        else
        {
            isMax = false;
        }

        chefType = ChefType.Cook1_1 + playerDataBase.Advancement;
        nowClassImg.sprite = GetAdvencementImg(chefType);

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

        nextChefType = ChefType.Cook1_1 + playerDataBase.Advancement + 1;
        nextClassImg.sprite = GetAdvencementImg(nextChefType);

        if (nextChefType.ToString().Length == 7)
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

        if(playerDataBase.IslandNumber >= 2)
        {
            titleLockedObj.SetActive(false);

            if (playerDataBase.Level >= nowNeed1 && playerDataBase.GourmetLevel >= nowNeed2 && playerDataBase.Proficiency >= nowNeed3)
            {
                lockedObj.SetActive(false);

                isActive = true;
            }
            else
            {
                lockedObj.SetActive(true);

                isActive = false;
            }
        }
        else
        {
            titleLockedObj.SetActive(true);
            lockedObj.SetActive(true);
        }

#if UNITY_EDITOR
        titleLockedObj.SetActive(false);
        lockedObj.SetActive(false);
        isActive = true;
#endif

        nowValue1 = playerDataBase.Advancement * value1;
        nowValue2 = playerDataBase.Advancement * value2;
        nowValue3 = playerDataBase.Advancement * value3;

        nowValueText.text = LocalizationManager.instance.GetString("NowPrice") + " <color=#FFFF00>+" + nowValue1.ToString("N1") + "%</color>\n"
            + LocalizationManager.instance.GetString("SuccessPercent") + " <color=#FFFF00>+" + nowValue2.ToString("N1") + "%</color>\n"
            + LocalizationManager.instance.GetString("DefDestroyPercent") + " <color=#FFFF00>+" + nowValue3.ToString("N2") + "%</color>";

        nowValue1 = (playerDataBase.Advancement + 1) * value1;
        nowValue2 = (playerDataBase.Advancement + 1) * value2;
        nowValue3 = (playerDataBase.Advancement + 1) * value3;

        nextValueText.text = LocalizationManager.instance.GetString("NowPrice") + " <color=#FFFF00>+" + nowValue1.ToString("N1") + "%</color>\n"
    + LocalizationManager.instance.GetString("SuccessPercent") + " <color=#FFFF00>+" + nowValue2.ToString("N1") + "%</color>\n"
    + LocalizationManager.instance.GetString("DefDestroyPercent") + " <color=#FFFF00>+" + nowValue3.ToString("N2") + "%</color>";

    }

    public void AdvencementLevelUp()
    {
        if (isDelay) return;

        if (isMax)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.MaxLevel);
            return;
        }

        if (!isActive)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NotEnoughConditions);

            return;
        }

        StartCoroutine(FadeInOut());

        playerDataBase.Advancement += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Advancement", playerDataBase.Advancement);

        SoundManager.instance.PlaySFX(GameSfxType.UpgradeMax);
        NotionManager.instance.UseNotion2(NotionType.SuccessPromotion);

        OpenView();

        Initialize();

        GameManager.instance.CheckPercent();

        FirebaseAnalytics.LogEvent("LevelUp_Advencement");

        isDelay = true;
        Invoke("Delay", 0.4f);
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
            case ChefType.Cook16_1:
                sp = imageDataBase.GetAdvancementArray(4);
                break;
        }

        return sp;
    }

    IEnumerator FadeInOut()
    {
        levelUpAnim.SetActive(true);
        canvasGroup.alpha = 0;

        currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        yield return new WaitForSeconds(1f); // Wait for 1 second

        currentTime = 0f; // Reset time for the next loop

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        levelUpAnim.SetActive(false);
    }
}
