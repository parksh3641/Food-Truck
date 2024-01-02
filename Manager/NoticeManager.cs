using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeManager : MonoBehaviour
{
    public GameObject noticeView;

    public GameObject checkMark;

    public GameObject alarm;


    private void Awake()
    {
        noticeView.SetActive(false);
        alarm.SetActive(false);
    }

    public void Initialize()
    {
        if (GameStateManager.instance.Tutorial && !GameStateManager.instance.HideNotice)
        {
            noticeView.SetActive(true);
        }
    }

    public void OpenNoticeView()
    {
        if (!noticeView.activeInHierarchy)
        {
            noticeView.SetActive(true);

            alarm.SetActive(false);

            checkMark.SetActive(GameStateManager.instance.HideNotice);

            FirebaseAnalytics.LogEvent("OpenNotice");
        }
        else
        {
            noticeView.SetActive(false);
        }
    }

    public void HideCheck()
    {
        if(GameStateManager.instance.HideNotice)
        {
            GameStateManager.instance.HideNotice = false;

            checkMark.SetActive(false);
        }
        else
        {
            GameStateManager.instance.HideNotice = true;

            checkMark.SetActive(true);
        }
    }
}
