using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionManager : MonoBehaviour
{
    public static PortionManager instance;

    public MoneyAnimation[] portionAnimation;

    public MoneyAnimation expAnimation;
    public MoneyAnimation defTicketPieceAnimation;
    public MoneyAnimation defTicketAnimation;
    public MoneyAnimation buffTicketAnimation;
    public MoneyAnimation skillTicketAnimation;
    public MoneyAnimation repairTicketAnimation;
    public MoneyAnimation rankPointAnimation;
    public MoneyAnimation[] islandCountAnimation;
    public MoneyAnimation eventTicketAnimation;

    private int random = 0;

    public LevelManager levelManager;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        instance = this;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    [Button]
    public void GetPortion(int index, int number)
    {
        switch (index)
        {
            case 0:
                playerDataBase.Portion1 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
                break;
            case 1:
                playerDataBase.Portion2 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
                break;
            case 2:
                playerDataBase.Portion3 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
                break;
            case 3:
                playerDataBase.Portion4 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
                break;
            case 4:
                playerDataBase.Portion5 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
                break;
        }

        portionAnimation[index].PlusMoney(number);
    }

    [Button]
    public void GetRandomPortion(int number)
    {
        random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                playerDataBase.Portion1 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
                break;
            case 1:
                playerDataBase.Portion2 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
                break;
            case 2:
                playerDataBase.Portion3 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
                break;
            case 3:
                playerDataBase.Portion4 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);
                break;
            case 4:
                playerDataBase.Portion5 += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion5", playerDataBase.Portion5);
                break;
        }

        portionAnimation[random].PlusMoney(number);
    }

    [Button]
    public void GetAllPortion(int number)
    {
        playerDataBase.Portion1 += number;
        playerDataBase.Portion2 += number;
        playerDataBase.Portion3 += number;
        playerDataBase.Portion4 += number;

        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion1", playerDataBase.Portion1);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion2", playerDataBase.Portion2);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion3", playerDataBase.Portion3);
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Portion4", playerDataBase.Portion4);

        portionAnimation[0].PlusMoney(number);
        portionAnimation[1].PlusMoney(number);
        portionAnimation[2].PlusMoney(number);
        portionAnimation[3].PlusMoney(number);
    }

    [Button]
    public void GetExp(int number)
    {
        playerDataBase.Exp += number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);
        levelManager.Initialize();

        expAnimation.PlusMoney(number);
    }

    [Button]
    public void GetDefTicketPiece(int number)
    {
        playerDataBase.DefDestroyTicketPiece += number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicketPiece", playerDataBase.DefDestroyTicketPiece);

        defTicketPieceAnimation.PlusMoney(number);
    }

    [Button]
    public void GetDefTickets(int number)
    {
        playerDataBase.DefDestroyTicket += number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("DefDestroyTicket", playerDataBase.DefDestroyTicket);

        defTicketAnimation.PlusMoney(number);
    }

    [Button]
    public void GetBuffTickets(int number)
    {
        playerDataBase.BuffTicket += number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("BuffTickets", playerDataBase.BuffTicket);

        buffTicketAnimation.PlusMoney(number);
    }

    [Button]
    public void GetSkillTickets(int number)
    {
        playerDataBase.SkillTicket += number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("SkillTickets", playerDataBase.SkillTicket);

        skillTicketAnimation.PlusMoney(number);
    }

    [Button]
    public void GetRecoverTickets(int number)
    {
        playerDataBase.RecoverTicket += number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RecoverTicket", playerDataBase.RecoverTicket);

        repairTicketAnimation.PlusMoney(number);
    }

    [Button]
    public void GetRankPoint(int number)
    {
        playerDataBase.RankPoint += number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("RankPoint", playerDataBase.RankPoint);

        rankPointAnimation.PlusMoney(number);
    }

    [Button]
    public void GetIslandCount(int index, int number)
    {
        switch (index)
        {
            case 0:
                playerDataBase.Island1Count += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island1Count", playerDataBase.Island1Count);
                break;
            case 1:
                playerDataBase.Island2Count += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island2Count", playerDataBase.Island2Count);
                break;
            case 2:
                playerDataBase.Island3Count += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island3Count", playerDataBase.Island3Count);
                break;
            case 3:
                playerDataBase.Island4Count += number;
                PlayfabManager.instance.UpdatePlayerStatisticsInsert("Island4Count", playerDataBase.Island4Count);
                break;
        }

        islandCountAnimation[index].PlusMoney(number);
    }

    [Button]
    public void GetEventTicket(int number)
    {
        playerDataBase.EventTicket += number;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("EventTicket", playerDataBase.EventTicket);

        eventTicketAnimation.PlusMoney(number);
    }
}
