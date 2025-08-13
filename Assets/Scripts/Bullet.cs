using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;        // Target to move toward
    public float speed = 5f;        // Movement speed of the bullet

    // Assigns the target the bullet will move toward
    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }
    
    // Called every frame to move the bullet towards its target or destroy if no target
    private void Update() {
        if (target != null) {
            // Move bullet toward target at a fixed speed
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } else {
            // Destroy bullet if target no longer exists
            Destroy(gameObject);
        }
    }

    // Called when the bullet collides with another collider
    private void OnCollisionEnter(Collision other) {
        // Increase player's cash by 5 on bullet impact
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().CashChange(5);

        // Destroy the collided object (enemy) and this bullet
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}