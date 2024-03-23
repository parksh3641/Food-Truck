using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GourmetManager : MonoBehaviour
{
    public static GourmetManager instance;

    public LocalizationContent levelText;


    private int value = 0;
    private int saveValue = 0;
    private int plusValue = 0;


    PlayerDataBase playerDataBase;


    private void Awake()
    {
        instance = this;

        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void FirstInitialize()
    {
        Invoke("Initialize", 0.5f);
    }

    public void Initialize()
    {
        value = 0;

        if (playerDataBase.TestAccount == 0)
        {
            value += playerDataBase.Level * 200;
            value += playerDataBase.CastleLevel * 200;

            value += playerDataBase.Island1Level * 1000;
            value += playerDataBase.Island2Level * 1000;
            value += playerDataBase.Island3Level * 1000;
            value += playerDataBase.Island4Level * 1000;

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
            value += playerDataBase.Skill15 * 100;
            value += playerDataBase.Skill16 * 100;
            value += playerDataBase.Skill17 * 100;
            value += playerDataBase.Skill18 * 100;
            value += playerDataBase.Skill19 * 100;

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
            value += playerDataBase.Treasure13 * 500;
            value += playerDataBase.Treasure14 * 500;

            value += playerDataBase.GetCharacter_Total_AbilityLevel() * 300;
            value += playerDataBase.GetAnimal_Total_AbilityLevel() * 300;
            value += playerDataBase.GetTruck_Total_AbilityLevel() * 300;
            value += playerDataBase.GetButterfly_Total_AbilityLevel() * 300;
            value += playerDataBase.GetTotems_Total_AbilityLevel() * 300;
            //value += playerDataBase.GetFlower_Total_AbilityLevel() * 300;

            value += playerDataBase.GetCharacterNumber() * 5000;
            value += playerDataBase.GetAnimalNumber() * 5000;
            value += playerDataBase.GetTruckNumber() * 5000;
            value += playerDataBase.GetButterflyNumber() * 5000;
            value += playerDataBase.GetTotemsNumber() * 5000;
            value += playerDataBase.GetFlowerNumber() * 5000;
        }

        //Debug.LogError(value);

        if (value != playerDataBase.GourmetLevel)
        {
            playerDataBase.GourmetLevel = value;

            PlayfabManager.instance.UpdatePlayerStatisticsInsert("GourmetLevel", playerDataBase.GourmetLevel);
        }

        levelText.localizationName = "GourmetScore";
        levelText.plusText = " : " + MoneyUnitString.ToCurrencyString(value);
        levelText.ReLoad();

        if(saveValue == 0)
        {
            saveValue = playerDataBase.GourmetLevel;
        }
        else
        {
            if (value > saveValue)
            {
                plusValue = value - saveValue;

                SoundManager.instance.PlaySFX(GameSfxType.Upgrade5);
                NotionManager.instance.UseNotion3(Color.green, "<size=45>" + MoneyUnitString.ToCurrencyString(saveValue) + "  ▶  " + MoneyUnitString.ToCurrencyString(value)
                    + "</size>\n" + LocalizationManager.instance.GetString("GourmetScore") + " +" + MoneyUnitString.ToCurrencyString(plusValue));

                saveValue = value;
            }
        }
    }

}
