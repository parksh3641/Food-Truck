using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryView;

    public GameObject alarm;
    public GameObject ingameAlarm;

    public ReceiveContent[] receiveContents;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        inventoryView.SetActive(false);

        alarm.SetActive(true);
        ingameAlarm.SetActive(true);
    }

    public void OpenInventoryView()
    {
        if (!inventoryView.activeInHierarchy)
        {
            inventoryView.SetActive(true);

            alarm.SetActive(false);
            ingameAlarm.SetActive(false);

            Initialize();

            FirebaseAnalytics.LogEvent("OpenInventory");
        }
        else
        {
            inventoryView.SetActive(false);
        }
    }

    void Initialize()
    {
        receiveContents[0].Initialize(RewardType.Island1, 0);
        receiveContents[1].Initialize(RewardType.Island2, 0);
        receiveContents[2].Initialize(RewardType.Island3, 0);
        receiveContents[3].Initialize(RewardType.Island4, 0);
        receiveContents[4].Initialize(RewardType.Portion1, playerDataBase.Portion1);
        receiveContents[5].Initialize(RewardType.Portion2, playerDataBase.Portion2);
        receiveContents[6].Initialize(RewardType.Portion3, playerDataBase.Portion3);
        receiveContents[7].Initialize(RewardType.Portion4, playerDataBase.Portion4);
        receiveContents[8].Initialize(RewardType.Portion5, playerDataBase.Portion5);
        receiveContents[9].Initialize(RewardType.RankPoint, playerDataBase.RankPoint);
        receiveContents[10].Initialize(RewardType.BuffTicket, playerDataBase.BuffTicket);
        receiveContents[11].Initialize(RewardType.SkillTicket, playerDataBase.SkillTicket);
        receiveContents[12].Initialize(RewardType.RepairTicket, playerDataBase.RecoverTicket);
        receiveContents[13].Initialize(RewardType.DefDestroyTicketPiece, playerDataBase.DefDestroyTicketPiece);
        receiveContents[14].Initialize(RewardType.DefDestroyTicket, playerDataBase.DefDestroyTicket);
        receiveContents[15].gameObject.SetActive(false);
    }

}
