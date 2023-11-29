using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneChanger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGame ()
    {
        SceneManager.LoadScene(1);
    }
    

}
