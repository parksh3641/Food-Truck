using Firebase.Analytics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Equip
{
    public EquipInfo[] equipInfos = new EquipInfo[10];

    private EquipInfo[] equipInfosArray = new EquipInfo[10];

    public void Initialize()
    {
        for(int i = 0; i < equipInfos.Length; i ++)
        {
            equipInfos[i].Initialize();
        }
    }

    public void SaveServerData(Equip equip)
    {
        if(equipInfos.Length >= equip.equipInfos.Length)
        {
            for (int i = 0; i < equipInfos.Length; i++)
            {
                equipInfos[i] = equip.equipInfos[i];
            }
        }
        else
        {
            EquipInfo[] equipInfosArray = new EquipInfo[equip.equipInfos.Length];

            for (int i = 0; i < equipInfos.Length; i++)
            {
                equipInfosArray[i] = equipInfos[i];
            }

            equipInfos = new EquipInfo[equip.equipInfos.Length];

            for (int i = 0; i < equipInfosArray.Length; i++)
            {
                equipInfos[i] = equipInfosArray[i];
            }
        }
    }
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

    public void Initialize()
    {
        rank = 0;

        option1 = 0;
        option1_Value = 0;

        option2 = 0;
        option2_Value = 0;

        option3 = 0;
        option3_Value = 0;

        option4 = 0;
        option4_Value = 0;
    }
}

public class EquipmentManager : MonoBehaviour
{
    public GameObject equipmentView;

    public GameObject equipInfoView;

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

    public GameObject levelUpAnim;
    public CanvasGroup canvasGroup;

    private int index = -1;

    private float duration = 0.7f;
    private float currentTime;


    public EffectManager effectManager;
    public InventoryManager inventoryManager;


    private void Awake()
    {
        equipmentView.SetActive(false);
        equipInfoView.SetActive(false);

        mainAlarm.SetActive(true);

        equipAlarm.SetActive(true);
        effectAlarm.SetActive(false);
        inventoryAlarm.SetActive(true);

        levelUpAnim.SetActive(false);

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

            FirebaseAnalytics.LogEvent("Open_Equipment");
        }
        else
        {
            GameManager.instance.CheckPercent();

            equipmentView.SetActive(false);
        }
    }

    public void OpenInfoView()
    {
        if (!equipInfoView.activeInHierarchy)
        {
            equipInfoView.SetActive(true);

            FirebaseAnalytics.LogEvent("Open_EquipmentInfo");
        }
        else
        {
            equipInfoView.SetActive(false);
        }
    }

    public void ChangeTopToggle(int number)
    {
        if (number == 1 || number == 2)
        {
            NotionManager.instance.UseNotion(NotionType.ComingSoon);
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            return;
        }

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

                break;
            case 2:

                break;
            case 3:
                inventoryAlarm.SetActive(false);
                inventoryManager.OpenInventoryView();

                break;
        }
    }

    public void Initialize()
    {
        for(int i = 0; i < equipContents.Length; i ++)
        {
            equipContents[i].Initialize(i, this);
        }
    }

    public void LevelIUpAnimation()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        levelUpAnim.SetActive(true);
        canvasGroup.alpha = 0;

        currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        yield return new WaitForSeconds(1f); // Wait for 1 second

        currentTime = 0f; // Reset time for the next loop

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        levelUpAnim.SetActive(false);
    }
}
