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

    public bool talkSkip = false;

    string str = "";

    public NickNameManager nameManager;

    WaitForSeconds talkDelay = new WaitForSeconds(0.04f);

    private void Awake()
    {
        tutorialView.SetActive(false);
    }

    public void TutorialStart()
    {
        Invoke("Wait", 2f);
    }

    void Wait()
    {
        tutorialView.SetActive(true);

        talkIndex = 0;
        Initialize(talkIndex);
    }

    void Initialize(int number)
    {
        nextText.enabled = false;

        talkSkip = false;

        Debug.Log(number);

        switch (number)
        {
            case 4:
                tutorialView.SetActive(false);

                if (GameStateManager.instance.NickName.Length > 15)
                {
                    nameManager.nickNameView.SetActive(true);
                }

                GameStateManager.instance.Tutorial = true;
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
            talkIndex++;
            Initialize(talkIndex);

            SoundManager.instance.PlaySFX(GameSfxType.Click);
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

        nextText.enabled = true;
    }
}
