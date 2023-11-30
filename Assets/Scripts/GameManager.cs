using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
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
