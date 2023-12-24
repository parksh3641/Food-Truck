using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningManager : MonoBehaviour
{
    public GameObject nickNameWarning;

    public GameObject bugReportWarning;

    public ReceiveContent receiveContent;


    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        nickNameWarning.SetActive(false);
        bugReportWarning.SetActive(false);
    }

    public void Initialize()
    {
        if(playerDataBase.NickNameWarning > 0)
        {
            OpenNickNameWarningView();

            playerDataBase.NickNameWarning = 0;
            PlayfabManager.instance.UpdatePlayerStatisticsInsert("NickNameWarning", playerDataBase.NickNameWarning);
        }

        if(playerDataBase.BugReportWarning > 0)
        {
            OpenBugReportWarning();
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

            receiveContent.Initialize(RewardType.Crystal, 500 * playerDataBase.BugReportWarning);
        }
        else
        {
            bugReportWarning.SetActive(false);
        }
    }

    public void ReceiveButton()
    {
        PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 500 * playerDataBase.BugReportWarning);

        SoundManager.instance.PlaySFX(GameSfxType.QuestReward);

        playerDataBase.BugReportWarning = 0;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("BugReportWarning", playerDataBase.BugReportWarning);

        OpenBugReportWarning();
    }

}
