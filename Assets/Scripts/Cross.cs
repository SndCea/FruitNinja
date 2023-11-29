using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cross : MonoBehaviour
{
    private bool active;

    public bool Active { get => active; 
        set { 
            active = value; 
            if (active)
            {
                MarkCrossRed();
            } else
            {
                MarkCrossBlack();
            }
        } 
    }
    private void MarkCrossRed()
    {
        GetComponent<Image>().color = Color.Lerp(Color.black, Color.HSVToRGB(0f, 100f, 100f), 0.5f);
    }
    private void MarkCrossBlack()
    {
        GetComponent<Image>().color = Color.black;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
