using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TruckInfo
{
    public TruckType truckType = TruckType.Bread;
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

    public float GetTruckEffect(TruckType type)
    {
        float number = 0;

        for (int i = 0; i < truckInfoList.Count; i++)
        {
            if (truckInfoList[i].truckType.Equals(type))
            {
                number = truckInfoList[i].effectNumber;
                break;
            }
        }

        return number;
    }
}
