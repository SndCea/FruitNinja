using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFruit : MonoBehaviour
{
    private Collider spawnArea;
    public GameObject[] fruitsPrefabs;
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 25f;

    public float maxLifetime = 5f;
    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        GameManager.Instance.GameOverEvent += StopFruit;
        StartCoroutine(SpawnFruit());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        GameManager.Instance.GameOverEvent -= StopFruit;
    }
    public void StopFruit()
    {
        StopAllCoroutines();
    }
    private IEnumerator SpawnFruit()
    {
        yield return new WaitForSeconds(2f);
        while (enabled)
        {
            GameObject fruitPrefab = fruitsPrefabs[Random.Range(0, fruitsPrefabs.Length)];
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(fruitPrefab, position, rotation);
            Destroy(fruit, maxLifetime);

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
