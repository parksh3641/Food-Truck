using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    public GameObject lockView;

    public GameObject[] menuIcon;

    public GameObject[] lockIcon;

    public GameObject tutorial;
    public LocalizationContent tutorialText;

    public LocalizationContent infoText;

    private int level = 0;

    private int number = 0;

    public GameManager gameManager;

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
        number = 0;
        Initialize();
    }

    [Button]
    public void LevelUp()
    {
        number += 1;
        UnLocked(number);
    }


    public void Initialize()
    {
        for (int i = 0; i < menuIcon.Length; i++)
        {
            menuIcon[i].SetActive(false);
        }

        tutorial.SetActive(true);
        tutorialText.localizationName = "Tutorial_First";
        tutorialText.ReLoad();

        menuIcon[7].SetActive(true);
        menuIcon[9].SetActive(true);
        menuIcon[10].SetActive(true);
        menuIcon[11].SetActive(true);

        if (playerDataBase.LockTutorial >= 1) //음식 변경
        {
            menuIcon[0].SetActive(true);

            tutorialText.localizationName = "Tutorial_Seconds";
            tutorialText.ReLoad();
        }

        if (playerDataBase.LockTutorial >= 2) //물약 해제
        {
            menuIcon[1].SetActive(true);
            menuIcon[2].SetActive(true);
            menuIcon[3].SetActive(true);
            menuIcon[5].SetActive(true);

            tutorial.SetActive(false);
        }

        if (playerDataBase.LockTutorial >= 3) //피버모드, 버프 해제
        {
            menuIcon[4].SetActive(true);
            menuIcon[6].SetActive(true);
            menuIcon[8].SetActive(true);
        }

        if (playerDataBase.LockTutorial >= 4) //레시피 해제
        {
            menuIcon[7].SetActive(false);
        }

        if (playerDataBase.LockTutorial >= 5) //퀘스트 해제
        {
            menuIcon[9].SetActive(false);
        }

        if (playerDataBase.LockTutorial >= 6) //택배 배송 해제
        {
            menuIcon[10].SetActive(false);
        }

        if (playerDataBase.LockTutorial >= 7) //챌린지 해제
        {
            menuIcon[11].SetActive(false);
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
                lockIcon[5].SetActive(true);

                gameManager.CheckPortion();

                break;
            case 3:
                lockIcon[4].SetActive(true);
                lockIcon[6].SetActive(true);

                break;
            case 4:
                lockIcon[7].SetActive(true);

                break;
            case 5:
                lockIcon[8].SetActive(true);

                break;
            case 6:
                lockIcon[9].SetActive(true);

                break;
            case 7:
                lockIcon[10].SetActive(true);

                break;
        }
    }

    public void CloseLockView()
    {
        lockView.SetActive(false);
    }
}
