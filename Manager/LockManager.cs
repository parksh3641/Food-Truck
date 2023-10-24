using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    public GameObject lockView;

    public GameObject[] lockIcon;

    public GameObject[] menuIcon;

    public LocalizationContent infoText;

    private int level = 0;


    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        lockView.SetActive(false);
    }

    [Button]
    public void OnReset()
    {
        playerDataBase.LockTutorial = 0;
    }

    [Button]
    public void LevelUp()
    {
        playerDataBase.LockTutorial += 1;
        Initialize();
    }


    public void Initialize()
    {
        for (int i = 0; i < menuIcon.Length; i++)
        {
            menuIcon[i].SetActive(false);
        }

        if (playerDataBase.LockTutorial >= 1) //음식 변경
        {
            menuIcon[0].SetActive(true);
        }

        if (playerDataBase.LockTutorial >= 2) //물약 해제
        {
            menuIcon[1].SetActive(true);
            menuIcon[2].SetActive(true);
            menuIcon[3].SetActive(true);
            menuIcon[4].SetActive(true);
        }

        if (playerDataBase.LockTutorial >= 3) //피버모드 해제
        {
            menuIcon[5].SetActive(true);
        }

        if (playerDataBase.LockTutorial >= 4) //연구소 해제
        {
            menuIcon[6].SetActive(true);
        }
    }

    public void UnLocked(int number)
    {
        if (playerDataBase.LockTutorial >= number) return;

        playerDataBase.LockTutorial = number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("LockTutorial", number);
        Initialize();

        lockView.SetActive(true);

        infoText.localizationName = "LockTutorial" + number;
        infoText.ReLoad();

        for (int i = 0; i < lockIcon.Length; i++)
        {
            lockIcon[i].SetActive(false);
        }

        switch(number)
        {
            case 1:
                lockIcon[0].SetActive(true);

                break;
            case 2:
                lockIcon[1].SetActive(true);
                lockIcon[2].SetActive(true);
                lockIcon[3].SetActive(true);
                lockIcon[4].SetActive(true);

                break;
            case 3:
                lockIcon[5].SetActive(true);

                break;
            case 4:
                lockIcon[6].SetActive(true);

                break;
        }
    }

    public void CloseLockView()
    {
        lockView.SetActive(false);
    }
}
