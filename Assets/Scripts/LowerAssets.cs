using UnityEngine;

public class LowerAssets : MonoBehaviour {
    // Set initial position slightly above ground on start
    void Start() {
        transform.position = new Vector3(0, 0.2f, 0);
    }
}