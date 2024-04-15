using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifticonContent : MonoBehaviour
{
    public GifticonType gifticonType = GifticonType.Gifticon_1;

    public Image icon;

    public Text titleText;

    public Text infoText;

    public Text numberText;

    public Text needText;

    public GameObject lockedObj;

    private int need = 0;
    private int max = 100;

    public GameObject checkEvent;
    public Text checkEventText;
    public GameObject checkEventButton;



    GifticonManager gifticonManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        lockedObj.SetActive(true);

        checkEvent.SetActive(false);
        checkEventButton.SetActive(false);
    }

    public void Initialize(GifticonType type, GifticonManager manager)
    {
        gifticonType = type;
        gifticonManager = manager;

        titleText.text = LocalizationManager.instance.GetString(type.ToString());

        infoText.text = LocalizationManager.instance.GetString("TotalNumber") + " : x";

        numberText.text = LocalizationManager.instance.GetString("EnterNumber") + " : ";

        switch (gifticonType)
        {
            case GifticonType.Gifticon_1:
                infoText.text += "1";

                numberText.text += playerDataBase.EventEnter1 + "/" + max;

                need = 500;
                break;
            case GifticonType.Gifticon_2:
                infoText.text += "2";

                numberText.text += playerDataBase.EventEnter2 + "/" + max;

                need = 175;
                break;
            case GifticonType.Gifticon_3:
                infoText.text += "2";

                numberText.text += playerDataBase.EventEnter3 + "/" + max;

                need = 80;
                break;
            case GifticonType.Gifticon_4:
                infoText.text += "5";

                numberText.text += playerDataBase.EventEnter4 + "/" + max;

                need = 45;
                break;
        }

        icon.sprite = imageDataBase.GetGifticonArray(type);

        needText.text = "x " + need.ToString();

        lockedObj.SetActive(true);

        if(playerDataBase.EventTicket >= need)
        {
            lockedObj.SetActive(false);
        }
    }

    public void EnterButton()    //응모 하기
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        if(!gifticonManager.CheckDate())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.EndEvent);
            return;
        }

        if(playerDataBase.EventTicket < need)
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.LowEventTicket);
            return;
        }

        playerDataBase.EventTicket -= need;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventTicket", playerDataBase.EventTicket);

        switch (gifticonType)
        {
            case GifticonType.Gifticon_1:
                if(playerDataBase.EventEnter1 + 1 > 100)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxEnterEventTicket);

                    return;
                }

                playerDataBase.EventEnter1 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventEnter1", playerDataBase.EventEnter1);
                break;
            case GifticonType.Gifticon_2:
                if (playerDataBase.EventEnter2 + 1 > 100)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxEnterEventTicket);

                    return;
                }

                playerDataBase.EventEnter2 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventEnter2", playerDataBase.EventEnter2);
                break;
            case GifticonType.Gifticon_3:
                if (playerDataBase.EventEnter3 + 1 > 100)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxEnterEventTicket);

                    return;
                }

                playerDataBase.EventEnter3 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventEnter3", playerDataBase.EventEnter3);
                break;
            case GifticonType.Gifticon_4:
                if (playerDataBase.EventEnter4 + 1 > 100)
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.MaxEnterEventTicket);

                    return;
                }

                playerDataBase.EventEnter4 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventEnter4", playerDataBase.EventEnter4);
                break;
        }

        playerDataBase.EventTicket -= need;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventTicket", playerDataBase.EventTicket);

        FirebaseAnalytics.LogEvent("Enter_" + gifticonType.ToString());

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessEnter);

        Initialize(gifticonType, gifticonManager);

        gifticonManager.Initialize();
    }

    public void CheckingEvent()
    {
        checkEvent.SetActive(true);
        checkEventButton.SetActive(false);

        checkEventText.text = LocalizationManager.instance.GetString("DrawingWinner");

        switch (gifticonType)
        {
            case GifticonType.Gifticon_1:
                if(playerDataBase.EventEnter1 == 9999)
                {
                    checkEventText.text = LocalizationManager.instance.GetString("EnterWin");

                    checkEventButton.SetActive(true);
                }
                else if(playerDataBase.EventEnter1 == -9999)
                {
                    checkEventText.text = LocalizationManager.instance.GetString("EnterLose");
                }
                break;
            case GifticonType.Gifticon_2:
                if (playerDataBase.EventEnter2 == 9999)
                {
                    checkEventText.text = LocalizationManager.instance.GetString("EnterWin");

                    checkEventButton.SetActive(true);
                }
                else if (playerDataBase.EventEnter2 == -9999)
                {
                    checkEventText.text = LocalizationManager.instance.GetString("EnterLose");
                }
                break;
            case GifticonType.Gifticon_3:
                if (playerDataBase.EventEnter3 == 9999)
                {
                    checkEventText.text = LocalizationManager.instance.GetString("EnterWin");

                    checkEventButton.SetActive(true);
                }
                else if (playerDataBase.EventEnter3 == -9999)
                {
                    checkEventText.text = LocalizationManager.instance.GetString("EnterLose");
                }
                break;
            case GifticonType.Gifticon_4:
                if (playerDataBase.EventEnter4 == 9999)
                {
                    checkEventText.text = LocalizationManager.instance.GetString("EnterWin");

                    checkEventButton.SetActive(true);
                }
                else if (playerDataBase.EventEnter4 == -9999)
                {
                    checkEventText.text = LocalizationManager.instance.GetString("EnterLose");
                }
                break;
        }
    }

    public void OpenURL()
    {
        Application.OpenURL("https://forms.gle/cQYu68KGDb2czwf26");
    }
}
