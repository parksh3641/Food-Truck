using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialView;

    public Text npcText;
    public Text talkText;
    public Text nextText;

    public int talkIndex = 0;
    public int talkReplace = 0;

    private bool talkSkip = false;
    private bool isSkip = false;
    private bool isQuiz = false;

    private string str = "";

    public NickNameManager nameManager;

    WaitForSeconds talkDelay = new WaitForSeconds(0.04f);
    WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        tutorialView.SetActive(false);

        isQuiz = false;
    }

    public void TutorialStart()
    {
        Invoke("Wait", 1f);
    }


    void Wait()
    {
        tutorialView.SetActive(true);

        talkIndex = 0;
        Initialize(talkIndex);
    }

    [Button]
    public void Next0() //╬х ╬╡юс
    {
        tutorialView.SetActive(true);

        isQuiz = false;

        talkIndex = 0;
        Initialize(talkIndex);
    }

    [Button]
    public void Next1()
    {
        tutorialView.SetActive(true);

        isQuiz = false;

        GameStateManager.instance.FirstSuccess = true;

        talkIndex = 6;
        Initialize(talkIndex);
    }

    [Button]
    public void Next2()
    {
        tutorialView.SetActive(true);

        isQuiz = false;

        GameStateManager.instance.FirstFail = true;

        talkIndex = 8;
        Initialize(talkIndex);
    }

    [Button]
    public void Next3()
    {
        tutorialView.SetActive(true);

        isQuiz = false;

        GameStateManager.instance.FirstDestory = true;

        talkIndex = 10;
        Initialize(talkIndex);
    }

    [Button]
    public void Reincarnation()
    {
        tutorialView.SetActive(true);

        isQuiz = false;

        talkIndex = 12;
        Initialize(talkIndex);
    }

    [Button]
    public void TodayQuizStart()
    {
        tutorialView.SetActive(true);

        isQuiz = true;

        talkIndex = 15;
        Initialize(talkIndex);
    }

    void Initialize(int number)
    {
        nextText.enabled = false;
        isSkip = false;

        talkSkip = false;

        if (isQuiz)
        {
            switch (number)
            {
                case 15:
                    str = LocalizationManager.instance.GetString("Tutorial_Quiz");
                    break;
                case 16:
                    str = LocalizationManager.instance.GetString("Quiz" + Random.Range(1, 11));
                    break;
                case 17:
                    str = LocalizationManager.instance.GetString("Tutorial_Quiz_End");
                    break;
                case 18:
                    tutorialView.SetActive(false);
                    break;
            }
        }
        else
        {
            switch (number)
            {
                case 3:
                    tutorialView.SetActive(false);

                    if (playerDataBase.ChangeNicknameCount == 0)
                    {
                        nameManager.OpenFreeNickName();
                        GameManager.instance.FirstReward();
                    }

                    break;
                case 7:
                    tutorialView.SetActive(false);
                    break;
                case 9:
                    tutorialView.SetActive(false);
                    break;
                case 11:
                    tutorialView.SetActive(false);
                    break;
                case 13:
                    tutorialView.SetActive(false);
                    break;
            }

            str = LocalizationManager.instance.GetString("Tutorial_" + (number + 1).ToString()).Replace("%%",
GameStateManager.instance.NickName);
        }

        StartCoroutine(Talking(str));
    }

    public void NextButton()
    {
        if (!talkSkip)
        {
            talkSkip = true;
        }
        else
        {
            if (isSkip)
            {
                talkIndex++;
                Initialize(talkIndex);

                SoundManager.instance.PlaySFX(GameSfxType.Click);
            }
        }
    }

    IEnumerator Talking(string talk)
    {
        talkText.text = "";

        talkReplace = 0;

        string[] replaceTextStr = new string[talk.Length];

        for (int i = 0; i < replaceTextStr.Length; i++)
        {
            replaceTextStr[i] = talk.Substring(i, 1);
        }

        while (!talkSkip && talkReplace < replaceTextStr.Length)
        {
            talkText.text += replaceTextStr[talkReplace];

            talkReplace++;

            yield return talkDelay;
        }

        talkText.text = talk;

        yield return waitForSeconds;

        nextText.enabled = true;
        isSkip = true;
    }
}
