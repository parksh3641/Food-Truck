using Firebase.Analytics;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{
    public GameObject newsView;

    public GameObject newsIcon;

    public GameObject alarm;
    public GameObject patchAlarm;

    public Text versionText;

    public NewsContent newsContent;
    public RectTransform newsContentTransform;

    [Title("Read More")]
    public GameObject infoView;
    public Text infoTitleText;
    public Text infoBodyText;

    [Space]
    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;

    public GameObject checkMark;

    public GameObject noticeView;
    public GameObject patchNoteView;

    private int countNews = 0;
    private int topNumber = -1;
    public bool isDelay = false;

    private bool first = false;

    public List<NewsContent> newsContentList = new List<NewsContent>();
    private List<TitleNewsItem> newsInfoList = new List<TitleNewsItem>();

    public WelcomeManager welcomeManager;
    public AttendanceManager attendanceManager;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        newsView.SetActive(false);
        infoView.SetActive(false);

        alarm.SetActive(true);
        patchAlarm.SetActive(true);

        newsContentList.Clear();

        for(int i = 0; i < 10; i ++)
        {
            NewsContent monster = Instantiate(newsContent);
            monster.transform.SetParent(newsContentTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;

            monster.gameObject.SetActive(false);

            newsContentList.Add(monster);
        }

        first = false;

        newsIcon.SetActive(true);

        versionText.text = "v" + Application.version;
    }

    public void Initialize()
    {
        if (playerDataBase.InGameTutorial == 1 && !GameStateManager.instance.HideNotice)
        {
            first = true;

            OpenNews();
        }

        if (GameStateManager.instance.StoreType == StoreType.OneStore)
        {
            newsIcon.SetActive(false);
        }
    }

    public void ChangeTopToggle(int number)
    {
        if (topNumber == number) return;

        if (isDelay) return;

        topNumber = number;

        for (int i = 0; i < topMenuImgArray.Length; i++)
        {
            topMenuImgArray[i].sprite = topMenuSpriteArray[0];
        }

        topMenuImgArray[number].sprite = topMenuSpriteArray[1];

        noticeView.SetActive(false);
        patchNoteView.SetActive(false);

        for (int i = 0; i < newsContentList.Count; i++)
        {
            newsContentList[i].gameObject.SetActive(false);
        }

        switch (number)
        {
            case 0:
                noticeView.SetActive(true);

                alarm.SetActive(false);

                FirebaseAnalytics.LogEvent("Open_Notice");
                break;
            case 1:
                patchNoteView.SetActive(true);

                patchAlarm.SetActive(false);

                isDelay = true;
                PlayfabManager.instance.ReadTitleNews(ReadTitleNews);

                FirebaseAnalytics.LogEvent("Open_PatchNote");
                break;
        }
    }

    public void OpenNews()
    {
        if (!newsView.activeSelf)
        {
            newsView.SetActive(true);

            if(topNumber == -1)
            {
                isDelay = false;

                ChangeTopToggle(0);
            }

            checkMark.SetActive(GameStateManager.instance.HideNotice);
        }
        else
        {
            newsView.SetActive(false);

            if (first)
            {
                first = false;

                if (!playerDataBase.WelcomeCheck && playerDataBase.WelcomeCount < 7)
                {
                    welcomeManager.first = true;
                    welcomeManager.OpenWelcomeView();
                }
                else if(!playerDataBase.AttendanceCheck)
                {
                    attendanceManager.first = true;
                    attendanceManager.OpenAttendanceView();
                }
            }
        }
    }

    public void ReadTitleNews(List<TitleNewsItem> item)
    {
        countNews = item.Count - 1;

        for (int i = 0; i < newsContentList.Count; i++)
        {
            newsContentList[i].gameObject.SetActive(false);
        }

        newsInfoList.Clear();

        for (int i = 0; i < item.Count; i++)
        {
            newsContentList[i].InitState(i, item[i].Title, item[i].Timestamp, this);
            newsContentList[i].newsManager = this;
            newsInfoList.Add(item[i]);
            newsContentList[i].gameObject.SetActive(true);
        }

        newsContentTransform.anchoredPosition = new Vector2(0, -9999);

        isDelay = false;
    }

    public void OpenReadMore(int number, string title)
    {
        infoView.SetActive(true);

        infoTitleText.text = title;
        infoBodyText.text = newsInfoList[number].Body;
        //infoBodyText.rectTransform.anchoredPosition = new Vector3(0, -305f, 0);
    }

    public void CloseReadMore()
    {
        infoView.SetActive(false);
    }

    public void OpenDiscord()
    {
        Application.OpenURL("https://discord.gg/yEV33a8ajy");

        FirebaseAnalytics.LogEvent("Open_Discord");
    }

    public void OpenKakaoTalk()
    {
        Application.OpenURL("https://open.kakao.com/o/gnjyGphg");

        FirebaseAnalytics.LogEvent("Open_KakaoTalk");
    }

    public void HideCheck()
    {
        if (GameStateManager.instance.HideNotice)
        {
            GameStateManager.instance.HideNotice = false;

            checkMark.SetActive(false);
        }
        else
        {
            GameStateManager.instance.HideNotice = true;

            checkMark.SetActive(true);
        }
    }
}
