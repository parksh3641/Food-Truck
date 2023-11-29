using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    public GameObject treasureView;


    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        treasureView.SetActive(false);
    }


    public void OpenTreasureView()
    {
        if (!treasureView.activeInHierarchy)
        {
            treasureView.SetActive(true);

            Initialize();

            FirebaseAnalytics.LogEvent("OpenTreasure");
        }
        else
        {
            treasureView.SetActive(false);
        }
    }

    void Initialize()
    {

    }

}
