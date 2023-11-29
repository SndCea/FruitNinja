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
    public TextMeshProUGUI missingText;
    public List<Cross> missingCrosses;
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
        foreach(Cross cross in missingCrosses)
        {
            cross.Active = false;
        }
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
