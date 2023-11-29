using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingDetector : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.MissingDetected();
    }
}
