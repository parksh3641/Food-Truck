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

    public GameObject button;

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

    public void Localization()
    {
        if(playerDataBase.LockTutorial == 0)
        {
            tutorialText.localizationName = "Tutorial_First";
            tutorialText.ReLoad();
        }
        else if(playerDataBase.LockTutorial == 1)
        {
            tutorialText.localizationName = "Tutorial_Seconds";
            tutorialText.ReLoad();
        }
    }


    public void Initialize()
    {
        for (int i = 0; i < menuIcon.Length; i++)
        {
            if (menuIcon[i] != null)
            {
                menuIcon[i].SetActive(false);
            }
        }

        tutorial.SetActive(true);

        menuIcon[7].SetActive(true);
        menuIcon[8].SetActive(true);
        menuIcon[9].SetActive(true);
        menuIcon[11].SetActive(true);
        menuIcon[12].SetActive(true);
        menuIcon[13].SetActive(true);
        menuIcon[14].SetActive(true);
        menuIcon[15].SetActive(true);
        menuIcon[16].SetActive(true);
        menuIcon[17].SetActive(true);

        if (playerDataBase.LockTutorial >= 1) //음식 변경
        {
            menuIcon[0].SetActive(true);
        }

        if (playerDataBase.LockTutorial >= 2) //물약 해제
        {
            menuIcon[1].SetActive(true);
            menuIcon[2].SetActive(true);
            menuIcon[3].SetActive(true);
            menuIcon[5].SetActive(true);

            tutorial.SetActive(false);
        }

        if (playerDataBase.LockTutorial >= 3) //피버모드 해제
        {
            menuIcon[4].SetActive(true);
            menuIcon[6].SetActive(true);
        }

        if (playerDataBase.LockTutorial >= 4) //레시피, 주방 청소, 버프 해제
        {
            menuIcon[7].SetActive(false);
            menuIcon[8].SetActive(false);
            menuIcon[9].SetActive(false);
            menuIcon[10].SetActive(true);
        }

        if (playerDataBase.LockTutorial >= 5) //퀘스트, 장비, 퀘스트 해제
        {
            menuIcon[11].SetActive(false);
            menuIcon[12].SetActive(false);
            menuIcon[15].SetActive(false);
        }

        if (playerDataBase.LockTutorial >= 6) //오프라인 보상 해제
        {
            menuIcon[13].SetActive(false);
        }

        if (playerDataBase.LockTutorial >= 7) //챌린지 해제
        {
            menuIcon[14].SetActive(false);
        }

        if (playerDataBase.LockTutorial >= 8) //도감, 이벤트
        {
            menuIcon[16].SetActive(false);
            menuIcon[17].SetActive(false);
            menuIcon[18].SetActive(true);
        }

        if (GameStateManager.instance.YoutubeVideo)
        {
            menuIcon[0].SetActive(false);
            menuIcon[10].SetActive(false);
        }

        if (GameStateManager.instance.StoreType == StoreType.OneStore)
        {
            menuIcon[18].SetActive(false);
        }
    }

    public void UnLocked(int number)
    {
        if (playerDataBase.LockTutorial >= number) return;

        if (number > 8) return;

        playerDataBase.LockTutorial = number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("LockTutorial", number);

        Initialize();

        lockView.SetActive(true);

        button.SetActive(false);
        Invoke("Delay", 3.0f);

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

                tutorialText.localizationName = "Tutorial_Seconds";
                tutorialText.ReLoad();

                if (playerDataBase.InGameTutorial == 0)
                {
                    GameManager.instance.moveArrow2.SetActive(true);
                }

                break;
            case 2:
                lockIcon[1].SetActive(true);
                lockIcon[2].SetActive(true);
                lockIcon[3].SetActive(true);
                lockIcon[5].SetActive(true);

                GameManager.instance.CheckPortion();

                break;
            case 3:
                lockIcon[4].SetActive(true);
                lockIcon[6].SetActive(true);

                break;
            case 4:
                lockIcon[7].SetActive(true);
                lockIcon[8].SetActive(true);
                lockIcon[9].SetActive(true);
                break;
            case 5:
                lockIcon[10].SetActive(true);
                lockIcon[11].SetActive(true);
                lockIcon[14].SetActive(true);

                break;
            case 6:
                lockIcon[12].SetActive(true);

                break;
            case 7:
                lockIcon[13].SetActive(true);
                break;
            case 8:
                lockIcon[14].SetActive(true);
                lockIcon[15].SetActive(true);
                break;
        }
    }

    void Delay()
    {
        button.SetActive(true);
    }

    public void CloseLockView()
    {
        lockView.SetActive(false);
    }

    public void ChangeFoodTutorial()
    {
        tutorialText.localizationName = "Tutorial_Third";
        tutorialText.ReLoad();

        GameManager.instance.moveArrow1.SetActive(false);
    }
    
    public void NextFoodTutorial()
    {
        tutorialText.localizationName = "Tutorial_First";
        tutorialText.ReLoad();
    }
}
