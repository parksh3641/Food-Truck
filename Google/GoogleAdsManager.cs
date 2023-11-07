using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAdsManager : MonoBehaviour
{
    public static GoogleAdsManager instance;

    public AdmobBanner admobBanner;
    public AdmobReward admobReward_Gold;
    public AdmobReward admobReward_Portion;
    public AdmobScreen admobScreen;
    public AdmobReward admobReward_ChestBoxGold;
    public AdmobReward admobReward_ChestBoxSources;
    public AdmobReward admobReward_ChestBoxCrystal;
    public AdmobReward admobReward_SellPriceTime;
    public AdmobReward admobReward_DefDestroyTime;

    private void Awake()
    {
        instance = this;
    }
}
