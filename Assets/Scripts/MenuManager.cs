using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class MenuManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string adUnitId;
    public TextMeshProUGUI lastPuntuationText;
    public TextMeshProUGUI rewardCanvasText;
    public TextMeshProUGUI rewardMenuText;
    public GameObject MenuCanvas;
    public GameObject RewardCanvas;
    public GameObject NotRewardCanvas;
    private int numFallosOriginal = 3;
    private bool rewardedGiven;
    void Start()
    {
        if (PlayerPrefs.GetInt("LastPunctuation") == null)
        {
            PlayerPrefs.SetInt("LastPunctuation", 0);
        }
        ResetPowerUp();
        rewardMenuText.text = "number of lifes: \n " + PlayerPrefs.GetInt("NumFallos") ;
        lastPuntuationText.text = "Last Punctuation: \n " + PlayerPrefs.GetInt("LastPunctuation") + " points";
        InitializeAds();
    }
    private void InitializeAds() {
        adUnitId = "ca-app-pub-5574905459463986/9952023975";
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadRewardedAd();
        });
    }

    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        var adRequest = new AdRequest.Builder().Build();

        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error.GetMessage());
                    return;
                }

                rewardedAd = ad;
                RegisterEventHandlers(rewardedAd);
            });
    }


    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                //uiScript.OnUserEarnedReward();
                Debug.Log("Reward video cargado, PREMIAR");
            });
        }
    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));


        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadRewardedAd();
        };
    }

    private void Update()
    {
        
    }

    public void Reward()
    {
        if (!rewardedGiven)
        {
            int extraLifes;
            extraLifes = UnityEngine.Random.Range(1, 7);
            extraLifes = PlayerPrefs.GetInt("NumFallos") + extraLifes;
            PlayerPrefs.SetInt("NumFallos", extraLifes);
            rewardCanvasText.text = "in the next game you play with  \n " + extraLifes + " lifes";
            rewardMenuText.text = "number of lifes: \n " + extraLifes;
            MenuCanvas.SetActive(false);
            RewardCanvas.SetActive(true);

            ShowRewardedAd();
            rewardedGiven = true; 
        } else
        {
            ShowNotRewardCanvas();
        }
        
    }
    private void ResetPowerUp()
    {
        PlayerPrefs.SetInt("NumFallos", numFallosOriginal);
        rewardedGiven = false;
        RewardCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }
    public void ShowNotRewardCanvas()
    {
        MenuCanvas.SetActive(false);
        RewardCanvas.SetActive(false);
        NotRewardCanvas.SetActive(true);
    }
    public void BackToMenu()
    {
        RewardCanvas.SetActive(false);
        NotRewardCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
