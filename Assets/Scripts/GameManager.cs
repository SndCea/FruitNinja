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
    public TextMeshProUGUI scoreText;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(this);
        }

#endregion

    }
    void Start()
    {
        this.points = 0;
        scoreText.text = "0";
    }

    void Update()
    {
        
    }

    public void ScorePoints()
    {
        this.points ++;
        scoreText.text = this.points.ToString();

    }

}
