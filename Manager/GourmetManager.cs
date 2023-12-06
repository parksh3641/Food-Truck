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

    public void Initialize()
    {
        value = 0;

        value += playerDataBase.AccessDate * 100;

        value += playerDataBase.UpgradeCount * 1;
        value += playerDataBase.SellCount * 2;

        value += playerDataBase.Level * 500;
        value += playerDataBase.Proficiency * 500;

        value += playerDataBase.CastleLevel * 1000;

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

        value += playerDataBase.GetCharacterNumber() * 10000;
        value += playerDataBase.GetAnimalNumber() * 10000;
        value += playerDataBase.GetTruckNumber() * 10000;
        value += playerDataBase.GetButterflyNumber() * 10000;

        Debug.LogError(value);

#if !UNITY_EDITOR
        if (value > playerDataBase.GourmetLevel)
        {
            playerDataBase.GourmetLevel = value;

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);
        }

#endif

        levelText.localizationName = "GourmetScore";
        levelText.plusText = " : " + MoneyUnitString.ToCurrencyString(value);
        levelText.ReLoad();
    }

}
