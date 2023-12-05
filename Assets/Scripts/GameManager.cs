using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    private string adUnitId;
    public static GameManager Instance;
    private AudioSource audioSource;
    public int points;
    public int missing;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;
    public GameObject Cross;
    public GameObject PanelCrosses;
    public Cross [] missingCrosses;
    public GameObject GameOverCanvas;
    public GameObject GameCanvas;
    public GameObject TransitionOut;
    public delegate void GameOverDelegate();
    public event GameOverDelegate GameOverEvent;
    public AudioClip FailAudioClip;



    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(this);
        }

        #endregion
        InicializeGame();

    }
    private void InicializeGame()
    {
        audioSource = GetComponent<AudioSource>();
        this.points = 0;
        scoreText.text = "0";
        //CreateCrosses();
        SetCrosses();
        GameOverCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        RequestInterstitial();
    }
    private void SetCrosses()
    {
        for (int i = 0; i < missingCrosses.Length; i++) 
        {
            bool state;
            missingCrosses[i].Active = false;
            if (i < PlayerPrefs.GetInt("NumFallos"))
            {
                state = true;
            } else
            {
                state = false;
            }
            missingCrosses[i].gameObject.SetActive(state);
        }
    }
    void Start()
    {
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

    public void ShowInterstitial()
    {
        interstitial.Show();
        RegisterEventHandlers(interstitial);
    }
    private void NextScene ()
    {
        TransitionOut.SetActive(true);
    }
    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            NextScene();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            NextScene();
        };
    }
        void Update()
    {
        
    }

    public void ScorePoints()
    {
        this.points ++;
        scoreText.text = this.points.ToString();

    }

    public void MissingDetected ()
    {
        missing++;
        audioSource.clip = FailAudioClip;
        audioSource.Play();
        if (missing >= PlayerPrefs.GetInt("NumFallos"))
        {
            if (GameOverEvent != null)
            {
                
                GameOverEvent();
                gameOverScoreText.text = "Score: " + points.ToString();
                PlayerPrefs.SetInt("LastPunctuation", points);
                GameCanvas.SetActive(false);
                GameOverCanvas.SetActive(true);
            }
            
        } else
        {
            foreach (Cross cross in missingCrosses)
            {
                if (!cross.Active && cross.gameObject.activeSelf)
                {
                    cross.Active = true;
                    break;
                }
            }
        }
        
        
    }

}
