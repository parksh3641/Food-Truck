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
    public NoticeManager noticeManager;
    public ChangeFoodManager changeFoodManager;
    public AttendanceManager attendanceManager;
    public GourmetManager gourmetManager;
    public WelcomeManager welcomeManager;
    public RankEventManager rankEventManager;
    public PlayTimeManager playTimeManager;
    public TreasureManager treasureManager;
    public WarningManager warningManager;

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
        noticeManager.Initialize();
        changeFoodManager.Initialize();
        attendanceManager.Initialize();
        welcomeManager.Initialize();
        rankEventManager.Initialize();
        playTimeManager.Initialize();
        treasureManager.Initialize();
        gourmetManager.FirstInitialize();
        warningManager.Initialize();

        Debug.LogError("Load Complete!");
    }
}
