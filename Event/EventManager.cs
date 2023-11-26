using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject eventView;

    public GameObject alarmObj;

    private void Awake()
    {
        eventView.SetActive(false);

        alarmObj.SetActive(true);
    }

    public void OpenEventView()
    {
        if (!eventView.activeInHierarchy)
        {
            eventView.SetActive(true);
            alarmObj.SetActive(false);

            FirebaseAnalytics.LogEvent("OpenEvent");
        }
        else
        {
            eventView.SetActive(false);
        }
    }
}
