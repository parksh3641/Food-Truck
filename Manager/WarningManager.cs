using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningManager : MonoBehaviour
{
    public GameObject nickNameWarning;
    public GameObject bugReportWarning;
    public GameObject updateWarning;
    public GameObject accountStopWarning;
    public Text accountStopNickname;

    public GameObject friendsWarning;
    public GameObject reviewWarning;

    public ReceiveContent receiveContent;
    public ReceiveContent receiveContent2;
    public ReceiveContent receiveContent3;
    public ReceiveContent receiveContent4;


    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        nickNameWarning.SetActive(false);
        bugReportWarning.SetActive(false);
        updateWarning.SetActive(false);
        accountStopWarning.SetActive(false);
        friendsWarning.SetActive(false);
        reviewWarning.SetActive(false);
    }

    public void Initialize()
    {
        if(playerDataBase.EventNumber == 1) //닉네임 제재
        {
            OpenNickNameWarningView();

            playerDataBase.EventNumber = 0;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventNumber", playerDataBase.EventNumber);
        }

        if(playerDataBase.EventNumber == 2) //버그 발견 보상
        {
            OpenBugReportWarning();
        }

        if(playerDataBase.EventNumber == 3) //계정 정지
        {
            OpenAccountStopWarning();
        }

        if (playerDataBase.UpdateNumber == 1) //업데이트 보상
        {
            OpenUpdateWarning();
        }

        if (playerDataBase.FriendsNumber == 1) //친구 초대 이벤트
        {
            OpenFriendsWarning();
        }

        if (playerDataBase.ReviewNumber == 1) //리뷰 이벤트
        {
            OpenReviewWarning();
        }
    }

    public void OpenNickNameWarningView()
    {
        if(!nickNameWarning.activeInHierarchy)
        {
            nickNameWarning.SetActive(true);
        }
        else
        {
            nickNameWarning.SetActive(false);
        }
    }

    public void OpenBugReportWarning()
    {
        if (!bugReportWarning.activeInHierarchy)
        {
            bugReportWarning.SetActive(true);

            receiveContent.Initialize(RewardType.Crystal, 1000);
        }
        else
        {
            bugReportWarning.SetActive(false);
        }
    }

    public void ReceiveButton()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1000);

        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        if (!playerDataBase.CheckIcon(IconType.Icon_25))
        {
            playerDataBase.SetIcon(IconType.Icon_25, 1);
            PlayfabManager.instance.GrantItemsToUser((IconType.Icon_25).ToString(), "Icon");
        }

        playerDataBase.EventNumber = 0;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventNumber", playerDataBase.EventNumber);

        OpenBugReportWarning();
    }

    public void OpenAccountStopWarning()
    {
        if (!accountStopWarning.activeInHierarchy)
        {
            accountStopWarning.SetActive(true);

            accountStopNickname.text = LocalizationManager.instance.GetString("NickName") + " : " + GameStateManager.instance.NickName;

#if !UNITY_EDITOR
            playerDataBase.UpgradeCount = 0;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("UpgradeCount", playerDataBase.UpgradeCount);

            playerDataBase.TotalLevel = 0;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("TotalLevel", playerDataBase.TotalLevel);

            playerDataBase.GourmetLevel = 0;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);
#endif
        }
        else
        {
            accountStopWarning.SetActive(false);

            Application.Quit();
        }
    }


    public void OpenUpdateWarning()
    {
        if (!updateWarning.activeInHierarchy)
        {
            updateWarning.SetActive(true);

            receiveContent2.Initialize(RewardType.Crystal, 300);
        }
        else
        {
            updateWarning.SetActive(false);
        }
    }

    public void ReceiveButton2()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);

        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        playerDataBase.UpdateNumber = 0;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("UpdateNumber", playerDataBase.UpdateNumber);

        OpenUpdateWarning();
    }

    public void OpenFriendsWarning()
    {
        if (!friendsWarning.activeInHierarchy)
        {
            friendsWarning.SetActive(true);

            receiveContent3.Initialize(RewardType.Crystal, 500);
        }
        else
        {
            friendsWarning.SetActive(false);
        }
    }
    public void ReceiveButton3()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 500);

        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        playerDataBase.FriendsNumber = 0;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("FriendsNumber", playerDataBase.FriendsNumber);

        OpenFriendsWarning();
    }

    public void OpenReviewWarning()
    {
        if (!reviewWarning.activeInHierarchy)
        {
            reviewWarning.SetActive(true);

            receiveContent4.Initialize(RewardType.Crystal, 3000);
        }
        else
        {
            reviewWarning.SetActive(false);
        }
    }
    public void ReceiveButton4()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 3000);

        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        playerDataBase.ReviewNumber = 2;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("ReviewNumber", playerDataBase.ReviewNumber);

        if (!playerDataBase.CheckIcon(IconType.Icon_26))
        {
            playerDataBase.SetIcon(IconType.Icon_26, 1);
            PlayfabManager.instance.GrantItemsToUser((IconType.Icon_26).ToString(), "Icon");
        }

        OpenReviewWarning();
    }
}
