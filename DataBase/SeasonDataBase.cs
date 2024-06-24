using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeasonClass
{
    public int level = 0;

    public RewardType freeRewardType = RewardType.Gold;
    public int freeCount = 0;

    public RewardType passRewardType = RewardType.Gold;
    public int passCount = 0;

}

[CreateAssetMenu(fileName = "SeasonDataBase", menuName = "ScriptableObjects/SeasonDataBase")]
public class SeasonDataBase : ScriptableObject
{
    [Title("Season Pass")]
    public List<SeasonClass> seasonClassList = new List<SeasonClass>();


    [Button]
    public void Initialize()
    {
        seasonClassList.Clear();

        for (int i = 0; i < 30; i ++)
        {
            SeasonClass seasonClass = new SeasonClass();

            seasonClass.level = i;

            switch (i % 5)
            {
                case 0:
                    seasonClass.freeRewardType = RewardType.Crystal;
                    seasonClass.freeCount = 30;

                    seasonClass.passRewardType = RewardType.Crystal;
                    seasonClass.passCount = 150;
                    break;
                case 1:
                    seasonClass.freeRewardType = RewardType.PortionSet;
                    seasonClass.freeCount = 1;

                    seasonClass.passRewardType = RewardType.PortionSet;
                    seasonClass.passCount = 3;
                    break;
                case 2:
                    seasonClass.freeRewardType = RewardType.AbilityPoint;
                    seasonClass.freeCount = 300;

                    seasonClass.passRewardType = RewardType.AbilityPoint;
                    seasonClass.passCount = 1500;
                    break;
                case 3:
                    seasonClass.freeRewardType = RewardType.Crystal;
                    seasonClass.freeCount = 50;

                    seasonClass.passRewardType = RewardType.Crystal;
                    seasonClass.passCount = 250;
                    break;
                case 4:
                    seasonClass.freeRewardType = RewardType.EventTicket;
                    seasonClass.freeCount = 100;

                    seasonClass.passRewardType = RewardType.EventTicket;
                    seasonClass.passCount = 1000;
                    break;
            }

            seasonClassList.Add(seasonClass);
        }
    }
}
