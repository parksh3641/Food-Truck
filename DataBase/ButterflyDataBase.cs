using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class ButterflyInfo
{
    public ButterflyType butterflyType = ButterflyType.Butterfly1;
    public int price = 0;
    public int crystal = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.DefDestroyPercentUp;
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

    public float GetButterflyEffect(int number)
    {
        if(butterflyInfoList[number] == null)
        {
            number = 0;
        }

        return butterflyInfoList[number].effectNumber;
    }
}
