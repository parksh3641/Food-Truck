using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TruckInfo
{
    public TruckType truckType = TruckType.Truck1;
    public int price = 0;
    public int crystal = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.UpgradePercentUp;
    public float effectNumber = 0;
}


[CreateAssetMenu(fileName = "TruckDataBase", menuName = "ScriptableObjects/TruckDataBase")]
public class TruckDataBase : ScriptableObject
{
    public List<TruckInfo> truckInfoList = new List<TruckInfo>();

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.None;
    public float retentionValue = 0f;
    public int[] retentionPrice = new int[5];

    public TruckInfo GetTruckInfo(TruckType type)
    {
        TruckInfo truck = new TruckInfo();

        for (int i = 0; i < truckInfoList.Count; i++)
        {
            if (truckInfoList[i].truckType.Equals(type))
            {
                truck = truckInfoList[i];
                break;
            }
        }

        return truck;
    }

    public float GetTruckEffect(int number)
    {
        if (truckInfoList[number] == null)
        {
            number = 0;
        }

        return truckInfoList[number].effectNumber;
    }

    public int GetRetentionPrice(int number)
    {
        return retentionPrice[number];
    }
}
