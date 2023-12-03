using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    public FadeInOut fadeInOut;

    public GameManager gameManager;
    public ResetManager resetManager;
    public LockManager lockManager;
    public ChestBoxManager chestBoxManager;
    public QuestManager questManager;
    public OfflineManager offlineManager;
    public NoticeManager noticeManager;
    public ChangeFoodManager changeFoodManager;

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
        gameManager.Initialize();
        resetManager.Initialize();
        lockManager.Initialize();
        chestBoxManager.Initialize();
        //questManager.Initialize();
        offlineManager.Initialize();
        noticeManager.Initialize();
        changeFoodManager.Initialize();

        Debug.Log("Load Complete!");
    }
}
