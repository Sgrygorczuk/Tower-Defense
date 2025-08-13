using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {
    // Target transform that the enemy will follow (the player)
    private Transform _target;
    // Reference to the NavMeshAgent component used for navigation
    private NavMeshAgent _agent;

    private void Start() {
        // Find the player GameObject by tag and get its Transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            _target = player.transform;
        } else {
            Debug.LogWarning("Player object not found! Make sure it is tagged 'Player'.");
        }

        // Get the NavMeshAgent component attached to this GameObject
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null) {
            Debug.LogError("NavMeshAgent component missing from this GameObject.");
        }
    }

    private void Update() {
        // If both target and agent are available, update the agent's destination
        if (_target != null && _agent != null) {
            _agent.SetDestination(_target.position);
        }
    }
}