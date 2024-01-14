using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GourmetManager : MonoBehaviour
{
    public LocalizationContent levelText;



    private int value = 0;


    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void FirstInitialize()
    {
        Invoke("Initialize", 0.5f);
    }

    public void Initialize()
    {
        value = 0;

        value += playerDataBase.UpgradeCount * 1;
        value += playerDataBase.SellCount * 3;
        value += playerDataBase.UseSources * 5;
        value += playerDataBase.BuffCount * 10;

        value += playerDataBase.AccessDate * 100;
        value += playerDataBase.Level * 1000;
        value += playerDataBase.QuestCount * 300;
        value += playerDataBase.CastleLevel * 500;
        value += playerDataBase.Proficiency * 1000; //¼÷·Ãµµ

        if (playerDataBase.Package1)
        {
            value += 10000;
        }

        if (playerDataBase.Package2)
        {
            value += 10000;
        }

        if (playerDataBase.Package3)
        {
            value += 10000;
        }

        if (playerDataBase.Package4)
        {
            value += 10000;
        }

        if (playerDataBase.Package5)
        {
            value += 10000;
        }

        if (playerDataBase.Package6)
        {
            value += 10000;
        }

        if (playerDataBase.RemoveAds)
        {
            value += 10000;
        }

        if (playerDataBase.GoldX2)
        {
            value += 10000;
        }

        if (playerDataBase.AutoUpgrade)
        {
            value += 10000;
        }

        if (playerDataBase.AutoPresent)
        {
            value += 10000;
        }

        if (playerDataBase.SuperOffline)
        {
            value += 10000;
        }

        if (playerDataBase.TestAccount == 0)
        {
            value += playerDataBase.Skill1 * 100;
            value += playerDataBase.Skill2 * 100;
            value += playerDataBase.Skill3 * 100;
            value += playerDataBase.Skill4 * 100;
            value += playerDataBase.Skill5 * 100;
            value += playerDataBase.Skill6 * 100;
            value += playerDataBase.Skill7 * 100;
            value += playerDataBase.Skill8 * 100;
            value += playerDataBase.Skill9 * 100;
            value += playerDataBase.Skill10 * 100;
            value += playerDataBase.Skill11 * 100;
            value += playerDataBase.Skill12 * 100;
            value += playerDataBase.Skill13 * 100;
            value += playerDataBase.Skill14 * 100;

            value += playerDataBase.Treasure1 * 500;
            value += playerDataBase.Treasure2 * 500;
            value += playerDataBase.Treasure3 * 500;
            value += playerDataBase.Treasure4 * 500;
            value += playerDataBase.Treasure5 * 500;
            value += playerDataBase.Treasure6 * 500;
            value += playerDataBase.Treasure7 * 500;
            value += playerDataBase.Treasure8 * 500;
            value += playerDataBase.Treasure9 * 500;
            value += playerDataBase.Treasure10 * 500;
            value += playerDataBase.Treasure11 * 500;
            value += playerDataBase.Treasure12 * 500;

            value += playerDataBase.GetCharacterNumber() * 5000;
            value += playerDataBase.GetAnimalNumber() * 5000;
            value += playerDataBase.GetTruckNumber() * 5000;
            value += playerDataBase.GetButterflyNumber() * 5000;
            value += playerDataBase.GetTotemsNumber() * 5000;
            value += playerDataBase.GetFlowerNumber() * 5000;
        }

        Debug.LogError(value);

        if (value != playerDataBase.GourmetLevel)
        {
            playerDataBase.GourmetLevel = value;

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);
        }

        levelText.localizationName = "GourmetScore";
        levelText.plusText = " : " + MoneyUnitString.ToCurrencyString(value);
        levelText.ReLoad();
    }

}
