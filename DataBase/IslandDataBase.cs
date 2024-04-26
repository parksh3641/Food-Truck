using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class IslandInfo
{
    public IslandType islandType = IslandType.Island1;
    public float success = 100;
    public float sellPrice = 0;
    public float destroy = 0;
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

    public float GetSuccess(IslandType type)
    {
        float number = 0;

        for (int i = 0; i < islandInfoList.Count; i++)
        {
            if (islandInfoList[i].islandType.Equals(type))
            {
                number = islandInfoList[i].success;
                break;
            }
        }

        return number;
    }

    public float GetSellPrice(IslandType type)
    {
        float number = 0;

        for (int i = 0; i < islandInfoList.Count; i++)
        {
            if (islandInfoList[i].islandType.Equals(type))
            {
                number = islandInfoList[i].sellPrice;
                break;
            }
        }

        return number;
    }

    public float GetDestroy(IslandType type)
    {
        float number = 0;

        for (int i = 0; i < islandInfoList.Count; i++)
        {
            if (islandInfoList[i].islandType.Equals(type))
            {
                number = islandInfoList[i].destroy;
                break;
            }
        }

        return number;
    }
}
