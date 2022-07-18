using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnController : MonoBehaviour
{
    [SerializeField] private Vector3 spawnBounds;
    [SerializeField] private Collider[] wallColliders;
    [SerializeField] private GameObject[] asteroidPrefabs;
    private int lastSpawnIndex;
    private float nextSpawnTime = 5f;
    private float spawnMultiplier = 0.95f;
    private float minSpawnTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AsteroidCoroutine());
    }

    private IEnumerator AsteroidCoroutine()
    {
        yield return new WaitForSeconds(nextSpawnTime);
        while (true)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(nextSpawnTime);
            nextSpawnTime = Mathf.Max(minSpawnTime, spawnMultiplier * nextSpawnTime);
        }
    }

    private void SpawnAsteroid()
    {
        // Choose spawn position
        int spawnPositionIndex;
        do
        {
            spawnPositionIndex = Random.Range(0, 4);
        }
        while (spawnPositionIndex == lastSpawnIndex);
        Collider wallCollider = wallColliders[spawnPositionIndex];
        Vector3 spawnPositionVector;
        Vector3 spawnForce;
        switch (spawnPositionIndex)
        {
            // Left
            case 0:
                spawnPositionVector = new Vector3(-spawnBounds.x, 0, 0);
                spawnForce = new Vector3(1, 0, 0);
                break;
            // Top
            case 1:
                spawnPositionVector = new Vector3(0, 0, spawnBounds.z);
                spawnForce = new Vector3(0, 0, -1);
                break;
            // Right
            case 2:
                spawnPositionVector = new Vector3(spawnBounds.x, 0, 0);
                spawnForce = new Vector3(-1, 0, 0);
                break;
            // Bottom
            case 3:
                spawnPositionVector = new Vector3(0, 0, -spawnBounds.z);
                spawnForce = new Vector3(0, 0, 1);
                break;
            default:
                spawnPositionVector = new Vector3(-spawnBounds.x, 0, 0);
                spawnForce = new Vector3(1, 0, 0);
                Debug.Log("Something went wrong here");
                break;
        }

        // Choose asteroid prefab
        int asteroidPrefabIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject asteroidPrefab = asteroidPrefabs[asteroidPrefabIndex];

        // Spawn
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPositionVector, asteroidPrefab.transform.rotation);
        asteroid.GetComponent<AsteroidController>().SetDirectionAndFirstWall(spawnForce, wallCollider);
    }
}
