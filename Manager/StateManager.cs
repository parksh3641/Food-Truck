using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    public FadeInOut fadeInOut;
    public Text loginText;
    public GameObject internet;

    public ShopManager shopManager;
    public GameManager gameManager;
    public LockManager lockManager;
    public ChestBoxManager chestBoxManager;
    public QuestManager questManager;
    public OfflineManager offlineManager;
    public NewsManager newsManager;
    public ChangeFoodManager changeFoodManager;
    public AttendanceManager attendanceManager;
    public GourmetManager gourmetManager;
    public WelcomeManager welcomeManager;
    public RankEventManager rankEventManager;
    public RecipeEventManager recipeEventManager;
    public LevelUpEventManager levelUpEventManager;
    public PlayTimeManager playTimeManager;
    public TreasureManager treasureManager;
    public WarningManager warningManager;
    public AdvancementManager advancementManager;
    public IconManager iconManager;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    private void Awake()
    {
        instance = this;

        loginText.text = "";

        internet.SetActive(false);
    }

    private void Start()
    {
        if(!GameStateManager.instance.AutoLogin)
        {
            fadeInOut.canvasGroup.gameObject.SetActive(true);
            fadeInOut.canvasGroup.alpha = 1;
        }
        else
        {
            if (PlayfabManager.instance.isActive)
            {
                fadeInOut.canvasGroup.gameObject.SetActive(false);
            }
            else
            {
                fadeInOut.canvasGroup.gameObject.SetActive(true);
                fadeInOut.canvasGroup.alpha = 1;
            }
        }
    }

    public void LoginStart()
    {
        loginText.text = "";
        internet.SetActive(false);
        StartCoroutine(LoginCoroution());
    }

    IEnumerator LoginCoroution()
    {
        loginText.text = LocalizationManager.instance.GetString("Login...");

        yield return waitForSeconds;

        loginText.text = LocalizationManager.instance.GetString("Login...") + ".";

        yield return waitForSeconds;

        loginText.text = LocalizationManager.instance.GetString("Login...") + "..";

        yield return waitForSeconds;

        loginText.text = LocalizationManager.instance.GetString("Login...") + "...";

        yield return waitForSeconds;

        StartCoroutine(LoginCoroution());
    }

    IEnumerator ServerCoroution()
    {
        loginText.text = LocalizationManager.instance.GetString("Server...");

        yield return waitForSeconds;

        loginText.text = LocalizationManager.instance.GetString("Server...") + ".";

        yield return waitForSeconds;

        loginText.text = LocalizationManager.instance.GetString("Server...") + "..";

        yield return waitForSeconds;

        loginText.text = LocalizationManager.instance.GetString("Server...") + "...";

        yield return waitForSeconds;

        StartCoroutine(ServerCoroution());
    }

    public void ServerStart()
    {
        StopAllCoroutines();
        loginText.text = "";
        StartCoroutine(ServerCoroution());
        StartCoroutine(CheckInternet());
    }

    IEnumerator CheckInternet()
    {
        yield return new WaitForSeconds(10);

        internet.SetActive(true);
    }

    public void Initialize()
    {
        fadeInOut.FadeIn();
        ResetManager.instance.Initialize();
        newsManager.Initialize();
        shopManager.Initialize();
        gameManager.Initialize();
        lockManager.Initialize();
        chestBoxManager.Initialize();
        //questManager.Initialize();
        offlineManager.Initialize();
        changeFoodManager.Initialize();
        attendanceManager.Initialize();
        welcomeManager.Initialize();
        rankEventManager.Initialize();
        recipeEventManager.Initialize();
        levelUpEventManager.Initialize();
        playTimeManager.Initialize();
        treasureManager.Initialize();
        gourmetManager.FirstInitialize();
        warningManager.Initialize();
        SeasonManager.instance.CheckSeason();
        advancementManager.Initialize();
        iconManager.Initialize();
        GourmetManager.instance.Initialize();

        StopAllCoroutines();
        loginText.text = "";

        Debug.LogError("Load Complete!");
    }
}
