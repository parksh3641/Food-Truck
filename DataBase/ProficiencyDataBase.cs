using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProficiencyMotherInfo
{
    public int needExp = 0;
}

[System.Serializable]
public class ProficiencyInfo
{
    public int needExp = 0;
}


[CreateAssetMenu(fileName = "ProficiencyDataBase", menuName = "ScriptableObjects/ProficiencyDataBase")]
public class ProficiencyDataBase : ScriptableObject
{
    public List<ProficiencyMotherInfo> proficiencyMotherInfoList = new List<ProficiencyMotherInfo>();

    public List<ProficiencyInfo> proficiencyInfoList = new List<ProficiencyInfo>();

    public void Initialize()
    {
        proficiencyMotherInfoList.Clear();

        for (int i = 0; i < 200; i++)
        {
            ProficiencyMotherInfo levelInfo = new ProficiencyMotherInfo();
            levelInfo.needExp = 5;

            if (i > 0)
            {
                levelInfo.needExp += 5 * i;
            }

            proficiencyMotherInfoList.Add(levelInfo);
        }
    }

    public int GetMotherLevel(int exp)
    {
        int _exp = 0;
        int level = 0;

        for (int i = 0; i < proficiencyMotherInfoList.Count; i++)
        {
            _exp += proficiencyMotherInfoList[i].needExp;

            if (exp >= _exp)
            {
                level++;
            }
            else
            {
                break;
            }
        }

        return level;
    }

    public int GetMotherNextExp(int level)
    {
        if (level > proficiencyMotherInfoList.Count - 1)
        {
            level = proficiencyMotherInfoList.Count - 1;
        }

        return proficiencyMotherInfoList[level].needExp;
    }

    public int GetMotherNowExp(int exp)
    {
        int _exp = exp;

        for (int i = 0; i < proficiencyMotherInfoList.Count; i++)
        {
            if (_exp >= proficiencyMotherInfoList[i].needExp)
            {
                _exp -= proficiencyMotherInfoList[i].needExp;
            }
            else
            {
                break;
            }
        }

        return _exp;
    }

    public int GetLevel(int exp)
    {
        int _exp = 0;
        int level = 0;

        for (int i = 0; i < proficiencyInfoList.Count; i++)
        {
            _exp += proficiencyInfoList[i].needExp;

            if (exp >= _exp)
            {
                level++;
            }
            else
            {
                break;
            }
        }

        return level;
    }

    public int GetNextExp(int level)
    {
        if (level > proficiencyInfoList.Count - 1)
        {
            level = proficiencyInfoList.Count - 1;
        }

        return proficiencyInfoList[level].needExp;
    }

    public int GetNowExp(int exp)
    {
        int _exp = exp;

        for (int i = 0; i < proficiencyInfoList.Count; i++)
        {
            if (_exp >= proficiencyInfoList[i].needExp)
            {
                _exp -= proficiencyInfoList[i].needExp;
            }
            else
            {
                break;
            }
        }

        return _exp;
    }
}
