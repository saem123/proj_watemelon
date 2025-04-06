using System;

public partial class AdService
{
    public void ShowBanner()
    {
        LoadBannerAd();
        ShowBannerAd();
    }

    public void HideBanner()
    {
        HideBannerAd();
    }

    public void ShowInterstitial()
    {
        LoadInterstitialAd();
        ShowInterstitialAd();
    }

    public void ShowRewarded(Action onRewarded = null)
    {
        LoadRewardedAd();
        ShowRewardedAd(onRewarded);
    }
} 