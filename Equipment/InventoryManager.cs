using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ReceiveContent[] receiveContents;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void OpenInventoryView()
    {
        Initialize();
    }

    void Initialize()
    {
        receiveContents[0].Initialize(RewardType.DefDestroyTicketPiece, playerDataBase.DefDestroyTicketPiece);
        receiveContents[1].Initialize(RewardType.Portion1, playerDataBase.Portion1);
        receiveContents[2].Initialize(RewardType.Portion2, playerDataBase.Portion2);
        receiveContents[3].Initialize(RewardType.Portion3, playerDataBase.Portion3);
        receiveContents[4].Initialize(RewardType.Portion4, playerDataBase.Portion4);

        receiveContents[5].Initialize(RewardType.Portion5, playerDataBase.Portion5);
        receiveContents[6].Initialize(RewardType.BuffTicket, playerDataBase.BuffTicket);
        receiveContents[7].Initialize(RewardType.SkillTicket, playerDataBase.SkillTicket);
        receiveContents[8].Initialize(RewardType.RepairTicket, playerDataBase.RecoverTicket);
        receiveContents[9].Initialize(RewardType.ChallengePoint, playerDataBase.ChallengePoint);
        receiveContents[10].Initialize(RewardType.RankPoint, playerDataBase.RankPoint);
        receiveContents[11].Initialize(RewardType.AbilityPoint, playerDataBase.AbilityPoint);

        receiveContents[12].Initialize(RewardType.Portion6, playerDataBase.Portion6);

        receiveContents[13].Initialize(RewardType.EventTicket, playerDataBase.EventTicket);

        receiveContents[14].Initialize(RewardType.DefDestroyTicket, playerDataBase.DefDestroyTicket);

        receiveContents[15].gameObject.SetActive(false);
        receiveContents[16].gameObject.SetActive(false);
        receiveContents[17].gameObject.SetActive(false);
        receiveContents[18].gameObject.SetActive(false);
    }

}
