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
        receiveContents[0].Initialize(RewardType.Island1_Heart, playerDataBase.Island1Count);
        receiveContents[1].Initialize(RewardType.Island2_Heart, playerDataBase.Island2Count);
        receiveContents[2].Initialize(RewardType.Island3_Heart, playerDataBase.Island3Count);
        receiveContents[3].Initialize(RewardType.Island4_Heart, playerDataBase.Island4Count);
        receiveContents[4].Initialize(RewardType.DefDestroyTicketPiece, playerDataBase.DefDestroyTicketPiece);
        receiveContents[5].Initialize(RewardType.Portion1, playerDataBase.Portion1);
        receiveContents[6].Initialize(RewardType.Portion2, playerDataBase.Portion2);
        receiveContents[7].Initialize(RewardType.Portion3, playerDataBase.Portion3);
        receiveContents[8].Initialize(RewardType.Portion4, playerDataBase.Portion4);

        receiveContents[9].Initialize(RewardType.Portion5, playerDataBase.Portion5);
        receiveContents[10].Initialize(RewardType.BuffTicket, playerDataBase.BuffTicket);
        receiveContents[11].Initialize(RewardType.SkillTicket, playerDataBase.SkillTicket);
        receiveContents[12].Initialize(RewardType.RepairTicket, playerDataBase.RecoverTicket);
        receiveContents[13].Initialize(RewardType.ChallengePoint, playerDataBase.ChallengePoint);
        receiveContents[14].Initialize(RewardType.RankPoint, playerDataBase.RankPoint);
        receiveContents[15].Initialize(RewardType.AbilityPoint, playerDataBase.AbilityPoint);

        receiveContents[16].Initialize(RewardType.Portion6, playerDataBase.Portion6);

        receiveContents[17].Initialize(RewardType.EventTicket, playerDataBase.EventTicket);
        receiveContents[17].Limit(1000);

        receiveContents[18].Initialize(RewardType.DefDestroyTicket, playerDataBase.DefDestroyTicket);
    }

}