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

    private void Awake()
    {
        instance = this;
    }
}
