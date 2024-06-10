using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRandomizer : MonoBehaviour
{
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.45f;
    [SerializeField] private float widthRange = 0.45f;
    [SerializeField] private GameObject _billboard;

    private float timer;

    void Start()
    {
        SpawnObstacle();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (timer > maxTime)
            {
                SpawnObstacle();
                timer = 0;
            }
        }
        timer += Time.deltaTime;
    }

    private void SpawnObstacle()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera not found");
            return;
        }

        Vector3 spawnPos = GetRandomPositionWithinCameraBounds(cam);
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0); // Rotates in 90-degree steps

        GameObject billboard = Instantiate(_billboard, spawnPos, randomRotation);

        Destroy(billboard, 5f);
    }

    private Vector3 GetRandomPositionWithinCameraBounds(Camera cam)
    {
        Vector3 screenPos = new Vector3(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            cam.nearClipPlane + 1f // Ensure the object is within the camera's view
        );

        Vector3 worldPos = cam.ViewportToWorldPoint(screenPos);
        worldPos.z = 0; // Ensure the object spawns at the same z-depth as the spawner

        return worldPos;
    }
}
