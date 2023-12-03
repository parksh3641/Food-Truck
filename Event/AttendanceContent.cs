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

    private void Awake()
    {
        for(int i = 0; i < receiveContent.Length; i ++)
        {
            receiveContent[i].gameObject.SetActive(false);
        }    
    }

    public void Initialize(int number, bool check, AttendanceManager manager)
    {
        attendanceManager = manager;

        titleText.localizationName = (index + 1) + "Day";
        titleText.ReLoad();

        selectObj.SetActive(false);
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

    public void ReceiveButton()
    {
        attendanceManager.ReceiveButton(index, SuccessReceive);
    }

    public void SuccessReceive()
    {
        selectObj.SetActive(false);
        lockObj.SetActive(true);
        clearObj.SetActive(true);
    }
}
