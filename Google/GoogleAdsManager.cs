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

    private void Awake()
    {
        instance = this;
    }
}
