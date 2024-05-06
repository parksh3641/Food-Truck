using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static int gold = 0;

    static PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public static int GetGold()
    {
        gold = 500000;

        gold += playerDataBase.NextFoodNumber * 100000;

        return gold;
    }
}




