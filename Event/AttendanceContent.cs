using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttendanceContent : MonoBehaviour
{
    public int index = 0;

    public LocalizationContent titleText;

    public ReceiveContent[] receiveContent;

    public GameObject selectObj;
    public GameObject lockObj;
    public GameObject clearObj;


    AttendanceManager attendanceManager;
    RankEventManager rankEventManager;
    PlayTimeManager playTimeManager;
    WelcomeManager welcomeManager;
    RecipeEventManager recipeEventManager;
    LevelUpEventManager levelUpEventManager;


    private void Awake()
    {
        for(int i = 0; i < receiveContent.Length; i ++)
        {
            receiveContent[i].gameObject.SetActive(false);
        }    
    }

    public void InitializeAttendance(int _index, int number, bool check, AttendanceManager manager)
    {
        index = _index;
        attendanceManager = manager;

        titleText.localizationName = (index + 1) + "Day";
        titleText.ReLoad();

        selectObj.SetActive(false);
        lockObj.SetActive(false);
        clearObj.SetActive(false);

        if (index == number)
        {
            if (!check)
            {
                selectObj.SetActive(true);
                lockObj.SetActive(false);
            }
            else
            {
                lockObj.SetActive(true);
            }
        }
        else
        {
            if(index > number)
            {
                lockObj.SetActive(true);
            }
            else
            {
                lockObj.SetActive(true);
                clearObj.SetActive(true);
            }
        }
    }

    public void InitializeRankEvent(int _index, int number, int check, int value, RankEventManager manager)
    {
        index = _index;
        rankEventManager = manager;

        titleText.localizationName = "RankEvent";
        titleText.plusText = " : " + number.ToString();
        titleText.ReLoad();

        selectObj.SetActive(false);
        lockObj.SetActive(false);
        clearObj.SetActive(false);

        if (index == check)
        {
            selectObj.SetActive(true);

            if(value >= number)
            {
                lockObj.SetActive(false);
            }
            else
            {
                lockObj.SetActive(true);
            }
        }
        else
        {
            if (index > check)
            {
                lockObj.SetActive(true);
            }
            else
            {
                lockObj.SetActive(true);
                clearObj.SetActive(true);
            }
        }

    }

    public void InitializePlayTime(int number, int check, PlayTimeManager manager)
    {
        playTimeManager = manager;

        titleText.localizationName = "PlayTime";

        selectObj.SetActive(false);
        lockObj.SetActive(false);
        clearObj.SetActive(false);

        switch (index)
        {
            case 0:
                titleText.plusText = " : 5";

                if(check == 0)
                {
                    selectObj.SetActive(true);

                    if (number >= 5)
                    {
                        lockObj.SetActive(false);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                    }
                }
                else
                {
                    if (index > check)
                    {
                        lockObj.SetActive(true);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                        clearObj.SetActive(true);
                    }
                }

                break;
            case 1:
                titleText.plusText = " : 10";

                if (check == 1)
                {
                    selectObj.SetActive(true);

                    if (number >= 10)
                    {
                        lockObj.SetActive(false);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                    }
                }
                else
                {
                    if (index > check)
                    {
                        lockObj.SetActive(true);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                        clearObj.SetActive(true);
                    }
                }
                break;
            case 2:
                titleText.plusText = " : 15";

                if (check == 2)
                {
                    selectObj.SetActive(true);

                    if (number >= 15)
                    {
                        lockObj.SetActive(false);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                    }
                }
                else
                {
                    if (index > check)
                    {
                        lockObj.SetActive(true);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                        clearObj.SetActive(true);
                    }
                }
                break;
            case 3:
                titleText.plusText = " : 20";

                if (check == 3)
                {
                    selectObj.SetActive(true);

                    if (number >= 20)
                    {
                        lockObj.SetActive(false);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                    }
                }
                else
                {
                    if (index > check)
                    {
                        lockObj.SetActive(true);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                        clearObj.SetActive(true);
                    }
                }
                break;
            case 4:
                titleText.plusText = " : 25";

                if (check == 4)
                {
                    selectObj.SetActive(true);

                    if (number >= 25)
                    {
                        lockObj.SetActive(false);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                    }
                }
                else
                {
                    if (index > check)
                    {
                        lockObj.SetActive(true);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                        clearObj.SetActive(true);
                    }
                }
                break;
            case 5:
                titleText.plusText = " : 30";

                if (check == 5)
                {
                    selectObj.SetActive(true);

                    if (number >= 30)
                    {
                        lockObj.SetActive(false);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                    }
                }
                else
                {
                    if (index > check)
                    {
                        lockObj.SetActive(true);
                    }
                    else
                    {
                        lockObj.SetActive(true);
                        clearObj.SetActive(true);
                    }
                }
                break;
        }

        titleText.ReLoad();
    }

    public void InitializeWelcome(int _index, int number, bool check, WelcomeManager manager)
    {
        index = _index;
        welcomeManager = manager;

        titleText.localizationName = (index + 1) + "Day";
        titleText.ReLoad();

        selectObj.SetActive(false);
        lockObj.SetActive(false);
        clearObj.SetActive(false);

        if (index == number)
        {
            if (!check)
            {
                selectObj.SetActive(true);
                lockObj.SetActive(false);
            }
            else
            {
                lockObj.SetActive(true);
            }
        }
        else
        {
            if (index > number)
            {
                lockObj.SetActive(true);
            }
            else
            {
                lockObj.SetActive(true);
                clearObj.SetActive(true);
            }
        }
    }

    public void InitializeRecipeEvent(int _index, int number, int check, int value, RecipeEventManager manager)
    {
        index = _index;
        recipeEventManager = manager;

        titleText.localizationName = "RecipeEvent";
        titleText.plusText = " : " + number.ToString();
        titleText.ReLoad();

        selectObj.SetActive(false);
        lockObj.SetActive(false);
        clearObj.SetActive(false);

        if (index == check)
        {
            selectObj.SetActive(true);

            if (value >= number)
            {
                lockObj.SetActive(false);
            }
            else
            {
                lockObj.SetActive(true);
            }
        }
        else
        {
            if (index > check)
            {
                lockObj.SetActive(true);
            }
            else
            {
                lockObj.SetActive(true);
                clearObj.SetActive(true);
            }
        }

    }

    public void InitializeLevelUpEvent(int _index, int number, int check, int value, LevelUpEventManager manager)
    {
        index = _index;
        levelUpEventManager = manager;

        titleText.localizationName = "RankEvent";
        titleText.plusText = " : " + number.ToString();
        titleText.ReLoad();

        selectObj.SetActive(false);
        lockObj.SetActive(false);
        clearObj.SetActive(false);

        if (index == check)
        {
            selectObj.SetActive(true);

            if (value >= number)
            {
                lockObj.SetActive(false);
            }
            else
            {
                lockObj.SetActive(true);
            }
        }
        else
        {
            if (index > check)
            {
                lockObj.SetActive(true);
            }
            else
            {
                lockObj.SetActive(true);
                clearObj.SetActive(true);
            }
        }

    }


    public void ReceiveButton()
    {
        if(attendanceManager != null)
        {
            if(!lockObj.activeInHierarchy)
            {
                attendanceManager.ReceiveButton(index, SuccessReceive);
            }
        }

        if(rankEventManager != null)
        {
            if (!lockObj.activeInHierarchy)
            {
                rankEventManager.ReceiveButton(SuccessReceive);
            }
        }

        if(playTimeManager != null)
        {
            if (!lockObj.activeInHierarchy)
            {
                playTimeManager.ReceiveButton(index, SuccessReceive);
            }
        }

        if (welcomeManager != null)
        {
            if (!lockObj.activeInHierarchy)
            {
                welcomeManager.ReceiveButton(index, SuccessReceive);
            }
        }

        if (recipeEventManager != null)
        {
            if (!lockObj.activeInHierarchy)
            {
                recipeEventManager.ReceiveButton(SuccessReceive);
            }
        }

        if (levelUpEventManager != null)
        {
            if (!lockObj.activeInHierarchy)
            {
                levelUpEventManager.ReceiveButton(SuccessReceive);
            }
        }
    }

    public void SuccessReceive()
    {
        selectObj.SetActive(false);
        lockObj.SetActive(true);
        clearObj.SetActive(true);
    }
}
