using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class MenuManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    private string adUnitId;
    public TextMeshProUGUI lastPuntuationText;
    public TextMeshProUGUI rewardCanvasText;
    public TextMeshProUGUI rewardMenuText;
    public GameObject MenuCanvas;
    public GameObject RewardCanvas;
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
        //InitializeAds();
        RequestInterstitial();
    }
    private void RequestInterstitial()
    {
        adUnitId = "ca-app-pub-5574905459463986/2361330646";

        // Clean up interstitial before using it
        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        AdRequest request = new AdRequest.Builder().Build();
        InterstitialAd.Load(adUnitId, request, (InterstitialAd ad, LoadAdError loadAdError) =>
        {
            if (loadAdError != null)
            {
                return;
            }
            else
            if (ad == null)
            {
                return;
            }
            Debug.Log("Interstitial ad loaded");
            interstitial = ad;

        });
    }
    private void Update()
    {
        
    }

    public void Reward()
    {
        if (!rewardedGiven)
        {
            int extraLifes;
            extraLifes = Random.Range(1, 7);
            extraLifes = PlayerPrefs.GetInt("NumFallos") + extraLifes;
            PlayerPrefs.SetInt("NumFallos", extraLifes);
            rewardCanvasText.text = "in the next game you play with  \n " + extraLifes + " lifes";
            rewardMenuText.text = "number of lifes: \n " + extraLifes;
            MenuCanvas.SetActive(false);
            RewardCanvas.SetActive(true);

            rewardedGiven = true; 
            interstitial.Show();
        }
        
    }
    private void ResetPowerUp()
    {
        PlayerPrefs.SetInt("NumFallos", numFallosOriginal);
        rewardedGiven = false;
        RewardCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }
    public void BackToMenu()
    {
        MenuCanvas.SetActive(true);
        RewardCanvas.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
