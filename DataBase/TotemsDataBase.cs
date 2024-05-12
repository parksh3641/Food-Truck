using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TotemsInfo
{
    public TotemsType totemsType = TotemsType.Totems1;
    public long price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.SuccessX2PercentUp;
    public float effectNumber = 0;
}

[CreateAssetMenu(fileName = "TotemsDataBase", menuName = "ScriptableObjects/TotemsDataBase")]
public class TotemsDataBase : ScriptableObject
{
    public List<TotemsInfo> totemsInfoList = new List<TotemsInfo>();

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.None;
    public float retentionValue = 0f;
    public int[] retentionPrice = new int[10];

    TotemsInfo info = new TotemsInfo();

    public TotemsInfo GetTotemsInfo(TotemsType type)
    {
        for (int i = 0; i < totemsInfoList.Count; i++)
        {
            if (totemsInfoList[i].totemsType.Equals(type))
            {
                info = totemsInfoList[i];
                break;
            }
        }

        return info;
    }

    public float GetTotemsEffect(int number)
    {
        if (totemsInfoList[number] == null)
        {
            number = 0;
        }

        return totemsInfoList[number].effectNumber;
    }

    public int GetRetentionPrice(int number)
    {
        return retentionPrice[number];
    }
}
