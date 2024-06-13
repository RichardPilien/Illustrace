using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPlatformSpawner : MonoBehaviour
{
    public GameObject topObstaclePrefab;
    public Transform car;
    public Movement carMovement;
    public float initialSpawnDistance = 240f;
    public float spawnDistance = 20f;   
    public float minYPosition = -3f;
    public float maxYPosition = 3f;
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
        while (car.position.x + spawnDistance > lastSpawnX)
        {
            SpawnPlatform();
        }

        DespawnPlatforms();

        if (car.position.y < fallThreshold)
        {
            ResetGame();
        }
    }

    void SpawnPlatform()
    {
        float randomYPosition = Random.Range(minYPosition, maxYPosition);
        float randomGap = Random.Range(minGap, maxGap);

        Vector3 spawnPosition = new Vector3(lastSpawnX + randomGap, randomYPosition, 80);
        GameObject newPlatform = Instantiate(topObstaclePrefab, spawnPosition, Quaternion.identity);

        newPlatform.transform.localRotation = Quaternion.Euler(newPlatform.transform.localRotation.y, newPlatform.transform.localRotation.y + 90, newPlatform.transform.localRotation.z);

        lastSpawnX += randomGap;
    }

    void DespawnPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("TopPlatform");
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

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("TopPlatform");
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
