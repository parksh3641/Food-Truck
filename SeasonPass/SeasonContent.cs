using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonContent : MonoBehaviour
{
    public int index = 0;
    public Text numberText;

    public ReceiveContent[] receiveContents;

    public GameObject freeSuccess; //¹«·á È¹µæ ¿Ï·á
    public GameObject passSuccess; //ÆÐ½º È¹µæ ¿Ï·á

    public bool freePurchase; //È°¼ºÈ­ ¿©ºÎ
    public bool passPurchase; //È°¼ºÈ­ ¿©ºÎ


    PlayerDataBase playerDataBase;
    SeasonDataBase seasonDataBase;
    SeasonPassManager seasonPassManager;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (seasonDataBase == null) seasonDataBase = Resources.Load("SeasonDataBase") as SeasonDataBase;
    }

    public void Initialize(int number,SeasonPassManager manager)
    {
        index = number;
        numberText.text = (index + 1).ToString();

        seasonPassManager = manager;

        freePurchase = false;
        passPurchase = false;

        freeSuccess.SetActive(false);
        passSuccess.SetActive(false);

        receiveContents[0].Initialize(seasonDataBase.seasonClassList[index].freeRewardType, seasonDataBase.seasonClassList[index].freeCount);
        receiveContents[1].Initialize(seasonDataBase.seasonClassList[index].passRewardType, seasonDataBase.seasonClassList[index].passCount);

        receiveContents[0].Locked();
        receiveContents[1].Locked();
    }

    public void GetFreeReward()
    {
        if (freePurchase && playerDataBase.GetSeasonPass(SeasonPassType.Free, index) == false)
        {
            switch (index % 5)
            {
                case 0:
                    PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 30);
                    break;
                case 1:
                    PortionManager.instance.GetAllPortion(1);
                    break;
                case 2:
                    PortionManager.instance.GetAbilityPoint(500);
                    break;
                case 3:
                    PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 50);
                    break;
                case 4:
                    PortionManager.instance.GetEventTicket(100);
                    break;
            }

            playerDataBase.UpdateSeasonPass(SeasonPassType.Free, index);
            freeSuccess.SetActive(true);
            seasonPassManager.CheckSeasonPass();
        }
        else
        {
            receiveContents[0].OpenInfo();
        }
    }

    public void GetPassReward()
    {
        if(passPurchase && playerDataBase.SeasonPass && playerDataBase.GetSeasonPass(SeasonPassType.Pass, index) == false)
        {
            switch (index % 5)
            {
                case 0:
                    PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 150);
                    break;
                case 1:
                    PortionManager.instance.GetAllPortion(3);
                    break;
                case 2:
                    PortionManager.instance.GetAbilityPoint(5000);
                    break;
                case 3:
                    PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 300);
                    break;
                case 4:
                    PortionManager.instance.GetEventTicket(1000);
                    break;
            }

            playerDataBase.UpdateSeasonPass(SeasonPassType.Pass, index);
            passSuccess.SetActive(true);
            seasonPassManager.CheckSeasonPass();
        }
        else
        {
            receiveContents[1].OpenInfo();
        }
    }


    public void UnLockFree() //È¹µæ °¡´É
    {
        freePurchase = true;

        receiveContents[0].UnLock();
    }

    public void CheckMarkFree() //È¹µæÇÔ
    {
        freePurchase = true;

        receiveContents[0].UnLock();
        freeSuccess.SetActive(true);
    }

    public void UnLockPass() //È¹µæ °¡´É
    {
        passPurchase = true;

        receiveContents[1].UnLock();
    }

    public void CheckMarkPass() //È¹µæ ÇÔ
    {
        passPurchase = true;

        receiveContents[1].UnLock();
        passSuccess.SetActive(true);
    }
}
