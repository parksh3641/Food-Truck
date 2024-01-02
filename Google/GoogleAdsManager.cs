using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAdsManager : MonoBehaviour
{
    public static GoogleAdsManager instance;

    //public AdmobBanner admobBanner;
    //public AdmobScreen admobScreen;
    public AdmobReward admobReward_Gold;
    public AdmobReward admobReward_Portion;
    public AdmobReward admobReward_ChestBoxGold;
    public AdmobReward admobReward_ChestBoxSources;
    public AdmobReward admobReward_ChestBoxCrystal;
    public AdmobReward admobReward_SellPriceTime;
    public AdmobReward admobReward_DefDestroyTime;
    public AdmobReward admobReward_ReincarnationX2;
    public AdmobReward admobReward_Delivery;
    public AdmobReward admobReward_Quest;
    public AdmobReward admobReward_Treasure;
    public AdmobReward admobReward_Buff3;
    public AdmobReward admobReward_Buff4;

    private void Awake()
    {
        instance = this;
    }
}
