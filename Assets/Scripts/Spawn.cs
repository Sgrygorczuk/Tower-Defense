using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject objectToSpawn;     // Prefab to instantiate
    public Transform spawnPoint;          // Location and rotation to spawn the object
    public float spawnInterval = 2f;      // Time between spawns
    public float intervalShrink = 0.005f; // Amount to decrease interval after each spawn
    
    private float _timer = 0f;            // Timer to track time since last spawn

    // Called every frame to handle spawning logic
    void Update() {
        _timer += Time.deltaTime;

        // Spawn object when timer exceeds current interval
        if (_timer >= spawnInterval) {
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            _timer = 0f;
            // Gradually decrease spawn interval but clamp to minimum of 1 second
            spawnInterval -= intervalShrink;
            if (spawnInterval <= 1f) {
                spawnInterval = 1f;
            }
        }
    }
}