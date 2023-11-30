using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;
    private bool slicing;
    private AudioSource audioSource;

    public GameObject SparkEffect;
    public Vector3 direction {  get; private set; }
    public float minSliceVelocity = 0.01f;
    public float sliceForce = 5f;
    private bool gameover;
    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        StopSlicing();
        gameover = false;
        GameManager.Instance.GameOverEvent += GameOver;

    }
    private void OnDisable()
    {
        StopSlicing();
        GameManager.Instance.GameOverEvent -= GameOver;

    }
    public void GameOver()
    {
        gameover = true;
        StopSlicing();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!gameover) {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                StartSlicing();
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                StopSlicing();
            }
            else if (slicing && Input.touchCount > 0)
            {
                ContinueSlicing();
            }
        }
    }
    private void StartSlicing ()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        SparkEffect.SetActive(true);
        //var emission = SparkEffect.emission; // Stores the module in a local variable
        //emission.enabled = true; // Applies the new value directly to the Particle System
        SoundHit();
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        SparkEffect.SetActive(false);
        //var emission = SparkEffect.emission; // Stores the module in a local variable
        //emission.enabled = false;
        bladeTrail.enabled = false;
    }
    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        direction = newPosition - transform.position;

        //Cuanto se ha movido desde el ultimo frame (como de "grande" es el vector)
        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }

    public void SoundHit()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            return;
        }
    }
}
