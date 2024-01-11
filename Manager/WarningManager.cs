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

    public ReceiveContent receiveContent;
    public ReceiveContent receiveContent2;


    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        nickNameWarning.SetActive(false);
        bugReportWarning.SetActive(false);
        updateWarning.SetActive(false);
        accountStopWarning.SetActive(false);
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

        if(playerDataBase.EventNumber == 3) //업데이트 보상
        {
            OpenUpdateWarning();
        }

        if(playerDataBase.EventNumber == 4) //계정 정지
        {
            OpenAccountStopWarning();
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

            receiveContent.Initialize(RewardType.Crystal, 1500);
        }
        else
        {
            bugReportWarning.SetActive(false);
        }
    }

    public void ReceiveButton()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 1500);

        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        playerDataBase.EventNumber = 0;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventNumber", playerDataBase.EventNumber);

        OpenBugReportWarning();
    }


    public void OpenUpdateWarning()
    {
        if (!bugReportWarning.activeInHierarchy)
        {
            updateWarning.SetActive(true);

            receiveContent2.Initialize(RewardType.Crystal, 500);
        }
        else
        {
            updateWarning.SetActive(false);
        }
    }

    public void ReceiveButton2()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 500);

        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);
        NotionManager.instance.UseNotion(NotionType.SuccessReward);

        playerDataBase.EventNumber = 0;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventNumber", playerDataBase.EventNumber);

        OpenUpdateWarning();
    }

    public void OpenAccountStopWarning()
    {
        if (!accountStopWarning.activeInHierarchy)
        {
            accountStopWarning.SetActive(true);

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
}
