using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject skillView;

    public GameObject lockedObj;

    public GameObject alarm;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;

    public GameObject[] skillArray;

    public RectTransform skillGrid;
    public RectTransform skillGrid2;

    private int index = -1;

    public SkillContent[] skillContents;


    private void Awake()
    {
        skillView.SetActive(false);

        skillGrid.anchoredPosition = new Vector2(0, -9999);
        skillGrid2.anchoredPosition = new Vector2(0, -9999);

        index = -1;

        alarm.SetActive(true);
    }

    public void OpenSkillView()
    {
        if (!skillView.activeInHierarchy && !lockedObj.activeInHierarchy)
        {
            skillView.SetActive(true);

            ChangeTopToggle(0);

            for (int i = 0; i < skillContents.Length; i++)
            {
                skillContents[i].Initialize();
            }

            skillContents[13].gameObject.SetActive(false);

            alarm.SetActive(false);

            FirebaseAnalytics.LogEvent("OpenSkill");
        }
        else
        {
            skillView.SetActive(false);
        }
    }

    public void ChangeTopToggle(int number)
    {
        if (index == number) return;

        index = number;

        for (int i = 0; i < topMenuImgArray.Length; i++)
        {
            topMenuImgArray[i].sprite = topMenuSpriteArray[0];
            skillArray[i].gameObject.SetActive(false);
        }

        topMenuImgArray[number].sprite = topMenuSpriteArray[1];
        skillArray[number].gameObject.SetActive(true);
    }
}

