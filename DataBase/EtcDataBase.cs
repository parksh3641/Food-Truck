using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BucketInfo
{
    public BucketType bucketType = BucketType.Bucket1;
    public long price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.ItemDropPercentUp;
    public float effectNumber = 0;

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.ItemDropPercentUp;
    public float retentionValue = 0f;
}

[System.Serializable]
public class ChairInfo
{
    public ChairType chairType = ChairType.Chair1;
    public long price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.AttackPowerPercentUp;
    public float effectNumber = 0;

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.AttackPowerPercentUp;
    public float retentionValue = 0f;
}

[System.Serializable]
public class TubeInfo
{
    public TubeType tubeType = TubeType.Tube1;
    public long price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.RankModeSuccessPercentUp;
    public float effectNumber = 0;

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.RankModeSuccessPercentUp;
    public float retentionValue = 0f;
}

[System.Serializable]
public class SurfboardInfo
{
    public SurfboardType surfboardType = SurfboardType.Surfboard1;
    public long price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.SuccessX3PercentUp;
    public float effectNumber = 0;

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.SuccessX3PercentUp;
    public float retentionValue = 0f;
}

[System.Serializable]
public class UmbrellaInfo
{
    public UmbrellaType umbrellaType = UmbrellaType.Umbrella1;
    public long price = 0;

    [Space]
    public PassiveEffect passiveEffect = PassiveEffect.CriticalPowerPercentUp;
    public float effectNumber = 0;

    [Space]
    public PassiveEffect retentionEffect = PassiveEffect.CriticalPowerPercentUp;
    public float retentionValue = 0f;
}

[CreateAssetMenu(fileName = "EtcDataBase", menuName = "ScriptableObjects/EtcDataBase")]
public class EtcDataBase : ScriptableObject
{
    public List<BucketInfo> bucketInfoList = new List<BucketInfo>();

    [Space]
    public List<ChairInfo> chairInfoList = new List<ChairInfo>();

    [Space]
    public List<TubeInfo> tubeInfoList = new List<TubeInfo>();

    [Space]
    public List<SurfboardInfo> surfboardInfoList = new List<SurfboardInfo>();

    [Space]
    public List<UmbrellaInfo> umbrellaInfoList = new List<UmbrellaInfo>();

    [Space]
    public int[] retentionPrice = new int[10];

    BucketInfo bucketInfo = new BucketInfo();
    ChairInfo chairInfo = new ChairInfo();
    TubeInfo tubeInfo = new TubeInfo();
    SurfboardInfo surfboardInfo = new SurfboardInfo();
    UmbrellaInfo umbrellaInfo = new UmbrellaInfo();

    public BucketInfo GetBucketInfo(BucketType type)
    {
        for (int i = 0; i < bucketInfoList.Count; i++)
        {
            if (bucketInfoList[i].bucketType.Equals(type))
            {
                bucketInfo = bucketInfoList[i];
                break;
            }
        }

        return bucketInfo;
    }

    public float GetBucketEffect(int number)
    {
        if (bucketInfoList[number] == null)
        {
            number = 0;
        }

        return bucketInfoList[number].effectNumber;
    }

    public ChairInfo GetChairInfo(ChairType type)
    {
        for (int i = 0; i < chairInfoList.Count; i++)
        {
            if (chairInfoList[i].chairType.Equals(type))
            {
                chairInfo = chairInfoList[i];
                break;
            }
        }

        return chairInfo;
    }

    public float GetChairEffect(int number)
    {
        if (chairInfoList[number] == null)
        {
            number = 0;
        }

        return chairInfoList[number].effectNumber;
    }

    public TubeInfo GetTubeInfo(TubeType type)
    {
        for (int i = 0; i < tubeInfoList.Count; i++)
        {
            if (tubeInfoList[i].tubeType.Equals(type))
            {
                tubeInfo = tubeInfoList[i];
                break;
            }
        }

        return tubeInfo;
    }

    public float GetTubeEffect(int number)
    {
        if (tubeInfoList[number] == null)
        {
            number = 0;
        }

        return tubeInfoList[number].effectNumber;
    }

    public SurfboardInfo GetSurfboardInfo(SurfboardType type)
    {
        for (int i = 0; i < surfboardInfoList.Count; i++)
        {
            if (surfboardInfoList[i].surfboardType.Equals(type))
            {
                surfboardInfo = surfboardInfoList[i];
                break;
            }
        }

        return surfboardInfo;
    }

    public float GetSurfboardEffect(int number)
    {
        if (surfboardInfoList[number] == null)
        {
            number = 0;
        }

        return surfboardInfoList[number].effectNumber;
    }

    public UmbrellaInfo GetUmbrellaInfo(UmbrellaType type)
    {
        for (int i = 0; i < umbrellaInfoList.Count; i++)
        {
            if (umbrellaInfoList[i].umbrellaType.Equals(type))
            {
                umbrellaInfo = umbrellaInfoList[i];
                break;
            }
        }

        return umbrellaInfo;
    }

    public float GetUmbrellaEffect(int number)
    {
        if (umbrellaInfoList[number] == null)
        {
            number = 0;
        }

        return umbrellaInfoList[number].effectNumber;
    }



    public int GetRetentionPrice(int number)
    {
        return retentionPrice[number];
    }
}
