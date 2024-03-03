using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RewardInfo
{
    public RewardType rewardType = RewardType.Gold;
    public int number = 0;
    public int addNumber = 0;
}


[System.Serializable]
public class DungeonInfo
{
    public DungeonType dungeonType;
    public int percent = 100;
    public int health = 100;
    public int timer = 30;

    [Header("Reward")]
    public List<RewardInfo> rewardInfos = new List<RewardInfo>();
}

[CreateAssetMenu(fileName = "DungeonDataBase", menuName = "ScriptableObjects/DungeonDataBase")]
public class DungeonDataBase : ScriptableObject
{
    public DungeonInfo[] dungeonInfos;

    public DungeonInfo GetDungeonInfo(DungeonType type)
    {
        DungeonInfo dungeonInfo = new DungeonInfo();

        for(int i = 0; i < dungeonInfos.Length; i ++)
        {
            if(dungeonInfos[i].dungeonType.Equals(type))
            {
                dungeonInfo = dungeonInfos[i];
                break;
            }
        }

        return dungeonInfo;
    }
}
