using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class IslandInfo
{
    public IslandType islandType = IslandType.Island1;
    public int price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.SellPricePercentUp;
    public float effectNumber = 0;
}


[CreateAssetMenu(fileName = "IslandDataBase", menuName = "ScriptableObjects/IslandDataBase")]
public class IslandDataBase : ScriptableObject
{
    public List<IslandInfo> islandInfoList = new List<IslandInfo>();

    public IslandInfo GetIslandInfo(IslandType type)
    {
        IslandInfo island = new IslandInfo();

        for (int i = 0; i < islandInfoList.Count; i++)
        {
            if (islandInfoList[i].islandType.Equals(type))
            {
                island = islandInfoList[i];
                break;
            }
        }

        return island;
    }

    public float GetIslandEffect(ButterflyType type)
    {
        float number = 0;

        for (int i = 0; i < islandInfoList.Count; i++)
        {
            if (islandInfoList[i].islandType.Equals(type))
            {
                number = islandInfoList[i].effectNumber;
                break;
            }
        }

        return number;
    }
}
