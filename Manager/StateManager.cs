using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    public FadeInOut fadeInOut;

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

    private void Awake()
    {
        instance = this;
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

    public void Initialize()
    {
        fadeInOut.FadeIn();
        ResetManager.instance.Initialize();
        shopManager.Initialize();
        gameManager.Initialize();
        lockManager.Initialize();
        chestBoxManager.Initialize();
        //questManager.Initialize();
        offlineManager.Initialize();
        newsManager.Initialize();
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

        Debug.LogError("Load Complete!");
    }
}
