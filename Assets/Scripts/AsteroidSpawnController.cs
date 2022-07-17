using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnController : MonoBehaviour
{
    [SerializeField] private Vector3 spawnBounds;
    [SerializeField] private Collider[] wallColliders;
    [SerializeField] private GameObject[] asteroidPrefabs;
    private float nextSpawnTime = 6f;
    private float timeSinceLastSpawn = 0f;
    private float spawnMultiplier = 0.85f;
    private float minSpawnTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroid();
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        while (timeSinceLastSpawn > nextSpawnTime)
        {
            timeSinceLastSpawn -= nextSpawnTime;
            nextSpawnTime = Mathf.Max(minSpawnTime, spawnMultiplier * nextSpawnTime);
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        GameObject asteroid = Instantiate(asteroidPrefabs[0], new Vector3(-spawnBounds.x, 0, 0), asteroidPrefabs[0].transform.rotation);
        asteroid.GetComponent<AsteroidController>().SetDirectionAndFirstWall(new Vector3(1, 0, 0), wallColliders[0]);
    }
}
