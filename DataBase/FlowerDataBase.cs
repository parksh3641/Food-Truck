using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlowerInfo
{
    public FlowerType flowerType = FlowerType.Flower1;
    public int price = 0;
    public int crystal = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.TotalPercentUp;
    public float effectNumber = 0;
}

[CreateAssetMenu(fileName = "FlowerDataBase", menuName = "ScriptableObjects/FlowerDataBase")]
public class FlowerDataBase : ScriptableObject
{
    public List<FlowerInfo> flowerInfoList = new List<FlowerInfo>();

    public FlowerInfo GetFlowerInfo(FlowerType type)
    {
        FlowerInfo flower = new FlowerInfo();

        for (int i = 0; i < flowerInfoList.Count; i++)
        {
            if (flowerInfoList[i].flowerType.Equals(type))
            {
                flower = flowerInfoList[i];
                break;
            }
        }

        return flower;
    }

    public float GetFlowerEffect(int number)
    {
        return flowerInfoList[number].effectNumber;
    }
}
