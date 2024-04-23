using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipPercent
{
    public EquipType equipType = EquipType.Equip_Index_1;

    public float[] normalRange = new float[2];
    public float[] rareRange = new float[2];
    public float[] uniqueRange = new float[2];
    public float[] legendaryRange = new float[2];
}

[CreateAssetMenu(fileName = "EquipDataBase", menuName = "ScriptableObjects/EquipDataBase")]
public class EquipDataBase : ScriptableObject
{
    protected float rarePercent = 6.0f;
    protected float uniquePercent = 1.8f;
    protected float legendaryPercent = 0.3f;

    public List<EquipPercent> equipPercentList = new List<EquipPercent>();

    private float range = 0;

    private string color;
    private string whiteColor = "<color=#FFFFFF>";
    private string blueColor = "<color=#00FFFF>";
    private string purpleColor = "<color=#FF64FF>";
    private string yellowColor = "<color=#FFFF00>";

    public float GetRankUpPercent(int number)
    {
        float num = 0;

        switch(number)
        {
            case 0:
                num = rarePercent;
                break;
            case 1:
                num = uniquePercent;
                break;
            case 2:
                num = legendaryPercent;
                break;
        }

        return num;
    }

    public float GetRange(EquipType equipType, RankType rankType)
    {
        for(int i = 0; i < equipPercentList.Count; i ++)
        {
            if(equipPercentList[i].equipType.Equals(equipType))
            {
                switch (rankType)
                {
                    case RankType.Normal:
                        range = Random.Range(equipPercentList[i].normalRange[0], equipPercentList[i].normalRange[1]);

                        break;
                    case RankType.Rare:
                        range = Random.Range(equipPercentList[i].rareRange[0], equipPercentList[i].rareRange[1]);

                        break;
                    case RankType.Unique:
                        range = Random.Range(equipPercentList[i].uniqueRange[0], equipPercentList[i].uniqueRange[1]);

                        break;
                    case RankType.Legendary:
                        range = Random.Range(equipPercentList[i].legendaryRange[0], equipPercentList[i].legendaryRange[1]);

                        break;
                }

                break;
            }
        }

        return Mathf.Floor(range * 100f) / 100f; ;
    }

    public string CheckValue(EquipType equipType, RankType rankType, float value)
    {
        for (int i = 0; i < equipPercentList.Count; i++)
        {
            if (equipPercentList[i].equipType.Equals(equipType))
            {
                switch (rankType)
                {
                    case RankType.Normal:
                        if(value <= equipPercentList[i].normalRange[0] + ((equipPercentList[i].normalRange[1] - equipPercentList[i].normalRange[0]) * 0.3f))
                        {
                            color = whiteColor;
                        }
                        else if (value <= equipPercentList[i].normalRange[0] + ((equipPercentList[i].normalRange[1] - equipPercentList[i].normalRange[0]) * 0.6f))
                        {
                            color = blueColor;
                        }
                        else if (value <= equipPercentList[i].normalRange[0] + ((equipPercentList[i].normalRange[1] - equipPercentList[i].normalRange[0]) * 0.9f))
                        {
                            color = purpleColor;
                        }
                        else if (value <= equipPercentList[i].normalRange[1])
                        {
                            color = yellowColor;
                        }

                        break;
                    case RankType.Rare:
                        if (value <= equipPercentList[i].rareRange[0] + ((equipPercentList[i].rareRange[1] - equipPercentList[i].rareRange[0]) * 0.3f))
                        {
                            color = whiteColor;
                        }
                        else if (value <= equipPercentList[i].rareRange[0] + ((equipPercentList[i].rareRange[1] - equipPercentList[i].rareRange[0]) * 0.6f))
                        {
                            color = blueColor;
                        }
                        else if (value <= equipPercentList[i].rareRange[0] + ((equipPercentList[i].rareRange[1] - equipPercentList[i].rareRange[0]) * 0.9f))
                        {
                            color = purpleColor;
                        }
                        else if (value <= equipPercentList[i].rareRange[1])
                        {
                            color = yellowColor;
                        }

                        break;
                    case RankType.Unique:
                        if (value <= equipPercentList[i].uniqueRange[0] + ((equipPercentList[i].uniqueRange[1] - equipPercentList[i].uniqueRange[0]) * 0.3f))
                        {
                            color = whiteColor;
                        }
                        else if (value <= equipPercentList[i].uniqueRange[0] + ((equipPercentList[i].uniqueRange[1] - equipPercentList[i].uniqueRange[0]) * 0.6f))
                        {
                            color = blueColor;
                        }
                        else if (value <= equipPercentList[i].uniqueRange[0] + ((equipPercentList[i].uniqueRange[1] - equipPercentList[i].uniqueRange[0]) * 0.9f))
                        {
                            color = purpleColor;
                        }
                        else if (value <= equipPercentList[i].uniqueRange[1])
                        {
                            color = yellowColor;
                        }

                        break;
                    case RankType.Legendary:
                        if (value <= equipPercentList[i].legendaryRange[0] + ((equipPercentList[i].legendaryRange[1] - equipPercentList[i].legendaryRange[0]) * 0.3f))
                        {
                            color = whiteColor;
                        }
                        else if (value <= equipPercentList[i].legendaryRange[0] + ((equipPercentList[i].legendaryRange[1] - equipPercentList[i].legendaryRange[0]) * 0.6f))
                        {
                            color = blueColor;
                        }
                        else if (value <= equipPercentList[i].legendaryRange[0] + ((equipPercentList[i].legendaryRange[1] - equipPercentList[i].legendaryRange[0]) * 0.9f))
                        {
                            color = purpleColor;
                        }
                        else if (value <= equipPercentList[i].legendaryRange[1])
                        {
                            color = yellowColor;
                        }

                        break;
                }

                break;
            }
        }

        return color;
    }
}
