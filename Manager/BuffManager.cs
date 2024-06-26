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

    public ButtonScaleAnimation buff1Anim;
    public GameObject buff1Obj;
    public Text buff1Text;

    public ButtonScaleAnimation buff2Anim;
    public GameObject buff2Obj;
    public Text buff2Text;

    public ButtonScaleAnimation buff3Anim;
    public GameObject buff3Obj;
    public Text buff3Text;

    public ButtonScaleAnimation buff4Anim;
    public GameObject buff4Obj;
    public Text buff4Text;

    private bool buff1;
    private bool buff2;
    private bool buff3;
    private bool buff4;

    private int time = 600;

    private int buff1Time = 0;
    private int buff2Time = 0;
    private int buff3Time = 0;
    private int buff4Time = 0;

    private int index = 0;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1);

    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        buff1Obj.SetActive(false);
        buff2Obj.SetActive(false);
        buff3Obj.SetActive(false);
        buff4Obj.SetActive(false);

        buff1 = false;
        buff2 = false;
        buff3 = false;
        buff4 = false;

        buffView.SetActive(false);
    }


    public void OpenBuffView(int number)
    {
        if (!buffView.activeInHierarchy && !playerDataBase.RemoveAds)
        {
            buffView.SetActive(true);

            index = number;

            buffTicketsText.text = LocalizationManager.instance.GetString("BuffTicket") + "\n<size=10>" + 
                LocalizationManager.instance.GetString("Hold") + " : " + playerDataBase.BuffTicket + "</size>";

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
                case 3:
                    infoText.localizationName = "AdReward_Buff4";
                    break;
            }

            infoText.ReLoad();
        }
        else
        {
            if(playerDataBase.RemoveAds)
            {
                switch (number)
                {
                    case 0:
                        NotionManager.instance.UseNotion(Color.green, LocalizationManager.instance.GetString("AdReward_Buff1_Info"));
                        break;
                    case 1:
                        NotionManager.instance.UseNotion(Color.green, LocalizationManager.instance.GetString("AdReward_Buff2_Info"));
                        break;
                    case 2:
                        NotionManager.instance.UseNotion(Color.green, LocalizationManager.instance.GetString("AdReward_Buff3_Info"));
                        break;
                    case 3:

                        break;
                }
            }

            buffView.SetActive(false);
        }
    }

    public void UseBuffTicket()
    {
        if(playerDataBase.BuffTicket <= 0)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowItemNotion);

            buffView.SetActive(false);
            return;
        }

        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        playerDataBase.BuffTicket -= 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuffTickets", playerDataBase.BuffTicket);

        BuffON();

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.UseItem);
    }

    public void WatchAd()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        switch (index)
        {
            case 0:
                GoogleAdsManager.instance.admobReward_Buff1.ShowAd(3);
                break;
            case 1:
                GoogleAdsManager.instance.admobReward_Buff2.ShowAd(3);
                break;
            case 2:
                GoogleAdsManager.instance.admobReward_Buff3.ShowAd(3);
                break;
            case 3:
                GoogleAdsManager.instance.admobReward_Buff4.ShowAd(3);
                break;
        }
    }

    public void BuffON()
    {
        buffView.SetActive(false);

        playerDataBase.BuffCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuffCount", playerDataBase.BuffCount);

        switch (index)
        {
            case 0:
                if (buff1) return;

                buff1Anim.StopAnim();

                buff1 = true;
                buff1Time = time;

                buff1Obj.SetActive(true);
                StartCoroutine(Buff1Coroution());

                GameManager.instance.OnBuff(0);

                FirebaseAnalytics.LogEvent("Use_Buff1");
                break;
            case 1:
                if (buff2) return;

                buff2Anim.StopAnim();

                buff2 = true;
                buff2Time = time;

                buff2Obj.SetActive(true);
                StartCoroutine(Buff2Coroution());

                GameManager.instance.OnBuff(1);

                FirebaseAnalytics.LogEvent("Use_Buff2");
                break;
            case 2:
                if (buff3) return;

                buff3Anim.StopAnim();

                buff3 = true;
                buff3Time = time;

                buff3Obj.SetActive(true);
                StartCoroutine(Buff3Coroution());

                GameManager.instance.OnBuff(2);

                FirebaseAnalytics.LogEvent("Use_Buff3");
                break;
            case 3:
                if (buff4) return;

                buff4Anim.StopAnim();

                buff4 = true;
                buff4Time = 300;

                buff4Obj.SetActive(true);
                StartCoroutine(Buff4Coroution());

                GameManager.instance.OnBuff(3);

                FirebaseAnalytics.LogEvent("Use_Buff4");
                break;
        }
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
            buff1Time -= 1;
        }
        else
        {
            buff1 = false;

            buff1Anim.PlayAnim();

            buff1Obj.SetActive(false);

            GameManager.instance.OffBuff(0);

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
            buff2Time -= 1;
        }
        else
        {
            buff2 = false;

            buff2Anim.PlayAnim();

            buff2Obj.SetActive(false);

            GameManager.instance.OffBuff(1);

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
            buff3Time -= 1;
        }
        else
        {
            buff3 = false;

            buff3Anim.PlayAnim();

            buff3Obj.SetActive(false);

            GameManager.instance.OffBuff(2);

            yield break;
        }

        buff3Text.text = (buff3Time / 60 % 60).ToString("D2") + ":" + (buff3Time % 60).ToString("D2");

        yield return waitForSeconds;

        StartCoroutine(Buff3Coroution());
    }

    IEnumerator Buff4Coroution()
    {
        if (buff4Time > 0)
        {
            buff4Time -= 1;
        }
        else
        {
            buff4 = false;

            buff4Anim.PlayAnim();

            buff4Obj.SetActive(false);

            GameManager.instance.OffBuff(3);

            yield break;
        }

        buff4Text.text = (buff4Time / 60 % 60).ToString("D2") + ":" + (buff4Time % 60).ToString("D2");

        yield return waitForSeconds;

        StartCoroutine(Buff4Coroution());
    }

    public void RemoveAdsBuff()
    {
        StopAllCoroutines();

        buff1Anim.StopAnim();
        buff2Anim.StopAnim();
        buff3Anim.StopAnim();
    }
}
