using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelInfo
{
    public int level = 0;
    public int needExp = 0;
}

[CreateAssetMenu(fileName = "LevelDataBase", menuName = "ScriptableObjects/LevelDataBase")]
public class LevelDataBase : ScriptableObject
{
    public List<LevelInfo> levelInfoList = new List<LevelInfo>();

    public void Initialize()
    {
        levelInfoList.Clear();

        for (int i = 0; i < 200; i ++)
        {
            LevelInfo levelInfo = new LevelInfo();
            levelInfo.level = i + 1;

            if(i < 10)
            {
                levelInfo.needExp = (300 + (85 * i)) * 5;
            }
            else
            {
                levelInfo.needExp = (300 + (830 * (i - 9))) * 10;
            }

            levelInfoList.Add(levelInfo);
        }
    }

    public int GetLevel(int exp)
    {
        int _exp = 0;
        int level = 0;

        for(int i = 0; i < levelInfoList.Count; i ++)
        {
            _exp += levelInfoList[i].needExp;

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
        if(level > levelInfoList.Count - 1)
        {
            level = levelInfoList.Count - 1;
        }

        return levelInfoList[level].needExp;
    }

    public int GetNowExp(int exp)
    {
        int _exp = exp;

        for (int i = 0; i < levelInfoList.Count; i++)
        {
            if (_exp >= levelInfoList[i].needExp)
            {
                _exp -= levelInfoList[i].needExp;
            }
            else
            {
                break;
            }
        }

        return _exp;
    }
}
