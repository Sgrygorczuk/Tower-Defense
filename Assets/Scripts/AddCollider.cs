using UnityEngine;

public class AddCollider : MonoBehaviour {
    // Adds a BoxCollider component to each direct child of this GameObject at start
    void Start() {
        for (int i = 0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.AddComponent<BoxCollider>(); }
    }
}