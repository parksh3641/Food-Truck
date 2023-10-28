using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class ButterflyInfo
{
    public ButterflyType butterflyType = ButterflyType.Butterfly1;
    public int price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.SellPriceX2Up;
    public float effectNumber = 0;
}

[CreateAssetMenu(fileName = "ButterflyDataBase", menuName = "ScriptableObjects/ButterflyDataBase")]
public class ButterflyDataBase : ScriptableObject
{
    public List<ButterflyInfo> butterflyInfoList = new List<ButterflyInfo>();

    public ButterflyInfo GetButterflyInfo(ButterflyType type)
    {
        ButterflyInfo butterfly = new ButterflyInfo();

        for (int i = 0; i < butterflyInfoList.Count; i++)
        {
            if (butterflyInfoList[i].butterflyType.Equals(type))
            {
                butterfly = butterflyInfoList[i];
                break;
            }
        }

        return butterfly;
    }

    public float GetButterflyEffect(ButterflyType type)
    {
        float number = 0;

        for (int i = 0; i < butterflyInfoList.Count; i++)
        {
            if (butterflyInfoList[i].butterflyType.Equals(type))
            {
                number = butterflyInfoList[i].effectNumber;
                break;
            }
        }

        return number;
    }
}
