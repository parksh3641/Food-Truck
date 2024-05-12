using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class ButterflyInfo
{
    public ButterflyType butterflyType = ButterflyType.Butterfly1;
    public long price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.DefDestroyPercentUp;
    public float effectNumber = 0;
}

[CreateAssetMenu(fileName = "ButterflyDataBase", menuName = "ScriptableObjects/ButterflyDataBase")]
public class ButterflyDataBase : ScriptableObject
{
    public List<ButterflyInfo> butterflyInfoList = new List<ButterflyInfo>();

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.None;
    public float retentionValue = 0f;
    public int[] retentionPrice = new int[10];

    ButterflyInfo info = new ButterflyInfo();

    public ButterflyInfo GetButterflyInfo(ButterflyType type)
    {
        for (int i = 0; i < butterflyInfoList.Count; i++)
        {
            if (butterflyInfoList[i].butterflyType.Equals(type))
            {
                info = butterflyInfoList[i];
                break;
            }
        }

        return info;
    }

    public float GetButterflyEffect(int number)
    {
        if(butterflyInfoList[number] == null)
        {
            number = 0;
        }

        return butterflyInfoList[number].effectNumber;
    }

    public int GetRetentionPrice(int number)
    {
        return retentionPrice[number];
    }
}
