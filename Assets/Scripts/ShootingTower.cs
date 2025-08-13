using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : MonoBehaviour {
    public float detectionRadius = 10f;       // Radius to detect targets within range
    public LayerMask detectionLayer;          // Layers that represent valid targets (e.g., Enemy)
    public Transform spawnPoint;               // Position from where projectiles are spawned
    public float detectionInterval = 10f;     // Time interval between shots
    public GameObject projectile;              // Projectile prefab to instantiate
    
    private float _timer = 0f;                 // Timer to track shooting cooldown
    private bool _canShoot = true;             // Flag to check if tower can shoot

    // Called every frame to update detection and shooting logic
    private void Update() {
        DetectObjects();

        // If tower can't shoot, increment timer to wait for cooldown
        if (!_canShoot) {
            _timer += Time.deltaTime;

            if (_timer >= detectionInterval) {
                _timer = 0f;
                _canShoot = true;               // Reset shooting ability after cooldown
            }
        }
    }

    // Detect objects within radius and shoot at the first detected target
    private void DetectObjects() {
        // Get all colliders in detection radius on specified layers
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        // Log detected objects (for debugging)
        foreach (Collider col in hitColliders) {
            Debug.Log("Detected: " + col.name);
        }

        // If targets found and tower can shoot, instantiate projectile and target first enemy
        if (hitColliders.Length > 0 && _canShoot) {
            GameObject bullet = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetTarget(hitColliders[0].transform);
            _canShoot = false;                  // Prevent shooting until cooldown completes
        }
    }

    // Draws a red wire sphere in the editor to visualize detection radius
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}