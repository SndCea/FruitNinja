using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI lastPuntuationText;
    void Start()
    {
        if (PlayerPrefs.GetInt("LastPunctuation") == null)
        {
            PlayerPrefs.SetInt("LastPunctuation", 0);
        }
        lastPuntuationText.text = "Last Punctuation: \n " + PlayerPrefs.GetInt("LastPunctuation") + " points";
    }

    void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }
}
