using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject eventView;

    private void Awake()
    {
        eventView.SetActive(false);
    }

    public void OpenEventView()
    {
        if (!eventView.activeInHierarchy)
        {
            eventView.SetActive(true);

            FirebaseAnalytics.LogEvent("OpenEvent");
        }
        else
        {
            eventView.SetActive(false);
        }
    }
}
