using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeManager : MonoBehaviour
{
    public GameObject noticeView;

    public GameObject alarm;


    private void Awake()
    {
        noticeView.SetActive(false);
        alarm.SetActive(false);
    }

    public void Initialize()
    {
#if !UNITY_EDITOR
        noticeView.SetActive(true);
#endif
    }

    public void OpenNoticeView()
    {
        if (!noticeView.activeInHierarchy)
        {
            noticeView.SetActive(true);

            alarm.SetActive(false);

            FirebaseAnalytics.LogEvent("OpenNotice");
        }
        else
        {
            noticeView.SetActive(false);
        }
    }
}
