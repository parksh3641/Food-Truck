using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject skillView;

    public LocalizationContent titleText;

    public RectTransform skillGrid;

    public SkillContent[] skillContents;


    private void Awake()
    {
        skillView.SetActive(false);

        skillGrid.anchoredPosition = new Vector2(0, -9999);
    }


    public void OpenSkillView()
    {
        if (!skillView.activeInHierarchy)
        {
            skillView.SetActive(true);

            titleText.localizationName = "Skill";
            titleText.ReLoad();

            Initialize(0);

            FirebaseAnalytics.LogEvent("OpenSkill");
        }
        else
        {
            skillView.SetActive(false);
        }
    }

    public void OpenSpeicalSkillView()
    {
        if (!skillView.activeInHierarchy)
        {
            skillView.SetActive(true);

            titleText.localizationName = "SpecialLabs";
            titleText.ReLoad();

            Initialize(1);

            FirebaseAnalytics.LogEvent("OpenSkill");
        }
        else
        {
            skillView.SetActive(false);
        }
    }

    void Initialize(int number)
    {
        for(int i = 0; i < skillContents.Length; i ++)
        {
            skillContents[i].Initialize();
            skillContents[i].gameObject.SetActive(false);
        }

        if(number == 0)
        {
            skillContents[0].gameObject.SetActive(true);
            skillContents[1].gameObject.SetActive(true);
            skillContents[2].gameObject.SetActive(true);
            skillContents[3].gameObject.SetActive(true);
            skillContents[4].gameObject.SetActive(true);
            skillContents[5].gameObject.SetActive(true);
        }
        else
        {
            skillContents[6].gameObject.SetActive(true);
            skillContents[7].gameObject.SetActive(true);
            skillContents[8].gameObject.SetActive(true);
            skillContents[9].gameObject.SetActive(true);
            skillContents[10].gameObject.SetActive(true);
        }

        skillGrid.anchoredPosition = new Vector2(0, -9999);
    }
}
