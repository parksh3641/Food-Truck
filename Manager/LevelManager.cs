using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelView;

    public GameObject alarm;

    public Text titleText;
    public Text titleInfoText;
    public Text levelText;
    public Text expText;
    public Image expFillamount;

    public LocalizationContent infoText;
    public Text accessDateText;

    public GameObject levelUpAnim;
    public CanvasGroup canvasGroup;

    private int nowLevel = 0;
    private int level = 0;
    private int nowExp = 0;
    private int nextExp = 0;

    private int maxExp = 0;

    private int expUp = 0;
    private float expUpPlus = 0;

    private float duration = 0.7f;
    private float currentTime;

    private Color skyblueColor = new Color(0, 1, 1);

    public AdvancementManager advancementManager;

    PlayerDataBase playerDataBase;
    LevelDataBase levelDataBase;
    AnimalDataBase animalDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (levelDataBase == null) levelDataBase = Resources.Load("LevelDataBase") as LevelDataBase;
        if (animalDataBase == null) animalDataBase = Resources.Load("AnimalDataBase") as AnimalDataBase;

        levelDataBase.Initialize();

        for (int i = 0; i < levelDataBase.levelInfoList.Count; i++)
        {
            maxExp += levelDataBase.levelInfoList[i].needExp;
        }

        alarm.SetActive(true);

        levelView.SetActive(false);
        levelUpAnim.SetActive(false);
    }


    public void OpenLevelView()
    {
        if (!levelView.activeInHierarchy)
        {
            levelView.SetActive(true);

            alarm.SetActive(false);

            Initialize();

            advancementManager.OpenView();

            infoText.ReLoad();

            FirebaseAnalytics.LogEvent("Open_Profile");
        }
        else
        {
            levelView.SetActive(false);
        }
    }

    public bool CheckMaxLevel()
    {
        if(playerDataBase.Exp >= maxExp)
        {
            playerDataBase.Exp = maxExp;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);

            return true;
        }
        else
        {
            return false;
        }
    }

    public void Initialize()
    {
        nowLevel = level;
        level = levelDataBase.GetLevel(playerDataBase.Exp);

        if(nowLevel != 0 && level > nowLevel)
        {
            GameManager.instance.CheckPercent();

            LevelIUpAnimation();

            FirebaseAnalytics.LogEvent("LevelUp_Profile");

            Debug.LogError("���� ��");
        }

        if(playerDataBase.Level != level)
        {
            playerDataBase.Level = level;
#if !UNITY_EDITOR
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("Level", playerDataBase.Level);
#endif
        }

        GameStateManager.instance.Level = level;

        titleText.text = "Lv." + level.ToString();
        levelText.text = level.ToString();

        titleInfoText.text = LocalizationManager.instance.GetString("LevelInfo") + "  <color=#FFFF00>(+" + expUp +")</color>";

        nowExp = levelDataBase.GetNowExp(playerDataBase.Exp);
        nextExp = levelDataBase.GetNextExp(level);

        if(playerDataBase.Exp >= maxExp)
        {
            expText.text = LocalizationManager.instance.GetString("MaxLevel");
            expFillamount.fillAmount = 1;
        }
        else
        {
            expText.text = nowExp + " / " + nextExp;
            expFillamount.fillAmount = (nowExp * 1.0f) / (nextExp * 1.0f);
        }


        infoText.localizationName = "SuccessPercent";

        if(level > 99)
        {
            infoText.plusText = " <color=#FFFF00>+" + (100 * 0.3f).ToString("N1") + "%</color>";
        }
        else
        {
            infoText.plusText = " <color=#FFFF00>+" + (level * 0.3f).ToString("N1") + "%</color>";
        }

        accessDateText.text = LocalizationManager.instance.GetString("AccessDate") + " : " + playerDataBase.AccessDate;

        GourmetManager.instance.Initialize();

        GameManager.instance.CheckLocked();
    }

    [Button]

    void LevelIUpAnimation()
    {
        StartCoroutine(FadeInOut());

        SoundManager.instance.PlaySFX(GameSfxType.Upgrade5);
        NotionManager.instance.UseNotion2(skyblueColor, (level - 1) + " �� " + level + "\n" + LocalizationManager.instance.GetString("Levelup"));
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
