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
    public PhotonManager photonManager;

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
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        string installerPackageName = packageManager.Call<string>("getInstallerPackageName", Application.identifier);

        // 패키지명으로부터 설치된 스토어 확인

        if (installerPackageName.Equals("com.android.vending"))
        {
            GameStateManager.instance.StoreType = StoreType.Google;

            Debug.Log("앱은 Google Play 스토어에서 설치되었습니다.");
        }
        else if (installerPackageName.Equals("com.amazon.venezia"))
        {
            GameStateManager.instance.StoreType = StoreType.Amazon;

            Debug.Log("앱은 Amazon Appstore에서 설치되었습니다.");
        }
        else if (installerPackageName.Equals("com.skt.skaf.A000Z00040"))
        {
            GameStateManager.instance.StoreType = StoreType.OneStore;

            Debug.Log("앱은 OneStore에서 설치되었습니다.");
        }
        else
        {
            GameStateManager.instance.StoreType = StoreType.None;

            Debug.Log("앱은 알 수 없는 소스에서 설치되었습니다.");
        }
#endif

        ResetManager.instance.Initialize();
        StopAllCoroutines();
        loginText.text = "";
    }

    public void SuccessReset()
    {
        fadeInOut.FadeIn();

        newsManager.Initialize();
        shopManager.Initialize();
        gameManager.Initialize();
        lockManager.Initialize();
        chestBoxManager.Initialize();
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
        advancementManager.Initialize();
        iconManager.Initialize();
        SeasonManager.instance.CheckSeason();
        GourmetManager.instance.Initialize();

        if (GameStateManager.instance.NickName.Length > 0)
        {
            photonManager.Initialize();
        }

        Debug.LogError("Load Complete!");
    }
}
