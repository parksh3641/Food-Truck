using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Equip
{
    public EquipInfo[] equipInfos = new EquipInfo[6];
}


[System.Serializable]
public class EquipInfo
{
    public int rank = 0;

    public int option1 = 0;
    public float option1_Value = 0;

    public int option2 = 0;
    public float option2_Value = 0;

    public int option3 = 0;
    public float option3_Value = 0;

    public int option4 = 0;
    public float option4_Value = 0;
}

public class EquipmentManager : MonoBehaviour
{
    public GameObject equipmentView;

    public GameObject mainAlarm;

    public GameObject equipAlarm;
    public GameObject effectAlarm;
    public GameObject inventoryAlarm;

    public RectTransform rectTransform;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;
    public GameObject[] contentArray;

    public EquipContent[] equipContents;


    public EffectManager effectManager;
    public InventoryManager inventoryManager;

    private int index = -1;


    private void Awake()
    {
        equipmentView.SetActive(false);

        mainAlarm.SetActive(true);

        equipAlarm.SetActive(true);
        effectAlarm.SetActive(true);
        inventoryAlarm.SetActive(true);

        rectTransform.anchoredPosition = new Vector2(0, -9999);
    }


    public void OpenView()
    {
        if(!equipmentView.activeInHierarchy)
        {
            equipmentView.SetActive(true);

            mainAlarm.SetActive(false);

            if (index == -1)
            {
                ChangeTopToggle(0);
            }
        }
        else
        {
            equipmentView.SetActive(false);
        }
    }

    public void ChangeTopToggle(int number)
    {
        if (index == number) return;

        index = number;

        for (int i = 0; i < topMenuImgArray.Length; i++)
        {
            topMenuImgArray[i].sprite = topMenuSpriteArray[0];
            contentArray[i].gameObject.SetActive(false);
        }

        topMenuImgArray[number].sprite = topMenuSpriteArray[1];
        contentArray[number].gameObject.SetActive(true);

        switch (number)
        {
            case 0:
                equipAlarm.SetActive(false);

                Initialize();

                break;
            case 1:
                effectAlarm.SetActive(false);

                break;
            case 2:
                inventoryAlarm.SetActive(false);

                inventoryManager.OpenInventoryView();

                break;
        }
    }

    public void Initialize()
    {
        for(int i = 0; i < equipContents.Length; i ++)
        {
            equipContents[i].Initialize(i);
        }
    }
}
