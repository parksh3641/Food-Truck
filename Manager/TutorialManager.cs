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

    string str = "";

    public NickNameManager nameManager;

    WaitForSeconds talkDelay = new WaitForSeconds(0.04f);
    WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        tutorialView.SetActive(false);
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
    public void Next0()
    {
        tutorialView.SetActive(true);

        talkIndex = 0;
        Initialize(talkIndex);
    }

    [Button]
    public void Next1()
    {
        tutorialView.SetActive(true);

        GameStateManager.instance.FirstSuccess = true;

        talkIndex = 6;
        Initialize(talkIndex);
    }

    [Button]
    public void Next2()
    {
        tutorialView.SetActive(true);

        GameStateManager.instance.FirstFail = true;

        talkIndex = 8;
        Initialize(talkIndex);
    }

    [Button]
    public void Next3()
    {
        tutorialView.SetActive(true);

        GameStateManager.instance.DestroyCount = 0;

        talkIndex = 10;
        Initialize(talkIndex);
    }

    [Button]
    public void Reincarnation()
    {
        tutorialView.SetActive(true);

        talkIndex = 12;
        Initialize(talkIndex);
    }

    void Initialize(int number)
    {
        nextText.enabled = false;
        isSkip = false;

        talkSkip = false;

        Debug.Log(number);

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

        StartCoroutine(Talking(str));

        //SoundManager.instance.PlaySFX(GameSfxType.TalkMy);
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
