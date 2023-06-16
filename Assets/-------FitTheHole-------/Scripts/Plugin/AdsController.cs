
using com.adjust.sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    public bool testAdAppOpen;
    public static AdsController instance;
    DataManager _dataController;
    SoundController soundController;
    public List<String> testDeviceIds = new List<string>();

    public string DevKeyAndroid;
    public string DevKeyIOS;

    public string bannerIdAndroid;
    public string interIdAndroid;
    public string videoIdAndroid;
    public string nativeAdsAndroid;


    public string bannerIdIOS;
    public string interIdIOS;
    public string videoIdIOS;
    public string nativeAdsIOS;


    string bannerId;
    string interId;
    string videoId;
    string nativeId;
    string openAdsId;

    bool loadbannerDone;
    bool doneWatchAds = false;


    private void Start()
    {
        if (_dataController == null)
        {
            _dataController = DataManager.instance;
            soundController = SoundController.instance;


#if UNITY_ANDROID
            appIdTemp = DevKeyAndroid;
            bannerId = bannerIdAndroid;
            interId = interIdAndroid;
            videoId = videoIdAndroid;

#elif UNITY_IOS
                appIdTemp = DevKeyIOS;
                bannerId = bannerIdIOS;
                interId = interIdIOS;
                videoId = videoIdIOS;
         
#endif



            Init();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);

        }
    }
    string appIdTemp;
    private void OnAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {
        double revenue = impressionData.Revenue;
        var impressionParameters = new[] {
  new Firebase.Analytics.Parameter("ad_platform", "AppLovin"),
  new Firebase.Analytics.Parameter("ad_source", impressionData.NetworkName),
  new Firebase.Analytics.Parameter("ad_unit_name", impressionData.AdUnitIdentifier),
  new Firebase.Analytics.Parameter("ad_format", impressionData.AdFormat),
  new Firebase.Analytics.Parameter("value", revenue),
  new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
};
        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression_abi", impressionParameters);

        var info = MaxSdk.GetAdInfo(adUnitId);

        var adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
        adRevenue.setRevenue(info.Revenue, "USD");
        adRevenue.setAdRevenueNetwork(info.NetworkName);
        adRevenue.setAdRevenueUnit(info.AdUnitIdentifier);
        adRevenue.setAdRevenuePlacement(info.Placement);

        Adjust.trackAdRevenue(adRevenue);

    }
    public void Init()
    {

        MaxSdk.SetSdkKey(appIdTemp);
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads
            InitializeInterstitialAds();
            InitializeRewardedAds();
            InitializeBannerAds();
            InitializeMRecAds();
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;

        };
    }
    bool nativeAds;
    public void InitializeMRecAds()
    {
        // MRECs are sized to 300x250 on phones and tablets
        MaxSdk.CreateMRec(nativeAdsAndroid, MaxSdkBase.AdViewPosition.CenterLeft);

        MaxSdkCallbacks.MRec.OnAdLoadedEvent += OnMRecAdLoadedEvent;
        MaxSdkCallbacks.MRec.OnAdLoadFailedEvent += OnMRecAdLoadFailedEvent;
        MaxSdkCallbacks.MRec.OnAdClickedEvent += OnMRecAdClickedEvent;
        MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnMRecAdRevenuePaidEvent;
        MaxSdkCallbacks.MRec.OnAdExpandedEvent += OnMRecAdExpandedEvent;
        MaxSdkCallbacks.MRec.OnAdCollapsedEvent += OnMRecAdCollapsedEvent;
    }
    public void ShowNativeAds()
    {
        if (DataManager.instance.saveData.removeAds)
            return;
        if (nativeAds)
            MaxSdk.ShowMRec(nativeAdsAndroid);
        //HideBanner();
    }
    public void HideNativeAds()
    {
        if (nativeAds)
            MaxSdk.HideMRec(nativeAdsAndroid);
        //ShowBanner();
    }
    public void OnMRecAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        nativeAds = true;
        Debug.LogError("====== load native success:");
    }

    public void OnMRecAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo error)
    {
        nativeAds = false;
        Debug.LogError("====== load native false:" + error.Message);
    }

    public void OnMRecAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    public void OnMRecAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    public void OnMRecAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    public void OnMRecAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    void SdkInitializationCompletedEvent()
    {
        Debug.LogError("======= init suces");
    }
    //void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    //{
    //    Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
    //    Debug.Log("unity - script: I got ImpressionDataReadyEvent allData: " + impressionData.allData);
    //}
    //private void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
    //{
    //    if (impressionData != null)
    //    {
    //        Firebase.Analytics.Parameter[] AdParameters = {
    //    new Firebase.Analytics.Parameter("ad_platform", "ironSource"),
    //    new Firebase.Analytics.Parameter("ad_source", impressionData.adNetwork),
    //    new Firebase.Analytics.Parameter("ad_unit_name", impressionData.instanceName),
    //    new Firebase.Analytics.Parameter("ad_format", impressionData.adUnit),
    //    new Firebase.Analytics.Parameter("currency","USD"),
    //    new Firebase.Analytics.Parameter("value", impressionData.lifetimeRevenue.ToString())
    //  };
    //        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", AdParameters);
    //        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression_abi", AdParameters);
    //    }
    //}
    //private void ImpressionSuccessEventABI(IronSourceImpressionData impressionData)
    //{
    //    if (impressionData != null)
    //    {
    //        Firebase.Analytics.Parameter[] AdParameters = {
    //    new Firebase.Analytics.Parameter("ad_platform", "ironSource"),
    //    new Firebase.Analytics.Parameter("ad_source", impressionData.adNetwork),
    //    new Firebase.Analytics.Parameter("ad_unit_name", impressionData.instanceName),
    //    new Firebase.Analytics.Parameter("ad_format", impressionData.adUnit),
    //    new Firebase.Analytics.Parameter("currency","USD"),
    //    new Firebase.Analytics.Parameter("value", impressionData.lifetimeRevenue.ToString())
    //  };
    //        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression_abi", AdParameters);
    //    }
    //}



    private void OnNativeAdsLoadedFalse(string arg1, int arg2)
    {
        Debug.LogError("=============== native load false");
    }

    private void OnNativeAdsLoaded(string obj)
    {
        Debug.LogError("=============== native load sucess");
    }

    public void InitializeBannerAds()
    {
        MaxSdkCallbacks.OnBannerAdLoadedEvent += BannerAdLoadedEvent;
        MaxSdkCallbacks.OnBannerAdLoadFailedEvent += BannerAdLoadedEventFalse;

#if UNITY_EDITOR

#else
            // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
            // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
            MaxSdk.CreateBanner(bannerId, MaxSdkBase.BannerPosition.BottomCenter);

            // Set background or background color for banners to be fully functional
            MaxSdk.SetBannerBackgroundColor(bannerId, colorBanner);
#endif
    }
    public Color colorBanner;
    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;
        MaxSdkCallbacks.OnInterstitialDisplayedEvent += OnInterstitialDisplayEvent;
        // Load the first interstitial
        RequestInter();
    }



    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardDisplayEvent;

        // Load the first rewarded ad
        RequestVideo();
    }



    int retryAttemptVideo;
    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        retryAttemptVideo = 0;
        EventController.AF_VIDEO_API_CALLED();

        Debug.LogError("========= video load sucess");
    }
    private void OnRewardDisplayEvent(string adUnitId)
    {
        EventController.AF_VIDEO_DISPLAYED();

    }
    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        //retryAttemptVideo++;
        //double retryDelay = Math.Pow(2, Math.Min(6, retryAttemptVideo));

        //Invoke("LoadRewardedAd", (float)retryDelay);

        Debug.LogError("========= video load false");
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        Time.timeScale = 1;
        SoundManager.Instance.SetMuteOnSoundManager();
        MusicManager.Instance.SetMuteOnMusicManager();

        //   RequestVideo();
    }


    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        AppOpenAdManager.ResumeFromAds = false;
        SoundManager.Instance.SetMuteOnSoundManager();
        MusicManager.Instance.SetMuteOnMusicManager();
        StartCoroutine(delayAction());
        // RequestVideo();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        doneWatchAds = true;
        // The rewarded ad displayed and the user should receive the reward.
    }


    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        retryAttemptInter = 0;
        EventController.AF_INTERS_API_CALLED();

        Debug.LogError("========= inter load sucess");
    }
    int retryAttemptInter;
    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        //retryAttemptInter++;
        //double retryDelay = Math.Pow(2, Math.Min(6, retryAttemptInter));

        //Invoke("LoadInterstitial", (float)retryDelay);

        Debug.LogError("========= inter load false");
    }
    private void OnInterstitialDisplayEvent(string adUnitId)
    {
        EventController.AF_INTERS_DISPLAYED();
        EventController.SUM_INTER_ALL_GAME();
        EventController.SHOW_INTER_ADJUST();
        //    EventController.AB_INTER_ID(DataParam.showInterType);

        DataParam.countShowInter++;
        //EventController.AB_INTER(DataParam.countShowInter);
        if (DataParam.countShowInter % 5 == 0)
        {
            EventController.SHOW_INTER_APPFLYER(DataParam.countShowInter);

        }

        SoundManager.Instance.audioSound.mute = true;
        MusicManager.Instance.audioMusic.mute = true;

        Time.timeScale = 0;

        Debug.LogError("=== displayed inter");

    }
    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.

        SoundManager.Instance.SetMuteOnSoundManager();
        MusicManager.Instance.SetMuteOnMusicManager();
        Time.timeScale = 1;
        //    RequestInter();
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        Time.timeScale = 1;
        SoundManager.Instance.SetMuteOnSoundManager();
        MusicManager.Instance.SetMuteOnMusicManager();
        RequestInter();
        DataParam.beginShowInter = System.DateTime.Now;
        AppOpenAdManager.ResumeFromAds = false;
        Debug.LogError("=== bam' close inter");
    }


    public void RequestVideo()
    {
        //   IronSource.Agent.init(appIdTemp, IronSourceAdUnits.REWARDED_VIDEO);
        MaxSdk.LoadRewardedAd(videoId);
    }
    public void RequestInter()
    {
        MaxSdk.LoadInterstitial(interId);
        //     IronSource.Agent.loadInterstitial();
    }

    void RequestBanner()
    {
        //  IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

    }

    private void InterstitialAdClosedEvent()
    {
        Time.timeScale = 1;
        SoundManager.Instance.SetMuteOnSoundManager();
        MusicManager.Instance.SetMuteOnMusicManager();
        RequestInter();
        DataParam.beginShowInter = System.DateTime.Now;
        AppOpenAdManager.ResumeFromAds = false;
    }
    public bool bannerOK;
    private void BannerAdLoadedEvent(string s)
    {
        Debug.LogError("====load banner success ");
        bannerOK = true;
        //HideBanner();
        //ShowBanner();
    }
    private void BannerAdLoadedEventFalse(string s, int i)
    {
        Debug.LogError("====load banner false ");
        bannerOK = false;
    }

    void InterstitialAdShowFailedEvent(/*IronSourceError error*/)
    {
        SoundManager.Instance.SetMuteOnSoundManager();
        MusicManager.Instance.SetMuteOnMusicManager();
        Time.timeScale = 1;
    }

    private void RewardedVideoAdShowFailedEvent(/*IronSourceError obj*/)
    {
        Time.timeScale = 1;
        SoundManager.Instance.SetMuteOnSoundManager();
        MusicManager.Instance.SetMuteOnMusicManager();
    }

    private void RewardedVideoAdRewardedEvent(/*IronSourcePlacement obj*/)
    {
        doneWatchAds = true;
    }
    IEnumerator delayAction()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (doneWatchAds)
        {
            acreward();
            EventController.SHOW_VIDEO_ADJUST();
            EventController.SUM_VIDEO_SHOW_NAME(nameEventVideo);
            if (nameEventVideo.Contains("skip"))
            {
                EventController.SKIP_LEVEL_NAME(DataParam.levelHintOrSkip);
            }
            else if (nameEventVideo == "hint")
            {
                EventController.HINT_LEVEL_NAME(DataParam.levelHintOrSkip);
            }
            DataParam.countShowVideo++;
            //EventController.AB_VIDEO(DataParam.countShowVideo);
            if (DataParam.countShowVideo % 5 == 0 || DataParam.countShowVideo == 2)
            {
                EventController.SHOW_VIDEO_APPFLYER(DataParam.countShowVideo);
            }
        }
        RequestVideo();
        // Debug.LogError("====== close video");
        acreward = null;

        doneWatchAds = false;
        Time.timeScale = 1;
    }

    private void RewardedVideoAdClosedEvent()
    {
        SoundManager.Instance.SetMuteOnSoundManager();
        MusicManager.Instance.SetMuteOnMusicManager();
        StartCoroutine(delayAction());
    }

    Action acreward;
    string nameEventVideo;
    public void ShowVideo(Action _ac, string name)
    {
        if (/*IronSource.Agent.isRewardedVideoAvailable()*/MaxSdk.IsRewardedAdReady(videoId))
        {
            acreward = _ac;
            doneWatchAds = false;
            nameEventVideo = name;
            MaxSdk.ShowRewardedAd(videoId);
            //  IronSource.Agent.showRewardedVideo();
            Time.timeScale = 0;
            SoundManager.Instance.audioSound.mute = true;
            MusicManager.Instance.audioMusic.mute = true;

            EventController.AF_VIDEO_AD_ELIGIBLE();
            //DataParam.afterShowAds = true;
            AppOpenAdManager.ResumeFromAds = true;
            // Debug.LogError("------ video show video");
        }
        else
        {
            //   Debug.LogError("------ video chua load");
            RequestVideo();
        }
    }
    public void ShowInter()
    {

        if (/*IronSource.Agent.isInterstitialReady()*/MaxSdk.IsInterstitialReady(interId))
        {
            //    IronSource.Agent.showInterstitial();
            MaxSdk.ShowInterstitial(interId);
            AppOpenAdManager.ResumeFromAds = true;
            // DataParam.afterShowAds = true;
            //  EventController.INTER_SHOW();
        }
        else
        {
            RequestInter();
        }
    }
    public void ShowBanner()
    {
        if (DataManager.instance.saveData.removeAds)
            return;
        if (bannerOK)
            MaxSdk.ShowBanner(bannerId);
        //if (bannerOK)
        //    IronSource.Agent.displayBanner();
        //else
        //    RequestBanner();
    }
    public void HideBanner()
    {

        if (bannerOK)
            MaxSdk.HideBanner(bannerId);
        //    IronSource.Agent.hideBanner();
    }

}
