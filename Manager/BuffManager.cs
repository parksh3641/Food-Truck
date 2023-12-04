using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    public GameObject buffView;

    public Text buffTicketsText;

    public LocalizationContent infoText;

    public GameObject buff1Obj;
    public Text buff1Text;

    public GameObject buff2Obj;
    public Text buff2Text;

    public GameObject buff3Obj;
    public Text buff3Text;

    private bool buff1;
    private bool buff2;
    private bool buff3;

    private int time = 600;

    private int buff1Time = 0;
    private int buff2Time = 0;
    private int buff3Time = 0;

    private int index = 0;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    public GameManager gameManager;



    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        buff1Obj.SetActive(false);
        buff2Obj.SetActive(false);
        buff3Obj.SetActive(false);

        buff1 = false;
        buff2 = false;
        buff3 = false;

        buffView.SetActive(false);
    }


    public void OpenBuffView(int number)
    {
        if (!buffView.activeInHierarchy)
        {
            buffView.SetActive(true);

            index = number;

            buffTicketsText.text = LocalizationManager.instance.GetString("BuffTicket") + "\n<size=10>" + 
                LocalizationManager.instance.GetString("Hold") + " : " + playerDataBase.BuffTickets + "</size>";

            switch(number)
            {
                case 0:
                    infoText.localizationName = "AdReward_Buff1";
                    break;
                case 1:
                    infoText.localizationName = "AdReward_Buff2";
                    break;
                case 2:
                    infoText.localizationName = "AdReward_Buff3";
                    break;
            }

            infoText.ReLoad();

            GameStateManager.instance.Pause = true;
        }
        else
        {
            buffView.SetActive(false);

            GameStateManager.instance.Pause = false;
        }
    }

    public void UseBuffTicket()
    {
        if(playerDataBase.BuffTickets <= 0)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowItemNotion);
            return;
        }

        playerDataBase.BuffTickets -= 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuffTickets", playerDataBase.BuffTickets);

        BuffON();

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.UseItem);
    }

    public void WatchAd()
    {
        switch(index)
        {
            case 0:
                GoogleAdsManager.instance.admobReward_SellPriceTime.ShowAd(3);
                break;
            case 1:
                GoogleAdsManager.instance.admobReward_DefDestroyTime.ShowAd(3);
                break;
            case 2:
                GoogleAdsManager.instance.admobReward_Buff3.ShowAd(3);
                break;
        }
    }

    public void BuffON()
    {
        buffView.SetActive(false);

        switch(index)
        {
            case 0:
                if (buff1) return;

                buff1 = true;
                buff1Time = time;

                buff1Obj.SetActive(true);
                StartCoroutine(Buff1Coroution());

                gameManager.OnBuff(0);

                FirebaseAnalytics.LogEvent("UseBuff1");
                break;
            case 1:
                if (buff2) return;

                buff2 = true;
                buff2Time = time;

                buff2Obj.SetActive(true);
                StartCoroutine(Buff2Coroution());

                gameManager.OnBuff(1);

                FirebaseAnalytics.LogEvent("UseBuff2");
                break;
            case 2:
                if (buff3) return;

                buff3 = true;
                buff3Time = time;

                buff3Obj.SetActive(true);
                StartCoroutine(Buff3Coroution());

                gameManager.OnBuff(2);

                FirebaseAnalytics.LogEvent("UseBuff3");
                break;
        }

        playerDataBase.BuffCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuffCount", playerDataBase.BuffCount);
        GameStateManager.instance.Pause = false;
    }

    public void SuccessWatchAd()
    {
        BuffON();

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);
    }

    IEnumerator Buff1Coroution()
    {
        if(buff1Time > 0)
        {
            if(!GameStateManager.instance.Pause)
            {
                buff1Time -= 1;
            }
        }
        else
        {
            buff1 = false;

            buff1Obj.SetActive(false);

            gameManager.OffBuff(0);

            yield break;
        }

        buff1Text.text = (buff1Time / 60 % 60).ToString("D2") + ":" + (buff1Time % 60).ToString("D2");

        yield return waitForSeconds;

        StartCoroutine(Buff1Coroution());
    }

    IEnumerator Buff2Coroution()
    {
        if (buff2Time > 0)
        {
            if (!GameStateManager.instance.Pause)
            {
                buff2Time -= 1;
            }
        }
        else
        {
            buff2 = false;

            buff2Obj.SetActive(false);

            gameManager.OffBuff(1);

            yield break;
        }

        buff2Text.text = (buff2Time / 60 % 60).ToString("D2") + ":" + (buff2Time % 60).ToString("D2");

        yield return waitForSeconds;

        StartCoroutine(Buff2Coroution());
    }

    IEnumerator Buff3Coroution()
    {
        if (buff3Time > 0)
        {
            if (!GameStateManager.instance.Pause)
            {
                buff3Time -= 1;
            }
        }
        else
        {
            buff3 = false;

            buff3Obj.SetActive(false);

            gameManager.OffBuff(2);

            yield break;
        }

        buff3Text.text = (buff3Time / 60 % 60).ToString("D2") + ":" + (buff3Time % 60).ToString("D2");

        yield return waitForSeconds;

        StartCoroutine(Buff3Coroution());
    }
}
