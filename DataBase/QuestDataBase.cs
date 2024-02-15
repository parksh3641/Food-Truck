using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestInfo
{
    public QuestType questType = QuestType.UpgradeCount;
    public int need = 0;
}

[CreateAssetMenu(fileName = "QuestDataBase", menuName = "ScriptableObjects/QuestDataBase")]
public class QuestDataBase : ScriptableObject
{
    public List<QuestInfo> questInfoList = new List<QuestInfo>();

    public RewardType rewardType = RewardType.Gold;
    public int reward = 0;
    public int reward2 = 0;

    public QuestInfo GetQuestInfo(QuestType type)
    {
        QuestInfo truck = new QuestInfo();

        for (int i = 0; i < questInfoList.Count; i++)
        {
            if (questInfoList[i].questType.Equals(type))
            {
                truck = questInfoList[i];
                break;
            }
        }

        return truck;
    }

}
