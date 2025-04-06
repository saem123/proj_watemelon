using UnityEngine;
using UnityEngine.UI;
using Saem;

public class TestAdButton : MonoBehaviour
{
    private Button adButton;

    private void Start()
    {
        adButton = GetComponent<Button>();
        
        adButton.onClick.AddListener(ShowInterstitialAd);
    }

    private void ShowInterstitialAd()
    {
        AdService.instance.ShowInterstitial();
    }

    private void OnDestroy()
    {
        if (adButton != null)
        {
            adButton.onClick.RemoveListener(ShowInterstitialAd);
        }
    }
} 