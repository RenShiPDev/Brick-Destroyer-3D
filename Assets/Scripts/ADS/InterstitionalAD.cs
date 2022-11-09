using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;
using UnityEngine.SceneManagement;

public class InterstitionalAD : MonoBehaviour
{
/*
    private Interstitial _interstitial;

    private void Start()
    {
        RequestInterstitial();
    }

    public void ShowOnDisplay()
    {
        ShowInterstitial();
    }

    private void RequestInterstitial()
    {
        string adUnitId = "R-M-*******-*";
        _interstitial = new Interstitial(adUnitId);

        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);

        _interstitial.OnInterstitialFailedToLoad += HandleInterstitialFailedToLoad;

        _interstitial.OnReturnedToApplication += Completed;
        _interstitial.OnLeftApplication += Completed;
        _interstitial.OnInterstitialShown += Completed;
        _interstitial.OnInterstitialDismissed += Completed;
        _interstitial.OnImpression += Completed;
    }

    private void ShowInterstitial()
    {
        if (this._interstitial.IsLoaded())
        {
            PlayerPrefs.SetInt("AdsCounter", 0);
            _interstitial.Show();
            Debug.Log("Showed");
        }
        else
        {
            Debug.Log("NotShow");
        }
    }
    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        Debug.Log("Failed to load");
    }
    public void Completed(object sender, System.EventArgs args)
    {
        Debug.Log("Completed");
    }*/
}
