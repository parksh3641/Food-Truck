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

    GifticonManager gifticonManager;

    PlayerDataBase playerDataBase;
    ImageDataBase imageDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        lockedObj.SetActive(true);
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

                numberText.text += playerDataBase.EventEnter1;

                need = 500;
                break;
            case GifticonType.Gifticon_2:
                infoText.text += "2";

                numberText.text += playerDataBase.EventEnter2;

                need = 175;
                break;
            case GifticonType.Gifticon_3:
                infoText.text += "2";

                numberText.text += playerDataBase.EventEnter3;

                need = 80;
                break;
            case GifticonType.Gifticon_4:
                infoText.text += "5";

                numberText.text += playerDataBase.EventEnter4;

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
                playerDataBase.EventEnter1 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventEnter1", playerDataBase.EventEnter1);
                break;
            case GifticonType.Gifticon_2:
                playerDataBase.EventEnter2 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventEnter2", playerDataBase.EventEnter2);
                break;
            case GifticonType.Gifticon_3:
                playerDataBase.EventEnter3 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventEnter3", playerDataBase.EventEnter3);
                break;
            case GifticonType.Gifticon_4:
                playerDataBase.EventEnter4 += 1;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventEnter4", playerDataBase.EventEnter4);
                break;
        }

        FirebaseAnalytics.LogEvent(gifticonType.ToString());

        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SuccessEnter);

        Initialize(gifticonType, gifticonManager);

        gifticonManager.Initialize();
    }
}
