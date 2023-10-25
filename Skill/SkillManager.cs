using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject skillView;

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

            Initialize();

            FirebaseAnalytics.LogEvent("OpenSkill");
        }
        else
        {
            skillView.SetActive(false);
        }
    }

    void Initialize()
    {
        for(int i = 0; i < skillContents.Length; i ++)
        {
            skillContents[i].Initialize();
        }
    }
}
