using UnityEngine;
using System;
using GoogleMobileAds.Api;
using Saem;

public partial class AdService : Service<AdService>
{
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    // AdMob 앱 ID
    private const string APP_ID = "ca-app-pub-3940256099942544~3347511713"; // 실제 앱 ID로 교체 필요

    // 배너 광고 ID
    private const string BANNER_AD_ID = "ca-app-pub-3940256099942544/6300978111"; // 실제 배너 광고 ID로 교체 필요

    // 전면 광고 ID
    private const string INTERSTITIAL_AD_ID = "ca-app-pub-3940256099942544/1033173712"; // 실제 전면 광고 ID로 교체 필요

    // 보상형 광고 ID
    private const string REWARDED_AD_ID = "ca-app-pub-3940256099942544/5224354917"; // 실제 보상형 광고 ID로 교체 필요

    public AdService()
    {
        // AdMob SDK 초기화
        MobileAds.Initialize(initStatus => {
            Debug.Log("AdMob SDK 초기화 완료");
        });
    }

    public void LoadBannerAd()
    {
        // 배너 광고 생성
        bannerView = new BannerView(BANNER_AD_ID, AdSize.Banner, AdPosition.Bottom);
        
        // 광고 요청 생성
        var request = new AdRequest();
        
        // 배너 광고 로드
        bannerView.LoadAd(request);
    }

    public void ShowBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Show();
        }
    }

    public void HideBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }

    public void LoadInterstitialAd()
    {
        // 전면 광고 요청 생성
        var request = new AdRequest();

        // 전면 광고 로드
        InterstitialAd.Load(INTERSTITIAL_AD_ID, request, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("전면 광고 로드 실패: " + error);
                return;
            }

            interstitialAd = ad;
            Debug.Log("전면 광고 로드 완료");
        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
    }

    public void LoadRewardedAd()
    {
        // 보상형 광고 요청 생성
        var request = new AdRequest();

        // 보상형 광고 로드
        RewardedAd.Load(REWARDED_AD_ID, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("보상형 광고 로드 실패: " + error);
                return;
            }

            rewardedAd = ad;
            Debug.Log("보상형 광고 로드 완료");
        });
    }

    public void ShowRewardedAd(Action onRewarded)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                onRewarded?.Invoke();
            });
        }
    }

    private void OnDestroy()
    {
        // 광고 객체 해제
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
} 