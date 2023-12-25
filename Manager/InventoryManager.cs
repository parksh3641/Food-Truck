using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryView;

    public ReceiveContent[] receiveContents;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        inventoryView.SetActive(false);
    }

    public void OpenInventoryView()
    {
        if (!inventoryView.activeInHierarchy)
        {
            inventoryView.SetActive(true);

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
        receiveContents[0].Initialize(RewardType.RankPoint, 0);
        receiveContents[1].Initialize(RewardType.DefDestroyTicket, playerDataBase.DefDestroyTicket);
        receiveContents[2].Initialize(RewardType.DefDestroyTicketPiece, playerDataBase.DefDestroyTicketPiece);
        receiveContents[3].Initialize(RewardType.BuffTickets, playerDataBase.BuffTickets);
        receiveContents[4].Initialize(RewardType.GoldTicket, 0);
        receiveContents[5].Initialize(RewardType.Portion1, playerDataBase.Portion1);
        receiveContents[6].Initialize(RewardType.Portion2, playerDataBase.Portion2);
        receiveContents[7].Initialize(RewardType.Portion3, playerDataBase.Portion3);
        receiveContents[8].Initialize(RewardType.Portion4, playerDataBase.Portion4);
        receiveContents[9].Initialize(RewardType.Portion5, playerDataBase.Portion5);
        //receiveContents[9].Initialize(RewardType.Portion6, playerDataBase.Portion6);
    }

}
