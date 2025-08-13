using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour {
    public Camera cam;                  // Camera used for raycasting from mouse position
    public LayerMask groundMask;       // Layer(s) representing valid ground surfaces
    public float hoverHeight = 0.1f;   // Height offset to avoid z-fighting when hovering
    public MeshRenderer mesh;          // Renderer for the hover preview mesh
    public MeshFilter meshFilter;      // MeshFilter to swap preview meshes (mine/tower)

    public Mesh mine;                  // Mesh used for mine preview
    public Mesh tower;                 // Mesh used for tower preview

    public GameObject mineObject;      // Prefab for instantiating a mine
    public GameObject towerObject;     // Prefab for instantiating a tower

    private PlayerController _playerController; // Reference to the player controller script
    public GameObject targetUIObject;  // UI element to detect mouse-over and block build
    public bool isMouseOverTarget = false; // True when mouse is over target UI element
    public GameObject mineParent; 
    
    // Represents the current build mode state
    private enum HoverState {
        Off,    // No build mode active
        Mine,   // Building a mine
        Tower,  // Building a tower
    }

    private HoverState _currentState = HoverState.Off; // Current build state

    // Cache references to components and player controller on start
    private void Start() {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        mesh = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        meshFilter = transform.GetChild(0).gameObject.GetComponent<MeshFilter>();
        mineParent = GameObject.Find("Mines");
    }

    // Physics update called at fixed intervals, handles hovering and building logic
    private void FixedUpdate() {
        if (_currentState != HoverState.Off) {
            isMouseOverTarget = IsMouseOverTarget();
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {
                // Change preview color green if hovering over ground, else red
                if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Ground") {
                    mesh.material.color = Color.green;
                } else {
                    mesh.material.color = Color.red;
                }

                // Position the hover preview slightly above the hit point
                Vector3 hoverPos = hit.point + Vector3.up * hoverHeight;
                transform.position = hoverPos;

                // Cancel build mode on right-click and clear preview mesh
                if (Input.GetMouseButtonDown(1)) {
                    _currentState = HoverState.Mine;
                    meshFilter.mesh = null;
                }

                // On left-click, attempt to build if over ground and not over UI
                if (Input.GetMouseButtonDown(0) && LayerMask.LayerToName(hit.collider.gameObject.layer) == "Ground" && !isMouseOverTarget) {
                    if (_currentState == HoverState.Mine && _playerController.GetCash() >= 50) {
                        Debug.Log("Building Mine");
                        GameObject mine = Instantiate(mineObject, new Vector3(hit.point.x, 0.2f, hit.point.z), Quaternion.identity);
                        mine.transform.SetParent(mineParent.transform);
                        _playerController.CashChange(-50);
                        _currentState = HoverState.Mine;
                        meshFilter.mesh = null;
                    }

                    if (_currentState == HoverState.Tower && _playerController.GetCash() >= 100) {
                        Debug.Log("Building Tower");
                        Instantiate(towerObject, new Vector3(hit.point.x, 0.2f, hit.point.z), Quaternion.identity);
                        _playerController.CashChange(-100);
                        _currentState = HoverState.Mine;
                        meshFilter.mesh = null;
                    }
                }
            }
        }
    }
    
    
    // Returns true if mouse is currently over the specified UI element
    private bool IsMouseOverTarget() {
        if (EventSystem.current == null || targetUIObject == null)
            return false;

        PointerEventData pointerData = new PointerEventData(EventSystem.current) {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results) {
            if (result.gameObject == targetUIObject)
                return true;
        }

        return false;
    }

    // Activate mine build mode and show mine preview mesh
    public void BuildMine() {
        _currentState = HoverState.Mine;
        meshFilter.mesh = mine;
    }

    // Activate tower build mode and show tower preview mesh
    public void BuildTower() {
        _currentState = HoverState.Tower;
        meshFilter.mesh = tower;
    }
}