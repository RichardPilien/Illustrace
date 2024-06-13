using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform car;
    public Movement carMovement;
    public float initialSpawnDistance = 240f;
    public float spawnDistance = 20f;
    public float minYPosition = -3f;
    public float maxYPosition = 3f; 
    public float minPlatformLength = 5f; 
    public float maxPlatformLength = 15f;
    public float minGap = 50f;
    public float maxGap = 100f;
    public float despawnDistance = 10f;
    public float fallThreshold = -10f;

    private float lastSpawnX;

    void Start()
    {
        lastSpawnX = car.position.x + initialSpawnDistance;
        while (lastSpawnX < car.position.x + spawnDistance)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // Spawn platforms as needed
        while (car.position.x + spawnDistance > lastSpawnX)
        {
            SpawnPlatform();
        }

        // Despawn platforms behind the car
        DespawnPlatforms();

        if (car.position.y < fallThreshold)
        {
            ResetGame();
        }
    }

    void SpawnPlatform()
    {
        float randomYPosition = Random.Range(minYPosition, maxYPosition);
        float randomPlatformLength = Random.Range(minPlatformLength, maxPlatformLength);
        float randomGap = Random.Range(minGap, maxGap);

        Vector3 spawnPosition = new Vector3(lastSpawnX + randomGap, randomYPosition, 80); 
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        newPlatform.transform.localScale = new Vector3(randomPlatformLength, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);

        lastSpawnX += randomPlatformLength + randomGap;
    }

    void DespawnPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            if (platform.transform.position.x < car.position.x - despawnDistance)
            {
                Destroy(platform);
            }
        }
    }

    void ResetGame()
    {
        carMovement.ResetPosition();

        lastSpawnX = car.position.x + initialSpawnDistance;

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            Destroy(platform);
        }

        while (lastSpawnX < car.position.x + spawnDistance)
        {
            SpawnPlatform();
        }
    }
}
