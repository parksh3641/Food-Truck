using Firebase.Analytics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject skillView;

    public Text countText;

    public GameObject lockedObj;

    public GameObject alarm;
    public GameObject ingameAlarm;

    public Text challengePointText;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;

    public GameObject[] skillArray;
    public RectTransform[] skillGrid;

    private int index = -1;
    private int challengePoint = 0;

    public SkillContent[] skillContents;

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        skillView.SetActive(false);

        for(int i = 0; i < skillGrid.Length; i ++)
        {
            skillGrid[i].anchoredPosition = new Vector2(0, -9999);
        }

        index = -1;

        alarm.SetActive(true);
        ingameAlarm.SetActive(true);
    }

    public void OpenSkillView()
    {
        if (!skillView.activeInHierarchy && !lockedObj.activeInHierarchy)
        {
            skillView.SetActive(true);

            alarm.SetActive(false);
            ingameAlarm.SetActive(false);

            GameManager.instance.RenewalVC();

            countText.text = playerDataBase.SkillTicket.ToString();

            if(index == -1)
            {
                ChangeTopToggle(0);
            }

            Initialize();

            FirebaseAnalytics.LogEvent("OpenReceipe");
        }
        else
        {
            skillView.SetActive(false);
        }
    }

    public void Initialize()
    {
        countText.text = playerDataBase.SkillTicket.ToString();

        for (int i = 0; i < skillContents.Length; i++)
        {
            if(skillContents[i].gameObject.activeInHierarchy)
            {
                skillContents[i].Checking();
            }
        }

        challengePointText.text = playerDataBase.ChallengePoint.ToString();
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

        if(number == 2)
        {
            if(playerDataBase.LockTutorial < 7)
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.UnLockedNotion4);
                return;
            }
        }

        for (int i = 0; i < skillContents.Length; i++)
        {
            if (skillContents[i].gameObject.activeInHierarchy)
            {
                skillContents[i].Initialize(this);
            }
        }

        Initialize();
    }

    public void OpenSkillTicketInfo()
    {
        ReceiveInfoManager.instance.OpenReceiveInfo(RewardType.SkillTicket, 2);
    }

    public void OpenCPInfo()
    {
        ReceiveInfoManager.instance.OpenReceiveInfo(RewardType.ChallengePoint, 2);
    }
}

