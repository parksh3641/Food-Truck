using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TotemsInfo
{
    public TotemsType totemsType = TotemsType.Totems1;
    public int price = 0;
    public int crystal = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.SuccessX2PercentUp;
    public float effectNumber = 0;
}

[CreateAssetMenu(fileName = "TotemsDataBase", menuName = "ScriptableObjects/TotemsDataBase")]
public class TotemsDataBase : ScriptableObject
{
    public List<TotemsInfo> totemsInfoList = new List<TotemsInfo>();

    public TotemsInfo GetTotemsInfo(TotemsType type)
    {
        TotemsInfo totems = new TotemsInfo();

        for (int i = 0; i < totemsInfoList.Count; i++)
        {
            if (totemsInfoList[i].totemsType.Equals(type))
            {
                totems = totemsInfoList[i];
                break;
            }
        }

        return totems;
    }

    public float GetTotemsEffect(int number)
    {
        if (totemsInfoList[number] == null)
        {
            number = 0;
        }

        return totemsInfoList[number].effectNumber;
    }
}
