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
        //receiveContents[0].Initialize(RewardType.Crystal, playerDataBase.Crystal);
        receiveContents[0].Initialize(RewardType.Exp, playerDataBase.Exp);
        receiveContents[1].Initialize(RewardType.RankPoint, playerDataBase.RankPoint);
        receiveContents[2].Initialize(RewardType.DefDestroyTicketPiece, playerDataBase.DefDestroyTicketPiece);
        receiveContents[3].Initialize(RewardType.DefDestroyTicket, playerDataBase.DefDestroyTicket);
        receiveContents[4].Initialize(RewardType.BuffTickets, playerDataBase.BuffTickets);
        receiveContents[5].Initialize(RewardType.SkillTickets, playerDataBase.SkillTickets);
        receiveContents[6].Initialize(RewardType.Portion1, playerDataBase.Portion1);
        receiveContents[7].Initialize(RewardType.Portion2, playerDataBase.Portion2);
        receiveContents[8].Initialize(RewardType.Portion3, playerDataBase.Portion3);
        receiveContents[9].Initialize(RewardType.Portion4, playerDataBase.Portion4);
        receiveContents[10].Initialize(RewardType.Portion5, playerDataBase.Portion5);
        receiveContents[11].gameObject.SetActive(false);
    }

}
