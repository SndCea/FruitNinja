using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int points;
    public int missing;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI missingText;
    public List<Cross> missingCrosses;
    public GameObject GameOverCanvas;
    public GameObject GameCanvas;
    public delegate void GameOverDelegate();
    public event GameOverDelegate GameOverEvent;


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
        Debug.Log("INICIO");
        this.points = 0;
        scoreText.text = "0";
        foreach (Cross cross in missingCrosses)
        {
            cross.Active = false;
        }
        GameOverCanvas.SetActive(false);
        GameCanvas.SetActive(true);
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
        if (missing >= missingCrosses.Count)
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
                if (!cross.Active)
                {
                    cross.Active = true;
                    break;
                }
            }
        }
        
        
    }

}
