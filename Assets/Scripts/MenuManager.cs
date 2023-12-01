using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class MenuManager : MonoBehaviour
{
    public string appID;
    public string intersitialAdUnityID;
    public string intersitialID;
    private InterstitialAd interstitialAd;
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
    }

    void Update()
    {
        
    }

    //private void InitializeAds()
    //{

    //    MobileAds.Initialize(initStatus => { });
    //    this.interstitialAd = new InterstitialAd(intersitialID);

    //    interstitialAd.LoadAd(new AdRequest.Builder().Build());
    //}
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
